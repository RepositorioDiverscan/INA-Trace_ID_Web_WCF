using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto.Entidades;
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

namespace Diverscan.MJP.UI.Reportes.VecimientoProdcuto
{
    public partial class wf_VencimientoProducto : System.Web.UI.Page
    {
        //private readonly IReportes _reportes;
        //private e_Usuario UsrLogged = new e_Usuario();

        //public wf_VencimientoProducto()
        //{
        //    _reportes = new NReporte();
        //}

        //private int _idBodega
        //{
        //    get
        //    {
        //        var data = ViewState["idBodega"];
        //        if (data == null)
        //        {
        //            data = ((e_Usuario)Session["USUARIO"]).IdBodega;
        //            ViewState["idBodega"] = data;
        //        }
        //        return Int32.Parse(data.ToString());
        //    }
        //    set { ViewState["idBodega"] = value; }
        //}
        //private List<EListVencimientoProducto> _listVencimientoProducto
        //{
        //    get
        //    {
        //        var data = ViewState["VencimientoP"] as List<EListVencimientoProducto>;
        //        if (data == null)
        //        {
        //            data = new List<EListVencimientoProducto>();
        //            ViewState["VencimientoP"] = data;
        //        }
        //        return data;
        //    }
        //    set { ViewState["VencimientoP"] = value; }
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
        //    GetListaBodegas();
        //    SetDatetime();
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

        //public bool validarEspacios()
        //{
        //    if (ddlBodegas.SelectedIndex < 1)
        //    {
        //        Mensaje("info", "Debe seleccionar una bodega.", "");
        //        return false;
        //    }
        //    return true;
        //}

        //private void GetListVencimientoArticulos(EVencimientoProducto vencimientoProducto)
        //{
        //    try
        //    {
        //        _listVencimientoProducto = _reportes.ObtenerDiaVecimientoArticulo(vencimientoProducto);
        //        RGArticulos.DataSource = _listVencimientoProducto;
        //        RGArticulos.DataBind();
        //    }
        //    catch(Exception ex)
        //    {
        //        Mensaje("error", "Ocurrio el error" + ex.Message, "");
        //    }
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

        //private void GetListaBodegas()
        //{
        //    List<EListBodega> listBodegas = _reportes.ListaBodegas();
        //    ddlBodegas.DataSource = listBodegas;
        //    ddlBodegas.DataTextField = "nombre";
        //    ddlBodegas.DataValueField = "idBodega";
        //    ddlBodegas.DataBind();
        //    ddlBodegas.Items.Insert(0, new ListItem("--Seleccione--", null));

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

        //protected void RGArticulos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RGArticulos.DataSource = _listVencimientoProducto;
        //}

        //protected void btnBusqueda_Click(object sender, EventArgs e)
        //{
        //    if (validarEspacios())
        //    {
        //        DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
        //        DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
        //        _idBodega = Convert.ToInt32(ddlBodegas.SelectedValue.ToString());
        //        string sku = txtSKU.Text.ToString();

        //        EVencimientoProducto vencimientoProducto = new EVencimientoProducto()
        //        {
        //            FechaInicio = fechaInicial,
        //            FechaFin = fechaFinal,
        //            IdBodega = _idBodega,
        //            Sku = sku
        //        };

        //        GetListVencimientoArticulos(vencimientoProducto);

        //    }
        //}

        //private void GenerarReporte()
        //{
        //    FileExceptionWriter exceptionWriter = new FileExceptionWriter();
        //    try
        //    {
        //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListVencimientoProducto));
        //        PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
        //        propertiesSelected[0] = properties.Find("DiasVencimiento", false);
        //        propertiesSelected[1] = properties.Find("Sku", false);
        //        propertiesSelected[2] = properties.Find("NombreProducto", false);
        //        propertiesSelected[3] = properties.Find("Cantidad", false);
        //        propertiesSelected[4] = properties.Find("Descripcion", false);
        //        var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
        //        var rutaVirtual = "~/temp/" + string.Format("VencimientoProducto.xlsx");
        //        var fileName = Server.MapPath(rutaVirtual);
        //        List<string> headers = new List<string>() { "Días de Vencimiento", "SKU", "Nombre de Artículo", "Cantidad","Ubicación" };
        //        ExcelExporter.ExportData(_listVencimientoProducto, fileName, propertySelected, headers);
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
    }
}