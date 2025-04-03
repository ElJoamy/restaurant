using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string? Login(LoginRequest request)
        {
            var user = _context.Personal.FirstOrDefault(u =>
                u.Usuario == request.Usuario && u.Contrasena == request.Contrasena);

            if (user == null) return null;

            // Crear claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdPersonal.ToString()),
                new Claim(ClaimTypes.Name, user.Usuario),
                new Claim("Rol", user.IdCargo.ToString())
            };

            // Crear key y creds
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
