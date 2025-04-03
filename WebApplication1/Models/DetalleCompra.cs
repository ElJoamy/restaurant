using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("DetalleCompra")]
    public class DetalleCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleCompra { get; set; }

        [Required]
        public int IdCompra { get; set; }

        [Required]
        public int IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

        // Campo calculado en la BD, no se asigna desde el código
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Subtotal { get; set; }

        public bool Estado { get; set; } = true;
    }
}
