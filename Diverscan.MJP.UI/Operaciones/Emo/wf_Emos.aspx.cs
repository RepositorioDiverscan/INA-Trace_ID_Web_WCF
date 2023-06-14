using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Emo;
using Diverscan.MJP.AccesoDatos.Invoiced;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Transportista;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.UI.CrystalReportes.Emo;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Emo
{
    public partial class wf_Emos : System.Web.UI.Page
    {

        private e_Usuario UsrLogged = new e_Usuario();
        private NTransportista _nTransportista;
        private FileExceptionWriter _fileException = new FileExceptionWriter();
        private NInvoiced _nInvoiced;
        private NEmo _nEmo;

        private long _idWarehouse
        {
            get
            {
                long result = -1;
                var data = ViewState["_idWarehouse"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idWarehouse"] = value;
            }
        }

        private long _idEmo
        {
            get
            {
                long result = -1;
                var data = ViewState["_idEmo"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idEmo"] = value;
            }
        }

        private List<EInvoiced> _invoicesList
        {
            get
            {
                var data = ViewState["_invoicesList"] as List<EInvoiced>;
                if (data == null)
                {
                    data = new List<EInvoiced>();
                    ViewState["_invoicesList"] = data;
                }
                return data;
            }
            set { ViewState["_invoicesList"] = value; }
        }

        private List<EEmo> _emoList
        {
            get
            {
                var data = ViewState["_emoList"] as List<EEmo>;
                if (data == null)
                {
                    data = new List<EEmo>();
                    ViewState["_emoList"] = data;
                }
                return data;
            }
            set { ViewState["_emoList"] = value; }
        }

        private long _idTransportista
        {
            get
            {
                long result = -1;
                var data = ViewState["_idTransportista"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idTransportista"] = value;
            }
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
                _idEmo = new long();
                _invoicesList = new List<EInvoiced>();
                _emoList = new List<EEmo>();
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
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
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

        private void FIlLDDLTransportista()
        {
            _nTransportista = new NTransportista(_fileException);
            List<ETransportista> transportistas = _nTransportista.BuscarTransportistaXBodega(_idWarehouse);
            ddlTransportista.DataSource = transportistas;
            ddlTransportista.DataTextField = "Nombre";
            ddlTransportista.DataValueField = "IdTransportista";
            ddlTransportista.DataBind();
            ddlTransportista.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
            ddlTransportista.Items[0].Attributes.Add("disabled", "disabled");
            ddlTransportista.Items.Insert(1, new ListItem("--Todos--", "0"));
        }

        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FIlLDDLTransportista();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            DateTime fechaInicioBusqueda;
            DateTime fechaFinBusqueda;
            fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
            fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;

            string busqueda = txtSearch.Text.Trim();

            if (ddBodega.SelectedIndex <= 0)
            {
                Mensaje("error", "Debe seleccionar una bodega!", "");
                return;
            }

            if(ddlTransportista.SelectedIndex < 0) 
            {
                Mensaje("error", "Debe seleccionar un Transportista o la opción todos!", "");
                return;
            }

            _idTransportista = long.Parse(ddlTransportista.SelectedValue);

            _nEmo = new NEmo(_fileException);
            _emoList = _nEmo.BuscarEmo(_idWarehouse, _idTransportista, fechaInicioBusqueda, fechaFinBusqueda, "");

            RGEmo.DataSource = _emoList;
            RGEmo.DataBind();
        }

        protected void btnReporteEmo_Click(object sender, EventArgs e)
        {
            GenerarReporteEmo();
        }

        private void GenerarReporteEmo()
        {
            try
            {
                if (_idEmo <= 0)
                {
                    Mensaje("error", "Debe seleccionar un Emo", "");
                    return;
                }

                if (_invoicesList.Count <= 0)
                {
                    Mensaje("error", "El Emo seleccionado no tiene facturas asociadas", "");
                    return;
                }

                ReportDocument report = new CrystalReportes.Emo.CREmo();

                List<EInvoiced> invoicedList = new List<EInvoiced>();
                invoicedList = _invoicesList;

                List<CREEmo> emosList = new List<CREEmo>();
                EEmo emo = _emoList.Find(x => x.IdEmo == _idEmo);
                CREEmo temp = new CREEmo();
                temp.IdEmo = emo.IdEmo;
                temp.NombreTransportista = emo.NombreTransportista;
                temp.TotalMonto = emo.TotalMonto;
                temp.TotalPeso = emo.TotalPeso;
                temp.QuantityInvoices = _invoicesList.Count;
                temp.RecordDate = emo.RecordDate;
                temp.IdInterno = emo.IdInterno;
                emosList.Add(temp);

                report.Database.Tables[0].SetDataSource(emosList);
                report.Database.Tables[1].SetDataSource(invoicedList);

                var fileName = "Emo " + temp.IdInterno + " " + ".pdf";
                var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\Emo\" + fileName;              

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

                Response.Clear();
                Response.AddHeader("Content-Type", "application/octet-stream");
                Response.AddHeader("Content-Transfer-Encoding", "Binary");
                Response.AddHeader("Content-disposition", "attachment; filename=\"" + fileName + "\"");
                Response.WriteFile(path);
                //Response.Flush();
                //Response.Close();
                Response.End();


            }
            catch (ThreadAbortException te)
            {
                Mensaje("error", "Se presento un error " + te.Message, "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento un error " + ex.Message, "");
            }
        }

        protected void RGEmo_NeedDataSource(object sender,
                  Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGEmo.DataSource = _emoList;
        }

        protected void RGEmo_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {

            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "Invoices":
                        {

                            long idEmo = long.Parse(dataItem.GetDataKeyValue("IdEmo").ToString());
                            _idEmo = idEmo;
                            _nEmo = new NEmo(_fileException);
                            _invoicesList = _nEmo.GetInvoicesByEmo(idEmo);
                            e.DetailTableView.DataSource = _invoicesList;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                //  cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }
    }
}