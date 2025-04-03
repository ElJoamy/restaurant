using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class CategoriaProductosService
    {
        private readonly AppDbContext _context;

        public CategoriaProductosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaProductos>> GetAll()
        {
            return await _context.CategoriasProductos.ToListAsync();
        }

        public async Task<CategoriaProductos?> GetById(int id)
        {
            return await _context.CategoriasProductos.FindAsync(id);
        }

        public async Task<CategoriaProductos> Create(CategoriaProductosDto dto)
        {
            var nueva = new CategoriaProductos
            {
                Nombre = dto.Nombre
            };

            _context.CategoriasProductos.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, CategoriaProductosDto dto)
        {
            var categoria = await _context.CategoriasProductos.FirstOrDefaultAsync(c => c.IdCategoria == id);
            if (categoria == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                categoria.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
