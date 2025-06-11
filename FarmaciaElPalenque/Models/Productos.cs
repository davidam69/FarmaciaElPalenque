namespace FarmaciaElPalenque.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? precio { get; set; }
        public Categoria? categoriaId { get; set; }
    }
}
