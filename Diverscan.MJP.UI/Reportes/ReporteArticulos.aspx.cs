using CrystalDecisions.CrystalReports.Engine;
using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    //public partial class ReporteArticulos : System.Web.UI.Page
    //{
    //    static List<EArticulo> listArticulos;
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

    //        }

    //        CargarDatos();

    //    }

    //    private void CargarDatos()
    //    {

    //        try
    //        {

    //            NReporte nReporte = new NReporte();
    //            listArticulos = nReporte.ObtenerArticulosYGTINReport();
    //            RadGridArticulos.DataSource = listArticulos;
    //            RadGridArticulos.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }

    //    }

    //    protected void RadGridArticulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        try
    //        {

    //            NReporte nReporte = new NReporte();
    //            listArticulos = nReporte.ObtenerArticulosYGTINReport();
    //            RadGridArticulos.DataSource = listArticulos;
    //            RadGridArticulos.DataBind();
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }
    //    }

    //    protected void btnGenerar_onClick(object sender, EventArgs e)
    //    {
    //        try
    //        {

    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EArticulo));
    //            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[6];
    //            propertiesSelected[0] = properties.Find("IdInterno", false);
    //            propertiesSelected[1] = properties.Find("Nombre", false);
    //            propertiesSelected[2] = properties.Find("GTIN13", false);
    //            propertiesSelected[3] = properties.Find("GTIN14", false);
    //            propertiesSelected[4] = properties.Find("Descripcion", false);
    //            propertiesSelected[5] = properties.Find("Contenido", false);

    //            string nombreArchivo = "Reporte_Articulos" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
    //            properties = new PropertyDescriptorCollection(propertiesSelected);
    //            var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
    //            var fileName = Server.MapPath(rutaVirtual);

    //            //Separado por celdas



    //            List<string> encabezado = new List<string>() { "" };
    //            List<string> detalle = new List<string>() { "IdInterno", "Nombre", "GTIN13", "GTIN14", "Descripcion", "Contenido", };
    //            List<string> saltoLinea = new List<string>() { };
    //            List<List<string>> headers = new List<List<string>>();
    //            //headers.Add(encabezado);
    //            headers.Add(saltoLinea);
    //            headers.Add(detalle);

    //            ExcelExporter.ExportData(listArticulos, fileName, properties, headers);

    //            DownLoad(fileName);
    //        }
    //        catch (Exception ex)
    //        {
    //            var cl = new clErrores();
    //            cl.escribirError(ex.Message, ex.StackTrace);
    //            ex.ToString();
    //        }
    //    }
    //    public void DownLoad(string FName)
    //    {
    //        try
    //        {
    //            FileInfo fileInfo = new FileInfo(FName);

    //            if (fileInfo.Exists)
    //            {
    //                Response.Clear();
    //                Response.AddHeader("Content-Disposition", "inline;attachment; filename=" + fileInfo.Name);
    //                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
    //                Response.ContentType = "application/octet-stream";
    //                Response.Flush();
    //                Response.WriteFile(fileInfo.FullName);
    //                Response.Close();
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