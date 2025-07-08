public class IAController : Controller
{
    private readonly AppDbContext _context;

    public IAController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Resultados()
    {
        if (HttpContext.Session.GetString("Rol") != "Administrador")
        {
            TempData["Mensaje"] = "Acceso denegado.";
            return RedirectToAction("Index", "Principal");
        }

        var usuarios = _context.Usuarios.ToList();

        var clustering = new UsuarioClustering();
        var resultados = clustering.EjecutarClustering(usuarios);

        return View(resultados);
    }
}

