using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class SuscriptorRepository : ISuscriptorRepository
{
    private readonly ApplicationDbContext _db;

    public SuscriptorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool SuscriptorExists(int id)
    {
        return _db.Suscriptores.Any(s => s.IdSuscriptor == id);
    }

    public bool SuscriptorExists(string email)
    {
        return _db.Suscriptores.Any(s => s.Email.ToLower().Trim() == email.ToLower().Trim());
    }

    public ICollection<Suscriptore> GetSuscriptores()
    {
        return _db.Suscriptores.OrderBy(s => s.Nombre).ToList();
    }

    public Suscriptore? GetSuscriptor(int id)
    {
        return _db.Suscriptores.FirstOrDefault(s => s.IdSuscriptor == id);
    }

    public bool CreateSuscriptor(Suscriptore suscriptor)
    {
        _db.Suscriptores.Add(suscriptor);
        return Save();
    }

    public bool UpdateSuscriptor(Suscriptore suscriptor)
    {
        _db.Suscriptores.Update(suscriptor);
        return Save();
    }

    public bool DeleteSuscriptor(Suscriptore suscriptor)
    {
        _db.Suscriptores.Remove(suscriptor);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 