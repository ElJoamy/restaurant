namespace WebApplication1.Models.DTOs
{
    public class DetalleCompraItemDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class CompraCompletaDto
    {
        public int IdProveedor { get; set; }
        public int IdSucursal { get; set; }
        public List<DetalleCompraItemDto> Detalles { get; set; } = new();
    }

    public class CompraCompletaDatosDto
    {
        public List<Proveedores> Proveedores { get; set; } = new();
        public List<Productos> Productos { get; set; } = new();
        public List<Sucursal> Sucursales { get; set; } = new();
        public List<CategoriaProductos> Categorias { get; set; } = new();
    }
}
