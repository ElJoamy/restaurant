using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("Productos")]
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        public int? IdCategoria { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        [MaxLength(20)]
        public string? Categoria { get; set; }

        public int Stock { get; set; } = 0;

        [MaxLength(20)]
        public EstadoProducto Estado { get; set; } = EstadoProducto.Disponible;
    }
}
