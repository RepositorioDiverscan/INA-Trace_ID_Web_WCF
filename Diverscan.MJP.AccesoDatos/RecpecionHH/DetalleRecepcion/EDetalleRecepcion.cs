using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion
{
    [DataContract]
    public class EDetalleRecepcion
    {
        long _idDetalleOrdenCompra;

        [DataMember]
        public long IdDetalleOrdenCompra { get => _idDetalleOrdenCompra; set => _idDetalleOrdenCompra = value; }
    }
}
