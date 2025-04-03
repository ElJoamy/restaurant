using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProfileService
    {
        private readonly AppDbContext _context;

        public ProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object?> GetProfile(int userId)
        {
            var persona = await _context.Personal
                .FirstOrDefaultAsync(p => p.IdPersonal == userId && p.Estado);

            if (persona == null)
                return null;

            var cargo = await _context.Cargos
                .Where(c => c.IdCargo == persona.IdCargo)
                .Select(c => c.NombreCargo)
                .FirstOrDefaultAsync();

            return new
            {
                persona.IdPersonal,
                persona.Usuario,
                Cargo = cargo
            };
        }
    }
}
