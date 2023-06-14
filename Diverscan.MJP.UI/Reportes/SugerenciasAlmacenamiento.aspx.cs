using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Reportes
{
    //public partial class SugerenciasAlmacenamiento : System.Web.UI.Page
    //{
    //    static List<EProductStorage> _productStorageList;
    //    e_Usuario UsrLogged = new e_Usuario();

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        UsrLogged = (e_Usuario)Session["USUARIO"];

    //        if (UsrLogged == null)
    //        {
    //            Response.Redirect("~/Administracion/wf_Credenciales.aspx");
    //        }
    //        if (!IsPostBack)
    //        {
    //            CargarDatos();
    //            FillDDBodega();
    //        }          
    //    }

    //    private void CargarDatos()
    //    {
    //        FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
    //        try
    //        {

    //            NReporte nReporte = new NReporte();
    //            List<e_MaestroArticulo> listArticulos = nReporte.GetProductsReportStorage();             

    //            RadGridArticulos.DataSource = listArticulos;
    //            RadGridArticulos.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //            fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);
    //        }
    //    }

    //    private void FillDDBodega()
    //    {
    //        NConsultas nConsultas = new NConsultas();
    //        List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
    //        ddBodega.DataSource = ListBodegas;
    //        ddBodega.DataTextField = "Nombre";
    //        ddBodega.DataValueField = "IdBodega";
    //        ddBodega.DataBind();
    //        ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
    //    }     

    //    protected void RadGridArticulos_SelectedIndexChanged(object sender, System.EventArgs e)
    //    {
    //        int idArticulo = -1, idBodega = -1;
    //        try
    //        {
    //            if (ddBodega.SelectedIndex > 0)
    //            {
    //                 idBodega = Convert.ToInt32(ddBodega.SelectedValue);
    //            }
    //            else {
    //                Mensaje("error", "¡Debe seleccionar una bodega!", "");
    //            }
               
    //            foreach (GridDataItem item in RadGridArticulos.SelectedItems)
    //            {
    //                 idArticulo = Convert.ToInt32(item["IdArticulo"].Text);                   
    //            }

    //            if (idBodega>0 && idArticulo > 0) {
    //                fillReportStorage(idArticulo,idBodega);
    //            }
                

    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }

    //    }

    //    private void fillReportStorage(int idArticulo, int idBodega)
    //    {
            
    //        RadGridProductStorage.DataSource = _productStorageList;
    //        RadGridProductStorage.DataBind();

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

    //    protected void btnGenerar_onClick(object sender, EventArgs e)
    //    {
    //        try
    //        {

    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EProductStorage));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
    //            propertiesSelected[0] = properties.Find("NameProduct", false);
    //            propertiesSelected[0] = properties.Find("Lot", false);
    //            propertiesSelected[1] = properties.Find("DateExp", false);
    //            propertiesSelected[2] = properties.Find("Description", false);
    //            propertiesSelected[3] = properties.Find("Quantity", false);
                


    //            string nombreArchivo = "Reporte_Almacenaje" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
    //            properties = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = @"~/Documents/" + string.Format(nombreArchivo);
    //            var fileName = Server.MapPath(rutaVirtual);
    //            //Separado por celdas


    //            List<string> encabezado = new List<string>() { "" };
    //            List<string> detalle = new List<string>() { "Producto", "Lote", "Fecha Exp.", "Descripción", "Cantiddad", };
    //            List<string> saltoLinea = new List<string>() { };
    //            List<List<string>> headers = new List<List<string>>();
    //            //headers.Add(encabezado);
    //            headers.Add(saltoLinea);
    //            headers.Add(detalle);

    //            ExcelExporter.ExportData(_productStorageList, fileName, properties, headers);

    //            if (!string.IsNullOrEmpty(fileName))
    //            {
    //                Response.Redirect(fileName);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }


    //    }
    //}
}