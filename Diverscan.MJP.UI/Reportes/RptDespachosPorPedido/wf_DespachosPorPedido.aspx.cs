using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class wf_DespachosPorPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            mostrarEnbezadoDelDetalleSeleccionPedido();
            if (!IsPostBack)
            {
                inicializarVariables();
            }
        }

        #region "Objetos para manipulacion de data"
        private n_Despachos _Despachos = new n_Despachos();

        //Variables para manejo de despachos
        private static long numSolicitud;
        private static decimal subTotal;
        private static string destinoDescripcion;
        private static string idInternoSolicitud;
        private static string idSolicitudTID;
        private static DateTime fechaPedidoOUT;//Permite almacenar la fecha del pedido obtenida al ejecutar la consulta

        //Variables para manejo de Solicitud Pedidos
        private static DateTime fechaInicioSeleccionada = DateTime.Now;
        private static DateTime fechaFinSeleccionada = DateTime.Now;
        private static long numSolicitudSeleccionada = -1;

        //Encabezado para grid DetalleSolicitud
        private static string fechaPedidoSeleccionado = "";
        private static string subTotalPedidoSeleccionado = "";
        private static string destinoPedidoSeleccionado = "";
        private static string idInternoSolicitudPedidoSeleccionado = "";

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
        private void inicializarVariables()
        {
            numSolicitud = 0;
            subTotal = 0;
            destinoDescripcion = "";
            idInternoSolicitud = "";
            idSolicitudTID = "";
            RDPFechaInicial.SelectedDate = DateTime.Now;
            RDPFechaFinal.SelectedDate = DateTime.Now;
        }
        #endregion

        #region "Exportar a EXCEL"
        //-->Exportar a excel grid Pedidos Despachadso

        private List<e_Pedidos_Despacho> _Pedidos_Despacho
        {
            get
            {
                var data = ViewState["DataPedidos_Despacho"] as List<e_Pedidos_Despacho>;
                if (data == null)
                {
                    data = new List<e_Pedidos_Despacho>();
                    ViewState["DataPedidos_Despacho"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataPedidos_Despacho"] = value;
            }
        }

        //-->Detalle Despacho Por NumSolicitud
        protected void btnExportarDetalleSolicitudDespacho_Click(object sender, EventArgs e)
        {
            if (_Detalle_Despacho_Por_Numero_Solicitud.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_Detalle_Despacho_Por_Numero_Solicitud_Reporte));

                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[9];
                propertiesSelected[0] = properties.Find("Codigo", false);                
                propertiesSelected[1] = properties.Find("Descripcion", false);                     
                propertiesSelected[2] = properties.Find("SSCCEtiqueta", false);
                propertiesSelected[3] = properties.Find("BultosUnidadMedidaConcatenado", false);
                propertiesSelected[4] = properties.Find("PedidoUI", false);
                propertiesSelected[5] = properties.Find("AlistadoUI", false);
                propertiesSelected[6] = properties.Find("UnidadMedidaUI", false);
                propertiesSelected[7] = properties.Find("Costo", false);
                propertiesSelected[8] = properties.Find("Total", false);

                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                string vehiculo = _Detalle_Despacho_Por_Numero_Solicitud[0].MarcaModeloVehiculo;
                string placa = _Detalle_Despacho_Por_Numero_Solicitud[0].Placa;
                //string nombreArchivo = "Detalle Despacho NumSolicitud[" + numSolicitud.ToString() + "] - Generado[" + fechaActual + "].xlsx";
                string nombreArchivo = "Detalle Despacho NumSolicitud[" + numSolicitudSeleccionada.ToString() + "] - Generado[" + fechaActual + "].xlsx";
                properties = new PropertyDescriptorCollection(propertiesSelected);

                var rutaVirtual = "~/temp/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);

                List<string> headerInfoPedido1 = new List<string>() { "#Solicitud TRACEID", "#Solicitud SAP", "Fecha Solicitud", "Destino Solicitud", "Placa", "Vehículo", "Sub Total" };
                //List<string> headerInfoPedido2 = new List<string>() { numSolicitud.ToString(), idInternoSolicitud.ToString(), lbFechaPedido.Text.ToString(), destinoDescripcion, placa, vehiculo, subTotal.ToString() };
                List<string> headerInfoPedido2 = new List<string>() { numSolicitudSeleccionada.ToString(), idInternoSolicitud.ToString(), lbFechaPedido.Text.ToString(), destinoDescripcion, placa, vehiculo, subTotal.ToString() };
                //List<string> headerDetalle = new List<string>() { "Código", "Lote", "Descripción", "Unidad Alisto", "UA Detalle", "SSCC", "Pedido", "Alistado" ,"UnidadMedida", "Costo", "Total" };
                List<string> headerDetalle = new List<string>() { "Código", "Descripción", "SSCC", "Bultos", "Alistado", "Pedido", "Und.", "Costo", "Total" };
                ////List<string> lineaTotal = new List<string>() { "Sub Total", subTotal.ToString() };
                List<string> saltoLinea = new List<string>() { "" };
                List<List<string>> headers = new List<List<string>>();
                headers.Add(headerInfoPedido1);
                headers.Add(headerInfoPedido2);
                headers.Add(saltoLinea);
                headers.Add(headerDetalle);
                // headers.Add(lineaTotal);
                ExcelExporter.ExportData(_Detalle_Despacho_Por_Numero_Solicitud, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }

        private List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> _Detalle_Despacho_Por_Numero_Solicitud
        {
            get
            {
                var data = ViewState["DataDetalleDespachoPorNumeroSolicitud"] as List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte>;
                if (data == null)
                {
                    data = new List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte>();
                    ViewState["DataDetalleDespachoPorNumeroSolicitud"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataDetalleDespachoPorNumeroSolicitud"] = value;
            }

        }
        #endregion

        #region "Validaciones"
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
                Mensaje("error", "Ingrese fechas válidas", "");
                return false;
            }
        }

        private bool validarNumeroSolicitud()
        {
            try
            {
                if (txtBuscarNumSolicitud.Text.Length > 0)
                {
                    numSolicitud = (long)Convert.ToDouble(txtBuscarNumSolicitud.Text.ToString());
                    if (numSolicitud <= 0)
                    {
                        Mensaje("error", "El número de solicitud debe ser mayor a 0", "");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Mensaje("error", "Debe ingresar un número de solicitud", "");
                    return false;
                }

            }
            catch (Exception)
            {
                Mensaje("error", "El número de solicitud debe ser entero sin caracteres especiales", "");
                return false;
            }
        }
        #endregion

        #region "Grid Pedidos Despacho"
        protected void chkVerSoloPedidosDespachados_CheckedChanged(object sender, EventArgs e)
        {
            cargarGridPedidosDespacho();
        }

        protected void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {
            cargarGridPedidosDespacho();
        }

        private void cargarGridPedidosDespacho()
        {
            try
            {
                if (validarFechas())
                {
                    radGridPedidosDespacho.DataSource = null;
                    radGridPedidosDespacho.DataBind();
                    var listaPedidosDespacho = _Despachos.ObtenerPedidosDespachos(fechaInicioSeleccionada, fechaFinSeleccionada, chkVerSoloPedidosDespachados.Checked);
                    radGridPedidosDespacho.DataSource = listaPedidosDespacho;
                    radGridPedidosDespacho.DataBind();
                    _Pedidos_Despacho = listaPedidosDespacho;
                    if (listaPedidosDespacho.Count <= 0)
                    {
                        Mensaje("info", "No se encontraron pedidos para el rango indicado", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Problema al obtener los pedidos: " + ex.ToString(), "");
            }
        }

        protected void radGridPedidosDespacho_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var listaPedidosDespacho = _Despachos.ObtenerPedidosDespachos(fechaInicioSeleccionada, fechaFinSeleccionada, chkVerSoloPedidosDespachados.Checked);
            radGridPedidosDespacho.DataSource = listaPedidosDespacho;
            _Pedidos_Despacho = listaPedidosDespacho;
        }

        protected void radGridPedidosDespacho_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                //idMaestroSolicitud = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");
                numSolicitudSeleccionada = (long)Convert.ToDouble((item["NumeroSolicituTID"].Text.Replace("&nbsp;", "")).ToString());
                cargarGridDetalleDespachoSolicitud();
            }
            else
            {
                //Mensaje("info", "Por favor seleccione una solicitud", "");
                //numSolicitudSeleccionada = -1;
            }
        }
        #endregion

        #region "Grid Detalle Despacho por numero Solicitud"

        protected void radGridDetalleDespachoSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                long numSolicitudABuscar = -1;
                if (numSolicitudSeleccionada == -1)
                {
                    //Si no se seleccionó ninguna solicitud se obtiene el número que esté en el campo de texto
                    if (validarNumeroSolicitud())
                    {
                        numSolicitudABuscar = (long)Convert.ToDouble(txtBuscarNumSolicitud.Text.ToString());
                    }
                }
                else
                {
                    numSolicitudABuscar = numSolicitudSeleccionada;
                }                
                var listaNumSolicitudDespachoDetalle = _Despachos.ObtenerDetalleDespachoPorNumeroSolicitudReporte(numSolicitudSeleccionada);
                radGridDetalleDespachoSolicitud.DataSource = listaNumSolicitudDespachoDetalle;
                _Detalle_Despacho_Por_Numero_Solicitud = listaNumSolicitudDespachoDetalle;

                if (listaNumSolicitudDespachoDetalle.Count < 1)
                {
                    Mensaje("info", "No se encontraron [Detalle Despacho Por Número Solicitud]", "");
                }
            }
            catch (Exception)
            {
                // Mensaje("error", "Algo salió mal, al obtener [Detalle Despacho Por Número Solicitud]", "");
            }
        }

        private void cargarGridDetalleDespachoSolicitud()
        {
            long numSolicitudABuscar = -1;
            if (numSolicitudSeleccionada == -1)
            {
                //Se valida el número de solicitud ingresado
                if (validarNumeroSolicitud())
                {
                    numSolicitudABuscar = (long)Convert.ToDouble(txtBuscarNumSolicitud.Text.ToString());
                }
            }
            else
            {
                numSolicitudABuscar = numSolicitudSeleccionada;
            }
            try
            {
                radGridDetalleDespachoSolicitud.DataSource = null;
                radGridDetalleDespachoSolicitud.DataBind();
                // var listaNumSolicitudDespachoDetalle = _Despachos.ObtenerDetalleDespachoPorNumeroSolicitud(numSolicitudABuscar, ref fechaPedidoOUT, ref destinoDescripcion, ref idInternoSolicitud, ref idSolicitudTID);
                var listaNumSolicitudDespachoDetalle = _Despachos.ObtenerDetalleDespachoPorNumeroSolicitudReporte(numSolicitudSeleccionada);
                radGridDetalleDespachoSolicitud.DataSource = listaNumSolicitudDespachoDetalle;
                radGridDetalleDespachoSolicitud.DataBind();
                _Detalle_Despacho_Por_Numero_Solicitud = listaNumSolicitudDespachoDetalle;

                if (listaNumSolicitudDespachoDetalle.Count < 1)
                {

                    Mensaje("info", "No se encontraron datos", "");
                }
                else
                {
                    fechaPedidoSeleccionado = listaNumSolicitudDespachoDetalle[0].FechaPedido.ToString();
                    subTotalPedidoSeleccionado = _Despachos.ObtenerTotalesDespachoPorNumeroSolicitudReporte(listaNumSolicitudDespachoDetalle)[0].TotalCosto.ToString();
                    destinoPedidoSeleccionado = listaNumSolicitudDespachoDetalle[0].NombreDestino;
                    idInternoSolicitudPedidoSeleccionado = listaNumSolicitudDespachoDetalle[0].IdInternoSolicitud;
                    Mensaje("ok", "Datos cargados correctamente", "");
                    mostrarEnbezadoDelDetalleSeleccionPedido();
                }
            }
            catch (Exception)
            {
                Mensaje("error", "Algo salió mal, al obtener [Detalle Despacho Por Número Solicitud]", "");
            }
        }

        protected void btnBuscarDespachoPedido_Click(object sender, EventArgs e)
        {
            cargarGridDetalleDespachoSolicitud();
        }

        private void mostrarEnbezadoDelDetalleSeleccionPedido()
        {
            lbFechaPedido.Text = fechaPedidoSeleccionado;
            lbSubTotal.Text = subTotalPedidoSeleccionado;
            lbDestinoPedido.Text = destinoPedidoSeleccionado;
            lbIdInternoSolicitud.Text = idInternoSolicitudPedidoSeleccionado;
        }

        #endregion

        #region "Exportación con formato"
        protected void btnExportarConFormatoCR_Click(object sender, EventArgs e)
        {
            if (numSolicitudSeleccionada <= 0)
            {
                Mensaje("info", "No se seleccionado ninguna solicitud", "");
            }
            else
            {
                Session["NumSolicitudSeleccionada"] = numSolicitudSeleccionada.ToString();
                //Response.Redirect("RptDespachosPorPedidoVisor.aspx"); //CrystalReport Versión.
                Response.Redirect("RptDespachosPorPedidoVisorF.aspx");
                //string pageurl = "RptDespachosPorPedidoVisorF.aspx";
                //Response.Write("<script> window.open('" + pageurl + "','_blank'); </script>");
            }
        }
        #endregion
    }
}

