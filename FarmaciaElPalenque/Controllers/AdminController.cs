namespace FarmaciaElPalenque.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;
        // Asumiendo que AppDbContext está correctamente inyectado
        public AdminController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // =================================================================
        // NUEVA ACCIÓN: RESULTADOS DEL ML Y ESTADO DE ENVÍO DE EMAILS
        // =================================================================
        /*
        public async Task<IActionResult> ResultadosClustering()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso Denegado.";
                return RedirectToAction("Index", "Principal");
            }

            
            var connectionString = _context.Database.GetConnectionString();
            var mlModel = new ClienteClustering(connectionString);
            var resultadosMl = mlModel.EjecutarClustering();

            var fechaInicioHoy = DateTime.Today;
            var resultadosFinales = new List<AnalisisClienteViewModel>();

          
            foreach (var (Cliente, Cluster, DeberiaAvisarse) in resultadosMl)
            {
             
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.nombre == Cliente.Nombre);

               
                bool enviadoHoy = false;

                

                resultadosFinales.Add(new AnalisisClienteViewModel
                {
                    ClienteDatos = Cliente,
                    ClusterId = Cluster,
                    DeberiaAvisarse = DeberiaAvisarse,
                    EmailCliente = usuario?.email ?? "N/A",
                    AvisoEnviadoHoy = enviadoHoy
                });
            }

            return View(resultadosFinales);
        }
        */

        public async Task<IActionResult> ResultadosClustering()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso Denegado.";
                return RedirectToAction("Index", "Principal");
            }

            var connectionString = _context.Database.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                TempData["Error"] = "No se pudo obtener la cadena de conexión a la base de datos.";
                return RedirectToAction("Panel", "Admin");
            }

            try
            {
                var mlModel = new ClienteClustering(connectionString);
                var resultadosMl = mlModel.EjecutarClustering();

                var fechaInicioHoy = DateTime.Today;
                var resultadosFinales = new List<AnalisisClienteViewModel>();

                foreach (var (Cliente, Cluster, DeberiaAvisarse) in resultadosMl)
                {
                // BUSCAR POR ID EN LUGAR DE NOMBRE (MÁS PRECISO)
                    var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.id == Cliente.UsuarioId);

                // Verificar si ya se envió un aviso hoy (necesitarías una tabla de logs)
                    bool enviadoHoy = false; // Implementar esta lógica

                    resultadosFinales.Add(new AnalisisClienteViewModel
                    {
                        ClienteDatos = Cliente,
                        ClusterId = Cluster,
                        DeberiaAvisarse = DeberiaAvisarse,
                        EmailCliente = usuario?.email ?? "N/A",
                        AvisoEnviadoHoy = enviadoHoy
                    });
                }

                return View(resultadosFinales);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error en el clustering: {ex.Message}";
                return RedirectToAction("Panel", "Admin");
            }
        }

        public IActionResult Panel()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso Denegado.";
                return RedirectToAction("Index", "Principal");
            }

            return View();
        }

        public IActionResult Usuarios()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso Denegado.";
                return RedirectToAction("Index", "Principal");
            }
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        [HttpPost]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (usuario == null)
            {
                TempData["Error"] = "Usuario no encontrado.";
                return RedirectToAction("ListaUsuarios");
            }

            
            if (usuario.email == HttpContext.Session.GetString("Usuario"))
            {
                TempData["Error"] = "No puedes eliminar tu propio usuario mientras estás logueado.";
                return RedirectToAction("ListaUsuarios");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Usuario {usuario.nombre} {usuario.apellido} eliminado correctamente.";
            return RedirectToAction("Usuarios");
        }

        [HttpGet]
        public IActionResult CrearProducto()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso denegado.";
                return RedirectToAction("Index", "Principal");
            }
            ViewBag.Categorias = _context.Categorias
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.nombre
                })
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CrearProducto(Productos producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }

            _context.Productos.Add(producto);
            _context.SaveChanges();
            TempData["Mensaje"] = "Producto agregado correctamente.";
            return RedirectToAction("ListaProductos");
        }

        public IActionResult EliminarProducto(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso denegado.";
                return RedirectToAction("Index", "Principal");
            }

            var producto = _context.Productos.FirstOrDefault(p => p.id == id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
                TempData["Mensaje"] = "Producto eliminado correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "Producto no encontrado.";
            }

            return RedirectToAction("ListaProductos");
        }

        public IActionResult ListaProductos()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso denegado.";
                return RedirectToAction("Index", "Principal");
            }

            var productos = _context.Productos.ToList();
            return View(productos);
        }

        [HttpPost]
        public IActionResult ActualizarLista(List<Productos> productos)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Mensaje"] = "Acceso Denegado.";
                return RedirectToAction("Index", "Principal");
            }

            foreach (var producto in productos)
            {
                var productoExistente = _context.Productos.Find(producto.id);
                if (productoExistente != null)
                {
                    productoExistente.nombre = producto.nombre;
                    productoExistente.precio = producto.precio;
                    productoExistente.Stock = producto.Stock;
                    productoExistente.categoriaId = producto.categoriaId;
                    productoExistente.imagenUrl = producto.imagenUrl;
                }
            }

            _context.SaveChanges();
            TempData["Mensaje"] = "Productos actualizados correctamente.";
            return RedirectToAction("ListaProductos");
        }

        [HttpPost]
        public async Task<JsonResult> EnviarAvisoManual(int usuarioId)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null)
                    return Json(new { success = false, message = "Usuario no encontrado" });

                if (string.IsNullOrWhiteSpace(usuario.email))
                    return Json(new { success = false, message = "El usuario no tiene un email válido" });

                var asunto = "Te extrañamos en Farmacia El Palenque";
                var mensaje = $"¡Hola {usuario.nombre}! Ha pasado un tiempo desde tu última visita a nuestra farmacia. Queríamos recordarte que estamos aquí para ayudarte. ¡Esperamos verte pronto!";

                await _emailSender.SendEmailAsync(usuario.email, asunto, mensaje);

                return Json(new { success = true, message = "Aviso enviado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}