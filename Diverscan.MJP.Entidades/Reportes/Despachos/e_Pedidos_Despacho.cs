using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Despachos
{
    [Serializable]
    public class e_Pedidos_Despacho
    {
        private long _numeroSolicituTID;
        private string _numeroSolicitudERP;
        private string _idInternoSolicitud;
        private long _idDestinoPedido;
        private string _nombreDestino;
        private string _estadoSolicitud;
        private DateTime _fechaPedido;


        public e_Pedidos_Despacho(){}

        public e_Pedidos_Despacho(long numeroSolicituTID, string numeroSolicitudERP, string idInternoSolicitud, long idDestinoPedido, string nombreDestino, string estadoSolicitud, DateTime fechaPedido)
        {
            _numeroSolicituTID = numeroSolicituTID;
            _numeroSolicitudERP = numeroSolicitudERP;
            _idInternoSolicitud = idInternoSolicitud;
            _idDestinoPedido = idDestinoPedido;
            _nombreDestino = nombreDestino;
            _estadoSolicitud = estadoSolicitud;
            _fechaPedido = fechaPedido;
        }

        public long NumeroSolicituTID
        {
            get { return _numeroSolicituTID; }
            set { _numeroSolicituTID = value; }
        }

        public string NumeroSolicitudERP
        {
            get { return _numeroSolicitudERP; }
            set { _numeroSolicitudERP = value; }
        }

        public string IdInternoSolicitud
        {
            get { return _idInternoSolicitud; }
            set { _idInternoSolicitud = value; }
        }

        public long IdDestinoPedido
        {
            get { return _idDestinoPedido; }
            set { _idDestinoPedido = value; }
        }

        public string NombreDestino
        {
            get { return _nombreDestino; }
            set { _nombreDestino = value; }
        }

        public string EstadoSolicitud
        {
            get { return _estadoSolicitud; }
            set { _estadoSolicitud = value; }
        }

        public DateTime FechaPedido
        {
            get { return _fechaPedido; }
            set { _fechaPedido = value; }
        }
        
        public string FechaPedidoExport
        {
            get { return _fechaPedido.ToShortDateString(); }
        }
    }
}
