namespace FarmaciaElPalenque.Models
{
    public static class ExtensionDelCarrito
    {
        private const string KEY = "Carrito";

        public static List<Carrito> ObtenerCarrito(this ISession session)
        {
            var json = session.GetString(KEY);
            return string.IsNullOrEmpty(json)
                ? new List<Carrito>()
                : System.Text.Json.JsonSerializer.Deserialize<List<Carrito>>(json)
                  ?? new List<Carrito>();
        }

        public static void GuardarCarrito(this ISession session, List<Carrito> cart)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(cart);
            session.SetString(KEY, json);
        }

        public static void BorrarCarrito(this ISession session) => session.Remove(KEY);
    }
}
