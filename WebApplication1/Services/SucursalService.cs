using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class SucursalService
    {
        private readonly AppDbContext _context;

        public SucursalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sucursal>> GetAll()
        {
            return await _context.Sucursales
                .Where(s => s.Estado)
                .ToListAsync();
        }

        public async Task<Sucursal?> GetById(int id)
        {
            return await _context.Sucursales
                .FirstOrDefaultAsync(s => s.IdSucursal == id && s.Estado);
        }

        public async Task<Sucursal> Create(SucursalDto dto)
        {
            var nueva = new Sucursal
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Capacidad = dto.Capacidad,
                Estado = dto.Estado ?? true
            };

            _context.Sucursales.Add(nueva);
            await _context.SaveChangesAsync();
            return nueva;
        }

        public async Task<bool> Update(int id, SucursalDto dto)
        {
            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.IdSucursal == id && s.Estado);
            if (sucursal == null) return false;

            sucursal.Nombre = dto.Nombre ?? sucursal.Nombre;
            sucursal.Direccion = dto.Direccion ?? sucursal.Direccion;
            sucursal.Telefono = dto.Telefono ?? sucursal.Telefono;
            sucursal.Capacidad = dto.Capacidad ?? sucursal.Capacidad;

            if (dto.Estado.HasValue && !dto.Estado.Value)
            {
                // Desactivar hijos
                var hijos = new List<object>();
                hijos.AddRange(_context.Personal.Where(p => p.IdSucursal == id && p.Estado));
                hijos.AddRange(_context.Inventarios.Where(i => i.IdSucursal == id && i.Estado));
                hijos.AddRange(_context.Compras.Where(c => c.IdSucursal == id && c.Estado));
                hijos.AddRange(_context.Mesas.Where(m => m.IdSucursal == id && m.EstadoMesa == EstadoMesa.Disponible));
                hijos.AddRange(_context.Reservas.Where(r => r.IdSucursal == id && r.Estado == EstadoReserva.Pendiente));
                hijos.AddRange(_context.Ventas.Where(v => v.IdSucursal == id && v.Estado == EstadoVenta.Pendiente));

                foreach (var hijo in hijos)
                {
                    switch (hijo)
                    {
                        case Personal p:
                            p.Estado = false;
                            break;
                        case Inventario i:
                            i.Estado = false;
                            break;
                        case Compras c:
                            c.Estado = false;
                            break;
                        case Mesas m:
                            m.EstadoMesa = EstadoMesa.Disponible; // O cambiar a Reservada si aplica lógica
                            break;
                        case Reservas r:
                            r.Estado = EstadoReserva.Cancelada;
                            break;
                        case Ventas v:
                            v.Estado = EstadoVenta.Cancelado;
                            break;
                    }
                }

                sucursal.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}