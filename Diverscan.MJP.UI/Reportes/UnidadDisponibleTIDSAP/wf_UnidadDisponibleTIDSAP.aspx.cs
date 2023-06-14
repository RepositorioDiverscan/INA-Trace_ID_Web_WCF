using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad;
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

namespace Diverscan.MJP.UI.Reportes.UnidadDisponibleTIDSAP
{
    //public partial class wf_UnidadDisponibleTIDSAP : System.Web.UI.Page
    //{
    //    private readonly IReportes _reportes;
    //    private e_Usuario UsrLogged = new e_Usuario();

    //    public wf_UnidadDisponibleTIDSAP()
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
    //    private List<EListUnidadDisponibleTIDSAP> _listUnidadDiponible
    //    {
    //        get
    //        {
    //            var data = ViewState["UnidadD"] as List<EListUnidadDisponibleTIDSAP>;
    //            if (data == null)
    //            {
    //                data = new List<EListUnidadDisponibleTIDSAP>();
    //                ViewState["UnidadD"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["UnidadD"] = value; }
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

    //    private void ObternerUnidadesDisponoblesTIDSAP(EUnidadDisponibleTIDSAP unidadDisponibleTIDSAP)
    //    {
    //        try
    //        {
    //            _listUnidadDiponible = _reportes.ObternerUnidadesDisponoblesTIDSAP(unidadDisponibleTIDSAP);
    //            RGUnidadDisponible.DataSource = _listUnidadDiponible;
    //            RGUnidadDisponible.DataBind();
    //        }
    //        catch(Exception ex)
    //        {
    //            Mensaje("error", "Ha ocurrido el error: "+ex.Message, "");
    //        }
    //    }

    //    protected void btnBusqueda_Click(object sender, EventArgs e)
    //    {
    //        if (ValidarEspacios())
    //        {
    //            _idBodega = Convert.ToInt32(ddlBodegas.SelectedValue.ToString());
    //            string sku = txtSKU.Text.Trim();
    //            EUnidadDisponibleTIDSAP unidadDisponibleTIDSAP = new EUnidadDisponibleTIDSAP()
    //            {
    //                IdBodega= _idBodega,
    //                IdInterno = sku
    //            };
    //            ObternerUnidadesDisponoblesTIDSAP(unidadDisponibleTIDSAP);
    //        }
    //    }

    //    private void GenerarReporte()
    //    {
    //        FileExceptionWriter exceptionWriter = new FileExceptionWriter();
    //        try
    //        {
    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListUnidadDisponibleTIDSAP));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
    //            propertiesSelected[0] = properties.Find("IdInterno", false);
    //            propertiesSelected[1] = properties.Find("Nombre", false);
    //            propertiesSelected[2] = properties.Find("UnidadTraceID", false);
    //            propertiesSelected[3] = properties.Find("UnidadSAP", false);
    //            propertiesSelected[4] = properties.Find("Diferencia", false);
    //            var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/temp/" + string.Format("UnidadDisponibleTIDSAP.xlsx");
    //            var fileName = Server.MapPath(rutaVirtual);
    //            List<string> headers = new List<string>() { "SKU", "Nombre de Artículo", "Unidades en TraceID", "Unidades en SAP", "Diferencia" };
    //            ExcelExporter.ExportData(_listUnidadDiponible, fileName, propertySelected, headers);
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

    //    protected void RGUnidadDisponible_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGUnidadDisponible.DataSource = _listUnidadDiponible;
    //    }
   // }
}