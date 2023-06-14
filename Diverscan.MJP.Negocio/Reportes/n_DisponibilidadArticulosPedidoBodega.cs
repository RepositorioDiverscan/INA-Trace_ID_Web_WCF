using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Reportes.DisponibilidadPorBodega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_DisponibilidadArticulosPedidoBodega
    {
        da_DisponibilidadArticulosPedidoBodega da_DisponibilidadArticulosPedidoBodega = new da_DisponibilidadArticulosPedidoBodega();

        public List<e_DisponibilidadArticulosPedidoBodega> GetListaDisponibilidadArticulosPedidoBodega(string idCompania, long idMaestroSolicitud, int idBodega)
        {
            return da_DisponibilidadArticulosPedidoBodega.GetListaDisponibilidadArticulosPedidoBodega(idCompania, idMaestroSolicitud, idBodega);
        }
    }
}
