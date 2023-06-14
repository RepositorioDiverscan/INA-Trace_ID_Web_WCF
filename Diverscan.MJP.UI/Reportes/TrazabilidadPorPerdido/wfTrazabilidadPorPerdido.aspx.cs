using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.UI.CrystalReportes.TrazabilidadPedido;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Reportes.TrazabilidadPorPerdido
{
    public partial class wfTrazabilidadPorPerdido : System.Web.UI.Page
    {
        private readonly IDespachoDePedidos despachoDePedidos;
        private readonly DetalleFacturado detalleFacturadoDBA;

        public wfTrazabilidadPorPerdido()
        {
            despachoDePedidos = new DepachoDePedidosDBA();
            detalleFacturadoDBA = new DetalleFacturado();
        }

        e_Usuario UsrLogged = new e_Usuario();

        private int idWarehouse = 0;

        #region variables de la vista 
        private List<EMaestroSolicitudFacturado> _listaMaestroFacturados
        {
            get
            {
                var data = ViewState["_listaMaestroFacturados"] as List<EMaestroSolicitudFacturado>;
                if (data == null)
                {
                    data = new List<EMaestroSolicitudFacturado>();
                    ViewState["_listaMaestroFacturados"] = data;
                }
                return data;
            }
            set { ViewState["_listaMaestroFacturados"] = value; }
        }

        private long _idMaestroFacturado
        {
            get
            {
                long result = -1;
                var data = ViewState["_idMaestroFacturado"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["_idMaestroFacturado"] = value;
            }
        }

        private List<EDetalleFacturadoTrazabilidad> _detalleFacturadoLista
        {
            get
            {
                var data = ViewState["_detalleFacturadoLista"] as List<EDetalleFacturadoTrazabilidad>;
                if (data == null)
                {
                    data = new List<EDetalleFacturadoTrazabilidad>();
                    ViewState["_detalleFacturadoLista"] = data;
                }
                return data;
            }
            set
            {
                ViewState["_detalleFacturadoLista"] = value;
            }
        }

        private EMaestroSolicitudFacturado _maestroFacturado
        {
            get
            {
                var data = ViewState["_maestroFacturado"] as EMaestroSolicitudFacturado;
                if (data == null)
                {
                    data = new EMaestroSolicitudFacturado();
                    ViewState["_maestroFacturado"] = data;
                }
                return data;
            }
            set { ViewState["_maestroFacturado"] = value; }
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

        #region EVENTOS
        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                    NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error " + ex.Message, "");
            }

        }       

        protected void RGPedidosFacturados_ItemCommand(object source, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        var olaFacturada = _listaMaestroFacturados[e.Item.DataSetIndex];
                        _idMaestroFacturado = olaFacturada.IdMaestroSolicitud;
                        if (olaFacturada != null)
                        {
                            _detalleFacturadoLista = detalleFacturadoDBA.
                                ObtenerDetalleFacturadoTrazabilidad(olaFacturada.IdMaestroSolicitud);
                            RGArticulosPedido.DataSource = _detalleFacturadoLista;
                            RGArticulosPedido.DataBind();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        protected void RGArticulosPedido_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGArticulosPedido.DataSource = _listaMaestroFacturados;
        }

        protected void RGArticulosPedido_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Articulos":
                    {
                        _idMaestroFacturado = long.Parse(dataItem.GetDataKeyValue("IdMaestroSolicitud").ToString());                     
                       
                        _detalleFacturadoLista = detalleFacturadoDBA.
                            ObtenerDetalleFacturadoTrazabilidad(_idMaestroFacturado);
                        e.DetailTableView.DataSource = _detalleFacturadoLista;
                        
                        break;
                    }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;
                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;

                string busqueda = txtSearch.Text.Trim();
                int idWarehouse = 0;

                if (ddBodega.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega", "");
                    return;
                }
                else
                    idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());

                List<EMaestroSolicitudFacturado> ObtenerListadoOlas = despachoDePedidos.ObtenerPreMaestrosFacturados(
                    idWarehouse, fechaInicioBusqueda, fechaFinBusqueda);

                Pedidos.Visible = true;

                _listaMaestroFacturados = ObtenerListadoOlas;
                RGArticulosPedido.DataSource = _listaMaestroFacturados;
                RGArticulosPedido.DataBind();
                PanelPedidos.Visible = true;                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento un error " + ex.Message, "");
            }
        }

        protected void btnReporteTrazabilidad_Click(object sender, EventArgs e)
        {
            if (_maestroFacturado != null)
            {
                GenerarReporteTrazabilidad();
            }
        }
        #endregion

        #region METODOS PRIVADOS

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


        private void GenerarReporteTrazabilidad()
        {
            try
            {
                if (_idMaestroFacturado < 0)
                {
                    Mensaje("error", "Debe seleccionar una Ola", "");
                    return;
                }

                ReportDocument report = new CrystalReportes.TrazabilidadPedido.CRTrazabilidadPedido();

                _maestroFacturado = _listaMaestroFacturados.Find(x => x.IdMaestroSolicitud == _idMaestroFacturado);
                _detalleFacturadoLista = detalleFacturadoDBA.
                              ObtenerDetalleFacturadoTrazabilidad(_idMaestroFacturado);

                List<CREMaestroSolicitudFacturado> _listaMaestroSolicitudFacturado = new List<CREMaestroSolicitudFacturado>();
                CREMaestroSolicitudFacturado maestroSolicitudFacturado = new CREMaestroSolicitudFacturado();
                maestroSolicitudFacturado.IdInternoCliente = _maestroFacturado.IdInternoCliente;
                maestroSolicitudFacturado.NombreCliente = _maestroFacturado.NombreCliente;
                maestroSolicitudFacturado.IdRegistroOla = _maestroFacturado.IdRegistroOla;
                _listaMaestroSolicitudFacturado.Add(maestroSolicitudFacturado);

                var tablename = report.Database.Tables[0];
                var tablename1 = report.Database.Tables[1];
                report.Database.Tables[0].SetDataSource(_listaMaestroSolicitudFacturado);
                report.Database.Tables[1].SetDataSource(_detalleFacturadoLista);

                var fileName = "Productos_Trazabilidad" + _idMaestroFacturado + ".pdf";
                var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\TrazabilidadPedido\" + fileName;                                                     

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
    }
}