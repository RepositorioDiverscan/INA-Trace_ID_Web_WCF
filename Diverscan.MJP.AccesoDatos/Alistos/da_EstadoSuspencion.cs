using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class da_EstadoSuspencion
    {
        public static string GetEstadoSuspencion(int idTareaU)
        {

            try
            {
                var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("GetEstadoSuspencion");
                db.AddInParameter(dbCommand, "@IdTareaU", DbType.Int64, idTareaU);
                db.AddOutParameter(dbCommand, "@Estado", DbType.String, 50);


                db.ExecuteNonQuery(dbCommand);

                string resultado = dbCommand.Parameters["@Estado"].Value.ToString();
                return resultado;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
