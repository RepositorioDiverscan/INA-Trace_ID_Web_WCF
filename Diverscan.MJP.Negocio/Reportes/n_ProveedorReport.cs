using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_ProveedorReport
    {
        public static List<e_ProveedorReport> ObtenerProveedorReportInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {
            return da_ProveedorReport.ObtenerProveedorReportInfo(fechaIni, fechaFin, idArticulo);
        }

        public static List<e_ProveedorGrid> ObtenerProveedorGridInfo(DateTime fechaIni, DateTime fechaFin, string idArticulo)
        {
            return da_ProveedorReport.ObtenerProveedorGridInfo(fechaIni, fechaFin, idArticulo);
        }
    }
}

