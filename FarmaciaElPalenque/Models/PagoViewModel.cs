namespace FarmaciaElPalenque.Models
{
    public class PagoViewModel
    {
        // Resumen
        public List<Carrito> Items { get; set; } = new();
        public decimal Total { get; set; }

        // Datos tarjeta (ficticia)
        public string? NombreTitular { get; set; }
        public string? NumeroTarjeta { get; set; }   // ej: 4111 1111 1111 1111
        public string? MesVenc { get; set; }         // "01".."12"
        public string? AnioVenc { get; set; }        // "2026"
        public string? CVV { get; set; }             // "123"
        public string? DireccionEnvio { get; set; }
        public string? MetodoPago { get; set; }
    }
}

