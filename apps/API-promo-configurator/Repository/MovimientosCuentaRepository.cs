using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class MovimientosCuentaRepository : IMovimientosCuentaRepository
{
    private readonly ApplicationDbContext _db;

    public MovimientosCuentaRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool MovimientoCuentaExists(long id)
    {
        return _db.MovimientosCuenta.Any(m => m.IdMovimiento == id);
    }

    public ICollection<MovimientosCuentum> GetMovimientosCuenta()
    {
        return _db.MovimientosCuenta
            .Include(m => m.IdContratoNavigation)
            .OrderBy(m => m.IdMovimiento)
            .ToList();
    }

    public MovimientosCuentum? GetMovimientoCuenta(long id)
    {
        return _db.MovimientosCuenta
            .Include(m => m.IdContratoNavigation)
            .FirstOrDefault(m => m.IdMovimiento == id);
    }

    public ICollection<MovimientosCuentum> GetMovimientosPorContrato(int idContrato)
    {
        return _db.MovimientosCuenta
            .Where(m => m.IdContrato == idContrato)
            .OrderBy(m => m.FechaMovimiento)
            .ToList();
    }

    public bool CreateMovimientoCuenta(MovimientosCuentum movimiento)
    {
        _db.MovimientosCuenta.Add(movimiento);
        return Save();
    }

    public bool UpdateMovimientoCuenta(MovimientosCuentum movimiento)
    {
        _db.MovimientosCuenta.Update(movimiento);
        return Save();
    }

    public bool DeleteMovimientoCuenta(MovimientosCuentum movimiento)
    {
        _db.MovimientosCuenta.Remove(movimiento);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 