using Microsoft.AspNetCore.Mvc;
using FarmaciaElPalenque.Models;
using System.Globalization;
using static System.Net.WebRequestMethods;

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
                new Productos { id = 1, nombre = "Bayaspirina", precio = 5728, categoriaId = categoria1, imagenUrl = "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg"},
                new Productos { id = 2, nombre = "Ibu400", precio = 15000, categoriaId = categoria1, imagenUrl = "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png"},
                new Productos { id = 3, nombre = "Shampoo Pantene", precio = 20000, categoriaId = categoria2, imagenUrl = "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg"},
                new Productos { id = 4, nombre = "Jabón Rexona", precio = 5000, categoriaId = categoria2, imagenUrl = "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg"}
            };

            return View(productos);
        }

        public IActionResult Detalle(int id)
        {
            var categoria1 = new Categoria { id = 1, nombre = "Medicamentos" };
            var categoria2 = new Categoria { id = 2, nombre = "Perfumeria" };

            var productos = new List<Productos>
            {
                new Productos { id = 1, nombre = "Bayaspirina", precio = 10000, categoriaId = categoria1, imagenUrl = "https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg" },
                new Productos { id = 2, nombre = "Ibu400", precio = 15000, categoriaId = categoria1, imagenUrl = "https://www.centraloeste.com.ar/media/catalog/product/cache/9c821fce06d7004f361a4c419f8b1787/7/7/7790839980453.png" },
                new Productos { id = 3, nombre = "Shampoo Pantene", precio = 20000, categoriaId = categoria2, imagenUrl = "https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg" },
                new Productos { id = 4, nombre = "Jabón Rexona", precio = 5000, categoriaId = categoria2, imagenUrl = "https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg" }
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
