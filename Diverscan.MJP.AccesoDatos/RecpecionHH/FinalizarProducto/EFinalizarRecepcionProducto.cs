using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH.FinalizarProducto
{
    [DataContract]
    public class EFinalizarRecepcionProducto
    {
        private long _idDetalleOrdenCompra;
        private string _numFactura;

        public EFinalizarRecepcionProducto(long idDetalleOrdenCompra, string numFactura)
        {
            IdDetalleOrdenCompra = idDetalleOrdenCompra;
            NumFactura = numFactura;
        }
        [DataMember]
        public long IdDetalleOrdenCompra { get => _idDetalleOrdenCompra; set => _idDetalleOrdenCompra = value; }
        [DataMember]
        public string NumFactura { get => _numFactura; set => _numFactura = value; }
    }
}
