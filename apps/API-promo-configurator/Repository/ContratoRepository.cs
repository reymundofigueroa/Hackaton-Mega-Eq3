using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Repository;

public class ContratoRepository : IContratoRepository
{
    private readonly ApplicationDbContext _db;

    public ContratoRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool ContratoExists(int id)
    {
        return _db.Contratos.Any(c => c.IdContrato == id);
    }

    public ICollection<Contrato> GetContratos()
    {
        return _db.Contratos.Include(s => s.Suscriptore).OrderBy(c => c.IdContrato).ToList();
    }

    public Contrato? GetContrato(int id)
    {
        return _db.Contratos.Include(s => s.Suscriptore).FirstOrDefault(c => c.IdContrato == id);
    }

    public bool CreateContrato(Contrato contrato)
    {
        _db.Contratos.Add(contrato);
        return Save();
    }

    public bool UpdateContrato(Contrato contrato)
    {
        _db.Contratos.Update(contrato);
        return Save();
    }

    public bool DeleteContrato(Contrato contrato)
    {
        _db.Contratos.Remove(contrato);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }
}