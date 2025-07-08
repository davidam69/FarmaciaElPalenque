using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmaciaElPalenque.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    precio = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false),
                    imagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "Medicamentos" },
                    { 2, "Perfumería" },
                    { 3, "Cuidado personal" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "apellido", "email", "nombre", "passwordHash", "rol" },
                values: new object[,]
                {
                    { 1, "General", "admin@palenque.com", "admin", "admin123", "Administrador" },
                    { 2, "Perez", "juan@correo.com", "Juan", "1234", "Cliente" },
                    { 3, "Garcia", "maria@correo.com", "Maria", "clave123", "Cliente" },
                    { 4, "Lopez", "carlos@correo.com", "Carlos", "qwerty", "Cliente" },
                    { 5, "Gonzalez", "laura@correo.com", "Laura", "pass1234", "Cliente" },
                    { 6, "Fernandez", "ana@correo.com", "Ana", "abc123", "Cliente" },
                    { 7, "Alvarez", "roberto@palenque.com", "Roberto", "adminadmin", "Administrador" },
                    { 8, "Martinez", "camila@correo.com", "Camila", "cami321", "Cliente" },
                    { 9, "Ruiz", "luciano@correo.com", "Luciano", "123456", "Cliente" },
                    { 10, "Mendez", "carolina@correo.com", "Carolina", "securepass", "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "Stock", "categoriaId", "imagenUrl", "nombre", "precio" },
                values: new object[,]
                {
                    { 1, 100, 1, "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg", "Bayaspirina", 5728 },
                    { 2, 100, 1, "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png", "Ibu400", 15000 },
                    { 3, 100, 2, "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg", "Shampoo Pantene", 20000 },
                    { 4, 100, 3, "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg", "Jabón Rexona", 5000 },
                    { 5, 100, 1, "https://www.farmaciassanchezantoniolli.com.ar/10123-medium_default/tafirol-x30-comp.jpg", "Paracetamol 500mg", 4300 },
                    { 6, 100, 3, "https://farmacityar.vtexassets.com/arquivos/ids/207795/220120_alcohol-en-gel-bialcohol-con-glicerina-x-250-ml_imagen-1.jpg?v=637497071230100000", "Alcohol en gel", 2900 },
                    { 7, 100, 3, "https://jumboargentina.vtexassets.com/arquivos/ids/768123/Cepillo-Dental-Oral-b-Complete-1-Un-1-223926.jpg?v=638114674058130000", "Cepillo Dental Oral-B", 2200 },
                    { 8, 100, 3, "https://www.masfarmacias.com/wp-content/uploads/7500435148443.jpg", "Toallitas Húmedas Pampers", 7800 },
                    { 9, 100, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR-oxxY1Y9TdkG8WTow2jN6IedoE1mp_ZCMBg&s", "Perfume Hugo Boss", 45200 },
                    { 10, 100, 2, "https://farmaciadelpuebloar.vtexassets.com/arquivos/ids/166590/desodorante-dove-men-care.png?v=638163070782970000", "Desodorante Dove", 6300 },
                    { 11, 100, 1, "https://images.precialo.com/products/te-en-saquitos-green-hills-blend-relax-x-20-saquitos/3d1dbd48-bcf7-4b67-82e3-e93ca551527d.jpeg", "Té de Hierbas Relax", 3600 },
                    { 12, 100, 2, "https://getthelookar.vtexassets.com/arquivos/ids/180043-800-auto?v=638484443678830000&width=800&height=auto&aspect=true", "Crema Nivea", 5400 },
                    { 13, 100, 3, "https://jumboargentina.vtexassets.com/arquivos/ids/178407-800-600?v=636383362696400000&width=800&height=600&aspect=true", "Algodón Estéril 100g", 2100 },
                    { 14, 100, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTusKNNE2NSzobZCKl7bKeECu4bX403oEezKg&s", "Jarabe para la Tos", 8700 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_categoriaId",
                table: "Productos",
                column: "categoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
