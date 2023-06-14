using Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.DepuracionPedido
{
    public class DB_DepuracionPedido
    {
        //Metodo para obtener todos los Encabezados de los Pedidos Originales
        public List<EPedidoOriginalEncabezado> ObtenerPedidoOriginalEncabezado(int idusuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ConsultaPedidoOriginalPorUsuario");

            dbTse.AddInParameter(dbCommand, "@idusuario", DbType.Int32, idusuario);

            List<EPedidoOriginalEncabezado> PedidoOriginal = new List<EPedidoOriginalEncabezado>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    var idPedidoOriginal = Convert.ToInt64(reader["idPedidoOriginal"].ToString());
                    var idInterno = reader["idInterno"].ToString();
                    var nombre = reader["Nombre"].ToString();
                    var FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    var FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
                    PedidoOriginal.Add(new EPedidoOriginalEncabezado(idPedidoOriginal, idInterno, nombre, FechaCreacion, FechaEntrega));
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

        
        //Metodo para eliminar un articulo de la solicitud de alisto
        public string ModificarCantidad(int idPedidoOriginal, int idArticulo, int cantidadModificar, string accion)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_ModificarCantidadPedidoOriginal");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, idPedidoOriginal);
                db.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                db.AddInParameter(dbCommand, "@cantidadModificar", DbType.Int64, cantidadModificar);
                db.AddInParameter(dbCommand, "@accion", DbType.String, accion);
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
