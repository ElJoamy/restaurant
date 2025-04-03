using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Proveedores")]
    public class Proveedores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedor { get; set; }

        [MaxLength(20)]
        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        public bool Estado { get; set; } = true;
    }
}
