namespace WebApplication1.Models.DTOs
{
    public class PersonalDto
    {
        public int? IdSucursal { get; set; }
        public int? IdCargo { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? CI { get; set; }
        public string? Correo { get; set; }
        public int? Telefono { get; set; }
        public string? Genero { get; set; }
        public int? Salario { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
