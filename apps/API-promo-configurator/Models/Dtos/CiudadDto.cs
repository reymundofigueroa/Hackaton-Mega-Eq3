using System;

namespace API_promo_configurator.Models.Dtos;

public class CiudadDto
{
    public int IdCiudad { get; set; }
    public string Nombre { get; set; } = null!;
    public int IdMunicipio { get; set; }
} 