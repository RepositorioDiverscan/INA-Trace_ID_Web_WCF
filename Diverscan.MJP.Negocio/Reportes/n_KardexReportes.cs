using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Trazabilidad;
using Diverscan.MJP.AccesoDatos.Trazabilidad;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using System.Data;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_KardexReportes
    {
        #region "Objetos requeridos"
        private da_KardexReportesDBA da_KardexReportesDBA = new da_KardexReportesDBA();
        private LotesDBA _LotesDBA = new LotesDBA();
        private ArticuloGTINDBA _articuloGTINDBA = new ArticuloGTINDBA();
        #endregion

        #region "Reporte Trazabilidad"
        public List<e_TrazabilidadBodegaArticulos> ObtenerTrazabilidadArticuloBodega(
            long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin, 
            string pLote, bool pFiltroPorLote, ref string idSAPArticuloSeleccionado,
            ref string totalGlobal, ref string unidadMedidaTotalGlobal, int IdBodega)
        {
            return da_KardexReportesDBA.ObtenerTrazabilidadArticuloBodega(
                pIdArticulo, pfechaInicio, pFechaFin, pLote, pFiltroPorLote,
                ref idSAPArticuloSeleccionado, ref totalGlobal, ref unidadMedidaTotalGlobal, IdBodega);
        }

        public List<e_ArticulosDespachadosPorLoteRangoFecha> ObtenerArticulosDespachadosPorLoteRangoFecha(
            long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin, string pLote, bool pFiltroPorLote, int IdBodega)
        {
            return da_KardexReportesDBA.ObtenerArticulosDespachadosPorLoteRangoFecha(
                pIdArticulo, pfechaInicio, pFechaFin, pLote, pFiltroPorLote, IdBodega);
        }

        public string ObtenerNumeroOC(long pIdArticulo, string pLote)
        {
            return da_KardexReportesDBA.ObtenerNumeroOC(pIdArticulo, pLote);
        }

        public List<LotesRecord> ObtenerLotesDespachadosPorArticuloFecha(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            return _LotesDBA.ObtenerLotesDespachadosPorArticuloFecha(idArticulo, fechaInicio, fechaFin);
        }

        public List<LotesRecord> ObtenerLotesTrazabilidadBodegaArticulo(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            return _LotesDBA.ObtenerLotesTrazabilidadBodegaArticulo(idArticulo, fechaInicio, fechaFin);
        }

        public List<e_Ajustes_Inventario_Articulo> ObtenerAjustesInventarioArticulo
            (long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin,
            string pLote, bool pFiltroPorLote, int IdBodega)
        {
            return da_KardexReportesDBA.ObtenerAjustesInventarioArticulo(
                pIdArticulo, pfechaInicio, pFechaFin, pLote, pFiltroPorLote, IdBodega);
        }
        #endregion

        #region "Reporte Kardex Macro"
        public List<e_KardexMacroArticulo> ObtenerDatosKardexMacro(long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin)
        {
            return da_KardexReportesDBA.ObtenerDatosKardexMacro(pIdArticulo, pfechaInicio, pFechaFin);
        }
        #endregion

        #region "Artículos"
        public List<e_ArticuloIdERP> ObtenerArticulosIdInternoERP()
        {
            return _articuloGTINDBA.ObtenerArticulosIdInternoERP();
        }

        public List<e_ArticuloIdERP> ObtenerArticulosIdInternoERPPorProveedor(long idProveedor)
        {
            return _articuloGTINDBA.ObtenerArticulosIdInternoERPPorProveedor(idProveedor);
        }
        #endregion

    }
}
