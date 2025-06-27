using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomiciliosController : ControllerBase
    {
        private readonly IDomicilioRepository _domicilioRepository;
        private readonly IMapper _mapper;

        public DomiciliosController(IDomicilioRepository domicilioRepository, IMapper mapper)
        {
            _domicilioRepository = domicilioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDomicilios()
        {
            var domicilios = _domicilioRepository.GetDomicilios();
            var domiciliosDto = domicilios.Select(d => _mapper.Map<DomicilioDto>(d)).ToList();
            return Ok(domiciliosDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDomicilio(int id)
        {
            var domicilio = _domicilioRepository.GetDomicilio(id);
            if (domicilio == null)
                return NotFound();
            return Ok(_mapper.Map<DomicilioDto>(domicilio));
        }
    }
} 