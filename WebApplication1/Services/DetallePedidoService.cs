using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class DetallePedidoService
    {
        private readonly AppDbContext _context;

        public DetallePedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetallePedido>> GetAll()
        {
            return await _context.DetallesPedido.ToListAsync();
        }

        public async Task<DetallePedido?> GetById(int id)
        {
            return await _context.DetallesPedido.FirstOrDefaultAsync(d => d.IdDetalle == id);
        }

        public async Task<DetallePedido> Create(DetallePedidoDto dto)
        {
            var detalle = new DetallePedido
            {
                IdPedido = dto.IdPedido!.Value,
                IdProducto = dto.IdProducto!.Value,
                Cantidad = dto.Cantidad!.Value,
                PrecioUnitario = dto.PrecioUnitario!.Value
            };

            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<bool> Update(int id, DetallePedidoDto dto)
        {
            var detalle = await _context.DetallesPedido.FirstOrDefaultAsync(d => d.IdDetalle == id);
            if (detalle == null) return false;

            detalle.IdPedido = dto.IdPedido ?? detalle.IdPedido;
            detalle.IdProducto = dto.IdProducto ?? detalle.IdProducto;
            detalle.Cantidad = dto.Cantidad ?? detalle.Cantidad;
            detalle.PrecioUnitario = dto.PrecioUnitario ?? detalle.PrecioUnitario;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}