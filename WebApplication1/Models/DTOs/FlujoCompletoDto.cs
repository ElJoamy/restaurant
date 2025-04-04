using WebApplication1.Enums;

namespace WebApplication1.Models.DTOs
{
    public class FlujoCompletoDto
    {
        public int IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public int IdMesa { get; set; }

        public MetodoPagoEnum MetodoPago { get; set; }

        public string NIT { get; set; } = string.Empty;

        public List<DetalleVentaDto> Detalles { get; set; } = new();
    }

    public class DetalleProductoDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
    public class FlujoCompletoDatosDto
    {
        public List<Cliente> Clientes { get; set; } = new();
        public List<Productos> Productos { get; set; } = new();
        public List<Mesas> MesasDisponibles { get; set; } = new();
        public List<MetodoPago> MetodosPago { get; set; } = new();
        public List<Sucursal> Sucursales { get; set; } = new();
    }
}