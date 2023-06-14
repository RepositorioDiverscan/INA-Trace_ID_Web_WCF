using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OrdenCompa;
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
using System.Globalization;
using Diverscan.MJP.Utilidades.general;
using System.Text;
using Diverscan.MJP.Entidades.OrdenCompra;
using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.UI.CrystalReportes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.UI.CrystalReportes.OrdenCompra;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;

namespace Diverscan.MJP.UI.Reportes.OrdenesCompra
{
    public partial class wf_ReporteOrdenesCompras : System.Web.UI.Page
    {
        //e_Usuario UsrLogged = new e_Usuario();
        //public static DataTable DTDetalleOC = new DataTable();
        //static int IdMaestroOrden;
        //static bool EstadoProc;
        //static int GlobalidMaestroOrdenCompra;

        //private List<eOrdenCompra> _ocEncabezado
        //{
        //    get
        //    {
        //        var data = ViewState["OcEncabezado"] as List<eOrdenCompra>;
        //        if (data == null)
        //        {
        //            data = new List<eOrdenCompra>();
        //            ViewState["OcEncabezado"] = data;
        //        }
        //        return data;
        //    }
        //    set
        //    {
        //        ViewState["OcEncabezado"] = value;
        //    }
        //}

        //private List<EDetalleOrdenC> _ocDetalle
        //{
        //    get
        //    {
        //        var data = ViewState["OcDetalle"] as List<EDetalleOrdenC>;
        //        if (data == null)
        //        {
        //            data = new List<EDetalleOrdenC>();
        //            ViewState["OcDetalle"] = data;
        //        }
        //        return data;
        //    }
        //    set
        //    {
        //        ViewState["OcDetalle"] = value;
        //    }
        //}

        //private List<OCDetalleRechazo> _ocDetalleRechazo
        //{
        //    get
        //    {
        //        var data = ViewState["ocDetalleRechazo"] as List<OCDetalleRechazo>;
        //        if (data == null)
        //        {
        //            data = new List<OCDetalleRechazo>();
        //            ViewState["ocDetalleRechazo"] = data;
        //        }
        //        return data;
        //    }
        //    set
        //    {
        //        ViewState["ocDetalleRechazo"] = value;
        //    }
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
        //        //RadGridOPEINGOrdenesDeCompraARecibir.MasterTableView.GetColumn("idMaestroOrdenCompra").Display = false;
        //        RadGridOPEINGOrdenesDeCompraARecibir.Culture = System.Globalization.CultureInfo.CurrentCulture;
        //        RadGridDetalleOrden.MasterTableView.GetColumn("IdArticulo").Display = false;
        //        FillDDBodega();
        //        SetDatetime();
        //    }
        //}

        //private void SetDatetime()
        //{
        //    DateTime datetime = DateTime.Now;

        //    if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
        //    {
        //        txtFechaFinBusqueda.SelectedDate = datetime.AddDays(1).Date;
        //    }
        //    else if (datetime.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        txtFechaFinBusqueda.SelectedDate = datetime.AddDays(2).Date;
        //    }
        //    else
        //    {
        //        txtFechaFinBusqueda.SelectedDate = datetime.AddDays(1).Date;
        //    }

        //    txtFechaInicioBusqueda.SelectedDate = datetime;
        //    txtFechaFinBusqueda.SelectedDate = datetime;
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

        //// DAVID
        ////protected void btnBuscar_onClick(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        DateTime? fechaInicioBusqueda = null;
        ////        DateTime? fechaFinBusqueda = null;

        ////        string numeroOrdenCompra = txtNumeroOrdenCompra.Text;

        ////        int idBodega = Convert.ToInt32(ddBodega.SelectedValue);

        ////        if (txtFechaInicioBusqueda.SelectedDate == null && txtFechaFinBusqueda.SelectedDate == null && numeroOrdenCompra.Equals(""))
        ////        {
        ////            Mensaje("error", "¡Verifique los campos!", "");
        ////        }
        ////        else if (txtFechaInicioBusqueda.SelectedDate != null && txtFechaFinBusqueda.SelectedDate != null)
        ////        {

        ////            fechaInicioBusqueda = txtFechaInicioBusqueda.SelectedDate.Value;
        ////            fechaFinBusqueda = txtFechaFinBusqueda.SelectedDate.Value;

        ////            if ((fechaInicioBusqueda > fechaFinBusqueda))
        ////            {
        ////                Mensaje("error", "La fecha de fin de búsqueda debe ser mayor a la de inicio de búsqueda !", "");
        ////            }
        ////            else
        ////            {
        ////                buscarDatos(fechaInicioBusqueda, fechaFinBusqueda, numeroOrdenCompra, idBodega);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            buscarDatos(fechaInicioBusqueda, fechaFinBusqueda, numeroOrdenCompra, idBodega);
        ////        }

        ////        if (_ocDetalle != null)
        ////        {
        ////            _ocDetalle.Clear();
        ////            RadGridDetalleOrden.DataSource = _ocDetalle;
        ////            RadGridDetalleOrden.DataBind();

        ////            _ocDetalleRechazo.Clear();
        ////         /*   RadGridDetalleRechazo.DataSource = _ocDetalleRechazo;
        ////            RadGridDetalleRechazo.DataBind();  */
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine(ex);
        ////    }
        ////}
        ////-- DAVID
        //List<eOrdenCompra> listcompra;
        //private void buscarDatos(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        //{
        //    try
        //    {

        //        NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
        //        listcompra = negocioOperaciones.ObtenerOrdenComprasBodega(
        //            fechaInicio, fechaFin, ordenCompra, idBodega);
        //        _ocEncabezado = listcompra;
        //        //listTempcompra = listcompra;
        //        RadGridOPEINGOrdenesDeCompraARecibir.DataSource = listcompra;
        //        RadGridOPEINGOrdenesDeCompraARecibir.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        ex.ToString();
        //    }

        //}

        //protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    if (e.CommandName == RadGrid.SelectCommandName)
        //    {
        //        string sSelectedRowValue = "Primary key for the clicked item from ItemCommand: " + (e.Item as GridDataItem).GetDataKeyValue("idInterno").ToString();
        //    }
        //}


        //protected void RadGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    int idBodega;
        //    if (ddBodega.Items.Count > 0)
        //    {
        //        idBodega = (Convert.ToInt32(ddBodega.SelectedValue));
        //        foreach (GridDataItem item in RadGridOPEINGOrdenesDeCompraARecibir.SelectedItems)
        //        {
        //            CheckBox chk = item["GridCheckBoxColumn"].Controls[0] as CheckBox;
        //            /*ID Interno de Panal*/
        //            int str = Convert.ToInt32(item["idMaestroOrdenCompra"].Text);
        //            IdMaestroOrden = Convert.ToInt32(item["idMaestroOrdenCompra"].Text);
        //            EstadoProc = chk.Checked;
        //            GlobalidMaestroOrdenCompra = str;
        //            List<EDetalleOrdenC> listDetallecompra;
        //            NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
        //            listDetallecompra = negocioOperaciones.ObtenerDetalleOrdenCompras(str, idBodega);
        //            //listTempDetalle = listDetallecompra;
        //            _ocDetalle = listDetallecompra;
        //            RadGridDetalleOrden.DataSource = listDetallecompra;
        //            RadGridDetalleOrden.DataBind();

        //            if (_ocDetalleRechazo != null)
        //            {
        //                _ocDetalleRechazo.Clear();
        //              /*  RadGridDetalleRechazo.DataSource = _ocDetalleRechazo;
        //                RadGridDetalleRechazo.DataBind();   */
        //            }
        //        }
        //    }
        //}


        //protected void RadGridArticulosDisponiblesBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RadGridOPEINGOrdenesDeCompraARecibir.DataSource = _ocEncabezado;
        //}

        //protected void RadGridDetalleOrdenCompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RadGridDetalleOrden.DataSource = _ocDetalle;
        //}


        ////protected void BtnGuardarReporte_onClick(object sender, EventArgs e)
        ////{
        ////    //var path = Server.MapPath("..\\..\\CrystalReportes\\OrdenCompra.pdf");
        ////    var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\OrdenCompra" + @"\OrdenCompra.pdf";

        ////    var ocEncabezado = _ocEncabezado.Select(x => x).FirstOrDefault(x => x.idMaestroOrdenCompra == IdMaestroOrden);
        ////    if (ocEncabezado != null && _ocDetalle.Count > 0)
        ////    {
        ////        ReportDocument report = new CROrdenesCompra();
        ////        List<eOrdenCompra> listOCEncabezado = new List<eOrdenCompra>();
        ////        listOCEncabezado.Add(ocEncabezado);
        ////        var tablename = report.Database.Tables[0];
        ////        report.Database.Tables[0].SetDataSource(_ocDetalle);
        ////        report.Database.Tables[1].SetDataSource(listOCEncabezado);

        ////        ExportOptions CrExportOptions;
        ////        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        ////        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        ////        CrDiskFileDestinationOptions.DiskFileName = path;
        ////        CrExportOptions = report.ExportOptions;
        ////        {
        ////            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        ////            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        ////            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
        ////            CrExportOptions.FormatOptions = CrFormatTypeOptions;
        ////        }
        ////        report.Export();
        ////        report.Close();
        ////        report.Dispose();
        ////        GC.Collect();

        ////        Response.AddHeader("Content-Type", "application/octet-stream");
        ////        Response.AddHeader("Content-Transfer-Encoding", "Binary");
        ////        Response.AddHeader("Content-disposition", "attachment; filename=\"OrdenCompra.pdf\"");
        ////        Response.WriteFile(path);
        ////        Response.End();

        ////    }
        ////    else
        ////    {
        ////        Mensaje("info", "¡Seleccione un orden de compra!", "");
        ////    }
        ////}

        //protected void RadGridDetalleOrden_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {

        //        int IdArtilo = 0;

        //        foreach (GridDataItem item in RadGridDetalleOrden.SelectedItems)
        //        {

        //            IdArtilo = Convert.ToInt32(item["IdArticulo"].Text);

        //        }
        //        NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
        //        _ocDetalleRechazo = negocioOperaciones.ObtenerMotivoRechazo(GlobalidMaestroOrdenCompra, IdArtilo);
        //     /*   RadGridDetalleRechazo.DataSource = _ocDetalleRechazo;
        //        RadGridDetalleRechazo.DataBind();  */
        //    }
        //    catch (Exception ex)
        //    {
        //        var cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        ex.ToString();


        //    }
        //}

        //protected void RadGridDetalleRechazo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RadGridDetalleOrden.DataSource = _ocDetalleRechazo;
        //}

        //private void FillDDBodega()
        //{
        //    NConsultas nConsultas = new NConsultas();
        //    List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
        //    ddBodega.DataSource = ListBodegas;
        //    ddBodega.DataTextField = "Nombre";
        //    ddBodega.DataValueField = "IdBodega";
        //    ddBodega.DataBind();
        //    ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        //    ddBodega.Items[0].Attributes.Add("disabled", "disabled");
        //}
    }
}