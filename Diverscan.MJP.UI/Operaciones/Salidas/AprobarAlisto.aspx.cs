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
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Diverscan.MJP.Negocio;
using Timer = System.Timers.Timer;
using System.Security.Cryptography;


namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class AprobarAlisto : System.Web.UI.Page
    {
        private static string[] CargaDetalleSol;
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 95;
        string Pagina = "";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel3.Unload += new EventHandler(UpdatePanel_Unload2);
        }

        void UpdatePanel_Unload2(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel2(sender as UpdatePanel);
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
                //CargarMaestroSolicitudesPendientes("", ddlAlistos);
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
                string[] Msj =  n_SmartMaintenance.CargarDDL(ddlidActividad, e_TablasBaseDatos.TblActividades(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidEvento, e_TablasBaseDatos.TblEventos(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlObjetoFuente, e_TablasBaseDatos.VistaObjetosFuente(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlAlistos, e_TablasBaseDatos.VistaPendientesAlisto(), UsrLogged.IdUsuario, false);
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000007" + ex.Message, "");
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

        private void CargarControles(string VitualPath, DropDownList DDL)
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
                }
                DDL.DataTextField = "nombre";
                DDL.DataValueField = "idObjetoFuente";
                DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
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
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000008" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-FLT-000009" + ex.Message, "");
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
        }

        protected void btnAgregar5_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblParametrosMetodo(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar4_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblMetodoAccion(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblProceso(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblProceso(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
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

        #endregion //EventosFrontEnd

        #endregion //TabsControl

        #region Despliegue de alistos

        private string idMaestro(string idDetalle)
        {
            string SQLMaestro = " Select a.idMaestroSolicitud From OPESALDetalleSolicitud a Where a.idLineaDetalleSolicitud = " + idDetalle;
            string idMaestro = n_ConsultaDummy.GetUniqueValue(SQLMaestro, "0");

            return idMaestro;
        }

        public string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        private void CargarMaestroSolicitudesPendientes(string NombreBD, DropDownList DDL)
        {
            string[] DetallesSol = null;
            if (!IsPostBack)
            {
                DDL.Items.Clear();
                DataSet DSBaseDatos = new DataSet();
                DataSet Maestros = new DataSet();
                string DetallesSolicitud = "";
                string idMaestrosSolicitud = "";
                string[] idMaestroSol;
                List<string> Resultado = new List<string>();
                string LineaMaestro = "";
                string SQLNumDocAccion = "Select a.NumDocumentoAccion From Vista_AlistosPendientes a";
                DSBaseDatos = n_ConsultaDummy.GetDataSet(SQLNumDocAccion, "0");
                if (DSBaseDatos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                    {
                        string NumDocumentoAccion = dsRowEvento["NumDocumentoAccion"].ToString();
                        DetallesSolicitud += NumDocumentoAccion + ";";
                    }
                    DetallesSol = DetallesSolicitud.Split(';');
                    for (int contador = 0; contador < DetallesSol.Length - 1; contador++)
                    {
                        idMaestrosSolicitud += idMaestro(DetallesSol[contador].ToString()).Trim() + ";";
                    }
                    idMaestrosSolicitud = idMaestrosSolicitud.Substring(0, idMaestrosSolicitud.Length - 1).Trim();
                    idMaestroSol = idMaestrosSolicitud.Split(';');
                    idMaestroSol = RemoveDuplicates(idMaestroSol);
                    for (int contador = 0; contador < idMaestroSol.Length; contador++)
                    {
                        if (!idMaestroSol[contador].Equals(""))
                        {
                            string SQLLineaMaestro = "Select a.idMaestroSolicitud , a.idUsuario , a.Comentarios , a.Nombre FROM OPESALMaestroSolicitud a Where a.idMaestroSolicitud  = " + idMaestroSol[contador];
                            Maestros = n_ConsultaDummy.GetDataSet(SQLLineaMaestro, "0");
                            foreach (DataRow dsRowEvento in Maestros.Tables[0].Rows)
                            {
                                string dtidMaestroSolicitud = dsRowEvento["idMaestroSolicitud"].ToString();
                                string dtidUsuario = dsRowEvento["idUsuario"].ToString();
                                string dtComentarios = dsRowEvento["Comentarios"].ToString();
                                string dtNombre = dsRowEvento["Nombre"].ToString();
                                LineaMaestro = "Solicitud: " + dtidMaestroSolicitud + " Comentarios: " + dtComentarios + " Nombre: " + dtNombre;
                                //ddlAlistos.Items.Add(LineaMaestro);
                            }
                        }
                    }
                    //ddlAlistos.Items.Insert(0, new ListItem("--Seleccionar--"));
                }
            }
            //ddlAlistos.DataBind();           
        }
        /*
        private void InfoAprobarAlisto()
        {
            
            try
            {
                string TramaInfoAlisto = ddDetalleSolicitud.SelectedItem.Text.ToString();
                string[] ARInfoAlisto = TramaInfoAlisto.Split('|');
                txtidLineaDetalleSolicitud.Text = ARInfoAlisto[1].ToString().Trim();
                string SqlIdRegistro = "  Select * From TRAIngresoSalidaArticulos a where  NumDocumentoAccion = '" + txtidLineaDetalleSolicitud.Text + "'";
                string idRegistro = n_ConsultaDummy.GetUniqueValue(SqlIdRegistro, "0");
                if (!String.IsNullOrEmpty(idRegistro))
                {
                    txtRegistro.Text = idRegistro;
                }
                //txtidLineaDetalleSolicitud.Text = ARInfoAlisto[4].ToString().Trim();
            }
            catch (Exception)
            {

            }
        }

        protected void ddDetalleSolicitud_SelectedIndexChanged1(object sender, EventArgs e)
        {
            InfoAprobarAlisto();
        }

        private void CargarRelSolDetalle(string item)
        {
           /* 
            try
            {
                ddDetalleSolicitud.Items.Clear();
                if (!ddlAlistos.SelectedItem.Text.ToString().Equals("--Seleccionar--"))
                {
                    DataSet DtDetalleAlisto = new DataSet();
                    string TramaMaestroSolicitud = ddlAlistos.SelectedItem.Value.ToString();
                    string[] InfoMaestroSol = TramaMaestroSolicitud.Split(' ');
                    string idSolicitud = InfoMaestroSol[1].ToString().Trim();
                    string[] SQLDetallesAlisto = CargaDetalleSol;
                    string aux = "";

                    for (int contador = 0; contador < SQLDetallesAlisto.Length - 1; contador++)
                    {
                        int idDetalle = Convert.ToInt32(SQLDetallesAlisto[contador]);
                        string SqlDetalleAlisto = "SELECT a.idLineaDetalleSolicitud , a.Nombre as DescripcionSolicitud , a.idMaestroSolicitud , " +
                              "c.Nombre as NombreDestino , b.Nombre as NombreArticulo , a.Cantidad  FROM OPESALDetalleSolicitud a , ADMMaestroArticulo b , "
                              + "ADMDestino c WHERE a.idArticulo = b.idArticulo and a.idDestino = c.idDestino and idLineaDetalleSolicitud = " + idDetalle
                              + " and idMaestroSolicitud = " + idSolicitud;

                        DtDetalleAlisto = n_ConsultaDummy.GetDataSet2(SqlDetalleAlisto, "0");

                        foreach (DataRow dsRowEvento in DtDetalleAlisto.Tables[0].Rows)
                        {
                            string dtidLineaDetalleSolicitud = dsRowEvento["idLineaDetalleSolicitud"].ToString();
                            string dtDescripcionSolicitud = dsRowEvento["DescripcionSolicitud"].ToString();
                            string dtidMaestroSolicitud = dsRowEvento["idMaestroSolicitud"].ToString();
                            string dtNombreDestino = dsRowEvento["NombreDestino"].ToString();
                            string dtNombreArticulo = dsRowEvento["NombreArticulo"].ToString();
                            string dtCantidad = dsRowEvento["Cantidad"].ToString();
                            aux = "|" + dtidLineaDetalleSolicitud + "|" + dtNombreArticulo;
                            //aux = "Solicitud: " + dtidMaestroSolicitud + " Detalle Solicitud: " + dtidLineaDetalleSolicitud + " Descripción: " + dtDescripcionSolicitud
                            //+ " Destino: " + dtNombreDestino + "Articulo: " + dtNombreArticulo
                            //+ " Cantidad: " + dtCantidad + "Id Registro " + idRegistro;
                            ddDetalleSolicitud.Items.Add(aux);
                            txtidMaestroSolicitud.Text = idSolicitud;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
             * 
             * /
        }


        protected void ddlAlistos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRelSolDetalle(ddlAlistos.SelectedValue.ToString());
        }
      */
        protected void ddlAlistos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string LineaDetalle = ddlAlistos.SelectedValue.ToString();
        }
      
        //}
        #endregion //Despliegue de alistos

       
    }
}