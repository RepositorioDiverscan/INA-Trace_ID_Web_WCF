using Diverscan.MJP.Entidades.Usuarios;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Usuarios
{
    public class da_Usuarios
    {
        public List<e_Usuarios> GetListUsuarios(string prefix, string IdCompania) 
        {
            List<e_Usuarios> ListUsuarios = new List<e_Usuarios>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from Usuarios where IdCompania = @IdCompania AND NOMBRE_PILA like + '%' + @SearchText + '%' or APELLIDOS_PILA like + '%' + @SearchText + '%' or EMAIL like + '%' + @SearchText + '%' or Usuario like + '%' + @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int IdUsuario = int.Parse(reader["IDUSUARIO"].ToString());
                                string Nombre = reader["Nombre"].ToString();
                                string idCompania = reader["IdCompania"].ToString();
                                string Usuario = reader["Usuario"].ToString();
                                string Contrasena = reader["CONTRASENNA"].ToString();
                                long IdRol = long.Parse(reader["idRol"].ToString());
                                string Email = reader["EMAIL"].ToString();
                                string Comentario = reader["COMENTARIO"].ToString();
                                bool EstaBloqueado = bool.Parse(reader["ESTA_BLOQUEADO"].ToString());
                                string NombrePila = reader["NOMBRE_PILA"].ToString();
                                string ApellidosPila = reader["APELLIDOS_PILA"].ToString();
                                decimal HorasProductivas = decimal.Parse(reader["HorasProductivas"].ToString());

                                ListUsuarios.Add(new e_Usuarios(IdUsuario, Nombre, idCompania, Usuario, Contrasena, IdRol, Email, Comentario, EstaBloqueado, NombrePila, ApellidosPila, HorasProductivas));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListUsuarios;
            }
            catch (Exception) 
            {
                return ListUsuarios;
            }
        }


        public string[] GetUsers(string prefix)
        {
            List<string> users = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select NOMBRE_PILA, IDUSUARIO from Usuarios where NOMBRE_PILA like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            users.Add(string.Format("{0}-{1}", sdr["NOMBRE_PILA"], sdr["IDUSUARIO"]));
                        }
                    }
                    conn.Close();
                }
            }
            return users.ToArray();
        }

        public List<e_Usuarios> GetUsuarios(string IdCompania)
        {
             List<e_Usuarios> ListUsuarios = new List<e_Usuarios>();
             try
             {
                 using (SqlConnection conn = new SqlConnection())
                 {
                     conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                     using (SqlCommand cmd = new SqlCommand())
                     {
                         cmd.CommandText = "select * from Usuarios where IdCompania = @IdCompania";
                         cmd.Parameters.AddWithValue("@IdCompania", IdCompania);

                         cmd.Connection = conn;
                         conn.Open();

                         using (SqlDataReader reader = cmd.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 int IdUsuario = int.Parse(reader["IDUSUARIO"].ToString());
                                 string Nombre = reader["Nombre"].ToString();
                                 string idCompania = reader["IdCompania"].ToString();
                                 string Usuario = reader["Usuario"].ToString();
                                 string Contrasena = reader["CONTRASENNA"].ToString();
                                 long IdRol = long.Parse(reader["idRol"].ToString());
                                 string Email = reader["EMAIL"].ToString();
                                 string Comentario = reader["COMENTARIO"].ToString();
                                 bool EstaBloqueado = bool.Parse(reader["ESTA_BLOQUEADO"].ToString());
                                 string NombrePila = reader["NOMBRE_PILA"].ToString();
                                 string ApellidosPila = reader["APELLIDOS_PILA"].ToString();
                                 decimal HorasProductivas = decimal.Parse(reader["HorasProductivas"].ToString());

                                 ListUsuarios.Add(new e_Usuarios(IdUsuario, Nombre, idCompania, Usuario, Contrasena, IdRol, Email, Comentario, EstaBloqueado, NombrePila, ApellidosPila, HorasProductivas));
                             }
                         }
                         conn.Close();
                     }
                 }
                 return ListUsuarios;
             }
             catch (Exception) 
             {
                 return ListUsuarios;
             }
        }

        public List<e_Usuarios> GetUserByRol(int idWarehouse, int idRol)
        {
            List<e_Usuarios> listUsuarios = new List<e_Usuarios>();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetUserByRol");
           
            dbTse.AddInParameter(dbCommand, "@idRol", DbType.Int32, idRol);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idWarehouse);

            var reader = dbTse.ExecuteReader(dbCommand);
          
            while (reader.Read())
            {
                e_Usuarios user = new e_Usuarios();

                user.IdUsuario = int.Parse(reader["IDUSUARIO"].ToString());                                     
                user.Nombre = reader["Nombre"].ToString();            
                user.IdBodega=Convert.ToInt32(reader["IdBodega"].ToString());
                user.IdSector = Convert.ToInt32(reader["idSector"].ToString());

                listUsuarios.Add(user);
            }

            return listUsuarios;    
        }
    }
}
