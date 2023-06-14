using Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.SolicitudTrasladoBodega.SolicitudesWeb
{
    public class DBSolicitudesTrasladoWeb
    {
        //Metodo para mostrar todos los encabezados con estado 1 o 2
        public List<EEncabezadoSolicitudTraslado> ObtenerEncabezadosSolicitudes(int idBodegaDestino)
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


        //Metodo para aceptar una solicitud de traslado a bodega
        public string AceptarSolicitudTraslado(int idSolicitudTrasladoBodega, int idUsuarioProcesador)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_AceptarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTrasladoBodega", DbType.Int64, idSolicitudTrasladoBodega);
                db.AddInParameter(dbCommand, "@idUsuarioProcesador", DbType.Int64, idUsuarioProcesador);
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


        //Metodo para aceptar una solicitud de traslado a bodega
        public string RechazarSolicitudTraslado(int idSolicitudTrasladoBodega)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_RechazarSolicitudTrasladoBodega");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idSolicitudTrasladoBodega", DbType.Int64, idSolicitudTrasladoBodega);
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
    }
}
