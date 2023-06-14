using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AjusteInventario
{
    public class N_SolicitudAjusteInventario
    {    
        public static long InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            return solicitudAjusteInventario_DBA.InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(solicitudAjusteInventarioRecord, articuloXSolicitudAjusteRecord);
        }

        public static string InsertarSolicitudAjusteInventarioYObtenerIdSolicitudHH(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            return solicitudAjusteInventario_DBA.InsertarSolicitudAjusteInventarioYObtenerIdSolicitudHH                                                                         (solicitudAjusteInventarioRecord, articuloXSolicitudAjusteRecord);
        }            

        public static List<AjusteSolicitudRecord> GetSolicitudAjusteInventario( 
            DateTime fechaInicio, DateTime fechaFin, int estado, int idBodega)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            return solicitudAjusteInventario_DBA.GetSolicitudAjusteInventario(fechaInicio, fechaFin, estado, idBodega);
        }

        public static List<AjusteSolicitudRecord> GetSolicitudAjusteInventarioPorID(
            long idSolicitudAjusteInventario, int idBodega)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            return solicitudAjusteInventario_DBA.GetSolicitudAjusteInventarioPorID(
                idSolicitudAjusteInventario,idBodega);
        }

        public static void UpdateSolicitudAjusteInventario(
            long idSolicitudAjusteInventario, int estado, long idCentroCosto, long idUsuario)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            solicitudAjusteInventario_DBA.UpdateSolicitudAjusteInventario(
                idSolicitudAjusteInventario, estado, idCentroCosto, idUsuario);
        }

        public static int GetIdSolicitudAjusteRefencia(int idSolicitudAjusteRefenrencia)
        {
            SolicitudAjusteInventario_DBA solicitudAjusteInventario_DBA = new SolicitudAjusteInventario_DBA();
            return solicitudAjusteInventario_DBA.GetIdSolicitudAjusteRefencia(idSolicitudAjusteRefenrencia);
        }
    }
}
