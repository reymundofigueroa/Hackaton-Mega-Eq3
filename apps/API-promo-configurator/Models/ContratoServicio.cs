using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class ContratoServicio
{
    public int IdContratoServicio { get; set; }

    public int IdContrato { get; set; }

    public int IdServicio { get; set; }

    public decimal PrecioContratado { get; set; }

    public DateOnly FechaAlta { get; set; }

    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}