using Diverscan.MJP.AccesoDatos.Pedidos.Cursos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Pedidos.Admin
{
    public class DBPedidosAdmin
    {
        #region Pedidos Administrativos

        //Metodo para obtener todos los Encabezados de los Pedidos Originales
        public List<EEncabezadoPedidoAdmin> ObtenerPedidosAdminEncabezados(int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerPedidosAdmin");

            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<EEncabezadoPedidoAdmin> PedidoOriginal = new List<EEncabezadoPedidoAdmin>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    PedidoOriginal.Add(new EEncabezadoPedidoAdmin(reader));
                }
            }
            return PedidoOriginal;
        }


        //Metodo para obtener todos los detalles de los Pedidos Originales
        public List<EDetallePedido> ObtenerPedidosCursosDetalle(int idPedidoOriginal)
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


        //Método para anular el pedido admin
        public string anularPedidoCurso(int idPedidoOriginal)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_AnularPedidoAdmin");

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


        //Método para aprobar el pedido de Admin
        public string AprobarPedidoAdmin(int idPedidoOriginal)
        {
            try
            {
                //Conectarse a la BD y especificar el SP a ejecutar
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_IngresarOlaPedidoAdmin");

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

        #endregion Pedidos Administrativos
    }
}
