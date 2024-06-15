using Diverscan.MJP.AccesoDatos.Devolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Devolutions
{
    public class n_RecibirSolicitudDevolucion
    {
        private da_RecibirSolicitudDevolucion Da_RecibirSolicitudDevolucion = new da_RecibirSolicitudDevolucion();

        public string ObtenerSolicitudesDevolucion(EDetalleRecibirSolicitudDevolucion detalle, int usuario)
        {
            try
            {
                return Da_RecibirSolicitudDevolucion.ObtenerSolicitudesDevolucion(detalle, usuario);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
