namespace WebApplication1.Models.DTOs
{
    public class VentaCompletaDto
    {
        public int IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public List<DetalleVentaDto> Detalles { get; set; }
        public int IdMetodoPago { get; set; }
        public string NIT { get; set; } = string.Empty;
    }

    public class DetalleVentaDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
    public class VentaDatosInicialesDto
    {
        public List<Cliente> Clientes { get; set; }
        public List<Productos> ProductosDisponibles { get; set; }
        public List<MetodoPago> MetodosPago { get; set; }
        public List<Sucursal> Sucursales { get; set; }
    }
}