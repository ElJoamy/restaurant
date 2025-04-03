using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class ProductosService
    {
        private readonly AppDbContext _context;

        public ProductosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Productos>> GetAll()
        {
            return await _context.Productos
                .Where(p => p.Estado == EstadoProducto.Disponible)
                .ToListAsync();
        }

        public async Task<Productos?> GetById(int id)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id && p.Estado == EstadoProducto.Disponible);
        }

        public async Task<Productos> Create(ProductosDto dto)
        {
            var producto = new Productos
            {
                IdCategoria = dto.IdCategoria,
                Nombre = dto.Nombre!,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                Stock = dto.Stock ?? 0,
                Estado = Enum.TryParse(dto.Estado, true, out EstadoProducto estado)
                    ? estado
                    : EstadoProducto.Disponible
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> Update(int id, ProductosDto dto)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null) return false;

            producto.IdCategoria = dto.IdCategoria ?? producto.IdCategoria;
            producto.Nombre = dto.Nombre ?? producto.Nombre;
            producto.Descripcion = dto.Descripcion ?? producto.Descripcion;
            producto.Precio = dto.Precio ?? producto.Precio;
            producto.Categoria = dto.Categoria ?? producto.Categoria;
            producto.Stock = dto.Stock ?? producto.Stock;

            if (!string.IsNullOrWhiteSpace(dto.Estado)
                && Enum.TryParse(dto.Estado, true, out EstadoProducto nuevoEstado))
            {
                producto.Estado = nuevoEstado;

                if (nuevoEstado != EstadoProducto.Disponible)
                {
                    // Desactivar hijos
                    var detallesCompra = await _context.DetallesCompra
                        .Where(dc => dc.IdProducto == id && dc.Estado)
                        .ToListAsync();
                    foreach (var dc in detallesCompra) dc.Estado = false;

                    var detallesPedido = await _context.DetallesPedido
                        .Where(dp => dp.IdProducto == id)
                        .ToListAsync();
                    _context.DetallesPedido.RemoveRange(detallesPedido);

                    var inventario = await _context.Inventarios
                        .Where(i => i.IdProducto == id && i.Estado)
                        .ToListAsync();
                    foreach (var i in inventario) i.Estado = false;

                    var detallesVentas = await _context.DetallesVentas
                        .Where(dv => dv.IdProducto == id)
                        .ToListAsync();
                    _context.DetallesVentas.RemoveRange(detallesVentas);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Desactivar(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null) return false;

            producto.Estado = EstadoProducto.Descontinuado;

            var detallesCompra = await _context.DetallesCompra
                .Where(dc => dc.IdProducto == id && dc.Estado)
                .ToListAsync();
            foreach (var dc in detallesCompra) dc.Estado = false;

            var detallesPedido = await _context.DetallesPedido
                .Where(dp => dp.IdProducto == id)
                .ToListAsync();
            _context.DetallesPedido.RemoveRange(detallesPedido);

            var inventario = await _context.Inventarios
                .Where(i => i.IdProducto == id && i.Estado)
                .ToListAsync();
            foreach (var i in inventario) i.Estado = false;

            var detallesVentas = await _context.DetallesVentas
                .Where(dv => dv.IdProducto == id)
                .ToListAsync();
            _context.DetallesVentas.RemoveRange(detallesVentas);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
