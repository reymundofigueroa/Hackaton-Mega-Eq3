using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface ICiudadRepository
{
    ICollection<Ciudade> GetCiudades();
    Ciudade? GetCiudad(int id);
    bool CiudadExists(int id);
    bool CiudadExists(string nombre);
    bool CreateCiudad(Ciudade ciudad);
    bool UpdateCiudad(Ciudade ciudad);
    bool DeleteCiudad(Ciudade ciudad);
    bool Save();
} 