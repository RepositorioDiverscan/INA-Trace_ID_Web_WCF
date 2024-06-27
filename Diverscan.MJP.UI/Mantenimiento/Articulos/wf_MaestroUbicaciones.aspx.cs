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
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.LogicaWMS;
using Control = System.Web.UI.Control;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_MaestroUbicaciones : System.Web.UI.Page
    {
        #region VARIABLES GLOBALES

        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 95;
        RadGridProperties radGridProperties = new RadGridProperties();
        DataTable dt;
        private bool isSuperAdmin;

        #endregion

        #region EVENTOS

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

                //Se valida si el rol del usuario es SuperAdmin
                if (!(UsrLogged.IdRoles.Equals("0")))
                {
                    RadPageView3.Selected = true;

                    RadTabStrip1.Visible = false;

                    ddlidBodega.SelectedValue = UsrLogged.IdBodega.ToString();
                    ddlidBodega.Enabled = false;

                    isSuperAdmin = false;
                }
                else
                {
                    isSuperAdmin = true;
                }

                FillDDBodega();
                enviaFront();
            }
        }

        #endregion

        #region Controles

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

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidAlmacen, e_TablasBaseDatos.TblAlmacenes(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidBodega, e_TablasBaseDatos.VistaBodegaCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidZona, e_TablasBaseDatos.TblZonas(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000009" + ex.Message, "");
            }
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            ddlidBodega.DataSource = ListBodegas;
            ddlidBodega.DataTextField = "Nombre";
            ddlidBodega.DataValueField = "IdBodega";
            ddlidBodega.DataBind();

            if (isSuperAdmin)
            {
                ddlidBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddlidBodega.Items[0].Attributes.Add("disabled", "disabled");
            }
        }

        private void ControlVersion()
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["esExpress"] == "Si")
                {

                }
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
            this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
            this.Panel5.Unload += new EventHandler(UpdatePanel4_Unload);
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
        }

        void UpdatePanel3_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel3(sender as UpdatePanel);
        }

        void UpdatePanel4_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel4(sender as UpdatePanel);
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

        public void RegisterUpdatePanel3(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel3 });
                }
            }
        }

        public void RegisterUpdatePanel4(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!(txtAbreviatura.Text == string.Empty || txtnombre.Text == string.Empty || txtdescripcion.Text == string.Empty))
            {
                System.Web.UI.Control Ctr = (System.Web.UI.Control)sender;
                var Panel = Ctr.Parent.Parent.Parent;
                string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblAlmacenes(), ToleranciaAgregar, UsrLogged.IdUsuario);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                CargarDDLS();
                LimpiarAlmacen();
                CargarAlmacen("", true);
            }
            else
            {
                Mensaje("error", "Debe de agregar la información solicitada.", "");
            }
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            if (!(txtAbreviatura0.Text == string.Empty || txtnombre0.Text == string.Empty || txtdescripcion0.Text == string.Empty))
            {
                Control Ctr = (Control)sender;
                var Panel = Ctr.Parent.Parent.Parent;
                string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblBodegas(), ToleranciaAgregar, UsrLogged.IdUsuario);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                CargarDDLS();
                LimpiarBodega();
                CargarBodega("", true);
            }
            else
            {
                Mensaje("error", "Debe de agregar la información solicitada.", "");
            }

        }
        ///Agregar una bueva ubicaciones
        protected void btnAgregar3_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSuperAdmin)
                {
                    if (ddlidBodega.SelectedIndex <= 0)
                    {
                        Mensaje("error", "Debe seleccionar la bodega.", "");
                        return;
                    }
                }

                if (ddlidZona.SelectedIndex <= 0)
                {
                    Mensaje("error", "Debe seleccionar la zona.", "");
                    return;
                }

                if (string.IsNullOrEmpty(txtpos.Text) | string.IsNullOrEmpty(txtcolumna.Text) | string.IsNullOrEmpty(txtestante.Text) | string.IsNullOrEmpty(txtnivel.Text) | string.IsNullOrEmpty(txtalto.Text) | string.IsNullOrEmpty(txtprofundidad.Text))
                {
                    Mensaje("error", "Debe ingresar todos los datos.", "");
                    return;
                }

                System.Web.UI.Control Ctr = (Control)sender;
                var Panel = Ctr.Parent.Parent.Parent;
                string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblMaestroUbicaciones(), ToleranciaAgregar, UsrLogged.IdUsuario);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                CargarDDLS();
                LimpiarUbicacion();
                FillDDBodega();
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                throw;
            }
        }

        protected void btnAgregar4_Click(object sender, EventArgs e)
        {
            if (!(txtAbreviatura00.Text == string.Empty || txtnombre00.Text == string.Empty || txtdescripcion00.Text == string.Empty))
            {
                System.Web.UI.Control Ctr = (System.Web.UI.Control)sender;
                var Panel = Ctr.Parent.Parent.Parent;
                string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblZonas(), ToleranciaAgregar, UsrLogged.IdUsuario);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                CargarDDLS();
                LimpiarZona();
                CargarZona("", true);
            }
            else
            {
                Mensaje("error", "Debe de agregar la información solicitada.", "");
            }
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblBodegas(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarBodega();
            CargarBodega("", true);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            System.Web.UI.Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblAlmacenes(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarAlmacen();
            CargarAlmacen("", true);
        }

        protected void btnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblMaestroUbicaciones(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            FillDDBodega();
            LimpiarUbicacion();
            CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
        }

        protected void btnEditar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblZonas(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarZona();
            CargarZona("", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void BtnlimpiarAlmacen_Click(object sender, EventArgs e)
        {
            LimpiarAlmacen();
            txtSearchAlmacen.Text = "";
            CargarAlmacen("", true);
        }

        protected void Btnlimpiar2_Click(object sender, EventArgs e)
        {
            LimpiarBodega();
            txtSearchBodega.Text = "";
            CargarBodega("", true);
        }

        protected void Btnlimpiar3_Click(object sender, EventArgs e)
        {
            LimpiarUbicacion();
            txtSearchUbicacion.Text = "";
            CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
        }

        protected void Btnlimpiar4_Click(object sender, EventArgs e)
        {
            LimpiarZona();
            txtSearchZona.Text = "";
            CargarZona("", true);
        }

        #endregion Controles

        #region TabsControl

        #region ControlRadGrid


        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
            //CargarGrid(Parent);
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
                    ((sender as System.Web.UI.WebControls.CheckBox).NamingContainer as Telerik.Web.UI.GridItem).Selected = (sender as System.Web.UI.WebControls.CheckBox).Checked;
                    bool checkHeader = true;
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        if (!(dataItem.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                        {
                            checkHeader = false;
                            break;
                        }
                    }
                    GridHeaderItem headerItem = RG.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.Header)[0] as GridHeaderItem;
                    (headerItem.FindControl("headerChkbox") as System.Web.UI.WebControls.CheckBox).Checked = checkHeader;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000010" + ex.Message, "");
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
                    System.Web.UI.WebControls.CheckBox headerCheckBox = (sender as System.Web.UI.WebControls.CheckBox);
                    foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                    {
                        (dataItem.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked = headerCheckBox.Checked;
                        dataItem.Selected = headerCheckBox.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000011" + ex.Message, "");
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
            System.Web.UI.WebControls.CheckBox cb = new System.Web.UI.WebControls.CheckBox();
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

        #region Almacen

        private void CargarAlmacen(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarAlmacen '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridAlmacen.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridAlmacen.DataBind();
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarAlmacen()
        {
            txtidAlmacen.Text = "";
            //ddlidCompania.SelectedValue = "--Seleccionar--";
            txtAbreviatura.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";

            btnAgregar.Visible = true;
            btnEditar.Visible = false;
        }

        protected void btnBuscarAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarAlmacen();
                CargarAlmacen(txtSearchAlmacen.Text.ToString().Trim(), true);
                //txtSearchAlmacen.Text = "";
                Mensaje("ok", "Se han encontrado registros", "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridAlmacen_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarAlmacen(txtSearchAlmacen.Text.ToString().Trim(), false);
                //CargarAlmacen("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridAlmacen_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidAlmacen.Text = item["idAlmacen"].Text.Replace("&nbsp;", "");
                    ddlidCompania.SelectedValue = item["idCompania"].Text.Replace("&nbsp;", "");
                    txtAbreviatura.Text = item["Abreviatura"].Text.Replace("&nbsp;", "");
                    txtnombre.Text = item["nombre"].Text.Replace("&nbsp;", "");
                    txtdescripcion.Text = item["descripcion"].Text.Replace("&nbsp;", "");

                    btnAgregar.Visible = false;
                    btnEditar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Almacen

        #region Bodega

        private void CargarBodega(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarBodega '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridBodega.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridBodega.DataBind();
                    Mensaje("ok", "Se han encontrado registros", "");
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarBodega()
        {
            txtidBodega.Text = "";
            //ddlidAlmacen.SelectedValue = "--Seleccionar--";
            txtAbreviatura0.Text = "";
            txtnombre0.Text = "";
            txtdescripcion0.Text = "";
            txtsecuencia.Text = "";

            btnAgregarBodega.Visible = true;
            btnEditarBodega.Visible = false;
        }

        protected void btnBuscarBodega_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarBodega();
                CargarBodega(txtSearchBodega.Text.ToString().Trim(), true);
                //txtSearchBodega.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarBodega(txtSearchBodega.Text.ToString().Trim(), false);
                //CargarBodega("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridBodega_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidBodega.Text = item["idBodega"].Text.Replace("&nbsp;", "");
                    ddlidAlmacen.SelectedValue = item["idAlmacen"].Text.Replace("&nbsp;", "");
                    //ddlidAlmacen.SelectedItem.Text = item["NombreAlmacen"].Text.Replace("&nbsp;", "");
                    txtAbreviatura0.Text = item["Abreviatura"].Text.Replace("&nbsp;", "");
                    txtnombre0.Text = item["nombre"].Text.Replace("&nbsp;", "");
                    txtdescripcion0.Text = item["descripcion"].Text.Replace("&nbsp;", "");
                    txtsecuencia.Text = item["secuencia"].Text.Replace("&nbsp;", "");

                    btnAgregarBodega.Visible = false;
                    btnEditarBodega.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Bodega

        #region Zona

        private void CargarZona(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";

                SQL = "EXEC SP_BuscarZona '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridZona.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridZona.DataBind();
                    Mensaje("ok", "Se han encontrado registros", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarZona()
        {
            txtidZona.Text = "";
            txtAbreviatura00.Text = "";
            txtnombre00.Text = "";
            txtdescripcion00.Text = "";
            txtsecuencia00.Text = "";
            btnAgregarZona.Visible = true;
            btnEditarZona.Visible = false;
        }

        protected void btnBuscarZona_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarZona();
                CargarZona(txtSearchZona.Text.ToString().Trim(), true);
                //txtSearchZona.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridZona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarZona(txtSearchZona.Text.ToString().Trim(), false);
                //CargarZona("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridZona_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidZona.Text = item["idZona"].Text.Replace("&nbsp;", "");
                    txtAbreviatura00.Text = item["Abreviatura"].Text.Replace("&nbsp;", "");
                    txtnombre00.Text = item["Nombre"].Text.Replace("&nbsp;", "");
                    txtdescripcion00.Text = item["Descripcion"].Text.Replace("&nbsp;", "");
                    txtsecuencia00.Text = item["secuencia"].Text.Replace("&nbsp;", "");

                    btnAgregarZona.Visible = false;
                    btnEditarZona.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Zona

        #region Ubicacion


        #region EVENTOS

        protected void btnBuscarUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
                //txtSearchUbicacion.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }


        protected void btn_exportarExcelOnClick(object sender, EventArgs e)
        {
            try
            {
                Mensaje("info", "Se ha generado el Documento Excel exitosamente!", "");
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), true);
                exportaExcel(txtSearchUbicacion.Text.ToString().Trim());
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }

        }

        #endregion

        #region METODOS

        private void CargarUbicacion(string buscar, bool pestana)
        {
            isSuperAdmin = UsrLogged.IdRoles.Equals("0") ? true : false;

            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                //Se valida si es SuperAdmin.
                if (isSuperAdmin)
                {
                    SQL = "EXEC SP_BuscarUbicacionXIdBodega '" + idCompania + "', '" + buscar + "', " + 1;
                }
                else
                {
                    SQL = "EXEC SP_BuscarUbicacionXIdBodega '" + idCompania + "', '" + buscar + "', " + UsrLogged.IdBodega;
                }

                //SQL = "EXEC SP_BuscarUbicacion '" + idCompania + "', '" + buscar + "'";  --> Antes el SP era SP_BuscarUbicacion porque no se necesitaba que filtraba las ubicaciones por bodega según el usuario.
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                dt = DSDatos.Tables[0];


                RadGridUbicacion.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridUbicacion.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarUbicacion()
        {
            txtidUbicacion.Text = "";

            if (isSuperAdmin) ddlidBodega.SelectedIndex = 0;

            //ddlidZona.SelectedValue = "--Seleccionar--";
            txtestante.Text = "";
            txtnivel.Text = "";
            txtcolumna.Text = "";
            txtpos.Text = "";
            txtlargo.Text = "";
            //txtareaAncho.Text = "";
            txtalto.Text = "";
            //txtcara.Text = "";
            txtprofundidad.Text = "";
            txtCapacidadPesoKilos.Text = "";
            txtCapacidadVolumenM3.Text = "";
            txtSecuencia000.Text = "";
            txtdescripcion.Text = "";
            btnAgregarUbicacion.Visible = true;
            btnEditarUbicacion.Visible = false;
            txtdescripcion000.Text = "";

            if (!(isSuperAdmin))
            {
                ddlidBodega.SelectedValue = UsrLogged.IdBodega.ToString();
            }
        }

        protected void RadGridUbicacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarUbicacion(txtSearchUbicacion.Text.ToString().Trim(), false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUbicacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidUbicacion.Text = item["idUbicacion"].Text.Replace("&nbsp;", "");
                    ddlidBodega.SelectedValue = item["idBodega"].Text.Replace("&nbsp;", "");
                    ddlidZona.SelectedValue = item["idZona"].Text.Replace("&nbsp;", "");
                    txtestante.Text = item["estante"].Text.Replace("&nbsp;", "");
                    txtnivel.Text = item["nivel"].Text.Replace("&nbsp;", "");
                    txtcolumna.Text = item["columna"].Text.Replace("&nbsp;", "");
                    txtpos.Text = item["pos"].Text.Replace("&nbsp;", "");
                    txtlargo.Text = item["largo"].Text.Replace("&nbsp;", "");
                    //txtareaAncho.Text = item["areaAncho"].Text.Replace("&nbsp;", "");
                    txtalto.Text = item["alto"].Text.Replace("&nbsp;", "");
                    //txtcara.Text = item["cara"].Text.Replace("&nbsp;", "");
                    txtprofundidad.Text = item["profundidad"].Text.Replace("&nbsp;", "");
                    txtCapacidadPesoKilos.Text = item["CapacidadPesoKilos"].Text.Replace("&nbsp;", "");
                    txtCapacidadVolumenM3.Text = item["CapacidadVolumenM3"].Text.Replace("&nbsp;", "");
                    txtdescripcion000.Text = item["Descripcion"].Text.Replace("&nbsp;", "");
                    txtSecuencia000.Text = item["Secuencia"].Text.Replace("&nbsp;", "");

                    btnAgregarUbicacion.Visible = false;
                    btnEditarUbicacion.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        public string exportaExcel(string buscar)
        {
            try
            {
                CargarUbicacion(buscar, true);

                SLDocument docExcel = new SLDocument();

                string nombreBodega = dt.Rows[1][2].ToString();

                //Se importa el DataTable al documento de Excel sl
                docExcel.ImportDataTable(1, 1, dt, true);

                //Agregamos estilos
                SLStyle style = docExcel.CreateStyle();
                style.Alignment.ReadingOrder = SLAlignmentReadingOrderValues.RightToLeft;
                style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.Blue);

                //Escondemos las celdas
                docExcel.HideColumn(1);
                docExcel.HideColumn(2);
                docExcel.HideColumn(10);
                docExcel.HideColumn(11);
                docExcel.HideColumn(12);
                docExcel.HideColumn(13);
                docExcel.HideColumn(14);
                docExcel.HideColumn(15);
                docExcel.HideColumn(16);
                docExcel.HideColumn(18);
                //Agrandamos el tamaño de la celda
                docExcel.SetColumnWidth(3, 20);
                docExcel.SetColumnWidth(5, 15);
                docExcel.SetColumnWidth(17, 24);
                //Le asignamos el estilo
                docExcel.SetRowStyle(1, style);

                // Guardamos el archivo en un MemoryStream
                using (var memoryStream = new MemoryStream())
                {
                    docExcel.SaveAs(memoryStream);

                    // Convertimos el archivo a base 64
                    var bytes = memoryStream.ToArray();
                    var base64 = Convert.ToBase64String(bytes);

                    return base64;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void enviaFront()
        {

            string buscar = Request.Form["Buscar"];

            switch (Request.Form["Metodo"])
            {
                case "Buscar":
                    Response.Clear();
                    Response.Write(exportaExcel(buscar));
                    Response.End();
                    break;
                default:
                    break;
            }

        }

        #endregion


        #endregion Ubicacion


        protected void TxtMinValid_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text.Length < 3)
            {
                Mensaje("error", "Debe ingresar tres dígitos.", "");
                txt.Text = "";
            }
            else
            {
                TxtDescripText();
            }
        }

        private void TxtDescripText()
        {
            da_MaestroArticulo Da_aestroArticulo = new da_MaestroArticulo();
            string ValueBodeg, ValueZona;
            if (!ddlidBodega.SelectedValue.Contains("Selec"))
            {
                ValueBodeg = Da_aestroArticulo.GETABRBOD(Convert.ToInt32(ddlidBodega.SelectedValue));
            }
            else
            {
                ValueBodeg = "";
            }

            if (!ddlidZona.SelectedValue.Contains("Selec"))
            {
                ValueZona = Da_aestroArticulo.GETABRZONE(Convert.ToInt32(ddlidZona.SelectedValue));
            }
            else
            {
                ValueZona = "";
            }

            txtdescripcion000.Text = ValueBodeg + "-" + ValueZona + "-" + txtestante.Text + "-" + txtcolumna.Text + "-" + txtnivel.Text + "-" + txtpos.Text;
        }

        protected void Dd_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtDescripText();

        }

    }
}