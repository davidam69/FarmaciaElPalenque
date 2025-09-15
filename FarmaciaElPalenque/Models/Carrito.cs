namespace FarmaciaElPalenque.Models
{
    public class Carrito
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }   // mejor decimal para dinero
        public int Cantidad { get; set; }
        public string? ImagenUrl { get; set; }

        public decimal Subtotal => Precio * Cantidad;
    }
}
