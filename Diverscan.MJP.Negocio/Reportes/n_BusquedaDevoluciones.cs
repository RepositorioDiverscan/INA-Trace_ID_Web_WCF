using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Trazabilidad;
using Diverscan.MJP.Entidades.Reportes;
using System.Data;

namespace Diverscan.MJP.Negocio.Reportes
{
   public class n_BusquedaDevoluciones
    {
        public static List<e_ObtenerDevoluciones> ObtenerDatosDevoluciones(int idDestino, DateTime pfechaInicio, DateTime pFechaFin)
        {
            return da_BusquedaDestino.ObtenerDatosBusquedaDestino(idDestino, pfechaInicio, pFechaFin);

        }

    }
}
