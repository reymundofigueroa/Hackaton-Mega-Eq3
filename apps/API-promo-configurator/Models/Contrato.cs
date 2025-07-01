using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Index("IdSuscriptor", Name = "IX_Contratos_IdSuscriptor")]
public partial class Contrato
{
    [Key]
    [Column("id_contrato")]
    public int IdContrato { get; set; }

    [Column("id_suscriptor")]
    public int IdSuscriptor { get; set; }

    [Column("id_sucursal")]
    public int IdSucursal { get; set; }

    [Column("fecha_contratacion")]
    public DateOnly FechaContratacion { get; set; }

    [Column("plazo_forzoso_meses")]
    public int PlazoForzosoMeses { get; set; }

    [Column("estado")]
    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdContratoNavigation")]
    public virtual ICollection<ContratoPromocione> ContratoPromociones { get; set; } = new List<ContratoPromocione>();

    [InverseProperty("IdContratoNavigation")]
    public virtual ICollection<ContratoServicio> ContratoServicios { get; set; } = new List<ContratoServicio>();

    [ForeignKey("IdSucursal")]
    [InverseProperty("Contratos")]
    public virtual Sucursale IdSucursalNavigation { get; set; } = null!;

    /*
    [ForeignKey("IdSuscriptor")]
    [InverseProperty("Contratos")]
    public virtual Suscriptore IdSuscriptorNavigation { get; set; } = null!;
    */
    [ForeignKey("IdSuscriptor")]
    public virtual Suscriptore Suscriptore { get; set; } = null!;

    [InverseProperty("IdContratoNavigation")]
    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();
}
