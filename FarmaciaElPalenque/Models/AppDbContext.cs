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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Categorías
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { id = 1, nombre = "Medicamentos" },
                new Categoria { id = 2, nombre = "Perfumería" },
                new Categoria { id = 3, nombre = "Cuidado personal" }
            );

            // Productos
            modelBuilder.Entity<Productos>().HasData(
                new Productos { id = 1, nombre = "Bayaspirina", precio = 5728, categoriaId = 1, imagenUrl = "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg", Stock = 100 },
                new Productos { id = 2, nombre = "Ibu400", precio = 15000, categoriaId = 1, imagenUrl = "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png", Stock = 100 },
                new Productos { id = 3, nombre = "Shampoo Pantene", precio = 20000, categoriaId = 2, imagenUrl = "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg", Stock = 100 },
                new Productos { id = 4, nombre = "Jabón Rexona", precio = 5000, categoriaId = 3, imagenUrl = "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg", Stock = 100 }
            );

            // Usuarios
            modelBuilder.Entity<Usuario>().HasData(
            new Usuario { id = 1, nombre = "admin", apellido= "General", passwordHash = "admin123", rol = "Administrador", email = "admin@palenque.com" },
            new Usuario { id = 2, nombre = "Juan", apellido="Perez", passwordHash = "1234", rol = "Cliente", email = "juan@correo.com" },
            new Usuario { id = 3, nombre = "Maria", apellido = "Garcia", passwordHash = "clave123", rol = "Cliente", email = "maria@correo.com" },
            new Usuario { id = 4, nombre = "Carlos", apellido = "Lopez", passwordHash = "qwerty", rol = "Cliente", email = "carlos@correo.com" },
            new Usuario { id = 5, nombre = "Laura", apellido="Gonzalez", passwordHash = "pass1234", rol = "Cliente", email = "laura@correo.com" },
            new Usuario { id = 6, nombre = "Ana", apellido = "Fernandez", passwordHash = "abc123", rol = "Cliente",  email = "ana@correo.com" },
            new Usuario { id = 7, nombre = "Roberto", apellido = "Alvarez", passwordHash = "adminadmin", rol = "Administrador", email = "roberto@palenque.com" },
            new Usuario { id = 8, nombre = "Camila", apellido = "Martinez", passwordHash = "cami321", rol = "Cliente", email = "camila@correo.com" },
            new Usuario { id = 9, nombre = "Luciano", apellido = "Ruiz", passwordHash = "123456", rol = "Cliente", email = "luciano@correo.com" },
            new Usuario { id = 10, nombre = "Carolina", apellido = "Mendez", passwordHash = "securepass", rol = "Cliente", email = "carolina@correo.com" }
            );



            modelBuilder.Entity<Productos>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.categoriaId);

            modelBuilder.Entity<Productos>()
                .Property(p => p.precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Productos>()
                .Property(p => p.Stock)
                .HasColumnType("int");
        }
    }
}

