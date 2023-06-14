using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System.Collections.Generic;


namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_DiferenciaInventario
    {
        public static List<e_DiferenciaInventario> ObtenerReporteDiferenciaInventario(string IdArticulo, string OpcionReporte, string idCompania)
        {
            return da_DiferenciaInventario.ObtenerReporteDiferenciaInventario(IdArticulo,OpcionReporte, idCompania);
        }
    }
}
