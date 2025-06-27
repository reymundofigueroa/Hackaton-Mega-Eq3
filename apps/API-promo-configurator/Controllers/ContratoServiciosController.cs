using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoServiciosController : ControllerBase
    {
        private readonly IContratoServicioRepository _contratoServicioRepository;
        private readonly IMapper _mapper;

        public ContratoServiciosController(IContratoServicioRepository contratoServicioRepository, IMapper mapper)
        {
            _contratoServicioRepository = contratoServicioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoServicios()
        {
            var contratoServicios = _contratoServicioRepository.GetContratoServicios();
            var contratoServiciosDto = contratoServicios.Select(cs => _mapper.Map<ContratoServicioDto>(cs)).ToList();
            return Ok(contratoServiciosDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratoServicio(int id)
        {
            var contratoServicio = _contratoServicioRepository.GetContratoServicio(id);
            if (contratoServicio == null)
                return NotFound();
            return Ok(_mapper.Map<ContratoServicioDto>(contratoServicio));
        }

        [HttpGet("contrato/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetServiciosPorContrato(int idContrato)
        {
            var servicios = _contratoServicioRepository.GetServiciosPorContrato(idContrato);
            var serviciosDto = servicios.Select(cs => _mapper.Map<ContratoServicioDto>(cs)).ToList();
            return Ok(serviciosDto);
        }

        // Puedes agregar aquí métodos POST, PUT, DELETE si lo necesitas
    }
}