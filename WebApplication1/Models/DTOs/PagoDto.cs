namespace WebApplication1.Models.DTOs
{
    public class PagoDto
    {
        public int? IdPedido { get; set; }
        public string? MetodoPago { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaPago { get; set; } = DateTime.Now;
    }
}
