using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Ciudade
{
    public int IdCiudad { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdMunicipio { get; set; }

    public virtual ICollection<Colonia> Colonia { get; set; } = new List<Colonia>();

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
