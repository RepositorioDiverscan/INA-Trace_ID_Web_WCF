using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced.PedidoFacturasTransportista
{
    [Serializable]
    public class EPedidoFacturasTransportista
    {
        private string _idSap;
        private DateTime _fechaCreacionPedido;
        private DateTime _fechaFactura;       
        private string _billNumber;
        private decimal _billPrice;  
        private bool _isEmo;

        public EPedidoFacturasTransportista(string idSap, DateTime fechaCreacionPedido, DateTime fechaFactura, string billNumber, decimal billPrice, bool isEmo)
        {
            _idSap = idSap;
            _fechaCreacionPedido = fechaCreacionPedido;
            _fechaFactura = fechaFactura;
            _billNumber = billNumber;
            _billPrice = billPrice;
            _isEmo = isEmo;
        }

        public EPedidoFacturasTransportista(IDataReader reader)
        {

            _idSap = reader["idInterno"].ToString();
            if (!String.IsNullOrEmpty(_idSap))
            {
                _fechaCreacionPedido = Convert.ToDateTime(reader["FechaCreacionPedido"].ToString());
                _fechaFactura = Convert.ToDateTime(reader["RecordDate"].ToString());
                _billNumber = reader["BillNumber"].ToString(); ;
                _billPrice = Convert.ToDecimal(reader["BillPrice"].ToString()); ;
                bool resultParse = false;
                if(Boolean.TryParse(reader["IsEmo"].ToString(), out resultParse))
                    _isEmo = resultParse;
            }
           
        }

        public string IdSap { get => _idSap; set => _idSap = value; }
        public DateTime FechaCreacionPedido { get => _fechaCreacionPedido; set => _fechaCreacionPedido = value; }
        public DateTime FechaFactura { get => _fechaFactura; set => _fechaFactura = value; }
        public string BillNumber { get => _billNumber; set => _billNumber = value; }
        public decimal BillPrice { get => _billPrice; set => _billPrice = value; }
        public bool IsEmo { get => _isEmo; set => _isEmo = value; }
    }
}
