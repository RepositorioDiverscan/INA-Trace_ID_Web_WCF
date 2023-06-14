using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad;
using Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.Trazabilidad
{
    public partial class Trazabilidad : System.Web.UI.Page
    {
        private readonly ITrazabilidadDBA trazabilidadDBA;

        public Trazabilidad()
        {
            trazabilidadDBA = new TrazabilidadDBA();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                loadArticulos();
                FillDDBodega();
                inicializacionElementos();
            }
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            //ddBodega.Items[0].Attributes.Add("disabled", "disabled");
        }

        private void loadArticulos()
        {
            var articulos = n_ArticuloGTIN.ObtenerArticulos();
            ddlArticulos.DataTextField = "Nombre_GTIN";
            ddlArticulos.DataValueField = "IdArticulo";
            ddlArticulos.DataSource = articulos;
            ddlArticulos.DataBind();
            nombreArticuloSeleccionado = ddlArticulos.SelectedItem.ToString();
        }
        private void inicializacionElementos()
        {
          
            try
            {
                RDPFechaFinal.SelectedDate = DateTime.Now;
                RDPFechaInicial.SelectedDate = DateTime.Now;
            }
            catch (Exception) { }


        }
        #region "Variables de Funcionamiento"
        private static string idSAPArticuloSeleccionado; //Permite almacenar el IdInterno del Artículo Seleccionado
        private static string nombreArticuloSeleccionado;
        private static string loteSeleccionado;
        private static long idArticuloSeleccionado;
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        private static string totalGlobal;
        private static string unidadMedidaTotalGlobal;
        private static string fechaActualTexto; //dd-mm-yyyy       

        #endregion

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var articulos = trazabilidadDBA.ObtenerArticulos(TxtReferencia.Text);
            ddlArticulos.DataTextField = "Nombre_GTIN";
            ddlArticulos.DataValueField = "IdArticulo";
            ddlArticulos.DataSource = articulos;
            ddlArticulos.DataBind();
            nombreArticuloSeleccionado = ddlArticulos.SelectedItem.ToString();
            TxtReferencia.Text = ""; 
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
        private bool validarFechasRangoParaCargarGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                Mensaje("error", "Las fechas ingresadas tienen un formato incorrecto", "");
                return false;
            }
        }
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarFechasRangoParaCargarGrid()) {

                    PanelTrazabilidad.Visible = true;
                        gridReporteOrdenCompraTrazabilidad.DataSource = null;
                        gridReporteOrdenCompraTrazabilidad.DataBind();
                        idArticuloSeleccionado = Convert.ToInt64(ddlArticulos.SelectedValue);
                        int valorBodega = Convert.ToInt32(ddBodega.SelectedValue);
                        if (valorBodega == 0 || valorBodega == null)
                        {
                             Mensaje("info", "Debe seleccionar una Bodega", "");
                        return;
                        }
                        var listaRegistros = trazabilidadDBA.ObtenerDatosTrazabilidad(Convert.ToInt32(idArticuloSeleccionado), fechaInicioSeleccionada, fechaFinSeleccionada, valorBodega);
                        gridReporteOrdenCompraTrazabilidad.DataSource = listaRegistros;
                        gridReporteOrdenCompraTrazabilidad.DataBind();
                        _KardexMacroArticulo = listaRegistros;

                        if (listaRegistros.Count < 1)
                        {
                            Mensaje("info", "No se encontraron datos para el reporte de Orden de compra", "");
                          
                        }                                                         
                } 

            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<EListadoTrazabilidad> _KardexMacroArticulo
        {
            get
            {
                var data = ViewState["DataKardexMacroArticulo"] as List<EListadoTrazabilidad>;
                if (data == null)
                {
                    data = new List<EListadoTrazabilidad>();
                    ViewState["DataKardexMacroArticulo"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataKardexMacroArticulo"] = value;
            }
        }

        protected void gridReporteOrdenCompraTrazabilidad_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gridReporteOrdenCompraTrazabilidad.DataSource = _KardexMacroArticulo;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (_KardexMacroArticulo.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListadoTrazabilidad));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                propertiesSelected[0] = properties.Find("IdTrazabilidad", false);
                propertiesSelected[1] = properties.Find("Cantidad", false);
                propertiesSelected[2] = properties.Find("IdEstado", false);
                propertiesSelected[3] = properties.Find("Operacion", false);
                propertiesSelected[4] = properties.Find("FechaRegistro", false);
                propertiesSelected[5] = properties.Find("Nombre", false);
                propertiesSelected[6] = properties.Find("Saldo", false);
                string nombreArticulo = "";
                try
                {
                    nombreArticulo = nombreArticuloSeleccionado.Split('-')[0];
                    nombreArticulo = nombreArticulo.Replace('/', ' ');
                    nombreArticulo = nombreArticulo.Replace('\\', ' ');
                    nombreArticulo = nombreArticulo.Replace(';', ' ');
                    nombreArticulo = nombreArticulo.Replace('.', ' ');
                }
                catch (Exception) { }
                DateTime fechaIni;
                DateTime fechaFin;
                try
                {
                    fechaIni = RDPFechaInicial.SelectedDate.Value;
                    fechaFin = RDPFechaFinal.SelectedDate.Value;
                }
                catch (Exception)
                {
                    fechaIni = DateTime.Now;
                    fechaFin = DateTime.Now;
                    //throw;
                }
                //DateTime.Now.ToString("yyyy-MM-dd h:mm tt");


                //string nombreArchivo = "Trazabilidad_Bodega Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                string nombreArchivo = "Trazabilidad Articulo " + ddlArticulos.SelectedItem.ToString()  + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                fechaActualTexto = DateTime.Now.ToShortDateString();
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //Separado por celdas

                //List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text, "Total Global:" + lbTotalGlobal.Text };
               
                List<string> detalle = new List<string>() { "IdTrazabilidad", "Cantidad", "IdEstado", "Operacion", "Fecha Registro", "Nombre", "Saldo"};
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> headers = new List<List<string>>();
                headers.Add(saltoLinea);
                headers.Add(detalle);
                ExcelExporter.ExportData(_KardexMacroArticulo, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);

            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }
    }
}