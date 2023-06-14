using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Negocio.LogicaWMS;
using System.ComponentModel;
using Diverscan.MJP.Entidades.Reportes;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class wf_Reporte_CantidadPedidaTiendas : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDl();
            }
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

        private void CargarDDl()
        {
            try
            {
                RDPFechaInicial.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
                //n_SmartMaintenance.CargarDDL(ddlidDestino, e_TablasBaseDatos.VistaDestinosBexim(), UsrLogged.IdUsuario, true);
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidDestino, e_TablasBaseDatos.TblDestino(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }

        protected void btnBuscarPorFechas_Click(object sender, EventArgs e)
        {
            try
            {

                CargarDestinos(ddlidDestino.SelectedValue);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {

                ddlidDestino.Items.Clear();
                CargarDDl();
                RadGridDestinosPedido.DataSource = null;
                RadGridDestinosPedido.DataBind();
                RadGridDetallePedidoTienda.DataSource = null;
                RadGridDetallePedidoTienda.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void CargarDestinos(string buscar)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                DateTime DTfechaInicio = RDPFechaInicial.SelectedDate ?? DateTime.Now;
                DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

                string fechaInicio = DTfechaInicio.ToString("yyyyMMdd") + " 00:00:00";
                string fechaFin = DTfechaFin.ToString("yyyyMMdd") + " 23:59:59";

                if (buscar == "--Seleccionar--")
                {
                    buscar = "";
                }

                SQL = "EXEC SP_Reporte_CantidadPedidaTienda '" + buscar + "', '" + fechaInicio + "', '" + fechaFin + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridDestinosPedido.DataSource = DSDatos;

                RadGridDestinosPedido.DataBind();

            }
            catch (Exception ex)
            {
                Mensaje("error", "Error al buscar. " + ex.Message, "");
            }
        }

        private void CargarDetallePedido(string buscar, DateTime fecha)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                DateTime DTfechaInicio = RDPFechaInicial.SelectedDate ?? DateTime.Now;
                DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

                string fechaInicio = fecha.ToString("yyyyMMdd") + " 00:00:00";
                string fechaFin = fecha.ToString("yyyyMMdd") + " 23:59:59";



                SQL = "EXEC SP_Reporte_DetallePedidoTienda '" + buscar + "', '" + fechaInicio + "', '" + fechaFin + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                var lista = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridDetallePedidoTienda.DataSource = DSDatos;

                RadGridDetallePedidoTienda.DataBind();

                ViewState["EntidadPedidoTienda"] = lista;

            }
            catch (Exception ex)
            {
                Mensaje("error", "Error al buscar. " + ex.Message, "");
            }
        }



        protected void RadGridDestinosPedido_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string IdUsuario = item["idDestino"].Text.Replace("&nbsp;", "");
                    DateTime fecha = Convert.ToDateTime(item["FechaCreacion"].Text.Replace("&nbsp;", ""));
                    CargarDetallePedido(IdUsuario, fecha);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private List<Entidad_DetallePedidoTienda> _ListIEntidad_PedidoTienda
        {
            get
            {
                DataSet L1 = (DataSet)ViewState["EntidadPedidoTienda"];
                DataTable dt = L1.Tables[0];
                var data2 = new List<Entidad_DetallePedidoTienda>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data2.Add(new Entidad_DetallePedidoTienda(dt.Rows[i]));
                }
                List<DataRow> list = dt.AsEnumerable().ToList();
                var data = data2;
                //var data = ViewState["IEntidad_DespachoObtener"] as List<IEntidad_PersonalAlisto>;
                if (data == null)
                {
                    data = new List<Entidad_DetallePedidoTienda>();
                    ViewState["EntidadPedidoTienda"] = data;
                }
                return data;
            }
            set
            {
                ViewState["EntidadPedidoTienda"] = value;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

            if (_ListIEntidad_PedidoTienda.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad_DetallePedidoTienda));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                //propertiesSelected[0] = properties.Find("IdArticulo", false);
                propertiesSelected[0] = properties.Find("idMaestroSolicitud", false);
                propertiesSelected[1] = properties.Find("Destino", false);
                propertiesSelected[2] = properties.Find("Referencia", false);
                propertiesSelected[3] = properties.Find("Nombre", false);
                propertiesSelected[4] = properties.Find("Cantidad", false);
                propertiesSelected[5] = properties.Find("FechaCreacion", false);
                propertiesSelected[6] = properties.Find("FechaProcesamiento", false);
                
                string nombreArchivo = "Reporte_PedidoTienda" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //Separado por celdas

                List<string> encabezado = new List<string>() { "" };
                List<string> detalle = new List<string>() { "Solicitud", "Destino", "Referencia", "Nombre", "Cantidad", "Fecha de Creacion", "Fecha de  Procesamiento",};
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> headers = new List<List<string>>();
                //headers.Add(encabezado);
                headers.Add(saltoLinea);
                headers.Add(detalle);
                try
                {
                    ExcelExporter.ExportData(_ListIEntidad_PedidoTienda, fileName, properties, headers);
                }
                catch (Exception ex)
                {

                    Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                }

                Response.Redirect(rutaVirtual, false);

            }
            else
            {
                //Mensaje("info", "No hay datos que exportar", "");
            }
        }

    }
}