using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class MesasService
    {
        private readonly AppDbContext _context;

        public MesasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mesas>> GetAll()
        {
            return await _context.Mesas
                .Where(m => m.EstadoMesa == EstadoMesa.Disponible)
                .ToListAsync();
        }

        public async Task<Mesas?> GetById(int id)
        {
            return await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == id && m.EstadoMesa == EstadoMesa.Disponible);
        }

        public async Task<Mesas> Create(MesasDto dto)
        {
            var nueva = new Mesas
            {
                IdSucursal = dto.IdSucursal ?? 0,
                NumeroMesa = dto.NumeroMesa ?? 0,
                Capacidad = dto.Capacidad ?? 0,
                EstadoMesa = Enum.TryParse(dto.EstadoMesa, out EstadoMesa estado) ? estado : EstadoMesa.Disponible
            };

            _context.Mesas.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, MesasDto dto)
        {
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.IdMesa == id);
            if (mesa == null) return false;

            mesa.IdSucursal = dto.IdSucursal ?? mesa.IdSucursal;
            mesa.NumeroMesa = dto.NumeroMesa ?? mesa.NumeroMesa;
            mesa.Capacidad = dto.Capacidad ?? mesa.Capacidad;

            if (!string.IsNullOrWhiteSpace(dto.EstadoMesa) &&
                Enum.TryParse(dto.EstadoMesa, out EstadoMesa nuevoEstado))
            {
                mesa.EstadoMesa = nuevoEstado;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Desactivar(int id)
        {
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.IdMesa == id && m.EstadoMesa == EstadoMesa.Disponible);
            if (mesa == null) return false;

            // Desactivar reservas relacionadas
            var reservas = await _context.Reservas
                .Where(r => r.IdMesa == id && r.Estado == EstadoReserva.Pendiente)
                .ToListAsync();

            foreach (var reserva in reservas)
            {
                reserva.Estado = EstadoReserva.Cancelada;
            }

            mesa.EstadoMesa = EstadoMesa.Reservada;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}