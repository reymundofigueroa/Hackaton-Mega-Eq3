using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class PromocionAlcance
{
    public int IdPromocionAlcance { get; set; }

    public int IdPromocion { get; set; }

    public int? IdEstado { get; set; }

    public int? IdMunicipio { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdColonia { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Ciudade? IdCiudadNavigation { get; set; }

    public virtual Colonia? IdColoniaNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Municipio? IdMunicipioNavigation { get; set; }

    public virtual Promocione IdPromocionNavigation { get; set; } = null!;

    public virtual Sucursale? IdSucursalNavigation { get; set; }
}
