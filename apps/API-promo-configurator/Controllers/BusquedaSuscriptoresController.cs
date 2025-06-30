using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Models.Dtos;


namespace API_promo_configurator.Controllers;

/// <summary>
/// Controlador para la búsqueda avanzada de suscriptores y consulta de servicios contratados.
/// Permite buscar suscriptores por nombre, apellidos, email o RFC, y consultar los servicios contratados por un suscriptor.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BusquedaSuscriptoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BusquedaSuscriptoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca suscriptores por nombre, apellidos, email o RFC.
    /// </summary>
    /// <param name="termino">Término de búsqueda (nombre, apellido, email o RFC)</param>
    /// <returns>Lista de suscriptores que coinciden con el término</returns>
    /// <response code="200">Lista de suscriptores encontrada</response>
    /// <response code="400">No se proporcionó un término de búsqueda</response>
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

    /// <summary>
    /// Obtiene los servicios contratados por un suscriptor, junto con promociones aplicadas.
    /// </summary>
    /// <param name="idSuscriptor">Identificador del suscriptor</param>
    /// <returns>Lista de servicios contratados y promociones asociadas</returns>
    /// <response code="200">Lista de servicios contratados encontrada</response>
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
