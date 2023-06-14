using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionProducto
{
    [DataContract]
    public class EDevolcionProducto
    {
        long _idDetalleOrdenCompra;
        string _lote;
        string _fechaVencimiento;
        decimal _cantidad;

        [DataMember]
        public long IdDetalleOrdenCompra { get => _idDetalleOrdenCompra; set => _idDetalleOrdenCompra = value; }
        [DataMember]
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        [DataMember]
        public string Lote { get => _lote; set => _lote = value; }
        [DataMember]
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
    }
}
