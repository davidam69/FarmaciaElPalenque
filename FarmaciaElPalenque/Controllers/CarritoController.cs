using Microsoft.AspNetCore.Mvc;

namespace FarmaciaElPalenque.Controllers
{
    public class CarritoController : Controller
    {
        private readonly AppDbContext _context;

        public CarritoController(AppDbContext context) => _context = context;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(int id, int cantidad = 1, string? returnUrl = null)
        {
            // ¿Está logueado?
            var email = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Tenés que iniciar sesión para agregar productos al carrito.";
                // Vuelvo al Login y, cuando se loguee, lo regreso donde estaba
                var back = returnUrl ?? Request.Headers["Referer"].ToString();
                return RedirectToAction("Login", "Cuenta", new { returnUrl = back });
            }

            // Usuario logueado → agregar/sumar cantidad
            var usuarioId = _context.Usuarios
                .Where(u => u.email == email)
                .Select(u => u.id)
                .First();


            var p = _context.Productos.AsNoTracking().FirstOrDefault(x => x.id == id);
            if (p == null) return NotFound("Producto no encontrado");

            var cart = HttpContext.Session.ObtenerCarrito();
            var item = cart.FirstOrDefault(i => i.ProductoId == id);
            if (item == null)
            {
                cart.Add(new Carrito
                {
                    ProductoId = p.id,
                    Nombre = p.nombre,
                    Precio = p.precio,        // cambiá columna a decimal si puedes
                    Cantidad = cantidad,
                    ImagenUrl = p.imagenUrl
                });
            }
            else
            {
                item.Cantidad += cantidad;
            }
            HttpContext.Session.GuardarCarrito(cart);
            TempData["Mensaje"] = $"Agregado: {p.nombre}";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Ver");
        }

        [HttpGet]
        public IActionResult Ver()
        {
            var cart = HttpContext.Session.ObtenerCarrito();
            ViewBag.Total = cart.Sum(i => i.Subtotal);
            return View(cart);
        }

        [HttpPost]
        public IActionResult CambiarCantidad(int id, int cantidad)
        {
            var cart = HttpContext.Session.ObtenerCarrito();
            var item = cart.FirstOrDefault(i => i.ProductoId == id);
            if (item != null)
            {
                if (cantidad <= 0) cart.Remove(item);
                else item.Cantidad = cantidad;
                HttpContext.Session.GuardarCarrito(cart);
            }
            return RedirectToAction("Ver");
        }

        [HttpPost]
        public IActionResult Quitar(int id)
        {
            var cart = HttpContext.Session.ObtenerCarrito();
            cart.RemoveAll(i => i.ProductoId == id);
            HttpContext.Session.GuardarCarrito(cart);
            return RedirectToAction("Ver");
        }

        [HttpPost]
        public IActionResult Vaciar()
        {
            HttpContext.Session.BorrarCarrito();
            return RedirectToAction("Ver");
        }



    }
}
