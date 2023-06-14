using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.Administracion;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Negocio.Administracion
{
    public class n_Credenciales
    {
        public static e_Usuario GetUsuarioLogin(string sNombreBaseDatos, string sUsuario)
        {
            return da_Credenciales.GetUsuarioLogin(sNombreBaseDatos, sUsuario);
        }

        public static e_Usuario ValidarUsuario(string usuario, string contrasenna)
        {
            var clErr = new clErrores();
            try
            {
                var MD5 = System.Security.Cryptography.MD5.Create();
                var eUsuario = new e_Usuario();
                var eUsuarioAdm = new e_Usuario();
                if (!string.IsNullOrEmpty(contrasenna))
                {
                    #region "Ingreso"
                    eUsuario = n_Credenciales.GetUsuarioLogin("MJPConnectionString", usuario);
                    eUsuarioAdm = n_Credenciales.GetUsuarioLogin("MJPConnectionString", "adminsis");
                    if (eUsuario.Bloqueado != true)
                    {
                        if (eUsuario.Usuario != "")
                        {
                            if (clHash.VerifyMd5Hash(MD5, contrasenna, eUsuario.Contrasenna, eUsuarioAdm.Contrasenna))
                            {
                                return eUsuario;
                            }
                            else
                            {
                                return null;

                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }

                    #endregion
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                clErr.escribirError(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public static string GetVersionSistemaHH()
        {
            return da_Credenciales.GetVersionSistemaHH();
        }
    }
}
