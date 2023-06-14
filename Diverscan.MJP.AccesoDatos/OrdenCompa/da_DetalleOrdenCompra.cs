using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Diverscan.MJP.AccesoDatos.OrdenCompa
{
    public class da_DetalleOrdenCompra
    {
        public void InsertarDetalleOrdenCompra(e_ProcesarOrdenCompra procesarOrdenCompra)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarOrdenCompra");

                dbTse.AddInParameter(dbCommand, "@idMaestroOrdenCompra", DbType.Int64, procesarOrdenCompra.IdMaestroOrdenCompra);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, procesarOrdenCompra.IdArticulo);
                dbTse.AddInParameter(dbCommand, "@CantidadxRecibir", DbType.Int64, procesarOrdenCompra.CantidadxRecibir);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, procesarOrdenCompra.IdUsuario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, procesarOrdenCompra.IdCompania);
                var result = dbTse.ExecuteNonQuery(dbCommand);
            }
            catch (Exception)
            {
                return;
            }
        }

        public string InsertarDetalleOrdenCompra(DataTable DetalleOC, Int64 idproveedor, DateTime fechacreacion, DateTime fecharecepcion, string numOC, string comentario, string IdCompania, int idUsuario)
        {
            try
            {
                DataTable TablaDatos = new DataTable();
                TablaDatos = DetalleOC;
                DataSet DB = new DataSet();
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarOrdenCompra_V2");

                dbTse.AddInParameter(dbCommand, "@idProveedor", DbType.Int64, idproveedor);
                dbTse.AddInParameter(dbCommand, "@fechacreacion", DbType.Date, fechacreacion);
                dbTse.AddInParameter(dbCommand, "@fecharecepcion", DbType.Date, fecharecepcion);
                dbTse.AddInParameter(dbCommand, "@numOC", DbType.String, numOC);
                dbTse.AddInParameter(dbCommand, "@comentario", DbType.String, comentario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, "AMCO");
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int16, idUsuario);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@ArticulosData";
                parameter.SqlDbType = System.Data.SqlDbType.Structured;
                parameter.Value = TablaDatos;
                dbCommand.Parameters.Add(parameter);

                DB = dbTse.ExecuteDataSet(dbCommand);
                string result = DB.Tables[0].Rows[0][0].ToString();
                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
