using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class VentasDto
    {
        public int? IdCliente { get; set; }
        public int? IdSucursal { get; set; }
        public DateTime? FechaVenta { get; set; } = DateTime.Now;
        public decimal? Total { get; set; } = 0;
        public string? Estado { get; set; }
    }
}
