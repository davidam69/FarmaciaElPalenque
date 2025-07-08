using Microsoft.ML.Data;

namespace FarmaciaElPalenque.MlModel
{
    public class UsuarioData
    {

        [LoadColumn(0)] public string? Nombre;
        [LoadColumn(1)] public float Edad;
        [LoadColumn(2)] public float ComprasMensuales;
        [LoadColumn(3)] public float MontoGastado;
        [LoadColumn(4)] public float DiasDesdeUltimaCompra;

    }
}
