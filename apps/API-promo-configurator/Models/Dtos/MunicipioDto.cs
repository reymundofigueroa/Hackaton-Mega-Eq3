using System;

namespace API_promo_configurator.Models.Dtos;

public class MunicipioDto
{
    public int IdMunicipio { get; set; }
    public string Nombre { get; set; } = null!;
    public int IdEstado { get; set; }
} 