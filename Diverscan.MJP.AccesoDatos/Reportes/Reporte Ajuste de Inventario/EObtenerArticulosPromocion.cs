using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario
{
    public class EObtenerArticulosPromocion
    {
       

        public EObtenerArticulosPromocion(int idDetallePromocion,int idArticuloNuevo, string idInternoPANAL, int idMaestroPromocion, int idInternoArticulo, string nombre, string gtin, decimal cantidad)
        {
            IdDetallePromocion = idDetallePromocion;
            IdMaestroPromocion = idMaestroPromocion;
            IdInternoArticulo = idInternoArticulo;
            IdArticuloNuevo = idArticuloNuevo;
            Nombre = nombre;
            Gtin = gtin;
            Cantidad = cantidad;
            IdInternoPANAL = idInternoPANAL;
        }

        public int IdDetallePromocion { get; set; }
        public int IdMaestroPromocion { get; set; }
        public int IdInternoArticulo { get; set; }
        public int IdArticuloNuevo { get; set; }
        public string IdInternoPANAL{ get; set; }
        public string Nombre { get; set; }
        public string Gtin { get; set; }
        public decimal Cantidad { get; set; }
    }
}
