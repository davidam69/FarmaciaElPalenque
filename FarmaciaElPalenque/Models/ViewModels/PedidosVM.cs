namespace FarmaciaElPalenque.Models.ViewModels
{
    public class PedidoResumenVM
    {
        public string Numero { get; set; } = "";
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int CantItems { get; set; }
    }

    public class PedidoDetalleVM
    {
        public string Numero { get; set; } = "";
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<Item> Items { get; set; } = new();

        public class Item
        {
            public string Nombre { get; set; } = "";
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal => PrecioUnitario * Cantidad;
        }
    }
}

