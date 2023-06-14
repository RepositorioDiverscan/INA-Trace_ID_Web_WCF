using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.OrdenCompra
{
    [DataContract]
    public class EstadoOrdenCompra
    {
        [DataMember]
        public string CantidadPendiente { get; set; }

        [DataMember]
        public string CantidadRechazada { get; set; }

        [DataMember]
        public string CantidadRecibir { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string idMaestroSolicitud { get; set; }


        public EstadoOrdenCompra(System.Data.IDataReader reader)
        {
            //CantidadPendiente = Convert.ToDouble(reader["CantidadPendiente"].ToString());
            //CantidadRecibir = Convert.ToDouble(reader["CantidadRecibir"].ToString());
            //Nombre = reader["Nombre"].ToString();
            //idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());

            CantidadPendiente = reader["CantidadPendiente"].ToString();
            CantidadRechazada = reader["CantidadRechazada"].ToString();
            CantidadRecibir = reader["CantidadRecibir"].ToString();
            Nombre = reader["Nombre"].ToString();
            idMaestroSolicitud = reader["idMaestroSolicitud"].ToString();

        }

    }
}
