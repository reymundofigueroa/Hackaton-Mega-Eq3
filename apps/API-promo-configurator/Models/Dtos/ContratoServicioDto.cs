using System;

namespace API_promo_configurator.Models.Dtos;

public class ContratoServicioDto
{
    public int IdContratoServicio { get; set; }
    public int IdContrato { get; set; }
    public int IdServicio { get; set; }
    public decimal PrecioContratado { get; set; }
    public DateTime FechaAlta { get; set; }
}