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
using Diverscan.MJP.Negocio.Programa;
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

/// BITACORA DE CAMBIOS
/// CAMBIO REQ00001 14/04/2016 DEBIDO A QUE A LOS APROBADORES DE REQUISICIONES NO LES APARECIAN LA MISMA

namespace Diverscan.MJP.UI.Salidas.Requisicion
{
    public partial class wf_ConsultaRequisicion : System.Web.UI.Page
    {
        public static string TblTab1Mantenimiento = "MaestroRequisicion";
        public static e_Usuario UsrLogged = new e_Usuario();
        public static int ToleranciaAgregar = 85;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
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

        #region Utilidades

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
        #endregion

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidUsuario, e_TablasBaseDatos.VistaUsuariosSinAdmin(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                 Msj = n_SmartMaintenance.CargarDDL(ddlidEstado, e_TablasBaseDatos.TblEstado(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidSolicitante, e_TablasBaseDatos.TblSolicitantes(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidDestino, e_TablasBaseDatos.TblDestino(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SAL-000004" + ex.Message, "");
            }
        }
        //TraceID.(2016). Administracion/Salidas/Pedido/wf_Pedidos.aspx.En Trace ID Codigos documentados(1).Costa Rica:Grupo Diverscan. 

       

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
            CargarGrid(Parent);
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SAL-000005" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SAL-000006" + ex.Message, "");
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

        private void CargarDatos(Control Contenedor)
        {
            try
            {
                string Vista = "";
                string VistaDetalle = "";
                TextBox KEY = new TextBox();
                KEY.Text = "";
                DataSet DS = new DataSet();
                DataSet DSDetalle = new DataSet();
                RadGrid RG = new RadGrid();

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Panel"))
                    {
                        Panel Panel = (Panel)c;
                        if (Panel.CssClass == "TituloPanelVista")
                        {
                            Vista = Panel.ID.Replace("0", string.Empty); ;
                        }
                        else
                        {
                            if (Panel.CssClass == "TituloPanelVistaDetalle")
                                VistaDetalle = Panel.ID.Replace("0", string.Empty);
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    {
                        TextBox TxtB = (TextBox)c;

                        if (TxtB.CssClass == "TextBoxBusqueda")
                        {
                            KEY = TxtB;
                        }
                        else
                        {
                            TxtB.Text = "";
                        }
                    }
                   
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    if (TxtB.CssClass == "TextBoxBusqueda")
                                    {
                                        KEY = TxtB;
                                    }
                                    else
                                    {
                                        TxtB.Text = "";
                                    }
                                }
                                else
                                {
                                    if (ccc is CheckBox)
                                    {
                                        CheckBox ChkB = (CheckBox)ccc;
                                        ChkB.Checked = false;
                                    }
                                    else
                                    {
                                        if (ccc is DropDownList)
                                        {
                                            DropDownList DdlB = (DropDownList)ccc;
                                            DdlB.Text = "--Seleccionar--";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                    {
                        CheckBox ChkB = (CheckBox)c;
                        ChkB.Checked = false;
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                    {
                        DropDownList DdlB = (DropDownList)c;
                        DdlB.Text = "--Seleccionar--";
                    }

                    if (c.GetType().ToString().Equals("Telerik.Web.UI.RadGrid"))
                    {
                        RadGrid RGn = (RadGrid)c;
                        RG = RGn;
                    }      
                }

                DS = n_ConsultaDummy.GetDataSet(Vista, KEY, "jgondres");
                double cantregistros = 0;
                try
                {
                    cantregistros = DS.Tables[0].Rows.Count;
                }
                catch (Exception)
                {
                    cantregistros = 0;
                }

                if (KEY.Text != "")
                {
                    if (cantregistros == 0)
                    {
                        Mensaje("info", "No hay resultados para la busqueda", "");
                    }
                    else
                    {
                        if (cantregistros == 1)
                        { Mensaje("ok", "Encontramos " + cantregistros.ToString() + " registro para su busqueda.", ""); }
                        else
                        { Mensaje("ok", "Encontramos " + cantregistros.ToString() + " registros para su busqueda.", ""); }
                    }
                }
                CargarGrid(Contenedor);
                if (Vista != "" && KEY.Text != "")
                {
                    foreach (Control c in Contenedor.Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                        {
                            TextBox TxtB = (TextBox)c;
                            if (TxtB.CssClass != "TextBoxBusqueda")
                            {
                                if (DS.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataColumn column in DS.Tables[0].Columns)
                                    {
                                        if (column.ColumnName == TxtB.ID)
                                        {
                                            TxtB.Text = DS.Tables[0].Rows[0][column].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    TxtB.Text = "";
                                }
                            }
                        }

                        if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                        {
                            foreach (Control cc in c.Controls)
                            {
                                foreach (Control ccc in cc.Controls)
                                {
                                    if (ccc is TextBox)
                                    {
                                        TextBox TxtB = (TextBox)ccc;
                                        if (TxtB.CssClass != "TextBoxBusqueda")
                                        {
                                            if (DS.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataColumn column in DS.Tables[0].Columns)
                                                {
                                                    if (column.ColumnName == TxtB.ID)
                                                    {
                                                        TxtB.Text = DS.Tables[0].Rows[0][column].ToString();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TxtB.Text = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ccc is CheckBox)
                                        {
                                            CheckBox ChkB = (CheckBox)ccc;
                                            if (DS.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataColumn column in DS.Tables[0].Columns)
                                                {
                                                    if (column.ColumnName == ChkB.ID)
                                                    {
                                                        ChkB.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0][column].ToString());
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (ccc is DropDownList)
                                            {
                                                DropDownList DdlB = (DropDownList)ccc;
                                                if (DS.Tables[0].Rows.Count > 0)
                                                {
                                                    foreach (DataColumn column in DS.Tables[0].Columns)
                                                    {
                                                        if (column.ColumnName == DdlB.ID)
                                                        {
                                                            string Texto = DS.Tables[0].Rows[0][column].ToString();
                                                            DdlB.SelectedIndex = DdlB.Items.IndexOf(DdlB.Items.FindByText(Texto));
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }

                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                        {
                            CheckBox ChkB = (CheckBox)c;
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataColumn column in DS.Tables[0].Columns)
                                {
                                    if (column.ColumnName == ChkB.ID)
                                    {
                                        ChkB.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0][column].ToString());
                                    }
                                }
                            }
                        }
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                        {
                            DropDownList DdlB = (DropDownList)c;
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataColumn column in DS.Tables[0].Columns)
                                {
                                    if (column.ColumnName == DdlB.ID)
                                    {
                                        string Texto = DS.Tables[0].Rows[0][column].ToString();
                                        DdlB.SelectedIndex = DdlB.Items.IndexOf(DdlB.Items.FindByText(Texto));
                                    }
                                }
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SAL-000007" + ex.Message, "");
            }
        }
       
        private void CargarGrid(Control Contenedor)
        {
            try
            {
                string VistaDetalle = "";
                DataSet DSDetalle = new DataSet();
                TextBox KEY = new TextBox();
                RadGrid RG = new RadGrid();

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Panel"))
                    {
                        Panel Panel = (Panel)c;
                        if (Panel.CssClass == "TituloPanelVistaDetalle")
                            VistaDetalle = Panel.ID.Replace("0", string.Empty);
                    }
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    {
                        TextBox TxtB = (TextBox)c;

                        if (TxtB.CssClass == "TextBoxBusqueda")
                        {
                            KEY = TxtB;
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    if (TxtB.CssClass == "TextBoxBusqueda")
                                    {
                                        KEY = TxtB;
                                    }
                                }
                            }
                        }
                    }
                    if (c.GetType().ToString().Equals("Telerik.Web.UI.RadGrid"))
                    {
                        RadGrid RGn = (RadGrid)c;
                        RG = RGn;
                    }
                }

                if (VistaDetalle != "")
                {
                    DSDetalle = n_ConsultaDummy.GetDataSet(VistaDetalle, KEY, "jgondres");
                    if (DSDetalle.Tables[0].Rows.Count > 0)
                    {
                        RG.DataSource = DSDetalle;
                        RG.Rebind();
                    }
                }
            }
            catch (Exception ex )
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-ADM-SAL-000008" + ex.Message, "");
            }
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
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, TblTab1Mantenimiento, ToleranciaAgregar, UsrLogged.IdUsuario);
            Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, TblTab1Mantenimiento, UsrLogged.IdUsuario);
            Mensaje(Msj[0], Msj[1], "");
        }

        #endregion //EventosFrontEnd

        #endregion //TabsControl
    }
}

