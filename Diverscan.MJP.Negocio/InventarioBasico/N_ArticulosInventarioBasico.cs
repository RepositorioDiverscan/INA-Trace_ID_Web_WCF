using Diverscan.MJP.AccesoDatos.InventarioBasico;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.InventarioBasico
{
    public class N_ArticulosInventarioBasico
    {
        public static int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario)
        {
            ArticulosInventarioBasicoDBA articulosInventarioBasicoDBA = new ArticulosInventarioBasicoDBA();
            return articulosInventarioBasicoDBA.InsertarTomaFisicaInventario(tomaFisicaInventario);
        }

        public static List<ICantidadPorUbicacionArticuloRecord> ObtenerArticulosInventarioBasico(long idInventario, long idArticulo)
        {
            ArticulosInventarioBasicoDBA articulosInventarioBasicoDBA = new ArticulosInventarioBasicoDBA();
            var articulos = articulosInventarioBasicoDBA.ObtenerArticulosInventarioBasico(idInventario, idArticulo);

            return articulos;
        }

        public static List<ICantidadPorUbicacionArticuloRecord> ObtenerTodosArticulosInventarioBasico(long idInventario)
        {
            ArticulosInventarioBasicoDBA articulosInventarioBasicoDBA = new ArticulosInventarioBasicoDBA();
            var articulos =articulosInventarioBasicoDBA.ObtenerTodosArticulosInventarioBasico(idInventario);

            return articulos;
        }
    }
}
