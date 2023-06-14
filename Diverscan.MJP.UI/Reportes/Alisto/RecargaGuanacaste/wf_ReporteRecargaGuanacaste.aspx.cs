using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.RecargaGuanacaste.Entidad;
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

namespace Diverscan.MJP.UI.Reportes.Alisto.RecargaGuanacaste
{
    //public partial class wf_ReporteRecargaGuanacaste : System.Web.UI.Page
    //{
    //    private readonly IReportes _reportes;
    //    private e_Usuario UsrLogged = new e_Usuario();

    //    private List<EListObtenerRecargaBodegaGuanacaste> _listOlas
    //    {
    //        get
    //        {
    //            var data = ViewState["RecargaG"] as List<EListObtenerRecargaBodegaGuanacaste>;
    //            if (data == null)
    //            {
    //                data = new List<EListObtenerRecargaBodegaGuanacaste>();
    //                ViewState["RecargaG"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["RecargaG"] = value; }
    //    }

    //    public wf_ReporteRecargaGuanacaste()
    //    {
    //        _reportes = new NReporte();
    //    }

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        UsrLogged = (e_Usuario)Session["USUARIO"];

    //        if (UsrLogged == null)
    //        {
    //            Response.Redirect("~/Administracion/wf_Credenciales.aspx");
    //        }

    //        if (!IsPostBack)
    //        {
    //            FillControls();
    //        }
    //    }

    //    protected override void OnInit(EventArgs e)
    //    {
    //        base.OnInit(e);
    //        this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
    //    }

    //    void UpdatePanel1_Unload(object sender, EventArgs e)
    //    {
    //        this.RegisterUpdatePanel(sender as UpdatePanel);
    //    }

    //    public void RegisterUpdatePanel(UpdatePanel panel)
    //    {
    //        foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
    //        {
    //            if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
    //            {
    //                methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
    //            }
    //        }
    //    }

    //    private void FillControls()
    //    {
    //        SetDatetime();
    //    }

    //    private void SetDatetime()
    //    {
    //        DateTime datetime = DateTime.Now;

    //        if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
    //        }
    //        else if (datetime.DayOfWeek == DayOfWeek.Saturday)
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
    //        }
    //        else
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
    //        }

    //        RDPFechaInicial.SelectedDate = datetime;
    //        RDPFechaFinal.SelectedDate = datetime;

    //    }

    //    private void ObtenerRecargaBodegaGuanacaste(EObtenerRecargaGuanacaste eObtenerRecargaGuanacaste)
    //    {
    //        try
    //        {
    //            _listOlas =  _reportes.ObtenerRecargaBodegaGuanacaste(eObtenerRecargaGuanacaste);

    //            RGRecargaGuanacaste.DataSource = _listOlas;

    //            RGRecargaGuanacaste.DataBind();

    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //    protected void RGRecargaGuanacaste_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGRecargaGuanacaste.DataSource = _listOlas;
    //    }

    //    private void Mensaje(string sTipo, string sMensaje, string sLLenado)
    //    {
    //        switch (sTipo)
    //        {
    //            case "error":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
    //                break;
    //            case "info":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
    //                break;
    //            case "ok":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
    //                break;
    //        }
    //    }

    //    protected void btnBusqueda_Click(object sender, EventArgs e)
    //    {
    //        DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
    //        DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);

    //        EObtenerRecargaGuanacaste eObtenerRecargaGuanacaste = new EObtenerRecargaGuanacaste()
    //        {
    //            FechaInicio = fechaInicial,
    //            FechaFin = fechaFinal
    //        };

    //        ObtenerRecargaBodegaGuanacaste(eObtenerRecargaGuanacaste);
    //    }

    //    private void GenerarReporte()
    //    {
    //        FileExceptionWriter exceptionWriter = new FileExceptionWriter();
    //        try
    //        {
    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListObtenerRecargaBodegaGuanacaste));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
    //            propertiesSelected[0] = properties.Find("NumeroOla", false);
    //            propertiesSelected[1] = properties.Find("Fecha", false);
    //            propertiesSelected[2] = properties.Find("Sku", false);
    //            propertiesSelected[3] = properties.Find("NombreArticulo", false);
    //            propertiesSelected[4] = properties.Find("Unidades", false);
    //            var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/temp/" + string.Format("RecargaGuanacaste.xlsx");
    //            var fileName = Server.MapPath(rutaVirtual);
    //            List<string> headers = new List<string>() { "Número de Ola", "Fecha Creación", "SKU", "Nombre de Artículo", "Unidades" };
    //            ExcelExporter.ExportData(_listOlas, fileName, propertySelected, headers);
    //            Response.Redirect(rutaVirtual, false);
    //        }
    //        catch (Exception ex)
    //        {
    //            exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
    //            Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
    //        }
    //    }

    //    protected void btnGenerarReporte_Click(object sender, EventArgs e)
    //    {
    //        GenerarReporte();
    //    }
    //}
}