using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Inventario")]
    public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInventario { get; set; }

        [Required]
        public int IdSucursal { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Estado { get; set; } = true;
    }
}
