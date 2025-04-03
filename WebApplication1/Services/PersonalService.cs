using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class PersonalService
    {
        private readonly AppDbContext _context;

        public PersonalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Personal>> GetAll()
        {
            return await _context.Personal
                .Where(p => p.Estado)
                .ToListAsync();
        }

        public async Task<Personal?> GetById(int id)
        {
            return await _context.Personal
                .FirstOrDefaultAsync(p => p.IdPersonal == id && p.Estado);
        }

        public async Task<Personal> Create(PersonalDto dto)
        {
            var nuevo = new Personal
            {
                // Validaciones aquí si deseas
                IdSucursal = dto.IdSucursal!.Value,
                IdCargo = dto.IdCargo!.Value,
                Nombre = dto.Nombre!,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno!,
                CI = dto.CI,
                Correo = dto.Correo!,
                Telefono = dto.Telefono,
                Genero = dto.Genero!,
                Usuario = dto.Usuario!,
                Contrasena = dto.Contrasena!,
                Salario = dto.Salario,
                Estado = dto.Estado ?? true
            };

            _context.Personal.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<bool> Update(int id, PersonalDto dto)
        {
            var personal = await _context.Personal.FirstOrDefaultAsync(p => p.IdPersonal == id && p.Estado);
            if (personal == null) return false;

            personal.IdSucursal = dto.IdSucursal ?? personal.IdSucursal;
            personal.IdCargo = dto.IdCargo ?? personal.IdCargo;
            personal.Nombre = dto.Nombre ?? personal.Nombre;
            personal.ApellidoPaterno = dto.ApellidoPaterno ?? personal.ApellidoPaterno;
            personal.ApellidoMaterno = dto.ApellidoMaterno ?? personal.ApellidoMaterno;
            personal.CI = dto.CI ?? personal.CI;
            personal.Correo = dto.Correo ?? personal.Correo;
            personal.Telefono = dto.Telefono ?? personal.Telefono;
            personal.Genero = dto.Genero ?? personal.Genero;
            personal.Salario = dto.Salario ?? personal.Salario;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                // Desactivar sus turnos
                var turnos = _context.TurnosPersonal.Where(t => t.IdPersonal == id && t.Estado);
                foreach (var turno in turnos)
                {
                    turno.Estado = false;
                }

                personal.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Desactivar(int id)
        {
            var personal = await _context.Personal.FirstOrDefaultAsync(p => p.IdPersonal == id && p.Estado);
            if (personal == null) return false;

            personal.Estado = false;

            var turnos = await _context.TurnosPersonal
                .Where(t => t.IdPersonal == id && t.Estado)
                .ToListAsync();

            foreach (var turno in turnos)
            {
                turno.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}