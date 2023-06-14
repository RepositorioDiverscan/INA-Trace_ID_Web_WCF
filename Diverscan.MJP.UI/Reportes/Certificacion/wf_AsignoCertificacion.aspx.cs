using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion;
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

namespace Diverscan.MJP.UI.Reportes.Certificacion
{
    public partial class wf_AsignoCertificacion : System.Web.UI.Page
    {
        //private readonly IReportes _reportes;
        //private e_Usuario UsrLogged = new e_Usuario();

        //public wf_AsignoCertificacion()
        //{
        //    _reportes = new NReporte();
        //}

        //private List<EListAsignacionCertificacion> _listAsignacionCertificacion
        //{
        //    get
        //    {
        //        var data = ViewState["AsignacionC"] as List<EListAsignacionCertificacion>;
        //        if (data == null)
        //        {
        //            data = new List<EListAsignacionCertificacion>();
        //            ViewState["AsignacionC"] = data;
        //        }
        //        return data;
        //    }
        //    set { ViewState["AsignacionC"] = value; }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    UsrLogged = (e_Usuario)Session["USUARIO"];

        //    if (UsrLogged == null)
        //    {
        //        Response.Redirect("~/Administracion/wf_Credenciales.aspx");
        //    }
        //    if (!IsPostBack)
        //    {
        //        FillControls();
        //    }
        //}

        //private void FillControls()
        //{
        //    SetDatetime();
        //}

        //private void SetDatetime()
        //{
        //    DateTime datetime = DateTime.Now;

        //    if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
        //    {
        //        RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
        //    }
        //    else if (datetime.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
        //    }
        //    else
        //    {
        //        RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
        //    }

        //    RDPFechaInicial.SelectedDate = datetime;
        //    RDPFechaFinal.SelectedDate = datetime;

        //}

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        //}

        //void UpdatePanel1_Unload(object sender, EventArgs e)
        //{
        //    this.RegisterUpdatePanel(sender as UpdatePanel);
        //}

        //public void RegisterUpdatePanel(UpdatePanel panel)
        //{
        //    foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        //    {
        //        if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
        //        {
        //            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
        //        }
        //    }
        //}

        //private bool ValidarEspacios()
        //{
        //    if (txtUsuario.Text.ToString() == "")
        //    {
        //        Mensaje("info", "Debe ingresar un usuario.", "");
        //        return false;
        //    }
        //    return true;
        //}

        //private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        //{
        //    switch (sTipo)
        //    {
        //        case "error":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
        //            break;
        //        case "info":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
        //            break;
        //        case "ok":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
        //            break;
        //    }
        //}

        //private void GenerarReporte()
        //{
        //    FileExceptionWriter exceptionWriter = new FileExceptionWriter();
        //    try
        //    {
        //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListAsignacionCertificacion));
        //        PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[6];
        //        propertiesSelected[0] = properties.Find("SSCC", false);
        //        propertiesSelected[1] = properties.Find("Nombre", false);
        //        propertiesSelected[2] = properties.Find("Sku", false);
        //        propertiesSelected[3] = properties.Find("UnidadAsignada", false);
        //        propertiesSelected[4] = properties.Find("CantidadCertificada", false);
        //        propertiesSelected[5] = properties.Find("EstadoSSCC", false);
        //        var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
        //        var rutaVirtual = "~/temp/" + string.Format("AsignacionCertificacion.xlsx");
        //        var fileName = Server.MapPath(rutaVirtual);
        //        List<string> headers = new List<string>() { "ConsecutivoSSCC", "Nombre de Artículo", "Sku", "Unidades Asignadas", "Cantidad Certificada", "Estado del SSCC" };
        //        ExcelExporter.ExportData(_listAsignacionCertificacion, fileName, propertySelected, headers);
        //        Response.Redirect(rutaVirtual, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
        //        Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
        //    }
        //}

        //protected void btnGenerarReporte_Click(object sender, EventArgs e)
        //{
        //    GenerarReporte();
        //}

        //private void ObtenerListadoAsignacionCertificacion(EAsignacionCertificacion asignacionCertificacion)
        //{
        //    try
        //    {
        //        _listAsignacionCertificacion = _reportes.ObtenerListadoAsignacionCertificacion(asignacionCertificacion);
        //        RGAsignacionCertificacion.DataSource = _listAsignacionCertificacion;
        //        RGAsignacionCertificacion.DataBind();
                
        //    }catch(Exception ex)
        //    {
                
        //    }

        //}

        //protected void btnBusqueda_Click(object sender, EventArgs e)
        //{
        //    if (ValidarEspacios())
        //    {
        //        DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
        //        DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
        //        string usuario = txtUsuario.Text.ToString();
        //        EAsignacionCertificacion asignacionCertificacion = new EAsignacionCertificacion
        //        {
        //            FechaInicio = fechaInicial,
        //            FechaFin = fechaFinal,
        //            Usuario = usuario, 
        //            IdOla = 0
        //        };
        //        ObtenerListadoAsignacionCertificacion(asignacionCertificacion);
        //    }
        //}

        //protected void RGAsignacionCertificacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RGAsignacionCertificacion.DataSource = _listAsignacionCertificacion;
        //}
    }
}