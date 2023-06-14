using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using Diverscan.MJP.Negocio.MotorDecisiones;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Negocio.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.MotivoAjusteInventario;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Entidades.Common;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.CustomEvent;
using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Negocio.TRAIngresoSalida;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.Inventario;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class AjusteInvertarioV2 : System.Web.UI.Page, IUbicacionEtiquetaViewer
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;


        public event EventHandler UbicacionEtiqueta;

        protected void Page_Load(object sender, EventArgs e)
        {
           // UbicacionEtiquetaLoader ubicacionEtiquetaLoader = new UbicacionEtiquetaLoader(this, new UbicacionEtiquetaDataAcces());                     
            
            UsrLogged = (e_Usuario)Session["USUARIO"];           

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            if (!IsPostBack)
            {
                _rdpA_fechaInicio.SelectedDate = DateTime.Now;
                _rdpA_fechaFin.SelectedDate = DateTime.Now;
                loadMotivoAjusteInventarioSolicitud(true);
                buscar();
                loadEstado();
            }
        }

        private void loadEstado()
        {
            var enumArray = Enum.GetValues(typeof(EstadoEnum));
            RDDLEstado.DataSource = enumArray;
            RDDLEstado.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);

            //this.UpdatePanelAccesosRoles.Unload += new EventHandler(UpdatePanel_Unload2);
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
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

        public void RegisterUpdatePanel2(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                }
            }
        }

        #region TabsControl

        #region ControlRadGrid

      
       
        
        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            try
            {
                RadGrid RG = (RadGrid)sender;
                if (RG.ID == "RadGrid1")
                {
                    ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                    bool checkHeader = true;
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                        {
                            checkHeader = false;
                            break;
                        }
                    }
                    GridHeaderItem headerItem = RG.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                    (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
                }
            }
            catch (Exception)
            {


            }
        }

        /// <summary>
        /// Evento que se dispara cuando se hace click sobre el encabezado de la columna tipo checkbox. 
        /// El checbox se crea desde el inicio en conjunto con la etiqueta antetior.
        /// Antes de asignarle el datasource al RadGrid. Tiene que estar dentro de le etiqueta columns, y
        /// el AutoPostBack debe ser True, de lo contrario no dispara el evento.
        ///     <HeaderTemplate>
        ///         <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
        ///         AutoPostBack="True" />
        ///     </HeaderTemplate>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            try
            {
                RadGrid RG = (RadGrid)sender;
                if (RG.ID == "RadGrid1")
                {
                    CheckBox headerCheckBox = (sender as CheckBox);
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                        dataItem.Selected = headerCheckBox.Checked;
                    }
                }
            }
            catch (Exception)
            {


            }

        }

        /// <summary>
        /// Este metodo se ejecuta cada vez que se presiona una fila.
        /// Para que funcion tiene que estar declarado cuando se crea el RadGrid 
        /// asi -->  onitemcommand="RadGrid1_ItemCommand" y PostaBack activo 
        /// asi -->  EnablePostBackOnRowClick="true"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    break;
                default:
                    break;
            }

        }

                #endregion //ControlRadGrid
  
        #endregion //TabsControl


        #region Solicitar Ajuste Invertario

        #region Events
        protected void _rblSolicitudTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = _rblSolicitudTipo.SelectedValue;
            if (value == "Ajuste De Entrada")
            {
                _lblS_UbicacionActual.Text = "Ubicación: ";
                _lblS_UbicacionActual.Visible = true;
                _txtS_UbicacionMover.Visible = false;
                _lblS_UbicacionMover.Visible = false;
                loadMotivoAjusteInventarioSolicitud(true);
            }
            if (value == "Ajuste De Salida")
            {
                _lblS_UbicacionActual.Text = "Ubicación: ";
                _lblS_UbicacionActual.Visible = true;
                _txtS_UbicacionMover.Visible = false;
                _lblS_UbicacionMover.Visible = false;
                loadMotivoAjusteInventarioSolicitud(false);
            }
            if (value == "Ajuste De Traslado")
            {
                _lblS_UbicacionActual.Text = "Ubicación Actual: ";
                _lblS_UbicacionMover.Text = "Ubicación a Mover: ";
                _lblS_UbicacionMover.Visible = true;
                _txtS_UbicacionMover.Visible = true;
            }
        }
        protected void _btnLimpiarSolicitud_Click(object sender, EventArgs e)
        {
            limpiarSolucion();            
        }
        private void limpiarSolucion()
        {
            _txtS_CodigoBarras.Text = "";
            _txtS_UbicacionActual.Text = "";
            _txtS_UbicacionMover.Text = "";        
        }                
        protected void _rgArticulosXSolicitud_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rgArticulosXSolicitud.DataSource = _articuloXSolicitudAjusteData;
        }
        #endregion
        
        private void loadMotivoAjusteInventarioSolicitud(bool tipoAjuste)
        {
            var motivos = MotivoAjusteInventarioLoader.ObtenerTodosRegistros(tipoAjuste);
            motivos.RemoveAll(x => x.IdAjusteInventario == 1 || x.IdAjusteInventario == 2);
            _ddlMotivoSolicitud.DataTextField = "Nombre";
            _ddlMotivoSolicitud.DataValueField = "IdAjusteInventario";
            _ddlMotivoSolicitud.DataSource = motivos;
            _ddlMotivoSolicitud.DataBind();
        }
        private void buscar()
        {
            //var today = DateTime.Now;
            //DateTime fecha = new DateTime(today.Year,today.Month,today.Day);           
            //int estado = 1;
            //var logList = N_SolicitudAjusteInventario.GetSolicitudAjusteInventario(fecha, fecha, estado);
            //_rgArticulosXSolicitud.DataSource = logList;
            //_rgArticulosXSolicitud.DataBind();
            //_articuloXSolicitudAjusteRecordDetalleData = logList;
        }
        private List<ArticuloXSolicitudAjusteRecord> _articuloXSolicitudAjusteData
        {
            get
            {
                var data = ViewState["articuloXSolicitudAjusteRecord"] as List<ArticuloXSolicitudAjusteRecord>;
                if (data == null)
                {
                    data = new List<ArticuloXSolicitudAjusteRecord>();
                    ViewState["articuloXSolicitudAjusteRecord"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articuloXSolicitudAjusteRecord"] = value;
            }
        }
        public void ShowMessage(string message)
        {
            Mensaje("error", message, "");
        }

        #region Metodos Privados
        private long getIdUbicacion(string etiqueta)
        {
           return UbicacionEtiquetaLoader.OtenerIdUbicacion(etiqueta);

            //long idUbicacion = 0;
            //UbicacionEtiquetaEvent ubicacionEtiquetaEvent = new UbicacionEtiquetaEvent(etiqueta);
            //if (UbicacionEtiqueta != null)
            //    UbicacionEtiqueta(this, ubicacionEtiquetaEvent);

            //if (ViewState.Count > 0)
            //{
            //    idUbicacion = (long)ViewState["IdUbicacion"];
            //}
            //return idUbicacion;
        }
        public void SetIdUbicacion(long idUbicacion)
        {
            ViewState["IdUbicacion"] = idUbicacion;
        }
        #endregion

        #endregion       
                
        #region Busquedas

        private void buscarAprobados()
        {
            try
            {
                DateTime fechaInicio = _rdpA_fechaInicio.SelectedDate ?? DateTime.Now;
                DateTime fechaFin = _rdpA_fechaFin.SelectedDate ?? DateTime.Now;
                int estado = (int)Enum.Parse(typeof(EstadoEnum), RDDLEstado.SelectedText);
                int idBodega = ((e_Usuario)Session["USUARIO"]).IdBodega;
                var logList = N_SolicitudAjusteInventario.GetSolicitudAjusteInventario(
                    fechaInicio, fechaFin, estado, idBodega);
                RGSolicitudAjustesInventario.DataSource = logList;
                RGSolicitudAjustesInventario.DataBind();
                _aprobadosAjusteSolicitudData = logList;

                _rgArticulosXSolicitudDetalle.DataSource = new List<AjusteSolicitudRecord>();
                _rgArticulosXSolicitudDetalle.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
        }

        private List<AjusteSolicitudRecord> _aprobadosAjusteSolicitudData
        {
            get
            {
                var data = ViewState["AprobadosAjusteSolicitudData"] as List<AjusteSolicitudRecord>;
                if (data == null)
                {
                    data = new List<AjusteSolicitudRecord>();
                    ViewState["AprobadosAjusteSolicitudData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["AprobadosAjusteSolicitudData"] = value;
            }
        }
               
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarAprobados();
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
        protected void _btnSolicitarAjuste_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(UsrLogged.IdUsuario);
                long idAjusteInventario = long.Parse(_ddlMotivoSolicitud.SelectedValue);
                int estado = 1;
                SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord = new SolicitudAjusteInventarioRecord(idUsuario, idAjusteInventario, estado);
                N_SolicitudAjusteInventario.InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(solicitudAjusteInventarioRecord, _articuloXSolicitudAjusteData);

                _articuloXSolicitudAjusteData = new List<ArticuloXSolicitudAjusteRecord>();
                _rgArticulosXSolicitud.DataSource = _articuloXSolicitudAjusteData;
                _rgArticulosXSolicitud.DataBind();
                Mensaje("info", "Fue Solicitado Con Exito", "");
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
            finally
            {
                limpiarSolucion();                
            }
        }
        protected void _btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_txtS_CodigoBarras.Text))
                {
                    Mensaje("info", "Debe de ingresar el codigo de barras", "");
                    return;
                }
                if (string.IsNullOrEmpty(_txtS_UbicacionActual.Text))
                {
                    Mensaje("info", "Debe de ingresar la ublicacion", "");
                    return;
                }
                var codigoBarrar = _txtS_CodigoBarras.Text.Trim();
                var ubicacion = _txtS_UbicacionActual.Text.Trim();
                string codLeido = codigoBarrar + ";" + ubicacion + ";" + _txtS_UbicacionMover.Text + ";1";
                var gs1Data = GS1Extractor.ExtraerGS1(codigoBarrar, UsrLogged.IdUsuario);
                if (!string.IsNullOrEmpty(gs1Data.IdArticulo) || !string.IsNullOrEmpty(gs1Data.IdUbicacion) ||
                    !string.IsNullOrEmpty(gs1Data.Lote) || !string.IsNullOrEmpty(gs1Data.Cantidad) || !string.IsNullOrEmpty(gs1Data.FechaVencimiento))
                {
                    long idArticulo = int.Parse(gs1Data.IdArticulo);
                    string lote = gs1Data.Lote;
                    int cantidad = Convert.ToInt32(Single.Parse(gs1Data.Cantidad));                    
                    DateTime fechaVencimiento = DateTime.Parse(gs1Data.FechaVencimiento);


                    var newEtiqueta = String.Format("({0}){1}", ubicacion.Substring(0, 2), ubicacion.Substring(2));
                    var idUbicacionActual = getIdUbicacion(newEtiqueta);

                    if (idUbicacionActual > 0)
                    {
                        long idUbicacionMover = idUbicacionActual;
                        if (string.IsNullOrEmpty(_lblS_UbicacionMover.Text))
                        {
                            newEtiqueta = String.Format("({0}){1}", ubicacion.Substring(0, 2), ubicacion.Substring(2));
                            idUbicacionMover = getIdUbicacion(newEtiqueta);
                        }


                        var extraInfoArticulo = N_DetalleArticulo.ObtenerArticuloPorIdArticulo(idArticulo);
                        if (extraInfoArticulo.EsGranel)
                        {
                            cantidad = gs1Data.Peso;                            
                        }
                        ArticuloXSolicitudAjusteRecord articuloXSolicitudAjusteRecord = new ArticuloXSolicitudAjusteRecord(extraInfoArticulo.IdArticulo, extraInfoArticulo.CodigoInterno,
                            extraInfoArticulo.NombreArticulo, extraInfoArticulo.UnidadMedida, extraInfoArticulo.EsGranel, lote,
                            fechaVencimiento, idUbicacionActual, idUbicacionMover, cantidad);

                        var auxArticuloXSolicitudAjusteData = _articuloXSolicitudAjusteData;
                        auxArticuloXSolicitudAjusteData.Add(articuloXSolicitudAjusteRecord);
                        _articuloXSolicitudAjusteData = auxArticuloXSolicitudAjusteData;

                        _rgArticulosXSolicitud.DataSource = _articuloXSolicitudAjusteData;
                        _rgArticulosXSolicitud.DataBind();
                    }
                    else
                        Mensaje("info", "No se encontro información relacionada con la Ubicación.", "");
                }
                else
                    Mensaje("info", "No se encontro información relacionada con el codigo de barras.", "");
                buscar();
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
            finally
            {
                limpiarSolucion();
            }
        }        
        #region Grids

        protected void RGLogAjustesInventario_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CheckBox cb = new CheckBox();
            switch (e.CommandName)
            {
                case "RowClick":
                    {
                        var solicitudAjuste = _aprobadosAjusteSolicitudData[e.Item.ItemIndex];
                        if (solicitudAjuste != null)
                        {
                            var articulos = N_ArticuloXSolicitudAjuste.ObtenerAgrupadoPorLote(solicitudAjuste.IdSolicitudAjusteInventario);
                            _rgArticulosXSolicitudDetalle.DataSource = articulos;
                            _rgArticulosXSolicitudDetalle.DataBind();

                            var costoTotal = articulos.Sum(x => x.PrecioXCantidad);
                            _lblCostoAjuste.Text = "Costo Ajuste: " + costoTotal;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        
        protected void RGSolicitudAjustesInventario_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rgArticulosXSolicitudDetalle.DataSource = _aprobadosAjusteSolicitudData;
        }

        #endregion       
    }
}