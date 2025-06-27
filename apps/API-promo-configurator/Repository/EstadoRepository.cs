using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class EstadoRepository : IEstadoRepository
{
    private readonly ApplicationDbContext _db;

    public EstadoRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool EstadoExists(int id)
    {
        return _db.Estados.Any(e => e.IdEstado == id);
    }

    public bool EstadoExists(string nombre)
    {
        return _db.Estados.Any(e => e.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
    }

    public ICollection<Estado> GetEstados()
    {
        return _db.Estados.OrderBy(e => e.Nombre).ToList();
    }

    public Estado? GetEstado(int id)
    {
        return _db.Estados.FirstOrDefault(e => e.IdEstado == id);
    }

    public bool CreateEstado(Estado estado)
    {
        _db.Estados.Add(estado);
        return Save();
    }

    public bool UpdateEstado(Estado estado)
    {
        _db.Estados.Update(estado);
        return Save();
    }

    public bool DeleteEstado(Estado estado)
    {
        _db.Estados.Remove(estado);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 