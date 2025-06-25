using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class ContratoPromocione
{
    public int IdContratoPromocion { get; set; }

    public int IdContrato { get; set; }

    public int IdPromocion { get; set; }

    public DateOnly FechaAplicacion { get; set; }

    public string? Metadata { get; set; }

    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    public virtual Promocione IdPromocionNavigation { get; set; } = null!;
}
