using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.RolUsuarioHH
{
    public class DRolUsuarioHH
    {
        public List<ERolUsuarioHH> ObtenerUsuariosHH(int IdRol)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_SELECT_ROLES");
            db.AddInParameter(dbCommand, "@IdRol", DbType.String, IdRol);

            List<ERolUsuarioHH> ListRolUsuarioHH = new List<ERolUsuarioHH>();

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ERolUsuarioHH permisoRol = new ERolUsuarioHH();
                    permisoRol.PermisoHH = reader["FormHH"].ToString();
                    ListRolUsuarioHH.Add(permisoRol);
                }
            }
            return ListRolUsuarioHH;
        }
    }
}
