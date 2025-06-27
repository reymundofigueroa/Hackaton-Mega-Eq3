using System;

namespace API_promo_configurator.Models.Dtos;

public class SuscriptorDto
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; } = null!;
    public string ApellidoPaterno { get; set; } = null!;
    public string? ApellidoMaterno { get; set; }
    public string? Rfc { get; set; }
    public string Email { get; set; } = null!;
    public string TelefonoContacto { get; set; } = null!;
    public DateTime FechaRegistro { get; set; }
    public int IdDomicilio { get; set; }
} 