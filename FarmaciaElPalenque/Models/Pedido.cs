namespace FarmaciaElPalenque.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public string numero { get; set; } = "";
        public DateTime fecha { get; set; } = DateTime.Now;
        public int usuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public decimal total { get; set; }
        public List<PedidoDetalle> Detalles { get; set; } = new();
    }

    public class PedidoDetalle
    {
        public int id { get; set; }
        public int pedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        public int productoId { get; set; }
        public string nombre { get; set; } = "";
        public decimal precioUnitario { get; set; }
        public int cantidad { get; set; }
    }
}
