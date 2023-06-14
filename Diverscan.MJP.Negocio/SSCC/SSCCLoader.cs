using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.Entidades.SSCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SSCC
{
   public  class SSCCLoader
    {
       public static List<SSCCRecord> ObtenerSSCC(string Detalle_SSCC)
        {
            ConsultarSSCC consultaSSCC_ = new ConsultarSSCC();
            return consultaSSCC_.GetConsultarSSCC(Detalle_SSCC);
        }
    }
}
