namespace WebApplication1.Models.DTOs
{
    public class FacturasDto
    {
        public int? IdVenta { get; set; }
        public int? IdMetodoPago { get; set; }
        public string? NIT { get; set; }
        public DateTime? FechaFactura { get; set; }
        public decimal? MontoTotal { get; set; }
    }
}
