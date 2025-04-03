using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class DetalleCompraService
    {
        private readonly AppDbContext _context;

        public DetalleCompraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleCompra>> GetAll()
        {
            return await _context.DetallesCompra
                .Where(dc => dc.Estado)
                .ToListAsync();
        }

        public async Task<DetalleCompra?> GetById(int id)
        {
            return await _context.DetallesCompra
                .FirstOrDefaultAsync(dc => dc.IdDetalleCompra == id && dc.Estado);
        }

        public async Task<DetalleCompra> Create(DetalleCompraDto dto)
        {
            var detalle = new DetalleCompra
            {
                IdCompra = dto.IdCompra!.Value,
                IdProducto = dto.IdProducto!.Value,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Estado = dto.Estado ?? true
            };

            _context.DetallesCompra.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<bool> Update(int id, DetalleCompraDto dto)
        {
            var detalle = await _context.DetallesCompra.FirstOrDefaultAsync(dc => dc.IdDetalleCompra == id && dc.Estado);
            if (detalle == null) return false;

            detalle.IdCompra = dto.IdCompra ?? detalle.IdCompra;
            detalle.IdProducto = dto.IdProducto ?? detalle.IdProducto;
            detalle.Cantidad = dto.Cantidad ?? detalle.Cantidad;
            detalle.PrecioUnitario = dto.PrecioUnitario ?? detalle.PrecioUnitario;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                detalle.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Desactivar(int id)
        {
            var detalle = await _context.DetallesCompra.FirstOrDefaultAsync(dc => dc.IdDetalleCompra == id && dc.Estado);
            if (detalle == null) return false;

            detalle.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}