using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class TurnoPersonalService
    {
        private readonly AppDbContext _context;

        public TurnoPersonalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TurnoPersonal>> GetAll()
        {
            return await _context.TurnosPersonal
                .Where(t => t.Estado)
                .ToListAsync();
        }

        public async Task<TurnoPersonal?> GetById(int id)
        {
            return await _context.TurnosPersonal
                .FirstOrDefaultAsync(t => t.IdTurnoPersonal == id && t.Estado);
        }

        public async Task<TurnoPersonal> Create(TurnoPersonalDto dto)
        {
            var turno = new TurnoPersonal
            {
                IdPersonal = dto.IdPersonal!.Value,
                Fecha = dto.Fecha!.Value,
                NombreTurno = dto.NombreTurno!,
                HoraEntrada = dto.HoraEntrada!.Value,
                HoraSalida = dto.HoraSalida!.Value,
                Observacion = dto.Observacion,
                Estado = dto.Estado ?? true
            };

            _context.TurnosPersonal.Add(turno);
            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<bool> Update(int id, TurnoPersonalDto dto)
        {
            var turno = await _context.TurnosPersonal.FirstOrDefaultAsync(t => t.IdTurnoPersonal == id && t.Estado);
            if (turno == null) return false;

            turno.IdPersonal = dto.IdPersonal ?? turno.IdPersonal;
            turno.Fecha = dto.Fecha ?? turno.Fecha;
            turno.NombreTurno = dto.NombreTurno ?? turno.NombreTurno;
            turno.HoraEntrada = dto.HoraEntrada ?? turno.HoraEntrada;
            turno.HoraSalida = dto.HoraSalida ?? turno.HoraSalida;
            turno.Observacion = dto.Observacion ?? turno.Observacion;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                turno.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DesactivarPorPersonal(int idPersonal)
        {
            var turnos = await _context.TurnosPersonal
                .Where(t => t.IdPersonal == idPersonal && t.Estado)
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
