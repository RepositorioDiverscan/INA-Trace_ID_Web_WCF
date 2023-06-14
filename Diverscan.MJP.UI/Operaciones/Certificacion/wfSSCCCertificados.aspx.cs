using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Certificacion
{
    public partial class wfSSCCCertificados : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        FileExceptionWriter fileExceptionWriter;
        NSSCC _nSSCC;

        public wfSSCCCertificados()
        {
            fileExceptionWriter = new FileExceptionWriter();
            _nSSCC = new NSSCC(fileExceptionWriter);
        }

        #region Sesion variables
        private int _idWarehouse
        {
            get
            {
                var result = -1;
                var data = ViewState["_idWarehouse"];
                if (data != null)
                {
                    result = Convert.ToInt32(data);
                }
                return result;
            }
            set
            {
                ViewState["_idWarehouse"] = value;
            }
        }
        private long _idOla
        {
            get
            {
                var result = -1;
                var data = ViewState["IdOla"];
                if (data != null)
                {
                    result = Convert.ToInt32(data);
                }
                return result;
            }
            set
            {
                ViewState["IdOla"] = value;
            }
        }
        private int _idSSCC
        {
            get
            {
                var result = -1;
                var data = ViewState["_idSSCC"];
                if (data != null)
                {
                    result = Convert.ToInt32(data);
                }
                return result;
            }
            set
            {
                ViewState["_idSSCC"] = value;
            }
        }
        private List<e_OPESALMaestroSolicitud> _listMaestros
        {
            get
            {
                var data = ViewState["listaOPESALMaestroSolicitud"] as List<e_OPESALMaestroSolicitud>;
                if (data == null)
                {
                    data = new List<e_OPESALMaestroSolicitud>();
                    ViewState["listaOPESALMaestroSolicitud"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaOPESALMaestroSolicitud"] = value;
            }
        }
        private List<ESSCC> _listaSSCCCertificados
        {
            get
            {
                var data = ViewState["_listaSSCCCertificados"] as List<ESSCC>;
                if (data == null)
                {
                    data = new List<ESSCC>();
                    ViewState["_listaSSCCCertificados"] = data;
                }
                return data;
            }
            set
            {
                ViewState["_listaSSCCCertificados"] = value;
            }
        }
        private List<EDetalleSSCCCertificado> _listaDetalleSSCC
        {
            get
            {
                var data = ViewState["_listaDetalleSSCC"] as List<EDetalleSSCCCertificado>;
                if (data == null)
                {
                    data = new List<EDetalleSSCCCertificado>();
                    ViewState["_listaDetalleSSCC"] = data;
                }
                return data;
            }
            set
            {
                ViewState["_listaDetalleSSCC"] = value;
            }
        }
        private List<ESectorWarehouse> _sectorsWarehouse
        {
            get
            {
                var data = ViewState["_sectorsWarehouse"] as List<ESectorWarehouse>;
                if (data == null)
                {
                    data = new List<ESectorWarehouse>();
                    ViewState["_sectorsWarehouse"] = data;
                }
                return data;
            }
            set
            {
                ViewState["_sectorsWarehouse"] = value;
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
                //fillddPrioridad();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        #region METODOS PRIVADOS

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
        #endregion

        #region EVENTOS
        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddBodega.SelectedIndex > 0)
            {
                _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
             //   NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);              
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud nOPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);

                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;

                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;
                string idInternoOrder = txtSearch.Text.Trim();
                int idWarehouse = 0;

                if (ddBodega.SelectedIndex > 0)
                {
                    idWarehouse = Convert.ToInt32(ddBodega.SelectedValue.ToString());
                }

                if (string.IsNullOrEmpty(idInternoOrder) && ddBodega.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe ingresar los campos de busquedad requeridos!!!", "");
                    return;
                }
                else
                    _listMaestros = nOPESALMaestroSolicitud.GetOrdersToEnlist(idWarehouse, fechaInicioBusqueda, fechaFinBusqueda, idInternoOrder);

                RGMaestroSolicitud.DataSource = _listMaestros;
                RGMaestroSolicitud.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }
        protected void RGMaestroSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RGMaestroSolicitud.DataSource = _listMaestros;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }       
        protected void RGMaestroSolicitud_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var prueba = RGMaestroSolicitud;
            if (e.CommandName == "btnVerDetalle")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;


                    _idOla = long.Parse(item["IdInterno"].Text);
                  
                    _listaSSCCCertificados = _nSSCC.ObtenerSSCCXIdSolicitud(_idOla);
                    RGSSCCOla.DataSource = _listaSSCCCertificados;
                    RGSSCCOla.DataBind();
                    Pedidos.Visible = true;
                    PanelPedidos.Visible = true;
                }
                catch (Exception ex)
                {
                    var cl = new clErrores();
                    cl.escribirError(ex.Message, ex.StackTrace);
                    ex.ToString();
                }
            }
        }
        protected void RGSSCCOla_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGSSCCOla.DataSource = _listaSSCCCertificados;
        }
        protected void RGSSCCOla_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Articulos":
                    {
                        _idSSCC = Convert.ToInt32(dataItem.GetDataKeyValue("IdSSCC").ToString());
                        _listaDetalleSSCC = _nSSCC.GetSSCCProducts(_idSSCC);
                        e.DetailTableView.DataSource = _listaDetalleSSCC;
                        break;
                    }
            }
        }                      
       /* private void GenerarReportePorOla()
        {
            if (_idOla < 0)
            {
                Mensaje("error", "Debe seleccionar una Ola", "");
                return;
            }

            if (_idSSCC < 0)
            {
                Mensaje("error", "Debe seleccionar un SSCC", "");
                return;
            }

            if (_listaDetalleSSCC.Count == 0)
            {
                Mensaje("error", "El SSCC seleccionado no contine articulos", "");
                return;
            }


            ReportDocument report = new CrystalReportes.SSCCCertificados.CRSSCCCertificados();

            List<e_OPESALMaestroSolicitud> maestroSolicitudLista =
                new List<e_OPESALMaestroSolicitud>();
            e_OPESALMaestroSolicitud maestroSolicitud = _listMaestros.
                Find(x => x.IdMaestroSolicitud == _idOla);
            maestroSolicitudLista.Add(maestroSolicitud);

            List<ESSCC> eSSCCCertificadoLista = new List<ESSCC>();
            ESSCC sSCCCertificado = _listaSSCCCertificados.
                Find(x => x.IdSSCC == _idSSCC);
            eSSCCCertificadoLista.Add(sSCCCertificado);


            var fileName = "Certificacion_" + sSCCCertificado.ConsecutivoSSCC + "_"  + ".pdf";
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\SSCCCertificados\" + fileName;            

            //var tablename1 = report.Database.Tables[0];            
            //var tablename2 = report.Database.Tables[1];
            //var tablename3 = report.Database.Tables[2];

            report.Database.Tables[0].SetDataSource(_listaDetalleSSCC);
            report.Database.Tables[1].SetDataSource(maestroSolicitudLista);
            report.Database.Tables[2].SetDataSource(eSSCCCertificadoLista);

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
            //Response.AddHeader("Content-disposition", "attachment; filename=\"Certificacion.pdf\"");
            Response.WriteFile(path);
            Response.End();

        }
        protected void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            GenerarReportePorOla();
        }
        */
        #endregion
    }
}