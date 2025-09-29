namespace FarmaciaElPalenque.Models
{
    public class PagoViewModel
    {
        // Resumen
        public List<Carrito> Items { get; set; } = new();
        public decimal Total { get; set; }

        // Datos tarjeta (ficticia)
        [Required(ErrorMessage = "El nombre del titular es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre del titular solo puede contener letras y espacios")]
        public string? NombreTitular { get; set; }
        [Required(ErrorMessage = "Ingrese un número de tarjeta válido")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El número de tarjeta debe tener 16 dígitos")]
        public string? NumeroTarjeta { get; set; }   // ej: 4111 1111 1111 1111
        [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "El mes de vencimiento debe estar entre 01 y 12")]
        public string? MesVenc { get; set; }         // "01".."12"
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El año de vencimiento debe tener 4 dígitos")]
        public string? AnioVenc { get; set; }        // "2026"
        [Required(ErrorMessage = "El CVV es obligatorio")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El CVV debe tener 3 dígitos")]
        public string? CVV { get; set; }             // "123"
        public string? DireccionEnvio { get; set; }
        public string? MetodoPago { get; set; }
    }
}

