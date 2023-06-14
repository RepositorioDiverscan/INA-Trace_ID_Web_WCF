using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.DetalleOrdenCompra
{
    [DataContract]
    public class EArticulos
    {


        [DataMember]
        public int IdArticulo { get; set; }

        [DataMember]
        public int Idusuario { get; set; }

        [DataMember]
        public int IdMaestroOrdenCompra { get; set; }

        [DataMember]
        public double Cantidad { get; set; }

        [DataMember]
        public string Lote { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string FechaVencimiento { get; set; }

        [DataMember]
        public string Ubicacion { get; set; }

        [DataMember]
        public string DescripcionRechazo{ get; set; }
}
}
