﻿namespace WebApplication1.Models.DTOs
{
    public class DetalleVentasDto
    {
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
    }
}