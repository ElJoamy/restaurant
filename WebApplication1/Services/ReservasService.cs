using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class ReservasService
    {
        private readonly AppDbContext _context;

        public ReservasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservas>> GetAll()
        {
            return await _context.Reservas
                .Where(r => r.Estado != EstadoReserva.Cancelada)
                .ToListAsync();
        }

        public async Task<Reservas?> GetById(int id)
        {
            return await _context.Reservas
                .FirstOrDefaultAsync(r => r.IdReserva == id && r.Estado != EstadoReserva.Cancelada);
        }

        public async Task<Reservas> Create(ReservasDto dto)
        {
            var nueva = new Reservas
            {
                IdCliente = dto.IdCliente!.Value,
                IdSucursal = dto.IdSucursal!.Value,
                IdMesa = dto.IdMesa!.Value,
                FechaReserva = dto.FechaReserva ?? DateTime.Now,
                Estado = Enum.TryParse(dto.Estado, out EstadoReserva estado) ? estado : EstadoReserva.Pendiente
            };

            _context.Reservas.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, ReservasDto dto)
        {
            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == id && r.Estado != EstadoReserva.Cancelada);
            if (reserva == null) return false;

            reserva.IdCliente = dto.IdCliente ?? reserva.IdCliente;
            reserva.IdSucursal = dto.IdSucursal ?? reserva.IdSucursal;
            reserva.IdMesa = dto.IdMesa ?? reserva.IdMesa;
            reserva.FechaReserva = dto.FechaReserva ?? reserva.FechaReserva;

            if (!string.IsNullOrWhiteSpace(dto.Estado) && Enum.TryParse(dto.Estado, out EstadoReserva nuevoEstado))
                reserva.Estado = nuevoEstado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Cancelar(int id)
        {
            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == id);
            if (reserva == null) return false;

            reserva.Estado = EstadoReserva.Cancelada;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
