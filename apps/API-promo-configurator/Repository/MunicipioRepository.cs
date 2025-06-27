using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class MunicipioRepository : IMunicipioRepository
{
    private readonly ApplicationDbContext _db;

    public MunicipioRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool MunicipioExists(int id)
    {
        return _db.Municipios.Any(m => m.IdMunicipio == id);
    }

    public bool MunicipioExists(string nombre)
    {
        return _db.Municipios.Any(m => m.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
    }

    public ICollection<Municipio> GetMunicipios()
    {
        return _db.Municipios.OrderBy(m => m.Nombre).ToList();
    }

    public Municipio? GetMunicipio(int id)
    {
        return _db.Municipios.FirstOrDefault(m => m.IdMunicipio == id);
    }

    public bool CreateMunicipio(Municipio municipio)
    {
        _db.Municipios.Add(municipio);
        return Save();
    }

    public bool UpdateMunicipio(Municipio municipio)
    {
        _db.Municipios.Update(municipio);
        return Save();
    }

    public bool DeleteMunicipio(Municipio municipio)
    {
        _db.Municipios.Remove(municipio);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 