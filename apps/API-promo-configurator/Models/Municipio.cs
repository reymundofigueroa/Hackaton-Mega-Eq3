using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Municipio
{
    public int IdMunicipio { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
