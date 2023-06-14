using Diverscan.MJP.Entidades.Consultas;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Consultas
{
    public class DGetArticuloXubicacion
    {
        public List<EGetArticulosXubicacion> GetOnePurchaseOrder(string ubicacion)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GetArticuloXubicacion");
            db.AddInParameter(dbCommand, "@Ubi", DbType.String, ubicacion);

            List<EGetArticulosXubicacion> mAIList = new List<EGetArticulosXubicacion>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new EGetArticulosXubicacion(reader));
                }
            }

            return mAIList;
        }






    }
}
