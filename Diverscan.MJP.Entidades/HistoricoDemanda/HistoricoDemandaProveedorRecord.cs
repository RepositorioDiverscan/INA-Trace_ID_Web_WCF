using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.HistoricoDemanda
{
    [Serializable]  
    public class HistoricoDemandaProveedorRecord
    {
        public string Cantidad { get; set; }     
        public string NombreArticulo { get; set; }
        public string IdInternoArticulo { get; set; }
        public string Unidad_Medida { set; get; }

        public HistoricoDemandaProveedorRecord(IDataReader reader)
        {
            Cantidad = reader["Cantidad"].ToString();           
            NombreArticulo = reader["NombreArticulo"].ToString();
            IdInternoArticulo = reader["idInternoArticulo"].ToString();
            Unidad_Medida = reader["Unidad_Medida"].ToString();
        }
    }
}
