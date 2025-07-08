using System.ComponentModel.DataAnnotations;

namespace FarmaciaElPalenque.Models
{
    public class Categoria
    {
        public int id { get; set; }
        [Required]
        public string? nombre { get; set; }

        public ICollection<Productos>? Productos { get; set; }
    }
}
