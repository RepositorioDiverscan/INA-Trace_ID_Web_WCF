using Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionInmediata;
using Diverscan.MJP.Entidades.Recepcion;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Recepcion
{
    public class n_DevolucionInmediata
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private da_DevolucionInmediata Da_DevolucionInmediata;
        public n_DevolucionInmediata(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            Da_DevolucionInmediata = new da_DevolucionInmediata();
        }

        public ResultadoObtenerDevolucionInmediata ObtenerDevolucionInmediata(string idbodega, string idCausa)
        {
            ResultadoObtenerDevolucionInmediata resultado = new ResultadoObtenerDevolucionInmediata();
            try
            {
                resultado.devoluciones = Da_DevolucionInmediata.ObtenerDevolucionInmediata(idbodega, idCausa);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;
        }

        public string RecibirDevolucionInmediata(e_RecepcionDevolucion e_Recepcion, string idEstadoDevolucion)
        {
            string resultado;
            try
            {
                resultado = Da_DevolucionInmediata.RecibirDevolucionInmediata(e_Recepcion, idEstadoDevolucion);

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado = ex.Message;

            }
            return resultado;
        }
    }
}
