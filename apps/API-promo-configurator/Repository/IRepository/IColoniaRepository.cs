using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IColoniaRepository
{
    ICollection<Colonia> GetColonias();
    Colonia? GetColonia(int id);
    bool ColoniaExists(int id);
    bool ColoniaExists(string nombre);
    bool CreateColonia(Colonia colonia);
    bool UpdateColonia(Colonia colonia);
    bool DeleteColonia(Colonia colonia);
    bool Save();
} 