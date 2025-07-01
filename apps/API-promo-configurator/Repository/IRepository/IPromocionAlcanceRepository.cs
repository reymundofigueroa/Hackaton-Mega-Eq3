using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IPromocionAlcanceRepository
{
    ICollection<PromocionAlcance> GetPromocionAlcances();
    PromocionAlcance? GetPromocionAlcance(int id);
    ICollection<PromocionAlcance> GetAlcancesPorPromocion(int idPromocion);
    bool PromocionAlcanceExists(int id);
    bool CreatePromocionAlcance(PromocionAlcance promocionAlcance);
    bool UpdatePromocionAlcance(PromocionAlcance promocionAlcance);
    bool DeletePromocionAlcance(PromocionAlcance promocionAlcance);
    bool Save();
} 