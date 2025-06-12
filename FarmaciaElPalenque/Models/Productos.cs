namespace FarmaciaElPalenque.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public decimal precio { get; set; }
        // public int? stock { get; set; }
        public Categoria? categoriaId { get; set; }
        public string? imagenUrl { get; set; }
        //public string? marca { get; set; }
    }
}
