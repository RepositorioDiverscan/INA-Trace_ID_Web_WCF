using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.OrdenCompra
{
    [DataContract]
    public class EDetailPurchaseOrder
    {

        private string _idMaestroOrdenCompra;
        private string _idDetalleOrdenCompra;
        private string _idArticulo;
        private string _GTIN;
        private string _comentario;
        private string _nombreHH;
        private string _minDiasVencimiento;
        private string _cantidadRecibir;
        private string _cantidadRecibida;
        private string _cantidadRechazada;
        private string _cantidadPendiente;
        private bool _conTrazabilidad;
        private string _numLinea;
        private string _transito;
        private string _idInterno;
        private string _numeroFactura;

        public EDetailPurchaseOrder()
        {

        }

        public EDetailPurchaseOrder(string idMaestroOrdenCompra, string fechaDespacho, string comentario,
            string nombreHH, string cantidadRecibir, string cantidadRecibida, string cantidadRechazada, string cantidadPendiente,
            string transito, string numFactura)
        {
            this._idMaestroOrdenCompra = idMaestroOrdenCompra;
            this._idArticulo = fechaDespacho;
            this._comentario = comentario;
            this._nombreHH = nombreHH;
            this._cantidadRecibir = cantidadRecibir;
            this._cantidadRecibida = cantidadRecibida;
            this._cantidadRechazada = cantidadRechazada;
            this._cantidadPendiente = cantidadPendiente;
            this._transito = transito;
            this.NumeroFactura = numFactura;
        }

        public EDetailPurchaseOrder(IDataReader reader)
        {
            this._idDetalleOrdenCompra = reader["idDetalleIngreso"].ToString();
            this._idMaestroOrdenCompra = reader["idMaestroIngreso"].ToString();
            this._idArticulo = reader["idArticulo"].ToString();
            this._GTIN = reader["GTIN"].ToString();
            this._comentario = reader["Comentario"].ToString(); 
            this._nombreHH = reader["NombreHH"].ToString();  
            this._cantidadRecibir = reader["CantidadxRecibir"].ToString();
            this._minDiasVencimiento = reader["DiasMinimosVencimiento"].ToString();
            this._conTrazabilidad = bool.Parse(reader["ConTrazabilidad"].ToString());
            this._cantidadRecibida = reader["CantidadRecibidad"].ToString();
            this._cantidadRechazada = reader["CantidadRechazada"].ToString();
            this._numLinea = reader["numLinea"].ToString();
            this._cantidadPendiente = reader["CantidadPendiente"].ToString();
            this._transito = reader["Transito"].ToString();
            this._idInterno = reader["idInterno"].ToString();
            this.NumeroFactura = reader["numFactura"].ToString();
        }

        [DataMember]
        public string IdMaestroOrdenCompra { get => _idMaestroOrdenCompra; set => _idMaestroOrdenCompra = value; }

        [DataMember]
        public string IdArticulo { get => _idArticulo; set => _idArticulo = value; }

        [DataMember]
        public string Comentario { get => _comentario; set => _comentario = value; }

        [DataMember]
        public string NombreProducto { get => _nombreHH; set => _nombreHH = value; }

        [DataMember]
        public string CantidadRecibir { get => _cantidadRecibir; set => _cantidadRecibir = value; }

        [DataMember]
        public string CantidadRecibida { get => _cantidadRecibida; set => _cantidadRecibida = value; }

        [DataMember]
        public string CantidadRechazada { get => _cantidadRechazada; set => _cantidadRechazada = value; }

        [DataMember]
        public string CantidadPendiente { get => _cantidadPendiente; set => _cantidadPendiente = value; }

        [DataMember]
        public string IdDetalleORderCompra { get => _idDetalleOrdenCompra; set => _idDetalleOrdenCompra = value; }

        [DataMember]
        public bool ConTrazabilidad { get => _conTrazabilidad; set => _conTrazabilidad = value; }

        [DataMember]
        public string MinDiasVencimiento { get => _minDiasVencimiento; set => _minDiasVencimiento = value; }

        [DataMember]
        public string GTIN { get => _GTIN; set => _GTIN = value; }
        [DataMember]
        public string NumLinea { get => _numLinea; set => _numLinea = value; }

        [DataMember]
        public string Transito { get => _transito; set => _transito = value; }

        [DataMember]
        public string IdInterno { get => _idInterno; set => _idInterno = value; }

        [DataMember]
        public string NumeroFactura { get => _numeroFactura; set => _numeroFactura = value; }
    }
}
