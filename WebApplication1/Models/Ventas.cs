using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("Ventas")]
    public class Ventas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVenta { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        public decimal Total { get; set; } = 0;

        [MaxLength(20)]
        public EstadoVenta Estado { get; set; } = EstadoVenta.Pendiente;
    }
}
