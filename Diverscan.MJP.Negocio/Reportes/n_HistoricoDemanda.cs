using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_HistoricoDemanda
    {
        public static List<e_HistoricoDemanda> ObtenerReporteHistoricoDemanda(DateTime fechaIni, DateTime fechaFin, string IdArticulo)
        {
            return da_HistoricoDemanda.ObtenerReporteHistoricoDemanda(fechaIni, fechaFin, IdArticulo);
        }
    }
}
