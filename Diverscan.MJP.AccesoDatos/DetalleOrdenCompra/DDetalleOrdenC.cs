using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.DetalleOrdenCompra
{
   public class DDetalleOrdenC
    {

        public string InsertarArticulosRR(DataTable eArticulos)
        {
            string respuesta = "";
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_ArticulosRecepcion");
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@eArticulos";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = eArticulos;
            dbCommand.Parameters.Add(parameter);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["Resultado"].ToString();
                }
            }

            return respuesta;
        }


    }
}
