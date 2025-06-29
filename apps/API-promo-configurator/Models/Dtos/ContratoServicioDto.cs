using System;

namespace API_promo_configurator.Models.Dtos;

public class ContratoServicioDto
{
    public int IdContratoServicio { get; set; }
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioContratado { get; set; }
    public DateTime FechaAlta { get; set; }
}