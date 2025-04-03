namespace WebApplication1.Models.DTOs
{
    public class ProductosDto
    {
        public int? IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string? Categoria { get; set; }
        public int? Stock { get; set; }
        public string? Estado { get; set; }
    }
}