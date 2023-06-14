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
using Diverscan.MJP.Utilidades.general;



namespace Diverscan.MJP.UI.FlujoDeTrabajo
{
    public partial class wf_FlujosDeProceso : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        RadGridProperties radGridProperties = new RadGridProperties();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            GetShapes();

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
        private void CargarDDLS()
        {
            //TraceID.(2016). FlujosDeTrabajo/wf_FlujosDeProceso.En Trace ID Codigos documentados(2).Costa Rica:Grupo Diverscan. 
        }

        #region TabsControl

        #region DiagramControl

        public void GetShapes()
        {        
            List<string> ListaIdProceso = new List<string>();
            List<string> ListaNombreProceso = new List<string>();
            DataSet DS = new DataSet();
            DS = n_ConsultaDummy.GetDataSet("select idproceso as ID , nombre as [Text], '#619eea' as Background, 'm0,0 L170,0 L170,140 L0,170 z' as d from " + e_TablasBaseDatos.TblProceso(), UsrLogged.IdUsuario);            
            if (DS.Tables[0].Rows.Count > 0)
            {
                ShapesList.DataSource = DS.Tables[0];                
                ShapesList.DataBind();
                RadDiagram1.Width = 750;
                RadDiagram1.Height = 480;
                RadDiagram1.ZoomMin = 0.75;
                RadDiagram1.ZoomMax = 1;
                RadDiagram1.ShapeDefaultsSettings.Width = 180;
                RadDiagram1.ShapeDefaultsSettings.Height = 30;
                RadDiagram1.ShapeDefaultsSettings.StrokeSettings.Color = "#fff";
                CargarShapesWF_from_DB();
                CargarConnectors_from_DB();
            }
            else
            {
                Mensaje("info", "No hay procesos para cargar","");            
            }     
        }

        #endregion //DiagramControl

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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000001" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000002" + ex.Message, "");
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

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblActividades(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblActividades(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.CargarDatos(Panel, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void AgregarWFShapesProcesos()
        {
            try
            {
            var json = JsonText.Text;
            string SQL = "";
            string idflow = "1";
            string source_table_key= "idproceso";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            e_Diagrama.ShapesAndConnectios ShapesAndConnections = ser.Deserialize<e_Diagrama.ShapesAndConnectios>(json);

            SQL = "delete from " + e_TablasBaseDatos.TblShapes() + " where " + e_TblShapesFields.idflow();
            SQL += " = '" + idflow + "'";
            n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);

            for (int i =0; i<ShapesAndConnections.shapes.Count; i++)
            {
                SQL = "insert into " + e_TablasBaseDatos.TblShapes() + " (";
                SQL += e_TblShapesFields.idShape() + ", ";
                SQL += e_TblShapesFields.idflow() + ", ";
                SQL += e_TblShapesFields.content_color() + ", ";
                SQL += e_TblShapesFields.fill_color() + ", ";
                SQL += e_TblShapesFields.source_table() + ", ";
                SQL += e_TblShapesFields.source_table_key() + ", ";
                SQL += e_TblShapesFields.type_() + ", ";
                SQL += e_TblShapesFields.x() + ", ";
                SQL += e_TblShapesFields.y() + " )";
                SQL += " values ('";
                SQL += ShapesAndConnections.shapes[i].id + "', '";
                SQL += idflow +"', '";
                SQL += ShapesAndConnections.shapes[i].content.color + "', '";
                SQL += ShapesAndConnections.shapes[i].fill.color + "', '";
                SQL += e_TablasBaseDatos.TblProceso() + "', '";
                SQL += source_table_key + "', '";
                SQL += ShapesAndConnections.shapes[i].type.ToString() + "', '";
                SQL += ShapesAndConnections.shapes[i].x + "', '";
                SQL += ShapesAndConnections.shapes[i].y + "')";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);
            }
            CargarShapesWF_from_DB();
            AgregarWFConnetionsProcesos(ShapesAndConnections);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000003" + ex.Message, "");
            }
        }

        private void CargarShapesWF_from_DB()
        {
            try
            {
            string SQL = "";
            string idflow = "1";
            string source_table_key = "idproceso";
            DataSet DS = new DataSet(); 
            SQL = "select ";                                  // column
            SQL += e_TblShapesFields.idShape() + ", ";              // 0
            SQL += e_TblShapesFields.idflow() + ", ";               // 1
            SQL += e_TblShapesFields.content_color() + ", ";        // 2
            SQL += e_TblShapesFields.fill_color() + ", ";           // 3
            SQL += e_TblShapesFields.source_table() + ", ";         // 4
            SQL += e_TblShapesFields.source_table_key() + ", ";     // 5
            SQL += e_TblShapesFields.type_() + ", ";                // 6
            SQL += e_TblShapesFields.x() + ", ";                    // 7
            SQL += e_TblShapesFields.y() + " ";                     // 8
            SQL += " from " + e_TablasBaseDatos.TblShapes();
            SQL += " where " + e_TblShapesFields.idflow() + " = '" + idflow  + "' ";
            DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
            string source_table = "";
            if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        DiagramShape shape = new DiagramShape();
                        source_table = DS.Tables[0].Rows[i][4].ToString();
                        source_table_key = DS.Tables[0].Rows[i][5].ToString();
                        shape.Id = DS.Tables[0].Rows[i][0].ToString();
                        shape.ContentSettings.Color = DS.Tables[0].Rows[i][2].ToString();
                        shape.FillSettings.Color = DS.Tables[0].Rows[i][3].ToString();
                        SQL = "select nombre from " + source_table + "  where " + source_table_key + " = '" + shape.Id + "' ";
                        shape.ContentSettings.Text = n_ConsultaDummy.GetUniqueValue(SQL,UsrLogged.IdUsuario);
                        shape.Type = DS.Tables[0].Rows[i][6].ToString();
                        shape.X = double.Parse(DS.Tables[0].Rows[i][7].ToString());
                        shape.Y = double.Parse(DS.Tables[0].Rows[i][8].ToString());
                        RadDiagram1.ShapesCollection.Add(shape);  
                    }               
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000004" + ex.Message, "");
            }
        }

        protected void AgregarWFConnetionsProcesos(e_Diagrama.ShapesAndConnectios ShapesAndConnections)
        {
            try
            {
                string SQL = "";
                string idflow = "1";
                SQL = "delete from " + e_TablasBaseDatos.TblConnectios() + " where " + e_TblConnectiosFields.idflow();
                SQL += " = '" + idflow + "'";
                n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);
                for (int i = 0; i < ShapesAndConnections.connections.Count; i++)
                {
                    SQL = "insert into " + e_TablasBaseDatos.TblConnectios() + " (";
                    SQL += e_TblConnectiosFields.idflow() + ", ";
                    SQL += e_TblConnectiosFields.nombre() + ", ";
                    SQL += e_TblConnectiosFields.idfrom() + ", ";
                    SQL += e_TblConnectiosFields.idto() +")";
                    SQL += " values ('";
                    SQL += idflow + "', '";
                    SQL += "No registra','";
                    SQL += ShapesAndConnections.connections[i].from.shapeId + "', '";
                    SQL += ShapesAndConnections.connections[i].to.shapeId + "')";
                    n_ConsultaDummy.PushData(SQL, UsrLogged.IdUsuario);
                }
                CargarConnectors_from_DB();
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-FLT-000005" + ex.Message, "");
            }
        }

        private void CargarConnectors_from_DB()
        {
            try
            {
                DataSet DS = new DataSet();
                string SQL = "";
                string idflow = "1";

                SQL = "select ";                                  // column
                SQL += e_TblConnectiosFields.nombre() + ", ";       // 0
                SQL += e_TblConnectiosFields.idfrom() + ", ";       // 1
                SQL += e_TblConnectiosFields.idto() + " ";         // 2
                SQL += " from " + e_TablasBaseDatos.TblConnectios();
                SQL += " where " + e_TblConnectiosFields.idflow() + " = '" + idflow + "' ";
                DS = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        DiagramConnection connection = new DiagramConnection();
                        connection.FromSettings.ShapeId = DS.Tables[0].Rows[i][1].ToString();
                        connection.ToSettings.ShapeId = DS.Tables[0].Rows[i][2].ToString();
                        string from = DS.Tables[0].Rows[i][1].ToString();
                        string to = DS.Tables[0].Rows[i][2].ToString();
                        RadDiagram1.ConnectionsCollection.Add(connection);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000006" + ex.Message, "");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarWFShapesProcesos();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblProceso(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
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

        #endregion //EventosFrontEnd

        protected void dropdownlist1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion //TabsControl

    }
}