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

            usuario.passwordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        private static bool IsBcrypt(string? h) =>
        !string.IsNullOrWhiteSpace(h) &&
        (h.StartsWith("$2a$") || h.StartsWith("$2b$") || h.StartsWith("$2y$"));


        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                // Si ya hay sesión activa, redirige según el rol
                var rol = HttpContext.Session.GetString("Rol");
                if (rol == "Administrador")
                {
                    return RedirectToAction("Panel", "Admin");
                }

                return RedirectToAction("Index", "Principal");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var mail = email?.Trim().ToLowerInvariant();
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == email);
            if (usuario == null || !IsBcrypt(usuario.passwordHash) || !BCrypt.Net.BCrypt.Verify(password, usuario.passwordHash))
            {
                ModelState.AddModelError("", "Email o contraseña incorrectos.");
                return View();
            }

            if (HttpContext.Session != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.email ?? "");
                HttpContext.Session.SetString("Rol", usuario.rol ?? "");
                HttpContext.Session.SetString("NombreCompleto", $"{usuario.nombre ?? ""} {usuario.apellido ?? ""}");
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