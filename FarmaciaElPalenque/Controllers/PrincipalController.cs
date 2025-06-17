using Microsoft.AspNetCore.Mvc;
using FarmaciaElPalenque.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FarmaciaElPalenque.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly AppDbContext _context;

        public PrincipalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos
                 .Include(p => p.Categoria) // Asegúrate de que la relación esté bien configurada
                 .ToList();

            return View(productos);
        }

        public IActionResult Detalle(int id)
        {

            var productoSeleccionado = _context.Productos
                .Include(p => p.Categoria) // Asegúrate de que la relación esté bien configurada
                .FirstOrDefault(p => p.id == id);

            if (productoSeleccionado == null)
            {
                return NotFound("Producto no encontrado");
            }
            return View(productoSeleccionado);
        }
    }
}
