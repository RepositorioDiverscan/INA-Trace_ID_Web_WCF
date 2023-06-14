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
using Diverscan.MJP.AccesoDatos.Certificación;
using Diverscan.MJP.UI.CrystalReportes.Certificacion;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Diverscan.MJP.UI.Operaciones.CertifConPermiso
{
    public partial class wf_CertConPermiso : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();

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
        private List<E_CertificacionDetalle> _listaDetalleCertificacion
        {
            get
            {
                var data = ViewState["listaDetalleCertificacion"] as List<E_CertificacionDetalle>;
                if (data == null)
                {
                    data = new List<E_CertificacionDetalle>();
                    ViewState["listaDetalleCertificacion"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaDetalleCertificacion"] = value;
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
            }

        }

        #region EVENTOS
        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        protected void RGAprobarSalida_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RGAprobarSalida.DataSource = _listMaestros;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            SearchOlas();
        }

        private void SearchOlas()
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                N_Certificacion nCerficacion = new N_Certificacion(fileExceptionWriter);

                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;

                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;
                string idInternoOrder = txtSearch.Text.Trim();
                int idWarehouse = 0;

                if (ddBodega.SelectedIndex > 0)
                {
                    idWarehouse = Convert.ToInt32(ddBodega.SelectedIndex.ToString());
                }

                if (string.IsNullOrEmpty(idInternoOrder) && ddBodega.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe ingresar los campos de busquedad requeridos!!!", "");
                    return;
                }
                else
                    _listMaestros = nCerficacion.GetOrdersToCertificated(idWarehouse, fechaInicioBusqueda, fechaFinBusqueda, idInternoOrder);

                RGAprobarSalida.DataSource = _listMaestros;
                RGAprobarSalida.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }
        protected void RadGridDetalleSalida_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (_listaDetalleCertificacion != null)
                {
                    RadGridDetalleSalida.DataSource = _listaDetalleCertificacion;
                    RadGridDetalleSalida.DataBind();
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        protected void RadGridDetalleSalida_ItemCommand1(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                _idOla = Convert.ToInt32(item["IdLineaDetalleSolicitud"].Text.Replace("&nbsp;", ""));
            }
        }

        protected void RGAprobarSalida_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ddSector.SelectedIndex = 0;
            var prueba = RGAprobarSalida;
            if (e.CommandName == "btnVerDetalle")
            {
                int idMaestroSolicitud = -1, idBodega = -1;
                try
                {
                    if (ddBodega.SelectedIndex > 0)
                    {
                        idBodega = Convert.ToInt32(ddBodega.SelectedValue);
                    }
                    else
                    {
                        Mensaje("error", "¡Debe seleccionar una bodega!", "");
                        return;
                    }

                    GridDataItem item = (GridDataItem)e.Item;
                    idMaestroSolicitud = Convert.ToInt32(item["IdMaestroSolicitud"].Text);

                    if (idBodega > 0 && idMaestroSolicitud > 0)
                    {
                        _idOla = idMaestroSolicitud;
                        GetGetDetalleSalidaArticulosSector(idBodega, idMaestroSolicitud);
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

        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddBodega.SelectedIndex > 0)
            {
                _idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);

                _sectorsWarehouse = nSectorWare.GetSectorsWarehouse(_idWarehouse);
                ddSector.DataSource = _sectorsWarehouse;
                ddSector.DataTextField = "Name";
                ddSector.DataValueField = "IdSectorWarehouse";
                ddSector.DataBind();
                ddSector.Items.Insert(0, new ListItem("--Todos--", "" + _sectorsWarehouse.Count));
            }
        }

        #endregion

        #region METODOS PRIVADOS
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

        private void GetGetDetalleSalidaArticulosSector(int idBodega, int idMaestroSalida)//cambiar
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                N_Certificacion n_Certificacion = new N_Certificacion(fileExceptionWriter);

                _listaDetalleCertificacion = n_Certificacion.GetDetalleCertificacion(idBodega, idMaestroSalida);

                RadGridDetalleSalida.DataSource = _listaDetalleCertificacion;
                RadGridDetalleSalida.DataBind();

                var alistadores = _listaDetalleCertificacion.Select(x => x.NombreUsuario).Distinct().ToList();


                ddAlistador.DataSource = alistadores;
                ddAlistador.DataBind();
                ddAlistador.Items.Insert(0, new ListItem("--Todos--", "0"));
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
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
        #endregion

        protected void ddSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSector.SelectedIndex > 0)
            {
                FiltrarSectores(ddSector.SelectedItem.Text);
            }
        }

        private void FiltrarSectores(string NombreDelSector)
        {
            if (NombreDelSector == "--Todos--")
            {
                RadGridDetalleSalida.DataSource = _listaDetalleCertificacion;
                RadGridDetalleSalida.DataBind();
            }
            else
            {
                List<E_CertificacionDetalle> articulosPorSector = _listaDetalleCertificacion.FindAll(x => x.NombreSector == NombreDelSector);
                RadGridDetalleSalida.DataSource = articulosPorSector;
                RadGridDetalleSalida.DataBind();
            }
        }

        protected void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            GenerarReportePorOla();
        }

        private void GenerarReportePorOla()
        {
            if (_idOla < 0)
            {
                Mensaje("error", "Debe seleccionar una Ola", "");
                return;
            }

            ReportDocument report = new CrystalReportes.Certificacion.CRCertificacion();

            List<E_CertificacionDetalle> articulosReporte = _listaDetalleCertificacion;
            var nombreSector = ddSector.SelectedItem.Text;
            if (nombreSector != "--Todos--")
            {
                articulosReporte = _listaDetalleCertificacion.FindAll(x => x.NombreSector == ddSector.SelectedItem.Text);
            }

            if (ddAlistador.SelectedItem.Text != "--Todos--")
            {
                articulosReporte = articulosReporte.FindAll(x => x.NombreUsuario == ddAlistador.SelectedItem.Text);
            }

            List<CRCertificacionDe> crCertificacionDeList = new List<CRCertificacionDe>(articulosReporte.Count);
            crCertificacionDeList.AddRange(articulosReporte.Select(i => new CRCertificacionDe()
            {
                CantidadAlistada = i.CantidadAlistada,
                CantidadDisponible = i.CantidadDisponible,
                CantidadSolicitada = i.Cantidad,
                NombreUsuario = i.NombreUsuario,
                Nombre = i.NombreArticulo,
                IdInternoPanal = i.IdInternoArticulo
            }));
            report.Database.Tables[1].SetDataSource(crCertificacionDeList);

            List<CRCertificacionEn> cRCertificacionEns = new List<CRCertificacionEn>();
            var OlasFacturas = _listMaestros.Where(x => x.IdMaestroSolicitud == _idOla).ToList<e_OPESALMaestroSolicitud>();
            string numeroOla = "";
            if (OlasFacturas.Count > 0)
            {
                CRCertificacionEn cRCertificacionEn = new CRCertificacionEn();
                cRCertificacionEn.Ruta = OlasFacturas[0].Nombre;
                cRCertificacionEn.idRegistroOlaRuta = OlasFacturas[0].IdInterno;
                numeroOla = OlasFacturas[0].IdInterno;
                cRCertificacionEn.NumeroLineas = articulosReporte.Count;
                cRCertificacionEns.Add(cRCertificacionEn);
            }
            report.Database.Tables[0].SetDataSource(cRCertificacionEns);

            var fileName = "Certificacion_" + numeroOla + "_" + nombreSector + ".pdf";
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\Certificacion\" + fileName;

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

        protected void ddAlistador_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<E_CertificacionDetalle> articulosPorSector = new List<E_CertificacionDetalle>();
            if (ddSector.SelectedItem.Text == "--Todos--")
            {
                articulosPorSector = _listaDetalleCertificacion;
            }
            else
            {
                articulosPorSector = _listaDetalleCertificacion.FindAll(x => x.NombreSector == ddSector.SelectedItem.Text);

            }

            var nombreAlistador = ddAlistador.SelectedItem.Text;
            if (nombreAlistador == "--Todos--")
            {
                RadGridDetalleSalida.DataSource = articulosPorSector;
                RadGridDetalleSalida.DataBind();
            }
            else
            {
                List<E_CertificacionDetalle> articulosPorSectorAlistador = articulosPorSector.FindAll(x => x.NombreUsuario == nombreAlistador);
                RadGridDetalleSalida.DataSource = articulosPorSectorAlistador;
                RadGridDetalleSalida.DataBind();
            }
        }

    }
}