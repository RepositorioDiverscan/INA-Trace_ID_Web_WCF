using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AjusteInventario
{
    public class N_LogAjustesTRA
    {
        public static long InsertLogAjustesTRA_Y_ObtenerIDLogAjustesTRA(long idSolicitudAjusteInventario)
        {
            LogAjustesTRA_DBA logAjustesTRA_DBA = new LogAjustesTRA_DBA();
            return logAjustesTRA_DBA.InsertLogAjustesTRA_Y_ObtenerIDLogAjustesTRA(idSolicitudAjusteInventario);
        }
        public static e_LogAjustesTRARecord ObtenerUnicoRegistro(e_LogAjustesTRARecord e_logAjustesTRARecord)
        {
            LogAjustesTRA_DBA logAjustesTRA_DBA = new LogAjustesTRA_DBA();
            return  logAjustesTRA_DBA.ObtenerUnicoRegistro(e_logAjustesTRARecord);
        }
    }
}
