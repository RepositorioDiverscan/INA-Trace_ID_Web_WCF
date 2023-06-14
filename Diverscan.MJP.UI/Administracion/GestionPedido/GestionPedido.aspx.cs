using System;
using System.Web.UI;
using Diverscan.MJP.Entidades;

namespace Diverscan.MJP.UI.Administracion.GestionPedido
{
    public partial class GestionPedido : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        string Pagina;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            Pagina = Page.AppRelativeVirtualPath.ToString();

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
        }
    }
}