using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.MotivoAjusteInventario
{
    public class LogAjusteInventarioBD
    {
        public List<LogAjusteInventarioRecord> GetLogAjusteInventarioData(DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_LogAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);

            List<LogAjusteInventarioRecord> logList = new List<LogAjusteInventarioRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    logList.Add(new LogAjusteInventarioRecord(reader));
                }
            }
            return logList;
        }

        public void UpdateLogAjusteInventario(long idRecord, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Update_LogAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@IdRecord", DbType.Int64, idRecord);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }
    }
}
