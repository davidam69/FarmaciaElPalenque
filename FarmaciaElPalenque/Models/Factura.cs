namespace FarmaciaElPalenque.Models
{
    public class FacturaItem
    {
        public string Nombre { get; set; } = "";
        public decimal PrecioUnitario { get; set; }   // mejor decimal para dinero
        public int Cantidad { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
    public class Factura
    {
        public string Numero { get; set; } = "";
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string? DireccionEnvio { get; set; }
        public string? MetodoPago { get; set; }


        public string ClienteNombre { get; set; } = "";
        public string ClienteEmail { get; set; } = "";

        public List<FacturaItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
