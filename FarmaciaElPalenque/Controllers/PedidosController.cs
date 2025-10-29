namespace FarmaciaElPalenque.Controllers
{
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context) => _context = context;

        private bool EsAdmin() => HttpContext.Session.GetString("Rol") == "Administrador";

        [HttpGet]
        public IActionResult MisCompras()
        {
            var email = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Acceso", "Cuenta", new { returnUrl = Url.Action("MisCompras", "Pedidos") });

            var usuarioId = _context.Usuarios
                .Where(u => u.email == email)
                .Select(u => u.id)
                .FirstOrDefault();

            if (usuarioId == 0) return RedirectToAction("Acceso", "Cuenta");

            var pedidos = _context.Pedidos
                .Where(p => p.usuarioId == usuarioId)
                .OrderByDescending(p => p.fecha)
                .Select(p => new PedidoResumenVM
                {
                    Numero = p.numero,
                    Fecha = p.fecha,
                    Total = p.total,
                    CantItems = p.Detalles.Count
                })
                .ToList();

            return View(pedidos);
        }

        [HttpGet]
        public IActionResult Detalle(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return RedirectToAction(nameof(MisCompras));

            var pedido = _context.Pedidos
                .Include(p => p.Detalles)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.numero == numero);

            if (pedido == null) return RedirectToAction(nameof(MisCompras));

            // Seguridad: solo dueño o admin
            var email = HttpContext.Session.GetString("Usuario") ?? "";
            if (!EsAdmin() && !string.Equals(email, pedido.Usuario?.email, StringComparison.OrdinalIgnoreCase))
                return RedirectToAction(nameof(MisCompras));

            var vm = new PedidoDetalleVM
            {
                Numero = pedido.numero,
                Fecha = pedido.fecha,
                Total = pedido.total,
                Items = pedido.Detalles.Select(d => new PedidoDetalleVM.Item
                {
                    Nombre = d.nombre,
                    Cantidad = d.cantidad,
                    PrecioUnitario = d.precioUnitario
                }).ToList()
            };

            return View(vm);
        }

        // Atajo para reusar tu generador de PDF
        [HttpGet]
        public IActionResult Descargar(string numero)
        {
            // Reutiliza tu acción existente de CheckoutController
            return RedirectToAction("DescargarFactura", "Checkout", new { numero });
        }
    }
}

