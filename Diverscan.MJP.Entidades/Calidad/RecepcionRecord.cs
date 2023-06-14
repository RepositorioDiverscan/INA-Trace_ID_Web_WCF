using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Calidad
{
    [Serializable]
    public class RecepcionRecord
    {
        public int Cantidad { set; get; }
        public string Lote { set; get; }
        public DateTime FechaRecepcion { set; get; }
        public string Proveedor { set; get; }
        public string NombreUsuario { set; get; }
        public string Unidad_Medida { set; get; }  

        public RecepcionRecord(IDataReader reader)
        {
             Cantidad =Convert.ToInt32(reader["Cantidad"]);
             Lote = reader["Lote"].ToString();
             FechaRecepcion= Convert.ToDateTime(reader["FechaRecepcion"]);
             Proveedor = reader["Proveedor"].ToString();
             NombreUsuario = reader["NombreUsuario"].ToString();
             Unidad_Medida = reader["Unidad_Medida"].ToString();
        }
    }
}
