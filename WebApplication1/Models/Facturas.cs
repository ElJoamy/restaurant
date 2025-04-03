using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Facturas")]
    public class Facturas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        [Required]
        public int IdVenta { get; set; }

        [Required]
        public int IdMetodoPago { get; set; }

        [Required]
        [MaxLength(20)]
        public string NIT { get; set; }

        public DateTime FechaFactura { get; set; } = DateTime.Now;

        [Required]
        public decimal MontoTotal { get; set; }
    }
}
