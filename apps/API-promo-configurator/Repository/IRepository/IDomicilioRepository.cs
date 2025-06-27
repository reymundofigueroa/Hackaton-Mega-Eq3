using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IDomicilioRepository
{
    ICollection<Domicilio> GetDomicilios();
    Domicilio? GetDomicilio(int id);
    bool DomicilioExists(int id);
    bool CreateDomicilio(Domicilio domicilio);
    bool UpdateDomicilio(Domicilio domicilio);
    bool DeleteDomicilio(Domicilio domicilio);
    bool Save();
} 