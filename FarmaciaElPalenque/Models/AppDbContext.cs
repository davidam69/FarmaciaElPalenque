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
                new Productos { id = 4, nombre = "Jabón Rexona", precio = 5000, categoriaId = 3, imagenUrl = "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg", Stock = 100 },
                new Productos { id = 5, nombre = "Paracetamol 500mg", precio = 4300, categoriaId = 1, imagenUrl = "https://www.farmaciassanchezantoniolli.com.ar/10123-medium_default/tafirol-x30-comp.jpg", Stock = 100 },
                new Productos { id = 6, nombre = "Alcohol en gel", precio = 2900, categoriaId = 3, imagenUrl = "https://farmacityar.vtexassets.com/arquivos/ids/207795/220120_alcohol-en-gel-bialcohol-con-glicerina-x-250-ml_imagen-1.jpg?v=637497071230100000", Stock = 100 },
                new Productos { id = 7, nombre = "Cepillo Dental Oral-B", precio = 2200, categoriaId = 3, imagenUrl = "https://jumboargentina.vtexassets.com/arquivos/ids/768123/Cepillo-Dental-Oral-b-Complete-1-Un-1-223926.jpg?v=638114674058130000", Stock = 100 },
                new Productos { id = 8, nombre = "Toallitas Húmedas Pampers", precio = 7800, categoriaId = 3, imagenUrl = "https://www.masfarmacias.com/wp-content/uploads/7500435148443.jpg", Stock = 100 },
                new Productos { id = 9, nombre = "Perfume Hugo Boss", precio = 45200, categoriaId = 2, imagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR-oxxY1Y9TdkG8WTow2jN6IedoE1mp_ZCMBg&s", Stock = 100 },
                new Productos { id = 10, nombre = "Desodorante Dove", precio = 6300, categoriaId = 2, imagenUrl = "https://farmaciadelpuebloar.vtexassets.com/arquivos/ids/166590/desodorante-dove-men-care.png?v=638163070782970000", Stock = 100 },
                new Productos { id = 11, nombre = "Té de Hierbas Relax", precio = 3600, categoriaId = 1, imagenUrl = "https://images.precialo.com/products/te-en-saquitos-green-hills-blend-relax-x-20-saquitos/3d1dbd48-bcf7-4b67-82e3-e93ca551527d.jpeg", Stock = 100 },
                new Productos { id = 12, nombre = "Crema Nivea", precio = 5400, categoriaId = 2, imagenUrl = "https://getthelookar.vtexassets.com/arquivos/ids/180043-800-auto?v=638484443678830000&width=800&height=auto&aspect=true", Stock = 100 },
                new Productos { id = 13, nombre = "Algodón Estéril 100g", precio = 2100, categoriaId = 3, imagenUrl = "https://jumboargentina.vtexassets.com/arquivos/ids/178407-800-600?v=636383362696400000&width=800&height=600&aspect=true", Stock = 100 },
                new Productos { id = 14, nombre = "Jarabe para la Tos", precio = 8700, categoriaId = 1, imagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTusKNNE2NSzobZCKl7bKeECu4bX403oEezKg&s", Stock = 100 }
            );

            // Usuarios
            modelBuilder.Entity<Usuario>().HasData(
            new Usuario { id = 1, nombre = "admin", apellido = "General", passwordHash = "admin123", rol = "Administrador", email = "admin@palenque.com" },
            new Usuario { id = 2, nombre = "Juan", apellido = "Perez", passwordHash = "1234", rol = "Cliente", email = "juan@correo.com" },
            new Usuario { id = 3, nombre = "Maria", apellido = "Garcia", passwordHash = "clave123", rol = "Cliente", email = "maria@correo.com" },
            new Usuario { id = 4, nombre = "Carlos", apellido = "Lopez", passwordHash = "qwerty", rol = "Cliente", email = "carlos@correo.com" },
            new Usuario { id = 5, nombre = "Laura", apellido = "Gonzalez", passwordHash = "pass1234", rol = "Cliente", email = "laura@correo.com" },
            new Usuario { id = 6, nombre = "Ana", apellido = "Fernandez", passwordHash = "abc123", rol = "Cliente", email = "ana@correo.com" },
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
                .HasColumnType("int");

            modelBuilder.Entity<Productos>()
                .Property(p => p.Stock)
                .HasColumnType("int");
        }
    }
}

