namespace WebApplication1.Models.DTOs
{
    public class PedidosDto
    {
        public int? IdCliente { get; set; }
        public int? IdMesa { get; set; }
        public DateTime? FechaPedido { get; set; }
        public decimal? Total { get; set; }
        public string? Estado { get; set; }
    }
}