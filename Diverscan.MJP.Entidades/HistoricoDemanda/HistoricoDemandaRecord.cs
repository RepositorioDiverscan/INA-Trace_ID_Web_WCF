using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.HistoricoDemanda
{
    [Serializable]  
    public class HistoricoDemandaRecord
    {
        public string Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreArticulo { get; set; }
        public string IdInternoArticulo { get; set; }
        public string Unidad_Medida { set; get; }

        public HistoricoDemandaRecord(IDataReader reader)
        {
            Cantidad = reader["Cantidad"].ToString(); ;
            Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
            NombreArticulo = reader["NombreArticulo"].ToString();
            IdInternoArticulo = reader["idInternoArticulo"].ToString();
            Unidad_Medida = reader["Unidad_Medida"].ToString();
        }

        public string DiaSemana
        {
            get
            {
                return Fecha.ToString("ddddddd",
                      new CultureInfo("es"));

            }
        }
    }
}
