using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de suscriptores.
    /// Permite consultar la lista de suscriptores y obtener información de un suscriptor específico.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        private readonly ISuscriptorRepository _suscriptorRepository;
        private readonly IMapper _mapper;

        public SuscriptoresController(ISuscriptorRepository suscriptorRepository, IMapper mapper)
        {
            _suscriptorRepository = suscriptorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de todos los suscriptores registrados.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de suscriptores con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de suscriptores</response>
        /// <response code="403">No autorizado para acceder a los suscriptores</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSuscriptores()
        {
            var suscriptores = _suscriptorRepository.GetSuscriptores();
            var suscriptoresDto = suscriptores.Select(s => _mapper.Map<SuscriptorDto>(s)).ToList();
            return Ok(suscriptoresDto);
        }

        /// <summary>
        /// Obtiene un suscriptor específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del suscriptor</param>
        /// <returns>Suscriptor encontrado</returns>
        /// <response code="200">Suscriptor encontrado</response>
        /// <response code="404">Suscriptor no encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSuscriptor(int id)
        {
            var suscriptor = _suscriptorRepository.GetSuscriptor(id);
            if (suscriptor == null)
                return NotFound();
            return Ok(_mapper.Map<SuscriptorDto>(suscriptor));
        }
    }
} 