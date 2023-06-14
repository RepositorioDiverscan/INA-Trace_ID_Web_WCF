using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [Serializable]
    public class DetalleSolicitudAjusteInventarioRecord
    {
        long _idSolicitudAjusteInventario;
        DateTime _fechaSolicitud;
        string _nombre;
        string _apellidos;
        long _idUbicacion;
        string _ubicacion;
        long _idArticulo;
        string _articulo;
        int _cantidadSolicita;
        int _cantidadExistente;
        string _motivoInventario;
        string _tipoMotivo;
        DateTime _fechaVencimiento;
        string _lote;

        public DetalleSolicitudAjusteInventarioRecord(long idSolicitudAjusteInventario, DateTime fechaSolicitud, string nombre,
        string apellidos,long idUbicacion, string ubicacion,long idArticulo, string articulo, int cantidadSolicita, int cantidadExistente, 
            string motivoInventario, bool tipoMotivo, DateTime fechaVencimiento,string lote)
        {
            _idSolicitudAjusteInventario = idSolicitudAjusteInventario;
            _fechaSolicitud = fechaSolicitud;
            _nombre = nombre;
            _apellidos = apellidos;
            _idUbicacion = idUbicacion;
            _ubicacion = ubicacion;
            _idArticulo = idArticulo;
            _articulo = articulo;
            _cantidadSolicita = cantidadSolicita;
            _cantidadExistente = cantidadExistente;
            _motivoInventario = motivoInventario;
            _fechaVencimiento = fechaVencimiento;
            _lote = lote;
            if (tipoMotivo)
                _tipoMotivo = "Entrada";
            else
                _tipoMotivo = "Salida";
        }

        public long IdSolicitudAjusteInventario
        {
            get { return _idSolicitudAjusteInventario; }
        }
        public DateTime FechaSolicitud
        {
            get { return _fechaSolicitud; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }
        public string Apellidos
        {
            get { return _apellidos; }
        }
        public long IdUbicacion
        {
            get { return _idUbicacion; }
        }
        public string Ubicacion
        {
            get { return _ubicacion; }
        }
        public long IdArticulo
        {
            get { return _idArticulo; }
        }
        public string Articulo
        {
            get { return _articulo; }
        }
        public int CantidadSolicita
        {
            get { return _cantidadSolicita; }
        }
        public int CantidadExistente
        {
            get { return _cantidadExistente; }
        }
        public string MotivoInventario
        {
            get { return _motivoInventario; }
        }

        public string TipoMotivo
        {
            get { return _tipoMotivo; }
        }

        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
        }
        public string Lote
        {
            get { return _lote; }
        }
    }
}
