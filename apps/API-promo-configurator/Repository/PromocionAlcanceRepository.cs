using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class PromocionAlcanceRepository : IPromocionAlcanceRepository
{
    private readonly ApplicationDbContext _db;

    public PromocionAlcanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool PromocionAlcanceExists(int id)
    {
        return _db.PromocionAlcances.Any(pa => pa.IdPromocionAlcance == id);
    }

    public ICollection<PromocionAlcance> GetPromocionAlcances()
    {
        return _db.PromocionAlcances
            .Include(pa => pa.IdPromocionNavigation)
            .Include(pa => pa.IdEstadoNavigation)
            .Include(pa => pa.IdMunicipioNavigation)
            .Include(pa => pa.IdCiudadNavigation)
            .Include(pa => pa.IdColoniaNavigation)
            .Include(pa => pa.IdSucursalNavigation)
            .OrderBy(pa => pa.IdPromocionAlcance)
            .ToList();
    }

    public PromocionAlcance? GetPromocionAlcance(int id)
    {
        return _db.PromocionAlcances
            .Include(pa => pa.IdPromocionNavigation)
            .Include(pa => pa.IdEstadoNavigation)
            .Include(pa => pa.IdMunicipioNavigation)
            .Include(pa => pa.IdCiudadNavigation)
            .Include(pa => pa.IdColoniaNavigation)
            .Include(pa => pa.IdSucursalNavigation)
            .FirstOrDefault(pa => pa.IdPromocionAlcance == id);
    }

    public ICollection<PromocionAlcance> GetAlcancesPorPromocion(int idPromocion)
    {
        return _db.PromocionAlcances
            .Include(pa => pa.IdEstadoNavigation)
            .Include(pa => pa.IdMunicipioNavigation)
            .Include(pa => pa.IdCiudadNavigation)
            .Include(pa => pa.IdColoniaNavigation)
            .Include(pa => pa.IdSucursalNavigation)
            .Where(pa => pa.IdPromocion == idPromocion)
            .OrderBy(pa => pa.IdPromocionAlcance)
            .ToList();
    }

    public bool CreatePromocionAlcance(PromocionAlcance promocionAlcance)
    {
        _db.PromocionAlcances.Add(promocionAlcance);
        return Save();
    }

    public bool UpdatePromocionAlcance(PromocionAlcance promocionAlcance)
    {
        _db.PromocionAlcances.Update(promocionAlcance);
        return Save();
    }

    public bool DeletePromocionAlcance(PromocionAlcance promocionAlcance)
    {
        _db.PromocionAlcances.Remove(promocionAlcance);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 