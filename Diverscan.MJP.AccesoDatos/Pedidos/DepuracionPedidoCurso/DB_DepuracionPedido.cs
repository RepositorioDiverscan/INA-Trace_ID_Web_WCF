using Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal;
using Diverscan.MJP.AccesoDatos.Pedidos.Cursos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.DepuracionPedido
{
    public class DB_DepuracionPedido
    {
        //Metodo para obtener todos los Encabezados de los Pedidos Originales
        public List<EEncabezadoPedidoCurso> ObtenerPedidosCursosEnDepuracionEncabezados(int idBodega,int idUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDepuracionPedidosCursos");

            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);


            List<EEncabezadoPedidoCurso> PedidoOriginal = new List<EEncabezadoPedidoCurso>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    PedidoOriginal.Add(new EEncabezadoPedidoCurso(reader));
                }
            }
            return PedidoOriginal;
        }


        //Metodo para obtener todos los detalles de los Pedidos Originales en depuración
        public List<EDetallePedido> ObtenerPedidosCursosEnDepuracionDetalle(int idPedidoOriginal)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetallePedidoCurso");

            dbTse.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int32, idPedidoOriginal);

            List<EDetallePedido> DetallePedido = new List<EDetallePedido>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    DetallePedido.Add(new EDetallePedido(reader));
                }
            }
            return DetallePedido;
        }


        //Método para modificar la cantidad del pedido
        public string ModificarCantidadPedido(int idPedidoOriginal, int idArticulo, int cantidadModificar, string accion)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ModificarCantidadPedidoOriginal");

                dbTse.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int32, idPedidoOriginal);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, idArticulo);
                dbTse.AddInParameter(dbCommand, "@cantidadModificar", DbType.Int32, cantidadModificar);
                dbTse.AddInParameter(dbCommand, "@accion", DbType.String, accion);
                dbTse.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                dbCommand.CommandTimeout = 3600;
                dbTse.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AprobarPedidoCurso(int idPedidoOriginal)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_IngresarOlaPedidoCurso");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, idPedidoOriginal);
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

        //Método para anular el pedido de curso
        public string anularPedidoCurso(int idPedidoOriginal)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_AnularPedidoCurso");

                //Enviar los parametros del SP
                db.AddInParameter(dbCommand, "@idPedidoOriginal", DbType.Int64, idPedidoOriginal);
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
