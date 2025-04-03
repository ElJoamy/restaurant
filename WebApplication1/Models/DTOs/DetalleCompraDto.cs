namespace WebApplication1.Models.DTOs
{
    public class DetalleCompraDto
    {
        public int? IdCompra { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
