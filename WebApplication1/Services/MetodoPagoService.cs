using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Enums;

namespace WebApplication1.Services
{
    public class MetodoPagoService
    {
        private readonly AppDbContext _context;

        public MetodoPagoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MetodoPago>> GetAll()
        {
            return await _context.MetodosPago.ToListAsync();
        }

        public async Task<MetodoPago?> GetById(int id)
        {
            return await _context.MetodosPago.FindAsync(id);
        }

        public async Task<MetodoPago> Create(MetodoPagoDto dto)
        {
            // Validación del enum
            if (!Enum.TryParse(dto.Metodo, out MetodoPagoEnum metodoEnum))
                throw new ArgumentException("Método de pago inválido");

            var metodo = new MetodoPago
            {
                Metodo = metodoEnum.ToString()
            };

            _context.MetodosPago.Add(metodo);
            await _context.SaveChangesAsync();
            return metodo;
        }
    }
}
