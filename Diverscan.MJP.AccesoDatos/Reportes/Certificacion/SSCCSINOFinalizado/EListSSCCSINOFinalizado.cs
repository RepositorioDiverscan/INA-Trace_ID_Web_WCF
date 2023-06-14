using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado
{
    [Serializable]
    public class EListSSCCSINOFinalizado
    {
        string _sscc,
            _fecha,
            _nombreCliente,
            _porCertificado;
        long _idOla;

        public EListSSCCSINOFinalizado(string sscc, string fecha, string nombreCliente, string porCertificado, long idOla)
        {
            _sscc = sscc;
            _fecha = fecha;
            _nombreCliente = nombreCliente;
            _porCertificado = porCertificado;
            _idOla = idOla;
        }

        public string Sscc { get => _sscc; set => _sscc = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public string NombreCliente { get => _nombreCliente; set => _nombreCliente = value; }
        public string PorCertificado { get => _porCertificado; set => _porCertificado = value; }
        public long IdOla { get => _idOla; set => _idOla = value; }
    }
}
