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
        => RedirectToAction("Acceso");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var rolUsuarioActual = HttpContext.Session.GetString("Rol");

            // Validación para administradores creando otros administradores
            if (rolUsuarioActual == "Administrador" && usuario.rol == "Administrador")
            {
                if (string.IsNullOrEmpty(usuario.email))
                {
                    ModelState.AddModelError("email", "El email es requerido para administradores");
                    return View(usuario);
                }

                // Forzar dominio @palenque.com
                var nombreUsuario = usuario.email.Contains('@')
                    ? usuario.email.Split('@')[0].Trim()
                    : usuario.email.Trim();

                usuario.email = $"{nombreUsuario}@palenque.com".ToLowerInvariant();

                // Validar que no exista otro administrador con el mismo email
                var adminExistente = _context.Usuarios
                    .Any(u => u.email == usuario.email && u.rol == "Administrador");

                if (adminExistente)
                {
                    ModelState.AddModelError("email", "Ya existe un administrador con este email");
                    return View(usuario);
                }
            }
            else
            {
                usuario.email = usuario.email?.Trim().ToLowerInvariant()!;
            }

            if (string.IsNullOrEmpty(usuario.rol))
            {
                usuario.rol = "Cliente";
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            TempData["Mensaje"] = "Usuario creado exitosamente";
            return RedirectToAction("Acceso");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        => RedirectToAction("Acceso", new { returnUrl });

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
                var vm = new Usuario { email = mail };
                return View("Acceso", vm);
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

        [HttpGet]
        public IActionResult Acceso(string? returnUrl = null)
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (!string.IsNullOrEmpty(rol))
            {
                if (rol == "Administrador")
                {
                    // El admin SÍ puede ver la pantalla doble para registrar usuarios
                    ViewBag.ReturnUrl = returnUrl;
                    return View();
                }

                // Cualquier otro usuario ya logueado -> al inicio
                return RedirectToAction("Index", "Principal");
            }

            // No logueado -> mostrar pantalla doble
            ViewBag.ReturnUrl = returnUrl;
            return View(new Usuario());
        }


    }
}
