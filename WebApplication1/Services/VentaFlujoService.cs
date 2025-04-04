using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class VentaFlujoService
    {
        private readonly AppDbContext _context;

        public VentaFlujoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcesarVenta(VentaCompletaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var venta = new Ventas
                {
                    IdCliente = dto.IdCliente,
                    IdSucursal = dto.IdSucursal,
                    FechaVenta = DateTime.Now,
                    Estado = EstadoVenta.Pendiente,
                    Total = 0
                };

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                decimal total = 0;

                foreach (var detalle in dto.Detalles)
                {
                    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == detalle.IdProducto);
                    if (producto == null || producto.Stock < detalle.Cantidad)
                        throw new Exception("Producto no encontrado o stock insuficiente.");

                    var precioUnitario = producto.Precio ?? 0;

                    var detalleVenta = new DetalleVentas
                    {
                        IdVenta = venta.IdVenta,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = precioUnitario
                    };

                    total += precioUnitario * detalle.Cantidad;

                    producto.Stock -= detalle.Cantidad;

                    if (producto.Stock == 0)
                        producto.Estado = EstadoProducto.Agotado;

                    _context.DetallesVentas.Add(detalleVenta);
                    _context.Productos.Update(producto);
                }


                venta.Total = total;
                venta.Estado = EstadoVenta.Pagado;

                var factura = new Facturas
                {
                    IdVenta = venta.IdVenta,
                    IdMetodoPago = dto.IdMetodoPago,
                    NIT = dto.NIT,
                    FechaFactura = DateTime.Now,
                    MontoTotal = total
                };

                _context.Facturas.Add(factura);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<VentaDatosInicialesDto> ObtenerDatosIniciales()
        {
            return new VentaDatosInicialesDto
            {
                Clientes = await _context.Clientes.ToListAsync(),
                ProductosDisponibles = await _context.Productos
                    .Where(p => p.Estado == EstadoProducto.Disponible && p.Stock > 0)
                    .ToListAsync(),
                MetodosPago = await _context.MetodosPago.ToListAsync(),
                Sucursales = await _context.Sucursales
                    .Where(s => s.Estado)
                    .ToListAsync()
            };
        }

    }
}