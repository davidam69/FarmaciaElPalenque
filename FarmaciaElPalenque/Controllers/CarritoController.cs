namespace FarmaciaElPalenque.Controllers
{
    public class CarritoController : Controller
    {
        private readonly AppDbContext _context;

        public CarritoController(AppDbContext context) => _context = context;

        private bool EsAdmin() => HttpContext.Session.GetString("Rol") == "Administrador";

        #region Agregar al carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(int id, int cantidad = 1, string? returnUrl = null)
        {
            if (EsAdmin())
            {
                TempData["Error"] = "Los administradores no pueden comprar.";
                return RedirectToAction("panel", "Admin");
            }

            // Requiere estar logueado para agregar al carrito
            var email = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Tenés que iniciar sesión para agregar productos al carrito.";
                // Vuelvo al Login y, cuando se loguee, lo regreso donde estaba
                var back = returnUrl ?? Request.Headers["Referer"].ToString();
                return RedirectToAction("Acceso", "Cuenta", new { returnUrl = back });
            }

            // no necesito el id del usuario por ahora solo para cuando necesite migrar el carrito a BD
            /* var usuarioId = _context.Usuarios
                .Where(u => u.email == email)
                .Select(u => u.id)
                .First(); */

            // buscar productos
            var p = _context.Productos.AsNoTracking().FirstOrDefault(x => x.id == id);
            if (p == null)
            {
                TempData["Error"] = "El producto no existe o fue eliminado.";
                return RedirectToAction("Index", "Principal");
            }
            if (p.Stock <= 0)
            {
                TempData["Error"] = $"No hay stock de {p.nombre}.";
                return Redirect(returnUrl ?? Url.Action("Ver")!);
            }

            // obtener carrito actual de sesión con su contenido existente
            var cart = HttpContext.Session.ObtenerCarrito() ?? new List<Carrito>();
            var item = cart.FirstOrDefault(i => i.ProductoId == id);

            var yaEnCarrito = item?.Cantidad ?? 0;
            var pedido = cantidad;

            // Validar stock antes de agregar o actualizar
            if (yaEnCarrito + pedido > p.Stock)
            {
                // Limitar el stock disponible
                var disponible = p.Stock - yaEnCarrito;
                if (disponible > 0)
                {
                    // Agregar solo la cantidad disponible
                    if (item == null)
                        cart.Add(new Carrito { ProductoId = p.id, Nombre = p.nombre!, Precio = p.precio, Cantidad = disponible, ImagenUrl = p.imagenUrl });
                    else
                        item.Cantidad = p.Stock; // tope

                    HttpContext.Session.GuardarCarrito(cart);
                    TempData["Error"] = $"Solo hay {p.Stock} unidades de {p.nombre}. Se ajustó al máximo disponible.";
                }
                else
                {
                    // No hay más stock para agregar
                    TempData["Error"] = $"Ya tenés {yaEnCarrito} en el carrito y el stock es {p.Stock}. No se agregaron más.";
                }

                return Redirect(returnUrl ?? Url.Action("Ver")!);
            }

            // agregar nuevo o actualizar cantidad
            if (item == null)
            {
                cart.Add(new Carrito
                {
                    ProductoId = p.id,
                    Nombre = p.nombre ?? "Producto sin nombre",
                    Precio = p.precio,        // cambiá columna a decimal si puedes
                    Cantidad = cantidad,
                    ImagenUrl = p.imagenUrl
                });
            }
            else
            {
                item.Cantidad += cantidad;
            }

            // guardar carrito actualizado en sesión
            HttpContext.Session.GuardarCarrito(cart);
            TempData["Mensaje"] = $"Agregado: {p.nombre ?? "(desconocido)"}";

            // redirigir a donde vino o al carrito
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect("Ver");

            return RedirectToAction("Ver");
        }
        #endregion

        #region Ver carrito
        [HttpGet]
        public IActionResult Ver()
        {
            if (EsAdmin())
            {
                TempData["Error"] = "Los administradores no pueden usar el carrito.";
                return RedirectToAction("Panel", "Admin");
            }

            var email = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Tenés que iniciar sesión para ver el carrito.";
                var back = Url.Action("Ver", "Carrito");
                return RedirectToAction("Acceso", "Cuenta", new { returnUrl = back });
            }

            var cart = HttpContext.Session.ObtenerCarrito() ?? new List<Carrito>();
            ViewBag.Total = cart.Sum(i => i.Subtotal);
            return View(cart);
        }

        #endregion

        #region Cambiar cantidad, Quitar, Vaciar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarCantidad(int id, int cantidad)
        {
            if (EsAdmin())
            {
                TempData["Error"] = "Los administradores no pueden comprar.";
                return RedirectToAction("Panel", "Admin");
            }
            // carrito solo para usuarios logueados
            var cart = HttpContext.Session.ObtenerCarrito() ?? new List<Carrito>();
            var item = cart.FirstOrDefault(i => i.ProductoId == id);

            // no está en el carrito
            if (item == null) return RedirectToAction("Ver");

            // verificar stock actual
            var p = _context.Productos.AsNoTracking().FirstOrDefault(x => x.id == id);
            if (p == null)
            {
                // producto ya no existe, lo saco del carrito
                cart.Remove(item);
                HttpContext.Session.GuardarCarrito(cart);
                TempData["Error"] = "El producto ya no está disponible y fue quitado del carrito.";
                return RedirectToAction("Ver");
            }

            // validar cantidad
            if (cantidad <= 0)
            {
                // saco del carrito
                cart.Remove(item);
                HttpContext.Session.GuardarCarrito(cart);
                return RedirectToAction("Ver");
            }

            // sin stock
            if (p.Stock <= 0)
            {
                cart.Remove(item);
                HttpContext.Session.GuardarCarrito(cart);
                TempData["Error"] = $"No hay stock de {p.nombre}. Se quitó del carrito.";
                return RedirectToAction("Ver");
            }

            // pedir más del stock
            if (cantidad > p.Stock)
            {
                // ajustar al máximo disponible
                item.Cantidad = p.Stock;
                HttpContext.Session.GuardarCarrito(cart);
                TempData["Error"] = $"Cantidad ajustada: solo hay {p.Stock} unidades de {p.nombre}.";
                return RedirectToAction("Ver");
            }

            // actualizar cantidad
            item.Cantidad = cantidad;
            HttpContext.Session.GuardarCarrito(cart);
            return RedirectToAction("Ver");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Quitar(int id)
        {
            if (EsAdmin())
            {
                TempData["Error"] = "Los administradores no pueden comprar.";
                return RedirectToAction("Panel", "Admin");
            }
            // Requiere estar logueado para modificar el carrito
            var cart = HttpContext.Session.ObtenerCarrito() ?? new List<Carrito>();
            cart.RemoveAll(i => i.ProductoId == id);
            HttpContext.Session.GuardarCarrito(cart);
            return RedirectToAction("Ver");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Vaciar()
        {
            if (EsAdmin())
            {
                TempData["Error"] = "Los administradores no pueden comprar.";
                return RedirectToAction("Panel", "Admin");
            }
            // Requiere estar logueado para modificar el carrito
            HttpContext.Session.BorrarCarrito();
            return RedirectToAction("Ver");
        }
        #endregion
    }
}
