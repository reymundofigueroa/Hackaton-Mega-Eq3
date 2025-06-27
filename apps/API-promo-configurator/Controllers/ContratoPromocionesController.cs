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

        [HttpGet("{id}")]
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
    }
} 