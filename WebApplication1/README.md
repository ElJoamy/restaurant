# Backend

## **FLUJO COMPLETO DE UNA VENTA**

### 1. **Cliente**
- Se busca el cliente en la tabla `Cliente`.
- Si **no existe**, se **registra uno nuevo**.
  - Campos mínimos: `Nombre`, `ApellidoPaterno`, `ApellidoMaterno`, `CI` o `NIT`.

### 2. **Crear Venta**
- Se registra una nueva venta en `Ventas` con:
  - `IdCliente`
  - `IdSucursal`
  - `FechaVenta` (auto)
  - `Estado` = `'Pendiente'`
  - `Total` = `0` (se calcula más adelante)

### 3. **Agregar Detalles de Venta**
- Por cada producto vendido:
  - Se valida que el producto exista y que **haya suficiente stock**.
  - Se crea un registro en `DetalleVentas` con:
    - `IdVenta`, `IdProducto`, `Cantidad`, `PrecioUnitario`
  - Se **descuenta el stock** de `Productos`.
  - Se puede registrar un movimiento en `Inventario` para trazabilidad (opcional, aún no implementado).

### 4. **Calcular Total**
- Se suma el `Subtotal` de cada `DetalleVenta`.
- Se actualiza el `Total` en la tabla `Ventas`.
- Se cambia el `Estado` a `'Pagado'`.

### 5. **Registrar Factura**
- Se inserta un registro en `Facturas` con:
  - `IdVenta`
  - `IdMetodoPago` (Efectivo, Tarjeta, QR)
  - `NIT` del cliente
  - `MontoTotal`
  - `FechaFactura`

## **FLUJO COMPLETO DE UNA ORDEN (Desde una mesa reservada hasta el pago final)**

### 1. **Cliente elige una mesa disponible**
- Se consulta una mesa disponible (`EstadoMesa = Disponible`).
- Se selecciona y se **reserva**:
  ```csharp
  mesa.EstadoMesa = EstadoMesa.Reservada;
  ```

### 2. **Se crea el Pedido**
- Se inserta en la tabla `Pedidos`:
  - `IdCliente`
  - `IdMesa`
  - `FechaPedido` (fecha actual)
  - `Estado = Recibido`
  - `Total = 0` inicialmente (se actualiza luego)

### 3. **Agregar productos al pedido (`DetallePedido`)**
- Por cada producto seleccionado:
  - Se valida que el stock sea suficiente
  - Se agrega un nuevo registro en `DetallePedido` con:
    - `IdPedido`
    - `IdProducto`
    - `Cantidad`
    - `PrecioUnitario`
- Se calcula el subtotal por producto y se acumula en `totalPedido`.

### 4. **Registrar el Pago**
- Una vez terminado el pedido, se registra el pago:
  - `IdPedido`
  - `MetodoPago` (como Enum: Efectivo, Tarjeta, QR)
  - `Monto = totalPedido`
  - `FechaPago = ahora`

### 5. **Guardar todo en una sola transacción**
- Todo esto se guarda usando `DbContext.Database.BeginTransactionAsync()` para asegurar integridad.
- Si algo falla, se hace rollback.

## ¿Qué tablas se ven afectadas?
| Tabla | Acción |
|-------|--------|
| `Mesas` | Se cambia el estado a **Reservada** |
| `Pedidos` | Se inserta un nuevo pedido |
| `DetallePedido` | Se agregan los productos que el cliente pidió |
| `Pagos` | Se registra el pago realizado por el pedido |

## Ventajas de este flujo
- **Modular**: puedes reutilizar partes del flujo.
- **Seguro**: toda la lógica está protegida por transacciones.
- **Escalable**: se puede extender para enviar notificaciones, aplicar descuentos, o imprimir facturas fácilmente.

## Extra: Endpoints que lo acompañan

### `GET /api/v1/ventas/flujo/datos-iniciales`
> Devuelve:
- Clientes
- Productos disponibles
- Métodos de pago
- Sucursales

### `POST /api/v1/ventas/flujo`
> Recibe un `VentaCompletaDto` y ejecuta **todo el flujo de venta** anterior.

### `GET /api/v1/flujo-completo/datos-iniciales`
> Devuelve:
- Clientes
- Productos disponibles
- Mesas disponibles
- Métodos de pago
- Sucursales

### `POST /api/v1/flujo-completo`
> Recibe un `FlujoCompletoDto` y ejecuta **el flujo completo de orden con mesa reservada y pago**.
