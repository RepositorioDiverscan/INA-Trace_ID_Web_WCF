using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Telerik.Web.UI;
using Diverscan.MJP.UI.CrystalReportes.DisponibleBodega;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;

namespace Diverscan.MJP.UI.Consultas.Administracion
{
    public partial class DisponibleBodegaNew : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            e_Usuario UsrLogged = new e_Usuario();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            else
            {
                IdBodega = UsrLogged.IdBodega;
            }

            if (!IsPostBack)
            {
                CargarDropProveedores();
           
            }

            //Disable the default item.
            DDropDownProve.Items[0].Attributes["disabled"] = "disabled";
        }


        private int IdBodega 
        {
            get
            {
                var idBodega = -1;
                var data = ViewState["IdBodega "];
                if (data != null)
                {
                    var result = int.TryParse(data.ToString(), out idBodega);
                    if(result)
                        ViewState["IdBodega "] = idBodega;
                }
                return idBodega;
            }
            set
            {
                ViewState["IdBodega "] = value;
            }
        }
        private void CargarDropProveedores()
        {
            NConsultas nConsultas = new NConsultas();
            List<EProveedor> ListTodosProvee = new List<EProveedor>();

            ListTodosProvee = nConsultas.ObtenerProveedores();


            DDropDownProve.DataSource = ListTodosProvee;
            DDropDownProve.DataTextField = "Nombre";
            DDropDownProve.DataValueField = "IdProveedor";
            DDropDownProve.DataBind();
            DDropDownProve.Items.Insert(0, new ListItem("--Seleccione--", "0"));

        }
        private void CargarDropArticulos(int idProv)
        {
            NConsultas nConsultas = new NConsultas();
            //     List<EArticulo> ListTodosArticul = new List<EArticulo>();
            ListTodosArticul = nConsultas.ObtenerArticulos(idProv);
            DropDownListArticulo.DataSource = ListTodosArticul;
            DropDownListArticulo.DataTextField = "Nombre";
            DropDownListArticulo.DataValueField = "IdArticulo";
            DropDownListArticulo.DataBind();
            DropDownListArticulo.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            DropDownListArticulo.Items[0].Attributes["disabled"] = "disabled";
        }
        protected void DDropDownProve_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargarDropArticulos(Convert.ToInt32(DDropDownProve.SelectedValue));

        }
        protected void DDropDownArti_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargarDropZonas(Convert.ToInt32(DropDownListArticulo.SelectedValue));
            lblNombreArticulo.Text = "";
            txtIdInterno.Text = "";
        }
        private void CargarDropZonas(int idArticulo)
        {
            try
            {
                NConsultas nConsultas = new NConsultas();

                ListZonas = nConsultas.ObtenerZonas(idArticulo);
                DropDownListZona.DataSource = ListZonas;
                DropDownListZona.DataTextField = "Nombre";
                DropDownListZona.DataValueField = "idZona";
                DropDownListZona.DataBind();
                int ZonasSize = ListZonas.Count + 1;
                DropDownListZona.Items.Insert(0, new ListItem("--Seleccione--", "999"));
                DropDownListZona.Items[0].Attributes["disabled"] = "disabled";
                DropDownListZona.Items.Insert(ZonasSize, new ListItem("--Todas--", "0"));
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }
        protected void DDropDownZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            EZona datos = new EZona();


            datos.Nombre = DropDownListZona.SelectedItem.Text;
            datos.IdZona = Convert.ToInt32(DropDownListZona.SelectedValue);
            bool alreadyExists = listaZonas.Any(x => x.Nombre == DropDownListZona.SelectedItem.Text);
            if (Convert.ToInt32(DropDownListZona.SelectedValue) == 0 || Convert.ToInt32(DropDownListZona.SelectedValue) == 999)
            {
                listaZonas = ListZonas;
            }
            else if (alreadyExists == false)
            {
                listaZonas.Add(datos);
            }
            RadGridZonas.DataSource = listaZonas;
            RadGridZonas.DataBind();
        }
        protected void btnGenerarReporte(object sender, EventArgs e)
        {
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\DisponibleBodega" + @"\DisponibleBodega.pdf";
            try
            {

                if (listArticulos.Count > 0)
                {
                    ReportDocument report = new CRDisponibleBodega();
                    report.Database.Tables[0].SetDataSource(listArticulos);

                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOtions = new PdfRtfWordFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = path;
                    CrExportOptions = report.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOtions;
                    }
                    report.Export();
                    report.Close();
                    report.Dispose();
                    GC.Collect();

                    Response.AddHeader("Content-Type", "application/octet-stream");
                    Response.AddHeader("Content-Transfer-Encoding", "Binary");
                    Response.AddHeader("Content-disposition", "attachment; filename=\"DisponibleBodega.pdf\"");
                    Response.WriteFile(path);
                    Response.End();
                }
                else
                {
                    Mensaje("info", "¡Seleccione un proveedor!", "");
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }
        private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        {
            switch (sTipo)
            {
                case "error":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
                    break;
                case "info":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
                    break;
                case "ok":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
                    break;
            }
        }
        protected void BtnAceptar_onClick(object sender, EventArgs e)
        {
            try
            {
                int idarticulo=0;
                if (!CheckSinArticulo.Checked)
                {
                    var ddlarticulo = DropDownListArticulo.SelectedValue;
                    if (!string.IsNullOrEmpty(ddlarticulo) && ddlarticulo != "0")
                        idarticulo = Convert.ToInt32(DropDownListArticulo.SelectedValue);
                    else
                        idarticulo = ListArticulosId.Count > 0 ? ListArticulosId[0].IdArticulo : 0;
                }
                
                NConsultas nConsultas = new NConsultas();
                var data = nConsultas.ObtenerArticulosDisponiblesXzonas(idarticulo, listaZonas, IdBodega);
                RadGridArticulosDisponibles.DataSource = data;
                RadGridArticulosDisponibles.DataBind();
                listArticulos = data;

                RadGridZonas.DataSource = listaZonas;
                RadGridZonas.DataBind();

            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }
        protected void btnBuscarIdInterno_Click(object sender, EventArgs e)
        {
            try
            {
                NArticulos nArticulos = new NArticulos();
                var articuloInfo = nArticulos.ObtenerIdArticulos(txtIdInterno.Text);
                if (articuloInfo == null)
                    return;
                CargarDropZonas(articuloInfo.IdArticulo);
                NConsultas nConsultas = new NConsultas();
                DropDownListArticulo.SelectedIndex = 0;
                var articulos = new List<EArticuloId>();
                articulos.Add(articuloInfo);                    
                ListArticulosId = articulos;

                lblNombreArticulo.Text = CargarNombreArticulo(txtIdInterno.Text);
                Session["NombreArticulo"] = lblNombreArticulo.Text;


            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }
        protected void RadGridZonas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGridZonas.DataSource = listaZonas;
                RadGridZonas.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();


            }
        }
        protected void RadGridArticulos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGridArticulosDisponibles.DataSource = listArticulos;
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();


            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            RadButton Button = (RadButton)sender;
            GridDataItem item = (GridDataItem)Button.NamingContainer;
            //string drawingID = item["Nombre"].Text;

            listaZonas.RemoveAt(item.ItemIndex);
            RadGridZonas.DataSource = listaZonas;
            RadGridZonas.DataBind();

        }
        private string CargarNombreArticulo(string idInterno)
        {
            NArticulos nArticulos = new NArticulos();
            string nombre = nArticulos.ObtenerNombreArticulos(txtIdInterno.Text).Nombre;
            return nombre;
        }
        private List<EArticulo> ListTodosArticul
        {
            get
            {
                var data = ViewState["ListAllProducts"] as List<EArticulo>;
                if (data == null)
                {
                    data = new List<EArticulo>();
                    ViewState["ListAllProducts"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListAllProducts"] = value;
            }
        }
        private List<EArticulo> listArticulos
        {
            get
            {
                var data = ViewState["listArticulos"] as List<EArticulo>;
                if (data == null)
                {
                    data = new List<EArticulo>();
                    ViewState["listArticulos"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listArticulos"] = value;
            }
        }
        private List<EArticuloId> ListArticulosId
        {
            get
            {
                var data = ViewState["ListEArticuloId"] as List<EArticuloId>;
                if (data == null)
                {
                    data = new List<EArticuloId>();
                    ViewState["ListEArticuloId"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListEArticuloId"] = value;
            }
        }
        private List<EZona> ListZonas
        {
            get
            {
                var data = ViewState["ListEZona"] as List<EZona>;
                if (data == null)
                {
                    data = new List<EZona>();
                    ViewState["ListEZona"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListEZona"] = value;
            }
        }
        private List<EZona> listaZonas
        {
            get
            {
                var data = ViewState["listaZonas"] as List<EZona>;
                if (data == null)
                {
                    data = new List<EZona>();
                    ViewState["listaZonas"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaZonas"] = value;
            }
        }
        protected void CheckSinArticulo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSinArticulo.Checked == true)
            {
                CargarDropZonas(0);
            }

        }
        protected void RadGridArticulosIdInterno_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGridArticulosIdInterno.DataSource = ListArticulosId;
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
