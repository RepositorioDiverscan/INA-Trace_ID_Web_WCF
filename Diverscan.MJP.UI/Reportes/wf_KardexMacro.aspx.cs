using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using Diverscan.MJP.Negocio.UsoGeneral;
using System.Text.RegularExpressions;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class wf_KardexMacro : System.Web.UI.Page
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
                loadArticulosIDERP();
                inicializacionElementos();
            }
        }

        #region "Objetos Requeridos"
        private n_KardexReportes _kardexReportes = new n_KardexReportes();
        #endregion

        #region "Variables de Funcionamiento"
        private static string idSAPArticuloSeleccionado; //Permite almacenar el IdInterno del Artículo Seleccionado
        private static string nombreArticuloSeleccionado;
        private static long idArticuloSeleccionado;
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        private static string totalGlobal;
        private static string unidadMedidaTotalGlobal;
        private static string fechaActualTexto; //dd-mm-yyyy            

        #endregion

        #region "Botones de búsqueda"
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            //Se obtiene el IdArticulo para ejecutar las consultas
            if (validarFechasRangoParaCargarGrid())
            {
                //lbIdArticuloTID.Text = "";
                idArticuloSeleccionado = Convert.ToInt64(ddlIdInternoArticulo.SelectedValue);
                cargarGridKardexMacro();
            }

        }


        #endregion

        #region "WebForm"


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


        protected void chkVerAjustesInventario_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkVerDespachos_CheckedChanged(object sender, EventArgs e)
        {
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
        private void inicializarVariables()
        {
            idSAPArticuloSeleccionado = ""; //Permite almacenar el IdInterno del Artículo Seleccionado         
            idArticuloSeleccionado = -1;
            fechaInicioSeleccionada = DateTime.Now;
            fechaFinSeleccionada = DateTime.Now;
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
        private bool validarFechasRangoParaNeedDataGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void ddlIDArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////nombreArticuloSeleccionado = ddlIdInternoArticulo.SelectedItem.ToString();
        }


        #endregion

        #region "Exportar a EXCEL"
        //-->Kardex macro Excel
        protected void btnExportarKardexMacro_Click(object sender, EventArgs e)
        {
            if (_KardexMacroArticulo.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_KardexMacroArticulo));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
                //propertiesSelected[0] = properties.Find("IdArticulo", false);
                //propertiesSelected[0] = properties.Find("IdInterno_Articulo", false);
                //propertiesSelected[1] = properties.Find("Nombre_Articulo", false);
                propertiesSelected[0] = properties.Find("Cantidad_Unidades_Inventario", false);
                propertiesSelected[1] = properties.Find("Unidad_medida", false);
                propertiesSelected[2] = properties.Find("Detalle_Movimiento", false);
                propertiesSelected[3] = properties.Find("Num_documento", false);
                propertiesSelected[4] = properties.Find("FechaExport", false);
                //string nombreArticulo = ddlIdInternoArticulo.Text;
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
                try
                {
                    string nombreArticulo = ddlIdInternoArticulo.SelectedItem.Text;
                    Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                    nombreArticulo = illegalInFileName.Replace(nombreArticulo, "");
                    string nombreArchivo = "Kardex Macro Art-" +
                        nombreArticulo + " " + 
                        fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                fechaActualTexto = DateTime.Now.ToShortDateString();
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);

                List<string> Encab = new List<string>() { "Artículo: " + ddlIdInternoArticulo.SelectedItem.Text };
                List<string> detalle = new List<string>() { "Cantidad", "Unidad Medida", "Detalle Movimiento", "Num.Documento", "Fecha Registro" };
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> contenidoReporte = new List<List<string>>();
                contenidoReporte.Add(Encab);
                contenidoReporte.Add(saltoLinea);
                contenidoReporte.Add(detalle);

               
                    ExcelExporter.ExportData(_KardexMacroArticulo, fileName, properties, contenidoReporte);
                    Response.Redirect(rutaVirtual, false);
                }
                catch (Exception ex)
                {
                    Mensaje("error", "No hay datos que exportar "+ex.Message, "");
                }
            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }    

        private List<e_KardexMacroArticulo> _KardexMacroArticulo
        {
            get
            {
                var data = ViewState["DataKardexMacroArticulo"] as List<e_KardexMacroArticulo>;
                if (data == null)
                {
                    data = new List<e_KardexMacroArticulo>();
                    ViewState["DataKardexMacroArticulo"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataKardexMacroArticulo"] = value;
            }
        }

        #endregion

        #region "Articulos"
        private void loadArticulosIDERP()
        {
            try
            {
                var articulos = _kardexReportes.ObtenerArticulosIdInternoERP();
                ddlIdInternoArticulo.DataTextField = "NombreArticuloIDInterno";
                ddlIdInternoArticulo.DataValueField = "IdArticuloERP";
                ddlIdInternoArticulo.DataSource = articulos;
                ddlIdInternoArticulo.DataBind();
                nombreArticuloSeleccionado = ddlIdInternoArticulo.SelectedItem.ToString();

            }
            catch (Exception)
            {
                Mensaje("error", "Ocurrio un problema al obtener los artículos por IdInterno", "");
            }

        }

        protected void ddlIdInternoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Grid KardexMacro"

        protected void radGridKardexMacro_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (validarFechasRangoParaNeedDataGrid())
                {
                    var listaRegistros = _kardexReportes.ObtenerDatosKardexMacro(idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada);
                    radGridKardexMacro.DataSource = listaRegistros;
                }
            }
            catch (Exception)
            {
                Mensaje("error", "Algo salió mal, al obtener datos de [Ajustes Inventario]", "");
            }
        }

        private void cargarGridKardexMacro()
        {                       
            try
            {
                radGridKardexMacro.DataSource = null;
                radGridKardexMacro.DataBind();
                if (validarFechasRangoParaCargarGrid())
                {
                    var listaRegistros = _kardexReportes.ObtenerDatosKardexMacro(idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada);
                    radGridKardexMacro.DataSource = listaRegistros;
                    radGridKardexMacro.DataBind();
                    _KardexMacroArticulo = listaRegistros;

                    if (listaRegistros.Count < 1)
                    {
                        Mensaje("info", "No se encontraron datos para el reporte", "");
                        return;
                    }

                    string SQL = "SELECT ISNULL(SUM(SUMACantidadEstado),0) FROM " +
                                e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaDisponibilidadArticulos() +
                                " WHERE idInterno = " + idArticuloSeleccionado;
                    lbTotalGlobal.Visible = true;
                    lbTotalGlobal.Text = n_ConsultaDummy.GetUniqueValue(SQL, "0");
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Algo salió mal, al obtener Datos del reporte: " + ex.ToString(), "");
            }
        }

        #endregion
    }
}
