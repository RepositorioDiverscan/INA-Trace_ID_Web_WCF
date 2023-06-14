using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion
{
    [Serializable]
    public class EListAsignacionCertificacion
    {
        string _SSCC,
            _nombre,
            _sku;
        int _unidadAsignada,
            _cantidadCertificada;
        string _estadoSSCC;

        public EListAsignacionCertificacion(string sSCC, string nombre, string sku, int unidadAsignada, int cantidadCertificada, string estadoSSCC)
        {
            _SSCC = sSCC;
            _nombre = nombre;
            _sku = sku;
            _unidadAsignada = unidadAsignada;
            _cantidadCertificada = cantidadCertificada;
            _estadoSSCC = estadoSSCC;
        }

        public string SSCC { get => _SSCC; set => _SSCC = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public int UnidadAsignada { get => _unidadAsignada; set => _unidadAsignada = value; }
        public int CantidadCertificada { get => _cantidadCertificada; set => _cantidadCertificada = value; }
        public string EstadoSSCC { get => _estadoSSCC; set => _estadoSSCC = value; }
    }
}
