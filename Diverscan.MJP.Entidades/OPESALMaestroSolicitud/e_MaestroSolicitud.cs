using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]
    public class e_MaestroSolicitud
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

        public e_MaestroSolicitud(IDataReader reader)
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
            _prioridadString = reader["PrioridadDescripcion"].ToString();
            _fechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
            _porcentajeAlisto = reader["PorcentajeAlistado"].ToString();

        }


        public e_MaestroSolicitud(long idMaestroSolicitud, string UsuarioNombre, DateTime FechaCreacion, string Nombre, string Comentarios,
            string PorcentjaAlisto,
            string IdCompania, string DestinoNombre, string idInterno, string idInternoSAP, int prioridad, DateTime fechaEntrega)
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
            _porcentajeAlisto = PorcentjaAlisto;

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
            get { return _porcentajeAlisto; }
            set { _porcentajeAlisto = value; }
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

        public int IdBodega
        {
            get => _idBodega;
            set => _idBodega = value;
        }

        public string NombreBodega
        {
            get => _nombreBodega;
            set => _nombreBodega = value;
        }

        public int IdDestino
        {
            get => _idDestino;
            set => _idDestino = value;
        }

        public int Prioridad
        {
            get => _prioridad;
            set => _prioridad = value;
        }

        public DateTime FechaEntrega
        {
            get => _fechaEntrega;
            set => _fechaEntrega = value;
        }

        public string PrioridadString
        {
            get => _prioridadString;
            set => _prioridadString = value;
        }
    }
}
