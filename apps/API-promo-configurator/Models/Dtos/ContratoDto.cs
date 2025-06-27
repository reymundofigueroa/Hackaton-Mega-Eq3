using System;

namespace API_promo_configurator.Models.Dtos;

public class ContratoDto
{
    public int IdContrato { get; set; }
    public int IdSuscriptor { get; set; }
    public int IdSucursal { get; set; }
    public DateTime FechaContratacion { get; set; }
    public int PlazoForzosoMeses { get; set; }
    public string Estado { get; set; } = null!;
}