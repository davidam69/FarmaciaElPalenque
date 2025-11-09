public class ClienteClustering
{
    private readonly string _connectionString;

    public ClienteClustering(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("La cadena de conexión no puede ser nula o vacía", nameof(connectionString));
        }
        _connectionString = connectionString;
    }

    public List<(ClienteData Cliente, uint PredictedClusterId, bool DeberiaAvisarse)> EjecutarClustering()
    {
        var mlContext = new MLContext();
        var clientes = new List<ClienteData>();

        var query = @"
            SELECT 
                u.id as UsuarioId,
                u.nombre,
                COUNT(p.id) AS ComprasTotales,
                SUM(p.total) AS MontoGastado,
                DATEDIFF(day, MAX(p.fecha), GETDATE()) AS DiasDesdeUltimaCompra,
                CASE 
                    WHEN COUNT(p.id) > 1 THEN 
                        AVG(DATEDIFF(day, p_anterior.fecha, p.fecha))
                    ELSE 0 
                END AS PromedioDiasEntreCompras
            FROM Usuarios u
            LEFT JOIN Pedidos p ON u.id = p.usuarioId
            LEFT JOIN Pedidos p_anterior ON p_anterior.usuarioId = u.id 
                AND p_anterior.fecha = (
                    SELECT MAX(fecha) 
                    FROM Pedidos p2 
                    WHERE p2.usuarioId = u.id AND p2.fecha < p.fecha
                )
            GROUP BY u.id, u.nombre
            HAVING COUNT(p.id) >= 1;";

        try
        {
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var comando = new SqlCommand(query, conexion))
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        clientes.Add(new ClienteData
                        {
                            UsuarioId = lector["UsuarioId"] == DBNull.Value ? 0 : Convert.ToInt32(lector["UsuarioId"]),
                            Nombre = lector["nombre"] == DBNull.Value ? string.Empty : lector["nombre"].ToString() ?? string.Empty,
                            ComprasTotales = lector["ComprasTotales"] == DBNull.Value ? 0 : Convert.ToSingle(lector["ComprasTotales"]),
                            MontoGastado = lector["MontoGastado"] == DBNull.Value ? 0 : Convert.ToSingle(lector["MontoGastado"]),
                            DiasDesdeUltimaCompra = lector["DiasDesdeUltimaCompra"] == DBNull.Value ? 0 : Convert.ToSingle(lector["DiasDesdeUltimaCompra"]),
                            PromedioDiasEntreCompras = lector["PromedioDiasEntreCompras"] == DBNull.Value ? 0 : Convert.ToSingle(lector["PromedioDiasEntreCompras"])
                        });
                    }
                }
            }

            // Validar que hay datos para procesar
            if (!clientes.Any())
            {
                return new List<(ClienteData, uint, bool)>();
            }

            var dataView = mlContext.Data.LoadFromEnumerable(clientes);

            var pipeline = mlContext.Transforms.Concatenate("Features",
                    nameof(ClienteData.ComprasTotales),
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
        catch (Exception ex)
        {
            // Loggear el error (deberías usar ILogger en producción)
            Console.WriteLine($"Error en EjecutarClustering: {ex.Message}");
            throw;
        }
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

// Clases de apoyo
public class ClienteData
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public float ComprasTotales { get; set; }
    public float MontoGastado { get; set; }
    public float DiasDesdeUltimaCompra { get; set; }
    public float PromedioDiasEntreCompras { get; set; }
}

public class PrediccionCliente
{
    [ColumnName("PredictedLabel")]
    public uint PredictedClusterId { get; set; }
}