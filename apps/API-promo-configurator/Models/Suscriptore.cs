using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

[Index("Email", Name = "UQ__Suscript__AB6E6164033A16E9", IsUnique = true)]
[Index("Rfc", Name = "UQ__Suscript__C2B034948441139B", IsUnique = true)]
public partial class Suscriptore
{
    [Key]
    [Column("id_suscriptor")]
    public int IdSuscriptor { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido_paterno")]
    [StringLength(100)]
    public string ApellidoPaterno { get; set; } = null!;

    [Column("apellido_materno")]
    [StringLength(100)]
    public string? ApellidoMaterno { get; set; }

    [Column("rfc")]
    [StringLength(13)]
    [Unicode(false)]
    public string? Rfc { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("telefono_contacto")]
    [StringLength(20)]
    [Unicode(false)]
    public string TelefonoContacto { get; set; } = null!;

    [Column("fecha_registro")]
    public DateOnly FechaRegistro { get; set; }

    [Column("id_domicilio")]
    public int IdDomicilio { get; set; }

    [InverseProperty("IdSuscriptorNavigation")]
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    [ForeignKey("IdDomicilio")]
    [InverseProperty("Suscriptores")]
    public virtual Domicilio IdDomicilioNavigation { get; set; } = null!;
}
