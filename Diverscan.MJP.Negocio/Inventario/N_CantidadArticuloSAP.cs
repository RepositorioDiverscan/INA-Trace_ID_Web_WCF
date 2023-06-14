using Diverscan.MJP.AccesoDatos.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class N_CantidadArticuloSAP
    {
        public static string ObtenerCantidadArticuloSAP(long idArticulo)
        {
            CantidadArticuloSAPDBA cantidadArticuloSAPDBA = new CantidadArticuloSAPDBA();
            return cantidadArticuloSAPDBA.ObtenerCantidadArticuloSAP(idArticulo);
        }
    }
}
