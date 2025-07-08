using Microsoft.ML.Data;

namespace FarmaciaElPalenque.MlModel
{
    public class PrediccionCliente
    {

        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

    }
}
