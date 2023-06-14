using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Common;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.Inventario;
using Diverscan.MJP.Negocio.MotivoAjusteInventario;
using Diverscan.MJP.Negocio.TRAIngresoSalida;
using Diverscan.MJP.UI.CrystalReportes.AjusteInventario;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class AutorizacionAjusteInventario : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        const string _mesajeSeleccionar = "--Seleccionar--";
        private long _idSolicitudAjusteSeleccionado 
        {
            get
            {
                long result = -1;
                var data = ViewState["idSolicitudAjuste"];
                if (data != null)
                {
                    result = long.Parse(data.ToString());
                }
                return result;
            }
            set
            {
                ViewState["idSolicitudAjuste"] = value;
            }          
        }

        private List<AjusteSolicitudRecord> _ajusteSolicitudData
        {
            get
            {
                var data = ViewState["LogAjusteInventarioData"] as List<AjusteSolicitudRecord>;
                if (data == null)
                {
                    data = new List<AjusteSolicitudRecord>();
                    ViewState["LogAjusteInventarioData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["LogAjusteInventarioData"] = value;
            }
        }

        private List<ArticuloXSolicitudAjusteDetalle> _detalleAjusteSolicitud 
        {
            get
            {
                var data = ViewState["detalleAjusteSolicitud"] as List<ArticuloXSolicitudAjusteDetalle>;
                if (data == null)
                {
                    data = new List<ArticuloXSolicitudAjusteDetalle>();
                    ViewState["detalleAjusteSolicitud"] = data;
                }
                return data;
            }
            set
            {
                ViewState["detalleAjusteSolicitud"] = value;
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
                _idSolicitudAjusteSeleccionado = -1;
                loadEstado();
                loadCentroCosto(_mesajeSeleccionar);
                RDPFechaInicio.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
                 _idUsuario = Convert.ToInt32(UsrLogged.IdUsuario.Replace("&nbsp;", ""));
                TraducirFiltrosTelerik.traducirFiltros(RGSolicitudAjustesInventario.FilterMenu);
            }
        }

        private int _idUsuario
        {
            get
            {
                var result = -1;
                var data = ViewState["IdUsuario"];
                if (data != null)
                {
                    result = Convert.ToInt32(data);
                }
                return result;
            }
            set
            {
                ViewState["IdUsuario"] = value;
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
            
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGSolicitudAjustesInventario.DataSource = _ajusteSolicitudData;

        }
             
        protected void RGLogAjustesInventario_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //GridDataItem item = (GridDataItem)e.Item;

            if (e.CommandName == "RowClick")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    long value = Convert.ToInt32(item["IdSolicitudAjusteInventario"].Text);

                    _detalleAjusteSolicitud = N_ArticuloXSolicitudAjuste.ObtenerAgrupadoPorLote(value);
                    _rgArticulosXSolicitudDetalle.DataSource = _detalleAjusteSolicitud;
                    _rgArticulosXSolicitudDetalle.DataBind();

                    var costoTotal = _detalleAjusteSolicitud.Sum(x => x.PrecioXCantidad);
                    _lblCostoAjuste.Text = "Costo Ajuste: " + costoTotal;

                    _rddlCentroCosto.SelectedText = _mesajeSeleccionar;

                    _idSolicitudAjusteSeleccionado = value;
                }
                catch (Exception ex)
                {
                    var cl = new clErrores();
                    cl.escribirError(ex.Message, ex.StackTrace);
                    ex.ToString();
                }
            }        
        }

        private void loadEstado()
        {
            var enumArray = Enum.GetValues(typeof(EstadoEnum));
            RDDLEstado.DataSource = enumArray;
            RDDLEstado.DataBind();
        }

        private void loadCentroCosto(string valor)
        {
            var centroCostos = n_CentroCostos.ObtenerCentroDeCostos();
            _rddlCentroCosto.DataTextField = "Nombre";
            _rddlCentroCosto.DataValueField = "IdCentroCostos";
            _rddlCentroCosto.DataSource = centroCostos;
            _rddlCentroCosto.DataBind();
            _rddlCentroCosto.Items.Insert(0, new Telerik.Web.UI.DropDownListItem(_mesajeSeleccionar, "0"));
            _rddlCentroCosto.SelectedText = "Bodega Tibas";                                
        }        
        
        public string getSearchType()
        {
            var value = _rblSearchType.SelectedIndex;
            string searchType = "";
            if (value == 0) // FULL SEARCH
            {
                searchType = "full_search";
            }
            else if (value == 1) // ID SEARCH
            {
                searchType = "id_search";
            }
            return searchType;
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar(getSearchType());
        }

        private void buscar(string searchType)
        {
            try
            {
                
                var logList = new List<AjusteSolicitudRecord>();
                int idBodega = ((e_Usuario)Session["USUARIO"]).IdBodega;
                if (searchType.Equals("full_search"))
                {
                    DateTime fechaInicio = RDPFechaInicio.SelectedDate ?? DateTime.Now;
                    DateTime fechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;
                    if (fechaInicio > fechaFin)
                    {
                        Mensaje("info", "La fecha de Inicio no puede ser mayor a la fecha final ", "");
                    }
                    int estado = (int)Enum.Parse(typeof(EstadoEnum), RDDLEstado.SelectedText);
                    logList = N_SolicitudAjusteInventario.GetSolicitudAjusteInventario(
                        fechaInicio, fechaFin, estado, idBodega);
                    

                }
                else if (searchType.Equals("id_search"))
                {
                    long idSolicitudAjusteInventario = long.Parse(TxtIdSolicitudAjusteInventario.Text);
                    logList = N_SolicitudAjusteInventario.GetSolicitudAjusteInventarioPorID(
                        idSolicitudAjusteInventario, idBodega);
                }
                _ajusteSolicitudData = logList;
                RGSolicitudAjustesInventario.DataSource = _ajusteSolicitudData;
                RGSolicitudAjustesInventario.DataBind();
                

                _rgArticulosXSolicitudDetalle.DataSource = new List<AjusteSolicitudRecord>();
                _rgArticulosXSolicitudDetalle.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }           
        }
                      
        public void btnAprobar_Click(object sender, EventArgs e)
        {
            aprobar();
            //ModalPopupExtender1.Show();
          //  Mensaje("info", "La solicitud fue aprobada. ", "");
        }
       

        private void aprobar()
        {
            var idCentroCosto = 1;

            try
            {
                if (RDDLEstado.SelectedText == "Pendientes")
                {
                   
                    for (int i = 0; i < RGSolicitudAjustesInventario.Items.Count; i++)
                    {
                        var item = RGSolicitudAjustesInventario.Items[i];
                        var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                        if (checkbox != null && checkbox.Checked)
                        {
                            var idRecord = long.Parse(item["IdSolicitudAjusteInventario"].Text);
                            if (idRecord > 0)
                            {
                                if (_ajusteSolicitudData.Count > 0)
                                 {
                                    var solicitud = _ajusteSolicitudData.Find(x => x.IdSolicitudAjusteInventario == idRecord);
                                    if (solicitud != null && solicitud.IdSolicitudAjusteInventario > 0)
                                    {
                                        if (solicitud.TipoMotivo == "Entrada")
                                        {
                                            registrarEntrada(solicitud);

                                            N_SolicitudAjusteInventario.
                                                UpdateSolicitudAjusteInventario(idRecord, 2, idCentroCosto, _idUsuario);
                                         
                                        }
                                        else if (solicitud.TipoMotivo == "Salida")
                                        {
                                            var result = registrarSalida(solicitud);
                                            if (result)
                                            {
                                                
                                                N_SolicitudAjusteInventario.UpdateSolicitudAjusteInventario(idRecord, 2, idCentroCosto, _idUsuario);

                                            }
                                            else
                                            {
                                                Mensaje("info", "La solicitud fue rechazada.", "");
                                                N_SolicitudAjusteInventario.UpdateSolicitudAjusteInventario(idRecord, 3, idCentroCosto, _idUsuario);                      
                                            }
                                        }
                                    }
                                }
                                Mensaje("info", "La solicitud fue aprobada. ", "");
                            }
                        }
                    }                  
                    buscar(getSearchType());
                }
                //ModalPopupExtender1.Hide();
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
        }
        
        protected void btnSi_Click(object sender, EventArgs e)
        {
            
        }


        #region TRA
        
        public void registrarEntrada(AjusteSolicitudRecord solicitud)
        {
            registrarMovimiento(solicitud, true, 12);           
        }

        private bool registrarSalida(AjusteSolicitudRecord solicitud)
        {
            return registrarMovimientoSalida(solicitud, false, 14);           
        } 

        private void registrarMovimiento(AjusteSolicitudRecord solicitud,bool sumUno_RestaCero, int idEstado)
        {
            var articulos = N_ArticuloXSolicitudAjuste.ObtenerAgrupadoPorLote(solicitud.IdSolicitudAjusteInventario);

            if (articulos.Count > 0)
            {
                var IDLogAjustesTRA = N_LogAjustesTRA.InsertLogAjustesTRA_Y_ObtenerIDLogAjustesTRA                                                                      (solicitud.IdSolicitudAjusteInventario);
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                if (IDLogAjustesTRA > 0)
                {
                    List<TRAIngresoSalidaArticulosRecord> listaAGuardar = new List<TRAIngresoSalidaArticulosRecord>();

                    for (int p = 0; p < articulos.Count; p++)
                    {
                        long idArticulo = articulos[p].IdArticulo;
                        var idUbicacion = articulos[p].IdUbicacionActual;
                        var lote = articulos[p].Lote;
                        DateTime fechaVencimiento = articulos[p].FechaVencimiento;
                                                
                        long idMetodoAccion = 8;
                        string idTablaCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA";
                        string idCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA.IdLogAjustesTRA";
                        string numDocumentoAccion = IDLogAjustesTRA.ToString();
                        bool procesado = false;
                        int cantidad =  1;
                        if (articulos[p].EsGranel)
                        {
                            cantidad = articulos[p].Cantidad;
                            var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                   sumUno_RestaCero, idArticulo, fechaVencimiento, lote, idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion,
                                   idCampoDocumentoAccion, numDocumentoAccion, idUbicacion, cantidad, procesado, DateTime.Now, idEstado);
                            listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                            //N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                        }
                        else
                        {                            
                            //for (int i = 0; i < articulos[p].Cantidad; i++)
                            //{
                                var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                    sumUno_RestaCero, idArticulo, fechaVencimiento, lote, idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion,
                                    idCampoDocumentoAccion, numDocumentoAccion, idUbicacion, articulos[p].Cantidad, procesado, DateTime.Now, idEstado);
                                listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                                //N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                            //}
                        }
                    }
                    N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(listaAGuardar);
                }
            }
            else
                Mensaje("info", "La solicitud no tiene articulos", "");
        }

        private bool registrarMovimientoSalida(AjusteSolicitudRecord solicitud, bool sumUno_RestaCero, int idEstado)
        {
            bool resultado = false;
            var articulos = N_ArticuloXSolicitudAjuste.ObtenerAgrupadoPorLote(solicitud.IdSolicitudAjusteInventario);
            var existen = ComprobarCantidadArticulosSalida(articulos);
            if (articulos.Count > 0 && existen)
            {
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                List<TRAIngresoSalidaArticulosRecord> listaAGuardar = new List<TRAIngresoSalidaArticulosRecord>();
                for (int p = 0; p < articulos.Count; p++)
                {
                    long idArticulo = articulos[p].IdArticulo;
                    var idUbicacion = articulos[p].IdUbicacionActual;
                    var lote = articulos[p].Lote;
                    DateTime fechaVencimiento = articulos[p].FechaVencimiento;

                    long idMetodoAccion = 8;
                    string idTablaCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos";
                    string idCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos.idRegistro";

                    bool procesado = false;
                    int cantidad = 1;

                    //var articulosParaSalida = N_TRAResumen.ObtenerArticuloSalida(idArticulo, idUbicacion, lote, fechaVencimiento);
                    // N_TRAResumen.ObtenerCantidadArticulosSalida(49, 772, "301017", DateTime.Parse("2018-01-30"));
                    //var articulosParaSalida = N_TRAResumen.ObtenerCantidadArticulosSalida(idArticulo, idUbicacion, lote, fechaVencimiento);

                    if (articulos[p].EsGranel)
                    {
                        //if (articulosParaSalida > 0)
                        //{
                            //string numDocumentoAccion = articulosParaSalida[0].IdRegistro.ToString();
                            string numDocumentoAccion = "";
                            var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                sumUno_RestaCero, idArticulo, fechaVencimiento, lote, idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion,
                                idCampoDocumentoAccion, numDocumentoAccion, idUbicacion, articulos[p].Cantidad, procesado, DateTime.Now, idEstado);
                            //N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                            listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                            //articulosParaSalida.RemoveAt(0);
                            resultado = true;
                        //}
                        //else
                        //{
                        //    Mensaje("info", "Solicitud parcialmente aceptada, No existen articulos para el descuento", "");
                        //}
                    }
                    else
                    {
                        //for (int i = 0; i < articulos[p].Cantidad; i++)
                        //{
                            //if (articulosParaSalida > 0)
                            //{
                                //string numDocumentoAccion = articulosParaSalida[0].IdRegistro.ToString();
                                string numDocumentoAccion = "";
                                var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                    sumUno_RestaCero, idArticulo, fechaVencimiento, lote, idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion,
                                    idCampoDocumentoAccion, numDocumentoAccion, idUbicacion, articulos[p].Cantidad, procesado, DateTime.Now, idEstado);
                                //N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                                listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                                //articulosParaSalida.RemoveAt(0);
                                resultado = true;
                            //}
                            //else
                            //{
                            //    Mensaje("info", "Solicitud parcialmente aceptada, No existen articulos para el descuento", "");
                            //    break;
                            //}
                        //}
                    }
                }
                N_TRAIngresoSalida.InsertTRASalidaRecord(listaAGuardar);
            }
            else
            {
                Mensaje("info", "La solicitud fue rechazada no hay articulos para realizar el ajuste ", "");
                resultado = false;
            }
            return resultado;
        }

        private bool ComprobarCantidadArticulosSalida(List<ArticuloXSolicitudAjusteDetalle> articulosSolicitud)
        {
            for (int p = 0; p < articulosSolicitud.Count; p++)
            {
                long idArticulo = articulosSolicitud[p].IdArticulo;
                var idUbicacion = articulosSolicitud[p].IdUbicacionActual;
                var lote = articulosSolicitud[p].Lote;
                DateTime fechaVencimiento = articulosSolicitud[p].FechaVencimiento;
                //var articulosParaSalida = N_TRAResumen.ObtenerArticuloSalida(idArticulo, idUbicacion, lote, fechaVencimiento);
                var cantidadArticulo = N_TRAResumen.ObtenerCantidadArticulosSalida(idArticulo, idUbicacion, lote, fechaVencimiento);
                if (cantidadArticulo < articulosSolicitud[p].Cantidad)
                    return false;
            }
            return true;
        }

        #endregion
                
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

        protected void btnRechazoSi_Click(object sender, EventArgs e)
        {
            rechazar();
        }

        private void rechazar()
        {
            //if (string.IsNullOrEmpty(_rddlCentroCosto.SelectedValue) || _rddlCentroCosto.SelectedText == _mesajeSeleccionar)
            //{
            //    Mensaje("info", "Debe Seleccionar un Centro de Costo", "");
            //    return;
            //}
            var idCentroCosto = 1;
            try
            {
                if (RDDLEstado.SelectedText == "Pendientes")
                {
                    for (int i = 0; i < RGSolicitudAjustesInventario.Items.Count; i++)
                    {
                        var item = RGSolicitudAjustesInventario.Items[i];
                        var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                        if (checkbox != null && checkbox.Checked)
                        {
                            var idRecord = long.Parse(item["IdSolicitudAjusteInventario"].Text);
                            if (idRecord > 0)
                            {
                                if (_ajusteSolicitudData.Count > 0)
                                {
                                    var solicitud = _ajusteSolicitudData.Find(x => x.IdSolicitudAjusteInventario == idRecord);
                                    if (solicitud != null && solicitud.IdSolicitudAjusteInventario > 0)
                                    {
                                        N_SolicitudAjusteInventario.UpdateSolicitudAjusteInventario(idRecord, 3, idCentroCosto, _idUsuario);
                                    }
                                }
                                
                            }
                            Mensaje("info", "La solicitud fue rechazada. ", "");
                        }
                    }
                    buscar(getSearchType());
                }
                //ModalPopupExtender1.Hide();
            }
            catch (Exception ex)
            {
                Mensaje("info", "Ha ocurrido un error, por favor intente lo más tarde o contacte a TI.", "");
                Mensaje("error", ex.Message, "");
            }
        
        }

        protected void _btnRechazar_Click(object sender, EventArgs e)
        {
            rechazar();
            //ModalPopupExtender2.Show();
        }

        protected void RDDLEstado_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            var estado = (EstadoEnum)Enum.Parse(typeof(EstadoEnum), RDDLEstado.SelectedText);
            if (estado == EstadoEnum.Pendientes)
            {
                _btnAprobar.Visible = true;
                _btnRechazar.Visible = true;
                //_rddlCentroCosto.Visible = true;    
            }
            else 
            {
                _btnAprobar.Visible = false;
                _btnRechazar.Visible = false;
                _rddlCentroCosto.Visible = false;
            }
        }

        protected void _rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = _rblSearchType.SelectedIndex;
            if(value == 0) // FULL SEARCH
            {
                LblFechaInicio.Text = "Fecha Inicio:";
                RDPFechaInicio.Visible = true;
                LblFechaFinal.Visible = true;
                RDPFechaFinal.Visible = true;
                LBLEstado.Visible = true;
                RDDLEstado.Visible = true;
                TxtIdSolicitudAjusteInventario.Visible = false;
                btnBuscar.Visible = true;
            } 
            else if (value == 1) // ID SEARCH
            {
                LblFechaInicio.Text = "Ingrese el ID de Solicitud de Ajuste Inventario a buscar";
                RDPFechaInicio.Visible = false;
                LblFechaFinal.Visible = false;
                RDPFechaFinal.Visible = false;
                LBLEstado.Visible = false;
                RDDLEstado.Visible = false;
                TxtIdSolicitudAjusteInventario.Visible = true;
                btnBuscar.Visible = true;
            }

        }

        protected void _rgArticulosXSolicitudDetalle_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //var articulos = N_ArticuloXSolicitudAjuste.ObtenerAgrupadoPorLote(_idSolicitudAjusteSeleccionado);
            _rgArticulosXSolicitudDetalle.DataSource = _detalleAjusteSolicitud;            
        }

        protected void BtnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (_idSolicitudAjusteSeleccionado <= 0) 
            {
                Mensaje("error", "Debe seleccionar una Solicitud de Ajuste", "");
                return;
            }

            if (_detalleAjusteSolicitud.Count <= 0) 
            {
                Mensaje("error", "La Solicitud de Ajuste no tiene detalle", "");
                return;
            }

            ReportDocument report = new CrystalReportes.AjusteInventario.CRAjusteInventario();

            List<AjusteSolicitudRecord> encabezado = new List<AjusteSolicitudRecord>();
            AjusteSolicitudRecord temp = _ajusteSolicitudData.Find(x => x.IdSolicitudAjusteInventario == _idSolicitudAjusteSeleccionado);
            encabezado.Add(temp);

            report.Database.Tables[0].SetDataSource(encabezado);
            report.Database.Tables[1].SetDataSource(_detalleAjusteSolicitud);

            var fileName = "Ajuste " + _idSolicitudAjusteSeleccionado + " " + ".pdf";
            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\AjusteInventario\" + fileName;

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

            // List<AjusteSolicitudRecord> _ajusteSolicitudData
            //List<ArticuloXSolicitudAjusteDetalle>
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            if (_idSolicitudAjusteSeleccionado <= 0)
            {
                Mensaje("error", "Debe seleccionar una Solicitud de Ajuste", "");
                return;
            }

            if (_detalleAjusteSolicitud.Count <= 0)
            {
                Mensaje("error", "La Solicitud de Ajuste no tiene detalle", "");
                return;
            }

            FileExceptionWriter exceptionWriter = new FileExceptionWriter();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ArticuloXSolicitudAjusteDetalle));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[8];
                propertiesSelected[0] = properties.Find("IdSolicitudAjusteInventario", false);
                propertiesSelected[1] = properties.Find("CodigoInterno", false);
                propertiesSelected[2] = properties.Find("NombreArticulo", false);
                propertiesSelected[3] = properties.Find("Lote", false);
                propertiesSelected[4] = properties.Find("FechaVencimiento", false);
                propertiesSelected[5] = properties.Find("UnidadMedida", false);              
                propertiesSelected[6] = properties.Find("EtiquetaActual", false);
                propertiesSelected[7] = properties.Find("CantidadUI", false);
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("Inventario.xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                List<string> headers = new List<string>() { "Id Solicitud", "SKU", "Nombre Articulo", "Lote", "Fecha de Venc.", "Unidad de Medida", "Ubicacion", "Cantidad" };
                ExcelExporter.ExportData(_detalleAjusteSolicitud, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
                Mensaje("error", "Ha ocurrido un error, vuelva a intentar.", "");
            }

        }
    }
}