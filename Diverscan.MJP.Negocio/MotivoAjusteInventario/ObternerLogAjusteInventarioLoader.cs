using Diverscan.MJP.AccesoDatos.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.MotivoAjusteInventario
{
    public class ObternerLogAjusteInventarioLoader
    {
        public static List<LogAjusteInventarioRecord> ObtenerLogAjusteInventario(DateTime fechaInicio, DateTime fechaFin, int estado)
        {        
            LogAjusteInventarioBD logAjusteInventarioBD = new LogAjusteInventarioBD();
            return logAjusteInventarioBD.GetLogAjusteInventarioData(fechaInicio, fechaFin,estado);
        }

        public static void UpdateLog(long idRecord, int estado)
        {
            LogAjusteInventarioBD logAjusteInventarioBD = new LogAjusteInventarioBD();
            logAjusteInventarioBD.UpdateLogAjusteInventario(idRecord,estado);
        }
    }
}
