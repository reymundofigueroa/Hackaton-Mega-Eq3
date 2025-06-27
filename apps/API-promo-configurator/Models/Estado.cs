using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Index("Nombre", Name = "UQ__Estados__72AFBCC6D5BDA2B8", IsUnique = true)]
public partial class Estado
{
    [Key]
    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();
}
