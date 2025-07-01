using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Municipio
{
    [Key]
    [Column("id_municipio")]
    public int IdMunicipio { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("id_estado")]
    public int IdEstado { get; set; }

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Municipios")]
    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    [InverseProperty("IdMunicipioNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
