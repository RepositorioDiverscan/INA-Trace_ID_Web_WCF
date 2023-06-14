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
using Diverscan.MJP.Negocio;
using System.IO;
using OfficeOpenXml;
using Diverscan.MJP.Negocio.Reportes;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class HistoricoDemanda : System.Web.UI.Page
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
        string nomrep = ConfigurationManager.AppSettings["HistoricoDemanda"].ToString();  // nombre del reporte
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
                RDPFechaInicial.SelectedDate = DateTime.Now;
                RDPFechaFinal.SelectedDate = DateTime.Now;
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
            rdDatosReporteHistoricoDemanda.DataSource = dataHistoricoDemanda;
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdformatoexporta, e_TablasBaseDatos.TblFormatoexporta(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(DdlIdarticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                /*
               Msj = n_SmartMaintenance.CargarDDL(ddlidDestino0, e_TablasBaseDatos.TblDestino(), UsrLogged.IdUsuario);
               if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
               //n_SmartMaintenance.CargarDDLsHoras(Panel3);
              */
                DdlIdarticulo.Items.Insert(1, new ListItem("Todos"));
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

                bool seleccionoArticulo;
                bool seleccionoFechaIni;
                bool seleccionoFechafin;

                if (DdlIdarticulo.SelectedIndex == 0)
                { 
                    Mensaje("error", "Debe de seleccionar un Proveedor.", "");
                    seleccionoArticulo = false;
                }
                else { seleccionoArticulo = true; }

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

                if (seleccionoArticulo && seleccionoFechaIni && seleccionoFechafin)
                {
                    if (btnGenera.Text == "Vista Previa")
                    {
                        string IdArticulo = "";
                        DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                        DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
                        IdArticulo = DdlIdarticulo.SelectedValue.ToString();

                        var HistoricoDemandaList = n_HistoricoDemanda.ObtenerReporteHistoricoDemanda(fechaIni, fechaFin,IdArticulo);//n_ProveedorReport.ObtenerProveedorReportInfo(fechaIni, fechaFin, IdDestino);


                        if (HistoricoDemandaList.Count > 0)
                        {
                            rdDatosReporteHistoricoDemanda.DataSource = HistoricoDemandaList;
                            rdDatosReporteHistoricoDemanda.DataBind();

                            BtnExporta.Enabled = true;
                            btnGenera.Text = "Reset";

                            dataHistoricoDemanda = HistoricoDemandaList;
                        }
                    }
                    else
                    {
                        rdDatosReporteHistoricoDemanda.DataSource = null;
                        rdDatosReporteHistoricoDemanda.DataBind();

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


       


        #endregion //TabsControl

        protected void BtnExporta_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validacion de campos

                bool seleccionoArticulo;
                bool seleccionoFechaIni;
                bool seleccionoFechafin;

                if (DdlIdarticulo.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe de seleccionar un Articulo.", "");
                    seleccionoArticulo = false;
                }
                else { seleccionoArticulo = true; }

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

                if (seleccionoArticulo && seleccionoFechaIni && seleccionoFechafin)
                {
                    string IdArticulo = "";
                    DataSet DSDatos = new DataSet();
                    e_ProveedorReport report = new e_ProveedorReport();
                    DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                    DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;
                    IdArticulo = DdlIdarticulo.SelectedValue.ToString();

                    string SQL = "exec Obtener_HistoricoDemandaExport @Fechaini='" + fechaIni + "',@Fechafin='" + fechaFin + "',@IdArticulo=N'" + IdArticulo + "'";

                    DSDatos = n_ConsultaDummy.GetDataSet(SQL, "");

                    DataRow dr = DSDatos.Tables[0].NewRow();
                    dr[0] = "Nombre Articulo";
                    dr[1] = "CodInterno";
                    dr[2] = "Año";
                    dr[3] = "Mes";
                    dr[4] = "Semana";
                    dr[5] = "Lunes";
                    dr[6] = "Martes";
                    dr[7] = "Miercoles";
                    dr[8] = "Jueves";
                    dr[9] = "Viernes";
                    dr[10] = "Sabado";
                    dr[11] = "Domingo";
                    dr[12] = "Total Semana";
                    dr[13] = "TotalLunes";
                    dr[14] = "TotalMartes";
                    dr[15] = "TotalMiercoles";
                    dr[16] = "TotalJueves";
                    dr[17] = "TotalViernes";
                    dr[18] = "TotalSabado";
                    dr[19] = "TotalDomingo";
                    DSDatos.Tables[0].Rows.InsertAt(dr, 0);

                    var rutaVirtual = "~/temp/" + string.Format("Reporte Historico Demanda.xlsx");
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

    


       

        List<e_HistoricoDemanda> dataHistoricoDemanda
        {
            get
            {
                var data = ViewState["DataHistoricoDemanda"] as List<e_HistoricoDemanda>;
                if (data == null)
                    return new List<e_HistoricoDemanda>();
                return data;
            }
            set
            {
                ViewState["DataHistoricoDemanda"] = value;
            }
        }
    
    }
}