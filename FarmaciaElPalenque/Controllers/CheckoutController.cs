using Microsoft.AspNetCore.Mvc;

namespace FarmaciaElPalenque.Controllers
{
    public class CheckoutController : Controller
    {
        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.ObtenerCarrito() ?? new();
            if (!cart.Any()) return RedirectToAction("Ver", "Carrito");

            // Revalidar stock y ajustar si hace falta
            var ids = cart.Select(c => c.ProductoId).ToList();
            var productos = _context.Productos
                .Where(p => ids.Contains(p.id))
                .Select(p => new { p.id, p.nombre, p.Stock })
                .ToList();

            var problemas = new List<string>();
            foreach (var i in cart)
            {
                var prod = productos.FirstOrDefault(p => p.id == i.ProductoId);
                if (prod == null) { problemas.Add($"Producto eliminado: {i.Nombre}"); continue; }
                if (i.Cantidad > prod.Stock)
                {
                    i.Cantidad = Math.Max(0, prod.Stock);
                    problemas.Add($"{prod.nombre}: stock {prod.Stock}. Cantidad ajustada.");
                }
            }
            HttpContext.Session.GuardarCarrito(cart);
            if (problemas.Any())
            {
                TempData["Error"] = string.Join("<br/>", problemas);
                return RedirectToAction("Ver", "Carrito");
            }

            var vm = new PagoViewModel { Items = cart, Total = cart.Sum(x => x.Subtotal) };
            return View(vm);
        }

        public IActionResult Pagar(PagoViewModel model)
        {
            var cart = HttpContext.Session.ObtenerCarrito() ?? new();
            if (!cart.Any()) return RedirectToAction("Ver", "Carrito");

            var email = HttpContext.Session.GetString("Usuario") ?? "";
            var user = _context.Usuarios.FirstOrDefault(u => u.email == email);
            if (user == null) return RedirectToAction("Login", "Cuenta", new { returnUrl = Url.Action("Index", "Checkout") });

            using var tx = _context.Database.BeginTransaction();

            // Traer productos en tracking (¡sin AsNoTracking!)
            var ids = cart.Select(i => i.ProductoId).ToList();
            var productos = _context.Productos
                .Where(p => ids.Contains(p.id))
                .ToDictionary(p => p.id);

            var problemas = new List<string>();
            decimal total = 0m;

            foreach (var i in cart)
            {
                if (!productos.TryGetValue(i.ProductoId, out var prod))
                {
                    problemas.Add($"Producto eliminado: {i.Nombre}");
                    continue;
                }
                if (i.Cantidad > prod.Stock)
                    problemas.Add($"{prod.nombre}: stock {prod.Stock}, intentaste {i.Cantidad}.");
                else
                    total += prod.precio * i.Cantidad;
            }

            if (problemas.Any())
            {
                TempData["Error"] = string.Join("<br/>", problemas);
                tx.Rollback();
                return RedirectToAction("Ver", "Carrito");
            }

            // Descontar stock
            foreach (var i in cart)
                productos[i.ProductoId].Stock -= i.Cantidad;

            // Número simple (si no usás numerador)
            var numero = $"F-{DateTime.Now:yyyyMMdd-HHmmss}";

            var pedido = new Pedido
            {
                numero = numero,
                fecha = DateTime.Now,
                usuarioId = user.id,
                total = total,
                Detalles = cart.Select(i =>
                {
                    var prod = productos[i.ProductoId];
                    return new PedidoDetalle
                    {
                        productoId = prod.id,
                        nombre = prod.nombre ?? "Producto",
                        precioUnitario = prod.precio,
                        cantidad = i.Cantidad
                    };
                }).ToList()
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            tx.Commit();

            var factura = new Factura
            {
                Numero = pedido.numero,
                Fecha = pedido.fecha,
                ClienteNombre = HttpContext.Session.GetString("NombreCompleto") ?? user.nombre ?? "Cliente",
                ClienteEmail = user.email ?? "",
                DireccionEnvio = model.DireccionEnvio,
                MetodoPago = model.MetodoPago,
                Items = pedido.Detalles.Select(d => new FacturaItem
                {
                    Nombre = d.nombre,
                    Cantidad = d.cantidad,
                    PrecioUnitario = d.precioUnitario
                }).ToList()
            };

            var pdf = FacturaPdfService.Generar(factura);
            HttpContext.Session.BorrarCarrito();

            return File(pdf, "application/pdf", $"Factura_{pedido.numero}.pdf");
        }
    
        /*
        [HttpGet]
        public IActionResult Confirmacion()
        {
            ViewBag.Mensaje = TempData["Mensaje"] ?? "Pago procesado.";
            return View();
        }*/
    }
}
