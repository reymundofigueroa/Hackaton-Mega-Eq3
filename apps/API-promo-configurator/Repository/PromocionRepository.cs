using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class PromocionRepository : IPromocionRepository
{
    private readonly ApplicationDbContext _db;

    public PromocionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool PromocionExists(int id)
    {
        return _db.Promociones.Any(p => p.IdPromocion == id);
    }

    public bool PromocionExists(string name)
    {
        return _db.Promociones.Any(p => p.Nombre.ToLower().Trim() == name.ToLower().Trim());
    }

    public ICollection<Promocione> GetPromociones()
    {
        return _db.Promociones.Include(p => p.Servicios).OrderBy(p => p.Nombre).ToList();
    }


    public Promocione? GetPromocion(int id)
    {
        return _db.Promociones
            .Include(p => p.Servicios)   
            .FirstOrDefault(p => p.IdPromocion == id);
    }

    public bool CreatePromocion(Promocione promocion)
    {
        _db.Promociones.Add(promocion);
        return Save();
    }

    public bool UpdatePromocion(Promocione promocion)
    {
        _db.Promociones.Update(promocion);
        return Save();
    }

    public bool DeletePromocion(Promocione promocion)
    {
        _db.Promociones.Remove(promocion);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
}