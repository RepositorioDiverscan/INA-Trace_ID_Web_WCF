using System;
using System.Collections.Generic;
using Diverscan.MJP.AccesoDatos.Administracion;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Negocio.Administracion
{
    public class n_Roles
    {
        #region 'InsRoles'
        public static Boolean InsRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRoles)
        {
            try
            {
                var daModAdministracionRoles = new da_Roles();
                return daModAdministracionRoles.InsRoles(sNombreBaseDatos, sUsuario, eRoles);
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                clLog.escribirError(exError.Message, exError.StackTrace);
                return false;
            }
        }
        #endregion

        #region 'UdpRoles'
        public static Boolean UdpRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRoles, Int32 idRol)
        {
            try
            {
                //var daModAdministracionRoles = new da_Roles();
                return da_Roles.UdpRoles(sNombreBaseDatos, sUsuario, eRoles, idRol);
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                clLog.escribirError(exError.Message, exError.StackTrace);
                return false;
            }
        }
        #endregion

        #region 'GetRoles'
        public static List<e_Roles> GetRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRoles)
        {
            return da_Roles.GetRoles(sNombreBaseDatos, sUsuario, eRoles);
        }

        public static e_Roles GetRolesXid(string sNombreBaseDatos, string sUsuario, string idRol)
        {
            return da_Roles.GetRolesXid(sNombreBaseDatos, sUsuario, idRol);
        }

        public static e_Roles GetRolesXid(string sNombreBaseDatos, string sUsuario, int idRol)
        {
            return da_Roles.GetRolesXid(sNombreBaseDatos, sUsuario, idRol);
        }

        public static string GetNombreRolPorUsuario(string sNombreBaseDatos, string sUsuario, string usuario)
        {
            return da_Roles.GetNombreRolPorUsuario(sNombreBaseDatos, sUsuario, usuario);
        }

        public static List<e_Roles> GetRolesPorNombre(string sNombreBaseDatos, string sUsuario, string nombre)
        {
            
            return da_Roles.GetRolesPorNombre(sNombreBaseDatos, sUsuario, nombre);
        }

        public static List<e_Roles> GetRolesPorCriterio(string sNombreBaseDatos, string sUsuario, string criterio)
        {

            return da_Roles.GetRolesPorCriterio(sNombreBaseDatos, sUsuario, criterio);
        }

        #endregion

        #region 'DelRol'
        public static Boolean DelRol(string sNombreBaseDatos, string sUsuario, Int32 idRol)
        {
            try
            {
                var daModAdministracionRoles = new da_Roles();
                return daModAdministracionRoles.DelRol(sNombreBaseDatos, sUsuario, idRol);
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                clLog.escribirError(exError.Message, exError.StackTrace);
                return false;
            }
        }
        #endregion
    }
}
