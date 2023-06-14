using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.CustomException;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.TRAIngresoSalidaArticulos
{
    public class TRAIngresoSalidaArticulosLoader
    {
        public static TRAIngresoSalidaArticulosRecord ObtenerUltimoRegistro(long idArticulo, string lote, long idUbicacion)
        {
            ConsultasTRA consultasTRA = new ConsultasTRA();
            var tras = consultasTRA.GetTRA(idArticulo, lote, idUbicacion);
            if (tras.Count > 0)
                return tras[0];
            throw new RecordNotFoundException("No existen registros con los parámetros de búsqueda definidos");
        }

        public static List<BodegaFisica_SistemaRecord> ObtenerExistencias(long idArticulo, long idInventario)
        {
            ConsultasTRA consultasTRA = new ConsultasTRA();
            return consultasTRA.ObtenerExistencias(idArticulo, idInventario);
        }

        public static List<ArticulosDisponibles> ObtenerArticulosDisponibles(long idArticulo)
        {
            ConsultasTRA consultasTRA = new ConsultasTRA();
            return consultasTRA.ObtenerArticulosDisponibles(idArticulo);
        }
        public static List<ICantidadPorUbicacionArticuloRecord> ObtenerCantidadArticulosInventario(long idInventario, long idArticulo)
        {
            ConsultasTRA consultasTRA = new ConsultasTRA();
            var articulos = consultasTRA.ObtenerCantidadArticulosInventario(idInventario, idArticulo);
            var articulosSeparados = GrupoExpansor.ExpandirGrupo(articulos);
            return articulosSeparados;
        }

        public static List<ICantidadPorUbicacionArticuloRecord> ObtenerCantidadTodosArticulosInventario(long idInventario)
        {
            ConsultasTRA consultasTRA = new ConsultasTRA();
            var articulos = consultasTRA.ObtenerCantidadTodosArticulosInventario(idInventario);
            var articulosSeparados = GrupoExpansor.ExpandirGrupo(articulos);
            return articulosSeparados;
        }



    }
}
