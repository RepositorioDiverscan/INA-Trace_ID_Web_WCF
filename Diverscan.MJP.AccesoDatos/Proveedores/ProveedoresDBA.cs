using Diverscan.MJP.Entidades.Proveedores;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Proveedores
{
    public class ProveedoresDBA
    {
        public List<ProveedoresRecord> ObtenerTodosProveedores()
        {
          var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosProveedores");

            
            List<ProveedoresRecord> proveedores = new List<ProveedoresRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    proveedores.Add(new ProveedoresRecord(reader));
                }
            }
            return proveedores;
        }
    }
}
