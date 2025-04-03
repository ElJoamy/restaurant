using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class MesasDto
    {
        public int? IdSucursal { get; set; }
        public int? NumeroMesa { get; set; }
        public int? Capacidad { get; set; }
        public string? EstadoMesa { get; set; }
    }
}
