using Diverscan.MJP.AccesoDatos.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.MotivoAjusteInventario
{
    public class n_CentroCostos
    {
        public static List<CentroCostosRegistro> ObtenerCentroDeCostos()
        {
            CentroCostosDBA centroCostosDBA = new CentroCostosDBA();
            return centroCostosDBA.ObtenerCentroDeCostos();
        }
    }
}
