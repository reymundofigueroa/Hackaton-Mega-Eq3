using API_promo_configurator.Data;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class MovimientosCuentaController : ControllerBase
    {
        private readonly IMovimientosCuentaRepository _movimientosCuentaRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public MovimientosCuentaController(IMovimientosCuentaRepository movimientosCuentaRepository, IMapper mapper, ApplicationDbContext db)
        {
            _movimientosCuentaRepository = movimientosCuentaRepository;
            _mapper = mapper;
            _db = db;
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

        [HttpGet("suscriptor/{idSuscriptor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientosPorSuscriptor(int idSuscriptor)
        {
            var movimientos = _db.MovimientosCuenta
                .Include(m => m.IdContratoNavigation)
                .Where(m => m.IdContratoNavigation.IdSuscriptor == idSuscriptor)
                .Select(m => new
                {
                    m.IdMovimiento,
                    m.IdContrato,
                    m.FechaMovimiento,
                    m.Concepto,
                    m.MontoCargo,
                    m.MontoPago,
                    m.SaldoResultante
                })
                .OrderBy(m => m.FechaMovimiento)
                .ToList();

            return Ok(movimientos);
        }
    }
} 