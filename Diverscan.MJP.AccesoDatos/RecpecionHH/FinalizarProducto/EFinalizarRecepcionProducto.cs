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
        private long _idDetalleIngreso;
        private string _numFactura;
        private short _tipoIngreso;

        public EFinalizarRecepcionProducto(long idDetalleIngreso, string numFactura, short tipoIngreso)
        {
            _idDetalleIngreso = idDetalleIngreso;
            _numFactura = numFactura;
            _tipoIngreso = tipoIngreso;
        }

        [DataMember]
        public long IdDetalleIngreso { get => _idDetalleIngreso; set => _idDetalleIngreso = value; }
        [DataMember]
        public string NumFactura { get => _numFactura; set => _numFactura = value; }
        [DataMember]
        public short TipoIngreso { get => _tipoIngreso; set => _tipoIngreso = value; }
    }
}
