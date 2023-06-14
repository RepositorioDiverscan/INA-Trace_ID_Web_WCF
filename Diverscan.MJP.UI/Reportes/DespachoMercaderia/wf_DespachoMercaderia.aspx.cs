using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.DespachoMercaderia
{
    public partial class wf_DespachoMercaderia : System.Web.UI.Page
    {
        private readonly IReportes _reportes;
        private e_Usuario UsrLogged = new e_Usuario();

        public wf_DespachoMercaderia()
        {
          //  _reportes = new NReporte();
        }

        private List<EListObtenerDespachoMercaderia> _listOlas
        {
            get
            {
                var data = ViewState["OlaD"] as List<EListObtenerDespachoMercaderia>;
                if (data == null)
                {
                    data = new List<EListObtenerDespachoMercaderia>();
                    ViewState["OlaD"] = data;
                }
                return data;
            }
            set { ViewState["OlaD"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                FillControls();
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

        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
            }
            else
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }

            RDPFechaInicial.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

        }

        private void FillControls()
        {
            SetDatetime();
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

        private void ObtenerListadoDespachoMercaderia(EObtenerDespachoMercaderia despachoMercaderia)
        {
            try
            {
                _listOlas = _reportes.ObtenerDespachoMercaderia(despachoMercaderia);
                RGOlas.DataSource = _listOlas;
                RGOlas.DataBind();
            }catch(Exception)
            {

            }
        }

        protected void RGOlas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGOlas.DataSource = _listOlas;
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
            DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
            string idOla = txtSearch.Text.Trim();

            EObtenerDespachoMercaderia despachoMercaderia = new EObtenerDespachoMercaderia
            {
                FechaInicio = fechaInicial,
                FechaFin = fechaFinal,
                IdOla = idOla
            };

            ObtenerListadoDespachoMercaderia(despachoMercaderia);
        }

        private void GenerarReporte()
        {
            FileExceptionWriter exceptionWriter = new FileExceptionWriter();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListObtenerDespachoMercaderia));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[4];
                propertiesSelected[0] = properties.Find("IdOla", false);
                propertiesSelected[1] = properties.Find("FechaDespacho", false);
                propertiesSelected[2] = properties.Find("NombreTransportista", false);
                propertiesSelected[3] = properties.Find("UnidadTransporte", false);
                var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("DespachoMercaderia.xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> headers = new List<string>() { "Número Ola", "Fecha Despacho", "Chofer Reparto", "Unidad Transporte"};
                ExcelExporter.ExportData(_listOlas, fileName, propertySelected, headers);
                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
                Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}