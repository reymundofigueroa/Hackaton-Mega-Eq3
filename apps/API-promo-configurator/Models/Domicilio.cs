using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_promo_configurator.Models;

public partial class Domicilio
{
    [Key]
    [Column("id_domicilio")]
    public int IdDomicilio { get; set; }

    [Column("calle")]
    [StringLength(255)]
    public string Calle { get; set; } = null!;

    [Column("numero_exterior")]
    [StringLength(50)]
    public string NumeroExterior { get; set; } = null!;

    [Column("numero_interior")]
    [StringLength(50)]
    public string? NumeroInterior { get; set; }

    [Column("referencias")]
    [StringLength(500)]
    public string? Referencias { get; set; }

    [Column("id_colonia")]
    public int IdColonia { get; set; }

    [ForeignKey("IdColonia")]
    [InverseProperty("Domicilios")]
    public virtual Colonia IdColoniaNavigation { get; set; } = null!;

    [InverseProperty("IdDomicilioNavigation")]
    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();

    [InverseProperty("IdDomicilioNavigation")]
    public virtual ICollection<Suscriptore> Suscriptores { get; set; } = new List<Suscriptore>();
}
