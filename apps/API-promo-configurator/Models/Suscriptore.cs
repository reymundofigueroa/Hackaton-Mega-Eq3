using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Suscriptore
{
    public int IdSuscriptor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string? Rfc { get; set; }

    public string Email { get; set; } = null!;

    public string TelefonoContacto { get; set; } = null!;

    public DateOnly FechaRegistro { get; set; }

    public int IdDomicilio { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual Domicilio IdDomicilioNavigation { get; set; } = null!;
}
