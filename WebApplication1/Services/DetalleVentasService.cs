using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class DetalleVentasService
    {
        private readonly AppDbContext _context;

        public DetalleVentasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleVentas>> GetAll()
        {
            return await _context.DetallesVentas.ToListAsync();
        }

        public async Task<DetalleVentas?> GetById(int id)
        {
            return await _context.DetallesVentas.FirstOrDefaultAsync(d => d.IdDetalleVenta == id);
        }

        public async Task<DetalleVentas> Create(DetalleVentasDto dto)
        {
            var nuevo = new DetalleVentas
            {
                IdVenta = dto.IdVenta!.Value,
                IdProducto = dto.IdProducto!.Value,
                Cantidad = dto.Cantidad!.Value,
                PrecioUnitario = dto.PrecioUnitario!.Value,
            };

            _context.DetallesVentas.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }
    }
}