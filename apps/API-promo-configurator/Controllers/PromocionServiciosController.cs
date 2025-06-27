using API_promo_configurator.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionServiciosController : ControllerBase
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IServicioRepository _servicioRepository;

        public PromocionServiciosController(IPromocionRepository promocionRepository, IServicioRepository servicioRepository)
        {
            _promocionRepository = promocionRepository;
            _servicioRepository = servicioRepository;
        }

        // Asociar un servicio a una promoción
        [HttpPost("asociar")]
        public IActionResult AsociarServicio([FromQuery] int idPromocion, [FromQuery] int idServicio)
        {
            var promocion = _promocionRepository.GetPromocion(idPromocion);
            var servicio = _servicioRepository.GetServicio(idServicio);

            if (promocion == null || servicio == null)
                return NotFound("Promoción o servicio no encontrado.");

            if (promocion.IdServicios.Contains(servicio))
                return BadRequest("El servicio ya está asociado a la promoción.");

            promocion.IdServicios.Add(servicio);
            _promocionRepository.UpdatePromocion(promocion);

            return NoContent();
        }

        // Desasociar un servicio de una promoción
        [HttpDelete("desasociar")]
        public IActionResult DesasociarServicio([FromQuery] int idPromocion, [FromQuery] int idServicio)
        {
            var promocion = _promocionRepository.GetPromocion(idPromocion);
            var servicio = _servicioRepository.GetServicio(idServicio);

            if (promocion == null || servicio == null)
                return NotFound("Promoción o servicio no encontrado.");

            if (!promocion.IdServicios.Contains(servicio))
                return BadRequest("El servicio no está asociado a la promoción.");

            promocion.IdServicios.Remove(servicio);
            _promocionRepository.UpdatePromocion(promocion);

            return NoContent();
        }

        // Obtener todos los servicios asociados a una promoción
        [HttpGet("servicios-de-promocion/{idPromocion}")]
        public IActionResult GetServiciosDePromocion(int idPromocion)
        {
            var promocion = _promocionRepository.GetPromocion(idPromocion);
            if (promocion == null)
                return NotFound("Promoción no encontrada.");

            // Puedes mapear a DTO si lo prefieres
            var servicios = promocion.IdServicios.Select(s => new {
                s.IdServicio,
                s.Nombre,
                s.Descripcion,
                s.PrecioBaseActual
            });

            return Ok(servicios);
        }

    }
}