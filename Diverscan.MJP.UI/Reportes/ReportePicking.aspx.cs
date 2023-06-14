using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.Negocio.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using System.ComponentModel;
using Diverscan.MJP.GestorImpresiones.Utilidades;

namespace Diverscan.MJP.UI.Reportes
{

    public partial class ReportePicking : System.Web.UI.Page
    {
       static  List<EMinPicking> listMinPickings = new List<EMinPicking>();
      
        e_Usuario UsrLogged = new e_Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                FillDDBodega();
            }
        }

        private void FillDDBodega()
        {
            List<EBodega> _listBodegas = new List<EBodega>();
            NConsultas nConsultas = new NConsultas();
            _listBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = _listBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }

        protected void DDBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _idBod = Convert.ToInt32(ddBodega.SelectedValue);
            n_MaestroArticulo n_MaestroArticulo = new n_MaestroArticulo();
            listMinPickings = n_MaestroArticulo.GetMinPicking(_idBod);

            RadGridArticulos.DataSource = listMinPickings;
            RadGridArticulos.DataBind();
        }


        protected void btnGenerar_onClick(object sender, EventArgs e)
        {
            try
            {

                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(EMinPicking));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[5];
                propertiesSelected[0] = properties.Find("IdArticulo", false);
                propertiesSelected[1] = properties.Find("NombreArticulo", false);
                propertiesSelected[2] = properties.Find("CantidMinPicking", false);
                propertiesSelected[3] = properties.Find("CantidadDisponible", false);
                propertiesSelected[4] = properties.Find("Cosiente", false);
   

               string nombreArchivo = "Reporte_Picking" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //Separado por celdas


                List<string> encabezado = new List<string>() { "" };
                List<string> detalle = new List<string>() { "IdArticulo", "NombreArticulo", "CantidMinPicking", "CantidadDisponible", "Cosiente",};
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> headers = new List<List<string>>();
                //headers.Add(encabezado);
                headers.Add(saltoLinea);
                headers.Add(detalle);

                ExcelExporter.ExportData(listMinPickings, fileName, properties, headers);

                if (!string.IsNullOrEmpty(fileName))
                {
                    Response.Redirect(fileName);
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }


        }




    }
}