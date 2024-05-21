using Microsoft.EntityFrameworkCore;

namespace CafeteriaHCC.Models
{
    public class CafeteriaContext : DbContext
    {
        public CafeteriaContext(DbContextOptions<CafeteriaContext> options)
            : base(options)
        {
        }

        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CatEstatusOrden> CatEstatusOrdenes { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<OrdenesDetalle> OrdenDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la relación entre HccProducto y HccAlmacen
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Almacen)
                .WithMany()
                .HasForeignKey(p => p.AlmacenId)
                .HasConstraintName("FK_alm_id");

            // Configurar la relación entre HccOrden y HccMesa
            modelBuilder.Entity<Ordenes>()
                .HasOne(o => o.Mesa)
                .WithMany()
                .HasForeignKey(o => o.MesaId)
                .HasConstraintName("FK_mes_id");

            // Configurar la relación entre HccOrden y HccCatEstatusOrden
            modelBuilder.Entity<Ordenes>()
                .HasOne(o => o.CategoriaOrden)
                .WithMany()
                .HasForeignKey(o => o.CategoriaOrdenId)
                .HasConstraintName("FK_catord_id");

            // Configurar la relación entre HccOrdenDetalle y HccOrden
            modelBuilder.Entity<OrdenesDetalle>()
                .HasOne(od => od.Ordenes)
                .WithMany()
                .HasForeignKey(od => od.OrdenId)
                .HasConstraintName("FK_ord_id");

            // Configurar la relación entre HccOrdenDetalle y HccProducto
            modelBuilder.Entity<OrdenesDetalle>()
                .HasOne(od => od.Producto)
                .WithMany()
                .HasForeignKey(od => od.ProductoId)
                .HasConstraintName("FK_prod_id");

            base.OnModelCreating(modelBuilder);
        }
    }
}