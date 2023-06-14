using Diverscan.MJP.AccesoDatos.PICKING;
using Diverscan.MJP.Entidades.PICKING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.PICKING
{
    public class ArticulosPickingLoader
    {
        public static List<PickingRecord> ObtenerDisponibilidadPIC()
        {
            ConsultaArticulosPicking consultaArticulosPicking = new ConsultaArticulosPicking();
            return consultaArticulosPicking.GetMotivoAjusteInvertario();
        }
    }
}
