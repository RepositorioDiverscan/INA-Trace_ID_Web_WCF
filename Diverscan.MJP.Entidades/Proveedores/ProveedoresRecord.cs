using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Proveedores
{
    public class ProveedoresRecord
    {
        public long IdProveedor { get; set; }
        public string NombreProveedor { get; set; }

        public ProveedoresRecord(IDataReader reader)
        {
            IdProveedor = Convert.ToInt64(reader["IdProveedor"].ToString());
            NombreProveedor = reader["Nombre"].ToString();
           
        }
    }
}
