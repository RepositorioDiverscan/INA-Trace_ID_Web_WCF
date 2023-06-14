using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.HistoricoDemanda;
using Diverscan.MJP.Negocio.HistoricoDemanda;
using Diverscan.MJP.Negocio.Proveedores;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class HistoricoDemandaV2 : System.Web.UI.Page
    {
        #region "Variables Requeridas"        
        private n_KardexReportes _kardexReportes = new n_KardexReportes();
        DateTime fechaDetalleSeleccionada;
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
                //LoadProveedores();
                loadArticulosIDERP();
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

        #region "Búsqueda"
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarGrids();
            cargarGridDemanda();
        }

        private void limpiarGrids()
        {
            RadGridDemanda.DataSource = null;
            RadGridDemanda.DataBind();
            RadGridDetalleDemanda.DataSource = null;
            RadGridDetalleDemanda.DataBind();
        }
        #endregion

        #region "Grid Demanda"
        private void cargarGridDemanda()
        {

            DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
            DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
            RadGridDemanda.DataSource = null;
            RadGridDemanda.DataBind();

            var demandas = n_HistoricoDemandas.ObtenerHistoricoDemandaArticuloIdInternoFechas(ddlIdInternoArticulo.SelectedValue.ToString(), fechaIni, fechaFin);
            RadGridDemanda.DataSource = demandas;
            RadGridDemanda.DataBind();
            _historicoDemandaData = demandas;
            lblCantidadTotal.Text = "Cantidad Total: " + demandas.Sum(x => Convert.ToDecimal(x.Cantidad)).ToString();

            if (demandas.Count < 1)
            {
                Mensaje("info", "No se encontraron datos para el rango indicado de fecha", "");
            }
            else
            {
                Mensaje("ok", "Datos cargados correctamente", "");
            }
        }

        protected void RadGridDemanda_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDemanda.DataSource = _historicoDemandaData;
        }

        private List<HistoricoDemandaRecord> _historicoDemandaData
        {
            get
            {
                var data = ViewState["HistoricoDemandaData"] as List<HistoricoDemandaRecord>;
                if (data == null)
                {
                    data = new List<HistoricoDemandaRecord>();
                    ViewState["HistoricoDemandaData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["HistoricoDemandaData"] = value;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (_historicoDemandaData.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(HistoricoDemandaRecord));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[6];
                propertiesSelected[0] = properties.Find("IdInternoArticulo", false);
                propertiesSelected[1] = properties.Find("NombreArticulo", false);
                propertiesSelected[2] = properties.Find("Fecha", false);
                propertiesSelected[3] = properties.Find("DiaSemana", false);
                propertiesSelected[4] = properties.Find("Unidad_Medida", false);
                propertiesSelected[5] = properties.Find("Cantidad", false);

                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format("HistoricoDemanda.xlsx");
                var fileName = Server.MapPath(rutaVirtual);

                List<List<string>> headersData = new List<List<string>>();
                //List<string> headersRecord = new List<string>() { lblCantidadTotal.Text };
                List<string> headersRecord = new List<string>() { "" };
                headersData.Add(headersRecord);
                List<string> headersLabel = new List<string>() { "ID ERP", "Articulo", "Fecha", "DiaSemana", "Unidad Medida", "Cantidad" };
                headersData.Add(headersLabel);

                ExcelExporter.ExportData(_historicoDemandaData, fileName, properties, headersData);
                Response.Redirect(rutaVirtual, false);
            }
        }

        protected void RadGridDemanda_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        var item = RadGridDemanda.Items[e.Item.ItemIndex];
                        if (item != null)
                        {

                            DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                            DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;

                            try
                            {// Se cae es porque el formato de fecha del servidor es diferente
                                fechaDetalleSeleccionada = Convert.ToDateTime(item["Fecha"].Text);
                                string idInterno = item["IdInternoArticulo"].Text;
                                ObtenerSolicitudRestaurantes(fechaDetalleSeleccionada, idInterno);
                                lblNombreArticulo.Text = "Articulo: " + item["NombreArticulo"].Text;
                                lblFecha.Text = "Fecha: " + item["Fecha"].Text;
                                _fechaBusquedaDetalle = fechaDetalleSeleccionada;
                            }
                            catch (Exception)
                            {//Si es distinto se procede a convertir a fecha del formato del servidor

                                DateTime dt = DateTime.ParseExact(item["Fecha"].Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                fechaDetalleSeleccionada = dt;
                                string idInterno = item["IdInternoArticulo"].Text;
                                ObtenerSolicitudRestaurantes(fechaDetalleSeleccionada, idInterno);
                                lblNombreArticulo.Text = "Articulo: " + item["NombreArticulo"].Text;
                                lblFecha.Text = "Fecha: " + item["Fecha"].Text;
                                _fechaBusquedaDetalle = fechaDetalleSeleccionada;
                            }

                        }
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        #region "Detalle Demanda"

        private List<SolicitudRestaurantesRecord> _solicitudRestaurantesData
        {
            get
            {
                var data = ViewState["SolicitudRestaurantesData"] as List<SolicitudRestaurantesRecord>;
                if (data == null)
                {
                    data = new List<SolicitudRestaurantesRecord>();
                    ViewState["SolicitudRestaurantesData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["SolicitudRestaurantesData"] = value;
            }
        }

        private DateTime _fechaBusquedaDetalle
        {
            get
            {
                var data = Convert.ToDateTime(ViewState["FechaBusquedaDetalle"]);
                //if (data == null)
                //{
                //    data = new List<SolicitudRestaurantesRecord>();
                //    ViewState["FechaBusquedaDetalle"] = data;
                //}
                return data;
            }
            set
            {
                ViewState["FechaBusquedaDetalle"] = value;
            }
        }

        protected void RadGridDetalleDemanda_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDetalleDemanda.DataSource = _solicitudRestaurantesData;
        }

        protected void btnExportarDetalle_Click(object sender, EventArgs e)
        {
            if (_solicitudRestaurantesData.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(SolicitudRestaurantesRecord));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[3];
                propertiesSelected[0] = properties.Find("NombreDespacho", false);
                propertiesSelected[1] = properties.Find("Unidad_Medida", false);
                propertiesSelected[2] = properties.Find("Cantidad", false);

                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format("DetalleDemanda.xlsx");
                var fileName = Server.MapPath(rutaVirtual);

                List<List<string>> headersData = new List<List<string>>();

                List<string> headersLabelsRecord = new List<string>() { lblNombreArticulo.Text, lblFecha.Text };
                headersData.Add(headersLabelsRecord);

                List<string> headersLabel = new List<string>() { "Punto Ventas", "Unidad Medida", "Cantidad" };
                headersData.Add(headersLabel);

                ExcelExporter.ExportData(_solicitudRestaurantesData, fileName, properties, headersData);
                Response.Redirect(rutaVirtual, false);
            }
        }

        private void ObtenerSolicitudRestaurantes(DateTime fechaInicio, string idInterno)
        {
            RadGridDetalleDemanda.DataSource = null;
            RadGridDetalleDemanda.DataBind();
            var solicitudRestaurantes = n_SolicitudRestaurantes.ObtenerSolicitudRestaurantes(fechaDetalleSeleccionada, idInterno);
            _solicitudRestaurantesData = solicitudRestaurantes;
            RadGridDetalleDemanda.DataSource = _solicitudRestaurantesData;
            RadGridDetalleDemanda.DataBind();
        }
        #endregion

        #region "Articulos"


        //Carga de artículos por IdInterno
        protected void ddlIdInternoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadArticulosIDERP()
        {
            try
            {
                var articulos = _kardexReportes.ObtenerArticulosIdInternoERP();
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