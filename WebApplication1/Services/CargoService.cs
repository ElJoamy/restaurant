using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class CargoService
    {
        private readonly AppDbContext _context;

        public CargoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cargo>> GetAll()
        {
            return await _context.Cargos
                .Where(c => c.Estado)
                .ToListAsync();
        }

        public async Task<List<Cargo>> GetByNombre(string nombre)
        {
            return await _context.Cargos
                .Where(c => c.Estado && c.NombreCargo.Contains(nombre))
                .ToListAsync();
        }

        public async Task<Cargo?> GetById(int id)
        {
            return await _context.Cargos
                .FirstOrDefaultAsync(c => c.IdCargo == id && c.Estado);
        }

        public async Task<Cargo> Create(CargoDto dto)
        {
            var nuevo = new Cargo
            {
                NombreCargo = dto.NombreCargo
            };

            _context.Cargos.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<bool> Update(int id, CargoDto dto)
        {
            var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.IdCargo == id && c.Estado);
            if (cargo == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.NombreCargo))
                cargo.NombreCargo = dto.NombreCargo;

            if (dto.Estado.HasValue)
                cargo.Estado = dto.Estado.Value;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Desactivar(int id)
        {
            var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.IdCargo == id && c.Estado);
            if (cargo == null) return false;

            // Desactivar el cargo
            cargo.Estado = false;

            // Desactivar todo el personal que tiene ese cargo
            var personalRelacionado = await _context.Personal
                .Where(p => p.IdCargo == id && p.Estado)
                .ToListAsync();

            foreach (var persona in personalRelacionado)
            {
                persona.Estado = false;

                // Desactivar también sus turnos
                var turnos = await _context.TurnosPersonal
                    .Where(t => t.IdPersonal == persona.IdPersonal && t.Estado)
                    .ToListAsync();

                foreach (var turno in turnos)
                {
                    turno.Estado = false;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
