using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class VentasService
    {
        private readonly AppDbContext _context;

        public VentasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ventas>> GetAll()
        {
            return await _context.Ventas
                .Where(v => v.Estado != EstadoVenta.Cancelado)
                .ToListAsync();
        }

        public async Task<Ventas?> GetById(int id)
        {
            return await _context.Ventas
                .FirstOrDefaultAsync(v => v.IdVenta == id && v.Estado != EstadoVenta.Cancelado);
        }

        public async Task<Ventas> Create(VentasDto dto)
        {
            var venta = new Ventas
            {
                IdCliente = dto.IdCliente ?? 0,
                IdSucursal = dto.IdSucursal ?? 0,
                FechaVenta = dto.FechaVenta ?? DateTime.Now,
                Total = dto.Total ?? 0,
                Estado = Enum.TryParse(dto.Estado, out EstadoVenta estado) ? estado : EstadoVenta.Pendiente
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<bool> Update(int id, VentasDto dto)
        {
            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.IdVenta == id && v.Estado != EstadoVenta.Cancelado);
            if (venta == null) return false;

            if (dto.IdCliente.HasValue) venta.IdCliente = dto.IdCliente.Value;
            if (dto.IdSucursal.HasValue) venta.IdSucursal = dto.IdSucursal.Value;
            if (dto.FechaVenta.HasValue) venta.FechaVenta = dto.FechaVenta.Value;
            if (dto.Total.HasValue) venta.Total = dto.Total.Value;

            if (!string.IsNullOrWhiteSpace(dto.Estado) && Enum.TryParse(dto.Estado, out EstadoVenta nuevoEstado))
            {
                venta.Estado = nuevoEstado;

                if (nuevoEstado == EstadoVenta.Cancelado)
                {
                    var detalles = await _context.DetallesVentas
                        .Where(d => d.IdVenta == id)
                        .ToListAsync();

                    var facturas = await _context.Facturas
                        .Where(f => f.IdVenta == id)
                        .ToListAsync();

                    foreach (var d in detalles)
                        _context.DetallesVentas.Remove(d);

                    foreach (var f in facturas)
                        _context.Facturas.Remove(f);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Cancelar(int id)
        {
            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.IdVenta == id && v.Estado != EstadoVenta.Cancelado);
            if (venta == null) return false;

            venta.Estado = EstadoVenta.Cancelado;

            var detalles = await _context.DetallesVentas
                .Where(d => d.IdVenta == id)
                .ToListAsync();

            var facturas = await _context.Facturas
                .Where(f => f.IdVenta == id)
                .ToListAsync();

            foreach (var d in detalles)
                _context.DetallesVentas.Remove(d);

            foreach (var f in facturas)
                _context.Facturas.Remove(f);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
