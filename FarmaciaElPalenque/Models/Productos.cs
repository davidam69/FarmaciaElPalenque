namespace FarmaciaElPalenque.Models
{
    public class Productos
    {
        public int id { get; set; }
        [Required, StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string nombre { get; set; } = "";
        [Required(ErrorMessage = "El precio es obligatorio")]
        [DataType(DataType.Currency)] // Formato de moneda
        public decimal precio { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int categoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public string? imagenUrl { get; set; }
        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }
    }
}
