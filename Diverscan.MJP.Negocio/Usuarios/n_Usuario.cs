using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.Usuarios;
using Diverscan.MJP.Entidades.Usuarios;

namespace Diverscan.MJP.Negocio.Usuarios
{
    public class n_Usuario
    {
        public string[] GetUsers(string prefix) 
        {
            da_Usuarios da_usuarios = new da_Usuarios();
            return da_usuarios.GetUsers(prefix);
        }

        public static List<e_Usuarios> GetListUsuarios(string prefix, string IdCompania) 
        {
            da_Usuarios da_usuarios = new da_Usuarios();
            return da_usuarios.GetListUsuarios(prefix, IdCompania);
        }

        public static List<e_Usuarios> GetListUsuarios(string IdCompania) 
        {
            da_Usuarios da_usuarios = new da_Usuarios();
            return da_usuarios.GetUsuarios(IdCompania);
        }

        public List<e_Usuarios> GetUserByRol(int idWarehouse,int idRol)
        {
            da_Usuarios da_usuarios = new da_Usuarios();
            return da_usuarios.GetUserByRol(idWarehouse,idRol);
        }
    }
}
