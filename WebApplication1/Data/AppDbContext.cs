using Microsoft.EntityFrameworkCore;
using WebApplication1.Enums;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<TurnoPersonal> TurnosPersonal { get; set; }
        public DbSet<CategoriaProductos> CategoriasProductos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<DetalleVentas> DetallesVentas { get; set; }
        public DbSet<Facturas> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mesas>()
                .Property(m => m.EstadoMesa)
                .HasConversion<string>();

            modelBuilder.Entity<Reservas>()
                .Property(r => r.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Ventas>()
                .Property(v => v.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Productos>()
                .Property(p => p.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Pedidos>()
                .Property(p => p.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<MetodoPago>()
                .Property(p => p.Metodo)
                .HasConversion<string>();
        }
    }
}
