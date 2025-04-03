using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class ProveedoresService
    {
        private readonly AppDbContext _context;

        public ProveedoresService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedores>> GetAll()
        {
            return await _context.Proveedores
                .Where(p => p.Estado)
                .ToListAsync();
        }

        public async Task<Proveedores?> GetById(int id)
        {
            return await _context.Proveedores
                .FirstOrDefaultAsync(p => p.IdProveedor == id && p.Estado);
        }

        public async Task<Proveedores> Create(ProveedoresDto dto)
        {
            var proveedor = new Proveedores
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Estado = dto.Estado ?? true
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> Update(int id, ProveedoresDto dto)
        {
            var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.IdProveedor == id && p.Estado);
            if (proveedor == null) return false;

            proveedor.Nombre = dto.Nombre ?? proveedor.Nombre;
            proveedor.Direccion = dto.Direccion ?? proveedor.Direccion;
            proveedor.Telefono = dto.Telefono ?? proveedor.Telefono;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                var compras = await _context.Compras
                    .Where(c => c.IdProveedor == id && c.Estado)
                    .ToListAsync();

                foreach (var compra in compras)
                {
                    compra.Estado = false;
                }

                proveedor.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Desactivar(int id)
        {
            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.IdProveedor == id && p.Estado);

            if (proveedor == null) return false;

            // Desactivar compras del proveedor
            var compras = await _context.Compras
                .Where(c => c.IdProveedor == id && c.Estado)
                .ToListAsync();

            foreach (var compra in compras)
            {
                compra.Estado = false;
            }

            proveedor.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
