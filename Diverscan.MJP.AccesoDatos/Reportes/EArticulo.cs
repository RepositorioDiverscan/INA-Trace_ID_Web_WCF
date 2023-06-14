using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
   public class EArticulo
    {
        public int IdInterno { get; set; }
        public string Nombre { get; set; }
        public string GTIN13 { get; set; }
        public string GTIN14 { get; set; }
        public string Descripcion { get; set; }
        public double Contenido { get; set; }


    }
}
