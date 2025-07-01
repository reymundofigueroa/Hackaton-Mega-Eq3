using System;

namespace API_promo_configurator.Models.Dtos;

public class CreateContratoPromocionDto
{
    public int IdContrato { get; set; }
    public int IdPromocion { get; set; }
    public DateTime FechaAplicacion { get; set; } = DateTime.Now;
} 