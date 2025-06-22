using Microsoft.ML;

namespace FarmaciaElPalenque.MlModel
{
    public class ClienteClustering
    {

        private readonly string rutaCSV;
        private readonly MLContext mlContext;

        public ClienteClustering(string rutaCSV)
        {
            this.rutaCSV = rutaCSV;
            this.mlContext = new MLContext();
        }

        public List<(ClienteData Cliente, uint Cluster, bool DeberiaAvisarse)> EjecutarClustering()
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<ClienteData>(
                rutaCSV, separatorChar: ',', hasHeader: true);

            var pipeline = mlContext.Transforms.Concatenate("Features",
                    nameof(ClienteData.Edad),
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

            var datosOriginales = mlContext.Data
                .CreateEnumerable<ClienteData>(dataView, reuseRowObject: false)
                .ToList();

            var hoy = DateTime.Today;

            var combinados = datosOriginales.Zip(resultados, (cliente, prediccion) =>
            {
                var fechaUltimaCompra = hoy.AddDays(-cliente.DiasDesdeUltimaCompra);
                var diasDesdeUltimaCompra = (hoy - fechaUltimaCompra).TotalDays;
                bool debeAvisarse = diasDesdeUltimaCompra > cliente.DiasDesdeUltimaCompra + 2;

                return (cliente, prediccion.PredictedClusterId, debeAvisarse);
            }).ToList();

            return combinados;
        }

        public void AnalizarDiasDeCompra(List<(ClienteData Cliente, uint Cluster)> resultados)
        {
            var agrupados = resultados
                .Select(r => new CompraConFecha { Cliente = r.Cliente, Cluster = r.Cluster })
                .GroupBy(cf => cf.Cluster)
                .ToList();

            foreach (var grupo in agrupados)
            {
                Console.WriteLine($"Cluster {grupo.Key}:");

                var dias = grupo.Select(x => (int)x.DiasDesdeUltimaCompra)
                                .GroupBy(d => d)
                                .OrderByDescending(g => g.Count());

                foreach (var dia in dias)
                {
                    Console.WriteLine($"  Día {dia.Key}: {dia.Count()} compras");
                }


            }
        }
    }
}
