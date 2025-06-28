using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class SucursalRepository : ISucursalRepository
{
    private readonly ApplicationDbContext _db;

    public SucursalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool SucursalExists(int id)
    {
        return _db.Sucursales.Any(s => s.IdSucursal == id);
    }

    public bool SucursalExists(string nombre)
    {
        return _db.Sucursales.Any(s => s.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
    }

    public ICollection<Sucursale> GetSucursales()
    {
        return _db.Sucursales.OrderBy(s => s.Nombre).ToList();
    }

    public Sucursale? GetSucursal(int id)
    {
        return _db.Sucursales.FirstOrDefault(s => s.IdSucursal == id);
    }

    public bool CreateSucursal(Sucursale sucursal)
    {
        _db.Sucursales.Add(sucursal);
        return Save();
    }

    public bool UpdateSucursal(Sucursale sucursal)
    {
        _db.Sucursales.Update(sucursal);
        return Save();
    }

    public bool DeleteSucursal(Sucursale sucursal)
    {
        _db.Sucursales.Remove(sucursal);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 