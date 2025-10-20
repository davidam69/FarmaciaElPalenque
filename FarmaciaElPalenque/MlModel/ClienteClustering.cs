using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

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
            var clientes = new List<ClienteData>();

            // CAMBIO 1: SE ELIMINAN 'u.edad' DEL SELECT Y DEL GROUP BY EN EL QUERY SQL
            var query = @"
                SELECT
                    u.nombre,
                    COUNT(c.id) AS ComprasTotales,
                    SUM(c.montoTotal) AS MontoGastado,
                    DATEDIFF(day, MAX(c.fechaCompra), GETDATE()) AS DiasDesdeUltimaCompra,
                    AVG(DATEDIFF(day, c_anterior.fechaCompra, c.fechaCompra)) AS PromedioDiasEntreCompras
                FROM Usuarios u
                LEFT JOIN Compras c ON u.id = c.usuarioId
                LEFT JOIN Compras c_anterior ON c_anterior.usuarioId = u.id AND c_anterior.fechaCompra < c.fechaCompra
                GROUP BY u.nombre 
                HAVING COUNT(c.id) > 1;
            ";

            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                using (var comando = new SqlCommand(query, conexion))
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        clientes.Add(new ClienteData
                        {
                            Nombre = lector["nombre"].ToString(),
                            // CAMBIO 2: SE ELIMINA LA LECTURA DE 'Edad'
                            ComprasMensuales = lector["ComprasTotales"] == DBNull.Value ? 0 : Convert.ToSingle(lector["ComprasTotales"]),
                            MontoGastado = lector["MontoGastado"] == DBNull.Value ? 0 : Convert.ToSingle(lector["MontoGastado"]),
                            DiasDesdeUltimaCompra = lector["DiasDesdeUltimaCompra"] == DBNull.Value ? 0 : Convert.ToSingle(lector["DiasDesdeUltimaCompra"]),
                            PromedioDiasEntreCompras = lector["PromedioDiasEntreCompras"] == DBNull.Value ? 0 : Convert.ToSingle(lector["PromedioDiasEntreCompras"])
                        });
                    }
                }
            }

            var dataView = mlContext.Data.LoadFromEnumerable(clientes);

            // CAMBIO 3: SE ELIMINA 'Edad' DE LA TUBERÍA DE ML.NET (FEATURES)
            var pipeline = mlContext.Transforms.Concatenate("Features",
                nameof(ClienteData.ComprasMensuales),
                nameof(ClienteData.MontoGastado),
                nameof(ClienteData.DiasDesdeUltimaCompra))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

            var modelo = pipeline.Fit(dataView);
            var predicciones = modelo.Transform(dataView);

            var resultados = mlContext.Data
                .CreateEnumerable<PrediccionCliente>(predicciones, reuseRowObject: false)
                .ToList();

            var combinados = clientes.Zip(resultados, (cliente, prediccion) =>
            {
                var tolerancia = 2.0f;
                bool debeAvisarse = false;

                if (cliente.PromedioDiasEntreCompras > 0)
                {
                    debeAvisarse = cliente.DiasDesdeUltimaCompra > (cliente.PromedioDiasEntreCompras + tolerancia);
                }

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