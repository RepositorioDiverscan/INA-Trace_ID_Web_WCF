using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Diverscan.MJP.AccesoDatos.Traslados.Utilities;
using Diverscan.MJP.AccesoDatos.Traslados.ManejoExcepciones;

namespace Diverscan.MJP.AccesoDatos.Traslados
{
    public class da_Traslados
    {
        public string ProcesarTraslados(Int64 idArticulo, string Lote,  string FV, Int64 idUbicacionorigen, Int64 idUbicaciondestino,
                                        Int64 Cantidad, int idUsuario, int idMetodoAccionSalida, int idMetodoAccionEntrada)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_PrincipalMovimientoTrazabilidad");
            DataSet DB = new DataSet();
            string result = "Proceso exitoso";

            try
            {
                dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
                dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.String, FV.Replace("-", ""));
                dbTse.AddInParameter(dbCommand, "@IdUbicacionOrigen", DbType.Int64, idUbicacionorigen);
                dbTse.AddInParameter(dbCommand, "IdUbicacionDestino", DbType.Int64, idUbicaciondestino);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, idUsuario);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccionSalida", DbType.Int32, idMetodoAccionSalida);
           	    dbTse.AddInParameter(dbCommand, "@IdMetodoAccionEntrada", DbType.Int32, idMetodoAccionEntrada);
                dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String,50);
                dbCommand.CommandTimeout = 3600;

                dbTse.ExecuteNonQuery(dbCommand);   //ExecuteScalar(dbCommand).ToString();

                result = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();

                //if (!string.IsNullOrEmpty(DB.Tables[0].Rows[0][0].ToString()))
                //    result = DB.Tables[0].Rows[0][0].ToString();
            }

            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public string ObtenerIdUbicacion(string Descripcion, int idWarehouse)
        {    
            string result = "";

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETLOCATION");
            db.AddInParameter(dbCommand, "@p_description", DbType.String, Descripcion);
            db.AddInParameter(dbCommand, "@p_idWarehouse", DbType.Int32, idWarehouse);
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    result = reader["RESULTADO"].ToString();
                }
            }   

            return result;
        }


        public string SetTransferProduct(int IdArticulo, string Lote, DateTime FechaVencimiento,int IdUbicacionOrigen,int Cantidad,int IdUsuario,int IdMetodoAccionSalida)
        {

             var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_PrincipalSalidaTrazabilidad");
            DataSet DB = new DataSet();
            string result = "Proceso exitoso";

            try
            {
                dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int32, IdArticulo);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
                dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, FechaVencimiento);
                dbTse.AddInParameter(dbCommand, "@IdUbicacionOrigen", DbType.Int32, IdUbicacionOrigen);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccionSalida", DbType.Int32, IdMetodoAccionSalida);
                dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 50);
                dbCommand.CommandTimeout = 3600;

               dbTse.ExecuteNonQuery(dbCommand);   

                result = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        public List<ETraslado> ProductDetailFromGtin(string GTIN)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GETPRODUCTFROMGTIN");

                dbTse.AddInParameter(dbCommand, "@p_gtin", DbType.String, GTIN);

                List<ETraslado> productdetail = new List<ETraslado>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ETraslado eTraslado = new ETraslado();
                        eTraslado.IdArticulo =Convert.ToInt32(reader["IdArticulo"].ToString());
                        eTraslado.ConTrazabilidad = reader["ConTrazabilidad"].ToString();
                        eTraslado.CANTIDADGTIN14 = Convert.ToDouble(reader["CANTIDADGTIN14"].ToString());
                        eTraslado.NOMBREARTICULO = reader["NOMBREARTICULO"].ToString();
                        eTraslado.NOMBREGTIN14 = reader["NOMBREGTIN14"].ToString();
                        productdetail.Add(eTraslado);
                    }
                }
                return productdetail;
            }
            catch (Exception)
            {
                return new List<ETraslado>();
            }
        }


        public string InTransferProduct(int IdArticulo, string Lote, DateTime FechaVencimiento, int IdUbicacionOrigen,int IdUbicacionDestino, int Cantidad, int IdUsuario, int IdMetodoAccionEntrada)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_PrincipalEntradaTrazabilidad");
            DataSet DB = new DataSet();
            string result = "";

            try
            {
                dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int32, IdArticulo);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
                dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, FechaVencimiento);
                dbTse.AddInParameter(dbCommand, "@IdUbicacionOrigen", DbType.Int32, IdUbicacionOrigen);
                dbTse.AddInParameter(dbCommand, "@IdUbicacionDestino", DbType.Int32, IdUbicacionDestino);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, Cantidad);
                dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccionEntrada", DbType.Int32, IdMetodoAccionEntrada);
                dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 50);
                dbCommand.CommandTimeout = 3600;

                dbTse.ExecuteNonQuery(dbCommand);

                result = dbTse.GetParameterValue(dbCommand, "@Resultado").ToString();
            }

            catch (Exception)
            {
                result = MensajeXUsuario.Mensaje();
            }

            return result;

        }

        public List<ETraslado> ObtenerEstadoSalidaUsuario(int IdUsuario, int IdMetodoAccion)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerEstadoSalidaUsuario");

                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, IdUsuario);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int32, IdMetodoAccion);

                List<ETraslado> productdetail = new List<ETraslado>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ETraslado eTraslado = new ETraslado();
                        eTraslado.NOMBREARTICULO = reader["Nombre"].ToString();
                        eTraslado.IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                        eTraslado.Cantidad= Convert.ToInt32(reader["Cantidad"].ToString());
                        eTraslado.Lote = reader["Lote"].ToString();
                        var ddd = reader["FechaVencimiento"].ToString().Substring(0, 10);
                        eTraslado.FechaVencimientoAndroid =Convert.ToDateTime(reader["FechaVencimiento"]).ToString("dd-MM-yyyy");
                        eTraslado.IdUbicacionOrigen = Convert.ToInt32(reader["IdUbicacion"].ToString());
                        eTraslado.NOMBREGTIN13 = reader["GTIN13"].ToString();
                        productdetail.Add(eTraslado);
                    }
                }
                return productdetail;
            }
            catch (Exception)
            {
              return new List<ETraslado>();
            }
        }
    }
}
