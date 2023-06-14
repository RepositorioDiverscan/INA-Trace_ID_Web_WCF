using Diverscan.MJP.AccesoDatos.SolicitudDeOla;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.OPESALMaestroSolicitud;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.SolicitudDeOlas
{
    public partial class wf_SolicitudDeOlas : System.Web.UI.Page
    {
        #region Global Variables
        private readonly ISolicitudDeOla SolicitudDeOla;
        private e_Usuario UsrLogged = new e_Usuario();
        private int _idBodega
        {
            get
            {
                var data = ViewState["idBodega"];
                if (data == null)
                {
                    data = ((e_Usuario)Session["USUARIO"]).IdBodega;
                    ViewState["idBodega"] = data;
                }
                return Int32.Parse(data.ToString());
            }
            set { ViewState["idBodega"] = value; }
        }
        private int _idOla
        {
            get
            {
                var data = ViewState["idOla"];
                if (data == null)
                {
                    data = 0;
                    ViewState["idOla"] = data;
                }
                return Int32.Parse(data.ToString());
            }
            set { ViewState["idOla"] = value; }
        }
        private List<E_ListadoOlasCreadas> _listadoOlasActivas
        {
            get
            {
                var data = ViewState["OlasActivas"] as List<E_ListadoOlasCreadas>;
                if (data == null)
                {
                    data = new List<E_ListadoOlasCreadas>();
                    ViewState["OlasActivas"] = data;
                }
                return data;
            }
            set { ViewState["OlasActivas"] = value; }
        }
        private List<E_ListadoPreMaestro> _listaPreMaestroSolicitud
        {
            get
            {
                var data = ViewState["PreMaestrosSolicitudes"] as List<E_ListadoPreMaestro>;
                if (data == null)
                {
                    data = new List<E_ListadoPreMaestro>();
                    ViewState["PreMaestrosSolicitudes"] = data;
                }
                return data;
            }
            set { ViewState["PreMaestrosSolicitudes"] = value; }
        }
        public wf_SolicitudDeOlas()
        {
            SolicitudDeOla = new SolicituDeOlaDBA();
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
                FillControls();
            }
        }
        private void FillControls() 
        {
            ObtenerListadoPreMaestro();
            ObtenerListadoOlasPendientes();
            ObtenerListadoOlasActivas();
            //ObtieneRutas();
            SetDatetime();
            fillddPrioridad();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }
        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        private void fillddPrioridad()
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);
            List<EPrioridadOrden> listaPrioridadOrden = oPESALMaestroSolicitud.GetPrioridadOrden();
            ddPrioridad.DataSource = listaPrioridadOrden;
            ddPrioridad.DataTextField = "Descripcion";
            ddPrioridad.DataValueField = "IdPrioridad";
            ddPrioridad.DataBind();
            
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
        protected void ddlRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
        private void ObtieneRutas()
        {
        
        }
        private void ObtenerListadoPreMaestro()
        {
            try
            {
                _listaPreMaestroSolicitud = SolicitudDeOla.ObtenerListadoPreMaestro(_idBodega);
                RGPreMaestro.DataSource = _listaPreMaestroSolicitud;
                RGPreMaestro.DataBind();
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }
        }
        protected void btnEliminarPedido_Click(object sender, EventArgs e)
        {
            List<int> ListadoSolicitud = new List<int>();
            for (int i = 0; i < RGPreMaestro.Items.Count; i++)
            {
                var item = RGPreMaestro.Items[i];
                var checkbox = item["checkDetalle"].Controls[0] as CheckBox;

                if (checkbox != null && checkbox.Checked)
                {
                    int idSolicitud = Convert.ToInt32(item["idMaestroSolicitud"].Text.Replace("&nbsp;", ""));

                    ListadoSolicitud.Add(idSolicitud);
                }
            }

            if (ListadoSolicitud.Count <= 0)
            {
                Mensaje("error", "Debe Seleccionar un Pedido!!!", "");
                return;
            }

            SolicitudDeOla.EliminarListadoPreMaestro(ListadoSolicitud, _idBodega);
            ObtenerListadoPreMaestro();

        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
         
            try
            {
         
                DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
                DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
        
                List<E_ListadoPreMaestro> ListaPreMaestroSolicitud = SolicitudDeOla.ObtenerListadoPreMaestroFechas(fechaInicial, fechaFinal, _idBodega);  // ruta
                RGPreMaestro.DataSource = ListaPreMaestroSolicitud;
                RGPreMaestro.DataBind();
                ObtieneRutas();
                       // SetDatetime();

                   }
                   catch (Exception ex)
                   {
                       Mensaje("error", "Ocurrio el error " + ex.Message, "");
                   }
            }
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                int prioridad = Convert.ToInt32(ddPrioridad.SelectedValue);
                //if (prioridad == 0)
                //{
                //    Mensaje("error", "Debe Seleccionar una prioridad", "");
                //    return;
                //}
                List<int> ListadoSolicitud = new List<int>();
                for (int i = 0; i < RGPreMaestro.Items.Count; i++)
                {
                    var item = RGPreMaestro.Items[i];
                    var checkbox = item["checkDetalle"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        int idSolicitud = Convert.ToInt32(item["idMaestroSolicitud"].Text.Replace("&nbsp;", ""));

                        ListadoSolicitud.Add(idSolicitud);
                    }
                }
                                
                SolicitudDeOla.InsertarOlaCompleta(ListadoSolicitud, txtComentarios.Text, prioridad, _idBodega);
                ObtenerListadoPreMaestro();
                ObtenerListadoOlasPendientes();
                ObtenerListadoOlasActivas();
                PanelEliminaOla.Visible = false;
                Mensaje("ok", "La Ola fue creada exitosamente!", "");
              //  ObtieneRutas();
                SetDatetime();
                txtComentarios.Text = "";
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }

        }
        public void ObtenerListadoOlasPendientes()
        {
            try
            {
                _listadoOlasActivas = SolicitudDeOla.ObtenerListadoOlas(0, _idBodega);
                RGOlasPendientes.DataSource = _listadoOlasActivas;
                RGOlasPendientes.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }

        }
        public void ObtenerListadoOlasActivas()
        {
            try
            {
                var ListadoOlasActivas = SolicitudDeOla.ObtenerListadoOlas(1, _idBodega);
                _listadoOlasActivas = ListadoOlasActivas;
                RGOlasAprobadas.DataSource = _listadoOlasActivas;
                RGOlasAprobadas.DataBind();
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }

        }
        protected void btnAprobarOla_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> ListadoNumeroOlas = new List<int>();
                for (int i = 0; i < RGOlasPendientes.Items.Count; i++)
                {

                    var item = RGOlasPendientes.Items[i];

                    var checkbox = item["checkAprobacion"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        int idOla = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                        ListadoNumeroOlas.Add(idOla);
                    }
                }

                if (ListadoNumeroOlas.Count > 0)
                {
                    SolicitudDeOla.AprobarOla(ListadoNumeroOlas);
                    ObtenerListadoPreMaestro();
                    ObtenerListadoOlasPendientes();
                    ObtenerListadoOlasActivas();
                    PanelEliminaOla.Visible = false;
                    Mensaje("ok", "La Ola fue aprobada exitosamente!", "");
                }
                else
                {
                    Mensaje("info", "Debe seleccionar alguna Ola para aprobar", "");
                }

            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }

        }
        protected void RGOlasPendientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (e.CommandName == "btnAgregarAOla")
            {
                int idSolicitud = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));

                List<E_ListadoOlasCreadas> ListadoOlasPendienteSeleccionada = SolicitudDeOla.ObtenerListadoOlasPendientesSeleccionadas(idSolicitud, _idBodega);
                RGOlasPendientes.DataSource = ListadoOlasPendienteSeleccionada;
                RGOlasPendientes.DataBind();

                LabelMensaje.Visible = true;
                btnEditarOla.Visible = true;
                btnCancelarEdicion.Visible = true;
                btnAprobarOla.Visible = false;
                PanelObservacion.Visible = false;
                LabelSeparadorEdicion.Visible = true;

            }
            else if (e.CommandName == "btnEliminaOla")
            {
                int idOla = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                PanelEliminaOla.Visible = true;
                List<E_ListadoSolicitudesEliminarOla> ListadoOlasAEliminar = SolicitudDeOla.ObtenerListadoSolicitudesEliminar(idOla);
                RGEliminaSolicitudOlas.DataSource = ListadoOlasAEliminar;
                RGEliminaSolicitudOlas.DataBind();
            }
        }
        protected void btnEditarOla_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> ListadoSolicitud = new List<int>();
                int idSolicitud1 = 0;
                int idSolicitud2 = 0;
                for (int i = 0; i < RGPreMaestro.Items.Count; i++)
                {
                    var item = RGPreMaestro.Items[i];
                    var checkbox = item["checkDetalle"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        idSolicitud1 = Convert.ToInt32(item["idMaestroSolicitud"].Text.Replace("&nbsp;", ""));

                        ListadoSolicitud.Add(idSolicitud1);
                    }
                }

                for (int i = 0; i < RGOlasPendientes.Items.Count; i++)
                {
                    var item = RGOlasPendientes.Items[i];
                    var checkbox = item["checkAprobacion"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        idSolicitud2 = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                    }
                }
                if (ListadoSolicitud.Count == 0)
                {
                    Mensaje("info", "Debe seleccionar una o varias solicitudes, intente nuevamente", "");
                }
                else if (idSolicitud2 == 0)
                {
                    Mensaje("info", "Debe seleccionar una Ola, intente nuevamente", "");
                }
                else
                {
                    SolicitudDeOla.EditarOla(ListadoSolicitud, idSolicitud2);
                    Mensaje("ok", "Se proceso existosamente!", "");
                    LabelMensaje.Visible = false;
                    btnEditarOla.Visible = false;
                    btnAprobarOla.Visible = true;
                    PanelObservacion.Visible = true;
                    btnCancelarEdicion.Visible = false;
                    LabelSeparadorEdicion.Visible = false;

                    ObtenerListadoPreMaestro();
                    ObtenerListadoOlasPendientes();
                    ObtenerListadoOlasActivas();
                }

                //ListadoSolicitud
                //idSolicitud2
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }


        }
        protected void btnEliminarSolicitudOla_Click(object sender, EventArgs e)
        {
            try
            {

                List<int> ListadoSolicitudEliminar = new List<int>();
                int idRegistroOla = 0;
                int idSolicitud = 0;
                for (int i = 0; i < RGEliminaSolicitudOlas.Items.Count; i++)
                {
                    var item = RGEliminaSolicitudOlas.Items[i];
                    var checkbox = item["checkElimina"].Controls[0] as CheckBox;

                    if (checkbox != null && checkbox.Checked)
                    {
                        idRegistroOla = Convert.ToInt32(item["IdOla"].Text.Replace("&nbsp;", ""));
                        idSolicitud = Convert.ToInt32(item["idPreMaestroSolicitud"].Text.Replace("&nbsp;", ""));
                        ListadoSolicitudEliminar.Add(idSolicitud);
                    }
                }

                SolicitudDeOla.EliminarSolicitudOla(ListadoSolicitudEliminar, idRegistroOla);
                ObtenerListadoPreMaestro();
                ObtenerListadoOlasPendientes();
                ObtenerListadoOlasActivas();
                PanelEliminaOla.Visible = false;
                Mensaje("ok", "Se proceso existosamente!", "");
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }


        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            PanelEliminaOla.Visible = false;
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

            RDPFechaInicial.SelectedDate = datetime;
            RDPFechaFinal.SelectedDate = datetime;

        }
        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            PanelDetallesMaestro.Visible = false;
        }
        protected void RGPreMaestro_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnVerDetalle")
                {
                    PanelDetallesMaestro.Visible = true;
                    GridDataItem item = (GridDataItem)e.Item;
                    string idMaestro = item["idMaestroSolicitud"].Text.Replace("&nbsp;", "");

                    List<E_ListadoDetallesMaestro> ListaDetalle =
                        SolicitudDeOla.ObtenerListadoDetalleMaestro(Convert.ToInt32(idMaestro));
                    RGDetalleMaestro.DataSource = ListaDetalle;
                    RGDetalleMaestro.DataBind();
                }
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }
        }
        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LabelMensaje.Visible = false;
            btnEditarOla.Visible = false;
            btnAprobarOla.Visible = true;
            PanelObservacion.Visible = true;
            btnCancelarEdicion.Visible = false;
            LabelSeparadorEdicion.Visible = false;
        }
        protected void RGOlasAprobadas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RGOlasAprobadas.DataSource = _listadoOlasActivas;
        }
        protected void RGOlasAprobadas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    _idOla = Convert.ToInt32(item["idRegistroOla"].Text.Replace("&nbsp;", ""));
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ocurrio el error" + ex.Message, "");
            }
        }
        protected void RGOlasPendientes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (_listadoOlasActivas.Count > 0)
            {
                RGOlasPendientes.DataSource = _listadoOlasActivas;
            }
        }
        protected void RGPreMaestro_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //if (_listaPreMaestroSolicitud.Count > 0)
            //{
            RGPreMaestro.DataSource = _listaPreMaestroSolicitud;
            //}
        }
        protected void btnEliminarOla_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idOla <= 0 )
                {
                    Mensaje("error", "Debe seleccionar una Ola primero!", "");
                    return;
                }

                int response = SolicitudDeOla.RevertirOla(_idOla);

                switch (response)
                {
                    case -1:
                        Mensaje("error", "Ocurrio un error, contacte con soporte", "");
                        break;
                    case 0:
                        Mensaje("error", "La ola no puede ser eliminada, ya que tiene SSCC asociados", "");
                        break;
                    case 1:
                        Mensaje("error", "La ola no puede ser eliminada, ya que tiene tareas asignadas", "");
                        break;
                    case 2:
                        FillControls();
                        Mensaje("ok", "La ola fue eliminada corrrectamente", "");
                        break;
                    default:
                        Mensaje("error", "Ocurrio un error, contacte con soporte", "");
                        break;
                }       
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ocurrio el error " + ex.Message, "");
            }
        }
    }
}