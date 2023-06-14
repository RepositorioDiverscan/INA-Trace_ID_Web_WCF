using Diverscan.MJP.Entidades.Alistos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public interface IEstadoAlistos
    {
        List<EstadoAlisto> StatusActualPedido(string idMaestroArticulo);

        List<EEnlist> ProductDetailFromGtin(string GTIN);

         string IngresarArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                           int cantidad, long idUbicacion, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida);

        string RevertirArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                    int cantidad, long idUbicacionOrigen, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida);
    }
}
