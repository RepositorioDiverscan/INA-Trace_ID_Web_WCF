using Diverscan.MJP.Entidades.Reportes.Kardex;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;


namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_KardexReportesDBA
    {
        public List<e_TrazabilidadBodegaArticulos> ObtenerTrazabilidadArticuloBodega(
            long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin,
            string pLote, bool pFiltroPorLote, ref string idSAPArticuloSeleccionado, 
            ref string totalGlobal, ref string unidadMedidaTotalGlobal, int IdBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            //var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Trazabilidad_Articulo_Bodega");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Trazabilidad_Articulo_BodegaV2");
            dbTse.AddInParameter(dbCommand, "@PIdArticulo", DbType.String, pIdArticulo);
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@PLote", DbType.String, pLote);
            dbTse.AddInParameter(dbCommand, "@PFiltroPorLote", DbType.Boolean, pFiltroPorLote);
            dbTse.AddOutParameter(dbCommand, "@PIdInternoOUT", DbType.String, 1000);
            dbTse.AddOutParameter(dbCommand, "@PGlobalArticuloOUT", DbType.Decimal, 1000000000);
            dbTse.AddOutParameter(dbCommand, "@PUnidadMedidaGlobalOUT", DbType.String, 100);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, IdBodega);
            List<e_TrazabilidadBodegaArticulos> articulosList = new List<e_TrazabilidadBodegaArticulos>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        int idArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                        string lote = reader["Lote"].ToString();
                        DateTime fechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"].ToString());
                        string idUbicacion = reader["IdUbicacion"].ToString();
                        int idEstado = Convert.ToInt32(reader["IdEstado"].ToString());
                        string estadoDescripcion = reader["EstadoDescripcion"].ToString();
                        DateTime fechaRegistroDate = Convert.ToDateTime(reader["FechaRegistroDate"].ToString());
                        decimal cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                        string etiquetaUbicacion = reader["EtiquetaUbicacion"].ToString();
                        string unidadMedida = reader["UnidadMedida"].ToString();
                        string nombreUsuario = reader["NombreUsuario"].ToString();
                        string detalleMovimiento = reader["MetodoAccionDetalle"].ToString();
                        string idMaestroOC = reader["IdMaestroOC"].ToString();
                        string numeroOC = reader["NumeroOC"].ToString();
                        articulosList.Add(new e_TrazabilidadBodegaArticulos
                        (
                            idArticulo,
                            lote,
                            fechaVencimiento,
                            idUbicacion,
                            idEstado,
                            estadoDescripcion,
                            fechaRegistroDate,
                            cantidad,
                            etiquetaUbicacion,
                            unidadMedida,
                            nombreUsuario,
                            detalleMovimiento,
                            idMaestroOC,
                            numeroOC
                        ));
                    }
                }
                idSAPArticuloSeleccionado = dbTse.GetParameterValue(dbCommand, "@PIdInternoOUT").ToString();
                totalGlobal = dbTse.GetParameterValue(dbCommand, "@PGlobalArticuloOUT").ToString();
                unidadMedidaTotalGlobal = dbTse.GetParameterValue(dbCommand, "@PUnidadMedidaGlobalOUT").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return articulosList;
        }

        public List<e_Ajustes_Inventario_Articulo> ObtenerAjustesInventarioArticulo(
            long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin, 
            string pLote, bool pFiltroPorLote, int IdBodega)
        {

            //Para poder aplicar el DISTINCT en la consulta
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand(
                "SP_Obtener_Ajuste_Inventario_Por_Articulo_Rango_Fecha_Filtro_LoteV2");
            dbTse.AddInParameter(dbCommand, "@PIdArticulo", DbType.String, pIdArticulo);
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@PLote", DbType.String, pLote);
            dbTse.AddInParameter(dbCommand, "@PFiltroPorLote", DbType.Boolean, pFiltroPorLote);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, IdBodega);
            List<e_Ajustes_Inventario_Articulo> articulosList = new List<e_Ajustes_Inventario_Articulo>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        string idMovimiento = reader["IdMovimiento"].ToString();
                        string numeroMovimiento = reader["NumeroMovimiento"].ToString();
                        int idArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                        string idInternoSAP = reader["IdInternoSAP"].ToString();
                        string nombreArticulo = reader["NombreArticulo"].ToString();
                        decimal cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                        string unidadMedida = reader["UnidadMedida"].ToString();
                        string ajusteInventarioDescripcion = reader["AjusteInventarioDescripcion"].ToString();
                        DateTime fechaRegistroTrazabilidad = Convert.ToDateTime(reader["FechaRegistroTrazabilidad"].ToString()); ;
                        string idDestino = reader["IdUbicacion"].ToString();
                        string descripcionDestino = reader["DescripcionUbicacion"].ToString();
                        int idUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                        string usuario = reader["Usuario"].ToString();
                        string usuarioNombreCompleto = reader["UsuarioNombreCompleto"].ToString();
                        int idEstado = Convert.ToInt32(reader["IdEstado"].ToString());
                        int idMetodoAccion = Convert.ToInt32(reader["IdMetodoAccion"].ToString());
                        string metodoAccionDetalle = reader["MetodoAccionDetalle"].ToString();
                        string lote = reader["Lote"].ToString();

                        articulosList.Add(new e_Ajustes_Inventario_Articulo
                        (
                            idMovimiento,
                            numeroMovimiento,
                            idArticulo,
                            idInternoSAP,
                            nombreArticulo,
                            cantidad,
                            unidadMedida,
                            ajusteInventarioDescripcion,
                            fechaRegistroTrazabilidad,
                            idDestino,
                            descripcionDestino,
                            idUsuario,
                            usuario,
                            usuarioNombreCompleto,
                            idEstado,
                            idMetodoAccion,
                            metodoAccionDetalle,
                            lote
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return articulosList;
        }

        public List<e_ArticulosDespachadosPorLoteRangoFecha> ObtenerArticulosDespachadosPorLoteRangoFecha(
            long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin, string pLote, bool pFiltroPorLote, int IdBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            // var dbCommand = dbTse.GetStoredProcCommand("Obtener_Articulos_Despachados_Por_Lote_Rango_Fecha");
            var dbCommand = dbTse.GetStoredProcCommand("Obtener_Articulos_Despachados_Por_Lote_Rango_FechaV2");            
            dbTse.AddInParameter(dbCommand, "@PIdArticulo", DbType.String, pIdArticulo);
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@PFiltroPorLote", DbType.Boolean, pFiltroPorLote);
            dbTse.AddInParameter(dbCommand, "@PLote", DbType.String, pLote);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, IdBodega);
            List<e_ArticulosDespachadosPorLoteRangoFecha> articulosList = new List<e_ArticulosDespachadosPorLoteRangoFecha>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        string idInternoSAPSolicitud = reader["IdInternoSAPSolicitud"].ToString();
                        long idSolicitudTID = (long)Convert.ToDouble(reader["IdSolicitudTID"].ToString());
                        string idArticulo = reader["IdArticulo"].ToString();
                        string idInterno = reader["IdInterno"].ToString();
                        string lote = reader["Lote"].ToString();
                        DateTime fechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"].ToString());
                        decimal cantidadDespachada = Convert.ToDecimal(reader["CantidadDespachada"].ToString());
                        decimal cantidadSolicitada = Convert.ToDecimal(reader["CantidadSolicitada"].ToString());
                        string unidadMedida = reader["UnidadMedida"].ToString();
                        int cantidadUnidadAlistoSolicitud = Convert.ToInt32(reader["CantidadUnidadAlistoSolicitud"].ToString());
                        string etiquetaUbicacionDespacho = reader["EtiquetaUbicacionDespacho"].ToString();
                        string destinoSolicitud = reader["DestinoSolicitud"].ToString();
                        DateTime fechaDespacho = Convert.ToDateTime(reader["FechaDespacho"].ToString());

                        articulosList.Add(new e_ArticulosDespachadosPorLoteRangoFecha(
                            idInternoSAPSolicitud,
                            idSolicitudTID,
                            idArticulo,
                            idInterno,
                            lote,
                            fechaVencimiento,
                            cantidadDespachada,
                            cantidadSolicitada,
                            unidadMedida,
                            cantidadUnidadAlistoSolicitud,
                            etiquetaUbicacionDespacho,
                            destinoSolicitud,
                            fechaDespacho
                            ));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return articulosList;
        }

        public string ObtenerNumeroOC(long pIdArticulo, string pLote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("Obtener_NumeroOC");
            dbTse.AddInParameter(dbCommand, "@PIdArticulo", DbType.String, pIdArticulo);
            dbTse.AddInParameter(dbCommand, "@PLote", DbType.String, pLote);
            string numeroOC = "";
            bool numEncontrado = false;
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read() && numEncontrado == false)
                    {
                        numeroOC = reader["NumeroOC"].ToString();
                        numEncontrado = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return numeroOC;
        }

        public List<e_KardexMacroArticulo> ObtenerDatosKardexMacro(long pIdArticulo, DateTime pfechaInicio, DateTime pFechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_KardexMacro");
            dbTse.AddInParameter(dbCommand, "@Fechaini", DbType.Date, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@Fechafin", DbType.Date, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, pIdArticulo);
            dbCommand.CommandTimeout = 3600;
            List<e_KardexMacroArticulo> listaRegistros = new List<e_KardexMacroArticulo>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        long _idInterno_Articulo = (long)Convert.ToDouble(reader["IdInterno_Articulo"].ToString());
                        string _nombre_Articulo = reader["Nombre_Articulo"].ToString();
                        decimal _cantidad_Unidades_Inventario = Convert.ToDecimal(reader["Cantidad_Unidades_Inventario"].ToString());
                        string _unidad_medida = reader["Unidad_medida"].ToString();
                        string _detalle_Movimiento = reader["Detalle_Movimiento"].ToString();
                        string _num_documento = reader["Num_documento"].ToString();
                        DateTime _fecha_Registro = Convert.ToDateTime(reader["Fecha_Registro"].ToString());

                        listaRegistros.Add(new e_KardexMacroArticulo
                        (
                            _idInterno_Articulo,
                            _nombre_Articulo,
                            _cantidad_Unidades_Inventario,
                            _unidad_medida,
                            _detalle_Movimiento,
                            _num_documento,
                            _fecha_Registro
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRegistros;
        }
    }
}
