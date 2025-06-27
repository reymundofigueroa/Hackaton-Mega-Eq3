using System;

namespace API_promo_configurator.Models.Dtos;

public class ContratoPromocionDto
{
    public int IdContratoPromocion { get; set; }
    public int IdContrato { get; set; }
    public int IdPromocion { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public string? Metadata { get; set; }
} 