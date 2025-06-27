using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IContratoRepository
{
    ICollection<Contrato> GetContratos();
    Contrato? GetContrato(int id);
    bool ContratoExists(int id);
    bool CreateContrato(Contrato contrato);
    bool UpdateContrato(Contrato contrato);
    bool DeleteContrato(Contrato contrato);
    bool Save();
}