using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    [Serializable]
    [DataContract]
    public class e_EncabezadoSolicitudDevolucion
    {
        private int _IdUsuario;
        private int _IdTransportista;
        private int _IdBodega;
        private List<e_DetalleSolicitudDevolucion> _DetallesSolicitud;
        private long _IdSolicitudDevolucion;
        private string _NombreUsuario;
        private string _FechaSolicitud;

        public e_EncabezadoSolicitudDevolucion(int idUsuario, int idTransportista, int idBodega)
        {
            _IdUsuario = idUsuario;
            _IdTransportista = idTransportista;
            _IdBodega = idBodega;
        }

        public e_EncabezadoSolicitudDevolucion(long idSolicitudDevolucion, string nombreUsuario, string fechaSolicitud)
        {
            _IdSolicitudDevolucion = idSolicitudDevolucion;
            _NombreUsuario = nombreUsuario;
            _FechaSolicitud = fechaSolicitud;
        }

        [DataMember]
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        [DataMember]
        public int IdTransportista { get => _IdTransportista; set => _IdTransportista = value; }
        [DataMember]
        public int IdBodega { get => _IdBodega; set => _IdBodega = value; }
        [DataMember]
        public List<e_DetalleSolicitudDevolucion> DetallesSolicitud { get => _DetallesSolicitud; set => _DetallesSolicitud = value; }
        [DataMember]
        public long IdSolicitudDevolucion { get => _IdSolicitudDevolucion; set => _IdSolicitudDevolucion = value; }
        [DataMember]
        public string NombreUsuario { get => _NombreUsuario; set => _NombreUsuario = value; }
        [DataMember]
        public string FechaSolicitud { get => _FechaSolicitud; set => _FechaSolicitud = value; }
    }
}
