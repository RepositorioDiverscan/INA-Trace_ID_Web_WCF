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
using Diverscan.MJP.Negocio.LogicaWMS;



namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class AjusteInventario : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();    // VarlorCantidadSeleccionada se usa para guardar el valor cuando se pasa de RadListBox1 a RadListBox2
        double VarlorCantidadSeleccionada = 0;
        // IdArticuloSeleccionado se usa para guardar el idarticulo cuando se pasa de RadListBox2 a RadListBox1
        string IdDetalleOrdenCompra = "";
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            //  CargarAccionesPagina(Pagina);


            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }


            if (!IsPostBack)
            {
                string SqlArticulos = "SELECT A.Nombre FROM ADMMaestroArticulo A WHERE idCompania = '" + UsrLogged.idCompania + "' Order by A.Nombre asc";
                string SqlZonas = "SELECT A.Nombre FROM ADMUBIZonas A";
                string SqlTipoAjuste = "SELECT A.Nombre  From ADMAjusteInventario A WHERE idCompania = '" + UsrLogged.idCompania + "'";
                string SqlUsuarios = "SELECT A.Nombre From Usuarios A WHERE idCompania = '" + UsrLogged.idCompania + "'";

                CargarDrop(didZona, SqlZonas, "Nombre");
                CargarDrop(ddidArticulo, SqlArticulos, "Nombre");
                CargarDrop(ddlIDUSUARIO00,SqlUsuarios,"Nombre");
                CargarDrop(ddlidAjusteInventario, SqlTipoAjuste, "Nombre");

                ddTipoAjuste.Items.Add("Ajuste de Inventario Sobrante");
                ddTipoAjuste.Items.Add("Ajuste de Inventario Faltante");
                ddTipoAjuste.Items.Insert(0, new ListItem("--Seleccionar--"));
                CargarDDLS();

            }

        }

        private void CargarAccionesPagina(string Pagina)
        {
            try
            {
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                List<e_AccionFlujo> Acciones = MD.ObtenerAcciones(Pagina);
                foreach (Control c in Panel1.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is Button)
                                {
                                    if (Acciones.Exists(x => x.ObjetoFuente == ccc.ID))
                                    {
                                        Button Btn = (Button)ccc;
                                        Btn.Visible = true;
                                        Btn.Text = Acciones.Find((x) => x.ObjetoFuente == ccc.ID).Nombre;
                                        Btn.Click += new EventHandler(Accion);
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (Control c in Panel2.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is Button)
                                {
                                    if (Acciones.Exists(x => x.ObjetoFuente == ccc.ID))
                                    {
                                        Button Btn = (Button)ccc;
                                        Btn.Visible = true;
                                        Btn.Text = Acciones.Find((x) => x.ObjetoFuente == ccc.ID).Nombre;
                                        Btn.Click += new EventHandler(Accion);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-INV-000001" + ex.Message, "");
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
            this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
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

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

            // Se hablita el evento PreRender para que antes de renderizar el Grid se formateen las columnas y hacerlo más User Fiendly
            FormatearColumnas(RadGrid1);
        }

        protected void RagGrid2_PreRender(object sender, EventArgs e)
        {
            // Se hablita el evento PreRender para que antes de renderizar el Grid se formateen las columnas y hacerlo más User Fiendly
            FormatearColumnas(RadGrid2);
        }

        protected void RadGrid3_PreRender(object sender, EventArgs e)
        {
            // Se hablita el evento PreRender para que antes de renderizar el Grid se formateen las columnas y hacerlo más User Fiendly
            FormatearColumnas(RadGrid3);
        }

        private void FormatearColumnas(RadGrid grid)
        {
            try
            {
                //bool dobleMayuscula = false;

                // Se recorren cada una de las columnas autogeneradas y luego se procede a validar el nombre de que tiene el header de la columna para cambiarle el nombre
                foreach (GridColumn col in grid.MasterTableView.AutoGeneratedColumns)
                {

                    // Validamos la cantidad de mayusculas para formatear el nombre de la columna como "CodigoArticulo" y no como "Codigoarticulo"
                    //if (CantidadMayusculas(col.UniqueName) > 1)
                    //    dobleMayuscula = true;


                    // Las instrucciones que están dentro de los IF lo que hacen es solamente ponerle la 
                    // primera letra en mayúscula, con esto le damos más estandar a los nombres de las columnas
                    if (col.UniqueName.StartsWith("txtid"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txtid", "")[0]) + col.UniqueName.ToLower().Replace("txtid", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().Equals("ddlidusuario"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("ddlid", "")[0]) + col.UniqueName.ToLower().Replace("ddlid", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().Equals("ddl idusuario"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("ddl id", "")[0]) + col.UniqueName.ToLower().Replace("ddl id", "").Substring(1);
                    }
                    else if (col.UniqueName.StartsWith("txt id"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txt id", "")[0]) + col.UniqueName.ToLower().Replace("txt id", "").Substring(1);
                    }
                    else if (col.UniqueName.StartsWith("txtl"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txtl", "")[0]) + col.UniqueName.ToLower().Replace("txtl", "").Substring(1);
                    }
                    else if (col.UniqueName.StartsWith("ddl id "))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("ddl id ", "")[0]) + col.UniqueName.ToLower().Replace("ddl id ", "").Substring(1);
                    }
                    else if (col.UniqueName.StartsWith("txt"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txt", "")[0]) + col.UniqueName.ToLower().Replace("txt", "").Substring(1);
                    }
                    else if (col.UniqueName.StartsWith("txm"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txm", "")[0]) + col.UniqueName.ToLower().Replace("txm", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().StartsWith("ddlid"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("ddlid", "")[0]) + col.UniqueName.ToLower().Replace("ddlid", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().StartsWith("ddl id"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("ddl id", "")[0]) + col.UniqueName.ToLower().Replace("ddl id", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().StartsWith("txtlid"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txtlid", "")[0]) + col.UniqueName.ToLower().Replace("txtlid", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().StartsWith("chk"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("chk", "")[0]) + col.UniqueName.ToLower().Replace("chk", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().Equals("txt idusuario"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("txt", "")[0]) + col.UniqueName.ToLower().Replace("txt", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().StartsWith("dtp"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("dtp", "")[0]) + col.UniqueName.ToLower().Replace("dtp", "").Substring(1);
                    }
                    else if (col.UniqueName.ToLower().Equals("id compania"))
                    {
                        col.HeaderText = char.ToUpper(col.UniqueName.ToLower().Replace("id", "")[0]) + col.UniqueName.ToLower().Replace("id", "").Substring(1);
                    }
                }
                grid.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CantidadMayusculas(string nombreColumna)
        {
            try
            {
                int cantMayusculas = 0;

                for (int i = 0; i < nombreColumna.Length; i++)
                {
                    if (char.IsUpper(nombreColumna[i]))
                    {
                        cantMayusculas++;
                    }
                }

                return cantMayusculas;
            }
            catch (Exception)
            {
                return 0;
            }

            
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIDUSUARIO, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidMetodoAccion, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                //Tab2

                Msj = n_SmartMaintenance.CargarDDL(ddlidMetodoAccion0, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdEstado0, e_TablasBaseDatos.TblEstado(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIDUSUARIO0, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
               

                //Tab3

                //Msj = n_SmartMaintenance.CargarDDL(ddidArticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidAjusteInventario, e_TablasBaseDatos.TBLADMAjusteInventario(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlIDUSUARIO00, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidZona, e_TablasBaseDatos.TblZonas(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                //n_SmartMaintenance.CargarDDLsHoras(Panel3);
                //TraceID.(2016). Administracion/Inventario/AjusteInventario.En Trace ID Codigos documentados(1).Costa Rica:Grupo Diverscan. 
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-INV-000002" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-INV-000003" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-INV-000003" + ex.Message, "");
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

        protected void BtnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblBitacoraAjustesAplicados(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

        }

        protected void BtnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblBitacoraAjustesAplicados(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TBLADMAjusteInventario(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TBLADMAjusteInventario(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblTransaccion(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblTransaccion(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        public void Accion(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            Mensaje("ok", resultado, "");
        }

        #endregion //EventosFrontEnd

        private void CargarDrop(DropDownList DDL , string query , string value)
        {
            try
            {
                DDL.Items.Clear();
                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = n_ConsultaDummy.GetDataSet(query, "0");
                if (DSBaseDatos != null)
                {
                    if (DSBaseDatos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                        {
                            string name = dsRowEvento["Nombre"].ToString();
                            DDL.Items.Add(name);
                        }
                       // DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
                        DDL.DataBind();
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        protected void ddTipoAjuste_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddTipoAjuste.SelectedIndex.ToString().Equals("1"))
            {

                txtTipoAjuste.Text = "True";

            }
            else if (ddTipoAjuste.SelectedIndex.ToString().Equals("2"))
            {

                txtTipoAjuste.Text = "False";
            }
            else 
            {
                txtTipoAjuste.Text = "";
            
            }

        }

        #endregion //TabsControl

        protected void ddlidArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                    dlLoteVencimiento.Items.Clear();
         
                    string Zona = didZona.SelectedItem.Value;
                    string SqlIdZona = "Select A.idZona From ADMUBIZonas A Where A.Nombre = '" + Zona + "'";
                    string Articulo = ddidArticulo.SelectedItem.Value;
                    string idZona = n_ConsultaDummy.GetUniqueValue(SqlIdZona, "0");
                    //txtZona
                    txtidZona.Text = idZona;
                    if (!Zona.Equals("--Seleccionar--") && !Articulo.Equals("--Seleccionar--") && !idZona.Equals("")) 
                    {

                        string SQLidArticulo = "SELECT A.idArticulo FROM ADMMaestroArticulo A Where A.Nombre = '" + Articulo + "'";
                        string idArticulo = n_ConsultaDummy.GetUniqueValue(SQLidArticulo, "0");
                        txtidArticulo.Text = idArticulo;
                        if (!String.IsNullOrEmpty(idArticulo))
                        {

                            List<e_Ubicacion> Ubicaciones = n_WMS.ObtenerDisponibilidadArticulo(idArticulo, "1000", "0",idZona);

                            foreach (e_Ubicacion Ubic in Ubicaciones)
                            {
                                foreach(e_Articulo Art in Ubic.Articulos)
                                {
                                    string FechaVencimiento = Art.FechaVencimiento.ToString();

                                   dlLoteVencimiento.Items.Add(Art.FechaVencimiento.ToString() + " | " + Art.Lote.ToString() + " | " + Ubic.idUbicacion + " | " + Ubic.idBodega);
                              
                                }
                            }
                             // dlLoteVencimiento.Items.Insert(0, new ListItem("--Seleccionar--"));

                        //   dlLoteVencimiento.Items.Insert(0, "--Seleccionar--");

                            //string InfoArticulo = "SELECT A.Lote , A.FechaVencimiento FROM TRAIngresoSalidaArticulos A Where A.idTablaCampoDocumentoAccion LIkE '%TRACEID.dbo.OPEINGDetalleOrdenCompra%' and A.idArticulo = '" + idArticulo + "'";
                            //DSBaseDatos = n_ConsultaDummy.GetDataSet(InfoArticulo, "0");
                            //if (DSBaseDatos.Tables[0].Rows.Count > 0)
                            //{
                            //    foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                            //    {
                            //        string Lote = dsRowEvento["Lote"].ToString();
                            //        string FechaVencimiento = dsRowEvento["FechaVencimiento"].ToString();
                            //        string valor = "Lote: " + Lote + "  FechaVencimiento: " + FechaVencimiento;
                            //        dlLoteVencimiento.Items.Add(valor);

                            //    }
                            //}
                        }
                    
                    }
                
                }                           
            catch (Exception)
            {

                throw;
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {

            try
            {

               // txidBodega.Text = FechaLote[3].ToString();
                string[] FechaLote = dlLoteVencimiento.SelectedItem.Value.Split('|');

                string  txUbicacionAct = FechaLote[2].ToString();
                string SqlAjusteInventario = "Select A.idAjusteInventario From ADMAjusteInventario A Where A.Nombre = '" + ddlidAjusteInventario.SelectedItem.Value.ToString() + "'";
                string SqlIdUsuario = "Select A.IDUSUARIO From USUARIOS A WHERE A.Nombre = '" + ddlIDUSUARIO00.SelectedItem.Value.ToString() + "'";
                string idAjusteInventario = n_ConsultaDummy.GetUniqueValue(SqlAjusteInventario,"0");
                string idUsuario = n_ConsultaDummy.GetUniqueValue(SqlIdUsuario,"0");
                string Cantidad = txtCantidad0.Text;
                string InsertBitAjustesAplicados = "Insert Into BitacoraAjustesAplicados(idZona , idArticulo ,idAjusteInventario,idUsuario,Cantidad,lote,FechaVencimiento) "
                + "Values(" + txtidZona.Text + "," + txtidArticulo.Text + "," + idAjusteInventario + "," + idUsuario + "," + Cantidad + ",'" + FechaLote[1].ToString() + "','" + FechaLote[0].ToString() + "')";

                
                n_ConsultaDummy.PushData(InsertBitAjustesAplicados, "0");
                
                string UbicaciónDestino = "";

                switch (UbicaciónDestino)
                {

                    case "1":
                        UbicaciónDestino = "2594";

                        break;
                    case "2":
                        UbicaciónDestino = "2595";
                        break;
                }
                string CodGS1 = n_WMS.CrearCodigoGS1(txtidArticulo.Text, txtCantidad.Text, txtFechaVencimiento.Text, txtLote.Text, "0");
                string CodLeido = CodGS1 + " ; " + txUbicacionAct + " ; " + UbicaciónDestino + " ; " + "0";

                n_WMS.LeerCodigoParaUbicarHH(CodLeido, "0", "75");

            }
            catch (Exception)
            {
                
                throw;
            }

        }


    }
}