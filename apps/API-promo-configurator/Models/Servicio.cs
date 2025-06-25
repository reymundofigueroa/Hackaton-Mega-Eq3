using System;
using System.Collections.Generic;

namespace API_promo_configurator.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioBaseActual { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ContratoServicio> ContratoServicios { get; set; } = new List<ContratoServicio>();
}
