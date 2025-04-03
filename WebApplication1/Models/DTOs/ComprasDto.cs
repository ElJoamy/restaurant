namespace WebApplication1.Models.DTOs
{
    public class ComprasDto
    {
        public int? IdSucursal { get; set; }
        public int? IdProveedor { get; set; }
        public DateTime? FechaCompra { get; set; }
        public decimal? Total { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
