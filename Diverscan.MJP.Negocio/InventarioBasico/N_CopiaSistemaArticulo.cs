using Diverscan.MJP.AccesoDatos.InventarioBasico;
using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.InventarioBasico
{
    public class N_CopiaSistemaArticulo
    {
        //Método que retorna los artículos de un inventario
        public static List<NombreIdArticuloRecord> ObtenerArticulosInventarioBasico(long idInventarioBasico)
        {
            CopiaSistemaArticuloDBA copiaSistemaArticuloDBA = new CopiaSistemaArticuloDBA();
            return copiaSistemaArticuloDBA.ObtenerArticulosInventarioBasico(idInventarioBasico);
        }

        public static List<ArticulosDisponibles> ObtenerArticulosCopiaSistema(long idInventario, long idArticulo)
        {
            CopiaSistemaArticuloDBA copiaSistemaArticuloDBA = new CopiaSistemaArticuloDBA();
            return copiaSistemaArticuloDBA.ObtenerArticulosCopiaSistema(idInventario, idArticulo);            
        }

        public static List<ArticulosDisponibles> ObtenerTodosArticulosCopiaSistema(long idInventario)
        {
            CopiaSistemaArticuloDBA copiaSistemaArticuloDBA = new CopiaSistemaArticuloDBA();
            return copiaSistemaArticuloDBA.ObtenerTodosArticulosCopiaSistema(idInventario);
        }
    }
}
