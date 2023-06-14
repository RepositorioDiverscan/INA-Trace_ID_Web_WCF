using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Negocio.LogicaWMS;

namespace Diverscan.MJP.UI.Consultas.Proveedores
{
    public partial class wf_ConsultaArticulosxProveedor : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        private DataSet _DSDatos = new DataSet();
        public int ToleranciaAgregar = 110;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDLS();
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        public void RegisterUpdatePanel(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }

        private void CargarDDLS()
        {
            try
            {


                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdProveedor, e_TablasBaseDatos.TblProveedores(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-ING-000001" + ex.Message, "");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                //    ConsultaSSCC("", true);
                ddlIdProveedor.Items.Clear();
                CargarDDLS();
                RadGridArticuloxProveedor.DataSource = null;
                RadGridArticuloxProveedor.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void ddlIdProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();                
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_Consulta_ProveedorXArticulo " + Convert.ToInt32(ddlIdProveedor.SelectedValue.ToString()) + "";
                _DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridArticuloxProveedor.DataSource = _DSDatos;
                RadGridArticuloxProveedor.DataBind();
                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridArticuloxProveedor_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (_DSDatos.Tables.Count > 0)
                {
                    RadGridArticuloxProveedor.DataSource = _DSDatos;
                    RadGridArticuloxProveedor.DataBind();              
                }
                  
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