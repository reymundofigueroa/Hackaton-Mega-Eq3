using API_promo_configurator.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalColoniasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SucursalColoniasController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("sucursal/{idSucursal}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetColoniasPorSucursal(int idSucursal)
        {
            var colonias = _db.Sucursales
                .Where(s => s.IdSucursal == idSucursal)
                .SelectMany(s => s.IdColonia)
                .Select(c => new
                {
                    c.IdColonia,
                    c.Nombre,
                    c.CodigoPostal,
                    c.IdCiudad
                })
                .ToList();

            return Ok(colonias);
        }

        [HttpGet("colonia/{idColonia}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSucursalesPorColonia(int idColonia)
        {
            var sucursales = _db.Sucursales
                .Where(s => s.IdColonia.Any(c => c.IdColonia == idColonia))
                .Select(s => new
                {
                    s.IdSucursal,
                    s.Nombre,
                    s.Telefono,
                    s.IdDomicilio
                })
                .ToList();

            return Ok(sucursales);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllSucursalColonias()
        {
            var relaciones = _db.Sucursales
                .SelectMany(s => s.IdColonia.Select(c => new
                {
                    IdSucursal = s.IdSucursal,
                    NombreSucursal = s.Nombre,
                    IdColonia = c.IdColonia,
                    NombreColonia = c.Nombre
                }))
                .ToList();

            return Ok(relaciones);
        }
    }
} 