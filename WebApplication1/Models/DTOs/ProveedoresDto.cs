namespace WebApplication1.Models.DTOs
{
    public class ProveedoresDto
    {
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool? Estado { get; set; } = true;
    }
}