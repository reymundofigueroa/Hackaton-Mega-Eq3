using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Colonia
{
    public int IdColonia { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public int IdCiudad { get; set; }

    public virtual ICollection<Domicilio> Domicilios { get; set; } = new List<Domicilio>();

    public virtual Ciudade IdCiudadNavigation { get; set; } = null!;

    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();

    public virtual ICollection<Sucursale> IdSucursals { get; set; } = new List<Sucursale>();
}
