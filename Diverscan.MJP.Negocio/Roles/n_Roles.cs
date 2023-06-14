using Diverscan.MJP.AccesoDatos.Roles;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Rol;
using Diverscan.MJP.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Roles
{
    public class n_Roles
    {
        public static List<e_Rol> GetListRoles(string prefix, string IdCompania)
        {
            da_Roles da_usuarios = new da_Roles();
            return da_usuarios.GetListRoles(prefix, IdCompania);
        }

        public static List<e_Rol> GetRoles(string IdCompania)
        {
            da_Roles da_usuarios = new da_Roles();
            return da_usuarios.GetRoles(IdCompania);
        }

        
        //public string[] GetEsAdministrador(string nombreUsuario, string contrasena, string idCompania)
        //{
        //    da_Roles da_Usuario = new da_Roles();
        //    n_Credenciales n_Credenciales = new n_Credenciales();
        //    e_Usuario usuario  = n_Credenciales.ValidarUsuario(nombreUsuario, contrasena);//Se ejecuta para obtener el código HASH del password 
        //    if(usuario != null)
        //    {
        //        return da_Usuario.GetEsAdministrador(usuario.Usuario, usuario.Contrasenna, idCompania);
        //    }
        //    else
        //    {
        //        string[] resultado = new string[3];
        //        resultado[0] = "false";
        //        resultado[1] = "Credenciales incorrectas";
        //        resultado[2] = "-1";
        //        return resultado;
        //    }            
        //}
    }
}
