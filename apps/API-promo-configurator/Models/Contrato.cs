using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public int IdSuscriptor { get; set; }

    public int IdSucursal { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public int PlazoForzosoMeses { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<ContratoPromocione> ContratoPromociones { get; set; } = new List<ContratoPromocione>();

    public virtual ICollection<ContratoServicio> ContratoServicios { get; set; } = new List<ContratoServicio>();

    public virtual Sucursale IdSucursalNavigation { get; set; } = null!;

    public virtual Suscriptore IdSuscriptorNavigation { get; set; } = null!;

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();
}
