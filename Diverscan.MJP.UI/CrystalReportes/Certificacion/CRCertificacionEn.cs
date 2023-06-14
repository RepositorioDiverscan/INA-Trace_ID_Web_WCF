using Diverscan.MJP.AccesoDatos.Certificación;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.CrystalReportes.Certificacion
{
    public class CRCertificacionEn : eCertificacionEn
    {
        //agregar numero de lineas int
        public int NumeroLineas { get; set; }
        public string idRegistroOlaRuta { get; set; }
    }
}