using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class SucursalService
    {
        private readonly AppDbContext _context;

        public SucursalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sucursal>> GetAll()
        {
            return await _context.Sucursales.Where(s => s.Estado).ToListAsync();
        }

        public async Task<Sucursal?> GetById(int id)
        {
            return await _context.Sucursales.FirstOrDefaultAsync(s => s.IdSucursal == id && s.Estado);
        }

        public async Task<List<Sucursal>> GetByNombre(string nombre)
        {
            return await _context.Sucursales
                .Where(s => s.Estado && s.Nombre!.Contains(nombre))
                .ToListAsync();
        }

        public async Task<Sucursal> Create(SucursalDto dto)
        {
            var nueva = new Sucursal
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Capacidad = dto.Capacidad,
                Estado = dto.Estado ?? true
            };

            _context.Sucursales.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, SucursalDto dto)
        {
            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.IdSucursal == id && s.Estado);
            if (sucursal == null) return false;

            sucursal.Nombre = dto.Nombre;
            sucursal.Direccion = dto.Direccion;
            sucursal.Telefono = dto.Telefono;
            sucursal.Capacidad = dto.Capacidad;
            sucursal.Estado = dto.Estado ?? true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
