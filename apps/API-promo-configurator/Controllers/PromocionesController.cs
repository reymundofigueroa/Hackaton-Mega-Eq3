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
    /// <summary>
    /// Controlador para la gestión de promociones.
    /// Permite consultar promociones, obtener detalles y crear promociones completas con servicios y alcances asociados.
    /// </summary>
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

        /// <summary>
        /// Obtiene la lista de todas las promociones registradas.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de promociones con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de promociones</response>
        /// <response code="403">No autorizado para acceder a las promociones</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromociones()
        {
            var promociones = _promocionRepository.GetPromociones();
            var promocionesDto = promociones.Select(p => _mapper.Map<PromocionDto>(p)).ToList();
            return Ok(promocionesDto);
        }

        /// <summary>
        /// Obtiene una promoción específica por su identificador.
        /// </summary>
        /// <param name="promocionId">Identificador de la promoción</param>
        /// <returns>Promoción encontrada</returns>
        /// <response code="200">Promoción encontrada</response>
        /// <response code="400">Petición incorrecta</response>
        /// <response code="403">No autorizado</response>
        /// <response code="404">Promoción no encontrada</response>
        [HttpGet("{promocionId:int}", Name = "GetPromocion")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocion(int promocionId) // El nombre del parametro debe ser igual al reflejado en la ruta de la propiedad
        {
            var promocion = _promocionRepository.GetPromocion(promocionId);

            if (promocion == null)
            {
                return NotFound($"La promoción con el ID {promocionId} no existe");
            }

            var promocionDto = _mapper.Map<PromocionDto>(promocion);

            return Ok(promocionDto);
        }

        /// <summary>
        /// Crea una promoción completa con servicios y alcances asociados.
        /// </summary>
        /// <param name="dto">Datos de la promoción completa a crear</param>
        /// <returns>Resultado de la creación de la promoción</returns>
        /// <remarks>
        /// Ejemplo de petición:
        ///
        ///     POST /api/Promociones/crear-completa
        ///     {
        ///         "nombre": "Promo Verano",
        ///         "descripcion": "Descuento especial de verano",
        ///         "tipoDescuento": "PORCENTAJE",
        ///         "valorDescuento": 0.15,
        ///         "aplicaA": "MENSUALIDAD",
        ///         "duracionMeses": 3,
        ///         "idServicios": [1, 2],
        ///         "alcances": [
        ///             {
        ///                 "idEstado": 1,
        ///                 "idMunicipio": 2,
        ///                 "idCiudad": 3,
        ///                 "idColonia": 4,
        ///                 "idSucursal": 5
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Promoción creada exitosamente</response>
        /// <response code="400">Datos inválidos o error al crear la promoción</response>
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
                        promocion.Servicios.Add(servicio);
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