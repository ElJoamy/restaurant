using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class ReservasDto
    {
        public int? IdCliente { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdMesa { get; set; }
        public DateTime? FechaReserva { get; set; }
        public string? Estado { get; set; }
    }
}
