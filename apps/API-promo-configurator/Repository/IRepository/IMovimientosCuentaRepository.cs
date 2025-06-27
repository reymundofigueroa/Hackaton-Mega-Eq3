using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IMovimientosCuentaRepository
{
    ICollection<MovimientosCuentum> GetMovimientosCuenta();
    MovimientosCuentum? GetMovimientoCuenta(long id);
    ICollection<MovimientosCuentum> GetMovimientosPorContrato(int idContrato);
    bool MovimientoCuentaExists(long id);
    bool CreateMovimientoCuenta(MovimientosCuentum movimiento);
    bool UpdateMovimientoCuenta(MovimientosCuentum movimiento);
    bool DeleteMovimientoCuenta(MovimientosCuentum movimiento);
    bool Save();
} 