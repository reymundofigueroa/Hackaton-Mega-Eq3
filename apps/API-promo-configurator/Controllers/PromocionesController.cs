using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionesController : ControllerBase
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public PromocionesController(IPromocionRepository promocionRepository, IMapper mapper, ApplicationDbContext db)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromociones()
        {
            var promociones = _promocionRepository.GetPromociones();
            var promocionesDto = promociones.Select(p => _mapper.Map<PromocionDto>(p)).ToList();
            return Ok(promocionesDto);
        }

        [HttpPost("crear-completa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearPromocionCompleta([FromBody] PromocionCompletaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var transaction = await _db.Database.BeginTransactionAsync();
            
            try
            {
                // 1. Crear la promoción
                var promocion = _mapper.Map<Promocione>(dto);
                _db.Promociones.Add(promocion);
                await _db.SaveChangesAsync(); // Esto genera el ID
                
                // 2. Crear servicios asociados (solo si se envían)
                if (dto.IdServicios != null && dto.IdServicios.Any())
                {
                    var servicios = await _db.Servicios
                        .Where(s => dto.IdServicios.Contains(s.IdServicio))
                        .ToListAsync();
                    
                    if (servicios.Count != dto.IdServicios.Count)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest("Algunos servicios especificados no existen");
                    }
                    
                    // Agregar servicios uno por uno a la colección de navegación
                    foreach (var servicio in servicios)
                    {
                        promocion.IdServicios.Add(servicio);
                    }
                }
                
                // 3. Crear alcances (solo si se envían)
                if (dto.Alcances != null && dto.Alcances.Any())
                {
                    var alcances = dto.Alcances.Select(a => new PromocionAlcance
                    {
                        IdPromocion = promocion.IdPromocion,
                        IdEstado = a.IdEstado,
                        IdMunicipio = a.IdMunicipio,
                        IdCiudad = a.IdCiudad,
                        IdColonia = a.IdColonia,
                        IdSucursal = a.IdSucursal
                    });
                    
                    _db.PromocionAlcances.AddRange(alcances);
                }
                
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return CreatedAtAction(nameof(GetPromociones), new { id = promocion.IdPromocion }, new { 
                    IdPromocion = promocion.IdPromocion,
                    Mensaje = "Promoción creada exitosamente",
                    ServiciosAsociados = dto.IdServicios?.Count ?? 0,
                    AlcancesCreados = dto.Alcances?.Count ?? 0
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al crear la promoción: {ex.Message}");
            }
        }

    }
}