using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class ColoniaRepository : IColoniaRepository
{
    private readonly ApplicationDbContext _db;

    public ColoniaRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool ColoniaExists(int id)
    {
        return _db.Colonias.Any(c => c.IdColonia == id);
    }

    public bool ColoniaExists(string nombre)
    {
        return _db.Colonias.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
    }

    public ICollection<Colonia> GetColonias()
    {
        return _db.Colonias.OrderBy(c => c.Nombre).ToList();
    }

    public Colonia? GetColonia(int id)
    {
        return _db.Colonias.FirstOrDefault(c => c.IdColonia == id);
    }

    public bool CreateColonia(Colonia colonia)
    {
        _db.Colonias.Add(colonia);
        return Save();
    }

    public bool UpdateColonia(Colonia colonia)
    {
        _db.Colonias.Update(colonia);
        return Save();
    }

    public bool DeleteColonia(Colonia colonia)
    {
        _db.Colonias.Remove(colonia);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 