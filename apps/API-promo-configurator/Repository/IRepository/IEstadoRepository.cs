using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IEstadoRepository
{
    ICollection<Estado> GetEstados();
    Estado? GetEstado(int id);
    bool EstadoExists(int id);
    bool EstadoExists(string nombre);
    bool CreateEstado(Estado estado);
    bool UpdateEstado(Estado estado);
    bool DeleteEstado(Estado estado);
    bool Save();
} 