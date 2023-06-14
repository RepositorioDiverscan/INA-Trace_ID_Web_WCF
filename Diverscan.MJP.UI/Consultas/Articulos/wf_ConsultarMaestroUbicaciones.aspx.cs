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

namespace Diverscan.MJP.UI.Consultas.Articulos
{
    public partial class wf_ConsultarMaestroUbicaciones : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
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
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
            this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
            this.Panel5.Unload += new EventHandler(UpdatePanel4_Unload);
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
        }

        void UpdatePanel3_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel3(sender as UpdatePanel);
        }

        void UpdatePanel4_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel4(sender as UpdatePanel);
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

        public void RegisterUpdatePanel2(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                }
            }
        }

        public void RegisterUpdatePanel3(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel3 });
                }
            }
        }

        public void RegisterUpdatePanel4(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
                }
            }
        }

        #region Almacen

        private void CargarAlmacen(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarAlmacen '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridAlmacen.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridAlmacen.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarAlmacen(txtSearch.Text.ToString().Trim(), true);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarAlmacen("", true);
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridAlmacen_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarAlmacen(txtSearch.Text.ToString().Trim(), false);
                //CargarAlmacen("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridAlmacen_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Almacen

        #region Bodega

        private void CargarBodega(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarBodega '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridBodega.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridBodega.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscarBodega_Click(object sender, EventArgs e)
        {
            try
            {
                CargarBodega(txtSearchBodega.Text.ToString().Trim(), true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnRefrescarBodega_Click(object sender, EventArgs e)
        {
            try
            {
                CargarBodega("", true);
                txtSearchBodega.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarBodega(txtSearchBodega.Text.ToString().Trim(), false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridBodega_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Bodega

        #region Zona

        private void CargarZona(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";

                SQL = "EXEC SP_BuscarZona '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridZona.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridZona.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscarZona_Click(object sender, EventArgs e)
        {
            try
            {
                CargarZona(txtSearchZona.Text.ToString().Trim(), true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnRefrescarZona_Click(object sender, EventArgs e)
        {
            try
            {
                CargarZona("", true);
                txtSearchZona.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridZona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarZona(txtSearchZona.Text.ToString().Trim(), false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridZona_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Zona

        #region Ubicacion

        private void CargarUbicacion(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarUbicacion '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridUbicacion.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridUbicacion.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnRefrescarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                CargarUbicacion("", true);
                txtSearchUbicacion.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUbicacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUbicacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Ubicacion
    }
}