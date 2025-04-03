using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class ComprasService
    {
        private readonly AppDbContext _context;

        public ComprasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Compras>> GetAll()
        {
            return await _context.Compras
                .Where(c => c.Estado)
                .ToListAsync();
        }

        public async Task<Compras?> GetById(int id)
        {
            return await _context.Compras
                .FirstOrDefaultAsync(c => c.IdCompra == id && c.Estado);
        }

        public async Task<Compras> Create(ComprasDto dto)
        {
            var nueva = new Compras
            {
                IdSucursal = dto.IdSucursal!.Value,
                IdProveedor = dto.IdProveedor!.Value,
                FechaCompra = dto.FechaCompra ?? DateTime.Now,
                Total = dto.Total ?? 0,
                Estado = dto.Estado ?? true
            };

            _context.Compras.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, ComprasDto dto)
        {
            var compra = await _context.Compras.FirstOrDefaultAsync(c => c.IdCompra == id && c.Estado);
            if (compra == null) return false;

            if (dto.IdSucursal.HasValue) compra.IdSucursal = dto.IdSucursal.Value;
            if (dto.IdProveedor.HasValue) compra.IdProveedor = dto.IdProveedor.Value;
            if (dto.FechaCompra.HasValue) compra.FechaCompra = dto.FechaCompra.Value;
            if (dto.Total.HasValue) compra.Total = dto.Total.Value;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                var detalles = await _context.DetallesCompra
                    .Where(d => d.IdCompra == id && d.Estado)
                    .ToListAsync();

                foreach (var detalle in detalles)
                {
                    detalle.Estado = false;
                }

                compra.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Desactivar(int id)
        {
            var compra = await _context.Compras.FirstOrDefaultAsync(c => c.IdCompra == id && c.Estado);
            if (compra == null) return false;

            // Desactivar los detalles de compra relacionados
            var detalles = await _context.DetallesCompra
                .Where(d => d.IdCompra == id && d.Estado)
                .ToListAsync();

            foreach (var detalle in detalles)
            {
                detalle.Estado = false;
            }

            compra.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}