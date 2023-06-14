using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.AprobarSalida;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OrdenCompa;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Diverscan.MJP.Negocio.Tareas;
using Diverscan.MJP.Entidades.Tareas;
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Entidades.Reportes.DisponibilidadPorBodega;
using System.ComponentModel;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_DisponibilidaBodegaPorPedido : System.Web.UI.Page
    {
        #region "Variables Requeridas"
        e_Usuario UsrLogged = new e_Usuario();
        string SQL = "";
        DataSet DS = new DataSet();
        static DataSet DSDatosExport = new DataSet(); //Para Grid Detalle
        static string idMaestroSolicitud = "";  //Obtener el IdPara cargar el detalle
        static string idBodega = "";
        static string textoABuscar = "";
        n_DisponibilidadArticulosPedidoBodega n_DisponibilidadArticulosPedidoBodega = new n_DisponibilidadArticulosPedidoBodega();
        #endregion

        #region "Web Form"
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                SetDatetime();
                RadGridPedidoBodega.Rebind();
                RDPFechaInicio.SelectedDate = DateTime.Now;
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
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }
        public void Mensaje(string sTipo, string sMensaje, string sLLenado)
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
        private void limpiarEtiquetasPedidoSeleccionado()
        {
            lbNumSolicitudTID.Text = "";
            lbNumeroPedidoERP.Text = "";
            lbDestino.Text = "";
            lbBodega.Text = "";
        }

        private void SetEtiquetasPedidoSeleccionado(string numSolicitudTID, string numPedidoERP, string destino, string bodega)
        {
            lbNumSolicitudTID.Text = numSolicitudTID;
            lbNumeroPedidoERP.Text = numPedidoERP;
            lbDestino.Text = destino;
            lbBodega.Text = bodega;
        }

        #endregion

        #region "Búsqueda"
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                //--->Antiguo 
                //DateTime FechaInicio = new DateTime();
                //DateTime FechaFinal = new DateTime();

                //FechaInicio = RDPFechaInicio.SelectedDate.Value;
                //FechaFinal = RDPFechaFinal.SelectedDate.Value;
                ////RDPFechaInicio.SelectedDate = RDPFechaInicio.SelectedDate.Value.AddDays(-1);

                //SQL = "EXEC SP_Filtrar_Pedidos_Numero_Pedido '" + txtSearch.Text + "', '" + FechaInicio.ToString("dd/MM/yyyy") + "','" + FechaFinal.ToString("dd/MM/yyyy") + "'";
                //DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                //RGPedidos.DataSource = new string[] { };
                //RGPedidos.DataSource = DS;
                //RGPedidos.DataBind();
                ////RDPFechaInicio.SelectedDate = RDPFechaInicio.SelectedDate.Value.AddDays(1);
                //SQL = "";
                //RadGridPedidoBodega.DataSource = null;

                //--->Implementado 
                cargarGridPedidos(txtSearch.Text);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarGridPedidos("");//Para Refrescar los datos

                txtSearch.Text = "";
                SetDatetime();
                RadGridPedidoBodega.DataSource = null;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }
        #endregion

        #region "Grid Pedidos"
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RGPedidos.DataSource = null;             

                //--->Implementado 
                DateTime FechaInicio = new DateTime();
                DateTime FechaFinal = new DateTime();

                FechaInicio = RDPFechaInicio.SelectedDate.Value;
                FechaFinal = RDPFechaFinal.SelectedDate.Value;
                //RDPFechaInicio.SelectedDate = RDPFechaInicio.SelectedDate.Value.AddDays(-1);

                //SQL = "EXEC SP_Filtrar_Pedidos_Numero_Pedido '" + txtSearch.Text + "', '" + FechaInicio.ToString("dd/MM/yyyy") + "','" + FechaFinal.ToString("dd/MM/yyyy") + "'";
                SQL = "EXEC SP_Filtrar_Pedidos_Numero_Pedido '" + textoABuscar + "', '" + FechaInicio.ToString("dd/MM/yyyy") + "','" + FechaFinal.ToString("dd/MM/yyyy") + "'";
                DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RGPedidos.DataSource = new string[] { };
                RGPedidos.DataSource = DS;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }
        protected void RGAprobarSalida_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                idMaestroSolicitud = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");
                idBodega = item["IdBodega"].Text.Replace("&nbsp;", "");
                CargarDisponiblesBodega(idMaestroSolicitud, true);

                //Set labels pedido seleccionado
                var numSolicitudTID = item["IdMaestroSolicitud"].Text.Replace("&nbsp;", "");
                var numPedidoERP = item["Solicitud"].Text.Replace("&nbsp;", "");
                var destino = item["Destino"].Text.Replace("&nbsp;", "");
                var bodega = item["Bodega"].Text.Replace("&nbsp;", "");
                SetEtiquetasPedidoSeleccionado(numSolicitudTID, numPedidoERP, destino, bodega);
            }
            else if (e.CommandName == "btnVerDetalle")
            {
                GridDataItem item = (GridDataItem)e.Item;
                idMaestroSolicitud = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");
                idBodega = item["IdBodega"].Text.Replace("&nbsp;", "");

                //Set labels pedido seleccionado
                var numSolicitudTID = item["IdMaestroSolicitud"].Text.Replace("&nbsp;", "");
                var numPedidoERP = item["Solicitud"].Text.Replace("&nbsp;", "");
                var destino = item["Destino"].Text.Replace("&nbsp;", "");
                var bodega = item["Bodega"].Text.Replace("&nbsp;", "");
                SetEtiquetasPedidoSeleccionado(numSolicitudTID, numPedidoERP, destino, bodega);

                CargarDisponiblesBodega(idMaestroSolicitud, true);
            }
        }
        private void cargarGridPedidos(string textoABuscar)
        {
            //DS.Clear();
            //SQL = "";
            //SQL = "SELECT * FROM Vista_PreDetalleSolicitudPorBodega" +
            //      "  ORDER BY Fecha DESC";
            //DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

            //RGPedidos.DataSource = new string[] { };
            //RGPedidos.DataSource = DS;
            //RGPedidos.DataBind();
            //RadGridPedidoBodega.DataSource = null;



            DateTime FechaInicio = new DateTime();
            DateTime FechaFinal = new DateTime();

            FechaInicio = RDPFechaInicio.SelectedDate.Value;
            FechaFinal = RDPFechaFinal.SelectedDate.Value;
            //RDPFechaInicio.SelectedDate = RDPFechaInicio.SelectedDate.Value.AddDays(-1);

            //SQL = "EXEC SP_Filtrar_Pedidos_Numero_Pedido '" + txtSearch.Text + "', '" + FechaInicio.ToString("dd/MM/yyyy") + "','" + FechaFinal.ToString("dd/MM/yyyy") + "'";
            SQL = "EXEC SP_Filtrar_Pedidos_Numero_Pedido '" + textoABuscar + "', '" + FechaInicio.ToString("dd/MM/yyyy") + "','" + FechaFinal.ToString("dd/MM/yyyy") + "'";
            DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

            RGPedidos.DataSource = new string[] { };
            RGPedidos.DataSource = DS;
            RGPedidos.DataBind();
            //RDPFechaInicio.SelectedDate = RDPFechaInicio.SelectedDate.Value.AddDays(1);
            SQL = "";
            RadGridPedidoBodega.DataSource = null;



        }
        private bool CheckRGAprobarSalida()
        {
            int contador = 0;
            for (int i = 0; i < RGPedidos.Items.Count; i++)
            {
                var item = RGPedidos.Items[i];
                var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;

                if (checkbox.Checked == false)
                {
                    contador++;
                }
            }

            if (contador == RGPedidos.Items.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            RDPFechaInicio.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

        }
        #endregion

        #region "Disponibilidad Articulos Bodega"
        protected void RadGridDetalleSalida_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarDisponiblesBodega(idMaestroSolicitud, false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        protected void RadGridDetalleSalida_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        private void CargarDisponiblesBodega(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                var _idBodega = Convert.ToInt32(idBodega);

                var ListaDatos = n_DisponibilidadArticulosPedidoBodega.GetListaDisponibilidadArticulosPedidoBodega(idCompania, Convert.ToInt64(buscar), _idBodega);

                if (ListaDatos.Count > 0)
                {
                    RadGridPedidoBodega.Visible = true;

                    RadGridPedidoBodega.DataSource = ListaDatos;
                    if (pestana)
                    {
                        RadGridPedidoBodega.DataBind();
                    }
                }
                else
                {
                    RadGridPedidoBodega.Visible = false;
                }
                _DetalleArticulosPedidoBodega = ListaDatos;

            }
            catch (Exception)//Entra solo cuando no se ha seleccionado una solicitud por bodega
            {
                //Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                //Mensaje("info", "Seleccione una solicitud para ver su detalle.", "");
            }
        }
        #endregion

        #region Exportar Excel
        protected void btnExportarExcelPedidosBodega_Click(object sender, EventArgs e)
        {

        }


        protected void btnExportarExcelDetallePedidoBodega_Click(object sender, EventArgs e)
        {
            if (_DetalleArticulosPedidoBodega.Count > 0)
            {


                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_DisponibilidadArticulosPedidoBodega));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[6];
                //propertiesSelected[0] = properties.Find("IdInternoArticulo", false);
                propertiesSelected[0] = properties.Find("NombreArticulo", false);
                propertiesSelected[1] = properties.Find("UnidadMedida", false);
                propertiesSelected[2] = properties.Find("CantidadEnPedido", false);
                propertiesSelected[3] = properties.Find("CantidadEnBodega", false);
                propertiesSelected[4] = properties.Find("DiferenciaBodegaPedido", false);
                propertiesSelected[5] = properties.Find("TipoArticulo", false);

                DateTime fechaIni;
                DateTime fechaFin;
                try
                { 
                    fechaIni = RDPFechaInicio.SelectedDate.Value;
                    fechaFin = RDPFechaFinal.SelectedDate.Value;
                }
                catch (Exception)
                {
                    fechaIni = DateTime.Now;
                    fechaFin = DateTime.Now;
                    //throw;
                }


                string nombreArchivo = "Detalle Pedido Bodega " + fechaIni.ToString("yyyy-MM-dd") + " a " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                string fechaActualTexto = DateTime.Now.ToShortDateString();
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "#Solicitud TID:" + lbNumSolicitudTID.Text, "#Pedido ERP:" + lbNumeroPedidoERP.Text, "Bodega:" + lbBodega.Text, "Destino:" + lbDestino.Text};
                List<string> headers = new List<string>() { "NombreArtículo", "Und. Medida", "Pedido UI", "Cant. Bodega UI", "Disponible UI", "Tipo Artículo"};
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> datosReporte = new List<List<string>>();
                datosReporte.Add(encabezado);
                datosReporte.Add(saltoLinea);
                datosReporte.Add(headers);

                ExcelExporter.ExportData(_DetalleArticulosPedidoBodega, fileName, properties, datosReporte);
                Response.Redirect(rutaVirtual, false);
            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }

        }

        private List<e_DisponibilidadArticulosPedidoBodega> _DetalleArticulosPedidoBodega
        {
            get
            {
                var data = ViewState["DetalleArticulosPedidoBodega"] as List<e_DisponibilidadArticulosPedidoBodega>;
                if (data == null)
                {
                    data = new List<e_DisponibilidadArticulosPedidoBodega>();
                    ViewState["DetalleArticulosPedidoBodega"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DetalleArticulosPedidoBodega"] = value;
            }
        }
        #endregion
    }
}