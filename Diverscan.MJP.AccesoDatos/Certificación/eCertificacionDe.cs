using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Certificación
{
    [Serializable]
    public class eCertificacionDe
    {
        public string NombreArticulo { get; set; }
        public string IdInterno { get; set; }
        public string CantidadSolicitada { get; set; }
        public string CantidadAlistada { get; set; }
    }
}
