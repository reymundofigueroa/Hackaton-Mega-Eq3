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
    public class ContratosController : ControllerBase
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IMapper _mapper;

        public ContratosController(IContratoRepository contratoRepository, IMapper mapper)
        {
            _contratoRepository = contratoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratos()
        {
            var contratos = _contratoRepository.GetContratos();
            var contratosDto = contratos.Select(c => _mapper.Map<ContratoDto>(c)).ToList();
            return Ok(contratosDto);
        }

        [HttpGet("{id:int}", Name = "GetContrato")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContrato(int id)
        {
            var contrato = _contratoRepository.GetContrato(id);
            if (contrato == null)
            {
                return NotFound($"El contrato con el Id {id} no fue encontrado");

            }

            var contratoDto = _mapper.Map<ContratoDto>(contrato);

            return Ok(_mapper.Map<ContratoDto>(contratoDto));
        }

    }
}