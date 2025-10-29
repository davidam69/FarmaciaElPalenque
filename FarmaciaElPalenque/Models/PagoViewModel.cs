namespace FarmaciaElPalenque.Models
{
    public class PagoViewModel : IValidatableObject
    {
        // Resumen del carrito
        public List<Carrito> Items { get; set; } = new();
        public decimal Total { get; set; }

        // Datos del pago
        [Required(ErrorMessage = "El nombre del titular es obligatorio")]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre del titular solo puede contener letras y espacios")]
        public string NombreTitular { get; set; } = "";

        [Required(ErrorMessage = "El número de tarjeta es obligatorio")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El número de tarjeta debe tener 16 dígitos")]
        public string? NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "El mes de vencimiento es obligatorio")]
        [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "El mes de vencimiento debe estar entre 01 y 12")]
        public string? MesVenc { get; set; }

        [Required(ErrorMessage = "El año de vencimiento es obligatorio")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El año de vencimiento debe tener 4 dígitos")]
        public string? AnioVenc { get; set; }

        [Required(ErrorMessage = "El CVV es obligatorio")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El CVV debe tener 3 dígitos")]
        public string? CVV { get; set; }

        public string? DireccionEnvio { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        public string? MetodoPago { get; set; }

        // --- VALIDACIÓN COMBINADA ---
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // === Validar vencimiento ===
            if (int.TryParse(MesVenc, out var mm) && int.TryParse(AnioVenc, out var yyyy))
            {
                // chequeos rápidos
                if (mm < 1 || mm > 12 || yyyy < DateTime.Now.Year - 1 || yyyy > DateTime.Now.Year + 30)
                {
                    yield return new ValidationResult("Fecha de vencimiento inválida.", new[] { nameof(MesVenc), nameof(AnioVenc) });
                }
                else
                {
                    // último día del mes a las 23:59
                    var lastDay = DateTime.DaysInMonth(yyyy, mm);
                    var expDate = new DateTime(yyyy, mm, lastDay, 23, 59, 59);

                    if (expDate < DateTime.Now)
                    {
                        yield return new ValidationResult("La tarjeta está vencida.", new[] { nameof(MesVenc), nameof(AnioVenc) });
                    }
                }
            }

            // === Validación Luhn (tarjeta realista) ===
            if (!string.IsNullOrWhiteSpace(NumeroTarjeta) && !LuhnOk(NumeroTarjeta))
            {
                yield return new ValidationResult("Número de tarjeta inválido.", new[] { nameof(NumeroTarjeta) });
            }
        }

        // algoritmo Luhn
        private static bool LuhnOk(string number)
        {
            var s = new string(number.Where(char.IsDigit).ToArray());
            int sum = 0;
            bool alt = false;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                int n = s[i] - '0';
                if (alt)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alt = !alt;
            }

            return sum % 10 == 0;
        }
    }
}
