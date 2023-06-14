using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.AprobarSalida;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OrdenCompa;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_TareasPorUsuario : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();                
        static DataSet DSDatosExport = new DataSet(); //Para Grid Detalle       
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDl();
                RadGridTareasUsuario.Rebind();
            }
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }


        private void CargarDDl()
        {
            try
            {
                n_SmartMaintenance.CargarDDL(ddlIdUsuario, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario, true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }

        public void Mensaje(string sTipo, string sMensaje, string sLLenado)
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

        private void CargarDetalleSolicitud(bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                int IdUsuario = Convert.ToInt32(ddlIdUsuario.SelectedValue.ToString());

                SQL = "EXEC SP_Obtener_Tareas_Por_Usuario_VD '" + IdUsuario + "', '" + idCompania + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                if (DSDatos.Tables[0].Rows.Count > 0)
                {
                    RadGridTareasUsuario.Visible = true;

                    RadGridTareasUsuario.DataSource = DSDatos;
                    if (pestana)
                    {
                        RadGridTareasUsuario.DataBind();
                    }
                }
                else
                {
                    RadGridTareasUsuario.Visible = false;
                }

                //DSDatosExport = DSDatos;

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {

        }

        protected void ddlIdUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDetalleSolicitud(true);
        }

        protected void RadGridTareasUsuario_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarDetalleSolicitud(false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
    }
}