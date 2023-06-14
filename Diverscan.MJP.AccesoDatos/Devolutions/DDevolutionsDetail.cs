using Diverscan.MJP.Entidades.Devolutions.DevolucionHeader;
using Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    public class DDevolutionsDetail
    {       
        public List<EDevolutionDetail> GetDevolutionDetail( string idDevolutionHeader)
        {
            List<EDevolutionDetail> listDevolutionDetails = new List<EDevolutionDetail>();           
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetDevolutionDetail");

            dbTse.AddInParameter(dbCommand, "@id_devolution_header", DbType.Int32, Convert.ToInt32(idDevolutionHeader));          
           
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    listDevolutionDetails.Add(new EDevolutionDetail(reader));
                }
            }
            return listDevolutionDetails;                        
        }
    }
}