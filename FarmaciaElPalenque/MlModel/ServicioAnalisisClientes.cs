using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FarmaciaElPalenque.MlModel;
using FarmaciaElPalenque.Services;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FarmaciaElPalenque.Models;

namespace FarmaciaElPalenque.Services
{
    public class ServicioAnalisisClientes : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicioAnalisisClientes(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Ejecutando tarea de análisis de clientes...");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var mlModel = new ClienteClustering(dbContext.Database.GetConnectionString());

                    var resultados = mlModel.EjecutarClustering();
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                    foreach (var resultado in resultados)
                    {
                        if (resultado.DeberiaAvisarse)
                        {
                            var cliente = dbContext.Usuarios.FirstOrDefault(u => u.nombre == resultado.Cliente.Nombre);

                            if (cliente != null && !string.IsNullOrEmpty(cliente.email))
                            {
                                var asunto = "Te extrañamos en Farmacia El Palenque";
                                var mensaje = $"¡Hola {cliente.nombre}! Ha pasado un tiempo desde tu última visita a nuestra farmacia. Queríamos recordarte que estamos aquí para ayudarte. ¡Esperamos verte pronto!";

                                await emailSender.SendEmailAsync(cliente.email, asunto, mensaje);
                                Console.WriteLine($"Correo de re-engagement enviado a: {cliente.email}");
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