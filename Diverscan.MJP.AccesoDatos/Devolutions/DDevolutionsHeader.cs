using Diverscan.MJP.Entidades.Devolutions.DevolucionHeader;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    public class DDevolutionsHeader
    {
        public List<EDevolutionsHeader> GetDevolutionHeader(string userEmail, string orderStatus)
        {          
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetDevolutionHeader");

            dbTse.AddInParameter(dbCommand, "@user_email", DbType.String, userEmail);
            dbTse.AddInParameter(dbCommand, "@order_status", DbType.String, orderStatus);

            List<EDevolutionsHeader> listDevolutionHeaders = new List<EDevolutionsHeader>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listDevolutionHeaders.Add(new EDevolutionsHeader(reader));
                }
            }           

            return listDevolutionHeaders;
        }

    }
}