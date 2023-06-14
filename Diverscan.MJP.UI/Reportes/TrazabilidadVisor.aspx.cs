using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Trazabilidad;
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Negocio.Trazabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class TrazabilidadVisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                inicializarElementos();
                loadArticulos();
            }
        }

        #region "Web form UI"

        protected void _rblPanelBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdDatosReporte.DataSource = null;
            rdDatosReporte.DataBind();
            RadGridDespachos.DataSource = null;
            RadGridDespachos.DataBind();
            var value = _rblPanelBusqueda.SelectedValue;
            if (value == "Fecha Recepción")
            {
                PanelFechaRecepcion.Visible = true;
                PanelLote.Visible = false;
                PanelFechaVencimiento.Visible = false;

            }
            if (value == "Lote")
            {
                PanelFechaRecepcion.Visible = false;
                PanelLote.Visible = true;
                PanelFechaVencimiento.Visible = false;
            }
            if (value == "Fecha Vencimiento")
            {
                PanelFechaRecepcion.Visible = false;
                PanelLote.Visible = false;
                PanelFechaVencimiento.Visible = true;
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

        private void inicializarElementos()
        {
            try
            {
                //Inicializar Fechas
                RDPFechaInicial.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
            }
            catch (Exception) { }            
        }
        #endregion

        #region "Articulos 

        private void loadArticulos()
        {
            var articulos = n_ArticuloGTIN.ObtenerArticulos();
            ddlIDArticulo.DataTextField = "Nombre_GTIN";
            ddlIDArticulo.DataValueField = "IdArticulo";
            ddlIDArticulo.DataSource = articulos;
            ddlIDArticulo.DataBind();
        }
        #endregion

        #region "Lotes"

        protected void ddlLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            string lote = ddlLotes.SelectedValue;
            mostrarTrazabilidadLote(idArticulo, lote);
        }

        #endregion

        #region "Grid Datos Reporte"

        private void mostrarTrazabilidadLote(long idArticulo, string lote)
        {

            //var trazabilidad = n_TRALoteFechas.GetTRALote(idArticulo, lote);
            var trazabilidad = n_TRALoteFechas.GetTRALoteV2(idArticulo, lote);
            rdDatosReporte.DataSource = trazabilidad;
            rdDatosReporte.DataBind();
            _traResumenData = trazabilidad;
            mostrarDespachosLote(idArticulo, lote);
        }
        private void mostrarTrazabilidadVencimiento(long idArticulo, DateTime fechaVencimiento)
        {

            var trazabilidad = n_TRALoteFechas.GetTRAVencimientoV2(idArticulo, fechaVencimiento);
            rdDatosReporte.DataSource = trazabilidad;
            rdDatosReporte.DataBind();
            _traResumenData = trazabilidad;
            mostrarDespachosFechaVencimiento(idArticulo, fechaVencimiento);
        }

        protected void rdDatosReporte_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rdDatosReporte.DataSource = _traResumenData;
        }
        #endregion

        #region "Grid Despachos por lote"

        protected void RadGridDespachos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDespachos.DataSource = _despachosData;
        }

        private void mostrarDespachosLote(long idArticulo, string lote)
        {
            var despachos = N_Despachos.ObtenerRepuestasCalidadPorLote(idArticulo, lote);
            RadGridDespachos.DataSource = despachos;
            RadGridDespachos.DataBind();
            _despachosData = despachos;
        }

        private void mostrarDespachosFechaVencimiento(long idArticulo, DateTime fechaVencimiento)
        {
            var despachos = N_Despachos.ObtenerRepuestasCalidadPorFechaVencimiento(idArticulo, fechaVencimiento);
            RadGridDespachos.DataSource = despachos;
            RadGridDespachos.DataBind();
            _despachosData = despachos;
        }
        #endregion

        #region "Busqueda para datos"

        protected void btnBuscarLote_Click(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            var lote = txtLote.Text;
            mostrarTrazabilidadLote(idArticulo, lote);
        }

        protected void btnFechaVencimiento_Click(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            DateTime fechavencimiento = RDPFechaVencimiento.SelectedDate.Value;
            mostrarTrazabilidadVencimiento(idArticulo, fechavencimiento);
        }

        protected void _btnBuscar_Click(object sender, EventArgs e)
        {

            rdDatosReporte.DataSource = null;
            rdDatosReporte.DataBind();
            RadGridDespachos.DataSource = null;
            RadGridDespachos.DataBind();
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
            DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;

            var lotes = N_Lotes.ObtenerLotesPorArticuloFecha(idArticulo, fechaIni, fechaFin);
            ddlLotes.DataTextField = "DataToShow";
            ddlLotes.DataValueField = "Lote";
            ddlLotes.DataSource = lotes;
            ddlLotes.DataBind();
            if (lotes.Count > 0)
            {
                string lote = ddlLotes.SelectedValue;
                mostrarTrazabilidadLote(idArticulo, lote);
            }
        }

        #endregion

        #region "Exportar a excel"

        protected void _btnExportar_Click(object sender, EventArgs e)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TraResumen));
            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[3];
            propertiesSelected[0] = properties.Find("Etiqueta", false);
            propertiesSelected[1] = properties.Find("Unidad_Medida", false);
            propertiesSelected[2] = properties.Find("Cantidad", false);

            properties = new PropertyDescriptorCollection(propertiesSelected);
            var rutaVirtual = "~/temp/" + string.Format("Trazabilidad.xlsx");
            var fileName = Server.MapPath(rutaVirtual);
            List<string> headers = new List<string>() { "Etiqueta", "Unidad Medida", "Cantidad" };
            ExcelExporter.ExportData(_traResumenData, fileName, properties, headers);
            Response.Redirect(rutaVirtual, false);
        }

        protected void btnExportarDespachos_Click(object sender, EventArgs e)
        {
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(DespachosRecord));
            //PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[3];
            //propertiesSelected[0] = properties.Find("NombreDespacho", false);
            //propertiesSelected[1] = properties.Find("FechaDespachoExport", false);
            //propertiesSelected[2] = properties.Find("Cantidad", false);

            //properties = new PropertyDescriptorCollection(propertiesSelected);
            //var rutaVirtual = "~/temp/" + string.Format("Despachos.xlsx");
            //var fileName = Server.MapPath(rutaVirtual);
            //List<string> headers = new List<string>() { "Despacho", "Fecha Despacho", "Cantidad" };
            //ExcelExporter.ExportData(_despachosData, fileName, properties, headers);
            //Response.Redirect(rutaVirtual, false);


            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(DespachosRecord));
            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[4];
            propertiesSelected[0] = properties.Find("NombreDespacho", false);
            propertiesSelected[1] = properties.Find("FechaDespachoExport", false);
            propertiesSelected[2] = properties.Find("Cantidad", false);
            propertiesSelected[3] = properties.Find("UnidadMedida", false);

            properties = new PropertyDescriptorCollection(propertiesSelected);
            var rutaVirtual = "~/temp/" + string.Format("Despachos.xlsx");
            var fileName = Server.MapPath(rutaVirtual);
            List<string> headers = new List<string>() { "Despacho", "Fecha Despacho", "Cantidad", "Unidad Medida" };
            ExcelExporter.ExportData(_despachosData, fileName, properties, headers);
            Response.Redirect(rutaVirtual, false);
        }

        private List<TraResumen> _traResumenData
        {
            get
            {
                var data = ViewState["TraResumenData"] as List<TraResumen>;
                if (data == null)
                {
                    data = new List<TraResumen>();
                    ViewState["TraResumenData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["TraResumenData"] = value;
            }
        }

        private List<DespachosRecord> _despachosData
        {
            get
            {
                var data = ViewState["DespachosData"] as List<DespachosRecord>;
                if (data == null)
                {
                    data = new List<DespachosRecord>();
                    ViewState["DespachosData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DespachosData"] = value;
            }
        }

        #endregion
    }
}