using Diverscan.MJP.Entidades.Operacion.TrasladoBodegas;
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado;
using System.Data;
using System.Data.SqlClient;

namespace Diverscan.MJP.AccesoDatos.Operacion.TrasladoBodegas
{
    public class DBTrasladoBodegas
    {
        #region Solicitud de Traslado Bodegas

        //Método para mostrar los artículos por bodegas 
        public List<EArticulosBodegas> ObtenerArticulosBodegas()
        {
            //Configuración de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosTrasladoBodega");

            List<EArticulosBodegas> listaArticulos = new List<EArticulosBodegas>();

            //Obtener la información de la BD
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaArticulos.Add(new EArticulosBodegas(reader));
                }
            }

            return listaArticulos;
        }


        //Método para obtener los artículos de una biodega en especifico
        public List<EArticulosBodegas> ObtenerArticulosBodegaEspecifica(int idBodega)
        {
            //Configuración de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosBodegaEspecifica");

            //Enviar los parametros del SP
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idBodega);


            List<EArticulosBodegas> listaArticulos = new List<EArticulosBodegas>();

            //Obtener la información de la BD
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listaArticulos.Add(new EArticulosBodegas(reader));
                }
            }

            return listaArticulos;
        }


        //Método para agregar una solicitud de traslado a bodega
        public string CrearSolicitudTrasladoBodega(ESolicitudTraslado solicitudTraslado)
        {
            try
            { 
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_CrearSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@numeroTransaccion", DbType.Int64, solicitudTraslado.NumeroTransaccion);
                db.AddInParameter(dbCommand, "@idUsuarioSolicita", DbType.Int64, solicitudTraslado.IdUsuarioSolicita);
                db.AddInParameter(dbCommand, "@idBodegaOrigen", DbType.Int64, solicitudTraslado.IdBodegaOrigen);
                db.AddInParameter(dbCommand, "@idBodegaDestino", DbType.Int64, solicitudTraslado.IdBodegaDestino);
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


        //Método para ingresar un nuevo artículo a una solicitud de traslado de Bodega
        public string ActualizarSolicitudTrasladoBodega(int idSolicitud, int idArticulo, int Cantidad)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_ActualizarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTraslado", DbType.Int64, idSolicitud);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                db.AddInParameter(dbCommand, "@cantidadSolicitada", DbType.Int64, Cantidad);
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


        //Método para agregar una solicitud de traslado a bodega
        public string ElimarSolicitudTrasladoBodega(int idSolicitud)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_EliminarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitud", DbType.Int64, idSolicitud);
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


        //Metodo para mostrar todos los encabezados de las solicitudes de Origen
        public List<EEncabezadoSolicitudTraslado> ObtenerEncabezadosSolicitudes(int idBodegaDestino)
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudesTrasladosBodegaOrigen");

            //Enviar los parametros del SP
            dbTse.AddInParameter(dbCommand, "@idBodegaOrigen", DbType.Int64, idBodegaDestino);

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


        //Metodo para obtener los detalle de un encabezado de una solicitud de traslado
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

        #endregion Solicitud de Traslado Bodegas


        #region Administración Solicitud de Traslado Bodegas

        //Metodo para mostrar todos los encabezados de las solicitudes de Destino
        public List<EEncabezadoSolicitudTraslado> ObtenerEncabezadosSolicitudesDestino(int idBodegaDestino)
        {
            //Configuracion de la BD y del SP a ejecutar
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudesTrasladosBodegaDestino");

            //Enviar los parametros del SP
            dbTse.AddInParameter(dbCommand, "@idBodegaDestino", DbType.Int64, idBodegaDestino);

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


        //Método para rechazar una solicitud de traslado a bodega
        public string RechazarSolicitudTrasladoBodega(int idSolicitudTrasladoBodega, int idUsuarioGestor)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_RechazarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTrasladoBodega", DbType.Int64, idSolicitudTrasladoBodega);
                db.AddInParameter(dbCommand, "@idUsuarioGestor", DbType.Int64, idUsuarioGestor);
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


        //Método para aceptar una solicitud de traslado a bodega
        public string AceptarSolicitudTrasladoBodega(EEncabezadoOla encabezadoOla)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_AceptarSolicitudTrasladoBodega");
                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTrasladoBodega", DbType.Int64, encabezadoOla.IdSolicitudTrasladoBodega);
                db.AddInParameter(dbCommand, "@idUsuarioProcesador", DbType.Int64, encabezadoOla.IdUsuarioProcesador);
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

        #endregion Administración Solicitud de Traslado Bodegas



    }
}
