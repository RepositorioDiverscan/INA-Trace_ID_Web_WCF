using Diverscan.MJP.AccesoDatos.HistoricoDemanda;
using Diverscan.MJP.Entidades.HistoricoDemanda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.HistoricoDemanda
{
    public class n_SolicitudRestaurantes
    {
        //public static List<SolicitudRestaurantesRecord> ObtenerSolicitudRestaurantes(DateTime fechaInicio, DateTime fechaFin, long idArticulo)
        public static List<SolicitudRestaurantesRecord> ObtenerSolicitudRestaurantes(DateTime fechaSeleccionada, string idInterno)
        {
            SolicitudRestaurantesDBA solicitudRestaurantesDBA = new SolicitudRestaurantesDBA();
            return solicitudRestaurantesDBA.ObtenerSolicitudRestaurantes(fechaSeleccionada, idInterno);
        }
    }
}
