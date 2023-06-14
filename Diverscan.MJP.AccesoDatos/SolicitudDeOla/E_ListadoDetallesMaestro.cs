using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    public class E_ListadoDetallesMaestro
    {
        public E_ListadoDetallesMaestro(int idPreLineaDetalleSolicitud, string nombre, string idInterno, decimal cantidad)
        {
            this.idPreLineaDetalleSolicitud = idPreLineaDetalleSolicitud;
            Nombre = nombre;
            this.idInterno = idInterno;
            Cantidad = cantidad;
        }

        public int idPreLineaDetalleSolicitud { get; set; }
        public string Nombre { get; set; }
        public string idInterno { get; set; }
        public decimal Cantidad { get; set; }

    }
}
