using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Servicio
{
    [Key]
    [Column("id_servicio")]
    public int IdServicio { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Column("precio_base_actual", TypeName = "decimal(10, 2)")]
    public decimal PrecioBaseActual { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("Servicio")]
    public virtual ICollection<ContratoServicio> ContratoServicios { get; set; } = new List<ContratoServicio>();

    /*
    [ForeignKey("IdServicio")]
    [InverseProperty("IdServicios")]

    public virtual ICollection<Promocione> IdPromocions { get; set; } = new List<Promocione>();
    */
    public virtual ICollection<Promocione> Promociones { get; set; } = new List<Promocione>();


}
