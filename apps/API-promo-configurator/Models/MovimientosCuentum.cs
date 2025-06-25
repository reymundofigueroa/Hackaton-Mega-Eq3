using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class MovimientosCuentum
{
    public long IdMovimiento { get; set; }

    public int IdContrato { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal MontoCargo { get; set; }

    public decimal MontoPago { get; set; }

    public decimal SaldoResultante { get; set; }

    public virtual Contrato IdContratoNavigation { get; set; } = null!;
}
