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
        public IActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            if (string.IsNullOrEmpty(usuario.rol))
            {
                usuario.rol = "Cliente";
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

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
        public IActionResult Login(string email, string passwordHash)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == email && u.passwordHash == passwordHash);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Email o contraseña incorrectos.");
                return View();
            }

            HttpContext.Session.SetString("Usuario", usuario.email ?? "");
            HttpContext.Session.SetString("Rol", usuario.rol ?? "");
            HttpContext.Session.SetString("NombreCompleto", $"{usuario.nombre ?? ""} {usuario.apellido ?? ""}");

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
