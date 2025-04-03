using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<Cliente> Create(ClienteDto dto)
        {
            var nuevo = new Cliente
            {
                Nombre = dto.Nombre!,
                ApellidoPaterno = dto.ApellidoPaterno!,
                ApellidoMaterno = dto.ApellidoMaterno!,
                CI = dto.CI,
                NIT = dto.NIT,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            _context.Clientes.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }

        public async Task<bool> Update(int id, ClienteDto dto)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if (cliente == null) return false;

            cliente.Nombre = dto.Nombre ?? cliente.Nombre;
            cliente.ApellidoPaterno = dto.ApellidoPaterno ?? cliente.ApellidoPaterno;
            cliente.ApellidoMaterno = dto.ApellidoMaterno ?? cliente.ApellidoMaterno;
            cliente.CI = dto.CI ?? cliente.CI;
            cliente.NIT = dto.NIT ?? cliente.NIT;
            cliente.Telefono = dto.Telefono ?? cliente.Telefono;
            cliente.Correo = dto.Correo ?? cliente.Correo;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
