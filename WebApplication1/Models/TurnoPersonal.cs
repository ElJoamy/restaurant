using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("TurnoPersonal")]
    public class TurnoPersonal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTurnoPersonal { get; set; }

        [Required]
        public int IdPersonal { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(20)]
        public string NombreTurno { get; set; }

        [Required]
        public TimeSpan HoraEntrada { get; set; }

        [Required]
        public TimeSpan HoraSalida { get; set; }

        [MaxLength(255)]
        public string? Observacion { get; set; }

        public bool Estado { get; set; } = true;
    }
}
