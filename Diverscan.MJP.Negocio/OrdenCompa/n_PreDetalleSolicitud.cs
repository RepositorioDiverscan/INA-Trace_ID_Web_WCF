using Diverscan.MJP.AccesoDatos.OrdenCompa;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OrdenCompa
{
    public class n_PreDetalleSolicitud
    {
        public List<e_PreDetalleSolicitud> GetPreDetalleSolicitud(long idMaestroSolicitud, int idBodega)
        {
            da_PreDetalleSolicitud  da_PreDetalleSolicitud  = new  da_PreDetalleSolicitud();
            return da_PreDetalleSolicitud.GetPreDetalleSolicitud(idMaestroSolicitud,idBodega);
        }

        public string ProcesarPreDetalleSolicitud(List<e_PreDetalleSolicitud> listPreDetalleSolicitud)
        {
            da_PreDetalleSolicitud da_preDetalleSolicitud = new da_PreDetalleSolicitud();
            string result = da_preDetalleSolicitud.ProcesarPreDetalleSolicitud(listPreDetalleSolicitud);
            return result;
        }

        public List<e_OPESALDetalleSolicitud> GetDetalleSolicitudParaAporbarSalida(long idMaestroSolicitud, int idBodega) 
        {
            da_PreDetalleSolicitud da_PreDetalleSolicitud = new da_PreDetalleSolicitud();
            return da_PreDetalleSolicitud.GetDetalleSolicitudParaAporbarSalida(idMaestroSolicitud, idBodega);
        }
    }
}
