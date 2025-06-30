using API_promo_configurator.Data;
using API_promo_configurator.Models.Dtos;
using API_promo_configurator.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Controllers
{
    /// <summary>
    /// Controlador para la gestión de movimientos de cuenta.
    /// Permite consultar todos los movimientos, obtener un movimiento específico y consultar movimientos por suscriptor.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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

        /// <summary>
        /// Obtiene la lista de todos los movimientos de cuenta registrados.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de movimientos de cuenta con información básica.
        /// </remarks>
        /// <response code="200">Retorna la lista de movimientos de cuenta</response>
        /// <response code="403">No autorizado para acceder a los movimientos de cuenta</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientosCuenta()
        {
            var movimientos = _movimientosCuentaRepository.GetMovimientosCuenta();
            var movimientosDto = movimientos.Select(m => _mapper.Map<MovimientosCuentaDto>(m)).ToList();
            return Ok(movimientosDto);
        }

        /// <summary>
        /// Obtiene un movimiento de cuenta específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del movimiento de cuenta</param>
        /// <returns>Movimiento de cuenta encontrado</returns>
        /// <response code="200">Movimiento de cuenta encontrado</response>
        /// <response code="404">Movimiento de cuenta no encontrado</response>
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

        /// <summary>
        /// Obtiene los movimientos de cuenta asociados a un suscriptor específico.
        /// </summary>
        /// <param name="idSuscriptor">Identificador del suscriptor</param>
        /// <returns>Lista de movimientos de cuenta del suscriptor</returns>
        /// <response code="200">Lista de movimientos de cuenta encontrada</response>
        [HttpGet("suscriptor/{idSuscriptor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientosPorSuscriptor(int idSuscriptor)
        {
            var movimientos = _db.MovimientosCuenta
                .Include(m => m.IdContratoNavigation)
                .Where(m => m.IdContratoNavigation.Suscriptore.IdSuscriptor == idSuscriptor)
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