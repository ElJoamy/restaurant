﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("Pedidos")]
    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdMesa { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public decimal Total { get; set; } = 0;

        [MaxLength(20)]
        public EstadoPedido Estado { get; set; } = EstadoPedido.Recibido;
    }
}
