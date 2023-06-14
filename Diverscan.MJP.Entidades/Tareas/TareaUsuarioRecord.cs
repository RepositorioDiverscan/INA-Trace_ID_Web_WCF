using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Tareas
{
    public class TareaUsuarioRecord
    {
        //long _idMaestroSolicitud 
        long _idLineaDetalleSolicitud;
        long _idBodega;
        int _idUsuario;
        //public TareaUsuarioRecord(long idMaestroSolicitud, long idBodega, int idUsuario)
        public TareaUsuarioRecord(long idLineaDetalleSolicitud, long idBodega, int idUsuario)
        {
            //_idMaestroSolicitud = idMaestroSolicitud;
            _idLineaDetalleSolicitud = idLineaDetalleSolicitud;
            _idBodega = idBodega;
            _idUsuario = idUsuario;
        }
        /*public long IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
        }*/

        public long IdDetalleSolicitud
        {
            get { return _idLineaDetalleSolicitud; }
        }
        public long IdBodega
        {
            get { return _idBodega; }
        }
        public int IdUsuario
        {
            get { return _idUsuario; }
        }
    }
}
