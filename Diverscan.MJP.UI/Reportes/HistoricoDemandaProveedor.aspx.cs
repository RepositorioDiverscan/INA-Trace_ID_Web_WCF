using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.HistoricoDemanda;
using Diverscan.MJP.Negocio.HistoricoDemanda;
using Diverscan.MJP.Negocio.Proveedores;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class HistoricoDemandaProveedor : System.Web.UI.Page
    {

        #region "Variables Requeridas"
        private n_KardexReportes _kardexReportes = new n_KardexReportes();
        #endregion


        #region "Web Form"
        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                LoadProveedores();
                RDPFechaInicial.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
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
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
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

        #endregion

        #region "Proveedores"
        private void LoadProveedores()
        {
            var proveedores = n_Proveedores.ObtenerTodosProveedores().OrderBy(x => x.NombreProveedor);
            ddlProveedor.DataTextField = "NombreProveedor";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataSource = proveedores;
            ddlProveedor.DataBind();

        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadArticulosIDERPPorProveedor();
        }
        #endregion

        #region "GridDemandaProveedor
        private void cargarGridDemanadaProveedor()
        {
            try
            {
                long idProveedor = Convert.ToInt64(ddlProveedor.SelectedValue);
                DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;

                var demandas = n_HistoricoDemandas.ObtenerHistoricoDemandaProveedorFechas(fechaIni, fechaFin, idProveedor);
                RadGridDemanda.DataSource = demandas;
                RadGridDemanda.DataBind();
                _historicoDemandaProveedorData = demandas;
                if (_historicoDemandaProveedorData.Count <= 0)
                {
                    Mensaje("info", "No se encontraron datos", "");
                }
                else
                {
                    Mensaje("ok", "Datos cargados correctamente", "");
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Problema al obtener los datos: " + ex.ToString(), "");              
            }            
        }

        protected void RadGridDemanda_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDemanda.DataSource = _historicoDemandaProveedorData;
        }
        #endregion

        #region "Búsqueda"
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            loadArticulosIDERPPorProveedor();
            cargarGridDemanadaProveedor();
        }
        #endregion

        #region "Exportación a excel"
        private List<HistoricoDemandaProveedorRecord> _historicoDemandaProveedorData
        {
            get
            {
                var data = ViewState["HistoricoDemandaProveedorData"] as List<HistoricoDemandaProveedorRecord>;
                if (data == null)
                {
                    data = new List<HistoricoDemandaProveedorRecord>();
                    ViewState["HistoricoDemandaProveedorData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["HistoricoDemandaProveedorData"] = value;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (_historicoDemandaProveedorData.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(HistoricoDemandaProveedorRecord));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[4];
                propertiesSelected[0] = properties.Find("IdInternoArticulo", false);
                propertiesSelected[1] = properties.Find("NombreArticulo", false);
                propertiesSelected[2] = properties.Find("Unidad_Medida", false);
                propertiesSelected[3] = properties.Find("Cantidad", false);

                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format("HistoricoDemandaProveedor.xlsx");
                var fileName = Server.MapPath(rutaVirtual);

                DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;

                List<List<string>> headersData = new List<List<string>>();
                List<string> headersRecord = new List<string>() { fechaIni.ToShortDateString() };
                headersData.Add(headersRecord);
                List<string> headersLabel = new List<string>() { "ID ERP", "Articulo", "Unidad Medida", "Cantidad" };
                headersData.Add(headersLabel);

                ExcelExporter.ExportData(_historicoDemandaProveedorData, fileName, properties, headersData);
                Response.Redirect(rutaVirtual, false);
            }
        }

        #endregion

        #region "Articulos"

        private void loadArticulosIDERPPorProveedor()
        {
            try
            {
                var articulos = _kardexReportes.ObtenerArticulosIdInternoERPPorProveedor((long)Convert.ToDecimal(ddlProveedor.SelectedValue.ToString()));
                ddlIdInternoArticulo.DataTextField = "NombreArticuloIDInterno";
                ddlIdInternoArticulo.DataValueField = "IdArticuloERP";
                ddlIdInternoArticulo.DataSource = articulos;
                ddlIdInternoArticulo.DataBind();

            }
            catch (Exception)
            {
                Mensaje("error", "Ocurrio un problema al obtener los artículos por IdInterno", "");
            }

        }

        #endregion
    }
}