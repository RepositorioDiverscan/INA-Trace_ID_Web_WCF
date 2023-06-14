using Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.FinalizarProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.ProductoRecibido;
using Diverscan.MJP.AccesoDatos.RecpecionHH.RechazoProducto;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH
{
    public class RecepcionHH_DBA
    {
        public string InsertarArticuloRecibidoOC(EProductoRecibido productoRecibido)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_InsertProductsPO");

            db.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, productoRecibido.IdUsuario);
            db.AddInParameter(dbCommand, "@IdDetalleOrdenCompra", DbType.Int32, productoRecibido.IdDetalleOrdenCompra);
            db.AddInParameter(dbCommand, "@IdArticulo", DbType.Int32, productoRecibido.IdArticulo);
            db.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, productoRecibido.Cantidad);
            db.AddInParameter(dbCommand, "@Lote", DbType.String, productoRecibido.Lote);
            db.AddInParameter(dbCommand, "@FechaVecimiento", DbType.String, productoRecibido.FechaVencimiento);
            db.AddInParameter(dbCommand, "@Ubicacion", DbType.String, productoRecibido.Ubicacion);
            db.AddOutParameter(dbCommand, "@Resultado", DbType.String, 150);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@Resultado"].Value.ToString();
            return resultado;
        }

        public string ActualizarCantidadProductoRecibidoOC(EDevolcionProducto devolcionProducto)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_ReturnProductsPO");

            db.AddInParameter(dbCommand, "@IdDetalleOrdenCompra", DbType.Int32, devolcionProducto.IdDetalleOrdenCompra);
            db.AddInParameter(dbCommand, "@Lote", DbType.String, devolcionProducto.Lote);
            db.AddInParameter(dbCommand, "@FechaVecimiento", DbType.String, devolcionProducto.FechaVencimiento);
            db.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, devolcionProducto.Cantidad);
            db.AddOutParameter(dbCommand, "@Resultado", DbType.String, 150);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@Resultado"].Value.ToString();
            return resultado;
        }

        public List<EListDetalleRecepcion> ObtenerDetalleRecepcionOC(long IdDetalleOrdenCompra)
        {

            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GetReceptionDetailPO");

            db.AddInParameter(dbCommand, "@IdDetalleOrdenCompra", DbType.Int32, IdDetalleOrdenCompra);

            List<EListDetalleRecepcion> listDetalleRecepcions = new List<EListDetalleRecepcion>();

            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Lote = reader["Lote"].ToString();
                    DateTime FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
                    decimal Cantidad = decimal.Parse(reader["Cantidad"].ToString());
                    string Descripcion = reader["Descripcion"].ToString();
                    string Estado = reader["estado"].ToString();
                    string MotivoRechazo = reader["MotivoRechazo"].ToString();
                    string DescripcionRechazo = reader["DescripcionRechazo"].ToString();


                    listDetalleRecepcions.Add(new EListDetalleRecepcion(Lote,FechaVencimiento,Cantidad,Descripcion,Estado,MotivoRechazo,DescripcionRechazo));
                }
            }

            return listDetalleRecepcions;
        }

        public string InsertarArticuloRechazadoOC(ERechazoProducto rechazoProducto)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("RejectProductsPO");

            db.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, rechazoProducto.IdUsuario);
            db.AddInParameter(dbCommand, "@IdDetalleOrdenCompra", DbType.Int32, rechazoProducto.IdDetalleOrdenCompra);
            db.AddInParameter(dbCommand, "@IdArticulo", DbType.Int32, rechazoProducto.IdArticulo);
            db.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, rechazoProducto.Cantidad);
            db.AddInParameter(dbCommand, "@Ubicacion", DbType.String, rechazoProducto.Ubicacion);
            db.AddInParameter(dbCommand, "@MotivoRechazo", DbType.String, rechazoProducto.MotivoRechazo);
            db.AddInParameter(dbCommand, "@DescripcionRechazo", DbType.String, rechazoProducto.DescripcionRechazo);
            db.AddOutParameter(dbCommand, "@Resultado", DbType.String, 150);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@Resultado"].Value.ToString();
            return resultado;
        }

        public string InsertarRecepcionProductoFinalizada(EFinalizarRecepcionProducto finalizar)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_FinishDetailPO");
            db.AddInParameter(dbCommand, "@IdDetalleOrdenCompra", DbType.Int32, finalizar.IdDetalleOrdenCompra);
            db.AddInParameter(dbCommand, "@numFactura", DbType.String, finalizar.NumFactura);
            db.AddOutParameter(dbCommand, "@Resultado", DbType.String, 150);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@Resultado"].Value.ToString();
            return resultado;
        }

    }
}
