using API_promo_configurator.Data;
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
    [EnableCors("AllowSpecificOrigin")] 
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public SucursalesController(ISucursalRepository sucursalRepository, IMapper mapper, ApplicationDbContext db)
        {
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursales()
        {
            var sucursales = _sucursalRepository.GetSucursales();
            var sucursalesDto = sucursales.Select(s => _mapper.Map<SucursalDto>(s)).ToList();
            return Ok(sucursalesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursal(int id)
        {
            var sucursal = _sucursalRepository.GetSucursal(id);
            if (sucursal == null)
                return NotFound();
            return Ok(_mapper.Map<SucursalDto>(sucursal));
        }

        [HttpGet("info-completa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursalesInfo()
        {
            var sucursales = _db.Sucursales
                .Include(s => s.IdColonia)
                    .ThenInclude(c => c.IdCiudadNavigation)
                        .ThenInclude(ci => ci.IdMunicipioNavigation)
                            .ThenInclude(m => m.IdEstadoNavigation)
                .Select(s => new
                {
                    s.IdSucursal,
                    s.Nombre,
                    s.Telefono,
                    s.IdDomicilio,
                    Colonias = s.IdColonia.Select(c => new
                    {
                        c.IdColonia,
                        c.Nombre,
                        c.CodigoPostal,
                        Ciudad = new
                        {
                            c.IdCiudadNavigation.IdCiudad,
                            c.IdCiudadNavigation.Nombre,
                            Municipio = new
                            {
                                c.IdCiudadNavigation.IdMunicipioNavigation.IdMunicipio,
                                c.IdCiudadNavigation.IdMunicipioNavigation.Nombre,
                                Estado = new
                                {
                                    c.IdCiudadNavigation.IdMunicipioNavigation.IdEstadoNavigation.IdEstado,
                                    c.IdCiudadNavigation.IdMunicipioNavigation.IdEstadoNavigation.Nombre
                                }
                            }
                        }
                    }).ToList()
                })
                .ToList();

            return Ok(sucursales);
        }
    }
} 