
namespace FarmaciaElPalenque.Services
{
    public class FacturaPdfService
    {
        public static byte[] Generar(Factura f)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("FARMACIA EL PALENQUE").SemiBold().FontSize(16);
                            col.Item().Text("CUIT: 20-12345678-9");
                            col.Item().Text("Av. Siempreviva 742");
                        });
                        row.ConstantItem(180).Column(col =>
                        {
                            col.Item().Text($"Factura: {f.Numero}").SemiBold();
                            col.Item().Text($"Fecha: {f.Fecha:dd/MM/yyyy HH:mm}");
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(8);

                        col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
                        {
                            c.Spacing(5);
                            c.Item().Text("Datos del cliente").SemiBold();
                            c.Item().Text($"{f.ClienteNombre}");
                            c.Item().Text($"{f.ClienteEmail}");
                        });

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(6);   // Producto
                                columns.RelativeColumn(2);   // Cant
                                columns.RelativeColumn(2);   // Precio
                                columns.RelativeColumn(2);   // Subtotal
                            });

                            // Encabezados
                            table.Header(h =>
                            {
                                h.Cell().Element(CellHeader).Text("Producto");
                                h.Cell().Element(CellHeader).AlignRight().Text("Cant.");
                                h.Cell().Element(CellHeader).AlignRight().Text("Precio");
                                h.Cell().Element(CellHeader).AlignRight().Text("Subtotal");

                                static IContainer CellHeader(IContainer c) =>
                                    c.DefaultTextStyle(x => x.SemiBold()).Background(Colors.Grey.Lighten2).Padding(6);
                            });

                            foreach (var it in f.Items)
                            {
                                table.Cell().Padding(6).Text(it.Nombre);
                                table.Cell().Padding(6).AlignRight().Text(it.Cantidad.ToString());
                                table.Cell().Padding(6).AlignRight().Text($"$ {it.PrecioUnitario:N2}");
                                table.Cell().Padding(6).AlignRight().Text($"$ {it.Subtotal:N2}");
                            }

                            table.Footer(ftr =>
                            {
                                ftr.Cell().ColumnSpan(3).PaddingTop(5).AlignRight().Text("TOTAL").SemiBold();
                                ftr.Cell().PaddingTop(5).AlignRight().Text($"$ {f.Total:N2}").SemiBold();
                            });
                        });

                        col.Item().PaddingTop(10).Text("¡Gracias por su compra!").Italic();
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado automáticamente - ");
                        txt.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}");
                    });
                });
            });

            using var ms = new MemoryStream();
            doc.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}
