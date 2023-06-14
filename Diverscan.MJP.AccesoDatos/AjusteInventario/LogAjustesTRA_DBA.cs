using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class LogAjustesTRA_DBA
    {
        public long InsertLogAjustesTRA_Y_ObtenerIDLogAjustesTRA(long idSolicitudAjusteInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_IngresarLogAjustesTRA");

            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, idSolicitudAjusteInventario);          
            dbTse.AddOutParameter(dbCommand, "@Identity", DbType.Int64, 0);

            var result = dbTse.ExecuteNonQuery(dbCommand);
            long identity = Convert.ToInt64(dbCommand.Parameters["@Identity"].Value);
            return identity;
        }

        public e_LogAjustesTRARecord ObtenerUnicoRegistro(e_LogAjustesTRARecord e_logAjustesTRARecord)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_LogAjustesTRA");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, e_logAjustesTRARecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, e_logAjustesTRARecord.IdUsuario);
            //dbTse.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, e_logAjustesTRARecord.FechaRegistro);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, e_logAjustesTRARecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, e_logAjustesTRARecord.FechaVencimiento);            
                        
            e_LogAjustesTRARecord newE_LogAjustesTRARecord = new e_LogAjustesTRARecord();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    newE_LogAjustesTRARecord = new e_LogAjustesTRARecord(reader);
                    break;
                }
            }
            return newE_LogAjustesTRARecord;
        } 
    }
}
