using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using HtmlAgilityPack;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.MaestroArticulo;
using Diverscan.MJP.Negocio.GS1;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Diverscan.MJP.Entidades.Reportes;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Text;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class Historico_OrdenC : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarGridOrdenes();
            }
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
            this.Panel1.Unload += new EventHandler(UpdatePanel_Unload);
        }

        void UpdatePanel_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
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

        private void CargarGridOrdenes()
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "SP_CONSULTA_HISTORICO_RECEPCION_V2";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RGOrdenCompra.DataSource = DSDatos;
                RGOrdenCompra.DataBind();
               
            }
            catch (Exception ex)
            {
                Mensaje1("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarFechasRangoParaCargarGrid())
                {
                    cargarGridOrdenCompraFiltro();
                }
            }
            catch (Exception)
            {

                Mensaje1("error", "Verifique que todos los datos hayan sido ingresados", "");

            }


        }

        private void cargarGridOrdenCompraFiltro()
        {
            try
            {
                RGOrdenCompra.DataSource = null;
                RGOrdenCompra.DataBind();
                if (validarFechasRangoParaCargarGrid())
                {
                    var listaRegistros = n_BusquedaOrdenC.ObtenerOrdenesC(Convert.ToInt32(TxtOC.Text), fechaInicioSeleccionada, fechaFinSeleccionada);
                    RGOrdenCompra.DataSource = listaRegistros;
                    RGOrdenCompra.DataBind();
                    //RGDevoluciones = listaRegistros;

                    if (listaRegistros.Count < 1)
                    {
                        Mensaje1("info", "No se encontraron datos para el reporte", "");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("error", "Algo salió mal, al obtener Datos del reporte: " + ex.ToString(), "");
            }
        }

        private bool validarFechasRangoParaCargarGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicio.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                Mensaje1("error", "Las fechas ingresadas tienen un formato incorrecto", "");
                return false;
            }
        }


        protected void RGOrdenCompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (validarFechasRangoParaCargarGrid())
                {
                    var listaRegistros = n_BusquedaOrdenC.ObtenerOrdenesC(Convert.ToInt32(TxtOC.Text), fechaInicioSeleccionada, fechaFinSeleccionada);
                    RGOrdenCompra.DataSource = listaRegistros;
                  

                }
            }
            catch (Exception)
            {
                Mensaje1("error", "Algo salió mal, al obtener datos de [Ajustes Inventario]", "");
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            RDPFechaFinal.Clear();
            RDPFechaInicio.Clear();
            TxtOC.Text = "";
            CargarGridOrdenes();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

            try
            {
                  PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_OrdenCE));
                   PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                   propertiesSelected[0] = properties.Find("Orden_Compra", false);
                   propertiesSelected[1] = properties.Find("Producto", false);
                   propertiesSelected[2] = properties.Find("Cantidad_Recibida", false);
                   propertiesSelected[3] = properties.Find("Cantidad_Rechazada", false);
                   propertiesSelected[4] = properties.Find("Articulos_OC", false);
                   propertiesSelected[5] = properties.Find("FechaRegistro", false);
                   propertiesSelected[6] = properties.Find("FechaCreacion", false);


                   properties = new PropertyDescriptorCollection(propertiesSelected);
                   var rutaVirtual = "~/Documentos/" + string.Format("Ordencompra.xlsx");
                   var fileName = Server.MapPath(rutaVirtual);
                   List<string> headers = new List<string>() { "Orden_Compra", "Producto", "Cantidad_Recibida", "Cantidad_Rechazada",
                   "Articulos_OC", "FechaRegistro", "FechaCreacion"};
                   ExcelExporter.ExportData(_traResumenData, fileName, properties, headers);
                   Response.Redirect(rutaVirtual, false);

                   Mensaje1("ok", "Exportando", "");

                //ExportarExcel("OrdenCompra.xls", "application/vnd.ms-excel");


            }
            catch (Exception)
            {
                Mensaje1("error", "Algo salió mal al exportar", "");

            }

        }
      
        private List<e_OrdenCE> _traResumenData
        {
            get
            {
                var data = ViewState["TraResumenData"] as List<e_OrdenCE>;
                if (data == null)
                {
                    data = new List<e_OrdenCE>();
                    ViewState["TraResumenData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["TraResumenData"] = value;
            }
        }


        private void ExportarExcel(string fileName, string contentType)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;fileName=" + fileName);
            Response.Charset = "";
            Response.ContentType = contentType;

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            RGOrdenCompra.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.Close();
            Response.End();



            //----------------------------------------------------------------------------------------------------------//
            /*
           
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_OrdenCE));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[7];
                    propertiesSelected[0] = properties.Find("Orden_Compra", false);
                    propertiesSelected[1] = properties.Find("Producto", false);
                    propertiesSelected[2] = properties.Find("Cantidad_Recibida", false);
                    propertiesSelected[3] = properties.Find("Cantidad_Rechazada", false);
                    propertiesSelected[4] = properties.Find("Articulos_OC", false);
                    propertiesSelected[5] = properties.Find("FechaRegistro", false);
                    propertiesSelected[6] = properties.Find("FechaCreacion", false);
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("OrdenCompra_" + DateTime.Now.ToLongDateString() + ".xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> headers = new List<string>() { "Orden_Compra", "Producto", "Cantidad_Recibida", "Cantidad_Rechazada",
                "Articulos_OC", "FechaRegistro", "FechaCreacion"};
     
                ExcelExporter.ExportData(_traResumenData, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
                Mensaje1("ok", "Exportando", "");*/

        }









    }
}