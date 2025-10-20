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
            .HasForeignKey(p => p.categoriaId)
            .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

            modelBuilder.Entity<Productos>()
                .Property(p => p.precio)
                .HasPrecision(18,2); // Define precisión y escala para decimal

            modelBuilder.Entity<Productos>()
                .Property(p => p.Stock);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Detalles)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.pedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.total).HasPrecision(18,2);

            modelBuilder.Entity<PedidoDetalle>()
                .Property(d => d.precioUnitario).HasPrecision(18,2); 

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.email)
                .IsUnique(); // Asegura que el email sea único
        }
    }
}


