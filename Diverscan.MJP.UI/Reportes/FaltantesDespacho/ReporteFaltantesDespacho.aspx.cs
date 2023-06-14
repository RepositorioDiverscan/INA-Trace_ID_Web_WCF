using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Despacho;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.UI.CrystalReportes.FaltantesDespacho;
using Diverscan.MJP.Utilidades;
using Microsoft.ReportingServices.DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Reportes.FaltantesDespacho
{
    public partial class ReporteFaltantesDespacho : System.Web.UI.Page
    {
        #region Global variables
        e_Usuario UsrLogged = new e_Usuario();
        DDespacho dDespacho = new DDespacho();
        #endregion 

        #region Sesion variables
        private int _idWarehouse
        {
            get
            {
                int result = -1;
                var data = ViewState["_idWarehouse"];
                if (data != null)
                {
                    result = Convert.ToInt32(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idWarehouse"] = value;
            }
        }
        private long _idArticulo
        {
            get
            {
                long result = -1;
                var data = ViewState["idArticulo"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["idArticulo"] = value;
            }
        }
        private DateTime _dateInit
        {
            get
            {
                DateTime result = DateTime.Now; ;
                var data = ViewState["dateInit"];
                if (data != null)
                {
                    result = Convert.ToDateTime(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["dateInit"] = value;
            }
        }
        private DateTime _dateFinal
        {
            get
            {
                DateTime result = DateTime.Now; ;
                var data = ViewState["dateFinal"];
                if (data != null)
                {
                    result = Convert.ToDateTime(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["dateFinal"] = value;
            }
        }
        private List<EArticuloDespacho> _articulosDespachados
        {
            get
            {
                var data = ViewState["articulosDespachados"] as List<EArticuloDespacho>;
                if (data == null)
                {
                    data = new List<EArticuloDespacho>();
                    ViewState["articulosDespachados"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articulosDespachados"] = value;
            }
        }
        private List<EOlaDespacho> _olasDespacho
        {
            get
            {
                var data = ViewState["olasDespacho"] as List<EOlaDespacho>;
                if (data == null)
                {
                    data = new List<EOlaDespacho>();
                    ViewState["olasDespacho"] = data;
                }
                return data;
            }
            set
            {
                ViewState["olasDespacho"] = value;
            }
        }
        #endregion 

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
        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            RDPDateInit.SelectedDate = datetime;

            if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
            {
                RDPDateFinal.SelectedDate = datetime.AddDays(1).Date;
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                RDPDateFinal.SelectedDate = datetime.AddDays(2).Date;
            }
            else
            {
                RDPDateFinal.SelectedDate = datetime.AddDays(1).Date;
            }
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
            ddBodega.Items[0].Attributes.Add("disabled", "disabled");
        }
        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                    /// NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega!!!", "");
                    return;
                }
                
                _dateInit = RDPDateInit.SelectedDate.Value;
                _dateFinal = RDPDateFinal.SelectedDate.Value;


                _articulosDespachados = dDespacho.ObtenerFaltantesDespacho(_dateInit, _dateFinal, _idWarehouse);
                if (_articulosDespachados.Count > 0)
                {
                    PedidosFacturados.Visible = true;
                    RGDArticulosDespachados.DataSource = _articulosDespachados;
                    RGDArticulosDespachados.DataBind();
                } else
                    Mensaje("info", "No se encontraron registros.", "");
            }
            catch (Exception)
            {
                Mensaje("error", "Ha ocurrido un error, intente lo más tarde.", "");
            }


        }
        protected void RGDArticulosDespachados_NeedDataSource(object sender,
           Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGDArticulosDespachados.DataSource = _articulosDespachados;
        }
    
        protected void RGDArticulosDespachados_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            try 
            { 
             GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "Articulo":
                        {
                            _idArticulo = Convert.ToInt32(dataItem.GetDataKeyValue("IdArticulo").ToString());
                            _olasDespacho = dDespacho.ObtenerOlasFaltanteDespacho(_dateInit, _dateFinal, _idArticulo, _idWarehouse);
                            e.DetailTableView.DataSource = _olasDespacho;
                            break;
                        }
                }
            } catch (Exception) 
            {
                Mensaje("error", "Ha ocurrido un error, intente lo más tarde.", "");
            }
           
        }

        #region SCRIPTSMANEGER
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);

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
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }
        #endregion

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            //if (_articulo != null)
            //{
            //    GenerarReporte();
            //}
        }

        private void GenerarReporte()
        {
            try
            {
                if (_idArticulo == 0)
                {
                    Mensaje("error", "Debe buscar un articulo primero!!!", "");
                    return;
                }

                ReportDocument report = new CrystalReportes.PedidosFacturadosProducto.PedidosFacturasProducto();
               

                var tablename = report.Database.Tables[0];
                report.Database.Tables[0].SetDataSource(_articulosDespachados);               

                var fileName = "Faltantes Despacho" + ""+ ".pdf";
                var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\PedidosFacturadosProducto\" + fileName;

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
                Response.AddHeader("Content-disposition", "attachment; filename=\"" + fileName + "\"");
                Response.WriteFile(path);
                Response.End();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }
        }

        public void Mensaje(string sTipo, string sMensaje, string sLLenado)
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