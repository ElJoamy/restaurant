using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("MetodoPago")]
    public class MetodoPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMetodoPago { get; set; }

        [Required]
        [MaxLength(50)]
        public string Metodo { get; set; }
    }
}
