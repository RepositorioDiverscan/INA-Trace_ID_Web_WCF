using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_CopiaSistemaArticuloCiclico
    {
        public static List<ArticulosDisponibles> ObtenerArticulosCopiaSistema(long idInventario, long idArticulo)
        {
            CopiaSistemaArticuloCiclicoDBA copiaSistemaArticuloCiclicoDBA = new CopiaSistemaArticuloCiclicoDBA();
            return copiaSistemaArticuloCiclicoDBA.ObtenerArticulosCopiaSistema(idInventario, idArticulo);
        }

        public static List<ArticulosDisponibles> ObtenerTodosArticulosCopiaSistema(long idInventario)
        {
            CopiaSistemaArticuloCiclicoDBA copiaSistemaArticuloCiclicoDBA = new CopiaSistemaArticuloCiclicoDBA();
            return copiaSistemaArticuloCiclicoDBA.ObtenerTodosArticulosCopiaSistema(idInventario);
        }
    }
}
