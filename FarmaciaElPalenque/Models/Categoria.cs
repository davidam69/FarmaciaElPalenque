using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
