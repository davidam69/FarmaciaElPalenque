using Microsoft.AspNetCore.Mvc;
using FarmaciaElPalenque.Models;

namespace FarmaciaElPalenque.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Index()
        {
            var categoria1 = new Categoria{ id = 1, nombre = "Medicamentos" };
            var categoria2 = new Categoria { id = 2, nombre = "Perfumeria" };

            var productos = new List<Productos>
            {
                new Productos { id = 1, nombre = "Aspirina", precio = "10.00", categoriaId = categoria1 },
                new Productos { id = 2, nombre = "Ibuprofeno", precio = "15.00", categoriaId = categoria1 },
                new Productos { id = 3, nombre = "Champú", precio = "20.00", categoriaId = categoria2 },
                new Productos { id = 4, nombre = "Jabón", precio = "5.00", categoriaId = categoria2 }
            };

            return View(productos);
        }

        public IActionResult Detalle(int id)
        {
            var categoria1 = new Categoria { id = 1, nombre = "Medicamentos" };
            var categoria2 = new Categoria { id = 2, nombre = "Perfumeria" };

            var productos = new List<Productos>
            {
                new Productos { id = 1, nombre = "Aspirina", precio = "10.00", categoriaId = categoria1 },
                new Productos { id = 2, nombre = "Ibuprofeno", precio = "15.00", categoriaId = categoria1 },
                new Productos { id = 3, nombre = "Champú", precio = "20.00", categoriaId = categoria2 },
                new Productos { id = 4, nombre = "Jabón", precio = "5.00", categoriaId = categoria2 }
            };

            var productoSeleccionado = productos.FirstOrDefault(p => p.id == id);

            if (productoSeleccionado == null)
            {
                return NotFound("Producto no encontrado");
            }
            return View(productoSeleccionado);
        }
    }
}
