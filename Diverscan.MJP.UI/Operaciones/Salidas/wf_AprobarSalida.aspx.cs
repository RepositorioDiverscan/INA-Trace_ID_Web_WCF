using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.Negocio.OPESALMaestroSolicitud;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.Usuarios;
using Diverscan.MJP.Negocio.SectorWarehouse;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_AprobarSalida : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        List<Entidades.Usuarios.e_Usuarios> usersList = new List<Entidades.Usuarios.e_Usuarios>();
        string SQL = "";
        DataSet DS = new DataSet();
        static DataSet DSDatosExport = new DataSet(); //Para Grid Detalle
        static string idMaestroSolicitud = "";  //Obtener el IdPara cargar el detalle
        private string valor_bodega;
        private List<ESectorWarehouse> _sectorsWarehouse
        {
            get
            {
                var data = ViewState["sectorsWarehouse"] as List<ESectorWarehouse>;
                if (data == null)
                {
                    data = new List<ESectorWarehouse>();
                    ViewState["sectorsWarehouse"] = data;
                }
                return data;
            }
            set
            {
                ViewState["sectorsWarehouse"] = value;
            }
        }
        private List<e_OPESALMaestroSolicitud> _listOrders
        {
            get
            {
                var data = ViewState["listOrders"] as List<e_OPESALMaestroSolicitud>;
                if (data == null)
                {
                    data = new List<e_OPESALMaestroSolicitud>();
                    ViewState["listOrders"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listOrders"] = value;
            }
        }
        private List<EPrioridadOrden> _listaPrioridadOrden
        {
            get
            {
                var data = ViewState["listaPrioridadOrdendd"] as List<EPrioridadOrden>;
                if (data == null)
                {
                    data = new List<EPrioridadOrden>();
                    ViewState["listaPrioridadOrdendd"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaPrioridadOrdendd"] = value;
            }
        }
        private int _idMaestroSolicitud
        {
            get
            {
                var result = -1;
                var data = ViewState["IdMaestroSolicitud"];
                if (data != null)
                {
                    result = Convert.ToInt32(data);
                }
                return result;
            }
            set
            {
                ViewState["IdMaestroSolicitud"] = value;
            }
        }
        private int _idBodega
        {
            get
            {
                var objeto = ViewState["IdBodega"];
                var data = 0;

                if (objeto == null)
                {
                    data = 0;

                }
                else
                {
                    data = Convert.ToInt32(objeto);
                }
                ViewState["IdBodega"] = data;
                return data;
            }
            set
            {
                ViewState["IdBodega"] = value;
            }
        }
        private List<EDetalleSalidaArticuloSector> _listaDetalleOrden
        {
            get
            {
                var data = ViewState["listaDetalleOrden"] as List<EDetalleSalidaArticuloSector>;
                if (data == null)
                {
                    data = new List<EDetalleSalidaArticuloSector>();
                    ViewState["listaDetalleOrden"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaDetalleOrden"] = value;
            }
        }

        private List<ETareasUsuarioSolicitud> _listaTareasAlistador
        {
            get
            {
                var data = ViewState["listaTareasAlistador"] as List<ETareasUsuarioSolicitud>;
                if (data == null)
                {
                    data = new List<ETareasUsuarioSolicitud>();
                    ViewState["listaTareasAlistador"] = data;
                }
                return data;
            }
            set
            {
                ViewState["listaTareasAlistador"] = value;
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

                if (!UsrLogged.IdRoles.Equals("0"))
                {
                    Label17.Visible = false;
                    ddBodega.Visible = false;

                    _idBodega = UsrLogged.IdBodega;
                    cargarSectores();
                }
                else
                {
                    FillDDBodega();
                }

            }

            TraducirFiltrosTelerik.traducirFiltros(RGAprobarSalida.FilterMenu);
            TraducirFiltrosTelerik.traducirFiltros(RadGridDetalleSalida.FilterMenu);
            TraducirFiltrosTelerik.traducirFiltros(RadGridTasks.FilterMenu);

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
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RGAprobarSalida.DataSource = _listOrders;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
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
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud nOPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);

                DateTime fechaInicioBusqueda;
                DateTime fechaFinBusqueda;


                fechaInicioBusqueda = RDPFechaInicio.SelectedDate.Value;
                fechaFinBusqueda = RDPFechaFinal.SelectedDate.Value;
                string idInternoOrder = txtSearch.Text.Trim();

                /*   var xx = ddBodega.SelectedValue;
                   var yy = ddBodega.SelectedItem;  */

                if (UsrLogged.IdRoles.Equals("0"))
                {
                    if (ddBodega.SelectedIndex > 0)
                    {
                        _idBodega = Convert.ToInt32(ddBodega.SelectedValue.ToString());
                    }
                }

                if (string.IsNullOrEmpty(idInternoOrder) && _idBodega == default(int))
                {
                    Mensaje("error", "Debe ingresar los campos de busquedad requeridos!!!", "");
                    return;
                }
                else
                    _listOrders = nOPESALMaestroSolicitud.GetOrdersToEnlist(_idBodega, fechaInicioBusqueda, fechaFinBusqueda, idInternoOrder);

                RGAprobarSalida.DataSource = _listOrders;
                RGAprobarSalida.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
            }
        }
        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                //Original
                //RGAprobarSalida.DataSource = new string[] { };
                //RGAprobarSalida.DataSource = n_AprobarSalida.GetAprobarSalidas();
                //RGAprobarSalida.DataBind();
                //txtSearch.Text = "";
                //TxtSQL.Text = "";
                //SetDatetime();

                DS.Clear();
                //SQL = "";
                //SQL = "SELECT * FROM Vista_PreDetalleSolicitudPorBodega" +
                //      "  ORDER BY Fecha DESC";

                SQL = "SELECT * FROM Vista_PreDetalleSolicitudPorBodegaV2" + //Mejora en el rendimiento de la vista 
                "  ORDER BY Fecha DESC";

                DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                RGAprobarSalida.DataSource = new string[] { };
                RGAprobarSalida.DataSource = DS;
                RGAprobarSalida.DataBind();
                txtSearch.Text = "";

                SetDatetime();
                RadGridDetalleSalida.DataSource = null;
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
                if (_listaDetalleOrden != null)
                {
                    RadGridDetalleSalida.DataSource = _listaDetalleOrden;
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
        protected void RadGridDetalleSalida_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                idMaestroSolicitud = item["IdLineaDetalleSolicitud"].Text.Replace("&nbsp;", "");
            }
        }
        protected void RGAprobarSalida_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName == "btnVerDetalle")
            {
                try
                {
                    if (UsrLogged.IdRoles.Equals("0") && ddBodega.SelectedIndex < 0)
                    {
                        Mensaje("error", "¡Debe seleccionar una bodega!", "");
                        return;
                    }

                    e.Item.Selected = true;
                    GridDataItem item = (GridDataItem)e.Item;
                    _idMaestroSolicitud = Convert.ToInt32(item["IdMaestroSolicitud"].Text);
                    _chkAsignado.Visible = true;
                    _chkAsignado.Checked = false;


                    if (_idBodega > 0 && _idMaestroSolicitud > 0)
                    {
                        GetGetDetalleSalidaArticulosSector(_idBodega, _idMaestroSolicitud);
                        _lblchkAsignado.Visible = true;
                        _chkAsignado.Visible = true;
                        //CargarDetalleSolicitud(idMaestroSolicitud, idBodega);
                    }
                }
                catch (Exception ex)
                {
                    var cl = new clErrores();
                    cl.escribirError(ex.Message, ex.StackTrace);
                    ex.ToString();
                }
                //TxtidBodega.Text = idbodega;
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
            if (ddBodega.SelectedIndex > 0)
            {
                _idBodega = Convert.ToInt32(ddBodega.SelectedValue);

                cargarSectores();
            }
        }
        protected void ddSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSector.SelectedIndex > 0)
            {
                if (UsrLogged.IdRoles.Equals("0"))
                {
                    _idBodega = Convert.ToInt32(ddBodega.SelectedValue);
                }

                n_Usuario nUsuario = new n_Usuario();
                usersList = nUsuario.GetUserByRol(_idBodega, 5); //idRol alistador = 5     
                int idSector = Convert.ToInt32(ddSector.SelectedValue);
                FiltrarSectores(ddSector.SelectedItem.Text);
                if (ddSector.SelectedIndex > 1)
                    usersList = usersList.FindAll(x => x.IdSector == idSector);

                ddlIdUsuario.DataSource = usersList;
                ddlIdUsuario.DataTextField = "Nombre";
                ddlIdUsuario.DataValueField = "IdUsuario";
                ddlIdUsuario.DataBind();
                ddlIdUsuario.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }
        }
        private void FiltrarSectores(string NombreDelSector)
        {

            if (NombreDelSector == "--Todos--")
            {
                RadGridDetalleSalida.DataSource = _listaDetalleOrden;
                RadGridDetalleSalida.DataBind();
            }
            else
            {
                List<EDetalleSalidaArticuloSector> PruebaList = _listaDetalleOrden.FindAll(x => x.NombreSector == NombreDelSector);
                RadGridDetalleSalida.DataSource = PruebaList;
                RadGridDetalleSalida.DataBind();
            }
        }
        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {

                int idusuario;
                int idPrioridad;
                List<long> listaIdLineaDetalleSolicitud = new List<long>();


                idPrioridad = 1;
                idusuario = Convert.ToInt32(ddlIdUsuario.SelectedValue);
                for (int i = 0; i < RadGridDetalleSalida.Items.Count; i++)
                {
                    var item = RadGridDetalleSalida.Items[i];
                    var checkbox = item["CheckUsuario"].Controls[0] as CheckBox;
                    if (checkbox.Checked)
                    {

                        long idlineaDetallesolicitud = Convert.ToInt64(item["IdLineaDetalleSolicitud"].Text);
                        listaIdLineaDetalleSolicitud.Add(idlineaDetallesolicitud);

                        //int idlineaDetallesolicitud = Convert.ToInt32(item["IdLineaDetalleSolicitud"].Text);
                        //InsertarTareaAlistador(idlineaDetallesolicitud, idusuario, idPrioridad);

                        int index = _listaDetalleOrden.FindIndex(p => p.IdLineaDetalleSolicitud == idlineaDetallesolicitud);
                        if (index >= 0)
                        {
                            _listaDetalleOrden[index].DetalleAlistado = "Pendiente";
                        }
                    }
                }

                if (listaIdLineaDetalleSolicitud.Count > 0)
                {
                    FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                    OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);
                    string Resultado = oPESALMaestroSolicitud.InsertarTareasAlistador(listaIdLineaDetalleSolicitud, idusuario, idPrioridad);

                    // llamar para recargar grid
                    GetGetDetalleSalidaArticulosSector(_idBodega, _idMaestroSolicitud);
                    GetTareasPendientesPorUsuario(Convert.ToInt32(ddlIdUsuario.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }
        protected void btnRemoveTask_Click(object sender, EventArgs e)
        {
            try
            {
                int idlineaDetalle;
                int idTareaUsuario;

                int cantidadItems = RadGridTasks.Items.Count;
                for (int i = 0; i < cantidadItems; i++)
                {
                    var item = RadGridTasks.Items[i];
                    var checkbox = item["CheckTask"].Controls[0] as CheckBox;
                    if (checkbox != null && checkbox.Checked)
                    {
                        idTareaUsuario = Convert.ToInt32(item["IdTareaUsuario"].Text);
                        idlineaDetalle = Convert.ToInt32(item["IdLineaDetalle"].Text);
                        ActualizarTareaAlistador(idlineaDetalle, idTareaUsuario);
                        _listaTareasAlistador.RemoveAll(x => x.IdTareaUsuario == idTareaUsuario);

                        // listaDetalleOrden.Find(x=>x.IdLineaDetalleSolicitud== idlineaDetalle);
                        int index = _listaDetalleOrden.FindIndex(p => p.IdLineaDetalleSolicitud == idlineaDetalle);
                        if (index >= 0)
                        {
                            _listaDetalleOrden[index].DetalleAlistado = "Sin asignar";
                        }
                        //   listaDetalleOrden.Where(p => p.IdLineaDetalleSolicitud == idlineaDetalle).
                        //Select(u => { u.DetalleAlistado = "Sin asignar"; return u; }).ToList();

                    }

                }

                GetTareasPendientesPorUsuario(Convert.ToInt32(ddlIdUsuario.SelectedValue));
                GetGetDetalleSalidaArticulosSector(_idBodega, _idMaestroSolicitud);
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        private void GetGetDetalleSalidaArticulosSector(int idBodega, int idMaestroSalida)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);

                _listaDetalleOrden = oPESALMaestroSolicitud.GetDetalleSalidaArticulosSector(idBodega, idMaestroSalida);

                RadGridDetalleSalida.DataSource = _listaDetalleOrden;
                RadGridDetalleSalida.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        private void InsertarTareaAlistador(int idLineaDeatalla, int idUsuario, int idPrioridad)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);
                if (idLineaDeatalla > 0 && idUsuario > 0 && idPrioridad > 0)
                {
                    string Resultado = oPESALMaestroSolicitud.InsertarTareaAlistador(idLineaDeatalla, idUsuario, idPrioridad);
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        private void ActualizarTareaAlistador(int idLineaDeatalla, int idTareaUsuario)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);

                string Resultado = oPESALMaestroSolicitud.ActualizarTareaAlistador(idLineaDeatalla, idTareaUsuario);
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        private void GetTareasPendientesPorUsuario(int idUsuario)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);

                _listaTareasAlistador = oPESALMaestroSolicitud.GetTareasPendientesPorUsuario(idUsuario);
                RadGridTasks.DataSource = _listaTareasAlistador;
                RadGridTasks.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        protected void ddlIdUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTareasPendientesPorUsuario(Convert.ToInt32(ddlIdUsuario.SelectedValue));
        }

        protected void RadGridTasks_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (_listaTareasAlistador != null)
                {
                    RadGridTasks.DataSource = _listaTareasAlistador;
                    RadGridTasks.DataBind();
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();


            }
        }

        protected void ChkAsignado_OnCheckedChanged(object sender, EventArgs e)
        {
            List<EDetalleSalidaArticuloSector> listaFiltrada = new List<EDetalleSalidaArticuloSector>();
            try
            {
                if (_chkAsignado.Checked)
                    listaFiltrada = _listaDetalleOrden.FindAll(x => x.DetalleAlistado.Contains("Sin asignar"));
                else
                    listaFiltrada = _listaDetalleOrden;

                RadGridDetalleSalida.DataSource = listaFiltrada;
                RadGridDetalleSalida.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                //   cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        public void cargarSectores()
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWare = new NSectorWareHouse(fileExceptionWriter);

            _sectorsWarehouse = nSectorWare.GetSectorsWarehouse(_idBodega);
            ddSector.DataSource = _sectorsWarehouse;
            ddSector.DataTextField = "Name";
            ddSector.DataValueField = "IdSectorWarehouse";
            ddSector.DataBind();
            ddSector.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddSector.Items.Insert(1, new ListItem("--Todos--", "" + _sectorsWarehouse.Count));
        }
    }
}