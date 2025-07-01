using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Index("Nombre", Name = "UQ__Sucursal__72AFBCC64812ED61", IsUnique = true)]
public partial class Sucursale
{
    [Key]
    [Column("id_sucursal")]
    public int IdSucursal { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("id_domicilio")]
    public int IdDomicilio { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [InverseProperty("IdSucursalNavigation")]
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    [ForeignKey("IdDomicilio")]
    [InverseProperty("Sucursales")]
    public virtual Domicilio IdDomicilioNavigation { get; set; } = null!;

    [InverseProperty("IdSucursalNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();

    [ForeignKey("IdSucursal")]
    [InverseProperty("IdSucursals")]
    public virtual ICollection<Colonia> IdColonia { get; set; } = new List<Colonia>();
}
