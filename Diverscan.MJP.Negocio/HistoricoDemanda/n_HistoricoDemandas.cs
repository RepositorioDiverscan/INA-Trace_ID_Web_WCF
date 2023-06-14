using Diverscan.MJP.AccesoDatos.HistoricoDemanda;
using Diverscan.MJP.Entidades.HistoricoDemanda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.HistoricoDemanda
{
    public class n_HistoricoDemandas
    {
        public static List<HistoricoDemandaRecord> ObtenerHistoricoDemandaArticuloFechas(DateTime fechaIni, DateTime fechaFin, long idArticulo, long idProveedor, bool filtroPorArticulo)
        {
            HistoricoDemandaDBA historicoDemandaDBA = new HistoricoDemandaDBA();
            return historicoDemandaDBA.ObtenerHistoricoDemandaArticuloFechas(fechaIni, fechaFin, idArticulo, idProveedor, filtroPorArticulo);
        }


        public static List<HistoricoDemandaProveedorRecord> ObtenerHistoricoDemandaProveedorFechas(DateTime fechaIni, DateTime fechaFin, long idProveedor)
        {
            HistoricoDemandaDBA historicoDemandaDBA = new HistoricoDemandaDBA();
            return historicoDemandaDBA.ObtenerHistoricoDemandaProveedorFechas(fechaIni, fechaFin, idProveedor);
        }
       
        //Versión mejorada permite obtener el histórico demanda por IdInternoArticulo
        public static List<HistoricoDemandaRecord> ObtenerHistoricoDemandaArticuloIdInternoFechas(string idInternoArticulo, DateTime fechaIni, DateTime fechaFin)
        {
            HistoricoDemandaDBA historicoDemandaDBA = new HistoricoDemandaDBA();
            return historicoDemandaDBA.ObtenerHistoricoDemandaArticuloIdInternoFechas(idInternoArticulo, fechaIni, fechaFin);
        }
    }
}
