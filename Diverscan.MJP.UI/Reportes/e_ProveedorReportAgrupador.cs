using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.Reportes
{
    public class e_ProveedorReportAgrupador
    {
        public static void Agrupar(List<e_ProveedorReport> e_ProveedorReports)
        {
            e_ProveedorReport[] arrayReport = new e_ProveedorReport[e_ProveedorReports.Count];
            e_ProveedorReports.CopyTo(arrayReport);


        }
    }
}