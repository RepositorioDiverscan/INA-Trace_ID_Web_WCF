using Diverscan.MJP.AccesoDatos.Proveedores;
using Diverscan.MJP.Entidades.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Proveedores
{
    public class n_Proveedores
    {
        public static List<ProveedoresRecord> ObtenerTodosProveedores()
        {
            ProveedoresDBA proveedoresDBA = new ProveedoresDBA();
            return proveedoresDBA.ObtenerTodosProveedores();
        }
    }
}
