using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface ISucursalRepository
{
    ICollection<Sucursale> GetSucursales();
    Sucursale? GetSucursal(int id);
    bool SucursalExists(int id);
    bool SucursalExists(string nombre);
    bool CreateSucursal(Sucursale sucursal);
    bool UpdateSucursal(Sucursale sucursal);
    bool DeleteSucursal(Sucursale sucursal);
    bool Save();
} 