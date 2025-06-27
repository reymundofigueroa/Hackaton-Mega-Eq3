using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class DomicilioRepository : IDomicilioRepository
{
    private readonly ApplicationDbContext _db;

    public DomicilioRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool DomicilioExists(int id)
    {
        return _db.Domicilios.Any(d => d.IdDomicilio == id);
    }

    public ICollection<Domicilio> GetDomicilios()
    {
        return _db.Domicilios.OrderBy(d => d.Calle).ToList();
    }

    public Domicilio? GetDomicilio(int id)
    {
        return _db.Domicilios.FirstOrDefault(d => d.IdDomicilio == id);
    }

    public bool CreateDomicilio(Domicilio domicilio)
    {
        _db.Domicilios.Add(domicilio);
        return Save();
    }

    public bool UpdateDomicilio(Domicilio domicilio)
    {
        _db.Domicilios.Update(domicilio);
        return Save();
    }

    public bool DeleteDomicilio(Domicilio domicilio)
    {
        _db.Domicilios.Remove(domicilio);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 