namespace WebApplication1.Models.DTOs
{
    public class InventarioDto
    {
        public int? IdSucursal { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public bool? Estado { get; set; } = true;
    }
}
