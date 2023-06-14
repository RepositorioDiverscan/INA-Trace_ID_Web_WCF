using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Utilidades;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("Administracion/wf_Credenciales.aspx");
            }
            ControlVersion();
            ControlMasterPage();
            NuevoUsuario();
            ControladorModal();
            if (!IsPostBack)
            {
                //mostrarNotificaciones();
            }
            
            
        }

        private void mostrarNotificaciones()
        {
            Notificaciones _notificaciones = new Notificaciones();
            string[] notificacionesObtenidas = _notificaciones.GetNotificacionesOnLoad();
            for (int i = 0; i < notificacionesObtenidas.Length; i++)
            {
                if (notificacionesObtenidas[i] != "false")
                    Response.Write("<script>alert('" + notificacionesObtenidas[i] + "');</script>");
            }
        }

        #region "METODOS"

        private void NuevoUsuario()
        {
            var eUsuario = (e_Usuario)Session["USUARIO"];

            //if (string.Equals(eUsuario.Contrasenna, "25f9e794323b453885f5181f1b624d0b"))
            //{
            //    const string ruta = "~/wf_Control.aspx?EsVer=pass";
            //    Response.Redirect(ruta);
            //}
        }

        private void ControlVersion()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["esExpress"] == "Si")
            {
                //    pnSalidas.Visible = false;
                //    pnIngresos.Visible = false;
                //    pnReportes.Visible = false;
                //}
            }
        }

        private void ControlMasterPage()
        {
            try
            {

                var _eUsuario = (e_Usuario)Session["USUARIO"];

                ((Label)Master.FindControl("lbNombre")).Text = _eUsuario.Nombre;
                ((Label)Master.FindControl("lbApellidos")).Text = _eUsuario.Apellido;

            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw;
            }
        }

        protected void ControladorModal()
        {
            try
            {
                var _eUsuario = (e_Usuario)Session["USUARIO"];


            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region "Utilidades"

        private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        {
            switch (sTipo)
            {
                case "error":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
                    break;
                case "info":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
                    break;
                case "ok":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
                    break;

            }
        }

        #endregion

    }
}

