using System;

namespace API_promo_configurator.Models.Dtos;

public class DomicilioDto
{
    public int IdDomicilio { get; set; }
    public string Calle { get; set; } = null!;
    public string NumeroExterior { get; set; } = null!;
    public string? NumeroInterior { get; set; }
    public string? Referencias { get; set; }
    public int IdColonia { get; set; }
} 