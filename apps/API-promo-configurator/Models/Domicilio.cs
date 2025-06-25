using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Domicilio
{
    public int IdDomicilio { get; set; }

    public string Calle { get; set; } = null!;

    public string NumeroExterior { get; set; } = null!;

    public string? NumeroInterior { get; set; }

    public string? Referencias { get; set; }

    public int IdColonia { get; set; }

    public virtual Colonia IdColoniaNavigation { get; set; } = null!;

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();

    public virtual ICollection<Suscriptore> Suscriptores { get; set; } = new List<Suscriptore>();
}
