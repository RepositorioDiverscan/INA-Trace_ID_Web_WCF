using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Trazabilidad;
using Diverscan.MJP.Entidades.Reportes;
using System.Data;
using Diverscan.MJP.Entidades;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_BusquedaOrdenC
    {
        public static List<e_ObtenerOrdenesC> ObtenerOrdenesC(int idMaestroOrdenC, DateTime pfechaInicio, DateTime pFechaFin)
        {
            return da_BusquedaOrdenC.ObtenerBusquedaOC(idMaestroOrdenC, pfechaInicio, pFechaFin);

        }

     
    }
}
