using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Recepcion
{
    public class ResultadoObtenerDevolucionInmediata : ResultWS
    {
        private List<e_DevolucionInmediata> _Devoluciones = new List<e_DevolucionInmediata>();

        public List<e_DevolucionInmediata> devoluciones { get => _Devoluciones; set => _Devoluciones = value; }
    }
}
