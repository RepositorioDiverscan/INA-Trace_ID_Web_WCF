using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class CantidadArticuloSAPDBA
    {
        public string ObtenerCantidadArticuloSAP(long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerCantidadSAP");           
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
   
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {                    
                    return reader["Cantidad"].ToString();
                }
            }
            return "0";
        }
    }
}
