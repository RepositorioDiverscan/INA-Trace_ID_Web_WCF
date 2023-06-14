using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
    [Serializable]
    public class Entidad_DetallePedidoTienda
    {
        private string _idMaestroSolicitud;

        private string _Destino;

        private string _Referencia;

        private string _Nombre;

        private double _Cantidad;

        private string _FechaCreacion;

        private string _FechaProcesamiento;

        public Entidad_DetallePedidoTienda(DataRow dataRow)
        {
            _idMaestroSolicitud = dataRow["idMaestroSolicitud"].ToString();
            _Destino = dataRow["Destino"].ToString();
            _Referencia = dataRow["Referencia"].ToString();
            _Nombre = dataRow["Nombre"].ToString();
            _Cantidad = Convert.ToDouble(dataRow["Cantidad"].ToString().Replace(".",","));
            _FechaCreacion = dataRow["FechaCreacion"].ToString();
            _FechaProcesamiento = dataRow["FechaProcesamiento"].ToString();
           
        }

        public string idMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public string Destino
        {
            get { return _Destino; }
            set { _Destino = value; }
        }

        public string Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public double Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string FechaProcesamiento
        {
            get { return _FechaProcesamiento; }
            set { _FechaProcesamiento = value; }
        }

    }
}
