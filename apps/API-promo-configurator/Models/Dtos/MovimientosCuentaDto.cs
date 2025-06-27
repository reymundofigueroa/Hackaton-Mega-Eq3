using System;

namespace API_promo_configurator.Models.Dtos;

public class MovimientosCuentaDto
{
    public long IdMovimiento { get; set; }
    public int IdContrato { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public string Concepto { get; set; } = null!;
    public decimal MontoCargo { get; set; }
    public decimal MontoPago { get; set; }
    public decimal SaldoResultante { get; set; }
} 