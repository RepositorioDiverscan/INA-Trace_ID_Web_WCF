using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_ArticuloGTIN
    {
        public static List<e_Destino_Dev> ObtenerArticulos()
        {
            ArticuloGTINDBA articuloGTINDBA = new ArticuloGTINDBA();
            return articuloGTINDBA.ObtenerArticulos();             
        }

        public static List<e_Destino_Dev> ObtenerArticulosPorProvedor(long idProveedor)
        {
            ArticuloGTINDBA articuloGTINDBA = new ArticuloGTINDBA();
            return articuloGTINDBA.ObtenerArticulosPorProvedor(idProveedor);
        }
    }
}
