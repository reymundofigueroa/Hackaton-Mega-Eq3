using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoniasController : ControllerBase
    {
        private readonly IColoniaRepository _coloniaRepository;
        private readonly IMapper _mapper;

        public ColoniasController(IColoniaRepository coloniaRepository, IMapper mapper)
        {
            _coloniaRepository = coloniaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetColonias()
        {
            var colonias = _coloniaRepository.GetColonias();
            var coloniasDto = colonias.Select(c => _mapper.Map<ColoniaDto>(c)).ToList();
            return Ok(coloniasDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetColonia(int id)
        {
            var colonia = _coloniaRepository.GetColonia(id);
            if (colonia == null)
                return NotFound();
            return Ok(_mapper.Map<ColoniaDto>(colonia));
        }
    }
} 