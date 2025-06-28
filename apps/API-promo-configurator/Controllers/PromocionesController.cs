using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")] 
    public class PromocionesController : ControllerBase
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;

        public PromocionesController(IPromocionRepository promocionRepository, IMapper mapper)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
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

        [HttpGet("{IdPromocion:int}", Name = "GetPromocion")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocion(int IdPromocion)
        {
            var promocion = _promocionRepository.GetPromocion(IdPromocion);

            if (promocion == null)
            {
                return NotFound($"La promoción con el Id {IdPromocion} no existe");
            }

            var promocionDto = _mapper.Map<PromocionDto>(promocion);

            return Ok(promocionDto);
        }
    }
}