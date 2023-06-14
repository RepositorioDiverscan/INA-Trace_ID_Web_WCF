using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Operacion.Salidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_VisorSSCCZonaTransitoPorDestino : System.Web.UI.Page
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
                inicializarVariables();
            }
        }

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

        private void inicializarVariables()
        {
            RDPFechaInicial.SelectedDate = DateTime.Now;
            RDPFechaFinal.SelectedDate = DateTime.Now;
            numSolicitudSelccionadaTID = -1;
            IdConsecutivoSSCCSeleccionado = -1;
        }

        private bool validarFechasSeleccionadas()
        {
            try
            {
                fechaInicioSeleccionada = Convert.ToDateTime(RDPFechaInicial.SelectedDate.ToString());
                fechaFinSeleccionada = Convert.ToDateTime(RDPFechaFinal.SelectedDate.ToString());
                return true;
            }
            catch (Exception)
            {
                Mensaje("error", "Las fechas ingresadas no tienen el formato correcto", "");
                return false;
            }
        }

        protected void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {
            cargarGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado();
            radGridSSCC_Zona_Transito_Por_Destino_Solicitud.DataSource = null;
            radGridSSCC_Zona_Transito_Por_Destino_Solicitud.DataBind();
            radGridArticulos_SSCC_Procesado.DataSource = null;
            radGridArticulos_SSCC_Procesado.DataBind();
        }
        #endregion

        #region "Variables y Objetos requeridos"

        n_OperacionesSalidas n_OperacionesSalidas = new n_OperacionesSalidas();
        private static DateTime fechaInicioSeleccionada = DateTime.Now;
        private static DateTime fechaFinSeleccionada = DateTime.Now;
        private static long numSolicitudSelccionadaTID = -1;
        private static long IdConsecutivoSSCCSeleccionado = -1;

        #endregion

        #region "Grid Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado"

        protected void radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var listaDatos = n_OperacionesSalidas.Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado(txtTextoBuscar.Text, fechaInicioSeleccionada, fechaFinSeleccionada);
                radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado.DataSource = listaDatos;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        protected void radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;                
                numSolicitudSelccionadaTID = (long)Convert.ToDouble((item["IdMaestroSolicitudTID"].Text.Replace("&nbsp;", "")).ToString());
                cargarGridSSCC_Zona_Transito_Por_Destino_Solicitud();
                radGridArticulos_SSCC_Procesado.DataSource = null;
                radGridArticulos_SSCC_Procesado.DataBind();
            }
        }

        private void cargarGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado()
        {
            try
            {
                if (validarFechasSeleccionadas())
                {
                    var listaDatos = n_OperacionesSalidas.Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado(txtTextoBuscar.Text, fechaInicioSeleccionada, fechaFinSeleccionada);
                    radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado.DataSource = listaDatos;
                    radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado.DataBind();
                }             
            }
            catch (Exception ex)
            {
                Mensaje("error", "Problema al cargar grid Destinos" + ex.ToString(), "");                
            }
        }

        #endregion

        #region "Grid SSCC_Zona_Transito_Por_Destino_Solicitud"

        protected void radGridSSCC_Zona_Transito_Por_Destino_Solicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var listaDatos = n_OperacionesSalidas.Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud(numSolicitudSelccionadaTID);
                radGridSSCC_Zona_Transito_Por_Destino_Solicitud.DataSource = listaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void radGridSSCC_Zona_Transito_Por_Destino_Solicitud_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                IdConsecutivoSSCCSeleccionado = (long)Convert.ToDouble((item["IdConsecutivoSSCC"].Text.Replace("&nbsp;", "")).ToString());
                cargarGridArticulos_SSCC_Procesado();
            }
        }

        private void cargarGridSSCC_Zona_Transito_Por_Destino_Solicitud()
        {
            try
            {                
                var listaDatos = n_OperacionesSalidas.Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud(numSolicitudSelccionadaTID);
                radGridSSCC_Zona_Transito_Por_Destino_Solicitud.DataSource = listaDatos;
                radGridSSCC_Zona_Transito_Por_Destino_Solicitud.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region "Grid Articulos_SSCC_Procesado"

        protected void radGridArticulos_SSCC_Procesado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var listaDatos = n_OperacionesSalidas.Obtener_Articulos_SSCC_Procesado(IdConsecutivoSSCCSeleccionado);
                radGridArticulos_SSCC_Procesado.DataSource = listaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarGridArticulos_SSCC_Procesado()
        {
            try
            {
                var listaDatos = n_OperacionesSalidas.Obtener_Articulos_SSCC_Procesado(IdConsecutivoSSCCSeleccionado);
                radGridArticulos_SSCC_Procesado.DataSource = listaDatos;
                radGridArticulos_SSCC_Procesado.DataBind();
            }
            catch (Exception ex)
            {           
                throw ex;
            }
        }

        #endregion       
    }
}