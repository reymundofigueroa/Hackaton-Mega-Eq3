using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class ContratoPromocionRepository : IContratoPromocionRepository
{
    private readonly ApplicationDbContext _db;

    public ContratoPromocionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool ContratoPromocionExists(int id)
    {
        return _db.ContratoPromociones.Any(cp => cp.IdContratoPromocion == id);
    }

    public ICollection<ContratoPromocione> GetContratoPromociones()
    {
        return _db.ContratoPromociones
            .Include(cp => cp.IdContratoNavigation)
            .Include(cp => cp.IdPromocionNavigation)
            .OrderBy(cp => cp.IdContratoPromocion)
            .ToList();
    }

    public ContratoPromocione? GetContratoPromocion(int id)
    {
        return _db.ContratoPromociones
            .Include(cp => cp.IdContratoNavigation)
            .Include(cp => cp.IdPromocionNavigation)
            .FirstOrDefault(cp => cp.IdContratoPromocion == id);
    }

    public ICollection<ContratoPromocione> GetPromocionesPorContrato(int idContrato)
    {
        return _db.ContratoPromociones
            .Include(cp => cp.IdPromocionNavigation)
            .Where(cp => cp.IdContrato == idContrato)
            .OrderBy(cp => cp.IdPromocionNavigation.Nombre)
            .ToList();
    }

    public bool CreateContratoPromocion(ContratoPromocione contratoPromocion)
    {
        _db.ContratoPromociones.Add(contratoPromocion);
        return Save();
    }

    public bool UpdateContratoPromocion(ContratoPromocione contratoPromocion)
    {
        _db.ContratoPromociones.Update(contratoPromocion);
        return Save();
    }

    public bool DeleteContratoPromocion(ContratoPromocione contratoPromocion)
    {
        _db.ContratoPromociones.Remove(contratoPromocion);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
} 