using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class FlujoCompletoService
    {
        private readonly AppDbContext _context;

        public FlujoCompletoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FlujoCompletoDatosDto> ObtenerDatosIniciales()
        {
            return new FlujoCompletoDatosDto
            {
                Clientes = await _context.Clientes.ToListAsync(),
                Productos = await _context.Productos
                    .Where(p => p.Estado == EstadoProducto.Disponible && p.Stock > 0)
                    .ToListAsync(),
                MesasDisponibles = await _context.Mesas
                    .Where(m => m.EstadoMesa == EstadoMesa.Disponible)
                    .ToListAsync(),
                MetodosPago = await _context.MetodosPago.ToListAsync(),
                Sucursales = await _context.Sucursales
                    .Where(s => s.Estado)
                    .ToListAsync()
            };
        }

        public async Task<bool> ProcesarFlujoCompleto(FlujoCompletoDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.IdMesa == dto.IdMesa);
                if (mesa == null || mesa.EstadoMesa != EstadoMesa.Disponible)
                    throw new Exception("Mesa no disponible");

                mesa.EstadoMesa = EstadoMesa.Reservada;

                var pedido = new Pedidos
                {
                    IdCliente = dto.IdCliente,
                    IdMesa = dto.IdMesa,
                    Estado = EstadoPedido.Recibido,
                    FechaPedido = DateTime.Now,
                    Total = 0
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                decimal totalPedido = 0;
                foreach (var detalle in dto.Detalles)
                {
                    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == detalle.IdProducto);
                    if (producto == null || producto.Stock < detalle.Cantidad)
                        throw new Exception("Producto no encontrado o stock insuficiente");

                    var precioUnitario = producto.Precio ?? 0;
                    totalPedido += precioUnitario * detalle.Cantidad;

                    _context.DetallesPedido.Add(new DetallePedido
                    {
                        IdPedido = pedido.IdPedido,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = precioUnitario
                    });
                }

                pedido.Total = totalPedido;

                _context.Pagos.Add(new Pago
                {
                    IdPedido = pedido.IdPedido,
                    MetodoPago = dto.MetodoPago.ToString(),
                    Monto = totalPedido,
                    FechaPago = DateTime.Now
                });


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
    }
}