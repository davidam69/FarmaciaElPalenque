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
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    { 1, 100, 1, "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg", "Bayaspirina", 5728m },
                    { 2, 100, 1, "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png", "Ibu400", 15000m },
                    { 3, 100, 2, "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg", "Shampoo Pantene", 20000m },
                    { 4, 100, 3, "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg", "Jabón Rexona", 5000m }
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
