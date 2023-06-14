using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]
    public class e_OPESALMaestroSolicitud
    {
        long _idMaestroSolicitud;
        string _UsuarioNombre;
        DateTime _FechaCreacion;
        string _Nombre;
        string _Comentarios;
        string _IdCompania;
        string _DestinoNombre;
        string _idInterno;
        string _idInternoSAP;
        int _idBodega;
        string _nombreBodega;
        int _idDestino;
        int _prioridad;
        DateTime _fechaEntrega;
        string _prioridadString;
        string _porcentajeAlisto;
        string _porcentajeAsignado;
        bool _certificado;
        int _cantidadSSCC;
        int _cantidadSSCCUbicados;
        string _SSCCUbicados;
        bool _impresionCertificacion;
        string _porcentajeSSCC;

        public e_OPESALMaestroSolicitud(IDataReader reader)
        {
            _idMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
            _FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
            _Nombre = reader["Nombre"].ToString();
            _Comentarios = reader["Comentarios"].ToString();
            _idDestino = Convert.ToInt32(reader["idDestino"].ToString());
            _DestinoNombre = reader["DestinoNombre"].ToString();
            _idInterno = reader["idInterno"].ToString();
            _idBodega = Convert.ToInt32(reader["idBodega"].ToString());
            _nombreBodega = reader["Bodega"].ToString();
            _prioridad = Convert.ToInt32(reader["Prioridad"].ToString());
            _prioridadString=reader["PrioridadDescripcion"].ToString(); 
            _fechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
            PorcentajeAlisto = reader["PorcentajeAlistado"].ToString();
            _porcentajeAsignado = reader["PorcentajeAsignado"].ToString();
            _cantidadSSCC = Convert.ToInt32(reader["totalSSCC"].ToString());
            _cantidadSSCCUbicados = Convert.ToInt32(reader["SSCCUbicados"].ToString());
            _certificado = Convert.ToBoolean(Convert.ToInt16(reader["certificado"].ToString()));
            _porcentajeSSCC = reader["PorcentajeSSCC"].ToString();
        }

        public e_OPESALMaestroSolicitud()
        {
        }

        public e_OPESALMaestroSolicitud(long idMaestroSolicitud,string UsuarioNombre,DateTime FechaCreacion,string Nombre,string Comentarios,
            
            string IdCompania,string DestinoNombre,string idInterno,string idInternoSAP,int prioridad,DateTime fechaEntrega,string PorcetajeAlisto)
        {
            _idMaestroSolicitud = idMaestroSolicitud;
            _UsuarioNombre = UsuarioNombre;
            _FechaCreacion = FechaCreacion;
            _Nombre = Nombre;
            _Comentarios = Comentarios;
            _IdCompania = IdCompania;
            _DestinoNombre = DestinoNombre;
            _idInterno = idInterno;
            _idInternoSAP = idInternoSAP;
            _prioridad = prioridad;
            FechaEntrega = fechaEntrega;
            PorcentajeAlisto = PorcetajeAlisto;

        }
  
        public long IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public string UsuarioNombre
        {
            get { return _UsuarioNombre; }
            set { _UsuarioNombre = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string PorcentajeAlistado
        {
            get { return PorcentajeAlisto; }
            set { PorcentajeAlisto = value; }
        }

        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        public string IdCompania
        {
            get { return _IdCompania; }
            set { _IdCompania = value; }
        }

        public string DestinoNombre
        {
            get { return _DestinoNombre; }
            set { _DestinoNombre = value; }
        }

        public string IdInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }

        public string IdInternoSAP
        {
            get { return _idInternoSAP; }
            set { _idInternoSAP = value; }
        }
        public string PorcentajeSSCC
        {
            get { return _porcentajeSSCC; }
            set { _porcentajeSSCC = value; }
        }
        public int IdBodega {
            get => _idBodega;
            set => _idBodega = value;
        }

        public string NombreBodega {
            get => _nombreBodega;
            set => _nombreBodega = value;
        }

        public int IdDestino {
            get => _idDestino;
            set => _idDestino = value;
        }

        public int Prioridad {
            get => _prioridad;
            set => _prioridad = value;
        }

        public DateTime FechaEntrega {
            get => _fechaEntrega;
            set => _fechaEntrega = value;
        }

        public string PrioridadString {
            get => _prioridadString;
            set => _prioridadString = value;
        }
        public string PorcentajeAsignado { get => _porcentajeAsignado; set => _porcentajeAsignado = value; }
        public bool Certificado { get => _certificado; set => _certificado = value; }
        public string PorcentajeAlisto { get => _porcentajeAlisto; set => _porcentajeAlisto = value; }
        public int CantidadSSCC { get => _cantidadSSCC; set => _cantidadSSCC = value; }
        public int CantidadSSCCUbciados { get => _cantidadSSCCUbicados; set => _cantidadSSCCUbicados = value; }
        public string SSCCUbicados { 
            get {
                    _SSCCUbicados = "" + _cantidadSSCCUbicados + " de " + _cantidadSSCC;
                    return _SSCCUbicados;
                } 
            set => _SSCCUbicados = value;
        }

        public bool ImpresionCertificacion { get => _impresionCertificacion; set => _impresionCertificacion = value; }
        //public string PorcentajeSSCC 
        //{ 
        //    get => _porcentajeSSCC; 
        //    set => _porcentajeSSCC = value; 
        //}
    }
}
