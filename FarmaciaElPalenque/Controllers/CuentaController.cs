using FarmaciaElPalenque.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmaciaElPalenque.Controllers
{
    public class CuentaController : Controller
    {
        public static List<Usuario> listaUsuarios = new List<Usuario>();

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

            usuario.id = listaUsuarios.Count + 1; // Asignar un ID único al usuario
            // Agregar el nuevo usuario a la lista
            listaUsuarios.Add(usuario);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
        {
            // Verificar si el usuario existe
            var usuario = listaUsuarios.FirstOrDefault(u => u.nombreUsuario == modelo.nombreUsuario && u.passwordHash == modelo.passwordHash);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View();
            }
            // Validar que usuario.nombreUsuario y usuario.rol no sean nulos antes de usarlos
            if (string.IsNullOrEmpty(usuario.nombreUsuario) || string.IsNullOrEmpty(usuario.rol) || string.IsNullOrEmpty(usuario.nombreCompleto))
            {
                ModelState.AddModelError("", "Error interno: datos del usuario incompletos.");
                return View();
            }
            HttpContext.Session.SetString("Usuario", usuario.nombreUsuario);
            HttpContext.Session.SetString("Rol", usuario.rol);
            HttpContext.Session.SetString("NombreCompleto", usuario.nombreCompleto ?? "");

            TempData["Mensaje"] = $"Bienvenido {usuario.nombreCompleto}";
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
