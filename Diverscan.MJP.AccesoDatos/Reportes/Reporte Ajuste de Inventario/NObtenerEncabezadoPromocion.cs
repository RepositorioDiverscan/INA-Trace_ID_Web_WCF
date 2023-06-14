using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario
{
    public class NObtenerEncabezadoPromocion
    {
        

        public NObtenerEncabezadoPromocion(int idMaestroPromocion, int idInternoArticulo, string nombre, DateTime fecha)
        {
            this.idMaestroPromocion = idMaestroPromocion;
            this.idInternoArticulo = idInternoArticulo;
            Nombre = nombre;
            Fecha = fecha;
          
        }

        public int idMaestroPromocion { get; set; }
        public int idInternoArticulo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha{ get; set; }
     
    }
}
