using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using HtmlAgilityPack;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Utilidades.general;



namespace Diverscan.MJP.UI.FlujoDeTrabajo
{
    public partial class wf_Procesos : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 95;
        string Pagina = "";
        RadGridProperties radGridProperties = new RadGridProperties();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel_Unload1);
            this.Panel3.Unload += new EventHandler(UpdatePanel_Unload2);
            this.Panel5.Unload += new EventHandler(UpdatePanel_Unload3);
            this.Panel8.Unload += new EventHandler(UpdatePanel_Unload4);


            //this.UpdatePanelAccesosRoles.Unload += new EventHandler(UpdatePanel_Unload2);
        }
        void UpdatePanel_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        void UpdatePanel_Unload1(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel1(sender as UpdatePanel);
        }
        void UpdatePanel_Unload2(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
        }
        void UpdatePanel_Unload3(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel3(sender as UpdatePanel);
        }
        void UpdatePanel_Unload4(object sender, EventArgs e)
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
        public void RegisterUpdatePanel1(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                }
            }
        }
        public void RegisterUpdatePanel2(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel3 });
                }
            }
        }
        public void RegisterUpdatePanel3(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
                }
            }
        }
        public void RegisterUpdatePanel4(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel5 });
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            Pagina = Page.AppRelativeVirtualPath.ToString();
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
          

            if (!IsPostBack)
            {
                CargarDDLS();
                CargarFormularios();
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
        
        

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidProceso, e_TablasBaseDatos.TblProceso(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidActividad, e_TablasBaseDatos.TblActividades(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidAccion, e_TablasBaseDatos.TblAcciones(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidMetodoAccion, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddvidAccion0, e_TablasBaseDatos.TblAcciones(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidTipoParametro, e_TablasBaseDatos.TblTipoParametro(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlNombre, e_TablasBaseDatos.TblParametrosMetodo(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidParametroAccion, e_TablasBaseDatos.VistaDDLParametrosDestino(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidTipoMetodo, e_TablasBaseDatos.VistaDDLTiposMetodos(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidEvento, e_TablasBaseDatos.TblEventos(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlObjetoFuente, e_TablasBaseDatos.VistaObjetosFuente(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                foreach (string nombre in MD.ConsultaMetodos())
                {
                    ddlidMetodo.Items.Add(nombre);
                }
                ddlidMetodo.Items.Insert(0, new ListItem("--Seleccionar--"));


            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Error 11231231" + ex.Message, "");
            }
        }

        #region TabsControl


        private static void FindAllButtonControls(HtmlNodeCollection htmlNodeCollection, List<HtmlNode> controlNodes)
        {
            foreach (HtmlNode childNode in htmlNodeCollection)
            {
                if (childNode.Name.Equals("asp:button"))
                {
                    controlNodes.Add(childNode);
                }
                else
                {
                    FindAllButtonControls(childNode.ChildNodes, controlNodes);
                }
            }
        }

        public static List<HtmlNode> FindButtonControlsAtVirtualPath(String path)
        {
            if (path.StartsWith("~/HH/"))
            {
                path = "~" + path.Substring(4);
            }
            HtmlAgilityPack.HtmlDocument aspx = new HtmlAgilityPack.HtmlDocument();

            aspx.OptionFixNestedTags = true;
            aspx.Load(HttpContext.Current.Server.MapPath(path));

            List<HtmlNode> controlNodes = new List<HtmlNode>();
            FindAllButtonControls(aspx.DocumentNode.ChildNodes, controlNodes);

            return controlNodes;
        }

        private void CargarFormularios()
        {
            string sourceDirectory = Server.MapPath("~/");
            DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectory);
            var aspxFiles = Directory.EnumerateFiles(sourceDirectory, "*.aspx", SearchOption.AllDirectories).Select(Path.GetFullPath);
            string currentFiles = "";
            ddlFuente.DataTextField = "nombre";
            ddlFuente.DataValueField = "idFormulario";
            foreach (string currentFile in aspxFiles)
            {
                string relpath = @"~\" + currentFile.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
                this.ddlFuente.Items.Add(relpath.Replace(@"\", @"/"));
                relpath = @"~\HH\" + currentFile.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
                this.ddlFuente.Items.Add(relpath.Replace(@"\", @"/"));
                currentFiles = currentFile;
            }


            DataSet DSBaseDatos = new DataSet();
            string SQL = "SELECT name FROM Sys.Databases WHERE (database_id <> 1 AND database_id <> 2 AND database_id <> 3 AND database_id <> 4) ORDER BY name ASC";
            DSBaseDatos = n_ConsultaDummy.GetDataSet(SQL, "0");
            if (DSBaseDatos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                {
                    string name = dsRowEvento["name"].ToString();
                    this.ddlFuente.Items.Add(name);
                }
            }
        }

        private void CargarControles(string VitualPath, DropDownList DDL )
        {
            List<HtmlNode> controles = FindButtonControlsAtVirtualPath(VitualPath);
            DDL.Items.Clear();
            for (int i = 0; i < controles.Count; i++)
            {
                DDL.Items.Add(controles[i].Id);
            }
            DDL.DataTextField = "nombre";
            DDL.DataValueField = "idObjetoFuente";
            DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
        }

        private void CargarNombreTablas(string NombreBD, DropDownList DDL)
        {
            DDL.Items.Clear();
            DataSet DSTablas = new DataSet();
            string SQL = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='" + NombreBD + "' ORDER BY TABLE_SCHEMA, TABLE_NAME ASC";
            DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            if (DSTablas.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dsRowEvento in DSTablas.Tables[0].Rows)
                {
                    string TABLE_SCHEMA = dsRowEvento["TABLE_SCHEMA"].ToString();
                    string TABLE_NAME = dsRowEvento["TABLE_NAME"].ToString();
                    DDL.Items.Add(TABLE_SCHEMA + "." + TABLE_NAME);
                    //this.ddlFuente.Items.Add(TABLE_SCHEMA + "." + TABLE_NAME);
                }

                DDL.DataTextField = "nombre";
                DDL.DataValueField = "idObjetoFuente";
                DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
        }

        private void CargarParametrosMetodo(string Metodo)
        {
            ddlNombre.Items.Clear();
            n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
            foreach (string nombre in MD.ConsultaParametros(Metodo))
            {
                ddlNombre.Items.Add(nombre);
            }
            ddlNombre.Items.Insert(0, new ListItem("--Seleccionar--"));
            ddlidTipoParametro.SelectedIndex = ddlidTipoParametro.Items.IndexOf(ddlidTipoParametro.Items.FindByText("--Seleccionar--"));
            ddlidTipoParametro.Enabled = true;
        }

        private void CargarTipoParametrosMetodo(string Metodo, string ParametroName)
        {
            n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
            string TipoParametro = MD.ConsultaTipoParametro(Metodo, ParametroName);
            if (TipoParametro != "")
            {
                ddlidTipoParametro.SelectedIndex = ddlidTipoParametro.Items.IndexOf(ddlidTipoParametro.Items.FindByText(TipoParametro));
                ddlidTipoParametro.Enabled = false;
            }
        }

        private void CargarMetodoAccion(string idAccion)
        {
            string SQL = "";
            ddlidMetodoAccion.Items.Clear();
            DataSet DS = new DataSet();
            SQL = "select " + e_TblMetodoAccionFields.idMetodoAccion() + "," ;
            SQL += e_TblMetodoAccionFields.Nombre() +  " from " + e_TablasBaseDatos.TblMetodoAccion();
            SQL += " where " + e_TblMetodoAccionFields.idAccion() + " = '" + idAccion + "'";
            DS = n_ConsultaDummy.GetDataSet(SQL, "0");
            ddlidMetodoAccion.DataSource = DS.Tables[0];
            ddlidMetodoAccion.DataTextField = e_TblMetodoAccionFields.Nombre();
            ddlidMetodoAccion.DataValueField = e_TblMetodoAccionFields.idMetodoAccion();
            ddlidMetodoAccion.DataBind();
            ddlidMetodoAccion.Items.Insert(0, new ListItem("--Seleccionar--"));
        }

        protected void ddlidMetodoAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarParametrosMetodo(ddlidMetodoAccion.SelectedItem.Text);
        }

        protected void ddlNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTipoParametrosMetodo(ddlidMetodoAccion.SelectedItem.Text, ddlNombre.SelectedItem.Text);
        }

       
        protected void ddlFuente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFuente.SelectedItem.Text.StartsWith("~"))
            {
                CargarControles(ddlFuente.SelectedItem.Text, ddlObjetoFuente);
            }
            else
            {
                CargarNombreTablas(ddlFuente.SelectedItem.Text, ddlObjetoFuente);
            }
            
        }

        protected void ddlidAccion0_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMetodoAccion(ddvidAccion0.Text);
        }

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

        #region EventosFrontEnd

        /// <summary>
        /// Para que esto funcione el boton debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo boton
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void btnEditar5_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblParametrosMetodo(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar5_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblParametrosMetodo(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }
        protected void btnEditar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblMetodoAccion(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblAcciones(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblAcciones(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblActividades(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblActividades(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblProceso(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblProceso(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void RadGrid1_Prerender(object sender, EventArgs e)
        {
            radGridProperties.FormatearColumnas(RadGrid1);
        }


        protected void RadGrid2_Prerender(object sender, EventArgs e)
        {
            radGridProperties.FormatearColumnas(RadGrid2);
        }


        protected void RadGrid3_Prerender(object sender, EventArgs e)
        {
            radGridProperties.FormatearColumnas(RadGrid3);
        }


        protected void RadGrid4_Prerender(object sender, EventArgs e)
        {
            radGridProperties.FormatearColumnas(RadGrid4);
        }


        protected void RadGrid5_Prerender(object sender, EventArgs e)
        {
            radGridProperties.FormatearColumnas(RadGrid5);
        }


        #endregion //EventosFrontEnd

       #endregion //TabsControl

    }
}