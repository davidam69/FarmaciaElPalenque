/* namespace FarmaciaElPalenque.MlModel
{
    public class ClienteData
    {
        [LoadColumn(0)]
        public string? Nombre { get; set; }
        [LoadColumn(1)]
        public float ComprasMensuales { get; set; }
        [LoadColumn(2)]
        public float MontoGastado { get; set; }
        [LoadColumn(3)]
        public float DiasDesdeUltimaCompra { get; set; }
        [LoadColumn(4)]
        public float PromedioDiasEntreCompras { get; set; }
    }
}  */

namespace FarmaciaElPalenque.MlModel
{
    public class ClienteData
    {
        [LoadColumn(0)]
        public int UsuarioId { get; set; }

        [LoadColumn(1)]
        public string? Nombre { get; set; }

        [LoadColumn(2)]
        public float ComprasTotales { get; set; }

        [LoadColumn(3)]
        public float MontoGastado { get; set; }

        [LoadColumn(4)]
        public float DiasDesdeUltimaCompra { get; set; }

        [LoadColumn(5)]
        public float PromedioDiasEntreCompras { get; set; }
    }
}