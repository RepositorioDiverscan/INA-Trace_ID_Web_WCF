using Diverscan.MJP.AccesoDatos.OPESALMaestroSolicitud;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OPESALMaestroSolicitud
{
    public class OPESALMaestroSolicitud
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private da_OPESALMaestroSolicitud _dOPESALMaestroSolicitud;

        public OPESALMaestroSolicitud(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dOPESALMaestroSolicitud = new da_OPESALMaestroSolicitud();
        }

        public static List<e_OPESALMaestroSolicitud> GetListOPESALMaestroSolicitud(string prefix, string IdCompania)
        {
            da_OPESALMaestroSolicitud da_oPESALMaestroSolicitud = new da_OPESALMaestroSolicitud();
            return da_oPESALMaestroSolicitud.GetListOPESALMaestroSolicitud(prefix, IdCompania);
        }

        public static List<e_OPESALMaestroSolicitud> GetMaestroArticulos(string IdCompania)
        {
            da_OPESALMaestroSolicitud da_oPESALMaestroSolicitud = new da_OPESALMaestroSolicitud();
            return da_oPESALMaestroSolicitud.GetMaestroArticulos(IdCompania);
        }

        public List<e_OPESALMaestroSolicitud> GetOrdersToEnlist(
           int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder) //
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetOrdersToEnlist(idInternoWarehouse, dateInit, dateEnd, idInternoOrder);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<e_OPESALMaestroSolicitud> GetOrders(
        int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder) //
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetOrders(idInternoWarehouse, dateInit, dateEnd, idInternoOrder);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EDetalleSalidaArticuloSector> GetDetalleSalidaArticulosSector(int idBodega, int idMaestroSolicitud)
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetDetalleSalidaArticulosSector(idBodega, idMaestroSolicitud);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public string InsertarTareaAlistador(int idLineaDetalleSolicitud, int idUsuario,int idPrioridad)
        {

            try
            {
                return _dOPESALMaestroSolicitud.InsertarTareaAlistador(idLineaDetalleSolicitud, idUsuario,idPrioridad);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }

        }

        public string InsertarTareasAlistador(List<long> listaIdLineaDetalleSolicitud, int idUsuario, int idPrioridad)
        {

            try
            {
                return _dOPESALMaestroSolicitud.InsertarTareasAlistador(listaIdLineaDetalleSolicitud, idUsuario, idPrioridad);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }

        }

        public string ActualizarTareaAlistador(int idLineaDetalleSolicitud, int idTareasUsuario)
        {
            try
            {
                return _dOPESALMaestroSolicitud.ActualizarTareaAlistador(idLineaDetalleSolicitud, idTareasUsuario);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<ETareasUsuarioSolicitud> GetTareasPendientesPorUsuario(int idUsuario)
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetTareasPendientesPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EPrioridadOrden> GetPrioridadOrden()
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetPrioridadOrden();
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EDetalleSalidaOrdenUsuario> GetDetalleSalidaOrdenUsuario(int idUsuario, int idMaestroSalida)
        {
            try
            {
                return _dOPESALMaestroSolicitud.GetDetalleSalidaOrdenUsuario(idUsuario, idMaestroSalida);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ORDERTOENLISTFILEPATHEXCEPTION);
                return null;
            }
        }

    }
}
