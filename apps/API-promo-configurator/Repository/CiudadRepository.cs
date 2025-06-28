using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class CiudadRepository : ICiudadRepository
{
    private readonly ApplicationDbContext _db;

    public CiudadRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CiudadExists(int id)
    {
        return _db.Ciudades.Any(c => c.IdCiudad == id);
    }

    public bool CiudadExists(string nombre)
    {
        return _db.Ciudades.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
    }

    public ICollection<Ciudade> GetCiudades()
    {
        return _db.Ciudades.OrderBy(c => c.Nombre).ToList();
    }

    public Ciudade? GetCiudad(int id)
    {
        return _db.Ciudades.FirstOrDefault(c => c.IdCiudad == id);
    }

    public bool CreateCiudad(Ciudade ciudad)
    {
        _db.Ciudades.Add(ciudad);
        return Save();
    }

    public bool UpdateCiudad(Ciudade ciudad)
    {
        _db.Ciudades.Update(ciudad);
        return Save();
    }

    public bool DeleteCiudad(Ciudade ciudad)
    {
        _db.Ciudades.Remove(ciudad);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 