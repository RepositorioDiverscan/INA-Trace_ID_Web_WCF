using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Utilidades;
using Telerik.Web.UI;
using System.Diagnostics;
using System.Reflection;


namespace Diverscan.MJP.UI.Administracion
{
    public partial class wf_Credenciales : System.Web.UI.Page
    {
        /// <summary>
        /// Load de pagina web
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            this.lblCompiledVersion.Text = "v." + version + " Rev.119";
            try
            {
                if (Environment.MachineName == "DESARROLLO07")
                {
                    //RadtxtUsuario.Text = "ncalderon";
                    //RadtxtContrasenna.Text = "SetDiv2015";
                }

            }
            catch(Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }

        
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            Response.ClearContent(); 
            HttpContext.Current.Session["USUARIO"] = null;
            HttpContext.Current.Session["Muestra"] = "False";
        }

        /// <summary>
        /// Control de evento de boton para iniciar session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IngresarClick(object sender, EventArgs e)
        {
            var clErr = new clErrores();
            try
            {
                var MD5 = System.Security.Cryptography.MD5.Create();
                var eUsuario = new e_Usuario();

                var eUsuarioAdm = new e_Usuario();
                if (!string.IsNullOrEmpty(RadtxtContrasenna.Text))
                {
                    #region "Ingreso"
                    eUsuario = n_Credenciales.GetUsuarioLogin("MJPConnectionString", RadtxtUsuario.Text);               
                    eUsuarioAdm = n_Credenciales.GetUsuarioLogin("MJPConnectionString", "adminsis");
                    if (eUsuario.Usuario == "SIN CONEXION" || eUsuario == null)
                    {
                        Response.Write("<script>alert('SIN CONEXIÓN AL SERVIDOR');" +
                                              "window.location.href = \"wf_Credenciales.aspx\";</script>");
                        return;
                    }

                    if (eUsuario.Bloqueado != true)
                    {
                        if (eUsuario.Usuario != "")
                        {
                            if (clHash.VerifyMd5Hash(MD5, RadtxtContrasenna.Text, eUsuario.Contrasenna, eUsuarioAdm.Contrasenna))
                            {
                                HttpContext.Current.Session["USUARIO"] = eUsuario;
                                Response.Redirect("~/Default.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Contraseña incorrecta');" +
                                               "window.location.href = \"wf_Credenciales.aspx\";</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('El Usuario no se encuentra registrado');" +
                                           "window.location.href = \"wf_Credenciales.aspx\";</script>");
                        }
                    }
                    else
                    {

                        Response.Write("<script>alert('El Usuario esta bloqueado');" +
                                       "window.location.href = \"wf_Credenciales.aspx\";</script>");
                    }

                    #endregion
                }
                else
                {
                    Response.Write("<script>alert('Ingrese una contraseña por favor');" +
                                   "window.location.href = \"wf_Credenciales.aspx\";</script>");
                }
                //eUsuario.Usuario = "x";
                //eUsuario.Contrasenna = "x";
                //eUsuario.Nombre = "x";
                //eUsuario.Apellido = "x";
                //eUsuario.IdUsuario ="2";
                //eUsuario.IdBodega = 1;
                //HttpContext.Current.Session["USUARIO"] = eUsuario;
                //Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' El Usuario no se encuentra registrado);" +
                                        "window.location.href = \"wf_Credenciales.aspx\";</script>");
                clErr.escribirError(ex.Message, ex.StackTrace);
            }
        }
    }
}