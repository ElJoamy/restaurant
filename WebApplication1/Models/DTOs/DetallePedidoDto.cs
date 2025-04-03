namespace WebApplication1.Models.DTOs
{
    public class DetallePedidoDto
    {
        public int? IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
    }
}