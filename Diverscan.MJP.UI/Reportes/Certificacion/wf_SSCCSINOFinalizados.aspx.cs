using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado;
using Diverscan.MJP.AccesoDatos.Reportes.Cliente;
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
    //public partial class wf_SSCCSINOFinalizados : System.Web.UI.Page
    //{
    //    private readonly IReportes _reportes;
    //    private e_Usuario UsrLogged = new e_Usuario();

    //    private List<EListSSCCSINOFinalizado> _listSSCC
    //    {
    //        get
    //        {
    //            var data = ViewState["LSSCC"] as List<EListSSCCSINOFinalizado>;
    //            if (data == null)
    //            {
    //                data = new List<EListSSCCSINOFinalizado>();
    //                ViewState["LSSCC"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["LSSCC"] = value; }
    //    }

    //    private List<EListObtenerCliente> _listClient
    //    {
    //        get
    //        {
    //            var data = ViewState["LC"] as List<EListObtenerCliente>;
    //            if (data == null)
    //            {
    //                data = new List<EListObtenerCliente>();
    //                ViewState["LC"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["LC"] = value; }
    //    }

    //    public wf_SSCCSINOFinalizados()
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

    //    private void FillControls()
    //    {
    //        SetDatetime();
    //    }

    //    private bool validarEspacio()
    //    {
    //        if (ddlCliente.SelectedIndex<1)
    //        {
    //            Mensaje("info", "Debe seleccionar un Cliente.", "");
    //            return false;
    //        }
    //        return true;
    //    }

    //    private void ObtenerListadoSSCCSINOFinalizado(ESSCCSINOFinalizado eSSCCSINOFinalizado)
    //    {
    //        try
    //        {
    //            _listSSCC = _reportes.ObtenerListadoSSCCSINOFinalizado(eSSCCSINOFinalizado);
    //            RGSSCC.DataSource = _listSSCC;
    //            RGSSCC.DataBind();
    //        }catch(Exception ex)
    //        {

    //        }
    //    }

    //    protected void btnBusqueda_Click(object sender, EventArgs e)
    //    {
    //        if (validarEspacio())
    //        {
    //            DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
    //            DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
    //            string idcliente = ddlCliente.SelectedValue.ToString();
    //            ESSCCSINOFinalizado eSSCCSINOFinalizado = new ESSCCSINOFinalizado
    //            {
    //                FehaInicio = fechaInicial,
    //                FechaFin = fechaFinal,
    //                IdCliente = idcliente
    //            };

    //            ObtenerListadoSSCCSINOFinalizado(eSSCCSINOFinalizado);
    //        }
            
    //    }

    //    protected void btnGenerarReporte_Click(object sender, EventArgs e)
    //    {
    //        GenerarReporte();
    //    }

    //    protected void RGSSCC_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGSSCC.DataSource = _listSSCC;
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

    //    private void GenerarReporte()
    //    {
    //        FileExceptionWriter exceptionWriter = new FileExceptionWriter();
    //        try
    //        {
    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListSSCCSINOFinalizado));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
    //            propertiesSelected[0] = properties.Find("Sscc", false);
    //            propertiesSelected[1] = properties.Find("Fecha", false);
    //            propertiesSelected[2] = properties.Find("NombreCliente", false);
    //            propertiesSelected[3] = properties.Find("PorCertificado", false);
    //            propertiesSelected[4] = properties.Find("IdOla", false);
    //            var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/temp/" + string.Format("SSCCSINOFinalizados.xlsx");
    //            var fileName = Server.MapPath(rutaVirtual);
    //            List<string> headers = new List<string>() { "SSCC Generado", "Fecha Creación SSCC", "Nombre del Cliente", "Porcentaje Certificado", "Número Ola" };
    //            ExcelExporter.ExportData(_listSSCC, fileName, propertySelected, headers);
    //            Response.Redirect(rutaVirtual, false);
    //        }
    //        catch (Exception ex)
    //        {
    //            exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
    //            Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
    //        }
    //    }

    //    private void ObtenerClientes(EObtenerCliente obtenerCliente)
    //    {
    //        try
    //        {
    //            _listClient = _reportes.ObtenerClientes(obtenerCliente);
    //            ddlCliente.DataSource = _listClient;
    //            ddlCliente.DataTextField = "NombreCliente";
    //            ddlCliente.DataValueField = "idCliente";
    //            ddlCliente.DataBind();
    //            ddlCliente.Items.Insert(0, new ListItem("--Seleccione--", null));
    //        }
    //        catch (Exception EX)
    //        {
    //            Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
    //        }
    //    }

    //    protected void RDPFechaFinal_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    //    {
    //        DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
    //        DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);

    //        EObtenerCliente obtenerCliente = new EObtenerCliente()
    //        {
    //            FechaInicio = fechaInicial,
    //            FechaFin = fechaFinal
    //        };

    //        ObtenerClientes(obtenerCliente);

    //    }
    //}
}