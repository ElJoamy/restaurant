namespace WebApplication1.Models.DTOs
{
    public class SucursalDto
    {
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int? Capacidad { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
