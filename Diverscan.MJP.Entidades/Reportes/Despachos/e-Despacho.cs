using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Despachos
{
    [Serializable]
    public class e_Despacho
    {

        public string Solicitud { get; set; }
        public string NombreArticulo { get; set; }
        public string Referencia { get; set; }
        public string Cantidad { set; get; }
        public string SSCC { set; get; }
        public string Destino { set; get; }
        public string FechaDespacho { set; get; }

        public e_Despacho(IDataReader reader)
        {
            Solicitud = reader["Solicitud"].ToString();
            NombreArticulo = reader["NombreArticulo"].ToString();
            Referencia = reader["Referencia"].ToString();
            Cantidad = reader["Cantidad"].ToString();
            SSCC = reader["SSCC"].ToString();
            Destino = reader["Destino"].ToString();
            FechaDespacho = reader["FechaDespacho"].ToString();

          
        }
    }
}

   
