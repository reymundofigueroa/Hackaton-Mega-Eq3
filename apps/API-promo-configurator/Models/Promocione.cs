using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Promocione
{
    [Key]
    [Column("id_promocion")]
    public int IdPromocion { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Column("fecha_inicio")]
    public DateOnly FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateOnly FechaFin { get; set; }

    [Column("tipo_descuento")]
    [StringLength(50)]
    [Unicode(false)]
    public string TipoDescuento { get; set; } = null!;

    [Column("valor_descuento", TypeName = "decimal(10, 2)")]
    public decimal ValorDescuento { get; set; }

    [Column("aplica_a")]
    [StringLength(50)]
    [Unicode(false)]
    public string AplicaA { get; set; } = null!;

    [Column("duracion_meses")]
    public int DuracionMeses { get; set; }

    [InverseProperty("IdPromocionNavigation")]
    public virtual ICollection<ContratoPromocione> ContratoPromociones { get; set; } = new List<ContratoPromocione>();

    [InverseProperty("IdPromocionNavigation")]
    public virtual ICollection<PromocionAlcance> PromocionAlcances { get; set; } = new List<PromocionAlcance>();

    [ForeignKey("IdPromocion")]
    [InverseProperty("Promociones")]
    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();

}
