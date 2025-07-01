using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Ciudade
{
    [Key]
    [Column("id_ciudad")]
    public int IdCiudad { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [InverseProperty("IdCiudadNavigation")]
    public virtual ICollection<Colonia> Colonia { get; set; } = new List<Colonia>();

    [ForeignKey("IdMunicipio")]
    [InverseProperty("Ciudades")]
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    [InverseProperty("IdCiudadNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
