using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;


namespace API_promo_configurator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusquedaSuscriptoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BusquedaSuscriptoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<Suscriptore>>> BuscarSuscriptores([FromQuery] string termino)
    {
        if (string.IsNullOrWhiteSpace(termino))
            return BadRequest("Debes proporcionar un término.");

        var resultados = await _context.Suscriptores
            .Where(s =>
                (s.Nombre != null && s.Nombre.Contains(termino)) ||
                (s.ApellidoPaterno != null && s.ApellidoPaterno.Contains(termino)) ||
                (s.ApellidoMaterno != null && s.ApellidoMaterno.Contains(termino)) ||
                (s.Email != null && s.Email.Contains(termino)) ||
                (s.Rfc != null && s.Rfc.Contains(termino))
            ).ToListAsync();

        return Ok(resultados);
    }

[HttpGet("servicios-contratados")]
public async Task<ActionResult<IEnumerable<ServicioContratadoDto>>> ObtenerServiciosContratados([FromQuery] int idSuscriptor)
{
    var servicios = await _context.Contratos
        .Where(c => c.IdSuscriptor == idSuscriptor)
        .Join(_context.ContratoServicios,
              c => c.IdContrato,
              cs => cs.IdContrato,
              (c, cs) => new { c, cs })
        .Join(_context.Servicios,
              temp => temp.cs.IdServicio,
              sv => sv.IdServicio,
              (temp, sv) => new { temp.c, temp.cs, sv })
        .GroupJoin(_context.ContratoPromociones,
              temp => temp.c.IdContrato,
              cp => cp.IdContrato,
              (temp, cpGroup) => new { temp.cs, temp.sv, cpGroup })
        .SelectMany(x => x.cpGroup.DefaultIfEmpty(), (x, cp) => new { x.cs, x.sv, cp })
        .GroupJoin(_context.Promociones,
              temp => temp.cp != null ? temp.cp.IdPromocion : 0,
              p => p.IdPromocion,
              (temp, pGroup) => new { temp.cs, temp.sv, temp.cp, pGroup })
        .SelectMany(x => x.pGroup.DefaultIfEmpty(), (x, p) => new ServicioContratadoDto
        {
            Servicio = x.sv.Nombre,
            PrecioContratado = x.cs.PrecioContratado,
            Promocion = p != null ? p.Nombre : null,
            TipoDescuento = p != null ? p.TipoDescuento : null,
            FechaAplicacion = x.cp != null ? 
                (DateTime?)x.cp.FechaAplicacion.ToDateTime(TimeOnly.MinValue) 
                : null
        })
        .OrderBy(dto => dto.Servicio)
        .ToListAsync();

    return Ok(servicios);
}

}
