using System.ComponentModel.DataAnnotations;

namespace FarmaciaElPalenque.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        [DataType(DataType.Currency)] // Formato de moneda
        public int precio { get; set; }
        public int categoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public string? imagenUrl { get; set; }
        //public string? marca { get; set; }
        public int Stock { get; set; }
    }
}
