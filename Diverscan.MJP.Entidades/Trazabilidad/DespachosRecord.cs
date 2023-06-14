using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Trazabilidad
{
    [Serializable]
    public class DespachosRecord
    {
        public int  Cantidad { set; get; }
        public DateTime FechaDespacho { set; get; }
        public string NombreDespacho { set; get; }
        public string UnidadMedida { get; set; }

        public DespachosRecord(IDataReader reader)
        {           
            Cantidad = Convert.ToInt32(reader["Cantidad"]);
            FechaDespacho = DateTime.Parse(reader["FechaDespacho"].ToString());
            NombreDespacho = reader["NombreDespacho"].ToString();
            UnidadMedida = reader["UnidadMedida"].ToString();
        }

        public string FechaDespachoExport
        {
            get { return FechaDespacho.ToShortDateString(); }
        }
    }
}
