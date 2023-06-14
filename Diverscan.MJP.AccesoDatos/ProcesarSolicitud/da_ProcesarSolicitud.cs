using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace Diverscan.MJP.AccesoDatos.ProcesarSolicitud
{
    public class da_ProcesarSolicitud
    {
        public string InsertarDetalleSolicitud(e_ProcesarSolicitud ProcesarSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarSolicitud");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, ProcesarSolicitud.Cantidad);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, ProcesarSolicitud.IdUsuario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, ProcesarSolicitud.IdCompania);
                dbTse.AddInParameter(dbCommand, "@ZonaAlmacen", DbType.String, ProcesarSolicitud.ZonaAlmacen);
                dbTse.AddInParameter(dbCommand, "@ZonaPicking", DbType.String, ProcesarSolicitud.ZonaPicking);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int64, ProcesarSolicitud.IdMetodoAccion);
                dbTse.AddInParameter(dbCommand, "@idLineaDetalleSolicitud", DbType.Int64, ProcesarSolicitud.idLineaDetalleSolicitud);

                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);   //ExecuteScalar(dbCommand).ToString();
                if (DB.Tables[0].Rows.Count > 0)
                   result = DB.Tables[0].Rows[0][0].ToString();
            }

            catch(Exception ex)
            {
                result = ex.Message;
            }

             return result;
        }

        public string GeneraTarea(int idUsuario, Int64 idMaestroSolicitud, string actualiza, int idBodega)  //, string SSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GeneraTarea");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
                dbTse.AddInParameter(dbCommand, "@botonactualiza",DbType.String, actualiza);
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                //dbTse.AddInParameter(dbCommand, "@", DbType.Int32, idBodega);

                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);
                if (DB.Tables[0].Rows.Count > 0)
                    result = DB.Tables[0].Rows[0][0].ToString() + ";" +
                             DB.Tables[0].Rows[0][1].ToString() + ";" +
                             DB.Tables[0].Rows[0][2].ToString() + ";" +
                             DB.Tables[0].Rows[0][3].ToString() + ";" +
                             DB.Tables[0].Rows[0][4].ToString() + ";" +
                             DB.Tables[0].Rows[0][5].ToString() + ";" +
                             DB.Tables[0].Rows[0][6].ToString() + ";" +
                             DB.Tables[0].Rows[0][7].ToString() + ";" +
                             DB.Tables[0].Rows[0][8].ToString() + ";" +
                             DB.Tables[0].Rows[0][9].ToString() + ";" +
                             DB.Tables[0].Rows[0][10].ToString() + ";" +
                             DB.Tables[0].Rows[0][11].ToString() + ";" +
                             DB.Tables[0].Rows[0][12].ToString() + ";" +
                             DB.Tables[0].Rows[0][13].ToString() + ";" +
                             DB.Tables[0].Rows[0][14].ToString() + ";" +
                             DB.Tables[0].Rows[0][15].ToString() + ";" +
                             DB.Tables[0].Rows[0][16].ToString() + ";" +
                             DB.Tables[0].Rows[0][17].ToString() + ";" +
                             DB.Tables[0].Rows[0][18].ToString() + ";" +
                             DB.Tables[0].Rows[0][19].ToString();
            }

            catch (Exception ex)
            {
                return result += ex.Message;
            }

            return result;
        }

        public string AlistarArticulo(Int64 idArticulo, string idCompania, string SSCCGenerado, int idMetodoAccion, string Lote, string FV,
            Int64 idMaestroSolicitud, Int64 Cantidad, Int64 Idubicacion, string idTarea)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AlistaArticuloV2");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int64, idMetodoAccion);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String,Lote);
                dbTse.AddInParameter(dbCommand, "@FV", DbType.String, FV);
                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@Idubicacion", DbType.Int64, Idubicacion);
                dbTse.AddInParameter(dbCommand, "@IdTarea", DbType.Int64, idTarea);

                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);
                if (DB.Tables[0].Rows.Count > 0)
                {
                    result = DB.Tables[0].Rows[0][0].ToString() + ";" +
                             DB.Tables[0].Rows[0][1].ToString().Replace(",",".") + ";" +
                             DB.Tables[0].Rows[0][2].ToString().Replace(",", ".");
                }


            }
            catch(Exception ex)
            {
                return result + "-" + ex.Message + ";0;0";
            }
             return result;
        }

        //ALISTAR ARTÍCULO V2 02-03-2018  [Permite alistar artículos que no son los más prontos a vencer]
        public string AlistarArticuloV2(Int64 idArticulo, string idCompania, string SSCCGenerado, int idMetodoAccion, string Lote, string FV,
        Int64 idMaestroSolicitud, Int64 Cantidad, Int64 Idubicacion, string idTarea, bool esAutorizado, int idUsuarioAutorizador)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            //var dbCommand = dbTse.GetStoredProcCommand("SP_AlistaArticuloV3Test"); SE USA PARA VALIDAR LA FECHA DE VENCIMIENTO Y LOTES, SE QUITO 
            var dbCommand = dbTse.GetStoredProcCommand("SP_AlistaArticulo_Bexim"); //SE MODIFICO PARA BEXIM
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int64, idMetodoAccion);
                dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
                dbTse.AddInParameter(dbCommand, "@FV", DbType.String, FV);
                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@Idubicacion", DbType.Int64, Idubicacion);
                dbTse.AddInParameter(dbCommand, "@IdTarea", DbType.Int64, idTarea); 
                dbTse.AddInParameter(dbCommand, "@EsAutorizado", DbType.Boolean, esAutorizado);
                dbTse.AddInParameter(dbCommand, "@IdUsuarioAutorizador", DbType.Int32, idUsuarioAutorizador);

                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);
                if (DB.Tables[0].Rows.Count > 0)
                {
                    result = DB.Tables[0].Rows[0][0].ToString() + ";" +
                             DB.Tables[0].Rows[0][1].ToString().Replace(",", ".") + ";" +
                             DB.Tables[0].Rows[0][2].ToString().Replace(",", ".");
                }


            }
            catch (Exception ex)
            {
                return result + "-" + ex.Message + ";0;0";
            }
            return result;
        }


        public string DevuelveInfoArticulo(string GS1, string idCompania)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InfoArticulo");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@GS1", DbType.String, GS1);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);
                if (DB.Tables[0].Rows.Count > 0)
                {
                    result = DB.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                return result + "-" + ex.Message + ";0";
            }

            return result;
        }

        public DataSet DevuelveInfoSSCC(string SSCCGenerado, string idcompania)
        {
            DataSet DB = new DataSet();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InfoSSCC");

            try
            {
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idcompania);
                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);

            }
            catch (Exception)
            {
                return null;
            }


            return DB;
        }

        public string DevolverArticuloSSCC(string idCompania, string SSCCGenerado, Int64 Cantidad, string UbicacionaMover, Int64 idArticulo)
        {
            DataSet DB = new DataSet();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_DevolverArticuloSSCC");
            string result = "No Exitoso-";

            try
            {
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Cantidad);
                dbTse.AddInParameter(dbCommand, "@UbicacionaMover", DbType.String, UbicacionaMover);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
             
                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);

                result = DB.Tables[0].Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        public string ProcesarDespacho(string SSCCGenerado, string idCompania, int idUsuario)
        {
            DataSet DB = new DataSet();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarDespachoV2");
            string result = "No Exitoso-";

            try
            {
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
                
                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);

                result = DB.Tables[0].Rows[0][0].ToString() + ";" + 
                         DB.Tables[0].Rows[0][1].ToString()+ ";" +
                         DB.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        public string AsociarSSCCTransito(string SSCCGenerado, string idCompania, int idUsuario, string EtiquetaUbicacion, string  ZonaTransito)
        {
            DataSet DB = new DataSet();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AsociarSSCCTransito");
            string result = "No Exitoso-";

            try
            {
                dbTse.AddInParameter(dbCommand, "@SSCCGenerado", DbType.String, SSCCGenerado);
                dbTse.AddInParameter(dbCommand, "@EtiquetaUbicacion", DbType.String, EtiquetaUbicacion);
                dbTse.AddInParameter(dbCommand, "@ZonaTransito", DbType.String, ZonaTransito);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, idCompania);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);

                dbCommand.CommandTimeout = 3600;

                DB = dbTse.ExecuteDataSet(dbCommand);

                result = DB.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

    }
}
