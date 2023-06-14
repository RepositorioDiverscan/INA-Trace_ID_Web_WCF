using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Kardex;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.TRAIngresoSalida;
using Diverscan.MJP.UI.CrystalReportes.TrazabilidadProducto;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class TrazabilidadProducto : System.Web.UI.Page
    {
        private e_Usuario UsrLogged = new e_Usuario();
        private List<KardexInfoBase> _productTrazabilityDetail {

            get {
                var data = ViewState["TRPDetalle"] as List<KardexInfoBase>;
                if (data == null)
                {
                    data = new List<KardexInfoBase>();
                    ViewState["TRPDetalle"] = data;
                }
                return data;
            }

            set { ViewState["TRPDetalle"] = value; }
        }

        private List<KardexInfoBase> _productTrabilityHeader
        {

            get
            {
                var data = ViewState["TRPEncabezado"] as List<KardexInfoBase>;
                if (data == null)
                {
                    data = new List<KardexInfoBase>();
                    ViewState["TRPEncabezado"] = data;
                }
                return data;
            }

            set { ViewState["TRPDetalle"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {

                SetDatetime();
                FillDDBodega();
            }
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        public void RegisterUpdatePanel(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UpdatePanel2.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }

        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
            }
            else
            {
                RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
            }

            RDPFechaInicio.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            FillRadGridTrazability();
        }

        private void FillRadGridTrazability()
        {

            try
            {
                string idInternoArticulo = txtSearch.Text;
                int idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);

                if (!String.IsNullOrEmpty(idInternoArticulo) && ddBodega.SelectedIndex > 0)
                {
                    DateTime fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                    DateTime fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;

                    N_TRAIngresoSalida n_TRAIngresoSalida = new N_TRAIngresoSalida();

                    _productTrazabilityDetail = n_TRAIngresoSalida.GetTrazabilityProduct(
                        idInternoArticulo, idWarehouse, fechaInicioBusqueda, fechaFinBusqueda);

                    _productTrabilityHeader.Add(_productTrazabilityDetail[0]);

                    lblArticulo.Visible = true;
                    txtArticulo.Visible = true;
                    txtArticulo.Text = _productTrazabilityDetail[0].NombreArticulo;

                    RGRProductTrazability.DataSource = _productTrazabilityDetail;
                    RGRProductTrazability.DataBind();
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {

        }      

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            //var path = Server.MapPath("..\\..\\CrystalReportes\\OrdenCompra.pdf");
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\TrazabilidadProducto" + @"\TrazabilidadProducto.pdf";

          
            if (_productTrazabilityDetail.Count > 0)
            {
                ReportDocument report = new TrazabilityProduct();
              
              //  report.Database.Tables[0].SetDataSource(_productTrabilityHeader);
                report.Database.Tables[0].SetDataSource(_productTrazabilityDetail);

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path;
                CrExportOptions = report.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                report.Export();
                report.Close();
                report.Dispose();
                GC.Collect();

                Response.AddHeader("Content-Type", "application/octet-stream");
                Response.AddHeader("Content-Transfer-Encoding", "Binary");
                Response.AddHeader("Content-disposition", "attachment; filename=\"TrazabilidadProducto.pdf\"");
                Response.WriteFile(path);
                Response.End();

            }
            else
            {
                Mensaje("info", "¡Seleccione un orden de compra!", "");
            }
        }

        protected void RGRProductTrazability_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //try
            //{
            //    FillRadGridTrazability();
            //}
            //catch (Exception ex)
            //{
            //    //  Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            //}
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
    }
}