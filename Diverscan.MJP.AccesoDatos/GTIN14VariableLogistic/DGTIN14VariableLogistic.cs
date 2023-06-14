using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.GTIN14VariableLogistic
{
    public class DGTIN14VariableLogistic
    {
        public EGTIN14VariableLogistic GetProductDetailGTIN14(string consercutivoGtin14)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetDetailGTIN14");

            
            dbTse.AddInParameter(dbCommand, "@gtin14", DbType.String, consercutivoGtin14);

            var reader = dbTse.ExecuteReader(dbCommand);
            EGTIN14VariableLogistic gtin14 = null;
            if (reader.Read()) { 
                gtin14 = new EGTIN14VariableLogistic(reader);
           }

            return gtin14;
        }

        public string GetProductBaseByGTIN14(string gtin14)
        {
            string respuesta = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_SearchGTINBase");
            
            dbTse.AddInParameter(dbCommand, "@gtin14", DbType.String, gtin14);
          
            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["Result"].ToString();
                }
            }

            return respuesta;
        }
    }
}
