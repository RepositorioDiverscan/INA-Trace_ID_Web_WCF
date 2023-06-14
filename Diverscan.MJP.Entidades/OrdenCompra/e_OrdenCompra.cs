using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.OrdenCompra
{
    [DataContract]
    public class e_OrdenCompra
    {
        [DataMember]
        public string IdMaestroOrdenCompra {get; set;}

        [DataMember]
        public string FechaDespacho { get; set; }

        [DataMember]
        public string NombreOrdenCompra { get; set; }

        [DataMember]
        public string NombreProducto { get; set; }

        [DataMember]
        public string CantidadRecibir { get; set; }

        [DataMember]
        public string CantidadRecibida { get; set; }

        [DataMember]
        public string NombreProveedor { get; set; }



        public e_OrdenCompra(IDataReader reader)
        {
            IdMaestroOrdenCompra = reader["IdMaestroOrdenCompra"].ToString();
            FechaDespacho = reader["FechaDespacho"].ToString();
            NombreOrdenCompra = reader["NombreOrdenCompra"].ToString();
            NombreProducto = reader["NombreProducto"].ToString();
            CantidadRecibir = reader["CantidadRecibir"].ToString();
            CantidadRecibida = reader["CantidadRecibida"].ToString();
            NombreProveedor = reader["NombreProveedor"].ToString();
        }
    }
}
