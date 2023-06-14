using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.AccesoDatos.Certificación
{
    public class N_Certificacion
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private d_Certificacion _d_Certificacion;

        public N_Certificacion(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _d_Certificacion = new d_Certificacion();
        }

        public List<E_CertificacionDetalle> GetDetalleCertificacion(int idBodega, int idMaestroSolicitud)
        {
            try
            {
                return _d_Certificacion.ObtenerDetalleCertificacion(idBodega, idMaestroSolicitud);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.CERTIFICATIONFILEPATHEXCEPTION);
                return null;
            }
        }

        public string CertificarLineaSSCC(string consecutivoSSCC, int idUsuario, List<EDetalleSSCCOla> detalleSSCCOlaLista)
        {
            try
            {
                string result = "";

                //List<string> pendientes = new List<string>();
                
                foreach (EDetalleSSCCOla temp in detalleSSCCOlaLista)
                {
                     _d_Certificacion.CertificarLineaSSCC(consecutivoSSCC, temp);
                }
                
                result = _d_Certificacion.CertificarSSCC(consecutivoSSCC,idUsuario);

                return result;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.CERTIFICATIONFILEPATHEXCEPTION);
                return ex.Message;
            }
        }

        public List<e_OPESALMaestroSolicitud> GetOrdersToCertificated(
          int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder)//
        {          
            try
            {              
              return _d_Certificacion.GetOrdersToCertificated(idInternoWarehouse, dateInit, dateEnd, idInternoOrder);      
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.CERTIFICATIONFILEPATHEXCEPTION);
                return new List<e_OPESALMaestroSolicitud>();
            }
        }

        public string CertificarOla(long idMaestroSolicitud, long idusuario)
        {
            try
            {
               return _d_Certificacion.CertificarOla(idMaestroSolicitud,idusuario);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.CERTIFICATIONFILEPATHEXCEPTION);
                return "";
            }
        }
    }
}
