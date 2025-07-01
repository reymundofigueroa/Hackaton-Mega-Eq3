using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IMunicipioRepository
{
    ICollection<Municipio> GetMunicipios();
    Municipio? GetMunicipio(int id);
    bool MunicipioExists(int id);
    bool MunicipioExists(string nombre);
    bool CreateMunicipio(Municipio municipio);
    bool UpdateMunicipio(Municipio municipio);
    bool DeleteMunicipio(Municipio municipio);
    bool Save();
} 