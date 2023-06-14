using System;
using System.Collections.Generic;
using System.Data;
using Diverscan.MJP.AccesoDatos.GestionPedido.Bodegas;
using Diverscan.MJP.AccesoDatos.GestionPedido.CajaChica;
using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudAlistos;
using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal
{
    public class DBAGestionPedido
    {
        //Metodo para obtener todos los Encabezados de los Pedidos Originales
        public List<EPedidoOriginalEncabezado> ObtenerPedidoOriginalEncabezado(int idbodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MuestraPedidoOriginal");
            
            dbTse.AddInParameter(dbCommand, "@idbodega", DbType.Int32, idbodega);

            List<EPedidoOriginalEncabezado> PedidoOriginal = new List<EPedidoOriginalEncabezado>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    PedidoOriginal.Add(new EPedidoOriginalEncabezado(reader));
                }
            }
            return PedidoOriginal;
        }


        //Metodo para obtener todos los detalles de un Encabezado de un Pedido
        public List<EPedidoOriginalDetalle> ObtenerPedidoOriginalDetalle(long idPedido)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MuestraDetallePedidoOriginal");

            dbTse.AddInParameter(dbCommand, "@idPedido", DbType.Int64, idPedido);
           
            List<EPedidoOriginalDetalle> PedidoOriginalDetalle = new List<EPedidoOriginalDetalle>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    var idArticulo = Convert.ToInt32(reader["idArticulo"]);
                    var idArticuloInterno = reader["idArticuloInterno"].ToString();
                    var nombre = reader["Nombre"].ToString();
                    var cantidadSolicitada = float.Parse(reader["cantidad"].ToString());
                    var cantidadDisponible = float.Parse(reader["CantidadDisponible"].ToString());
                    var idPedidoOriginal = Convert.ToInt32(reader["IDPedidoOriginal"]);
                    var cantidadResolver = Convert.ToInt32(reader["Resolver"]);
                    PedidoOriginalDetalle.Add(new EPedidoOriginalDetalle(idArticulo, idArticuloInterno, nombre, cantidadSolicitada, cantidadDisponible, idPedidoOriginal, cantidadResolver));
                }
            }
            return PedidoOriginalDetalle;
        }


        #region Solicitud Alisto
            
        //Metodo para obtener Encabezados de Solicitudes de Alisto
        public List<EEncabezadoAlisto> ObtenerEncabezadosAlistos()
        {            
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MostrarPreMaestroSolicitud");

            List<EEncabezadoAlisto> alistos = new List<EEncabezadoAlisto>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    alistos.Add(new EEncabezadoAlisto(reader));
                }
            }

            return alistos;            
        }


        //Metodo para obtener Encabezados de Solicitudes de Alisto
        public List<EDetalleAlisto> ObtenerDetalleAlistos(int idMaestroSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MostrarDetallePreSolicitud");

            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int32, idMaestroSolicitud);


            List<EDetalleAlisto> listaDetalle = new List<EDetalleAlisto>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaDetalle.Add(new EDetalleAlisto(reader));
                }
            }

            return listaDetalle;
        }


        //Metodo para ingresar una solicitud de alisto
        public string InsertarSolicitudAlisto(ESolicitudAlisto solicitudAlisto)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_InsertarSolicitudAlisto");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idUsuario", DbType.Int64, solicitudAlisto.IdUsuario);
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, solicitudAlisto.IdPedidoOriginal);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, solicitudAlisto.IdArticulo);
                db.AddInParameter(dbCommand, "@idBodega", DbType.Int64, solicitudAlisto.IdBodega);
                db.AddInParameter(dbCommand, "@cantidadAlistar", DbType.Int64, solicitudAlisto.CantidadAlisto);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }

            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para eliminar un articulo de la solicitud de alisto
        public string EliminarArticuloSolicitudAlisto(int idMaestroSolicitud, int idDetalle)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_EliminarArticuloSolicitudAlisto");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
                db.AddInParameter(dbCommand, "@idDetalle", DbType.Int64, idDetalle);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para eliminar un articulo de la solicitud de alisto
        public string EliminarSolicitudAlisto(int idMaestroSolicitud)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_EliminarSolicitudAlisto");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        #endregion Solicitud Alisto


        #region Solicitud Traslado Bodega

        //Metodo para obtener la cantidad disponible en las diferentes bodegas
        public List<EBodegasGPedidos> ObtenerCantidadesBodega(int idArticulo)
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");

            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosDisponiblesXBodega");
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);

            List<EBodegasGPedidos> listaBodegas = new List<EBodegasGPedidos>();

            //Obtener las bodegas del SP
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaBodegas.Add(new EBodegasGPedidos(reader));
                }
            }

            return listaBodegas;
        }


        //Metodo para ingresar una solicitud de traslado de Bodega
        public string InsertarSolicitudTrasladoBodega(ESolicitudTraslado solicitudTraslado)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_InsertarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idUsuarioSolicita", DbType.Int64, solicitudTraslado.IdUsuarioSolicita);
                db.AddInParameter(dbCommand, "@idBodegaOrigen", DbType.Int64, solicitudTraslado.IdBodegaOrigen);
                db.AddInParameter(dbCommand, "@idBodegaDestino", DbType.Int64, solicitudTraslado.IdBodegaDestino);
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, solicitudTraslado.IdPedidoOriginal);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, solicitudTraslado.IdArticulo);
                db.AddInParameter(dbCommand, "@cantidadSolicitada", DbType.Int64, solicitudTraslado.CantidadSolicitada);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }

            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para mostrar todos los encabezados con estado 1 o 2
        public List<EEncabezadoSolicitudTraslado> ObtenerEncabezadosSolicitudes()
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MostrarEncabezadoSolicitudesBodega");

            List<EEncabezadoSolicitudTraslado> listaEncabezados = new List<EEncabezadoSolicitudTraslado>();

            //Obtener las bodegas del SP
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaEncabezados.Add(new EEncabezadoSolicitudTraslado(reader));
                }
            }

            return listaEncabezados;
        }


        //Metodo para obtener el detalle de un encabezado de una solicitud de traslado
        public List<EDetalleSolicitudTraslado> ObtenerDetalleSolicitudes(int idSolicitudTraslado)
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_MostrarDetalleSolicitudesBodega");
            dbTse.AddInParameter(dbCommand, "@idSolicitudTrasladoBodega", DbType.Int64, idSolicitudTraslado);

            List<EDetalleSolicitudTraslado> listaDetalle = new List<EDetalleSolicitudTraslado>();

            //Obtener las bodegas del SP
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaDetalle.Add(new EDetalleSolicitudTraslado(reader));
                }
            }

            return listaDetalle;
        }


        //Metodo para comprobar si un articulo existe en algun detalle
        public string ComprobarArticuloEnDetalle(int idBodegaDestino, int idArticulo)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_ConsultarArticulosPorBodegaTraslado");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idBodegaDestino", DbType.Int64, idBodegaDestino);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para eliminar una solicitud
        public string EliminarSolicitudTraslado(int idSolicitudTraslado)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_BorrarSolicitudTraslado");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTraslado", DbType.Int64, idSolicitudTraslado);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }

        #endregion Solicitud Traslado Bodega


        #region Caja Chica
        
        //Metodo para ingresar a caja chica
        public string InsertarCajaChica(int idPedidoOriginal, int idUsuario, int idArticulo, int cantidadSolicitada)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_InsertarCompraCajaChica");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, idPedidoOriginal);
                db.AddInParameter(dbCommand, "@idUsuario", DbType.Int64, idUsuario);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                db.AddInParameter(dbCommand, "@cantidadSolicitada", DbType.Int64, cantidadSolicitada);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para obtener los encabezados de la Caja Chica
        public List<EEncabezadosCajaChica> ObtenerEncabezadosCC()
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");


            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerEncabezadoCajaChica");

            List<EEncabezadosCajaChica> listaCC = new List<EEncabezadosCajaChica>();

            //Obtener las bodegas del SP
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaCC.Add(new EEncabezadosCajaChica(reader));
                }
            }

            return listaCC;
        }


        //Metodo para obtener los encabezados de la Caja Chica
        public List<EDetalleCajaChica> ObtenerDetallesCC(int idCajaChica)
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");


            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetalleCajaChica");
            dbTse.AddInParameter(dbCommand, "@idCajaChica", DbType.Int64, idCajaChica);

            List<EDetalleCajaChica> listaCC = new List<EDetalleCajaChica>();

            //Obtener las bodegas del SP
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaCC.Add(new EDetalleCajaChica(reader));
                }
            }

            return listaCC;
        }


        //Metodo para eliminar un articulo de una caja chica
        public string EliminarArticuloCC(int idCajaChica, int idDetalle)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_EliminarArticuloCajaChica");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idCajaChica", DbType.Int64, idCajaChica);
                db.AddInParameter(dbCommand, "@idDetalle", DbType.Int64, idDetalle);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para eliminar toda la caja chica
        public string EliminarCC(int idCajaChica)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_EliminarCajaChicaTotalidad");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idCajaChica", DbType.Int64, idCajaChica);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }


        //Metodo para procesar una caja chica
        public string ProcesarCajaChica(int idCajaChica)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_ConfirmarCajaChica");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idCajaChica", DbType.Int64, idCajaChica);
                db.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                db.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
        }
        #endregion Caja Chica


    }
}
