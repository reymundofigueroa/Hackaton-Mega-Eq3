using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Table("Contrato_Promociones")]
public partial class ContratoPromocione
{
    [Key]
    [Column("id_contrato_promocion")]
    public int IdContratoPromocion { get; set; }

    [Column("id_contrato")]
    public int IdContrato { get; set; }

    [Column("id_promocion")]
    public int IdPromocion { get; set; }

    [Column("fecha_aplicacion")]
    public DateOnly FechaAplicacion { get; set; }

    [Column("metadata")]
    [StringLength(500)]
    public string? Metadata { get; set; }

    [ForeignKey("IdContrato")]
    [InverseProperty("ContratoPromociones")]
    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    [ForeignKey("IdPromocion")]
    [InverseProperty("ContratoPromociones")]
    public virtual Promocione IdPromocionNavigation { get; set; } = null!;
}
