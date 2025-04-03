using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(20)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [MaxLength(20)]
        public string ApellidoMaterno { get; set; }

        [MaxLength(20)]
        public string? CI { get; set; }

        [MaxLength(20)]
        public string? NIT { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        [MaxLength(50)]
        public string? Correo { get; set; }
    }
}
