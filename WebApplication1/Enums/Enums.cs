namespace WebApplication1.Enums
{
    public enum EstadoVenta
    {
        Pendiente,
        Pagado,
        Cancelado
    }

    public enum EstadoMesa
    {
        Disponible,
        Reservada
    }

    public enum EstadoReserva
    {
        Confirmada,
        Cancelada,
        Pendiente
    }
    public enum EstadoProducto
    {
        Disponible,
        Agotado,
        Descontinuado
    }
    public enum EstadoPedido
    {
        Recibido,
        EnPreparacion,
        Listo,
        Entregado,
        Cancelado
    }
    public enum MetodoPagoEnum
    {
        Efectivo,
        Tarjeta,
        QR
    }

}
