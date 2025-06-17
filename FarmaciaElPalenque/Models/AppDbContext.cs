using Microsoft.EntityFrameworkCore;

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
            new Usuario { id = 1, nombreUsuario = "admin", passwordHash = "admin123", rol = "Administrador", nombreCompleto = "Admin General", email = "admin@palenque.com" },
            new Usuario { id = 2, nombreUsuario = "juanperez", passwordHash = "1234", rol = "Cliente", nombreCompleto = "Juan Pérez", email = "juan@correo.com" },
            new Usuario { id = 3, nombreUsuario = "mariagarcia", passwordHash = "clave123", rol = "Cliente", nombreCompleto = "María García", email = "maria@correo.com" },
            new Usuario { id = 4, nombreUsuario = "carloslopez", passwordHash = "qwerty", rol = "Cliente", nombreCompleto = "Carlos López", email = "carlos@correo.com" },
            new Usuario { id = 5, nombreUsuario = "lauragonzalez", passwordHash = "pass1234", rol = "Cliente", nombreCompleto = "Laura González", email = "laura@correo.com" },
            new Usuario { id = 6, nombreUsuario = "anafernandez", passwordHash = "abc123", rol = "Cliente", nombreCompleto = "Ana Fernández", email = "ana@correo.com" },
            new Usuario { id = 7, nombreUsuario = "robertoalvarez", passwordHash = "adminadmin", rol = "Administrador", nombreCompleto = "Roberto Álvarez", email = "roberto@palenque.com" },
            new Usuario { id = 8, nombreUsuario = "camilamartinez", passwordHash = "cami321", rol = "Cliente", nombreCompleto = "Camila Martínez", email = "camila@correo.com" },
            new Usuario { id = 9, nombreUsuario = "lucianoruiz", passwordHash = "123456", rol = "Cliente", nombreCompleto = "Luciano Ruiz", email = "luciano@correo.com" },
            new Usuario { id = 10, nombreUsuario = "carolinamendez", passwordHash = "securepass", rol = "Cliente", nombreCompleto = "Carolina Méndez", email = "carolina@correo.com" }
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

