using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_AprobarDespacho
    {
        int _idMaestroSolicitud;
        int _idUsuario;
        DateTime _FechaCreacion;
        string _Nombre;
        string _Comentarios;
        string _IdCompania;
        int _idDestino;
        string _idInterno;
        string _idInternoSAP;
        bool _Procesada;
        DateTime _FechaProcesamiento;
        bool _Activo;
        bool _Sincronizado;
        int _idBodega;
        int _Prioridad;
        DateTime _FechaEntrega;

        public e_AprobarDespacho(int idMaestroSolicitud, int idUsuario, DateTime FechaCreacion, string Nombre, string Comentarios, string IdCompania, int idDestino,
            string idInterno, string idInternoSAP, bool Procesada, DateTime FechaProcesamiento, bool Activo, bool Sincronizado, int idBodega, int Prioridad,
            DateTime FechaEntrega)
        {
            _idMaestroSolicitud = idMaestroSolicitud;
            _idUsuario = idUsuario;
            _FechaCreacion = FechaCreacion;
            _Nombre = Nombre;
            _Comentarios = Comentarios;
            _IdCompania = IdCompania;
            _idDestino = idDestino;
            _idInterno = idInterno;
            _idInternoSAP = idInternoSAP;
            _Procesada = Procesada;
            _FechaProcesamiento = FechaProcesamiento;
            _Activo = Activo;
            _Sincronizado = Sincronizado;
            _idBodega = idBodega;
            _Prioridad = Prioridad;
            _FechaEntrega = FechaEntrega;
        }

        public int IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public int idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
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

        public int idDestino
        {
            get { return _idDestino; }
            set { _idDestino = value; }
        }

        public string idInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }

        public string idInternoSAP
        {
            get { return _idInternoSAP; }
            set { _idInternoSAP = value; }
        }

        public bool Procesada
        {
            get { return _Procesada; }
            set { _Procesada = value; }
        }

        public DateTime FechaProcesamiento
        {
            get { return _FechaProcesamiento; }
            set { _FechaProcesamiento = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public bool Sincronizado
        {
            get { return _Sincronizado; }
            set { _Sincronizado = value; }
        }

        public int idBodega
        {
            get { return _idBodega; }
            set { _idBodega = value; }
        }

        public int Prioridad
        {
            get { return _Prioridad; }
            set { _Prioridad = value; }
        }

        public DateTime FechaEntrega
        {
            get { return _FechaEntrega; }
            set { _FechaEntrega = value; }
        }
    }
}
