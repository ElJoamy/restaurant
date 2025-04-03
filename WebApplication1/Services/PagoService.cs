using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class PagoService
    {
        private readonly AppDbContext _context;

        public PagoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pago>> GetAll()
        {
            return await _context.Pagos.ToListAsync();
        }

        public async Task<Pago?> GetById(int id)
        {
            return await _context.Pagos.FirstOrDefaultAsync(p => p.IdPago == id);
        }

        public async Task<Pago> Create(PagoDto dto)
        {
            var nuevo = new Pago
            {
                IdPedido = dto.IdPedido!.Value,
                MetodoPago = dto.MetodoPago!,
                Monto = dto.Monto ?? 0,
                FechaPago = dto.FechaPago ?? DateTime.Now
            };

            _context.Pagos.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<bool> Update(int id, PagoDto dto)
        {
            var pago = await _context.Pagos.FirstOrDefaultAsync(p => p.IdPago == id);
            if (pago == null) return false;

            pago.IdPedido = dto.IdPedido ?? pago.IdPedido;
            pago.MetodoPago = dto.MetodoPago ?? pago.MetodoPago;
            pago.Monto = dto.Monto ?? pago.Monto;
            pago.FechaPago = dto.FechaPago ?? pago.FechaPago;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
