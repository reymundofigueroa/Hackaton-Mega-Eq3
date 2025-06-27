using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSuscriptores()
        {
            var suscriptores = _suscriptorRepository.GetSuscriptores();
            var suscriptoresDto = suscriptores.Select(s => _mapper.Map<SuscriptorDto>(s)).ToList();
            return Ok(suscriptoresDto);
        }

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