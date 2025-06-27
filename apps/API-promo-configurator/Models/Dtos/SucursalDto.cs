using System;

namespace API_promo_configurator.Models.Dtos;

public class SucursalDto
{
    public int IdSucursal { get; set; }
    public string Nombre { get; set; } = null!;
    public int IdDomicilio { get; set; }
    public string? Telefono { get; set; }
} 