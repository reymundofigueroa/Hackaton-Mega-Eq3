using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de servicios asignados a contratos.
    /// Permite consultar servicios de contratos, obtener detalles y consultar servicios por contrato específico.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoServiciosController : ControllerBase
    {
        private readonly IContratoServicioRepository _contratoServicioRepository;
        private readonly IMapper _mapper;

        public ContratoServiciosController(IContratoServicioRepository contratoServicioRepository, IMapper mapper)
        {
            _contratoServicioRepository = contratoServicioRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de todos los servicios asignados a contratos.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de servicios asignados a contratos con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de servicios de contratos</response>
        /// <response code="403">No autorizado para acceder a los servicios de contratos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoServicios()
        {
            var contratoServicios = _contratoServicioRepository.GetContratoServicios();
            var contratoServiciosDto = contratoServicios.Select(cs => _mapper.Map<ContratoServicioDto>(cs)).ToList();
            return Ok(contratoServiciosDto);
        }

        /// <summary>
        /// Obtiene un servicio de contrato específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del servicio de contrato</param>
        /// <returns>Servicio de contrato encontrado</returns>
        /// <response code="200">Servicio de contrato encontrado</response>
        /// <response code="404">Servicio de contrato no encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoServicio(int id)
        {
            var contratoServicio = _contratoServicioRepository.GetContratoServicio(id);
            if (contratoServicio == null)
                return NotFound();
            return Ok(_mapper.Map<ContratoServicioDto>(contratoServicio));
        }

        /// <summary>
        /// Obtiene los servicios asignados a un contrato específico.
        /// </summary>
        /// <param name="idContrato">Identificador del contrato</param>
        /// <returns>Lista de servicios asignados al contrato</returns>
        /// <response code="200">Lista de servicios del contrato encontrada</response>
        [HttpGet("contrato/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetServiciosPorContrato(int idContrato)
        {
            var servicios = _contratoServicioRepository.GetServiciosPorContrato(idContrato);
            var serviciosDto = servicios.Select(cs => _mapper.Map<ContratoServicioDto>(cs)).ToList();
            return Ok(serviciosDto);
        }

    }
}