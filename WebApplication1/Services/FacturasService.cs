using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class FacturasService
    {
        private readonly AppDbContext _context;

        public FacturasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Facturas>> GetAll()
        {
            return await _context.Facturas.ToListAsync();
        }

        public async Task<Facturas?> GetById(int id)
        {
            return await _context.Facturas.FindAsync(id);
        }

        public async Task<Facturas> Create(FacturasDto dto)
        {
            var factura = new Facturas
            {
                IdVenta = dto.IdVenta!.Value,
                IdMetodoPago = dto.IdMetodoPago!.Value,
                NIT = dto.NIT!,
                FechaFactura = dto.FechaFactura ?? DateTime.Now,
                MontoTotal = dto.MontoTotal!.Value
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<bool> Update(int id, FacturasDto dto)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null) return false;

            factura.IdVenta = dto.IdVenta ?? factura.IdVenta;
            factura.IdMetodoPago = dto.IdMetodoPago ?? factura.IdMetodoPago;
            factura.NIT = dto.NIT ?? factura.NIT;
            factura.FechaFactura = dto.FechaFactura ?? factura.FechaFactura;
            factura.MontoTotal = dto.MontoTotal ?? factura.MontoTotal;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
