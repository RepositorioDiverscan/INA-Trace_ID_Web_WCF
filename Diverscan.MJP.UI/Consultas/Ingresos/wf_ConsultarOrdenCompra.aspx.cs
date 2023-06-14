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

namespace Diverscan.MJP.UI.Consultas.Ingresos
{
    public partial class wf_ConsultarOrdenCompra : System.Web.UI.Page
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
                RDPFechaInicio.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
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

        #region OrdenCompra

        private void CargarOrdenCompra(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                DateTime DTfechaInicio = RDPFechaInicio.SelectedDate ?? DateTime.Now;
                DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

                string fechaInicio = DTfechaInicio.ToString("yyyyMMdd") + " 00:00:00";
                string fechaFin = DTfechaFin.ToString("yyyyMMdd") + " 23:59:59";

                SQL = "EXEC SP_BuscarMaestroOrdenCompra '" + idCompania + "', '" + buscar + "', '" + fechaInicio + "', '" + fechaFin + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridOrdenCompra.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridOrdenCompra.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarOrdenCompra()
        {
            CargarDetalleOrdenCompra("", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarOrdenCompra();
                CargarOrdenCompra(txtSearch.Text.ToString().Trim(), true);
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
                LimpiarOrdenCompra();
                CargarOrdenCompra("", true);
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridOrdenCompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarOrdenCompra(txtSearch.Text.ToString().Trim(), false);
                //CargarOrdenCompra("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridOrdenCompra_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string idMaestroOrdenCompra = item["idMaestroOrdenCompra"].Text.Replace("&nbsp;", "");

                    CargarDetalleOrdenCompra(idMaestroOrdenCompra, true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion OrdenCompra

        #region DetalleOrdenCompra
        
        
        private void CargarDetalleOrdenCompra(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarDetalleOrdenCompra '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                if (DSDatos.Tables[0].Rows.Count > 0)
                {
                    RadGridDetalleOrdenCompra.Visible = true;

                    RadGridDetalleOrdenCompra.DataSource = DSDatos;
                    _detalleOrdenCompraData = DSDatos;
                    if (pestana)
                    {
                        RadGridDetalleOrdenCompra.DataBind();
                    }
                }
                else
                {
                    RadGridDetalleOrdenCompra.Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridDetalleOrdenCompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var data = _detalleOrdenCompraData;

            if (data.Tables.Count>0 && data.Tables[0].Rows.Count > 0)
                RadGridDetalleOrdenCompra.DataSource = _detalleOrdenCompraData;
            //try
            //{
            //    CargarDetalleOrdenCompra("", false);
            //}
            //catch (Exception ex)
            //{
            //    Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            //}
        }

        protected void RadGridDetalleOrdenCompra_ItemCommand(object source, GridCommandEventArgs e)
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

        #endregion DetalleOrdenCompra

       
        private DataSet _detalleOrdenCompraData
        {
            get
            {
                var data = ViewState["detalleOrdenCompraData"] as DataSet;
                if (data == null)
                {
                    data = new DataSet();
                    ViewState["detalleOrdenCompraData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["detalleOrdenCompraData"] = value;
            }
        }
        
    }
}