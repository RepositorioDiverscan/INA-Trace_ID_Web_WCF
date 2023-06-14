using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_AuditoriaReport
    {
        public static List<e_KardexReport> ObtenerAuditoriaReportInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {
            return da_AuditoriaReport.ObtenerAuditoriaReportInfo(fechaIni, fechaFin, idArticulo);
        }
    }
}
