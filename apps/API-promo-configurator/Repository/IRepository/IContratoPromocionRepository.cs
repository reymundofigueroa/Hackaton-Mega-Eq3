using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IContratoPromocionRepository
{
    ICollection<ContratoPromocione> GetContratoPromociones();
    ContratoPromocione? GetContratoPromocion(int id);
    ICollection<ContratoPromocione> GetPromocionesPorContrato(int idContrato);
    bool ContratoPromocionExists(int id);
    bool CreateContratoPromocion(ContratoPromocione contratoPromocion);
    bool UpdateContratoPromocion(ContratoPromocione contratoPromocion);
    bool DeleteContratoPromocion(ContratoPromocione contratoPromocion);
    bool Save();
} 