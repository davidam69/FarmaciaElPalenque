namespace FarmaciaElPalenque.ViewComponents
{
    public class CartBadgeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var esAdmin = HttpContext.Session.GetString("Rol") == "Administrador";
            if (esAdmin)
            {
                // O no renderizás nada:
                return Content(string.Empty);
            }

            // Usa tus extensiones de sesión
            var cart = HttpContext.Session.ObtenerCarrito() ?? new List<Carrito>();
            var count = cart.Sum(i => i.Cantidad);

            return View(count); // pasa el número como modelo
        }
    }
}
