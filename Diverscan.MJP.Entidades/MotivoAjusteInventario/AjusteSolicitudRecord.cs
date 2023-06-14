using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [Serializable]
    public class AjusteSolicitudRecord
    {
        long _idSolicitudAjusteInventario;
        DateTime _fechaSolicitud;
        string _nombre;
        string _apellidos;              
        string _motivoInventario;
        string _tipoMotivo;
        string _centroCosto;

        public AjusteSolicitudRecord()
        {
        }

        public AjusteSolicitudRecord(long idSolicitudAjusteInventario, DateTime fechaSolicitud, string nombre,
        string apellidos, string motivoInventario, bool tipoMotivo, string centroCosto)
        {
            _idSolicitudAjusteInventario = idSolicitudAjusteInventario;
            _fechaSolicitud = fechaSolicitud;
            _nombre = nombre;
            _apellidos = apellidos; 
            _motivoInventario = motivoInventario;
            if (tipoMotivo)
                _tipoMotivo = "Entrada";
            else
                _tipoMotivo = "Salida";
            _centroCosto = centroCosto;
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
        public string MotivoInventario
        {
            get { return _motivoInventario; }
        }
        public string TipoMotivo
        {
            get { return _tipoMotivo; }
        }

        public string CentroCosto
        {
            get { return _centroCosto; }
        }      
    }
}
