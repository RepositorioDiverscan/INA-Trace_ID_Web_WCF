using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.ProcesarRecepcionUbicacion
{
    public class da_ProcesarRecepcionUbicacion
    {
        public string ProcesarRecepcion(Int64 idMaestroOC, Int64 idArticulo, Int64 Cantidad, int idUsuario, string idCompania, string ZonaRecepcion,
                                        Int64 IdBodega, Int64 IdMetodoAccion, string FV, string Lote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarRecepcion");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...;0;0;0;0";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idMaestroOC", DbType.Int64, idMaestroOC);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@ZonaRecepcion", DbType.String, ZonaRecepcion);
                dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int64, IdBodega);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int64, IdMetodoAccion);
                dbTse.AddInParameter(dbCommand, "@FV", DbType.String, FV.Replace("-",""));
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);

                dbCommand.CommandTimeout = 3600;
                DB = dbTse.ExecuteDataSet(dbCommand);   //ExecuteScalar(dbCommand).ToString();
                result = DB.Tables[0].Rows[0][0].ToString() + ";" + DB.Tables[0].Rows[0][1].ToString() + ";" + DB.Tables[0].Rows[0][2].ToString() + ";" + DB.Tables[0].Rows[0][3].ToString() + ";" + DB.Tables[0].Rows[0][4].ToString();

            }

            catch (Exception ex)
            {
                result = ex.Message + "0;0;0;0";
            }

            return result;
        }

        public string ProcesarUbicacion(Int64 idArticulo, Int64 Cantidad, int idUsuario, string idCompania, string ZonaPic, string ZonaAlm, Int64 IdMetodoAccion, 
                                        string FV, string Lote, string EtiqUbic)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarUbicacion");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...;0;0;0";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@ZonaPicking", DbType.String, ZonaPic);
                dbTse.AddInParameter(dbCommand, "@ZonaAlmacenamiento", DbType.String, ZonaAlm);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int64, IdMetodoAccion);
                dbTse.AddInParameter(dbCommand, "@FV", DbType.String, FV);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
                dbTse.AddInParameter(dbCommand, "@EtiquetaUbicacion", DbType.String, EtiqUbic);

                dbCommand.CommandTimeout = 3600;
                DB = dbTse.ExecuteDataSet(dbCommand);
                result = DB.Tables[0].Rows[0][0].ToString() + ";" +
                         DB.Tables[0].Rows[0][1].ToString() + ";" + 
                         DB.Tables[0].Rows[0][2].ToString() + ";" + 
                         DB.Tables[0].Rows[0][3].ToString();

            }

            catch (Exception ex)
            {
                result = ex.Message + ";0;0;0";
            }

            return result;
        }

        public string TotalArticuloyPendienteOC(Int64 idArticulo, Int64 idMaestroOC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_TotalArticuloyPendienteOC");
            DataSet DB = new DataSet();
            string result = "0;0;0";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@idMaestroOC", DbType.Int64, idMaestroOC);
                dbTse.AddOutParameter(dbCommand, "@TAOCp", DbType.Decimal, 10);
                dbTse.AddOutParameter(dbCommand, "@APRp", DbType.Decimal, 10);
                dbTse.AddOutParameter(dbCommand, "@ARp", DbType.Decimal, 10);
              
                dbCommand.CommandTimeout = 3600;
                dbTse.ExecuteNonQuery(dbCommand);

                string TAOC = (dbTse.GetParameterValue(dbCommand, "@TAOCp").ToString());
                string APR = (dbTse.GetParameterValue(dbCommand, "@APRp").ToString());
                string AR = (dbTse.GetParameterValue(dbCommand, "@ARp").ToString());

                result = TAOC + ";" + APR + ";" + AR;

            }

            catch (Exception ex)
            {
                result = ex.Message + "0;0;0";
            }

            return result;
        }

    }    

}
