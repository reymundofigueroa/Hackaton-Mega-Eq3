using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IMapper _mapper;

        public MunicipiosController(IMunicipioRepository municipioRepository, IMapper mapper)
        {
            _municipioRepository = municipioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMunicipios()
        {
            var municipios = _municipioRepository.GetMunicipios();
            var municipiosDto = municipios.Select(m => _mapper.Map<MunicipioDto>(m)).ToList();
            return Ok(municipiosDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMunicipio(int id)
        {
            var municipio = _municipioRepository.GetMunicipio(id);
            if (municipio == null)
                return NotFound();
            return Ok(_mapper.Map<MunicipioDto>(municipio));
        }
    }
} 