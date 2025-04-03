using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Pago")]
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPago { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [Required]
        [MaxLength(20)]
        public string MetodoPago { get; set; }

        [Required]
        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; } = DateTime.Now;
    }
}
