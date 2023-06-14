using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.HistoricoDemanda
{
    [Serializable]
    public class SolicitudRestaurantesRecord
    {
        public string  Cantidad { set; get; }
        public string NombreDespacho { set; get; }
        public string Unidad_Medida { set; get; }

        public SolicitudRestaurantesRecord(IDataReader reader)
        {
            Cantidad = reader["Cantidad"].ToString();            
            NombreDespacho = reader["NombreDespacho"].ToString();
            Unidad_Medida = reader["Unidad_Medida"].ToString();
        }
    }
}
