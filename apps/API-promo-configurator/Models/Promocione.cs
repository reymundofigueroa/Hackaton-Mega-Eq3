using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Promocione
{
    public int IdPromocion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string TipoDescuento { get; set; } = null!;

    public decimal ValorDescuento { get; set; }

    public string AplicaA { get; set; } = null!;

    public int DuracionMeses { get; set; }

    public virtual ICollection<ContratoPromocione> ContratoPromociones { get; set; } = new List<ContratoPromocione>();

    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
