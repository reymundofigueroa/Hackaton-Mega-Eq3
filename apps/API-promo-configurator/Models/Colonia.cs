using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Colonia
{
    [Key]
    [Column("id_colonia")]
    public int IdColonia { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("codigo_postal")]
    [StringLength(10)]
    [Unicode(false)]
    public string CodigoPostal { get; set; } = null!;

    [Column("id_ciudad")]
    public int IdCiudad { get; set; }

    [InverseProperty("IdColoniaNavigation")]
    public virtual ICollection<Domicilio> Domicilios { get; set; } = new List<Domicilio>();

    [ForeignKey("IdCiudad")]
    [InverseProperty("Colonia")]
    public virtual Ciudade IdCiudadNavigation { get; set; } = null!;

    [InverseProperty("IdColoniaNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();

    [ForeignKey("IdColonia")]
    [InverseProperty("IdColonia")]
    public virtual ICollection<Sucursale> IdSucursals { get; set; } = new List<Sucursale>();
}
