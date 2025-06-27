using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Table("Movimientos_Cuenta")]
[Index("IdContrato", Name = "IX_MovimientosCuenta_IdContrato")]
public partial class MovimientosCuentum
{
    [Key]
    [Column("id_movimiento")]
    public long IdMovimiento { get; set; }

    [Column("id_contrato")]
    public int IdContrato { get; set; }

    [Column("fecha_movimiento", TypeName = "datetime")]
    public DateTime FechaMovimiento { get; set; }

    [Column("concepto")]
    [StringLength(255)]
    public string Concepto { get; set; } = null!;

    [Column("monto_cargo", TypeName = "decimal(10, 2)")]
    public decimal MontoCargo { get; set; }

    [Column("monto_pago", TypeName = "decimal(10, 2)")]
    public decimal MontoPago { get; set; }

    [Column("saldo_resultante", TypeName = "decimal(12, 2)")]
    public decimal SaldoResultante { get; set; }

    [ForeignKey("IdContrato")]
    [InverseProperty("MovimientosCuenta")]
    public virtual Contrato IdContratoNavigation { get; set; } = null!;
}
