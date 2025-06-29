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
    public class ContratosController : ControllerBase
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public ContratosController(IContratoRepository contratoRepository, IMapper mapper, ApplicationDbContext db)
        {
            _contratoRepository = contratoRepository;
            _mapper = mapper;
            _db = db;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContrato(int id)
        {
            var contrato = _contratoRepository.GetContrato(id);
            if (contrato == null)
                return NotFound();
            return Ok(_mapper.Map<ContratoDto>(contrato));
        }

        [HttpGet("suscriptor/{idSuscriptor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContratosPorSuscriptor(int idSuscriptor)
        {
            var contratos = _db.Contratos
                .Include(c => c.IdSuscriptorNavigation)
                .Include(c => c.IdSucursalNavigation)
                .Include(c => c.ContratoServicios)
                    .ThenInclude(cs => cs.IdServicioNavigation)
                .Include(c => c.ContratoPromociones)
                    .ThenInclude(cp => cp.IdPromocionNavigation)
                .Where(c => c.IdSuscriptor == idSuscriptor)
                .Select(c => new
                {
                    c.IdContrato,
                    c.FechaContratacion,
                    c.PlazoForzosoMeses,
                    c.Estado,
                    Suscriptor = new
                    {
                        c.IdSuscriptorNavigation.IdSuscriptor,
                        c.IdSuscriptorNavigation.Nombre,
                        c.IdSuscriptorNavigation.ApellidoPaterno,
                        c.IdSuscriptorNavigation.ApellidoMaterno,
                        c.IdSuscriptorNavigation.Email
                    },
                    Sucursal = new
                    {
                        c.IdSucursalNavigation.IdSucursal,
                        c.IdSucursalNavigation.Nombre,
                        c.IdSucursalNavigation.Telefono
                    },
                    Servicios = c.ContratoServicios.Select(cs => new
                    {
                        cs.IdContratoServicio,
                        cs.PrecioContratado,
                        cs.FechaAlta,
                        Servicio = new
                        {
                            cs.IdServicioNavigation.IdServicio,
                            cs.IdServicioNavigation.Nombre,
                            cs.IdServicioNavigation.Descripcion,
                            cs.IdServicioNavigation.PrecioBaseActual
                        }
                    }).ToList(),
                    Promociones = c.ContratoPromociones.Select(cp => new
                    {
                        cp.IdContratoPromocion,
                        cp.FechaAplicacion,
                        cp.Metadata,
                        Promocion = new
                        {
                            cp.IdPromocionNavigation.IdPromocion,
                            cp.IdPromocionNavigation.Nombre,
                            cp.IdPromocionNavigation.Descripcion,
                            cp.IdPromocionNavigation.TipoDescuento,
                            cp.IdPromocionNavigation.ValorDescuento,
                            cp.IdPromocionNavigation.AplicaA,
                            cp.IdPromocionNavigation.DuracionMeses
                        }
                    }).ToList()
                })
                .ToList();

            return Ok(contratos);
        }

    }
}