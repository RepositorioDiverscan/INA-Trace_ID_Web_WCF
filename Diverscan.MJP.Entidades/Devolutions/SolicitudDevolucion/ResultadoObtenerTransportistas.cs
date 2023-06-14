using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    public class ResultadoObtenerTransportistas : ResultWS
    {
        private List<e_Transportista> _Transportistas = new List<e_Transportista>();

        public List<e_Transportista> transportistas { get => _Transportistas; set => _Transportistas = value; }
    }
}
