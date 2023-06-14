using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class EEnlist
    {

        public string Nombre { get; set; }

        public double Cantidad { get; set; }

        public double Contenido { get; set; }

        public string Descripcion { get; set; }


        public EEnlist(System.Data.IDataReader reader)
        {
            Nombre = reader["Nombre"].ToString();
            Cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
            Contenido = Convert.ToDouble(reader["Contenido"].ToString());
            Descripcion = reader["Descripcion"].ToString();
         ;
        }

    }
}
