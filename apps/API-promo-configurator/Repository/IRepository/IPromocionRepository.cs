using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IPromocionRepository
{
    ICollection<Promocione> GetPromociones();
    Promocione? GetPromocion(int id);
    bool PromocionExists(int id);
    bool PromocionExists(string name);
    bool CreatePromocion(Promocione promocion);
    bool UpdatePromocion(Promocione promocion);
    bool DeletePromocion(Promocione promocion);
    bool Save();
}