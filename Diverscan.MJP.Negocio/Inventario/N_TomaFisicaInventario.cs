using Diverscan.MJP.AccesoDatos.Inventario;
using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_TomaFisicaInventario
    {
        public static int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario)
        {
            TomaFisicaInventarioDBA tomaFisicaInventarioDBA = new TomaFisicaInventarioDBA();
            return tomaFisicaInventarioDBA.InsertarTomaFisicaInventario(tomaFisicaInventario);
        }
    }
}
