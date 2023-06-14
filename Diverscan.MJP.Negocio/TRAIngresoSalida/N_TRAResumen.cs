using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.TRAIngresoSalida
{
    public class N_TRAResumen
    {
        public static List<TRAIngresoSalidaArticulosRecord> ObtenerArticuloSalida(long idArticulo, long idUbicacion, string lote, DateTime fechaVencimiento)
        {
            TRAResumenDBA traResumenDBA = new TRAResumenDBA();
            var articulos = traResumenDBA.ObtenerArticuloSalida(idArticulo, idUbicacion, lote, fechaVencimiento);
            return articulos;
        }

        public static int ObtenerCantidadArticulosSalida(long idArticulo, long idUbicacion, string lote, DateTime fechaVencimiento)
        {
            TRAResumenDBA traResumenDBA = new TRAResumenDBA();
            return traResumenDBA.ObtenerCantidadArticulosSalida(idArticulo, idUbicacion, lote, fechaVencimiento);
        }
    }
}
