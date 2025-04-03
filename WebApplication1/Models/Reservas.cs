using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("Reservas")]
    public class Reservas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReserva { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        [Required]
        public int IdMesa { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [MaxLength(20)]
        public EstadoReserva Estado { get; set; } = EstadoReserva.Pendiente;
    }
}
