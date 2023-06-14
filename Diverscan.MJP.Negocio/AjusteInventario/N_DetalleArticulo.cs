using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AjusteInventario
{
    public class N_DetalleArticulo
    {
        public static ArticuloRecord ObtenerArticuloPorIdArticulo(long idArticulo)
        {
            DetalleArticulo_DBA detalleArticulo_DBA = new DetalleArticulo_DBA();
            return detalleArticulo_DBA.ObtenerArticuloPorIdArticulo(idArticulo);
        }
    }
}
