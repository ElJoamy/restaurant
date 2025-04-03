using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class InventarioService
    {
        private readonly AppDbContext _context;

        public InventarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventario>> GetAll()
        {
            return await _context.Inventarios
                .Where(i => i.Estado)
                .ToListAsync();
        }

        public async Task<Inventario?> GetById(int id)
        {
            return await _context.Inventarios
                .FirstOrDefaultAsync(i => i.IdInventario == id && i.Estado);
        }

        public async Task<Inventario> Create(InventarioDto dto)
        {
            var nuevo = new Inventario
            {
                IdSucursal = dto.IdSucursal ?? throw new ArgumentNullException(nameof(dto.IdSucursal)),
                IdProducto = dto.IdProducto ?? throw new ArgumentNullException(nameof(dto.IdProducto)),
                Cantidad = dto.Cantidad ?? 0,
                FechaRegistro = dto.FechaRegistro ?? DateTime.Now,
                Estado = dto.Estado ?? true
            };

            _context.Inventarios.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<bool> Update(int id, InventarioDto dto)
        {
            var inventario = await _context.Inventarios.FirstOrDefaultAsync(i => i.IdInventario == id && i.Estado);
            if (inventario == null) return false;

            inventario.IdSucursal = dto.IdSucursal ?? inventario.IdSucursal;
            inventario.IdProducto = dto.IdProducto ?? inventario.IdProducto;
            inventario.Cantidad = dto.Cantidad ?? inventario.Cantidad;
            inventario.FechaRegistro = dto.FechaRegistro ?? inventario.FechaRegistro;

            if (dto.Estado.HasValue)
                inventario.Estado = dto.Estado.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Deactivate(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null || !inventario.Estado) return false;

            inventario.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
