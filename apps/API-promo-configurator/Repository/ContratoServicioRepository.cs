using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class ContratoServicioRepository : IContratoServicioRepository
{
    private readonly ApplicationDbContext _db;

    public ContratoServicioRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool ContratoServicioExists(int id)
    {
        return _db.ContratoServicios.Any(cs => cs.IdContratoServicio == id);
    }

    public ICollection<ContratoServicio> GetContratoServicios()
    {
        return _db.ContratoServicios
            .Include(cs => cs.IdContratoNavigation)
            .Include(cs => cs.Servicio)
            .OrderBy(cs => cs.IdContratoServicio)
            .ToList();
    }

    public ContratoServicio? GetContratoServicio(int id)
    {
        return _db.ContratoServicios
            .Include(cs => cs.IdContratoNavigation)
            .Include(cs => cs.Servicio)
            .FirstOrDefault(cs => cs.IdContratoServicio == id);
    }

    public ICollection<ContratoServicio> GetServiciosPorContrato(int idContrato)
    {
        return _db.ContratoServicios
            .Include(cs => cs.Servicio)
            .Where(cs => cs.IdContrato == idContrato)
            .OrderBy(cs => cs.Servicio.Nombre)
            .ToList();
    }

    public bool CreateContratoServicio(ContratoServicio contratoServicio)
    {
        _db.ContratoServicios.Add(contratoServicio);
        return Save();
    }

    public bool UpdateContratoServicio(ContratoServicio contratoServicio)
    {
        _db.ContratoServicios.Update(contratoServicio);
        return Save();
    }

    public bool DeleteContratoServicio(ContratoServicio contratoServicio)
    {
        _db.ContratoServicios.Remove(contratoServicio);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
}