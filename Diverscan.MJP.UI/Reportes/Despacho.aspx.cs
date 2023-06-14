using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MensajesRespuesta;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.ComponentModel;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using Diverscan.MJP.Negocio.LogicaWMS;
using System.Data;
using Diverscan.MJP.Entidades.Reportes.Despachos;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class Despacho : System.Web.UI.Page
    {
        ReporteDespacho_factory ReporteDespacho_factory = new ReporteDespacho_factory();
        private Respuestas mensajes = new Respuestas();
        e_Usuario _eUsuario = new e_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            if (!IsPostBack)
            {
                inicializarVariables();
                CargarGridDespacho();
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

        private void Mensaje1(string sTipo, string sMensaje, string sLLenado)
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

        private void CargarGridDespacho()
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(_eUsuario.IdUsuario);

                SQL = "SP_Reporte_Despacho";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, _eUsuario.IdUsuario);

                RadGridReporteDespacho.DataSource = DSDatos;
                RadGridReporteDespacho.DataBind();

                RadGridReporteDespacho.DataSource = _ArticulosDisponiblesBodega;
                

            }
            catch (Exception ex)
            {
                Mensaje1("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTabla();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {

            inicializarVariables();
            CargarGridDespacho();

        }

        protected void RadGridReporteDespacho_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            try
            {
                CargarTabla();
                
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

        private void CargarTabla()
        {

            RadGridReporteDespacho.DataSource = ListaReporte();
            RadGridReporteDespacho.DataBind();
            
            UpdatePanel1.Update();

        }

        private DateTime fechaInicioSeleccionada = DateTime.Now;
        private DateTime fechaFinSeleccionada = DateTime.Now;


        private void inicializarVariables()
        {

            RDPFechaInicial.SelectedDate = DateTime.Now;
            RDPFechaFinal.SelectedDate = DateTime.Now;
            string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo, e_TablasBaseDatos.TblMaestroArticulos(), _eUsuario.IdUsuario, true);
            if (Msj[1] != "") Mensaje1(Msj[0], Msj[1], "");
        }

        private bool validarFechas()
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

        private List<IEntidad_Despacho> ListaReporte()
        {
            DateTime fechaInicio = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
            DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);

            //DateTime DTfechaInicio = RDPFechaInicial.SelectedDate ?? DateTime.Now;
            //DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

            //DateTime fechaInicio = Convert.ToDateTime(DTfechaInicio.ToString("yyyyMMdd") + " 00:00:00");
            //DateTime fechaFin = Convert.ToDateTime(DTfechaFin.ToString("yyyyMMdd") + " 23:59:59");

            int idArticulo = 0;
            if (ddlidArticulo.SelectedValue.ToString() == "--Seleccionar--")
            {
                idArticulo = 0;
            }
            else
            {
                idArticulo = int.Parse(ddlidArticulo.SelectedValue.ToString());
            }
            var list = ReporteDespacho_factory.build().Obtener_Reporte_Despacho(fechaInicio, fechaFinal, idArticulo, ref mensajes);
            //ViewState["DataArticulosDespacho"] = list;
            ViewState["IEntidad_DespachoObtener"] = list;
            return ListIEntidad_DespachoObtener = list;
        }

        private List<IEntidad_Despacho> ListIEntidad_DespachoObtener
        {
            get
            {
                var data = ViewState["IEntidad_DespachoObtener"] as List<IEntidad_Despacho>;
                if (data == null)
                {
                    data = new List<IEntidad_Despacho>();
                    ViewState["IEntidad_DespachoObtener"] = data;
                }
                return data;
            }
            set
            {
                ViewState["IEntidad_DespachoObtener"] = value;
            }
        }
      

        #region Exportar 

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var lista = RadGridReporteDespacho.DataSource;
            var l2 = ViewState["DataArticulosDespacho"];
            var l3 = ViewState["IEntidad_DespachoObtener"];
            //_ArticulosDisponiblesBodega = RadGridReporteDespacho.DataSource;
            if (_ArticulosDisponiblesBodega.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(IEntidad_Despacho));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                //propertiesSelected[0] = properties.Find("IdArticulo", false);
                propertiesSelected[0] = properties.Find("Solicitud", false);
                propertiesSelected[1] = properties.Find("NombreArticulo", false);
                propertiesSelected[2] = properties.Find("Referencia", false);
                propertiesSelected[3] = properties.Find("Cantidad", false);
                propertiesSelected[4] = properties.Find("SSCC", false);
                propertiesSelected[5] = properties.Find("Destino", false);
                propertiesSelected[6] = properties.Find("FechaDespacho", false);

                string nombreArchivo = "Reporte-Despachos " + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //Separado por celdas

                List<string> encabezado = new List<string>() { "" };
                List<string> detalle = new List<string>() { "Solicitud", "Nombre Articulo", "Referencia Bexim", "Cantidad", "SSCC Asociado", "Destino", "Fecha de Despacho" };
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> headers = new List<List<string>>();
                //headers.Add(encabezado);
                headers.Add(saltoLinea);
                headers.Add(detalle);
                ExcelExporter.ExportData(_ArticulosDisponiblesBodega, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);

            }
            else
            {
                //Mensaje("info", "No hay datos que exportar", "");
            }
        }

        private List<IEntidad_Despacho> _ArticulosDisponiblesBodega
        {
            get
            {
                var data = ViewState["DataArticulosDespacho"] as List<IEntidad_Despacho>;
                if (data == null)
                {
                    data = new List<IEntidad_Despacho>();
                    ViewState["DataArticulosDespacho"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataArticulosDespacho"] = value;
            }
        }

        #endregion

    }
}