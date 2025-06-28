using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosCuentaController : ControllerBase
    {
        private readonly IMovimientosCuentaRepository _movimientosCuentaRepository;
        private readonly IMapper _mapper;

        public MovimientosCuentaController(IMovimientosCuentaRepository movimientosCuentaRepository, IMapper mapper)
        {
            _movimientosCuentaRepository = movimientosCuentaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientosCuenta()
        {
            var movimientos = _movimientosCuentaRepository.GetMovimientosCuenta();
            var movimientosDto = movimientos.Select(m => _mapper.Map<MovimientosCuentaDto>(m)).ToList();
            return Ok(movimientosDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientoCuenta(long id)
        {
            var movimiento = _movimientosCuentaRepository.GetMovimientoCuenta(id);
            if (movimiento == null)
                return NotFound();
            return Ok(_mapper.Map<MovimientosCuentaDto>(movimiento));
        }

        [HttpGet("contrato/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientosPorContrato(int idContrato)
        {
            var movimientos = _movimientosCuentaRepository.GetMovimientosPorContrato(idContrato);
            var movimientosDto = movimientos.Select(m => _mapper.Map<MovimientosCuentaDto>(m)).ToList();
            return Ok(movimientosDto);
        }
    }
} 