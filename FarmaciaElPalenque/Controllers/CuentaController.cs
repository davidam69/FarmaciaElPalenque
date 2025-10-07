namespace FarmaciaElPalenque.Controllers
{
    public class CuentaController : Controller
    {
        private readonly AppDbContext _context;

        public CuentaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }
            usuario.email = usuario.email?.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(usuario.rol))
            {
                usuario.rol = "Cliente";
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                // Si ya hay sesión activa, redirige según el rol
                var rol = HttpContext.Session.GetString("Rol");
                if (rol == "Administrador") return RedirectToAction("Panel", "Admin");
                return RedirectToAction("Index", "Principal");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string passwordHash, string? returnUrl = null)
        {
            var mail = email?.Trim().ToLowerInvariant();
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == mail);
            if (usuario == null || string.IsNullOrEmpty(usuario.passwordHash) || !string.Equals(usuario.passwordHash, passwordHash, StringComparison.Ordinal))
            {
                ModelState.AddModelError("", "Email o contraseña incorrectos.");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            HttpContext.Session.SetString("Usuario", usuario.email ?? "");
            HttpContext.Session.SetString("Rol", usuario.rol ?? "");
            HttpContext.Session.SetString("NombreCompleto", $"{usuario.nombre ?? ""} {usuario.apellido ?? ""}");

            TempData.Remove("Error");

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            if (string.Equals(usuario.rol, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Mensaje"] = $"Bienvenido {usuario.nombre} {usuario.apellido}";
                return RedirectToAction("Panel", "Admin");
            }

            TempData["Mensaje"] = $"Bienvenido {usuario.nombre} {usuario.apellido}";
            return RedirectToAction("Index", "Principal");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Has cerrado sesión correctamente.";
            return RedirectToAction("Index", "Principal");
        }
    }
}
