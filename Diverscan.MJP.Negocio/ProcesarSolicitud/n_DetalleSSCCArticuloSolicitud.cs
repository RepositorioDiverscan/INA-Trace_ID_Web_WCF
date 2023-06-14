using Diverscan.MJP.AccesoDatos.ProcesarSolicitud;
using Diverscan.MJP.Entidades.ProcesarSolicitud;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.ProcesarSolicitud
{
    public class n_DetalleSSCCArticuloSolicitud
    {
        da_DetalleSSCCArticuloSolicitud _da_DetalleSSCCArticuloSolicitud = new da_DetalleSSCCArticuloSolicitud();

        public List<e_SSCCSolicitud> ObtenerSSCCNoDespachadosSolicitud(string SSCCGenerado)
        {
            return _da_DetalleSSCCArticuloSolicitud.ObtenerSSCCNoDespachadosSolicitud(SSCCGenerado);
        }

        public List<e_ArticulosPendientesSolicitud> ObtenerArticulosPendientesSolicitud(string SSCCGenerado)
        {
            return _da_DetalleSSCCArticuloSolicitud.ObtenerArticulosPendientesSolicitud(SSCCGenerado);
        }

        public string ObtenerDestinoPorSSCCGenerado(string SSCCGenerado)
        {
            return _da_DetalleSSCCArticuloSolicitud.ObtenerDestinoPorSSCCGenerado(SSCCGenerado);
        }
    }
}
