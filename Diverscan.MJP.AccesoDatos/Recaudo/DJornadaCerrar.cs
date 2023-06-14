using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    public class DJornadaCerrar
    {
        public void CerrarJornada(long idJornada)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("Sp_Bus_CerrarJornarda_VP");
            db.AddInParameter(dbCommand, "@IdJornada", DbType.Int64, idJornada);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}
