using Diverscan.MJP.Entidades.Rol;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace Diverscan.MJP.AccesoDatos.Roles
{
    public class da_Roles
    {
        public List<e_Rol> GetListRoles(string prefix, string IdCompania)
        {
            List<e_Rol> ListRoles = new List<e_Rol>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //cmd.CommandText = "select * from SEGRol where idCompania = @IdCompania AND (Nombre like + '%' + @SearchText + '%' or Descripcion like + '%' + @SearchText + '%')";
                        cmd.CommandText = "EXEC SP_BuscarRol @IdCompania, @SearchText";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long IdRol = int.Parse(reader["idRol"].ToString());
                                string Nombre = reader["Nombre"].ToString();
                                string Descripcion = reader["Descripcion"].ToString();
                                string idCompania = reader["idCompania"].ToString();

                                ListRoles.Add(new e_Rol(IdRol, Nombre, Descripcion, idCompania));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListRoles;
            }
            catch (Exception)
            {
                return ListRoles;
            }
        }

        public List<e_Rol> GetRoles(string IdCompania)
        {
            List<e_Rol> ListRoles = new List<e_Rol>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //cmd.CommandText = "select * from SEGRol where idCompania = @IdCompania";
                        cmd.CommandText = "EXEC SP_BuscarRol @IdCompania, ''";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);

                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long IdRol = int.Parse(reader["idRol"].ToString());
                                string Nombre = reader["Nombre"].ToString();
                                string Descripcion = reader["Descripcion"].ToString();
                                string idCompania = reader["idCompania"].ToString();

                                ListRoles.Add(new e_Rol(IdRol, Nombre, Descripcion, idCompania));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListRoles;
            }
            catch (Exception)
            {
                return ListRoles;
            }
        }
       
    }
}
