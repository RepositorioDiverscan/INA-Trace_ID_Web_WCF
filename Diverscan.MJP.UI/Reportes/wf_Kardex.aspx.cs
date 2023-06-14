using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Threading;
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
using Diverscan.MJP.Negocio.Reportes;
using System.IO;
using OfficeOpenXml;

namespace Diverscan.MJP.UI.Reports
{
    public partial class wf_Kardex : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        
        //public int ToleranciaAgregar = 80;
        string Pagina = "";
        string dia, mes, año;

        string RutaTemp = ConfigurationManager.AppSettings["RutaTemp"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            

            //CargarAccionesPagina(Pagina);


            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }


            if (!IsPostBack)
            {
                loadArticulos();
                CargarDDLS();
                //CargarUnidadesEmpaque();
                ////GetComboUnidadEmpaque();
            }


           //Panel3.m
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
                                        //Btn.Click += new EventHandler(Accion);
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
        }

        void UpdatePanel1_Unload(object sender, EventArgs e)
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
            rdDatosReporte.DataSource = dataKardexFiltrado;
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidERP, e_TablasBaseDatos.VistaArticulosSAP(), UsrLogged.IdUsuario,true);
                 //n_SmartMaintenance.CargarDDL(ddlIDArticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //var Msj = n_SmartMaintenance.CargarDDL(ddlIdformatoexporta, e_TablasBaseDatos.TblFormatoexporta(), UsrLogged.IdUsuario, false);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdmetodoaccion, e_TablasBaseDatos.VistaMetodoAccion(), UsrLogged.IdUsuario, false);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
               // //Msj = 
               //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
               //n_SmartMaintenance.CargarDDLsHoras(Panel3);
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
        /*
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
       */
        #endregion //EventosFrontEnd

         #endregion //TabsControl

        public void btnGenera_Click(object sender, EventArgs e)
        {
            string Idarticuloevalua = "";
            string SQL = "";
            DataSet DB = new DataSet();
            try
            {

                #region Validacion de Campos

                bool seleccionoArticulo;
                bool seleccionoFechaIni;
                bool seleccionoFechaFin;

                if (ddlIDArticulo.SelectedIndex == 0)
                {
                    Mensaje("error", "Debe de seleccionar un Artículo.", "");
                    seleccionoArticulo = false;
                }
                else { seleccionoArticulo = true; }

                if (RDPFechaInicial.SelectedDate == null)
                {
                    Mensaje("error", "Debe de seleccionar una Fecha de Inicio.", "");
                    seleccionoFechaIni = false;
                }
                else { seleccionoFechaIni = true; }

                if (RDPFechaFinal.SelectedDate == null)
                {
                    Mensaje("error", "Debe de seleccionar una Fecha de Fin.", "");
                    seleccionoFechaFin = false;
                }
                else { seleccionoFechaFin = true; }

                #endregion
               
                if (seleccionoArticulo && seleccionoFechaIni && seleccionoFechaFin)
                {
                    if (btnGenera.Text == "Vista Previa")
                    {
                        e_KardexReport report = new e_KardexReport();
                        DateTime fechaIni = RDPFechaInicial.SelectedDate.Value;
                        DateTime fechaFin = RDPFechaFinal.SelectedDate.Value;

                        if (ddlIDArticulo.SelectedValue.ToString() == "--Seleccionar--")
                            Idarticuloevalua = ddlidERP.SelectedValue.ToString();
                        else
                            Idarticuloevalua = ddlIDArticulo.SelectedValue.ToString();

                        var KardexReportList = n_KardexReport.ObtenerKardexReportInfo(fechaIni, fechaFin, Idarticuloevalua);

                        if (KardexReportList.Count > 0)
                        {
                            rdDatosReporte.DataSource = KardexReportList;
                            rdDatosReporte.DataBind();
                            // BtnImprime.Enabled = true;
                            btnGenera.Enabled = true;
                            BtnExporta.Enabled = true;
                            LblExporta.Enabled = true;
                            //ddlIdformatoexporta.Enabled = true;
                            btnGenera.Text = "Reset";
                            dataKardex = KardexReportList;
                            dataKardexFiltrado = dataKardex;
                            TxtSaldoGlobal.Visible = false;
                            TxtUnidad.Visible = false;
                            LblSaldoGlobal.Visible = false;
                            TxtSaldoGlobal.ReadOnly = false;
                            TxtUnidad.ReadOnly = false;
                            TxtSaldoGlobal.Text = "";
                            TxtUnidad.Text = ""; 
                        }
                        else
                        {
                          Mensaje("info", "No se encontraron datos que presentar", "");
                        }

                        SQL = "SELECT vDXA.DISPONIBLES * MA.PesoKilos AS Disponible,SUBSTRING(MA.Unidad_Medida,CHARINDEX('-',MA.Unidad_Medida)+1,20) " +
                                "  FROM Vista_DisponibleXidArticulo         AS vDXA" +
                                "    INNER JOIN TRaceid..ADMMaestroArticulo AS MA ON (vDXA.idArticulo = MA.idArticulo)" +
                                "  WHERE vDXA.idArticulo = " + Idarticuloevalua;
                        DB = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                        TxtSaldoGlobal.Visible = true;
                        TxtUnidad.Visible = true;
                        LblSaldoGlobal.Visible = true;
                        if (DB.Tables[0].Rows.Count > 0)
                        {
                            TxtSaldoGlobal.Text = String.Format("{0:###,###,###.####}", DB.Tables[0].Rows[0][0].ToString());
                            TxtUnidad.Text = DB.Tables[0].Rows[0][1].ToString();
                        }
                        else
                        {
                            TxtSaldoGlobal.Text = "0.00";
                            TxtUnidad.Text = "---";
                        }
                        TxtSaldoGlobal.ReadOnly = true;
                        TxtUnidad.ReadOnly = true;

                    }
                    else
                    {

                        rdDatosReporte.DataSource = null;
                        rdDatosReporte.DataBind();

                        // btnGenera.Enabled = false;
                        // BtnImprime.Enabled = false; 
                        BtnExporta.Enabled = false;
                        LblExporta.Enabled = false;
                        //ddlIdformatoexporta.SelectedIndex = 0;
                        //ddlIdformatoexporta.Enabled = false;
                        TxtSaldoGlobal.Visible = false;
                        TxtUnidad.Visible = false;
                        LblSaldoGlobal.Visible = false;
                        TxtSaldoGlobal.ReadOnly = false;
                        TxtUnidad.ReadOnly = false;
                        TxtSaldoGlobal.Text = "";
                        TxtUnidad.Text = ""; 
                        btnGenera.Text = "Vista Previa";
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "OPS! Algo salio mal... " + ex.Message, "");
            }
        }

        protected void BtnImprime_Click(object sender, EventArgs e)
        {
           
            Lblmensaje.Visible = true;
            System.Windows.Forms.PrintDialog DlgImpr = new System.Windows.Forms.PrintDialog();  // cuadro de diálogo para elegir impresora.
            string nimpr = "";

            nimpr = DlgImpr.ShowDialog().ToString();  // Selecciona la impresora.
            if (nimpr == "OK")   // si el cuadro de diálogo devuelve un estatus OK, se elige el nombre de la impresora.
            {
                nimpr = DlgImpr.PrinterSettings.PrinterName;
                //ConectaReporte();  //  se carga el reporte y se conecta al servidor.
                //AplicaParametros();  // aplica los parametros elegidos en el webform

                e_Reportes.DocumentoReporte.PrintOptions.PrinterName = nimpr;  // asigna la impresora.
                //CRV.ReportSource = e_Reportes.DocumentoReporte;  // muestra en pantalla lo que va a imprimir.
                e_Reportes.DocumentoReporte .PrintToPrinter(1, false, 0, 0);  // imprime el reporte.
            }

             return;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_KardexReport));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[14];
                propertiesSelected[0] = properties.Find("ArticuloSAP", false);
                propertiesSelected[1] = properties.Find("Articulo", false);
                propertiesSelected[2] = properties.Find("Motivo", false);
                propertiesSelected[3] = properties.Find("SaldoInicial", false);
                propertiesSelected[4] = properties.Find("Unid_inventario", false);
                propertiesSelected[5] = properties.Find("Saldo", false);
                propertiesSelected[6] = properties.Find("Saldo_actual", false);
                propertiesSelected[7] = properties.Find("Zona", false);
                propertiesSelected[8] = properties.Find("Lote", false);
                propertiesSelected[9] = properties.Find("FechaVencimientoExport", false);
                propertiesSelected[10] = properties.Find("OCDestino", false);
                propertiesSelected[11] = properties.Find("Peso", false);
                propertiesSelected[12] = properties.Find("FecharegistroExport", false);
                propertiesSelected[13] = properties.Find("Unidad", false);

                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/temp/" + string.Format("Kardex.xlsx");
                var fileName = Server.MapPath(rutaVirtual);
                //fileName =(rutaVirtual);
                List<string> headers = new List<string>() { "idArticuloERP", "Artículo", "Motivo", "Saldo inicial", "Cantidad", "Saldo",
                    "Saldo Actual Global","Zona", "Lote", "Fecha Vencimiento", "Documento_Destino", "Peso", "FechaRegistro", "Unidad de Medida" };
                ExcelExporter.ExportData(this.dataKardexFiltrado, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
                Mensaje("ok", "Proceso Exitoso-" + fileName, "");
                
            }
            catch (Exception ex)
            {
                Mensaje("error", "Proceso NO Exitoso-" + ex.Message, "");
            }           
        }

        private void AplicaParametros()
        {
            string Idarticuloevalua = "";
            e_Reportes.valores.Clear();

            // aqui pasamos el valor del primer parametro: Artículo.
            if (ddlIDArticulo.SelectedValue.ToString() == "--Seleccionar--")
                Idarticuloevalua = ddlidERP.SelectedValue.ToString();
            else
                Idarticuloevalua = ddlIDArticulo.SelectedValue.ToString();
            
            e_Reportes.valor.Value = int.Parse(Idarticuloevalua);  // Toma el valor del parámetro.
            e_Reportes.Parametros = e_Reportes.DocumentoReporte.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            e_Reportes.Parametro = e_Reportes.Parametros["Artic"];  // Crea el parametro.
            e_Reportes.valores.Add(e_Reportes.valor); // Añade el valor del parametro.
            e_Reportes.Parametro.ApplyCurrentValues(e_Reportes.valores); // Aplica el parametro al reporte.

            // aqui pasamos el valor del segundo parametro: Fechaini.
            año = RDPFechaInicial.SelectedDate.Value.Year.ToString();
            mes = RDPFechaInicial.SelectedDate.Value.Month.ToString();
            dia = RDPFechaInicial.SelectedDate.Value.Day.ToString();
            mes = (int.Parse(mes) < 10 ? "0" + mes : mes); // evaluamos si el mes tiene menos de 2 dijitos, le añadimos el "0".
            dia = (int.Parse(dia) < 10 ? "0" + dia : dia); // evaluamos si el día tiene menos de 2 dijitos, le añadimos el "0".
            e_Reportes.valor.Value = (char)39 + año + mes + dia + (char)39;  // Toma el valor del parámetro. ('aaaammdd')
            e_Reportes.Parametros = e_Reportes.DocumentoReporte.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            e_Reportes.Parametro = e_Reportes.Parametros["Fechaini"];  // Crea el parametro.
            e_Reportes.valores.Add(e_Reportes.valor);  // Añade el valor del parametro.
            e_Reportes.Parametro.ApplyCurrentValues(e_Reportes.valores); // Aplica el parametro al reporte.

            // aqui pasamos el valor del tercer parametro: Fechafin.
            RDPFechaFinal.SelectedDate = RDPFechaFinal.SelectedDate.Value.AddDays(1);  // se le agrega un día más para que tome el ultima día en el reporte.
            año = RDPFechaFinal.SelectedDate.Value.Year.ToString();
            mes = RDPFechaFinal.SelectedDate.Value.Month.ToString();
            dia = RDPFechaFinal.SelectedDate.Value.Day.ToString();
            mes = (int.Parse(mes) < 10 ? "0" + mes : mes); // evaluamos si el mes tiene menos de 2 dijitos, le añadimos el "0".
            dia = (int.Parse(dia) < 10 ? "0" + dia : dia); // evaluamos si el día tiene menos de 2 dijitos, le añadimos el "0".
            e_Reportes.valor.Value = (char)39 + año + mes + dia + (char)39;  // Toma el valor del parámetro. ('aaaammdd')
            e_Reportes.Parametros = e_Reportes.DocumentoReporte.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            e_Reportes.Parametro = e_Reportes.Parametros["Fechafin"];  // Crea el parametro.
            e_Reportes.valores.Add(e_Reportes.valor);  // Añade el valor del parametro.
            e_Reportes.Parametro.ApplyCurrentValues(e_Reportes.valores); // Aplica el parametro al reporte.
            RDPFechaFinal.SelectedDate = RDPFechaFinal.SelectedDate.Value.AddDays(-1);  // ponemos la fecha original.

            // aqui pasamos el valor del cuarto parametro: Idmetodoaccion.
            e_Reportes.valor.Value = ddlIdmetodoaccion.SelectedValue.ToString() == "--Seleccionar--" ? 0 : int.Parse(ddlIdmetodoaccion.SelectedValue.ToString());  // si no eligió nada, pone 00.
            e_Reportes.Parametros = e_Reportes.DocumentoReporte.DataDefinition.ParameterFields;  // establece los parámetros del reporte.
            e_Reportes.Parametro = e_Reportes.Parametros["Idmetodoaccion"];  // Crea el parametro.
            e_Reportes.valores.Add(e_Reportes.valor); // Añade el valor del parametro.
            e_Reportes.Parametro.ApplyCurrentValues(e_Reportes.valores); // Aplica el parametro al reporte.
        }

        private void ConectaReporte()
        {
            e_Reportes.DocumentoReporte.Load(Server.MapPath(e_Reportes.nomrep));  // Asignamos la Ruta del reporte.
            e_Reportes.DocumentoReporte.SetDatabaseLogon(e_Reportes.Usrep, e_Reportes.Pasrep, e_Reportes.Serrep, e_Reportes.DBrep);  //  accesamos al servidor.
        }

        private void InicializaReporte(bool Inicia = true)
        {
            if (Inicia)
            {
                e_Reportes.nomrep = ConfigurationManager.AppSettings["Kardex"].ToString();
                e_Reportes.Usrep = ConfigurationManager.AppSettings["usuario"].ToString();
                e_Reportes.Pasrep = ConfigurationManager.AppSettings["contrasenia"].ToString();
                e_Reportes.Serrep = ConfigurationManager.AppSettings["server"].ToString();
                e_Reportes.DBrep = ConfigurationManager.AppSettings["bd"].ToString();

                e_Reportes.DocumentoReporte = new ReportDocument(); // Se crea un nuevo ReportDocument.
                e_Reportes.valores = new ParameterValues();         // objeto arreglo de valores del parametro.
                e_Reportes.valor = new ParameterDiscreteValue();   // El valor del parametro.  
            }
            else
            {
               // se limpian todas las propiedades del objeto e_reportes.
                e_Reportes.nomrep = "";
                e_Reportes.Usrep = "";
                e_Reportes.Pasrep = "";
                e_Reportes.Serrep = "";
                e_Reportes.DBrep = "";

                e_Reportes.DocumentoReporte = null; // Se crea un nuevo ReportDocument.
                e_Reportes.valores = null;          // objeto arreglo de valores del parametro.
                e_Reportes.valor = null;            // El valor del parametro.
                e_Reportes.Parametros = null;
                e_Reportes.Parametro = null;
            }
        }

        List<e_KardexReport> dataKardex
        {
            get
            {
                var data = ViewState["DataKardex"] as List<e_KardexReport>;
                if (data == null)
                    return new List<e_KardexReport>();
                return data;
            }
            set
            {
                ViewState["DataKardex"] = value;
            }
        }

        List<e_KardexReport> dataKardexFiltrado
        {
            get
            {
                var data = ViewState["DataKardexFiltrado"] as List<e_KardexReport>;
                if (data == null)
                {
                    return new List<e_KardexReport>();
                    ViewState["DataKardexFiltrado"] = data;
                }
                
                return data;
            }
            set
            {
                ViewState["DataKardexFiltrado"] = value;
            }
        }

        protected void ddlIdmetodoaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdmetodoaccion.SelectedValue == "--Seleccionar--")
            {
                rdDatosReporte.DataSource = dataKardex;
                rdDatosReporte.DataBind();
                dataKardexFiltrado = dataKardex;
            }
            else
            {
                var idMotivo = int.Parse(ddlIdmetodoaccion.SelectedValue);
                var filtrado = dataKardex.Where(x => x.IdMetodoAccion == idMotivo).ToList();
                dataKardexFiltrado = filtrado;
                rdDatosReporte.DataSource = filtrado;
                rdDatosReporte.DataBind();
            }
        }

        private void loadArticulos()
        {
            var articulos = n_ArticuloGTIN.ObtenerArticulos();
            ddlIDArticulo.DataTextField = "Nombre_GTIN";
            ddlIDArticulo.DataValueField = "IdArticulo";
            ddlIDArticulo.DataSource = articulos;
            ddlIDArticulo.DataBind();
        }
    }
}