using FarmaciaElPalenque.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

public class UsuarioClustering
{
    private readonly MLContext mlContext;

    public UsuarioClustering()
    {
        mlContext = new MLContext();
    }

    public List<(Usuario Usuario, uint Cluster, bool DeberiaAvisarse)> EjecutarClustering(List<Usuario> usuarios)
    {
        var dataView = mlContext.Data.LoadFromEnumerable(usuarios.Select(u => new UsuarioInput
        {
            Edad = u.Edad,
            ComprasMensuales = u.ComprasMensuales,
            MontoGastado = (float)u.MontoGastado,
            DiasDesdeUltimaCompra = u.DiasDesdeUltimaCompra
        }));

        var pipeline = mlContext.Transforms.Concatenate("Features", nameof(UsuarioInput.Edad), nameof(UsuarioInput.ComprasMensuales),
            nameof(UsuarioInput.MontoGastado), nameof(UsuarioInput.DiasDesdeUltimaCompra))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
            .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

        var modelo = pipeline.Fit(dataView);
        var predicciones = modelo.Transform(dataView);

        var resultados = mlContext.Data
            .CreateEnumerable<PrediccionCliente>(predicciones, reuseRowObject: false)
            .ToList();

        return usuarios.Zip(resultados, (usuario, prediccion) =>
        {
            bool debeAvisarse = usuario.DiasDesdeUltimaCompra > 15;
            return (usuario, prediccion.PredictedClusterId, debeAvisarse);
        }).ToList();
    }
}

public class UsuarioInput
{
    public float Edad { get; set; }
    public float ComprasMensuales { get; set; }
    public float MontoGastado { get; set; }
    public float DiasDesdeUltimaCompra { get; set; }
}

public class PrediccionCliente
{
    [ColumnName("PredictedLabel")]
    public uint PredictedClusterId { get; set; }
}
