using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Negocio.Usuarios;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.LogicaWMS;
using System.Web.Services;
using Diverscan.MJP.AccesoDatos.Usuarios;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.Negocio.SectorWarehouse;

namespace Diverscan.MJP.UI.Administracion.Seguridad
{
    public partial class wf_SeguridadUsuarios : System.Web.UI.Page
    {
        static List<EBodega> ListBodegas = new List<EBodega>();
        static List<ESectorWarehouse> _listaSectores = new List<ESectorWarehouse>();
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        private bool isSuperAdmin;
        RadGridProperties radGridProperties = new RadGridProperties();

        private DataSet _dsUsuarios
        {
            get
            {
                var data = ViewState["dsUsuarios"] as DataSet;
                if (data == null)
                {
                    data = new DataSet();
                    ViewState["dsUsuarios"] = data;
                }
                return data;
            }
            set
            {
                ViewState["dsUsuarios"] = value;
            }
        }

        private List<Diverscan.MJP.Entidades.Usuarios.e_Usuarios> ListUsuarios
        {
            get
            {
                var data = ViewState["ListUsuarios"] as List<Diverscan.MJP.Entidades.Usuarios.e_Usuarios>;
                if (data == null)
                {
                    data = new List<Diverscan.MJP.Entidades.Usuarios.e_Usuarios>();
                    ViewState["ListUsuarios"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListUsuarios"] = value;
            }
        }

        private List<Diverscan.MJP.Entidades.Rol.e_Rol> ListRoles
        {
            get
            {
                var data = ViewState["ListRoles"] as List<Diverscan.MJP.Entidades.Rol.e_Rol>;
                if (data == null)
                {
                    data = new List<Diverscan.MJP.Entidades.Rol.e_Rol>();
                    ViewState["ListRoles"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListRoles"] = value;
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
                if (!(UsrLogged.IdRoles.Equals("0")))
                {
                    RadTabStrip1.Visible = false;

                    ddBodega.SelectedValue = UsrLogged.IdBodega.ToString();
                    ddBodega.Enabled = false;

                    //Se carga el ddl de zonas con la bodega del usuario logueado.
                    FillDDSectores(UsrLogged.IdBodega);

                    isSuperAdmin = false;
                }
                else
                {
                    isSuperAdmin = true;
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                CargarDDLS();
                FillDDBodega();
                CargarUsuarios("", true, UsrLogged.IdRoles);
                //CargarFormularios();
            }
        }

        public static string[] GetUsers(string prefix)
        {
            n_Usuario n_usuario = new n_Usuario();
            return n_usuario.GetUsers(prefix).ToArray();
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidRol, e_TablasBaseDatos.VistaRolesSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidRol0, e_TablasBaseDatos.VistaRolesSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidRol00, e_TablasBaseDatos.VistaRolesSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlIdCompania0, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SEG-000001" + ex.Message, "");
            }
        }

        private void Mensaje(string sTipo, string sMensaje, string sLlenado)
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (!(isSuperAdmin))
            {
                ddSector.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }

            try
            {
                var MD5 = System.Security.Cryptography.MD5.Create();
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string Resultado = "";
                string idUsuario = txtIDUSUARIO.Text.ToString().Trim();
                string idCompania = ddlIdCompania.Text.ToString().Trim();
                string Usuario = txtUsuario.Text.ToString().Trim();
                string Contrasenna = txtCONTRASENNA.Text.ToString().Trim();
                string RepitaContrasenna = txtRepitacontraseña.Text.ToString().Trim();
                string idRol = ddlidRol.Text.ToString().Trim();
                string email = txtEMAIL.Text.ToString().Trim();
                string comentario = txtCOMENTARIO.Text.ToString().Trim();
                bool esta_bloqueado = chkESTA_BLOQUEADO.Checked;
                string nombre = txtNOMBRE_PILA.Text.ToString().Trim();
                string apellidos = txtAPELLIDOS_PILA.Text.ToString().Trim();

                int IdBodega = 0;
                if (ddBodega.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe seleccionar una bodega!!!", "");
                    return;
                }
                else
                    IdBodega = Convert.ToInt32(ddBodega.SelectedValue);
                

                int idSector = 0;
                if (ddSector.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe seleccionar una sector!!!", "");
                    return;
                }
                else
                    idSector = Convert.ToInt32(ddSector.SelectedValue);

                if ((Contrasenna != RepitaContrasenna))
                {
                    Resultado = "Contraseña no es correcta";
                    return;
                }

                else
                {
                    if (ValidarCamposUsuario() && !string.IsNullOrEmpty(Usuario))
                    {
                        if (!string.IsNullOrEmpty(Contrasenna))
                        {
                            Contrasenna = clHash.GetMd5Hash(MD5, Contrasenna);   // encripta el password ingresado.
                        }

                        SQL = "EXEC SP_EditarUsuario '" + idUsuario + "', '" + idCompania + "', '" + Usuario + "', '" +
                            Contrasenna + "', '" + idRol + "', '" + email + "', '" + comentario + "', '" + esta_bloqueado + "', '" +
                            nombre + "', '" + apellidos + "'" + ", '" + IdBodega + "'" + ", '" + idSector + "'";
                        Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);

                        if (Resultado == "Ok")
                        {
                            Resultado = "Usuario editado correctamente";
                            CargarDDLS();
                            LimpiarUsuarios();
                            CargarUsuarios("", true, UsrLogged.IdRoles);
                        }
                        else if (Resultado == "Email")
                        {
                            Resultado = "El correo ya esta registrado";
                        }
                        else if (Resultado == "Usuario")
                        {
                            Resultado = "Usuario de Diverscan, no se actualizara la información.";
                        }
                        else if (Resultado == "Diverscan")
                        {
                            Resultado = "Usuario de Diverscan, no se actualizara la contraseña.";
                        }
                        else
                        {
                            Resultado = "Error al editar el usuario";
                        }
                    }
                    else
                    {
                        Resultado = "Hay campos requeridos en blanco";
                    }

                }

                Mensaje("info", Resultado, "");

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var MD5 = System.Security.Cryptography.MD5.Create();
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string Resultado = "";
                string idCompania = ddlIdCompania.Text.ToString().Trim();
                string Usuario = txtUsuario.Text.ToString().Trim();
                string Contrasenna = txtCONTRASENNA.Text.ToString().Trim();
                string RepitaContrasenna = txtRepitacontraseña.Text.ToString().Trim();
                string idRol = ddlidRol.Text.ToString().Trim();
                string email = txtEMAIL.Text.ToString().Trim();
                string comentario = txtCOMENTARIO.Text.ToString().Trim();
                bool esta_bloqueado = chkESTA_BLOQUEADO.Checked;
                string nombre = txtNOMBRE_PILA.Text.ToString().Trim();
                string apellidos = txtAPELLIDOS_PILA.Text.ToString().Trim();
                int IdBodega = Convert.ToInt32(ddBodega.SelectedValue.ToString());
                int idSector = Convert.ToInt32(ddSector.SelectedValue.ToString());

                if ((Contrasenna != RepitaContrasenna) || (Contrasenna == ""))
                {
                    Resultado = "Contraseña no es correcta";
                }
                else
                {
                    if (ValidarCamposUsuario())
                    {
                        Contrasenna = clHash.GetMd5Hash(MD5, Contrasenna);   // encripta el password ingresado.

                        SQL = "EXEC SP_InsertarUsuario '" + idCompania + "', '" + Usuario + "', '" + Contrasenna + "', '" +
                        idRol + "', '" + email + "', '" + comentario + "', '" + esta_bloqueado + "', '" + nombre + "', '" +
                        apellidos + "'" + ", '" + IdBodega + "'" + ", '" + idSector + "'";

                        Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);

                        if (Resultado == "Ok")
                        {
                            Resultado = "Usuario insertado correctamente";
                            CargarDDLS();
                            LimpiarUsuarios();
                            CargarUsuarios("", true, UsrLogged.IdRoles);
                        }
                        else if (Resultado == "Email")
                        {
                            Resultado = "El correo ya esta registrado";
                        }
                        else if (Resultado == "Usuario")
                        {
                            Resultado = "El usuario ya esta registrado";
                        }
                        else
                        {
                            Resultado = "Error al registrar el usuario";
                        }
                    }
                    else
                    {
                        Resultado = "Hay campos requeridos en blanco";
                    }

                }

                Mensaje("info", Resultado, "");

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private bool ValidarCamposUsuario()
        {
            bool resultado = true;

            if (ddlIdCompania.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtUsuario.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtNOMBRE_PILA.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtAPELLIDOS_PILA.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtEMAIL.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (ddlidRol.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }

            return resultado;
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TBLAccesosporRol(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TBLAccesosporRol(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
        }

        protected void btnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblRoles(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarRol();
            CargarRoles2();
        }

        protected void btnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblRoles(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            CargarDDLS();
            LimpiarRol();
            CargarRoles2();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //n_WMS wms = new n_WMS();

            //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

            //var usuarios = n_Usuario.GetListUsuarios(txtSearch.Text, idCompania);
            //ListUsuarios = usuarios;

            //RadGrid1.DataSource = ListUsuarios;
            //RadGrid1.DataBind();

            CargarUsuarios(txtSearch.Text.ToString().Trim(), false, UsrLogged.IdRoles);
        }

        private void LimpiarRol()
        {
            txtidRol.Text = "";
            txtNombre.Text = "";
            txtDescripcion0.Text = "";
            //ddlIdCompania0.SelectedValue = "--Seleccionar--";

            Button3.Visible = true;
            Button4.Visible = false;
        }

        public void Accion(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            Mensaje("ok", resultado, "");
        }

        protected void Btnlimpiar1_Click(object sender, EventArgs e)
        {
            LimpiarUsuarios();
            txtSearch.Text = "";
            CargarUsuarios("", true, UsrLogged.IdRoles);
        }

        protected void Btnlimpiar2_Click(object sender, EventArgs e)
        {
            // botón limpiar, ubicación. 
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void Btnlimpiar3_Click(object sender, EventArgs e)
        {
            CargarDDLS();
            LimpiarRol();
            CargarRoles2();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
            this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
            this.Panel5.Unload += new EventHandler(UpdatePanel5_Unload);
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

        void UpdatePanel5_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel5(sender as UpdatePanel);
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

        public void RegisterUpdatePanel5(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel5 });
                }
            }
        }

        #region TabsControl

        #region ControlRadGrid

        protected void RadGridRoles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CargarRoles();
        }

        public void CargarRoles()
        {
            n_WMS wms = new n_WMS();

            string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
            var roles = Diverscan.MJP.Negocio.Roles.n_Roles.GetRoles(idCompania);
            ListRoles = roles;
            RadGridRoles.DataSource = ListRoles;
            //RadGridRoles.DataBind();
        }

        private void CargarRoles2()
        {
            n_WMS wms = new n_WMS();

            string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

            var roles = Diverscan.MJP.Negocio.Roles.n_Roles.GetListRoles("", idCompania);
            ListRoles = roles;

            RadGridRoles.DataSource = ListRoles;
            RadGridRoles.DataBind();
        }

        protected void RadGridRoles_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                txtidRol.Text = item["IdRol"].Text.Replace("&nbsp;", "");
                txtNombre.Text = item["Nombre"].Text.Replace("&nbsp;", "");
                txtDescripcion0.Text = item["Descripcion"].Text.Replace("&nbsp;", "");
                ddlIdCompania0.SelectedValue = item["IdCompania"].Text.Replace("&nbsp;", "");

                Button3.Visible = false;
                Button4.Visible = true;
            }
        }

        protected void btnBuscarRoles_Click(object sender, EventArgs e)
        {
            n_WMS wms = new n_WMS();

            string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

            var roles = Diverscan.MJP.Negocio.Roles.n_Roles.GetListRoles(txtBuscarRoles.Text, idCompania);
            ListRoles = roles;

            RadGridRoles.DataSource = ListRoles;
            RadGridRoles.DataBind();
        }

        protected void RadGrid2_Prerender(object sender, EventArgs e)
        {
            //radGridProperties.FormatearColumnas(RadGrid2);
        }

        protected void RadGrid3_Prerender(object sender, EventArgs e)
        {
            //radGridProperties.FormatearColumnas(RadGrid3);
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SEG-000002" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SEG-000003" + ex.Message, "");
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

        #endregion //ControlRadGrid

        #region EventosFrontEnd

        /// <summary>
        /// Para que esto funcione el boton debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo boton
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        #endregion //EventosFrontEnd

        protected void ddlIdCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do nothing
        }

        #endregion //TabsControl

        //private void CargarFormularios()
        //{
        //    var cl = new clErrores();
        //   // carga los WEB form definidos en la aplicación
        //    string sourceDirectory = Server.MapPath("~/");
        //    DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectory);
        //    var aspxFiles = Directory.EnumerateFiles(sourceDirectory, "*.aspx", SearchOption.AllDirectories).Select(Path.GetFullPath);
        //    string currentFiles = "";
        //    ddlAspx.DataTextField = "nombre";
        //    ddlAspx.DataValueField = "idFormulario";
        //    foreach (string currentFile in aspxFiles)
        //    {
        //        string relpath = @"~\" + currentFile.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        //        relpath = relpath.Replace(@"\", @"/");

        //        if (!relpath.StartsWith("~/obj/"))
        //        {
        //            this.ddlAspx.Items.Add(relpath);
        //            currentFiles = currentFile;
        //        }
        //    }

        //    ddlAspx.Items.Insert(0, new ListItem("--Seleccionar--"));

        //   // carga los form que se usan en la HH
        //    DataSet formhh = new DataSet();
        //    string SQL = "";

        //    SQL = "SELECT idprogramas FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLProgramasHH ();
        //    formhh = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

        //    if (formhh.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow filas in formhh.Tables[0].Rows)
        //        {
        //            this.ddlFormhh.Items.Add(filas["idprogramas"].ToString()); 
        //        }

        //        ddlFormhh.Items.Insert(0, new ListItem("--Seleccionar--"));
        //    }

        //}

        protected void chkCambiarContrasenna_CheckedChanged(object sender, EventArgs e)
        {
            chkCambiarContrasenna = (CheckBox)sender;
            if (chkCambiarContrasenna.Checked)
            {
                Label7.Visible = true;
                txtCONTRASENNA.Visible = true;
                Label5.Visible = true;
                txtRepitacontraseña.Visible = true;
            }
            else
            {
                txtCONTRASENNA.Text = "";
                txtRepitacontraseña.Text = "";

                Label7.Visible = false;
                txtCONTRASENNA.Visible = false;
                Label5.Visible = false;
                txtRepitacontraseña.Visible = false;
            }
        }

        #region Usuarios

        private void CargarUsuarios(string buscar, bool pestana, string idRol)
        {
            try
            {
                n_WMS wms = new n_WMS();

                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                //Se valida si es SuperAdmin.
                if (isSuperAdmin)
                {
                    SQL = "EXEC SP_BuscarUsuarioXIdBodega '" + idCompania + "', '" + buscar + "', " + 1;
                }
                else
                {
                    SQL = "EXEC SP_BuscarUsuarioXIdBodega '" + idCompania + "', '" + buscar + "', " + UsrLogged.IdBodega;
                }

                //SQL = "EXEC SP_BuscarUsuario '" + idCompania + "', '" + buscar + "'";
                _dsUsuarios = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridUsuarios.DataSource = _dsUsuarios;
                //if (pestana)
                //{
                RadGridUsuarios.DataBind();
                //}
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarUsuarios()
        {
            txtIDUSUARIO.Text = "";
            //ddlIdCompania.SelectedValue = "--Seleccionar--";
            txtUsuario.Text = "";
            txtNOMBRE_PILA.Text = "";
            txtAPELLIDOS_PILA.Text = "";
            txtEMAIL.Text = "";
            //ddlidRol.SelectedValue = "--Seleccionar--";
            txtCONTRASENNA.Text = "";
            txtRepitacontraseña.Text = "";
            txtCOMENTARIO.Text = "";
            chkESTA_BLOQUEADO.Checked = false;

            btnAgregar.Visible = true;
            btnEditar.Visible = false;

            chkCambiarContrasenna.Checked = false;
            chkCambiarContrasenna.Visible = false;

            Label7.Visible = true;
            txtCONTRASENNA.Visible = true;
            Label5.Visible = true;
            txtRepitacontraseña.Visible = true;

            if (isSuperAdmin)
            {
                ddBodega.SelectedIndex = 0;
                ddSector.Items.Clear();
            }
            else
            {
                FillDDSectores(UsrLogged.IdBodega);
            }
        }

        protected void btnBuscarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarUsuarios();
                CargarUsuarios(txtSearch.Text.ToString().Trim(), true, UsrLogged.IdRoles);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (_dsUsuarios.Tables.Count > 0 && _dsUsuarios.Tables[0].Rows.Count > 0)
                    RadGridUsuarios.DataSource = _dsUsuarios;
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridUsuarios_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string ESTA_BLOQUEADO = item["ESTA_BLOQUEADO"].Text.Replace("&nbsp;", "");

                    bool bloqueado = false;

                    if (ESTA_BLOQUEADO.Equals("True"))
                    {
                        bloqueado = true;
                    }

                    txtIDUSUARIO.Text = item["IDUSUARIO"].Text.Replace("&nbsp;", "");
                    ddlIdCompania.SelectedIndex = 0;
                    txtUsuario.Text = item["Usuario"].Text.Replace("&nbsp;", "");
                    txtNOMBRE_PILA.Text = item["NOMBRE_PILA"].Text.Replace("&nbsp;", "");
                    txtAPELLIDOS_PILA.Text = item["APELLIDOS_PILA"].Text.Replace("&nbsp;", "");
                    txtEMAIL.Text = item["EMAIL"].Text.Replace("&nbsp;", "");
                    ddlidRol.SelectedValue = item["IdRol"].Text.Replace("&nbsp;", "");
                    txtCONTRASENNA.Text = "";
                    txtRepitacontraseña.Text = "";
                    txtCOMENTARIO.Text = item["COMENTARIO"].Text.Replace("&nbsp;", "");
                    chkESTA_BLOQUEADO.Checked = bloqueado; //ESTA_BLOQUEADO          
                    int IdBod = ListBodegas.Find(x => x.Nombre == item["NOMBRE_BODEGA"].Text.Replace("&nbsp;", "")).IdBodega;
                    ddBodega.SelectedValue = IdBod.ToString();

                    btnAgregar.Visible = false;
                    btnEditar.Visible = true;

                    chkCambiarContrasenna.Checked = false;
                    chkCambiarContrasenna.Visible = true;

                    txtCONTRASENNA.Text = "";
                    txtRepitacontraseña.Text = "";

                    Label7.Visible = false;
                    txtCONTRASENNA.Visible = false;
                    Label5.Visible = false;
                    txtRepitacontraseña.Visible = false;

                    string nameSector = item["NOMBRE_SECTOR"].Text.Replace("&nbsp;", "");
                    FillDDSectores(IdBod);
                    ddSector.SelectedValue = "" + _listaSectores.Find(x => x.Name == nameSector).IdSectorWarehouse;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }


        #endregion Usuarios

        protected void ddlidRol0_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idRol = ddlidRol0.SelectedValue.ToString();
            CargarFormulariosAsignados(true);
            CargarFormulariosSinAsignar(true);
        }

        protected void ddlidRol00_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idRol = ddlidRol00.SelectedValue.ToString();
            CargarFormulariosAsignadosHH(true);
            CargarFormulariosSinAsignarHH(true);
        }

        #region Formularios Asignados WEB

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idRol = ddlidRol0.SelectedValue.ToString();
                string idFormulario = lblIdFormularioSinAsignar.Text;

                SQL = "EXEC SP_InsertarFormulariosAsignados '" + idRol + "', '" + idFormulario + "'";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);

                CargarFormulariosAsignados(true);
                CargarFormulariosSinAsignar(true);
                lblIdFormularioAsignado.Text = "";
                lblIdFormularioSinAsignar.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void CargarFormulariosAsignados(bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                string idRol = ddlidRol0.SelectedValue.ToString();
                bool esWeb = true;

                SQL = "EXEC SP_ObtenerFormulariosAsignados '" + idRol + "', '" + esWeb + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridFormulariosAsignados.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridFormulariosAsignados.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosAsignados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarFormulariosAsignados(false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosAsignados_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string idFormulario = item["idFormulario"].Text.ToString().Trim();

                    lblIdFormularioAsignado.Text = idFormulario;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Formularios Asignados WEB

        #region Formularios Sin Asignar WEB

        protected void btnSinAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idRol = ddlidRol0.SelectedValue.ToString();
                string idFormulario = lblIdFormularioAsignado.Text;

                SQL = "EXEC SP_EliminarFormulariosAsignados '" + idRol + "', '" + idFormulario + "'";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);

                CargarFormulariosAsignados(true);
                CargarFormulariosSinAsignar(true);
                lblIdFormularioAsignado.Text = "";
                lblIdFormularioSinAsignar.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void CargarFormulariosSinAsignar(bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                string idRol = ddlidRol0.SelectedValue.ToString();
                bool esWeb = true;

                SQL = "EXEC SP_ObtenerFormulariosSinAsignar '" + idRol + "', '" + esWeb + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridFormulariosSinAsignar.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridFormulariosSinAsignar.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosSinAsignar_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarFormulariosSinAsignar(false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosSinAsignar_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string idFormulario = item["idFormulario"].Text.ToString().Trim();

                    lblIdFormularioSinAsignar.Text = idFormulario;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Formularios Sin Asignar WEB

        #region Formularios Asignados HH

        protected void btnAsignarHH_Click(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idRol = ddlidRol00.SelectedValue.ToString();
                string idFormulario = lblIdFormularioSinAsignarHH.Text;

                SQL = "EXEC SP_InsertarFormulariosAsignados '" + idRol + "', '" + idFormulario + "'";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);

                CargarFormulariosAsignadosHH(true);
                CargarFormulariosSinAsignarHH(true);
                lblIdFormularioAsignadoHH.Text = "";
                lblIdFormularioSinAsignarHH.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void CargarFormulariosAsignadosHH(bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                string idRol = ddlidRol00.SelectedValue.ToString();
                bool esWeb = false;

                SQL = "EXEC SP_ObtenerFormulariosAsignados '" + idRol + "', '" + esWeb + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridFormulariosAsignadosHH.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridFormulariosAsignadosHH.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosAsignadosHH_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarFormulariosAsignadosHH(false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosAsignadosHH_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string idFormulario = item["idFormulario"].Text.ToString().Trim();

                    lblIdFormularioAsignadoHH.Text = idFormulario;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Formularios Asignados HH

        #region Formularios Sin Asignar HH

        protected void btnSinAsignarHH_Click(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idRol = ddlidRol00.SelectedValue.ToString();
                string idFormulario = lblIdFormularioAsignadoHH.Text;

                SQL = "EXEC SP_EliminarFormulariosAsignados '" + idRol + "', '" + idFormulario + "'";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);

                CargarFormulariosAsignadosHH(true);
                CargarFormulariosSinAsignarHH(true);
                lblIdFormularioAsignadoHH.Text = "";
                lblIdFormularioSinAsignarHH.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void CargarFormulariosSinAsignarHH(bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                //string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                string idRol = ddlidRol00.SelectedValue.ToString();
                bool esWeb = false;

                SQL = "EXEC SP_ObtenerFormulariosSinAsignar '" + idRol + "', '" + esWeb + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridFormulariosSinAsignarHH.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridFormulariosSinAsignarHH.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosSinAsignarHH_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarFormulariosSinAsignarHH(false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridFormulariosSinAsignarHH_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    string idFormulario = item["idFormulario"].Text.ToString().Trim();

                    lblIdFormularioSinAsignarHH.Text = idFormulario;
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion Formularios Sin Asignar HH

        protected void ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddBodega.SelectedIndex > 0)
            {
                FillDDSectores(Convert.ToInt32(ddBodega.SelectedValue));
            }
        }

        private void FillDDSectores(int idBodega)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);

            _listaSectores = nSectorWareHouse.GetSectorsWarehouse(idBodega);
            ddSector.DataSource = _listaSectores;
            ddSector.DataTextField = "Name";
            ddSector.DataValueField = "IdSectorWarehouse";
            ddSector.DataBind();
            ddSector.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }
    }
}