using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Sucursal")]
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSucursal { get; set; }

        [MaxLength(20)]
        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        public int? Capacidad { get; set; }

        public bool Estado { get; set; } = true;
    }
}
