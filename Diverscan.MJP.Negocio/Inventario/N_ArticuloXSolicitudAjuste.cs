using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_ArticuloXSolicitudAjuste
    {
        public static List<ArticuloXSolicitudAjusteDetalle> ObtenerDetalleSoliditudAjusteInventario(long pidSolicitudAjusteInventario)
        {
            ArticuloXSolicitudAjusteDBA articuloXSolicitudAjusteDBA = new ArticuloXSolicitudAjusteDBA();
            return articuloXSolicitudAjusteDBA.ObtenerDetalleSoliditudAjusteInventario(pidSolicitudAjusteInventario);
        }

        public static List<ArticuloXSolicitudAjusteDetalle> ObtenerAgrupadoPorLote(long pidSolicitudAjusteInventario)
        {
            ArticuloXSolicitudAjusteDBA articuloXSolicitudAjusteDBA = new ArticuloXSolicitudAjusteDBA();
            var listArticulos = articuloXSolicitudAjusteDBA.ObtenerDetalleSoliditudAjusteInventario(pidSolicitudAjusteInventario);
            List<ArticuloXSolicitudAjusteDetalle> finalList = new List<ArticuloXSolicitudAjusteDetalle>();
            while (listArticulos.Count > 0)
            {
                var consulta = listArticulos.Where(x => x.IdArticulo == listArticulos[0].IdArticulo && x.Lote == listArticulos[0].Lote
                    && x.FechaVencimiento == listArticulos[0].FechaVencimiento && x.IdUbicacionActual == listArticulos[0].IdUbicacionActual).ToList();

                var newCantidad = consulta.Sum(x => x.Cantidad);
                listArticulos[0].Cantidad = newCantidad;
                finalList.Add(listArticulos[0]);
                for (int x = 0; x < consulta.Count; x++)
                {
                    listArticulos.Remove(consulta[x]);
                }
            }
            return finalList;
        }
    }
}
