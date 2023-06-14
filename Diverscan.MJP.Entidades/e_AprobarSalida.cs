using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_AprobarSalida
    {
        long _idMaestroSolicitud;
        string _solicitud;
        private readonly string _idBodega;
        string _bodega;
        string _destino;
        private readonly string _idInterno;
        string _fecha;

        public e_AprobarSalida(long idMaestroSolicitud, string solicitud, string idBodega, string bodega, string destino, string idInterno, string fecha) 
        {
            _idMaestroSolicitud = idMaestroSolicitud;
            _solicitud = solicitud;
            _idBodega = idBodega;
            _bodega = bodega;
            _destino = destino;
            _idInterno = idInterno;
            _fecha = fecha;
        }

        public long IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public string Solicitud
        {
            get { return _solicitud; }
            set { _solicitud = value; }
        }

        public string IdBodega
        {
            get { return _idBodega; }
        }

        public string Bodega
        {
            get { return _bodega; }
            set { _bodega = value; }
        }

        public string Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }

        public string IdInterno
        {
            get { return _idInterno; }
        }

        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
    }
}
