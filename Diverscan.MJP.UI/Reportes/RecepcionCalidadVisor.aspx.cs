using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Calidad;
using Diverscan.MJP.Negocio.Calidad;
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Negocio.Trazabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class RecepcionCalidadVisor : System.Web.UI.Page
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


        #region "Web Form"
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

        protected void _rblPanelBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdDatosReporte.DataSource = null;
            rdDatosReporte.DataBind();
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

        private void inicializarElementos()
        {
            try
            {
                RDPFechaInicial.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
            }
            catch (Exception) { }            
        }
        #endregion

        #region "Lotes"

        private void loadArticulos()
        {
            var articulos = n_ArticuloGTIN.ObtenerArticulos();
            ddlIDArticulo.DataTextField = "Nombre_GTIN";
            ddlIDArticulo.DataValueField = "IdArticulo";
            ddlIDArticulo.DataSource = articulos;
            ddlIDArticulo.DataBind();
        }
        protected void ddlLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            string lote = ddlLotes.SelectedValue;
            buscarPorLote(idArticulo, lote);
        }
        #endregion

        #region "Busqueda"
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
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
                buscarPorLote(idArticulo, lote);
            }
            else
            {
                Mensaje("info", "No se encontraron datos para este artículo", "");
                rdDatosReporte.DataSource = null;
                rdDatosReporte.DataBind();
            }
        }

        protected void btnBuscarLote_Click(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            string lote = txtLote.Text;
            buscarPorLote(idArticulo, lote);
        }

        protected void btnFechaVencimiento_Click(object sender, EventArgs e)
        {
            long idArticulo = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            DateTime fechavencimiento = RDPFechaVencimiento.SelectedDate.Value;
            var respuestas = n_RespuestasCalidad.ObtenerRepuestasCalidadPorFechaVencimiento(idArticulo, fechavencimiento);
            rdDatosReporte.DataSource = respuestas;
            rdDatosReporte.DataBind();
            _respuestasData = respuestas;
        }

        private void buscarPorLote(long idArticulo, string lote)
        {
            var respuestas = n_RespuestasCalidad.ObtenerRepuestasCalidadPorLote(idArticulo, lote);
            rdDatosReporte.DataSource = respuestas;
            rdDatosReporte.DataBind();
            _respuestasData = respuestas;
            if (respuestas.Count > 0)
            {
                Mensaje("ok", "Datos cargados correctamente", "");
            }

        }
        #endregion

        #region "Grid Reporte"
        protected void rdDatosReporte_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rdDatosReporte.DataSource = _respuestasData;
        }
        #endregion

        #region "Exportar a Excel"
        private List<RespuestasCalidad> _respuestasData
        {
            get
            {
                var data = ViewState["RespuestasData"] as List<RespuestasCalidad>;
                if (data == null)
                {
                    data = new List<RespuestasCalidad>();
                    ViewState["RespuestasData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["RespuestasData"] = value;
            }
        }

        protected void _btnExportar_Click(object sender, EventArgs e)
        {
            if (_respuestasData.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(RespuestasCalidad));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                propertiesSelected[0] = properties.Find("OrdenCompra", false);
                propertiesSelected[1] = properties.Find("Pregunta", false);
                propertiesSelected[2] = properties.Find("Respuesta", false);
                propertiesSelected[3] = properties.Find("Comentario", false);
                propertiesSelected[4] = properties.Find("ComentarioSeparado", false);
                propertiesSelected[5] = properties.Find("Usuario", false);
                propertiesSelected[6] = properties.Find("UsuarioAutoriza", false);

                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("RecepcionCalidad.xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> encabezado = new List<string>() { "Articulo", ddlIDArticulo.SelectedItem.Text, "Lote - FechaVencimiento", ddlLotes.SelectedItem.Text };
                List<string> saltoLinea = new List<string>() { };
                List<string> detalle = new List<string>() { "OrdenCompra", "Pregunta", "Respuesta", "Dispositivo", "Comentario", "Usuario", "Autorizado por" };
                List<List<string>> headers = new List<List<string>>();
                headers.Add(encabezado);
                headers.Add(saltoLinea);
                headers.Add(detalle);
                ExcelExporter.ExportData(_respuestasData, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
            }
        }

        #endregion

    }
}