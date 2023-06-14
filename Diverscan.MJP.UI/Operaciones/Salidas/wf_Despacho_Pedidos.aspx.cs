using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.AprobarSalida;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OrdenCompa;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Diverscan.MJP.Negocio.Tareas;
using Diverscan.MJP.Entidades.Tareas;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.Negocio.OPESALMaestroSolicitud;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.Usuarios;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using CrystalDecisions.CrystalReports.Engine;
using Diverscan.MJP.UI.CrystalReportes.Certificacion;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Transportista;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_Despacho_Pedidos : System.Web.UI.Page
    {
        private readonly IDespachoDePedidos DespachoDePedidos;

        public wf_Despacho_Pedidos()
        {
            DespachoDePedidos = new DepachoDePedidosDBA();
        }

        public int idOla_public = 0;
        e_Usuario UsrLogged = new e_Usuario();
        List<Entidades.Usuarios.e_Usuarios> usersList = new List<Entidades.Usuarios.e_Usuarios>();
        string SQL = "";
        DataSet DS = new DataSet();
        static DataSet DSDatosExport = new DataSet(); //Para Grid Detalle
        static string idMaestroSolicitud = "";  //Obtener el IdPara cargar el detalle
        private string valor_bodega;
        private List<e_OPESALMaestroSolicitud> _listOrders = new List<e_OPESALMaestroSolicitud>();
        private List<ESectorWarehouse> _sectorsWarehouse = new List<ESectorWarehouse>();         
        private NTransportista _nTransportista;
        private FileExceptionWriter _fileException = new FileExceptionWriter();

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

        #region variables de la vista 
        private List<E_ListadoOlasFactura> _e_ListadoOlasFactura
        {
            get
            {
                var data = ViewState["e_ListadoOlasFactura"] as List<E_ListadoOlasFactura>;
                if (data == null)
                {
                    data = new List<E_ListadoOlasFactura>();
                    ViewState["e_ListadoOlasFactura"] = data;
                }
                return data;
            }
            set
            {
                ViewState["e_ListadoOlasFactura"] = value;
            }
        }

        private int _idOla
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
                
        private List<E_ListadoDetalleOla> _e_ListadoDetalleOla
        {
            get
            {
                var data = ViewState["e_ListadoDetalleOla"] as List<E_ListadoDetalleOla>;
                if (data == null)
                {
                    data = new List<E_ListadoDetalleOla>();
                    ViewState["e_ListadoDetalleOla"] = data;
                }
                return data;
            }
            set
            {
                ViewState["e_ListadoDetalleOla"] = value;
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
                    _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                    FIlLDDLTransportista();
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
            ddlTransportista.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddlTransportista.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGAprobarSalida.DataSource = _e_ListadoOlasFactura;        
        }

        protected void RGAprobarSalida_ItemCommand(object source, GridCommandEventArgs e)
        {
            for (int i = 0; i < RGAprobarSalida.Items.Count; i++)
            {
                var item = RGAprobarSalida.Items[i];
                var checkbox = item["checkFacturar"].Controls[0] as CheckBox;

                if (checkbox != null && checkbox.Checked)
                {
                    idOla_public = 0;
                    int idOla = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                    _idOla = idOla;
                    string facturado = item["Facturado"].Text.Replace("&nbsp;", "");

                    if (facturado == "Facturada")
                    {
                        btnFacturar.Visible = false;
                        labelSeparador.Visible = false;
                        btnCancelar.Visible = false;

                        btnCancelarFacturada.Visible = true;
                    }
                    else
                    {
                        btnFacturar.Visible = true;
                        labelSeparador.Visible = true;
                        btnCancelar.Visible = true;

                        btnCancelarFacturada.Visible = false;
                    }

                    idOla_public = idOla;
                    PanelDetalle.Visible = true;
                    List<E_ListadoDetalleOla> ListadoDetalle = DespachoDePedidos.ObtenerDetalleOla(idOla);
                    _e_ListadoDetalleOla = ListadoDetalle;
                    RGDetalleOla.DataSource = ListadoDetalle;
                    RGDetalleOla.DataBind();
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
                } else
                    idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());

                int facturado = 0;

                if (cbFacturado.Checked == true)
                {
                    facturado = 1;
                    PanelAprobadas.Visible = true;
                    PanelPendiente.Visible = false;
                }
                else
                {
                    facturado = 0;
                    PanelAprobadas.Visible = false;
                    PanelPendiente.Visible = true;
                }

                List<E_ListadoOlasFactura> ObtenerListadoOlas = DespachoDePedidos.ObtenerListadoOlas(idWarehouse, fechaInicioBusqueda, fechaFinBusqueda, busqueda, facturado);
                _e_ListadoOlasFactura = ObtenerListadoOlas;
                RGAprobarSalida.DataSource = _e_ListadoOlasFactura;
                RGAprobarSalida.DataBind();
                PaneOlas.Visible = true;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presento un error " + ex.Message, "");
            }
        }
        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                DS.Clear();

                SQL = "SELECT * FROM Vista_PreDetalleSolicitudPorBodegaV2" + //Mejora en el rendimiento de la vista 
                "  ORDER BY Fecha DESC";

                DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                RGAprobarSalida.DataSource = new string[] { };
                RGAprobarSalida.DataSource = DS;
                RGAprobarSalida.DataBind();
                txtSearch.Text = "";

                SetDatetime();
                //RadGridDetalleSalida.DataSource = null;

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
        }

        protected void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                int idOla = 0;               

                for (int i = 0; i < RGAprobarSalida.Items.Count; i++)
                {
                    var item = RGAprobarSalida.Items[i];
                    var checkbox = item["checkFacturar"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                       
                        idOla = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                      
                    }
                }

                if (idOla == 0){
                    Mensaje("info", "Debe haber una Ola seleccionada", "");
                    return;
                }

                E_ListadoOlasFactura ola = _e_ListadoOlasFactura.Find(x => x.idRegistroOla == idOla);

                if (!ola.Ruta.Equals("Interno"))
                {
                    if (ddlTransportista.SelectedIndex <= 0)
                    {
                        Mensaje("error", "Debe seleccionar un transportista para crear un Emo", "");
                        return;
                    }
                    _idTransportista = long.Parse(ddlTransportista.SelectedValue);
                }
                else
                    _idTransportista = 0;

                DespachoDePedidos.FacturarOla(idOla,_idTransportista);
                    PanelDetalle.Visible = false;
                    Mensaje("ok", "La Ola fue facturada exitosamente", "");
                    //SetDatetime();
                    //FillDDBodega();
                    //txtSearch.Text = "";
                    PaneOlas.Visible = false;                                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Se presente un error: " + ex.Message, "");
            }
        }

        protected void btnCancelarFacturada_Click(object sender, EventArgs e)
        {
            PanelDetalle.Visible = false;
        }

        private void GenerarReportePorOla()
        {
            if(_idOla<0)
            {
                Mensaje("error", "Debe seleccionar una Ola", "");
                return;
            }
            List<CRCertificacionEn> cRCertificacionEns = new List<CRCertificacionEn>();
            var OlasFacturas = _e_ListadoOlasFactura.Where(x => x.idRegistroOla == _idOla).ToList<E_ListadoOlasFactura>();
            if(OlasFacturas.Count>0)
            {
                CRCertificacionEn cRCertificacionEn = new CRCertificacionEn();
                cRCertificacionEn.Ruta = OlasFacturas[0].Ruta;
                cRCertificacionEn.idRegistroOlaRuta = OlasFacturas[0].idRegistroOla.ToString();
                cRCertificacionEn.NumeroLineas = _e_ListadoDetalleOla.Count;
                cRCertificacionEns.Add(cRCertificacionEn);
            }
            var fileName = "Certificacion_" + _idOla + ".pdf";
            //var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\Certificacion" + @"\Certificacion.pdf";
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\Certificacion\" + fileName;
            ReportDocument report = new CrystalReportes.Certificacion.CRCertificacion();
            var tablename = report.Database.Tables[0];
            report.Database.Tables[0].SetDataSource(cRCertificacionEns);
            report.Database.Tables[1].SetDataSource(_e_ListadoDetalleOla);

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
            Response.AddHeader("Content-disposition", "attachment; filename=\""+fileName+"\"");
            //Response.AddHeader("Content-disposition", "attachment; filename=\"Certificacion.pdf\"");
            Response.WriteFile(path);
            Response.End();

        }

        protected void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            GenerarReportePorOla();
        }

        protected void RGDetalleOla_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGDetalleOla.DataSource = _e_ListadoDetalleOla;           
        }
    }
}