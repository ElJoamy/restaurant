using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class TurnoPersonalDto
    {
        public int? IdPersonal { get; set; }

        public DateTime? Fecha { get; set; }

        public string? NombreTurno { get; set; }

        public TimeSpan? HoraEntrada { get; set; }

        public TimeSpan? HoraSalida { get; set; }

        public string? Observacion { get; set; }

        public bool? Estado { get; set; } = true;
    }
}
