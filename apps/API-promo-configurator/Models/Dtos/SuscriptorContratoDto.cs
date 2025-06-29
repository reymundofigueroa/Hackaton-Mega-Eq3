using System;

namespace API_promo_configurator.Models.Dtos;

public class SuscriptorContratoDto
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; } = null!;
    public string ApellidoPaterno { get; set; } = null!;
} 