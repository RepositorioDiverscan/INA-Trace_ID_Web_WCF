using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [DataContract]
    public class MotivoAjusteInventarioRecord
    {
        [DataMember]
        public long IdAjusteInventario { set; get; }
        [DataMember]
        public string Nombre { set; get; }

        public MotivoAjusteInventarioRecord(long idAjusteInventario, string nombre)
        {
            IdAjusteInventario = idAjusteInventario;
            Nombre = nombre;
        }
        public MotivoAjusteInventarioRecord(IDataReader reader)
        {
            IdAjusteInventario = long.Parse(reader["IdAjusteInventario"].ToString());
            Nombre = reader["Nombre"].ToString();
        }
    }
}
