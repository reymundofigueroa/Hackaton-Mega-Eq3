using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de servicios.
    /// Permite consultar la lista de servicios disponibles en el sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        // Inyección de dependencias necesario para poder trabajar con los métodos y los DTOs del modelo original
        private readonly IServicioRepository _servicioRepository;
        private readonly IMapper _mapper;

        public ServiciosController(IServicioRepository servicioRepository, IMapper mapper)
        {
            _servicioRepository = servicioRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de todos los servicios registrados.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de servicios con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de servicios</response>
        /// <response code="403">No autorizado para acceder a los servicios</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // El usuario no está autorizado a acceder a este recurso
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetServicios()
        {
            var servicios = _servicioRepository.GetServicios();
            var serviciosDto = new List<ServicioDto>();

            foreach (var servicio in servicios)
            {
                serviciosDto.Add(_mapper.Map<ServicioDto>(servicio));
            }

            return Ok(serviciosDto);
        }
    }
}
