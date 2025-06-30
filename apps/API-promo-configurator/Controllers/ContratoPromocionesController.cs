using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoPromociones()
        {
            var contratoPromociones = _contratoPromocionRepository.GetContratoPromociones();
            var contratoPromocionesDto = contratoPromociones.Select(cp => _mapper.Map<ContratoPromocionDto>(cp)).ToList();
            return Ok(contratoPromocionesDto);
        }

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

        [HttpGet("contrato/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocionesPorContrato(int idContrato)
        {
            var promociones = _contratoPromocionRepository.GetPromocionesPorContrato(idContrato);
            var promocionesDto = promociones.Select(cp => _mapper.Map<ContratoPromocionDto>(cp)).ToList();
            return Ok(promocionesDto);
        }

        // Endpoint para asignar una promoción a un contrato
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // El usuario no está autorizado a acceder a este recurso
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Realizó mala petición
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Realizó mala petición
        [ProducesResponseType(StatusCodes.Status201Created)] // Se creó correctamente
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Error en el servidor
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