using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.ProcesarSolicitud
{
    public class da_InsertaPreDetalleSolicitud
    {
        public string InsertarPreDetalleSolicitud(e_Insertapredetallesolicitud Insertapredetallesolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertaPreDetalleSolicitud");
            DataSet DB = new DataSet();
            string result = "Proceso no exitoso...";

            try
            {
                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, Insertapredetallesolicitud.IdMaestroSolicitud);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, Insertapredetallesolicitud.Idarticulo);
                dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int64, Insertapredetallesolicitud.Cantidad);
                dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, Insertapredetallesolicitud.IdUsuario);
                dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String, Insertapredetallesolicitud.IdCompania);
              
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
    }
}
