using Diverscan.MJP.AccesoDatos.AprobarDespacho;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AprobarDespacho
{
    public class n_AprobarDespacho
    {
        public static List<e_AprobarDespacho> GetAprobarDespachos()
        {
            da_AprobarDespacho da_aprobarDespacho = new da_AprobarDespacho();
            return da_aprobarDespacho.GetAprobarDespacho();

        }
    }
}
