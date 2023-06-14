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
using Diverscan.MJP.Negocio;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.MotorDecisiones;
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
using System.Reflection.Emit;
using System.Threading;

namespace Diverscan.MJP.UI.Operaciones.Ingresos
{
    public partial class ProcesarOrdenCompra : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        // VarlorCantidadSeleccionada se usa para guardar el valor cuando se pasa de RadListBox1 a RadListBox2
        double VarlorCantidadSeleccionada = 0;
        // IdArticuloSeleccionado se usa para guardar el idarticulo cuando se pasa de RadListBox2 a RadListBox1
        string IdDetalleOrdenCompra = "";
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        List<string> LineasDetalle = new List<string>();
        List<string> Cantidades = new List<string>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel_Unload);
        }

        void UpdatePanel_Unload(object sender, EventArgs e)
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
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel2 });
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel3 });
                }
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            CargarAccionesPagina(Pagina);

            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadListBox1, RadListBox1, RadAjaxLoadingPanel1);
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {                
                CargarDDLS();
                CargarListBox1();
                CargarListBox2();
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

        private void CargarDDLS()
        {
         //TraceID.(2016). Operaciones/Ingresos/ProcesarOdenCompra.En Trace ID Codigos documentados(8).Costa Rica:Grupo Diverscan.  
               //string[] Msj = n_SmartMaintenance.CargarDDL(DDLidMaestroOrdenCompra, e_TablasBaseDatos.VistaMaestroOrdenCompraProveedor(), UsrLogged.IdUsuario);
               // if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }
             
        #region TabsControl

        #region CargaDeEventosDelMotordeDecision

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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000001" + ex.Message, "");
            }

        } 

        #endregion

        #region ListBoxControl

        private void CargarListBox1()
        {
            string SQLQuery = "";
            SQLQuery = "SELECT a.idDetalleOrdenCompra, b.nombre, a.CantidadxRecibir ";
            SQLQuery += "FROM OPEINGDetalleOrdenCompra as a, ADMMaestroArticulo as b ";
            SQLQuery += "where a.idarticulo = b.idArticulo ";
            SQLQuery += "and a.idmaestroordencompra = '" + ddlidMaestroOrdenCompra.Text + "' ";
            SQLQuery += "and a.CantidadRecibida = 0 ";
            n_ManejadorControlesASPX.CargarRadListBox(RadListBox1, SQLQuery, UsrLogged.IdUsuario, "Qtxt11", "nombre", "idDetalleOrdenCompra");
        }

        private void CargarListBox2()
        {
            string SQLQuery = "";
            SQLQuery = "SELECT a.idDetalleOrdenCompra, b.nombre, a.CantidadRecibida ";
            SQLQuery += "FROM OPEINGDetalleOrdenCompra as a, ADMMaestroArticulo as b ";
            SQLQuery += "where a.idarticulo = b.idArticulo ";
            SQLQuery += "and a.idmaestroordencompra = '" + ddlidMaestroOrdenCompra.Text + "' ";
            SQLQuery += "and a.CantidadRecibida > 0 ";
            n_ManejadorControlesASPX.CargarRadListBox(RadListBox2, SQLQuery, UsrLogged.IdUsuario, "Qtxt12", "nombre", "idDetalleOrdenCompra");
        }

        protected void RadListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = String.Format(
                "RadListBox1: SelectedIndexChanged {{ SelectedIndex: {0}, SelectedValue: {1} }}",
                RadListBox1.SelectedIndex,
                RadListBox1.SelectedValue);

        }

        
        private void LogEvent(object sender, string eventName, IEnumerable<RadListBoxItem> items)
        {
            List<string> affectedItems = new List<string>();
            foreach (RadListBoxItem item in items)
            {
                affectedItems.Add(item.Text + "(" + item.Value + ")");
            }

            string message = string.Format("{0}: {1} Items:{2}",
                "Orden de Compra:",
                eventName,
                String.Join(", ", affectedItems.ToArray())
                );
            Mensaje("info", message, "");
           
        }

        protected void RadListBox1_Inserting(object sender, RadListBoxInsertingEventArgs e)
        {
            LogEvent(sender, "Inserting", e.Items);
        }

        protected void RadListBox1_Inserted(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Inserted", e.Items);
        }

        protected void RadListBox1_Reordered(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Reordered", e.Items);
        }

        protected void RadListBox1_Reordering(object sender, RadListBoxReorderingEventArgs e)
        {

            LogEvent(sender, "Reordering", e.Items);
        }

        protected void RadListBox1_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            foreach (RadListBoxItem item in e.Items)
            {
                if (e.SourceListBox == RadListBox1)
                {
                    RadNumericTextBox RN2 = (RadNumericTextBox)item.FindControl("Qtxt12");
                    if (RN2 != null)
                    {
                        RN2.Value = VarlorCantidadSeleccionada;
                        RN2.MaxValue = VarlorCantidadSeleccionada;
                    }
                    item.Text = item.Text;
                }
                else
                {
                    DataSet DS = new DataSet();
                    string SQLQuery = "";
                    SQLQuery = "SELECT "+e_TblDetalleOrdenesCompraFields.CantidadxRecibir()+" ";
                    SQLQuery += "FROM "+e_TablasBaseDatos.TblDetalleOrdenesCompra() +" ";
                    SQLQuery += "where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + IdDetalleOrdenCompra + "' ";
                    SQLQuery += "and " + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + " = '" + ddlidMaestroOrdenCompra.Text + "' ";
                    DS = n_ConsultaDummy.GetDataSet(SQLQuery, UsrLogged.IdUsuario);
                    RadNumericTextBox RN1 = (RadNumericTextBox)item.FindControl("Qtxt11");
                    if (RN1 != null) RN1.Value = double.Parse( DS.Tables[0].Rows[0][0].ToString());
                    item.Text = item.Text;
                }
                item.DataBind();
            }
            LogEvent(sender, "Transeferido", e.Items);
        }
        protected void RadListBox1_Transferring(object sender, RadListBoxTransferringEventArgs e)
        {           
                foreach (RadListBoxItem item in e.Items)
                {
                    if (e.SourceListBox == RadListBox1)
                    {
                        RadNumericTextBox RN1 = (RadNumericTextBox)item.FindControl("Qtxt11");
                        if (RN1 != null)
                        {
                            VarlorCantidadSeleccionada = double.Parse(RN1.Text);
                        }
                    }
                    else
                    {
                        IdDetalleOrdenCompra = item.Value;
                    }
                }
       
            LogEvent(sender, "Transferiendo", e.Items);
        }

        protected void RadListBox1_Updating(object sender, RadListBoxUpdatingEventArgs e)
        {
            LogEvent(sender, "Updating", e.Items);
        }

        protected void RadListBox1_Updated(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Updated", e.Items);
        }

        protected void RadListBox1_Deleted(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Deleted", e.Items);
        }

        protected void RadListBox1_Deleting(object sender, RadListBoxDeletingEventArgs e)
        {
            LogEvent(sender, "Deleting", e.Items);
        }

        protected void RadListBox2_Reordered(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Reordered", e.Items);
        }

        protected void RadListBox2_Reordering(object sender, RadListBoxReorderingEventArgs e)
        {
            LogEvent(sender, "Reordering", e.Items);
        }

        protected void RadListBox2_Deleting(object sender, RadListBoxDeletingEventArgs e)
        {
            LogEvent(sender, "Deleting", e.Items);
        }

        protected void RadListBox2_Deleted(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Deleted", e.Items);
        }

        protected void RadListBox2_Inserting(object sender, RadListBoxInsertingEventArgs e)
        {
            LogEvent(sender, "Inserting", e.Items);
        }

        protected void RadListBox2_Inserted(object sender, RadListBoxEventArgs e)
        {
            LogEvent(sender, "Inserted", e.Items);
        }
        
        #endregion
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
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000002" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000003" + ex.Message, "");
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
        protected void btnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblAcciones(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblAcciones(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }
        
        //protected void btnEditar2_Click(object sender, EventArgs e)
        //{
        //TraceID.(2016). Operaciones/Ingresos/ProcesarOdenCompra.En Trace ID Codigos documentados(9).Costa Rica:Grupo Diverscan. 
        //}

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {            
            string Pagina = "~/Operaciones/Ingresos/ProcesarOrdenCompra.aspx";
            string CodLeido = txtCodigoLeido.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion21");
            txtInformacion.Text = Respuesta;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListBox1();
            CargarListBox2();
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }
            
        public void Accion(object sender, EventArgs e)
        {
           // procesar la OC en HH
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = string.Empty;
            if (Ctr.ID == "btnAccion21")
            {
                string Pagina = "~/Operaciones/Ingresos/ProcesarOrdenCompra.aspx";
                string CodLeido = txtCodigoLeido.Text;
                resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, Ctr.ID.ToString());
                txtInformacion.Text = resultado;
            }
            else
            {
                resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            }          
            Mensaje("ok", resultado, "");
        }

        #endregion //EventosFrontEnd

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        #endregion //TabsControl
    
    }
}