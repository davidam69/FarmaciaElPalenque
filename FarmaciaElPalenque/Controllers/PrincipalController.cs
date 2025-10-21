namespace FarmaciaElPalenque.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public PrincipalController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .ToList();

            return View(productos);
        }

        public IActionResult Detalle(int id)
        {
            var productoSeleccionado = _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.id == id);

            if (productoSeleccionado == null)
            {
                return NotFound("Producto no encontrado");
            }
            return View(productoSeleccionado);
        }

        public async Task<IActionResult> TestEmail()
        {
          
            var toEmail = "agus.luu@gmail.com";

            var subject = "Prueba de Envío (Farmacia El Palenque)";
            var body = "<h2 style='color: #007bff;'>¡Envío Exitoso!</h2><p>Este correo confirma que la configuración de MailKit en tu proyecto funciona.</p>";

            try
            {
                
                await _emailSender.SendEmailAsync(toEmail, subject, body);

                
                ViewBag.Resultado = $"Correo enviado exitosamente a {toEmail}.";
            }
            catch (Exception ex)
            {
                
                ViewBag.Resultado = $"Error al enviar el correo: {ex.Message}.";
            }

 
            return View();
        }
    }
}