using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.UbicacionSku
{
    //public partial class wf_ReporteTotalUbicacionesSku : System.Web.UI.Page
    //{
    //    e_Usuario _UsrLogged = new e_Usuario();
    //    private readonly IReportes _reportes;

    //    public wf_ReporteTotalUbicacionesSku()
    //    {
    //        _reportes = new NReporte();
    //    }

    //    private List<EListObtenerUbicacionSku> _listUbucacionSku
    //    {
    //        get
    //        {
    //            var data = ViewState["listUbucacionSku"] as List<EListObtenerUbicacionSku>;
    //            if (data == null)
    //            {
    //                data = new List<EListObtenerUbicacionSku>();
    //                ViewState["listUbucacionSku"] = data;
    //            }
    //            return data;
    //        }
    //        set
    //        {
    //            ViewState["listUbucacionSku"] = value;
    //        }
    //    }
    //    private int _idUbicacion
    //    {
    //        get
    //        {
    //            var data = ViewState["idUbicacion"];
    //            if (data == null)
    //            {
    //                data = 0;
    //                ViewState["idUbicacion"] = data;
    //            }
    //            return Int32.Parse(data.ToString());
    //        }
    //        set { ViewState["idUbicacion"] = value; }
    //    }
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        if (_UsrLogged == null)
    //        {
    //            Response.Redirect("~/Administracion/wf_Credenciales.aspx");
    //            return;
    //        }

    //        if (!IsPostBack)
    //        {
    //            CargarDropZonas(0);

    //        }
    //    }

    //    private void CargarDropZonas(int idArticulo)
    //    {
    //        try
    //        {
    //            NConsultas nConsultas = new NConsultas();
    //            ddlZonas.DataSource = nConsultas.ObtenerZonas(idArticulo);
    //            ddlZonas.DataTextField = "Nombre";
    //            ddlZonas.DataValueField = "idZona";
    //            ddlZonas.DataBind();
    //            ddlZonas.Items.Insert(0, new ListItem("--Seleccione--", "999"));
    //            ddlZonas.Items[0].Attributes["disabled"] = "disabled";
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new GestorImpresiones.Utilidades.clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }

    //    }

    //    private void CargarDropUbicaciones(int idZona)
    //    {
    //        try
    //        {

    //            EObtenerUbicacion obtenerUbicacion = new EObtenerUbicacion() { IdZona = idZona };
    //            ddlUbicacion.DataSource = _reportes.ObtenerUbicacion(obtenerUbicacion);
    //            ddlUbicacion.DataTextField = "Descripcion";
    //            ddlUbicacion.DataValueField = "IdUbicacion";
    //            ddlUbicacion.DataBind();

    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new GestorImpresiones.Utilidades.clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
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

    //    protected void ddlZonas_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        int idZona = Convert.ToInt32(ddlZonas.SelectedValue);
    //        CargarDropUbicaciones(idZona);
    //    }

    //    protected void btnBusqueda_Click(object sender, EventArgs e)
    //    {
    //        if (ValidarEspacios())
    //        {
    //            string sku = txtSKU.Text.Trim();
    //            EObtenerUbicacionSku obtenerUbicacionSku = new EObtenerUbicacionSku()
    //            {
    //                IdUbicacion = _idUbicacion,
    //                IdInterno = sku
    //            };
    //            ObtenerTotalUbicacionesXsku(obtenerUbicacionSku);
    //        }
    //    }

    //    private void ObtenerTotalUbicacionesXsku(EObtenerUbicacionSku eObtenerUbicacion)
    //    {
    //        try
    //        {
    //            _listUbucacionSku = _reportes.ObtenerTotalUbicacionesXsku(eObtenerUbicacion);
    //            RGInventario.DataSource = _listUbucacionSku;
    //            RGInventario.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new GestorImpresiones.Utilidades.clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }
    //    }
    //    protected void RGInventario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGInventario.DataSource = _listUbucacionSku;
    //    }

    //    private bool ValidarEspacios()
    //    {

    //        if (ddlZonas.SelectedIndex < 1 && txtSKU.Text.ToString() == "")
    //        {
    //            Mensaje("info", "Debe seleccionar una zona.", "");
    //            return false;
    //        }

    //        if (ddlZonas.SelectedIndex < 1)
    //        {
    //            _idUbicacion = 0;
    //        }
    //        else
    //        {
    //            _idUbicacion = Convert.ToInt32(ddlUbicacion.SelectedValue);
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

    //    private void GenerarReporte()
    //    {
    //        FileExceptionWriter exceptionWriter = new FileExceptionWriter();
    //        try
    //        {
    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EListObtenerUbicacionSku));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[4];
    //            propertiesSelected[0] = properties.Find("IdInterno", false);
    //            propertiesSelected[1] = properties.Find("Nombre", false);
    //            propertiesSelected[2] = properties.Find("Descripcion", false);
    //            propertiesSelected[3] = properties.Find("CantidadDisponible", false);
    //            var propertySelected = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/temp/" + string.Format("TotalUbicacionesSKU.xlsx");
    //            var fileName = Server.MapPath(rutaVirtual);
    //            List<string> headers = new List<string>() { "SKU", "Nombre de Articulo", "Ubicacion", "Cantidad Disponible" };
    //            ExcelExporter.ExportData(_listUbucacionSku, fileName, propertySelected, headers);
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