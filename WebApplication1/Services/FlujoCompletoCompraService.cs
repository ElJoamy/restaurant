using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class FlujoCompletoCompraService
    {
        private readonly AppDbContext _context;

        public FlujoCompletoCompraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompraCompletaDatosDto> ObtenerDatosIniciales()
        {
            return new CompraCompletaDatosDto
            {
                Proveedores = await _context.Proveedores.Where(p => p.Estado).ToListAsync(),
                Productos = await _context.Productos.Where(p => p.Estado == EstadoProducto.Disponible).ToListAsync(),
                Sucursales = await _context.Sucursales.Where(s => s.Estado).ToListAsync(),
                Categorias = await _context.CategoriasProductos.ToListAsync()
            };
        }

        public async Task<bool> ProcesarCompra(CompraCompletaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var compra = new Compras
                {
                    IdProveedor = dto.IdProveedor,
                    IdSucursal = dto.IdSucursal,
                    FechaCompra = DateTime.Now,
                    Total = 0,
                    Estado = true
                };

                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();

                decimal total = 0;

                foreach (var item in dto.Detalles)
                {
                    var producto = await _context.Productos
                        .FirstOrDefaultAsync(p => p.IdProducto == item.IdProducto)
                        ?? throw new Exception("Producto no encontrado");

                    producto.Stock += item.Cantidad;

                    _context.DetallesCompra.Add(new DetalleCompra
                    {
                        IdCompra = compra.IdCompra,
                        IdProducto = producto.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        Estado = true
                    });

                    _context.Inventarios.Add(new Inventario
                    {
                        IdProducto = producto.IdProducto,
                        IdSucursal = dto.IdSucursal,
                        Cantidad = item.Cantidad,
                        FechaRegistro = DateTime.Now,
                        Estado = true
                    });

                    total += item.Cantidad * item.PrecioUnitario;
                }

                compra.Total = total;
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
