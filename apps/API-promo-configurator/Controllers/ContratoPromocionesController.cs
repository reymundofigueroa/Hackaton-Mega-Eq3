using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de promociones asignadas a contratos.
    /// Permite consultar promociones de contratos, obtener detalles y asignar promociones a contratos específicos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoPromocionesController : ControllerBase
    {
        private readonly IContratoPromocionRepository _contratoPromocionRepository;
        private readonly IMapper _mapper;

        public ContratoPromocionesController(IContratoPromocionRepository contratoPromocionRepository, IMapper mapper)
        {
            _contratoPromocionRepository = contratoPromocionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de todas las promociones asignadas a contratos.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de promociones asignadas a contratos con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de promociones de contratos</response>
        /// <response code="403">No autorizado para acceder a las promociones de contratos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoPromociones()
        {
            var contratoPromociones = _contratoPromocionRepository.GetContratoPromociones();
            var contratoPromocionesDto = contratoPromociones.Select(cp => _mapper.Map<ContratoPromocionDto>(cp)).ToList();
            return Ok(contratoPromocionesDto);
        }

        /// <summary>
        /// Obtiene una promoción de contrato específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la promoción de contrato</param>
        /// <returns>Promoción de contrato encontrada</returns>
        /// <response code="200">Promoción de contrato encontrada</response>
        /// <response code="404">Promoción de contrato no encontrada</response>
        [HttpGet("{id}", Name = "GetContratoPromocion")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoPromocion(int id)
        {
            var contratoPromocion = _contratoPromocionRepository.GetContratoPromocion(id);
            if (contratoPromocion == null)
                return NotFound();
            return Ok(_mapper.Map<ContratoPromocionDto>(contratoPromocion));
        }

        /// <summary>
        /// Obtiene las promociones asignadas a un contrato específico.
        /// </summary>
        /// <param name="idContrato">Identificador del contrato</param>
        /// <returns>Lista de promociones asignadas al contrato</returns>
        /// <response code="200">Lista de promociones del contrato encontrada</response>
        [HttpGet("contrato/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocionesPorContrato(int idContrato)
        {
            var promociones = _contratoPromocionRepository.GetPromocionesPorContrato(idContrato);
            var promocionesDto = promociones.Select(cp => _mapper.Map<ContratoPromocionDto>(cp)).ToList();
            return Ok(promocionesDto);
        }

        /// <summary>
        /// Asigna una promoción a un contrato específico.
        /// </summary>
        /// <param name="createContratoPromocionDto">Datos de la promoción a asignar al contrato</param>
        /// <returns>Promoción de contrato creada</returns>
        /// <remarks>
        /// Ejemplo de petición:
        ///
        ///     POST /api/ContratoPromociones
        ///     {
        ///         "idContrato": 1,
        ///         "idPromocion": 2,
        ///         "fechaAplicacion": "2025-07-01T00:18:22.015Z",
        ///         "metadata": "Promoción aplicada por campaña de verano"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Promoción asignada exitosamente al contrato</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">No autorizado para asignar promociones</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AsignarPromoContrato([FromBody] CreateContratoPromocionDto createContratoPromocionDto)
        {
            if (createContratoPromocionDto == null)
            {
                return BadRequest(ModelState);
            }

            // TODO: Verificar que el contrato no tenga ya esa promoción asignada

            var promocionContrato = _mapper.Map<ContratoPromocione>(createContratoPromocionDto);

            if (!_contratoPromocionRepository.CreateContratoPromocion(promocionContrato))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al asignar la promoción al contrato con el ID {promocionContrato.IdContrato}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetContratoPromocion", new { id = promocionContrato.IdContratoPromocion }, promocionContrato);
        }
    }
} 