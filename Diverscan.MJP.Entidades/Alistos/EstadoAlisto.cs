using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Alistos
{
    [DataContract]
    public class EstadoAlisto
    {
        [DataMember]
        public string NombreDestino { get; set; }

        [DataMember]
        public string CantidadAlistado { get; set; }

        [DataMember]
        public string CantidadPedido { get; set; }

        [DataMember]
        public string NombreArticulo { get; set; }

        //[DataMember]
        //public string Lote { get; set; }

        //[DataMember]
        //public string FechaVencimiento { get; set; }

        [DataMember]
        public string SSCCAsociado { get; set; }

        [DataMember]
        public string Nombre_Usuario { get; set; }

        //[DataMember]
        //public string CantidadUnidadAlisto { get; set; }

        //[DataMember]
        //public string CantidadInventario { get; set; }

        //[DataMember]
        //public string idInterno { get; set; }

        //[DataMember]
        //public string idInternoSAP { get; set; }

        public EstadoAlisto(System.Data.IDataReader reader)
        {
            NombreDestino = reader["Destino"].ToString();
            CantidadAlistado = reader["CantidadAlistado"].ToString();
            CantidadPedido = reader["CantidadPedido"].ToString();
            NombreArticulo = reader["NombreArticulo"].ToString();
            //Lote = reader["Lote"].ToString();
            //FechaVencimiento = reader["FechaVencimiento"].ToString();
            SSCCAsociado = reader["SSCCAsociado"].ToString();
            Nombre_Usuario = reader["Nombre_Usuario"].ToString();
            //CantidadUnidadAlisto = reader["CantidadUnidadAlisto"].ToString();
            //CantidadInventario = reader["CantidadInventario"].ToString();
            //idInterno = reader["idInterno"].ToString();
            //idInternoSAP = reader["idInternoSAP"].ToString();
        }
    }
}
