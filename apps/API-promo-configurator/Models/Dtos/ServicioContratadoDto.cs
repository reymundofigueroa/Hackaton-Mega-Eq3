namespace API_promo_configurator.Models.Dtos
{
    public class ServicioContratadoDto
    {
        public string Servicio { get; set; } = null!;
        public decimal PrecioContratado { get; set; }
        public string? Promocion { get; set; }
        public string? TipoDescuento { get; set; }
        public DateTime? FechaAplicacion { get; set; }
    }
}
