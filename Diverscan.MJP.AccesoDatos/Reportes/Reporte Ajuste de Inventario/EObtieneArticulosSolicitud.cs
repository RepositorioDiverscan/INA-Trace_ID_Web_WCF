using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario
{
    public class EObtieneArticulosSolicitud
    {
        public EObtieneArticulosSolicitud(int idSolicitudAjusteInventario, int idArticulo, string idInterno, string nombre, decimal cantidad)
        {
            IdSolicitudAjusteInventario = idSolicitudAjusteInventario;
            IdArticulo = idArticulo;
            IdInterno = idInterno;
            Nombre = nombre;
            Cantidad = cantidad;
        }

        public int IdSolicitudAjusteInventario { get; set; }

        public int IdArticulo { get; set; }

        public string IdInterno { get; set; }

        public string Nombre { get; set; }

        public decimal Cantidad { get; set; }
    }
}
