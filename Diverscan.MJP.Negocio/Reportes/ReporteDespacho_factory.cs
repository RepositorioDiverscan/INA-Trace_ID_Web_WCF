using Diverscan.MJP.AccesoDatos.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Diverscan.MJP.Negocio.Reportes
{
    public class ReporteDespacho_factory
    {
        public static ReporteDespachoN Create()
        {
            return new ReporteDespacho_factory().build();
        }

        public ReporteDespachoN build()
        {
            ReporteDespacho_DB Reporte_Despacho_DB = new ReporteDespacho_DB();
            ReporteDespachoN ReporteDespachoN = new ReporteDespachoN(Reporte_Despacho_DB);
            return ReporteDespachoN;
        }

    }
}

