using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad;
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

namespace Diverscan.MJP.UI.Reportes.RotacionInventario
{
    //public partial class wf_ReporteRotacionInventario : System.Web.UI.Page
    //{
    //    private readonly IReportes _reportes;
    //    private e_Usuario UsrLogged = new e_Usuario();

    //    public wf_ReporteRotacionInventario()
    //    {
    //        _reportes = new NReporte();
    //    }

    //    private int _idBodega
    //    {
    //        get
    //        {
    //            var data = ViewState["idBodega"];
    //            if (data == null)
    //            {
    //                data = ((e_Usuario)Session["USUARIO"]).IdBodega;
    //                ViewState["idBodega"] = data;
    //            }
    //            return Int32.Parse(data.ToString());
    //        }
    //        set { ViewState["idBodega"] = value; }
    //    }
    //    private List<EListRotacionInventario> _listRotacionInventario
    //    {
    //        get
    //        {
    //            var data = ViewState["RotacionI"] as List<EListRotacionInventario>;
    //            if (data == null)
    //            {
    //                data = new List<EListRotacionInventario>();
    //                ViewState["RotacionI"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["RotacionI"] = value; }
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

    //    private void FillControls()
    //    {
    //        GetListaBodegas();
    //        SetDatetime();
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

    //    private void GetListRotacionInventario(ERotacionInventario rotacionInventario)
    //    {
    //        try
    //        {
    //            _listRotacionInventario = _reportes.ListRotacionInventarios(rotacionInventario);
    //            RGRotacionInventario.DataSource = _listRotacionInventario;
    //            RGRotacionInventario.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            Mensaje("error", "Ocurrio el error" + ex.Message, "");
    //        }
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

    //    private void GetListaBodegas()
    //    {
    //        List<EListBodega> listBodegas = _reportes.ListaBodegas();
    //        ddlBodegas.DataSource = listBodegas;
    //        ddlBodegas.DataTextField = "nombre";
    //        ddlBodegas.DataValueField = "idBodega";
    //        ddlBodegas.DataBind();
    //        ddlBodegas.Items.Insert(0, new ListItem("--Seleccione--", null));

    //    }

    //    private bool ValidarEspacios()
    //    {
    //        if (ddlBodegas.SelectedIndex < 1)
    //        {
    //            Mensaje("info", "Debe seleccionar una bodega.", "");
    //            return false;
    //        }
    //        return true;
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
    //        if (ValidarEspacios())
    //        {
    //            DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
    //            DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
    //            _idBodega = Convert.ToInt32(ddlBodegas.SelectedValue.ToString());
    //            string sku = txtSKU.Text.ToString();
    //            ERotacionInventario rotacionInventario = new ERotacionInventario()
    //            {
    //                FechaInicio = fechaInicial,
    //                FechaFin = fechaFinal,
    //                IdInterno = sku,
    //                IdBodega = _idBodega

    //            };
    //            GetListRotacionInventario(rotacionInventario);
    //        }
    //    }

    //    protected void RGRotacionInventario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGRotacionInventario.DataSource = _listRotacionInventario;
    //    }

    //    private void GenerarReporte()
    //    {
    //        FileExceptionWriter exceptionWriter = new FileExceptionWriter();
    //        try
    //        {
    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListRotacionInventario));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
    //            propertiesSelected[0] = properties.Find("UnidadesFrecuencia", false);
    //            propertiesSelected[1] = properties.Find("SKU", false);
    //            propertiesSelected[2] = properties.Find("Unidades", false);
    //            propertiesSelected[3] = properties.Find("Promedio", false);
    //            propertiesSelected[4] = properties.Find("Nombre", false);
    //            var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/temp/" + string.Format("RotacionInventario.xlsx");
    //            var fileName = Server.MapPath(rutaVirtual);
    //            List<string> headers = new List<string>() { "Unidades de Frecuencia", "SKU", "Unidades", "Promedio", "Nombre de Artículo"};
    //            ExcelExporter.ExportData(_listRotacionInventario, fileName, propertySelected, headers);
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