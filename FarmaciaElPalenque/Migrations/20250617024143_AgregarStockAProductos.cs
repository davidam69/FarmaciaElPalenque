using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmaciaElPalenque.Migrations
{
    /// <inheritdoc />
    public partial class AgregarStockAProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

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
                columns: new[] { "id", "email", "nombreCompleto", "nombreUsuario", "passwordHash", "rol" },
                values: new object[,]
                {
                    { 1, "admin@palenque.com", "Admin General", "admin", "admin123", "Administrador" },
                    { 2, "juan@correo.com", "Juan Pérez", "juanperez", "1234", "Cliente" },
                    { 3, "maria@correo.com", "María García", "mariagarcia", "clave123", "Cliente" },
                    { 4, "carlos@correo.com", "Carlos López", "carloslopez", "qwerty", "Cliente" },
                    { 5, "laura@correo.com", "Laura González", "lauragonzalez", "pass1234", "Cliente" },
                    { 6, "ana@correo.com", "Ana Fernández", "anafernandez", "abc123", "Cliente" },
                    { 7, "roberto@palenque.com", "Roberto Álvarez", "robertoalvarez", "adminadmin", "Administrador" },
                    { 8, "camila@correo.com", "Camila Martínez", "camilamartinez", "cami321", "Cliente" },
                    { 9, "luciano@correo.com", "Luciano Ruiz", "lucianoruiz", "123456", "Cliente" },
                    { 10, "carolina@correo.com", "Carolina Méndez", "carolinamendez", "securepass", "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "categoriaId", "imagenUrl", "nombre", "precio" },
                values: new object[,]
                {
                    { 1, 1, "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg", "Bayaspirina", 5728m },
                    { 2, 1, "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png", "Ibu400", 15000m },
                    { 3, 2, "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg", "Shampoo Pantene", 20000m },
                    { 4, 2, "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg", "Jabón Rexona", 5000m }
                });
        }
    }
}
