using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Table("Contrato_Servicios")]
[Index("IdContrato", Name = "IX_ContratoServicios_IdContrato")]
[Index("IdContrato", "IdServicio", Name = "UQ__Contrato__29A22DAAEF60BAEA", IsUnique = true)]
public partial class ContratoServicio
{
    [Key]
    [Column("id_contrato_servicio")]
    public int IdContratoServicio { get; set; }

    [Column("id_contrato")]
    public int IdContrato { get; set; }

    [Column("id_servicio")]
    public int IdServicio { get; set; }

    [Column("precio_contratado", TypeName = "decimal(10, 2)")]
    public decimal PrecioContratado { get; set; }

    [Column("fecha_alta")]
    public DateOnly FechaAlta { get; set; }

    [ForeignKey("IdContrato")]
    [InverseProperty("ContratoServicios")]
    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    [ForeignKey("IdServicio")]
    [InverseProperty("ContratoServicios")]
    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
