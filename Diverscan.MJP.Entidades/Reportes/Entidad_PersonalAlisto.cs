using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{

    [Serializable]
    public class Entidad_PersonalAlisto
    { 

        private string  _Solicitud;

        private string _Destino;

        private double _CantidadAlistado;

        private double _CantidadPedido;

        private string _Referencia_Interno;

        private string _NombreArticulo;

        private string _SSCCAsociado;

        private double _CantidadUnidadAlisto;

        private string _Encargado;

        private string _Alistado;

        private string _Suspendido;

        private string _FechaCreacion;

        private string _FechaRegistro;

        private string _FechaAsignacion;


        public Entidad_PersonalAlisto(DataRow dataRow) 
        {
            _Solicitud = dataRow["Solicitud"].ToString();
            _Destino = dataRow["Destino"].ToString();
            _CantidadAlistado = Convert.ToDouble(dataRow["CantidadAlistado"].ToString().Replace(",","."));
            _CantidadPedido = Convert.ToDouble(dataRow["CantidadPedido"].ToString().Replace(",", "."));
            _Referencia_Interno = dataRow["Referencia_Interno"].ToString();
            _NombreArticulo = dataRow["NombreArticulo"].ToString();
            _SSCCAsociado = dataRow["SSCCAsociado"].ToString();
            _CantidadUnidadAlisto = Convert.ToDouble(dataRow["CantidadUnidadAlisto"].ToString().Replace(",", "."));
            _Encargado = dataRow["Encargado"].ToString();
            _Alistado = dataRow["Alistado"].ToString();
            _Suspendido = dataRow["Suspendido"].ToString();
            _FechaCreacion = dataRow["FechaCreacion"].ToString();
            _FechaRegistro = dataRow["FechaRegistro"].ToString();
            _FechaAsignacion = dataRow["FechaAsignacion"].ToString();
        }
        public string Solicitud
        {
            get { return _Solicitud; }
            set { _Solicitud = value; }
        }

        public string Destino
        {
            get { return _Destino; }
            set { _Destino = value; }
        }

        public double CantidadAlistado
        {
            get { return _CantidadAlistado; }
            set { _CantidadAlistado = value; }
        }

        public double CantidadPedido
        {
            get { return _CantidadPedido; }
            set { _CantidadPedido = value; }
        }

        public string Referencia_Interno
        {
            get { return _Referencia_Interno; }
            set { _Referencia_Interno = value; }
        }

        public string NombreArticulo
        {
            get { return _NombreArticulo; }
            set { _NombreArticulo = value; }
        }

        public string SSCCAsociado
        {
            get { return _SSCCAsociado; }
            set { _SSCCAsociado = value; }
        }

        public double CantidadUnidadAlisto
        {
            get { return _CantidadUnidadAlisto; }
            set { _CantidadUnidadAlisto = value; }
        }

        public string Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        public string Alistado
        {
            get { return _Alistado; }
            set { _Alistado = value; }
        }

        public string Suspendido
        {
            get { return _Suspendido; }
            set { _Suspendido = value; }
        }

        public string FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string FechaRegistro
      
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public string FechaAsignacion
        {
            get { return _FechaAsignacion; }
            set { _FechaAsignacion = value; }
        }

    }
}
