using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Compras")]
    public class Compras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        [Required]
        public int IdProveedor { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; }

        [Required]
        public decimal Total { get; set; }

        public bool Estado { get; set; } = true;
    }
}
