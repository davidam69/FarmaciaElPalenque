using FarmaciaElPalenque.MlModel;
using System;

namespace FarmaciaElPalenque.Models
{
    
    public class AnalisisClienteViewModel
    {
        
        public ClienteData ClienteDatos { get; set; }
        public uint ClusterId { get; set; }
        public bool DeberiaAvisarse { get; set; }

        public string EmailCliente { get; set; }
        public bool AvisoEnviadoHoy { get; set; }
    }
}