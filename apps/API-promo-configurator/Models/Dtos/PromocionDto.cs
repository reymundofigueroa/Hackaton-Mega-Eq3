using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models.Dtos;

public class PromocionDto
{
    public int IdPromocion { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string TipoDescuento { get; set; } = null!;
    public decimal ValorDescuento { get; set; }
    public string AplicaA { get; set; } = null!;
    public int DuracionMeses { get; set; }
    // Para mostrar los servicios asociados (opcional)
    public List<int>? IdServicios { get; set; }
}