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
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.LogicaWMS;
namespace Diverscan.MJP.UI.Mantenimiento.Despachos
{
    public partial class wf_Transportistas : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public static string TblTab1Mantenimiento = e_TablasBaseDatos.TblTipoDestino();
        public static string TblTab2Mantenimiento = e_TablasBaseDatos.TblCompania();
        public static string TblTab21Mantenimiento = e_TablasBaseDatos.TblDestino();
        public int ToleranciaAgregar = 110;
        RadGridProperties radGridProperties = new RadGridProperties();

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            

            if (!IsPostBack)
            {
                CargarDDLS();
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
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

        /// <summary>
        /// Evento llamado desde el grida para cargar los datos.
        /// Esta opcion se coloca cuando se crea el grid en ASPX --> OnNeedDataSource="RadGrid1_NeedDataSource"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidcolor, e_TablasBaseDatos.TblColores(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidTransportista, e_TablasBaseDatos.TblTransportista(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdTipoVehiculo, e_TablasBaseDatos.TblTiposVehiculo(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdMarcaVehiculo, e_TablasBaseDatos.TblMarcasVehiculos(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdCompania0, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000011" + ex.Message, "");
            }
        }


        /// <summary>
        /// Evento que se dispara cuando se hace click sobre el checkbox. El checbox se crea desde el inicio
        /// antes de asignarle el datasource al RadGrid. Tiene que estar dentro de le etiqueta columns, y
        /// el AutoPostBack debe ser True, de lo contrario no dispara el evento.
        ///     <ItemTemplate>
        ///         <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection"
        ///         AutoPostBack="True" />
        ///     </ItemTemplate>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000012" + ex.Message, "");
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
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-MAN-DES-000013" + ex.Message, "");
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

        #region EventosFrontEnd

        /// <summary>
        /// Para que esto funcione el boton debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo boton
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblVehiculos(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarVehiculos();
            CargarVehiculos("", true);
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblVehiculos(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarVehiculos();
            CargarVehiculos("", true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblTransportista(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarTransportistas();
            CargarTransportistas("", true);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblTransportista(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarTransportistas();
            CargarTransportistas("", true);
        }

        protected void Btnlimpiar1_Click(object sender, EventArgs e)
        {
            CargarDDLS();
            LimpiarTransportistas();
            txtSearchTransportistas.Text = "";
            CargarTransportistas("", true);
        }

        protected void Btnlimpiar2_Click(object sender, EventArgs e)
        {
            CargarDDLS();
            LimpiarVehiculos();
            txtSearchVehiculos.Text = "";
            CargarVehiculos("", true);
        }

        #endregion //EventosFrontEnd

        protected void ddlIdCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do nothing
        }

        #endregion //TabsControl

        #region Transportistas

        private void CargarTransportistas(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarTransportistas '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridTransportistas.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridTransportistas.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarTransportistas()
        {
            txtidTransportista.Text = "";
            ddlIdCompania.SelectedValue = "--Seleccionar--";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtComentarios.Text = "";

            btnAgregar.Visible = true;
            btnEditar.Visible = false;
        }

        protected void btnBuscarTransportistas_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarTransportistas();
                CargarTransportistas(txtSearchTransportistas.Text.ToString().Trim(), true);
                //txtSearchTransportistas.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridTransportistas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarTransportistas(txtSearchTransportistas.Text.ToString().Trim(), false);
                //CargarTransportistas("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridTransportistas_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidTransportista.Text = item["idTransportista"].Text.Replace("&nbsp;", "");
                    ddlIdCompania.SelectedValue = item["IdCompania"].Text.Replace("&nbsp;", "");
                    txtNombre.Text = item["Nombre"].Text.Replace("&nbsp;", "");
                    txtTelefono.Text = item["Telefono"].Text.Replace("&nbsp;", "");
                    txtCorreo.Text = item["Correo"].Text.Replace("&nbsp;", "");
                    txtComentarios.Text = item["Comentarios"].Text.Replace("&nbsp;", "");

                    btnAgregar.Visible = false;
                    btnEditar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Transportistas

        #region Vehiculos

        private void CargarVehiculos(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarVehiculos '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridVehiculos.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridVehiculos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarVehiculos()
        {
            txtidVehiculo.Text = "";
            ddlIdCompania0.SelectedValue = "--Seleccionar--";
            ddlidTransportista.SelectedValue = "--Seleccionar--";
            ddlIdTipoVehiculo.SelectedValue = "--Seleccionar--";
            ddlIdMarcaVehiculo.SelectedValue = "--Seleccionar--";
            ddlidcolor.SelectedValue = "--Seleccionar--";
            txtPlaca.Text = "";
            txtModelo.Text = "";
            txtCapacidadVolumen.Text = "";
            txtCapacidadPeso.Text = "";
            txtComentario.Text = "";

            btnAgregar2.Visible = true;
            btnEditar2.Visible = false;
        }

        protected void btnBuscarVehiculos_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarVehiculos();
                CargarVehiculos(txtSearchVehiculos.Text.ToString().Trim(), true);
                //txtSearchVehiculos.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridVehiculos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarVehiculos(txtSearchVehiculos.Text.ToString().Trim(), false);
                //CargarVehiculos("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridVehiculos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidVehiculo.Text = item["IdVehiculo"].Text.Replace("&nbsp;", "");
                    ddlIdCompania0.SelectedValue = item["idCompania"].Text.Replace("&nbsp;", "");
                    ddlidTransportista.SelectedValue = item["idTransportista"].Text.Replace("&nbsp;", "");
                    ddlIdTipoVehiculo.SelectedValue = item["idTipoVehiculo"].Text.Replace("&nbsp;", "");
                    ddlIdMarcaVehiculo.SelectedValue = item["idMarcaVehiculo"].Text.Replace("&nbsp;", "");
                    ddlidcolor.SelectedValue = item["idColor"].Text.Replace("&nbsp;", "");
                    txtPlaca.Text = item["Placa"].Text.Replace("&nbsp;", "");
                    txtModelo.Text = item["Modelo"].Text.Replace("&nbsp;", "");
                    txtCapacidadVolumen.Text = item["CapacidadVolumen"].Text.Replace("&nbsp;", "");
                    txtCapacidadPeso.Text = item["CapacidadPeso"].Text.Replace("&nbsp;", "");
                    txtComentario.Text = item["Comentario"].Text.Replace("&nbsp;", "");

                    btnAgregar2.Visible = false;
                    btnEditar2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Vehiculos
    }
}