using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.AccesoDatos;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.UI.Inventario
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static int IdBodega;

        List<EMinPicking> _lisMinPickings
        {
            get
            {
                var data = ViewState["lisMinPickings"] as List<EMinPicking>;
                if (data == null)
                {
                    data = new List<EMinPicking>();
                    ViewState["lisMinPickings"] = data;
                }
                return data;
            }
            set
            {
                ViewState["lisMinPickings"] = value;
            }
        }

        List<EMinPicking> eMinPickings
        {
            get
            {
                var data = ViewState["eMinPickings"] as List<EMinPicking>;
                if (data == null)
                {
                    data = new List<EMinPicking>();
                    ViewState["eMinPickings"] = data;
                }
                return data;
            }
            set
            {
                ViewState["eMinPickings"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            e_Usuario UsrLogged = new e_Usuario();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            else
            {
                IdBodega = UsrLogged.IdBodega;
            }
            if (!IsPostBack)
            {

                FillDDBodega();
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
            }
        }

        protected void BtnCargarPicking(object sender, EventArgs e)
        {
                        
            try
            {
                int idBodega;
                if (pickingBodega.SelectedItem.Value != null)
                {
                    idBodega = Convert.ToInt32(pickingBodega.SelectedItem.Value);
                    cargardatos(idBodega);

                }                           
            }
            catch(Exception ex)
            {
                var c1 = new clErrores();
                c1.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

     
        private void cargardatos(int idBodega)
        {
            try
            {
                da_MaestroArticulo da = new da_MaestroArticulo();
                eMinPickings = da.GetMinPicking(idBodega);
                RadGridMinimoPicking.DataSource = eMinPickings;
                RadGridMinimoPicking.DataBind();

            }
            catch(Exception ex) {
                var cl =new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            pickingBodega.DataSource = ListBodegas;
            pickingBodega.DataTextField = "Nombre";
            pickingBodega.DataValueField = "IdBodega";
            pickingBodega.DataBind();
            pickingBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            pickingBodega.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void RadGridMinimoPicking_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGridMinimoPicking.DataSource = eMinPickings;
                RadGridMinimoPicking.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }
    }
}