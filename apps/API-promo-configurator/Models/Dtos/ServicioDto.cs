using System;

namespace API_promo_configurator.Models.Dtos;

// DTO para listar los servicios
public class ServicioDto
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioBaseActual { get; set; }
}
