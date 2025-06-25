using System;
using API_promo_configurator.Models;

namespace API_promo_configurator.Repository.IRepository;

public interface IServicioRepository
{
    // Método para obtener todos los servicios disponibles
    ICollection<Servicio> GetServicios();

    // Método para obtener un servicio mediante su ID
    Servicio? GetServicio(int id);

    // Método para comprobar si un servicio existe mediante el ID
    bool ServicioExists(int id);

    // Método para comprobar si un servicio existe mediante su nombre
    bool ServicioExists(string name);

    // Método para crear un servicio nuevo
    bool CreateServicio(Servicio servicio);

    // Método para Actualizar un servicio
    bool UpdateServicio(Servicio servicio);

    // Método para borrar un servicio
    bool DeleteServicio(Servicio servicio);

    // Método par guardar los cambios en la BD
    bool Save();
}
