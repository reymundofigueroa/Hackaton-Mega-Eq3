using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface ISuscriptorRepository
{
    ICollection<Suscriptore> GetSuscriptores();
    Suscriptore? GetSuscriptor(int id);
    bool SuscriptorExists(int id);
    bool SuscriptorExists(string email);
    bool CreateSuscriptor(Suscriptore suscriptor);
    bool UpdateSuscriptor(Suscriptore suscriptor);
    bool DeleteSuscriptor(Suscriptore suscriptor);
    bool Save();
} 