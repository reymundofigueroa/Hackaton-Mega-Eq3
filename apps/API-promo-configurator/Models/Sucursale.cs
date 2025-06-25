using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Sucursale
{
    public int IdSucursal { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdDomicilio { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual Domicilio IdDomicilioNavigation { get; set; } = null!;

    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();

    public virtual ICollection<Colonia> IdColonia { get; set; } = new List<Colonia>();
}
