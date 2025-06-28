using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Table("Promocion_Alcance")]
[Index("IdPromocion", Name = "IX_PromocionAlcance_IdPromocion")]
public partial class PromocionAlcance
{
    [Key]
    [Column("id_promocion_alcance")]
    public int IdPromocionAlcance { get; set; }

    [Column("id_promocion")]
    public int IdPromocion { get; set; }

    [Column("id_estado")]
    public int? IdEstado { get; set; }

    [Column("id_municipio")]
    public int? IdMunicipio { get; set; }

    [Column("id_ciudad")]
    public int? IdCiudad { get; set; }

    [Column("id_colonia")]
    public int? IdColonia { get; set; }

    [Column("id_sucursal")]
    public int? IdSucursal { get; set; }

    [ForeignKey("IdCiudad")]
    [InverseProperty("PromocionAlcances")]
    public virtual Ciudade? IdCiudadNavigation { get; set; }

    [ForeignKey("IdColonia")]
    [InverseProperty("PromocionAlcances")]
    public virtual Colonia? IdColoniaNavigation { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("PromocionAlcances")]
    public virtual Estado? IdEstadoNavigation { get; set; }

    [ForeignKey("IdMunicipio")]
    [InverseProperty("PromocionAlcances")]
    public virtual Municipio? IdMunicipioNavigation { get; set; }

    [ForeignKey("IdPromocion")]
    [InverseProperty("PromocionAlcances")]
    public virtual Promocione IdPromocionNavigation { get; set; } = null!;

    [ForeignKey("IdSucursal")]
    [InverseProperty("PromocionAlcances")]
    public virtual Sucursale? IdSucursalNavigation { get; set; }
}
