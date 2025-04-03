using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("Mesas")]
    public class Mesas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMesa { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        [Required]
        public int NumeroMesa { get; set; }

        [Required]
        public int Capacidad { get; set; }

        [MaxLength(20)]
        public EstadoMesa EstadoMesa { get; set; } = EstadoMesa.Disponible;
    }
}
