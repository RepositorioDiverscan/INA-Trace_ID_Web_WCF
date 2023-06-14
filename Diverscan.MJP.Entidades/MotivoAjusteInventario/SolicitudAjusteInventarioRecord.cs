using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [DataContract]
    public class SolicitudAjusteInventarioRecord
    {
        long _idSolicitudAjusteInventario;
        int _idUsuario;  
        long _idAjusteInventario;
        DateTime _fechaSolicitud;
        int _estado;
        DateTime _fechaAprobado;
        string _fechaSolicitudAndroid;
        string _fechaAprobadoAndroid;
        long _idSolicitudAjusteInventarioRef;
        public SolicitudAjusteInventarioRecord(long idSolicitudAjusteInventario, int idUsuario, long idAjusteInventario,
        DateTime fechaSolicitud, int estado, DateTime fechaAprobado)
        {
            _idSolicitudAjusteInventario = idSolicitudAjusteInventario;
            _idUsuario= idUsuario;          
            _idAjusteInventario = idAjusteInventario;         
            _fechaSolicitud = fechaSolicitud;
            _estado = estado;
            _fechaAprobado = fechaAprobado;         
        }

        public SolicitudAjusteInventarioRecord(int idUsuario, long idAjusteInventario, int estado)
        {         
            _idUsuario = idUsuario;         
            _idAjusteInventario = idAjusteInventario;
            _estado = estado;           
        }
        [DataMember]
        public long IdSolicitudAjusteInventario 
        {
            get { return _idSolicitudAjusteInventario; }
            set { _idSolicitudAjusteInventario = value; }
        }
        [DataMember]
        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        
        [DataMember]
        public long IdAjusteInventario
        {
            get { return _idAjusteInventario; }
            set { _idAjusteInventario = value; }
        }
        
        
        public DateTime FechaSolicitud
        {
            get { return _fechaSolicitud; }
            set { _fechaSolicitud = value; }
        }

        [DataMember]
        public int Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        
        public DateTime FechaAprobado
        {
            get { return _fechaAprobado; }
            set { _fechaAprobado = value; }
        }

        [DataMember]
        public string FechaSolicitudAndroid {
            get {
                if(FechaSolicitud == null) {
                    return "";
                }                  
                return FechaSolicitud.ToShortDateString();
            }
            set {
                _fechaSolicitudAndroid = value;
                if (!String.IsNullOrEmpty(_fechaSolicitudAndroid))
                {
                    FechaSolicitud = DateTime.ParseExact(_fechaSolicitudAndroid, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        [DataMember]
        public string FechaAprobadoAndroid
        {
            get
            {
                if (FechaAprobado == null)
                {
                    return "";
                }
                return FechaAprobado.ToShortDateString();
            }
            set
            {
                _fechaAprobadoAndroid = value;
                if (!String.IsNullOrEmpty(_fechaAprobadoAndroid))
                {
                    FechaAprobado = DateTime.ParseExact(_fechaAprobadoAndroid, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }
        [DataMember]
        public long IdSolicitudAjusteInventarioRef
        {
            get { return _idSolicitudAjusteInventarioRef; }
            set { _idSolicitudAjusteInventarioRef = value; }
        }
    }
}
