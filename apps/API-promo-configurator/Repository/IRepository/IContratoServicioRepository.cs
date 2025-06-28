using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IContratoServicioRepository
{
    ICollection<ContratoServicio> GetContratoServicios();
    ContratoServicio? GetContratoServicio(int id);
    ICollection<ContratoServicio> GetServiciosPorContrato(int idContrato);
    bool ContratoServicioExists(int id);
    bool CreateContratoServicio(ContratoServicio contratoServicio);
    bool UpdateContratoServicio(ContratoServicio contratoServicio);
    bool DeleteContratoServicio(ContratoServicio contratoServicio);
    bool Save();
}