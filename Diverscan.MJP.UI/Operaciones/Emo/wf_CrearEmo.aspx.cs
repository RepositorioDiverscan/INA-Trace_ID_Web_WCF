using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Emo;
using Diverscan.MJP.AccesoDatos.Invoiced;
using Diverscan.MJP.AccesoDatos.Invoiced.PedidoFacturasTransportista;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Transportista;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.UI.CrystalReportes.Emo;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Emo
{
    public partial class wf_CrearEmo : System.Web.UI.Page
    {        
        private e_Usuario UsrLogged = new e_Usuario();
        private NTransportista _nTransportista;
        private FileExceptionWriter _fileException = new FileExceptionWriter();
        private NInvoiced _nInvoiced;
        private NEmo _nEmo;
        private NPedidoFacturasTransportista _nPedidoFacturasTransportista;
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


        private List<EPedidoFacturasTransportista> _orderDenyList
        {
            get
            {
                var data = ViewState["_orderDenyList"] as List<EPedidoFacturasTransportista>;
                if (data == null)
                {
                    data = new List<EPedidoFacturasTransportista>();
                    ViewState["_orderDenyList"] = data;
                }
                return data;
            }
            set { ViewState["_orderDenyList"] = value; }
        }

        private List<EInvoiced> _invoicesEmo
        {
            get
            {
                var data = ViewState["_invoicesEmo"] as List<EInvoiced>;
                if (data == null)
                {
                    data = new List<EInvoiced>();
                    ViewState["_invoicesEmo"] = data;
                }
                return data;
            }
            set { ViewState["_invoicesEmo"] = value; }
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

        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FIlLDDLTransportista();
                    FillRGEmo();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }

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

        private void FillRGEmo()
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                DateTime dateInit = DateTime.Now;
                DateTime dateEnd = DateTime.Now;
                TimeSpan newTime = new TimeSpan(00, 00, 0);
                dateInit = dateInit.Date + newTime;
                dateEnd = dateEnd.Date + newTime;

                if (ddBodega.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega", "");
                    return;
                }

                _nEmo = new NEmo(_fileException);
                _emoList = _nEmo.BuscarEmo(_idWarehouse, 0, dateInit, dateEnd, "");               

                RGEmo.DataSource = _emoList;
                RGEmo.DataBind();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
          
            FillRGEmo();         
            FillInvoices();
            FillRGOrderDeny();
        }

        private void FillRGOrderDeny()
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;
                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;

                if (ddBodega.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega", "");
                    return;
                }

                if (ddlTransportista.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar un transportista para crear un Emo", "");
                    return;
                }

                if (ddlTransportista.SelectedIndex == 1)
                    return;

                _idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());
                _idTransportista = long.Parse(ddlTransportista.SelectedValue);

                string busqueda = txtSearch.Text.Trim();
                _nPedidoFacturasTransportista = new NPedidoFacturasTransportista(fileExceptionWriter);
                List<EPedidoFacturasTransportista> orderDenyList = _nPedidoFacturasTransportista.BuscarPedidoFacturasTransportista(_idWarehouse, _idTransportista, fechaInicioBusqueda, fechaFinBusqueda, busqueda);
                _orderDenyList = orderDenyList;
                RGOrdersDeny.DataSource = _orderDenyList;
                RGOrdersDeny.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento un error " + ex.Message, "");
            }
        }

       
        private void FillInvoices()
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;
                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;

                string busqueda = txtSearch.Text.Trim();

                if (ddBodega.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega", "");
                    return;
                }

                if (ddlTransportista.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar un transportista para crear un Emo", "");
                    return;
                }

                if(ddlTransportista.SelectedIndex == 1)
                    return;

                _idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());
                _idTransportista = long.Parse(ddlTransportista.SelectedValue);
                _nInvoiced = new NInvoiced(fileExceptionWriter);
                _invoicesList = _nInvoiced.BuscarInvoiced(_idWarehouse,_idTransportista, fechaInicioBusqueda, fechaFinBusqueda, busqueda);

                RGMInvoiced.DataSource = _invoicesList;
                RGMInvoiced.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }
        }

        protected void RGMInvoiced_NeedDataSource(object sender,
           Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGMInvoiced.DataSource = _invoicesList;
        }

        protected void RGOrdersDeny_NeedDataSource(object sender,
       Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGOrdersDeny.DataSource = _orderDenyList;
        }

        protected void btnCreateEmo_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> facturasEmo = new List<long>();
                for (int i = 0; i < RGMInvoiced.Items.Count; i++)
                {
                    var item = RGMInvoiced.Items[i];
                    var checkbox = item["checkEmo"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        facturasEmo.Add(long.Parse(item["IdRecord"].Text.Replace("&nbsp;", "")));
                    }
                }

                if (facturasEmo.Count <= 0)
                {
                    Mensaje("error", "Debe seleccionar una factura para crear un Emo", "");
                    return;
                }

                if (ddBodega.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega para crear un Emo", "");
                    return;
                }

                if (ddlTransportista.SelectedIndex < 0)
                {
                    Mensaje("error", "Debe seleccionar un transportista para crear un Emo", "");
                    return;
                }

                _idWarehouse = long.Parse(ddBodega.SelectedValue);
                _idTransportista = long.Parse(ddlTransportista.SelectedValue);

                _nEmo = new NEmo(_fileException);
                int idUser = Convert.ToInt32(((e_Usuario)Session["USUARIO"]).IdUsuario);
                long idEmo = _nEmo.CreateEmo(_idWarehouse, _idTransportista, idUser);

                if (idEmo <= 0)
                {
                    Mensaje("error", "Hubo un problema al crear el Emo, intenteto otra vez.", "");
                    return;
                }

                string response = _nEmo.InsertInvoicesByEmo(idEmo, facturasEmo);

                if (response.Contains("exitosamente"))
                {
                    Mensaje("info", response, "");
                    FillRGEmo();
                    FillInvoices();
                }                    
                else
                    Mensaje("error", response, "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }
        }

        protected void RGEmo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        long idEmo = long.Parse(item["IdEmo"].Text.Replace("&nbsp;", ""));
                        if (idEmo > 0)
                        {
                            _idEmo = idEmo;
                            _nEmo = new NEmo(_fileException);
                            _invoicesEmo = _nEmo.GetInvoicesByEmo(idEmo);
                        }
                        break;
                    }
                default:
                    break;
            }
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

                if (_invoicesEmo.Count <= 0)
                {
                    Mensaje("error", "El Emo seleccionado no tiene facturas asociadas", "");
                    return;
                }

                ReportDocument report = new CrystalReportes.Emo.CREmo();

                List<EInvoiced> invoicedList = new List<EInvoiced>();
                invoicedList = _invoicesEmo;

                List<CREEmo> emosList = new List<CREEmo>();
                EEmo emo = _emoList.Find(x => x.IdEmo == _idEmo);
                CREEmo temp = new CREEmo();
                temp.IdEmo = emo.IdEmo;
                temp.NombreTransportista = emo.NombreTransportista;
                temp.TotalMonto = emo.TotalMonto;
                temp.TotalPeso = emo.TotalPeso;
                temp.QuantityInvoices = invoicedList.Count;
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
    }
}