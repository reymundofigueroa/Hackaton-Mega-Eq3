using System;
using API_promo_configurator.Data;
using API_promo_configurator.Models;
using API_promo_configurator.Repository.IRepository;

namespace API_promo_configurator.Repository;

public class ServicioRepository : IServicioRepository
{

    // Creamos una instancia del contexto
    private readonly ApplicationDbContext _db;

    // Inyección de dependencias
    public ServicioRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    // Métodos para comprobar si el servicio existe

    public bool ServicioExists(int id)
    {
        return _db.Servicios.Any(s => s.IdServicio == id);
    }

    public bool ServicioExists(string name)
    {
        return _db.Servicios.Any(s => s.Nombre.ToLower().Trim() == name.ToLower().Trim());
    }

    // Obtener todos los servicios
    public ICollection<Servicio> GetServicios()
    {
        return _db.Servicios.OrderBy(s => s.Nombre).ToList();
    }

    // Obtener un servicio mediante su ID
    public Servicio? GetServicio(int id)
    {
        return _db.Servicios.FirstOrDefault(s => s.IdServicio == id);
    }

    // Guardar los cambios y ejecutar acciones en la BD
    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    // SIN IMPLEMENTACIÓN
    public bool CreateServicio(Servicio servicio)
    {
        throw new NotImplementedException();
    }

    // SIN IMPLEMENTACIÓN
    public bool DeleteServicio(Servicio servicio)
    {
        throw new NotImplementedException();
    }

    // SIN IMPLEMENTACIÓN
    public bool UpdateServicio(Servicio servicio)
    {
        throw new NotImplementedException();
    }
}
