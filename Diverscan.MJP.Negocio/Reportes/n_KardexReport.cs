using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_KardexReport
    {
        public static List<e_KardexReport> ObtenerKardexReportInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {
            return da_KardexReport.ObtenerKardexReportInfo(fechaIni, fechaFin, idArticulo);
        }
    }
}
