namespace FarmaciaElPalenque.Services
{
    public class ServicioAnalisisClientes : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ServicioAnalisisClientes> _logger;

        public ServicioAnalisisClientes(IServiceProvider serviceProvider, ILogger<ServicioAnalisisClientes> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Ejecutando tarea de análisis de clientes...");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var connectionString = dbContext.Database.GetConnectionString();
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        _logger.LogError("No se pudo obtener la cadena de conexión de la base de datos");
                        await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                        continue;
                    }

                    var mlModel = new ClienteClustering(connectionString);

                    var resultados = mlModel.EjecutarClustering();
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                    foreach (var resultado in resultados)
                    {
                        if (resultado.DeberiaAvisarse)
                        {
                            // BUSCAR POR ID EN LUGAR DE NOMBRE
                            var cliente = dbContext.Usuarios.FirstOrDefault(u => u.id == resultado.Cliente.UsuarioId);

                            if (cliente != null && !string.IsNullOrEmpty(cliente.email))
                            {
                                var asunto = "Te extrañamos en Farmacia El Palenque";
                                var mensaje = $"¡Hola {cliente.nombre}! Ha pasado un tiempo desde tu última visita a nuestra farmacia. Queríamos recordarte que estamos aquí para ayudarte. ¡Esperamos verte pronto!";
                                try
                                {
                                    await emailSender.SendEmailAsync(cliente.email, asunto, mensaje);
                                    _logger.LogInformation($"Correo de re-engagement enviado a: {cliente.email}");

                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, $"Error al enviar correo a: {cliente.email}");
                                }
                            }
                            else
                            {
                                _logger.LogWarning($"Cliente no encontrado o sin email: UsuarioId {resultado.Cliente.UsuarioId}");
                            }
                        }
                    }
                }

                // Espera 24 horas antes de volver a ejecutar
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}