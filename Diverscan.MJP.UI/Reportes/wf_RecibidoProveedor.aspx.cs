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
using Diverscan.MJP.Negocio.MotorDecisiones;
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
using CrystalDecisions.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.Negocio;
using System.IO;
using OfficeOpenXml;
using Diverscan.MJP.Negocio.Reportes;

namespace Diverscan.MJP.UI.Reports
{
    public partial class wf_RecibidoProveedor : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        // VarlorCantidadSeleccionada se usa para guardar el valor cuando se pasa de RadListBox1 a RadListBox2
        double VarlorCantidadSeleccionada = 0;
        // IdArticuloSeleccionado se usa para guardar el idarticulo cuando se pasa de RadListBox2 a RadListBox1
        string IdDetalleOrdenCompra = "";
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        static string StrConexionRep = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;  // usamos este string para obtener el usuario, 
                                                                                                                        // password, servidor y BD para poder desplegar 
                                                                                                                        // el reporte.
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        ReportDocument Documento = new ReportDocument(); // Se crea un nuevo ReportDocument.
        ParameterFieldDefinitions pfds;   // Contendra todos los objetos parámetros del informe.
        ParameterFieldDefinition pfd;     // representa un parametro de reporte.
        ParameterValues valores = new ParameterValues();   // objeto arreglo de valores del parametro.
        ParameterDiscreteValue valoresd = new ParameterDiscreteValue(); // El valor del parametro.  
        string nomrep = ConfigurationManager.AppSettings["RecProv"].ToString();  // nombre del reporte
        string dia, mes, año;

        string Serrep = ConfigurationManager.AppSettings["server"].ToString();
        string DBrep = ConfigurationManager.AppSettings["bd"].ToString();
        string Usrep = ConfigurationManager.AppSettings["usuario"].ToString();
        string Pasrep = ConfigurationManager.AppSettings["contrasenia"].ToString();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            CargarAccionesPagina(Pagina);


            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }


            if (!IsPostBack)
            {
                CargarDDLS();
                //CargarUnidadesEmpaque();
                ////GetComboUnidadEmpaque();
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
                                //else if (ccc is CrystalReportViewer)
                                //{
                                //    if (Acciones.Exists(x => x.ObjetoFuente == ccc.ID))
                                //    {
                                //        CrystalReportViewer CRVi = (CrystalReportViewer)ccc;
                                //        CRVi.Visible = true;
                                //        //CRVi.PrintMode = Acciones.Find((x) => x.ObjetoFuente == ccc.ID);
                                //        //CRVi.  += new EventHandler(Accion);
                                //    }
                                //}
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
            catch (Exception Ex)
            {
                Mensaje("error", Ex.Message, "");
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
            rdDatosReporteProveedor.DataSource = dataProveedorReport;
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIDProveedor, e_TablasBaseDatos.TblProveedores(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdformatoexporta, e_TablasBaseDatos.TblFormatoexporta(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(DdlIdarticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                /*
               Msj = n_SmartMaintenance.CargarDDL(ddlidDestino0, e_TablasBaseDatos.TblDestino(), UsrLogged.IdUsuario);
               if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
               //n_SmartMaintenance.CargarDDLsHoras(Panel3);
              */
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Error 11231231" + ex.Message, "");
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
        /// Para que esto funcione el botón debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo botón
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void BtnAgregar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblDestinoRestriccionHorario(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void BtnEditar3_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblDestinoRestriccionHorario(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnAgregar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblDetalleSolicitud(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar2_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblDetalleSolicitud(), UsrLogged.IdUsuario);
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
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblMaestroSolicitud(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblMaestroSolicitud(), UsrLogged.IdUsuario);
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


        protected void btnGenera_Click(object sender, EventArgs e)
        {
            try
            {

                #region Validacion de campos

                bool seleccionoProveedor;
                bool seleccionoFechaIni;
                bool seleccionoFechafin;

                if (ddlIDProveedor.SelectedIndex == 0)
                { 
                    Mensaje("error", "Debe de seleccionar un Proveedor.", "");
                    seleccionoProveedor = false;
                }
                else { seleccionoProveedor = true; }

                if (RDPFechaInicial.SelectedDate == null)
                {
                    seleccionoFechaIni = false;
                    Mensaje("error", "Debe de seleccionar una Fecha de Inicio.", "");
                }
                else { seleccionoFechaIni = true; }

                if (RDPFechaFinal.SelectedDate == null)
                {
                    seleccionoFechafin = false;
                    Mensaje("error", "Debe de seleccionar una Fecha de Fin.", "");
                }
                else { seleccionoFechafin = true; }
                
                #endregion

                if (seleccionoProveedor && seleccionoFechaIni && seleccionoFechafin)
                {
                    if (btnGenera.Text == "Vista Previa")
                    {
                        string IdProvedor = "";
                        e_ProveedorReport report = new e_ProveedorReport();
                        DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                        DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
                        IdProvedor = ddlIDProveedor.SelectedValue.ToString();

                        var proveedorReportList = n_ProveedorReport.ObtenerProveedorReportInfo(fechaIni, fechaFin, IdProvedor);

                        
                        if (proveedorReportList.Count > 0)
                        {
                            rdDatosReporteProveedor.DataSource = proveedorReportList;
                            rdDatosReporteProveedor.DataBind();

                            BtnExporta.Enabled = true;
                            btnGenera.Text = "Reset";

                            dataProveedorReport = proveedorReportList;
                        }
                    }
                    else
                    {
                        rdDatosReporteProveedor.DataSource = null;
                        rdDatosReporteProveedor.DataBind();

                        // btnGenera.Enabled = false;
                        // BtnImprime.Enabled = false; 
                        BtnExporta.Enabled = false;
                        // LblExporta.Enabled = false;
                        //ddlIDProveedor.SelectedIndex = 0;
                        //ddlIDProveedor.Enabled = false;
                        btnGenera.Text = "Vista Previa";
                    }
                    
                }

            }
            catch (Exception)
            {
 
            }
        }


        protected void btnGenera_Click2(object sender, EventArgs e)
        {
            if (btnGenera.Text == "Vista Previa")
            {
                try
                {
                    ConectaReporte();  //  se carga el reporte y se conecta al servidor.
                    CRV.HasCrystalLogo = false;  // No pone el logo de Crystal Reports.
                    CRV.HasExportButton = false;  // No muestra el botón de Exportación.
                    CRV.HasPrintButton = false;  //  No muestra el botón de impresión.
                    CRV.HasToggleGroupTreeButton = false;  // No muestra el botón de  agrupamientos de arbol.
                    CRV.HasRefreshButton = false;  // No muestra botón refresh.
                    CRV.ToolPanelView = ToolPanelViewType.None;  // No muestra ningún panel.
                    CRV.Zoom(75);  // Muestra el reporte con un zoom del 75%.
                    CRV.ShowFirstPage();  // Presenta la primer página.

                    AplicaParametros();  // aplica los parametros elegidos en el webform

                    CRV.Visible = true;
                    CRV.ReportSource = Documento;  // Mostramos el Reporte

                    if (Documento.HasRecords)  // si el reporte no trae datos, no activa los botones imprimir ni exportar.
                    {
                        BtnImprime.Enabled = true;  // se habilita botón imprimir.
                        BtnExporta.Enabled = true;  // se habilita botón exportar.
                        LblExporta.Enabled = true;  // se habilita etiqueta exportar cómo.
                        ddlIdformatoexporta.Enabled = true;  // se habilita el combo de formatos a exportar.
                    }

                    btnGenera.Text = "Reset";  //  Cuando se termina de generar el reporte, el boton cambia de "función" y lo que hace es un reset al reporte, es decir,
                    //limpia todo lo relacionado con el reporte y el crystal report viewer.
                }
                catch (Exception ex)
                {
                    Mensaje("error", "OPS! Algo salio mal... " + ex.Message, "");
                }
            }
            else
            {
                // si el boton tiene el texto "Reset", limpia todo lo relacionado al reporte y al crystal report viewer, despues lo restaura a su función original.
                CRV.ReportSource = "";
                // BtnImprime.Enabled = false;  // se deshabilita botón imprimir.
                BtnExporta.Enabled = false;  // se deshabilita botón exportar.
                LblExporta.Enabled = false;  // se deshabilita etiqueta exportar cómo.
                ddlIdformatoexporta.SelectedIndex = 0;  // se pone el primer registro del ddl.
                ddlIdformatoexporta.Enabled = false;  // se deshabilita el combo de formatos a exportar.
                btnGenera.Text = "Vista Previa";
            }
        }


        #endregion //TabsControl

        protected void BtnImprime_Click(object sender, EventArgs e)
        {

            Lblmensaje.Visible = true;
            System.Windows.Forms.PrintDialog DlgImpr = new System.Windows.Forms.PrintDialog();  // cuadro de diálogo para elegir impresora.
            string nimpr = "";

            nimpr = DlgImpr.ShowDialog().ToString();  // Selecciona la impresora.
            if (nimpr == "OK")   // si el cuadro de diálogo devuelve un estatus OK, se elige el nombre de la impresora.
            {
                nimpr = DlgImpr.PrinterSettings.PrinterName;
                ConectaReporte();  //  se carga el reporte y se conecta al servidor.
                AplicaParametros();  // aplica los parametros elegidos en el webform

                Documento.PrintOptions.PrinterName = nimpr;  // asigna la impresora.
                CRV.ReportSource = Documento;  // muestra en pantalla lo que va a imprimir.
                Documento.PrintToPrinter(1, false, 0, 0);  // imprime el reporte.
            }

            return;
        }

        protected void BtnExporta_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validacion de campos

                bool seleccionoProveedor;
                bool seleccionoFechaIni;
                bool seleccionoFechafin;

                if (ddlIDProveedor.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe de seleccionar un Proveedor.", "");
                    seleccionoProveedor = false;
                }
                else { seleccionoProveedor = true; }

                if (RDPFechaInicial.SelectedDate == null)
                {
                    seleccionoFechaIni = false;
                    Mensaje("error", "Debe de seleccionar una Fecha de Inicio.", "");
                }
                else { seleccionoFechaIni = true; }

                if (RDPFechaFinal.SelectedDate == null)
                {
                    seleccionoFechafin = false;
                    Mensaje("error", "Debe de seleccionar una Fecha de Fin.", "");
                }
                else { seleccionoFechafin = true; }

                #endregion

                if (seleccionoProveedor && seleccionoFechaIni && seleccionoFechafin)
                {
                    string IdArticulo = "";
                    DataSet DSDatos = new DataSet();
                    e_ProveedorReport report = new e_ProveedorReport();
                    DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                    DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
                    IdArticulo = ddlIDProveedor.SelectedValue.ToString();

                    string SQL = "exec Obtener_ProveedorReportInfo @Fechaini='" + fechaIni + "',@Fechafin='" + fechaFin + "',@art=N'" + IdArticulo + "', @Exportar='1'";

                    DSDatos = n_ConsultaDummy.GetDataSet(SQL, "");

                    var rutaVirtual = "~/temp/" + string.Format("Comparativo Recibido Proveedor.xlsx");
                    var fileName = Server.MapPath(rutaVirtual);
                    Console.WriteLine(fileName);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    var newFile = new FileInfo(fileName);
                    using (ExcelPackage aplicacion = new ExcelPackage(newFile))
                    {
                        aplicacion.Workbook.Worksheets.Add("Hoja 1");
                        ExcelWorksheet hoja_trabajo = aplicacion.Workbook.Worksheets[1];

                        for (int i = 0; i < DSDatos.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < DSDatos.Tables[0].Columns.Count; j++)
                            {
                                hoja_trabajo.Cells[i + 1, j + 1].Value = DSDatos.Tables[0].Rows[i][j].ToString();
                            }
                        }

                        hoja_trabajo.Cells["A1:Z10000"].AutoFitColumns();

                        aplicacion.Save();
                    }

                    Response.Redirect(rutaVirtual, false);
                }
            }
            catch(Exception ex)
            {
                new clErrores().escribirError(ex.Message, ex.StackTrace);
            }
        }

        protected void BtnExporta_Click2(object sender, EventArgs e)
        {
            string rutaexp = "D:\\Temp\\";  // definimos la ruta de la exportación.
            string nomarc = "RecibidoProveedor";  //  este sera el nombre del archivo a exportar.

            try
            {
                ConectaReporte();  //  se carga el reporte y se conecta al servidor.
                DiskFileDestinationOptions OpcImporta = new DiskFileDestinationOptions();  // se definen los objetos para el destino de la exportación en disco.
                ExportOptions OpcExporta = new ExportOptions();  // se define el objeto para las opciones de exportación.

                OpcExporta.ExportDestinationOptions = OpcImporta;

                // este bloque es para elejir el tipo de formato a exportar y el nombre y extención que se le pondra al archivo.
                switch (int.Parse(ddlIdformatoexporta.SelectedValue.ToString()))
                {
                    case 1:
                        PdfFormatOptions Pfo = new PdfFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.PortableDocFormat;
                        nomarc += ".pdf";
                        break;

                    case 2:
                        ExcelFormatOptions EFO = new ExcelFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.Excel;
                        nomarc += ".xls";
                        break;

                    case 3:
                        PdfRtfWordFormatOptions Wfo = new PdfRtfWordFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.WordForWindows;
                        nomarc += ".doc";
                        break;

                    case 4:
                        PdfRtfWordFormatOptions Rfo = new PdfRtfWordFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.RichText;
                        nomarc += ".rtf";
                        break;

                    case 5:
                        CharacterSeparatedValuesFormatOptions Cfo = new CharacterSeparatedValuesFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.CharacterSeparatedValues;
                        nomarc += ".csv";
                        break;

                    case 8:
                        OpcExporta.ExportFormatType = ExportFormatType.CrystalReport;
                        nomarc += ".rpt";
                        break;

                    case 9:
                        TextFormatOptions Tfo = new TextFormatOptions();
                        OpcExporta.ExportFormatType = ExportFormatType.Text;
                        nomarc += ".txt";
                        break;
                }

                AplicaParametros();  // aplica los parametros elegidos en el webform

                OpcExporta.ExportDestinationType = ExportDestinationType.DiskFile;  // se define el tipo de destino de exportación.
                OpcImporta.DiskFileName = rutaexp + nomarc; // se le asigna la ruta para importación.
                CRV.ReportSource = Documento;
                Documento.Export(OpcExporta);  // se ejecuta la importación.  OpcExporta
            }

            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }

            string rtamnsg = "D:/Temp/" + nomarc;  // se le pone la ruta de exportación de esta manera porque al ponerlo en el mensaje sale todo pegado.
            Mensaje("ok", "Archivo Exportado cómo:" + rtamnsg, "");
        }

        private void AplicaParametros()
        {
            valores.Clear();

            // aqui pasamos el valor del primer parametro: Idproveedor.
            valoresd.Value = int.Parse(ddlIDProveedor.SelectedValue.ToString());  // Toma el valor del parámetro.
            pfds = Documento.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            pfd = pfds["Idproveedor"];  // Crea el parametro.
            valores.Add(valoresd); // Añade el valor del parametro.
            pfd.ApplyCurrentValues(valores); // Aplica el parametro al reporte.

            // aqui pasamos el valor del segundo parametro: Fechaini.
            año = RDPFechaInicial.SelectedDate.Value.Year.ToString();
            mes = RDPFechaInicial.SelectedDate.Value.Month.ToString();
            dia = RDPFechaInicial.SelectedDate.Value.Day.ToString();
            mes = (int.Parse(mes) < 10 ? "0" + mes : mes); // evaluamos si el mes tiene menos de 2 dijitos, le añadimos el "0".
            dia = (int.Parse(dia) < 10 ? "0" + dia : dia); // evaluamos si el día tiene menos de 2 dijitos, le añadimos el "0".
            valoresd.Value = (char)39 + año + mes + dia + (char)39;  // Toma el valor del parámetro. ('aaaammdd')
            pfds = Documento.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            pfd = pfds["Fechaini"];  // Crea el parametro.
            valores.Add(valoresd);  // Añade el valor del parametro.
            pfd.ApplyCurrentValues(valores); // Aplica el parametro al reporte.

            // aqui pasamos el valor del tercer parametro: Fechafin.
            RDPFechaFinal.SelectedDate = RDPFechaFinal.SelectedDate.Value.AddDays(1);  // se le agrega un día más para que tome el ultima día en el reporte.
            año = RDPFechaFinal.SelectedDate.Value.Year.ToString();
            mes = RDPFechaFinal.SelectedDate.Value.Month.ToString();
            dia = RDPFechaFinal.SelectedDate.Value.Day.ToString();
            mes = (int.Parse(mes) < 10 ? "0" + mes : mes); // evaluamos si el mes tiene menos de 2 dijitos, le añadimos el "0".
            dia = (int.Parse(dia) < 10 ? "0" + dia : dia); // evaluamos si el día tiene menos de 2 dijitos, le añadimos el "0".
            valoresd.Value = (char)39 + año + mes + dia + (char)39;  // Toma el valor del parámetro. ('aaaammdd')
            pfds = Documento.DataDefinition.ParameterFields;   // establece los parámetros del reporte.
            pfd = pfds["Fechafin"];  // Crea el parametro.
            valores.Add(valoresd);  // Añade el valor del parametro.
            pfd.ApplyCurrentValues(valores);  // Aplica el parametro al reporte.
            RDPFechaFinal.SelectedDate = RDPFechaFinal.SelectedDate.Value.AddDays(-1);  // ponemos la fecha original.

            // aqui pasamos el valor del cuarto parametro: Artic.
            valoresd.Value = DdlIdarticulo.SelectedValue.ToString() == "--Seleccionar--" ? 0 : int.Parse(DdlIdarticulo.SelectedValue.ToString());  // si no eligió nada, pone 00.
            pfds = Documento.DataDefinition.ParameterFields;   // establece los parámetros del reporte.
            pfd = pfds["Artic"];  // Crea el parametro.
            valores.Add(valoresd);  // Añade el valor del parametro.
            pfd.ApplyCurrentValues(valores);   // Aplica el parametro al reporte.
        }

        private void ConectaReporte()
        {
            Documento.Load(Server.MapPath(nomrep));  // Asignamos la Ruta del reporte.
            Documento.SetDatabaseLogon(Usrep, Pasrep, Serrep, DBrep);  //  accesamos al servidor.
        }

        List<e_ProveedorReport> dataProveedorReport
        {
            get
            {
                var data = ViewState["DataProveedorReport"] as List<e_ProveedorReport>;
                if (data == null)
                    return new List<e_ProveedorReport>();
                return data;
            }
            set
            {
                ViewState["DataProveedorReport"] = value;
            }
        }
    }
}