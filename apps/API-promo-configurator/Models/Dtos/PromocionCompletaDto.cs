using System;

namespace API_promo_configurator.Models.Dtos;

public class PromocionCompletaDto
{
    // Datos obligatorios de la promoción
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string TipoDescuento { get; set; } = null!;
    public decimal ValorDescuento { get; set; }
    public string AplicaA { get; set; } = null!;
    public int DuracionMeses { get; set; } = 1;
    
    // Campos opcionales - si están vacíos, no se crean registros
    public List<int>? IdServicios { get; set; } = null;
    public List<AlcanceDto>? Alcances { get; set; } = null;
}

public class AlcanceDto
{
    public int? IdEstado { get; set; }
    public int? IdMunicipio { get; set; }
    public int? IdCiudad { get; set; }
    public int? IdColonia { get; set; }
    public int? IdSucursal { get; set; }
} 