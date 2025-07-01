using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionAlcancesController : ControllerBase
    {
        private readonly IPromocionAlcanceRepository _promocionAlcanceRepository;
        private readonly IMapper _mapper;

        public PromocionAlcancesController(IPromocionAlcanceRepository promocionAlcanceRepository, IMapper mapper)
        {
            _promocionAlcanceRepository = promocionAlcanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocionAlcances()
        {
            var promocionAlcances = _promocionAlcanceRepository.GetPromocionAlcances();
            var promocionAlcancesDto = promocionAlcances.Select(pa => _mapper.Map<PromocionAlcanceDto>(pa)).ToList();
            return Ok(promocionAlcancesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPromocionAlcance(int id)
        {
            var promocionAlcance = _promocionAlcanceRepository.GetPromocionAlcance(id);
            if (promocionAlcance == null)
                return NotFound();
            return Ok(_mapper.Map<PromocionAlcanceDto>(promocionAlcance));
        }

        [HttpGet("promocion/{idPromocion}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAlcancesPorPromocion(int idPromocion)
        {
            var alcances = _promocionAlcanceRepository.GetAlcancesPorPromocion(idPromocion);
            var alcancesDto = alcances.Select(pa => _mapper.Map<PromocionAlcanceDto>(pa)).ToList();
            return Ok(alcancesDto);
        }
    }
} 