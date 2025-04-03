using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersonal { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        [Required]
        public int IdCargo { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }

        [MaxLength(20)]
        public string? ApellidoPaterno { get; set; }

        [Required]
        [MaxLength(20)]
        public string ApellidoMaterno { get; set; }

        [MaxLength(20)]
        public string? CI { get; set; }

        [Required]
        [MaxLength(50)]
        public string Correo { get; set; }

        public int? Telefono { get; set; }

        [Required]
        [MaxLength(1)]
        public string Genero { get; set; }

        [Required]
        [MaxLength(20)]
        public string Usuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Contrasena { get; set; }

        public int? Salario { get; set; }

        public bool Estado { get; set; } = true;
    }
}
