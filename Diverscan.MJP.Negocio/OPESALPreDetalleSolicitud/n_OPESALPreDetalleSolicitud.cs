using Diverscan.MJP.AccesoDatos.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Diverscan.MJP.Negocio.OPESALPreDetalleSolicitud
{
    public class n_OPESALPreDetalleSolicitud
    {
        public static e_OPESALPreDetalleSolicitudArticulo GetDetallesArticuloPorIdInterno(string IdCompania, string IdInternoArticulo, decimal CantidadProducto, string gtin)
        {
            da_OPESALPreDetalleSolicitud da_OPESALPreDetalleSolicitud = new da_OPESALPreDetalleSolicitud();
            return da_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(IdCompania, IdInternoArticulo, CantidadProducto, gtin);
        }
    }
}
