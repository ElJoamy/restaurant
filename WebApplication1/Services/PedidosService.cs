using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Enums;

namespace WebApplication1.Services
{
    public class PedidosService
    {
        private readonly AppDbContext _context;

        public PedidosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedidos>> GetAll()
        {
            return await _context.Pedidos
                .Where(p => p.Estado != EstadoPedido.Cancelado)
                .ToListAsync();
        }

        public async Task<Pedidos?> GetById(int id)
        {
            return await _context.Pedidos
                .FirstOrDefaultAsync(p => p.IdPedido == id && p.Estado != EstadoPedido.Cancelado);
        }

        public async Task<Pedidos> Create(PedidosDto dto)
        {
            var pedido = new Pedidos
            {
                IdCliente = dto.IdCliente!.Value,
                IdMesa = dto.IdMesa!.Value,
                FechaPedido = dto.FechaPedido ?? DateTime.Now,
                Total = dto.Total ?? 0,
                Estado = Enum.TryParse(dto.Estado, out EstadoPedido ep) ? ep : EstadoPedido.Recibido
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> Update(int id, PedidosDto dto)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.IdPedido == id);
            if (pedido == null) return false;

            pedido.IdCliente = dto.IdCliente ?? pedido.IdCliente;
            pedido.IdMesa = dto.IdMesa ?? pedido.IdMesa;
            pedido.FechaPedido = dto.FechaPedido ?? pedido.FechaPedido;
            pedido.Total = dto.Total ?? pedido.Total;

            if (!string.IsNullOrWhiteSpace(dto.Estado) && Enum.TryParse(dto.Estado, out EstadoPedido nuevoEstado))
            {
                if (nuevoEstado == EstadoPedido.Cancelado)
                {
                    var detalles = await _context.DetallesPedido.Where(d => d.IdPedido == id).ToListAsync();
                    _context.DetallesPedido.RemoveRange(detalles);

                    var pagos = await _context.Pagos.Where(p => p.IdPedido == id).ToListAsync();
                    _context.Pagos.RemoveRange(pagos);
                }
                pedido.Estado = nuevoEstado;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}