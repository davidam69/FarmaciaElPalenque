using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmaciaElPalenque.Migrations
{
    /// <inheritdoc />
    public partial class ProductosExtraSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "Stock", "categoriaId", "imagenUrl", "nombre", "precio" },
                values: new object[,]
                {
                    { 5, 100, 1, "https://example.com/img/paracetamol.jpg", "Paracetamol 500mg", 4300.00m },
                    { 6, 100, 3, "https://example.com/img/alcoholgel.jpg", "Alcohol en gel", 2900.00m },
                    { 7, 100, 3, "https://example.com/img/cepillo.jpg", "Cepillo Dental Oral-B", 2200.00m },
                    { 8, 100, 3, "https://example.com/img/toallitas.jpg", "Toallitas Húmedas Pampers", 7800.00m },
                    { 9, 100, 2, "https://example.com/img/hugoboss.jpg", "Perfume Hugo Boss", 45200.00m },
                    { 10, 100, 2, "https://example.com/img/dove.jpg", "Desodorante Dove", 6300.00m },
                    { 11, 100, 1, "https://example.com/img/te_relax.jpg", "Té de Hierbas Relax", 3600.00m },
                    { 12, 100, 2, "https://example.com/img/nivea.jpg", "Crema Nivea", 5400.00m },
                    { 13, 100, 3, "https://example.com/img/algodon.jpg", "Algodón Estéril 100g", 2100.00m },
                    { 14, 100, 1, "https://example.com/img/jarabe.jpg", "Jarabe para la Tos", 8700.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 14);
        }
    }
}
