using API_promo_configurator.Data;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de sucursales.
    /// Permite consultar la lista de sucursales, obtener información de una sucursal específica y consultar información geográfica completa de las sucursales.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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

        /// <summary>
        /// Obtiene la lista de todas las sucursales registradas.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de sucursales con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de sucursales</response>
        /// <response code="403">No autorizado para acceder a las sucursales</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursales()
        {
            var sucursales = _sucursalRepository.GetSucursales();
            var sucursalesDto = sucursales.Select(s => _mapper.Map<SucursalDto>(s)).ToList();
            return Ok(sucursalesDto);
        }

        /// <summary>
        /// Obtiene una sucursal específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la sucursal</param>
        /// <returns>Sucursal encontrada</returns>
        /// <response code="200">Sucursal encontrada</response>
        /// <response code="404">Sucursal no encontrada</response>
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

        /// <summary>
        /// Obtiene la información geográfica completa de todas las sucursales, incluyendo colonias, ciudades, municipios y estados.
        /// </summary>
        /// <returns>Lista de sucursales con información geográfica detallada</returns>
        /// <response code="200">Lista de sucursales con información completa encontrada</response>
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