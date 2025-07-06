using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FarmaciaElPalenque.MlModel
{
    public class ClienteClustering
    {
        private readonly string connectionString;
        private readonly MLContext mlContext;

        public ClienteClustering(string connectionString)
        {
            this.connectionString = connectionString;
            this.mlContext = new MLContext();
        }

        public List<(ClienteData Cliente, uint Cluster, bool DeberiaAvisarse)> EjecutarClustering()
        {
            // 1. Leer datos desde la base
            var clientes = new List<ClienteData>();

            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                var query = "SELECT Edad, ComprasMensuales, MontoGastado, DiasDesdeUltimaCompra FROM Clientes";

                using (var comando = new SqlCommand(query, conexion))
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        clientes.Add(new ClienteData
                        {
                            Edad = Convert.ToSingle(lector["Edad"]),
                            ComprasMensuales = Convert.ToSingle(lector["ComprasMensuales"]),
                            MontoGastado = Convert.ToSingle(lector["MontoGastado"]),
                            DiasDesdeUltimaCompra = Convert.ToSingle(lector["DiasDesdeUltimaCompra"])
                        });
                    }
                }
            }

            // 2. Cargar datos en ML.NET
            var dataView = mlContext.Data.LoadFromEnumerable(clientes);

            // 3. Definir pipeline de clustering
            var pipeline = mlContext.Transforms.Concatenate("Features",
                    nameof(ClienteData.Edad),
                    nameof(ClienteData.ComprasMensuales),
                    nameof(ClienteData.MontoGastado),
                    nameof(ClienteData.DiasDesdeUltimaCompra))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

            // 4. Entrenar el modelo
            var modelo = pipeline.Fit(dataView);
            var predicciones = modelo.Transform(dataView);

            // 5. Obtener resultados de clustering
            var resultados = mlContext.Data
                .CreateEnumerable<PrediccionCliente>(predicciones, reuseRowObject: false)
                .ToList();

            var hoy = DateTime.Today;

            // 6. Combinar con la lógica de aviso personalizada
            var combinados = clientes.Zip(resultados, (cliente, prediccion) =>
            {
                // Simula que la última compra fue hace X días
                var fechaUltimaCompra = hoy.AddDays(-cliente.DiasDesdeUltimaCompra);
                var diasDesdeUltimaCompra = (hoy - fechaUltimaCompra).TotalDays;

                // Si se pasó por más de 2 días de su frecuencia habitual, avisar
                bool debeAvisarse = diasDesdeUltimaCompra > cliente.DiasDesdeUltimaCompra + 2;

                return (cliente, prediccion.PredictedClusterId, debeAvisarse);
            }).ToList();

            return combinados;
        }

        public void AnalizarDiasDeCompra(List<(ClienteData Cliente, uint Cluster)> resultados)
        {
            var agrupados = resultados
                .Select(r => new { r.Cliente, r.Cluster })
                .GroupBy(x => x.Cluster);

            foreach (var grupo in agrupados)
            {
                Console.WriteLine($"Cluster {grupo.Key}:");

                var dias = grupo
                    .Select(x => (int)x.Cliente.DiasDesdeUltimaCompra)
                    .GroupBy(d => d)
                    .OrderByDescending(g => g.Count());

                foreach (var dia in dias)
                {
                    Console.WriteLine($"  Día {dia.Key}: {dia.Count()} clientes");
                }
            }
        }
    }
}
