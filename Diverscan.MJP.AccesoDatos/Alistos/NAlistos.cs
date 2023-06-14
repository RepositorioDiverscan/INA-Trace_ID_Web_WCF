using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class NAlistos
    {
        readonly EstadoAlistos dAConsultas = new EstadoAlistos();
        public List<EEnlist> ObtenerProductoPorGTIN(string GTIN)
        {
            return dAConsultas.ProductDetailFromGtin(GTIN);
        }

        public string SetProductManualEnlist(List<EEnlist> ProductList)
        {
            return dAConsultas.SetProductManualEnlist(ProductList);
        }

        public List<EEncabezadoSalida> GetEncabezadoSalidas(int idUsuario)
        {
            return dAConsultas.GetEncabezadoSalidas(idUsuario);
        }

        public string InsertSSCCCode(string SSCCCode, int idMaestroSolicitud)
        {
            return dAConsultas.InsertSSCCCode(SSCCCode, idMaestroSolicitud);
        }

        public string IngresarArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                           int cantidad, long idUbicacion, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {

            return dAConsultas.IngresarArticuloSSCC(idConsecutivoSSCC,idMaestroSolicitud, idArticulo,lote,FechaVencimiento,
                                                    cantidad, idUbicacion, idLineaDetalleSolicitud,idUsuario, idMetodoAccionSalida);
        }

        public string RevertirArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, DateTime FechaVencimiento,
                                         int cantidad, long idUbicacionDestino, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {

            return dAConsultas.RevertirArticuloSSCC(idConsecutivoSSCC, idMaestroSolicitud, idArticulo, lote, FechaVencimiento,
                                                    cantidad, idUbicacionDestino, idLineaDetalleSolicitud, idUsuario, idMetodoAccionSalida);
        }

    }
}
