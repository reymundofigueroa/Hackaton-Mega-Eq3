using System;

namespace API_promo_configurator.Models.Dtos;

public class PromocionAlcanceDto
{
    public int IdPromocionAlcance { get; set; }
    public int IdPromocion { get; set; }
    public int? IdEstado { get; set; }
    public int? IdMunicipio { get; set; }
    public int? IdCiudad { get; set; }
    public int? IdColonia { get; set; }
    public int? IdSucursal { get; set; }
} 