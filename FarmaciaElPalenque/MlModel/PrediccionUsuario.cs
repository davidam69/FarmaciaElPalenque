using Microsoft.ML.Data;

namespace FarmaciaElPalenque.MlModel
{
    public class PrediccionUsuario
    {

        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

    }
}
