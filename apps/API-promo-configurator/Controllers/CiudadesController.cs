using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : ControllerBase
    {
        private readonly ICiudadRepository _ciudadRepository;
        private readonly IMapper _mapper;

        public CiudadesController(ICiudadRepository ciudadRepository, IMapper mapper)
        {
            _ciudadRepository = ciudadRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCiudades()
        {
            var ciudades = _ciudadRepository.GetCiudades();
            var ciudadesDto = ciudades.Select(c => _mapper.Map<CiudadDto>(c)).ToList();
            return Ok(ciudadesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCiudad(int id)
        {
            var ciudad = _ciudadRepository.GetCiudad(id);
            if (ciudad == null)
                return NotFound();
            return Ok(_mapper.Map<CiudadDto>(ciudad));
        }
    }
} 