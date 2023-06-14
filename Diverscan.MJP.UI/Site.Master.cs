using System;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Negocio.UsoGeneral;


namespace Diverscan.MJP.UI
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            StyleHtml();
            e_Usuario _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            if (_eUsuario.IdUsuario != null)
            {
                lbNombre.Text = _eUsuario.Nombre;
                lbApellidos.Text = _eUsuario.Apellido;
                //TraceID.(2016). SiteMaster.En Trace ID Codigos documentados(19).Costa Rica:Grupo Diverscan. 
            }
            else
            {
                //btnCambiarSession.Visible = false;
            }

            // ApplyAppPathModifier will add the session ID if we're using Cookieless session.
            string urlWithSessionID = Response.ApplyAppPathModifier(Request.Url.PathAndQuery);
            RadPanelItem clickedItem = RadPanelBar1.FindItemByUrl(urlWithSessionID);
            // Expand the parent of the clicked item
            if (clickedItem != null)
            {
                clickedItem.ExpandParentItems();
                ShowPath(clickedItem);
            }

            string pageContentString;
            if (Request.QueryString["page"] == null)
            {
                pageContentString = "home";
            }
            else
            {
                pageContentString = Request.QueryString["page"];
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            //Response.ClearContent();

            ValidaAccesos(_eUsuario);
            //Mensaje("error", "Este usuario no tiene Rol asignado o no hay formularios asignados a este Rol", "");         

        }

        protected void StyleHtml()
        {
            try
            {
                var ruta = ConfigurationSettings.AppSettings["Institucion"];

                if (!string.IsNullOrEmpty(ruta))
                {
                    switch (ruta)
                    {
                        case "MJP":
                            imgLodoMJP.ImageUrl = "~/Images/Logos/TraceID2.png";
                            //lbInstitucion.Text = "Ministerio de Justicia y Paz";
                            //lbInstitucion.Font.Size = FontUnit.XXLarge;
                            cabezera.Attributes["class"] = "headerMJP";
                            CuerpoHtml.Attributes["class"] = "bodMJP";
                            MenuNavigate.Attributes["class"] = "clear hideSkiplink";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

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
                case "alert":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + sMensaje + "');", true);
                    break;
            }
        }

        protected void BtnCerrarSesionClick(object sender, EventArgs e)
        {
            try
            {
                //Bitacora Web JJGD
                var eUsuario = (e_Usuario)Session["USUARIO"];
                e_BitacoraWeb eBitacoraWeb = new e_BitacoraWeb();
                eBitacoraWeb.Accion = "Cerró sesión";
                eBitacoraWeb.idUsuario = Convert.ToInt32(eUsuario.IdUsuario);
                if (n_BitacoraWeb.InsertarBitacoraWeb("MJPConnectionString", "", eBitacoraWeb))
                { }
                HttpContext.Current.Session["USUARIO"] = null;
                ValidaAccesos(eUsuario);
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            catch (Exception ex)
            {
                var cler = new clErrores();
                cler.escribirError(ex.Message, ex.StackTrace);
            }
        }

        private void ShowPath(RadPanelItem clickedItem)
        {
            foreach (RadPanelItem childItem in clickedItem.PanelBar.GetAllItems())
            {
                childItem.CssClass = "";
            }
            clickedItem.CssClass = "rpSelected";
        }

        protected void btnCambiarSession_OnClick(object sender, EventArgs e)
        {
            //TraceID.(2016). SiteMaster.En Trace ID Codigos documentados(20).Costa Rica:Grupo Diverscan. 
            var ruta = "~/wf_Control.aspx?EsVer=ver";
            Response.Redirect(ruta);
        }

        protected void OcultarControles()
        {
            foreach (Control c in RadPanelBar1.Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    foreach (Control ccc in cc.Controls)
                    {
                        if (ccc is RadPanelItem)
                        {
                            RadPanelItem RutaWEB = (RadPanelItem)ccc;
                            RutaWEB.Visible = false;
                        }
                    }
                }
            }
        }

        protected void ValidaAccesos(e_Usuario eUsuario)
        {
            DataSet DS = new DataSet();
            string SQL = "";
            this.ddlmenu.Items.Clear();

            //SQL = "select ddlAspx,txtidAccesosporRol,idrol from TRACEID..Vista_AccesosporUsuario where idusuario = " + usuario;
            SQL = "EXEC SP_ObtenerFormulariosAsignados '" + eUsuario.IdRoles + "', '1'";

            DS = n_ConsultaDummy.GetDataSet(SQL, eUsuario.Usuario);

            OcultarControles();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Fila in DS.Tables[0].Rows)
                {
                    foreach (Control c in RadPanelBar1.Controls)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is RadPanelItem)
                                {
                                    RadPanelItem RutaWEB = (RadPanelItem)ccc;
                                    if (eUsuario.IdRoles == "0")
                                        RutaWEB.Visible = true;
                                    //else if (RutaWEB.NavigateUrl == Fila["ddlAspx"].ToString())
                                    else if (RutaWEB.NavigateUrl == Fila["Ruta"].ToString())
                                        RutaWEB.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                OcultarControles();
            }
        }
    }
}
