namespace FarmaciaElPalenque.MlModel
{
    public class CompraConFecha
    {

        public ClienteData Cliente;
        public uint Cluster;
        public float DiasDesdeUltimaCompra => Cliente.DiasDesdeUltimaCompra;

    }
}
