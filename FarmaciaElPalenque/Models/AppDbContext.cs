namespace FarmaciaElPalenque.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.categoriaId);

            modelBuilder.Entity<Productos>()
                .Property(p => p.precio)
                .HasColumnType("int");

            modelBuilder.Entity<Productos>()
                .Property(p => p.Stock)
                .HasColumnType("int");

            modelBuilder.Entity<Pedido>(e =>
            {
                e.Property(p => p.total).HasColumnType("decimal(18,2)");
                e.HasMany(p => p.Detalles).WithOne(d => d.Pedido).HasForeignKey(d => d.pedidoId);
            });

            modelBuilder.Entity<PedidoDetalle>(e =>
            {
                e.Property(d => d.precioUnitario).HasColumnType("decimal(18,2)");
            });
        }
    }
}


