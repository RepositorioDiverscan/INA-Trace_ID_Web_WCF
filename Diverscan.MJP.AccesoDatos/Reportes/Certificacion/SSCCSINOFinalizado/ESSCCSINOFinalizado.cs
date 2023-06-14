using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado
{
    [Serializable]
    public class ESSCCSINOFinalizado
    {
        DateTime _fehaInicio,
            _fechaFin;
        string _idCliente;

        public DateTime FehaInicio { get => _fehaInicio; set => _fehaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public string IdCliente { get => _idCliente; set => _idCliente = value; }
    }
}
