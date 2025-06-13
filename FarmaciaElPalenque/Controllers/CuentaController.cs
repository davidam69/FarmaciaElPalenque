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
        public IActionResult Login(string nombreUsuario, string passwordHash)
        {
            // Verificar si el usuario existe
            var usuario = listaUsuarios.FirstOrDefault(u => u.nombreUsuario == nombreUsuario && u.passwordHash == passwordHash);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View();
            }

            HttpContext.Session.SetString("Usuario", usuario.nombreUsuario);
            HttpContext.Session.SetString("Rol", usuario.rol);

            TempData["Mensaje"] = $"Bienvenido {usuario.nombreCompleto}";
            return RedirectToAction("Index", "Principal");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Has cerrado sesión correctamente.";
            return RedirectToAction("Login");
        }
    }
}
