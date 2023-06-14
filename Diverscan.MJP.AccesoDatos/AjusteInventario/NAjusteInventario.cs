using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class NAjusteInventario
    {
        public List<EAjusteInventario> ObtenerAjusteInventarioXIdSolicitudAjuste(int IdSolicitudAjuste)
        {
            DAjusteInventario dAjusteInventario = new DAjusteInventario();
            return dAjusteInventario.ObtenerAjusteInvetarioXSolicitud(IdSolicitudAjuste);
        }
    }
}
