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
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Diverscan.MJP.UI.Operaciones.Ingresos
{
    public partial class wf_UbicarArticulo : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        string Patron = "[ABCDEFGHIJKLMNÑOPRSTUVWXYZ|°¬!#$%&/()=?¡¨*_:;,´+¿'¬\\><-~{}`" + (char)34 + "]"; // patron de caracteres a evaluar en el Regex, para evaluar 
                                                                                                          // si en algún textbox hay letras, cuando se deben aceptar numeros.

        private enum EstadosOC
        {
            NoProcesada = 0
            , Procesada = 1
        }

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
                    //methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
                }
            }
        }

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
                CargarDDLS();
                txtCODBARRAS0.Attributes.Add("onchange", "DisplayLoadingImage3();");
                txtCODBARRAS.Attributes.Add("onchange", "DisplayLoadingImage4();");
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
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(DdlidMOC0, e_TablasBaseDatos.VistaMaestroOrdenCompraProveedor(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo0, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(DdlidMOC, e_TablasBaseDatos.VistaMaestroOrdenCompraProveedor(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(DdlidPregunta, e_TablasBaseDatos.VistaPreguntas(), UsrLogged.IdUsuario, false);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDLRespuestas(DdlidRespuesta, e_TablasBaseDatos.VistaRespuestas(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000005" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000006" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000007" + ex.Message, "");
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
        public void Accion(object sender, EventArgs e)
        {
            // botón para ver disponibilidad del artículo o los datos asociados al mismo en recepción o ubicación.
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, "btnAccion1");
            Mensaje("ok", resultado, "");
        }

        //private void ObtenerFechaVencimientoArtUbicacion(string idArticulo, DropDownList DDLFechaVencimiento)
        //{
        //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(10).Costa Rica:Grupo Diverscan. 
        //}

        //protected void ddlFechaVencimiento_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(11).Costa Rica:Grupo Diverscan. 
        //}


        #endregion //EventosFrontEnd

        #endregion //TabsControl

        //protected void btnAccion21_Click(object sender, EventArgs e)
        //{
        //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(12).Costa Rica:Grupo Diverscan. 
        //}

        protected void btnAccion32_Click(object sender, EventArgs e)
        {
            /*-----------------------*
              botón recibir artículo
             *-----------------------*/
            try
            {
                // primero se determina si la OC ya fue procesada.
                string EvaluaprocesarOC = "";
                EvaluaprocesarOC = DdlidMOC.SelectedItem.Text;
                int lengthEvaluaprocesarOC = EvaluaprocesarOC.Length;
                int IndiceOC = DdlidMOC.SelectedIndex;
                //if (EvaluaprocesarOC.Substring(lengthEvaluaprocesarOC-1) == "0")
                //{
                //    string Codleido = txtCODBARRAS0.Text + ";0;" + DdlidMOC.SelectedValue.ToString();
                //    string MensajeSP = n_WMS.InsertDetalleOrdenCompra(Codleido,  UsrLogged.IdUsuario, "0");
                //    CargarDDLS();
                //    DdlidMOC.SelectedIndex = IndiceOC;
                //}

                // se determina si el artículo es a granel o no.
                bool EsGRANEL = CargarEntidadesGS1.GS1128_EsArticuloGranel(txtCODBARRAS0.Text);

                // este if es para determinar si el articulo a recibir es a granel o no, pues la cantidad a recibir puede ser diferente a
                //  la que dice el código GS1. Si es asi se reconstruye el código y sigue el proceso de recepción.
                if (EsGRANEL)
                {
                    // los bloques if es para determinar si el textbox de cantidad, que es el unico que se modifica si el artículo es de granel, tiene un valor valido 
                    // (entero positivo)
                    if (txtCantidad0.Text == "Digite la cantidad" || string.IsNullOrEmpty(txtCantidad0.Text) || Regex.IsMatch(txtCantidad0.Text.ToUpper(), Patron))
                    {
                        Mensaje("error", "Debe poner una cantidad valida...", "");
                        return;
                    }
                    else if (Single.Parse(txtCantidad0.Text) <= 0.00)
                    {
                        Mensaje("error", "Debe poner una cantidad valida...", "");
                        return;
                    }

                    // Metodo que arma el nuevo código GS1.
                    string codleido = txtCODBARRAS0.Text + ";" + txtCantidad0.Text + ";" + DdlidMOC.SelectedValue.ToString();
                    string respuesta = n_WMS.ProcesarCodigoGS1ArticuloGranel(codleido, UsrLogged.IdUsuario);
                    string[] mensaje = respuesta.Split(';');

                    if (mensaje[0] == "0")
                    {
                        Mensaje("error", mensaje[1], "");
                        return;
                    }

                    txtCODBARRAS0.Text = mensaje[0];
                }

                // Recibe el artículo.
                string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                string CodLeido = txtCODBARRAS0.Text + ";OC" + DdlidMOC.SelectedValue.ToString();
                string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion32");
                string[] canti = Respuesta.Split('|');

                if (canti.Length > 1)
                {
                    string mensaerror = (canti[0].Replace("\n", "|| "));
                    mensaerror = mensaerror.Replace("'", "");
                    TxtTotal.Text = canti[1];
                    TxtPendiente.Text = canti[2];
                    TxtTotalRechazado.Text = canti[3];
                    Mensaje("ok", mensaerror, "");
                }
                else
                {
                    string mensaerror = (canti[0].Replace("\n", "|| "));
                    mensaerror = mensaerror.Replace("'", "");
                    Mensaje("ok", mensaerror, "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
            //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(13).Costa Rica:Grupo Diverscan.
            //string CodLeido = "select nombre from traceid.dbo.Vista_MaestroOrdenCompraProveedor"; 
        }

        protected void btnAccion23_Click(object sender, EventArgs e)
        {
            // botón ubicar artículo.??????
            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            string CodLeido = txtCODBARRAS.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion22");
            Mensaje("ok", Respuesta, "");
        }

        protected void btnAccion24_Click(object sender, EventArgs e)
        {
            // botón aprobar ubicación artículo.
            if (string.IsNullOrEmpty(txtUbicacionLeida.Text))
            {
                Mensaje("info", "Pistolee una ubicación válida...", "");
                return;
            }

            // valida si la ubicación existe.
            txtUbicacionLeida.Text = txtUbicacionLeida.Text.Trim();
            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            string CodLeido = txtUbicacionLeida.Text.Trim();
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion23");

            if (string.IsNullOrEmpty(Respuesta))
            {
                Mensaje("info", "Ubicación no válida o no pertenece a la compañía actual...", "");
                return;
            }

            // si la ubicación es valida, continúa el proceso.
            Pagina = "";
            CodLeido = "";
            Respuesta = "";

            Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            CodLeido = txtCODBARRAS.Text.Trim() + ";" + txtUbicacionLeida.Text.Trim();
            Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion22");

            if (Respuesta.Contains('|'))
            {
                string[] trama = Respuesta.Split('|');
                txtTotalArticulos.Text = trama[1].ToString();
                txtTotalArticulosPorUbicar.Text = trama[2].ToString();
                //TxtTotalRechazado 
                Mensaje("ok", (trama[0].Replace("\n", "|| ")), "");
            }
            else
            {
                string mensaerror = (Respuesta.Replace("\n", "|| "));
                mensaerror = mensaerror.Replace("'", "");
                Mensaje("info", mensaerror, "");
            }


        }

        protected void DdlidRespuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            // combo de respuestas en pestaña de control de calidad, si se elije NO, muestra un texto para poner un mensaje y el password del administrador.
            //string Prueba = DdlidRespuesta.SelectedItem.Text.ToString();
            //if (Prueba.Equals("No"))
            // {
            //     lblComment.Visible = true;
            //     txtComent.Visible = true;
            //     lblAdmin.Visible = true;
            //     txtPass.Visible = true;
            //     LbCodigoBarras.Visible = true;
            //     txtCodigosBarras1.Visible = true;
            //     LblLogin.Visible = true;
            //     LblPaswword.Visible = true;
            //     TxtLogin.Visible = true;
            // }
            //else
            //{
            //     lblComment.Visible = false;
            //     txtComent.Visible = false;
            //     lblAdmin.Visible = false;
            //     txtPass.Visible = false;
            //     LbCodigoBarras.Visible = false;
            //     txtCodigosBarras1.Visible = false;
            //     LblLogin.Visible = false;
            //     LblPaswword.Visible = false;
            //     TxtLogin.Visible = false;
            // }
        }

        protected void ddlidMaestroOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            // combo de ordenes de compra en pestaña de control de calidad, según las OC se cargan los artículos que pertenecen a esa OC.
            //string OC = DdlidMOC0.SelectedValue.ToString();
            //CargarNombreTablas(OC, ddlidArticulo0);
        }

        protected void DdlidMOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = DdlidMOC.SelectedIndex;
            if (index > 0)
                btnFinalizarOC.Enabled = true;
            else
                btnFinalizarOC.Enabled = false;

            //if (HabilitaRecepcion(Int64.Parse(DdlidMOC.SelectedValue)))
            btnAccion32.Enabled = true;
            //else
            //{
            //    Mensaje("info", "Formulario de Control de calidad no se ha contestado en su totalidad, no se puede recibir el producto.", "");
            //    btnAccion32.Enabled = true;
            //}

        }

        protected void btnFinalizarOC_Click(object sender, EventArgs e)
        {
            string text = DdlidMOC.SelectedItem.Text;
            string OCNumber = DdlidMOC.SelectedItem.Value;
            string mensaje = "No exitoso";

            try
            {

                if (HabilitaCierreOC(OCNumber))
                {

                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue.Length > 2)
                        confirmValue = confirmValue.Substring(confirmValue.Length - 2, 2);

                    if (confirmValue == "Si")
                    {
                        string SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroOrdenesCompra() +
                                     "  SET Procesada = " + (int)EstadosOC.Procesada + "," +
                                     "      FechaProcesamiento = GETDATE() " +
                                     "  WHERE idMaestroOrdenCompra = " + OCNumber + "";
                        bool result = n_ConsultaDummy.PushData(SQL, "");

                        if (result)
                        {
                            CargarDDLS();
                            // botón limpiar, recepción
                            Control Ctr = (Control)sender;
                            var Panel = Ctr.Parent.Parent.Parent;
                            n_SmartMaintenance.LimpiarForm(Panel);
                            mensaje = "Cierre de OC EXITOSO";
                        }
                        else
                        {
                            mensaje = "NO EXITOSO Cierre de OC";
                        }

                        Mensaje("ok", mensaje, "");
                        btnFinalizarOC.Enabled = false;
                    }
                }
                else
                    Mensaje("info", "Las preguntas del Formulario de Control de calidad no se han contestado en su totalidad, no se puede cerrar la Orden de Compra.", "");
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
        }

        private void CargarNombreTablas(String OC, DropDownList DDL)
        {
            DDL.Items.Clear();
            DataSet DSTablas = new DataSet();
            string SQL = "SELECT distinct Nombre,idarticulo FROM Vista_ArticulosOC WHERE idMaestroOrdenCompra ='" + OC + "'";
            DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            if (DSTablas.Tables[0].Rows.Count > 0)  // Carga los artículos de la OC.
            {
                DDL.DataSource = DSTablas;
                DDL.DataTextField = "Nombre";
                DDL.DataValueField = "idarticulo";
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
            }

            SQL = " SELECT DISTINCT A." + e_VistaPregunta.idPregunta() + ", A." + e_VistaPregunta.Pregunta() +
                        "   FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPreguntas() + " AS A" +
                        "   WHERE A." + e_VistaPregunta.VerFormulario() + " = 1 " +
                        "EXCEPT" +
                        " SELECT DISTINCT A." + e_VistaPregunta.idPregunta() + ", A." + e_VistaPregunta.Pregunta() +
                        "   FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPreguntas() + " AS A" +
                        "    FULL OUTER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblADMRespuestasFormulario() + " AS B ON (A." + e_VistaPregunta.idPregunta() + " = B. " + e_TblADMRespuestasFormulario.idPregunta() + ")" +
                        "   WHERE A." + e_VistaPregunta.VerFormulario() + " = 1" +
                        "         AND B." + e_TblADMRespuestasFormulario.OrdenCompra() + " = " + OC;

            //DdlidPregunta.Items.Clear();
            //DSTablas.Clear();
            //DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            //if (DSTablas.Tables[0].Rows.Count > 0)  // Carga las preguntas pendientes de la OC.
            //{
            //    DdlidPregunta.DataSource = DSTablas;
            //    DdlidPregunta.DataTextField = "Nombre";
            //    DdlidPregunta.DataValueField = "idPregunta";
            //    DdlidPregunta.DataBind();
            //    // if idpregunta > 5 btnAccion42.enabled = false;
            //}

            //DdlidPregunta.Items.Insert(0, new ListItem("--Seleccionar--"));


        }

        protected void btnAccion41_Click(object sender, EventArgs e)
        {
            // botón guardar respuesta
            //string codigoBarras = txtCodigosBarras1.Text;
            //string OrdenCompra = DdlidMOC0.Text;
            //string Articulo = ddlidArticulo0.Text;
            //string Pregunta = DdlidPregunta.Text;
            //string Resp = DdlidRespuesta.Text;
            //string Comentario = txtComent.Text;
            //string AdminPass = txtPass.Text;
            //string Temperatura = TxtTemperatura.Text;
            //string Vencimiento = "";
            //string SQL = "";

            //try
            //{
            //    if (Resp.Equals("1"))
            //    {
            //        TxtEvalua.Text = "True";
            //        Comentario = "";
            //    }

            //    if (Resp.Equals("2"))
            //    {
            //        TxtEvalua.Text = "False";
            //    }

            //    if (OrdenCompra.Equals("--Seleccionar--") || Articulo.Equals("--Seleccionar--") || Pregunta.Equals("--Seleccionar--") || Resp.Equals("--Seleccionar--"))
            //    {
            //        Mensaje("info", " Por favor seleccione una opción de: Orden de compra, Artículo, Pregunta o Respuesta, para continuar", "");
            //        return;
            //    }

            //    if (Resp.Equals("2") && (string.IsNullOrEmpty(Comentario)))  // Resp.Equals("2") && (string.IsNullOrEmpty(AdminPass)) ||
            //    {
            //        Mensaje("ok", "El campo Comentario no puede estar vacío", "");
            //        return;
            //    }

            //    else
            //    {
            //        string CodLeido = OrdenCompra + ";" + Articulo + ";" + Pregunta + ";" + Resp + ";" + Comentario + ";" + AdminPass + ";" + Temperatura + ";" + Vencimiento + ";" + TxtEvalua.Text + ";" + codigoBarras+ ";" +TxtLogin.Text;
            //        string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            //        string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion41");
            //        Mensaje("ok", Respuesta, "");

            //        string[] accion = Respuesta.Split(';');
            //        if (accion.Count() > 1)
            //        {
            //            if (accion[1] == "False")
            //            {
            //                txtComent.Visible = true;
            //                lblComment.Visible = true;
            //                txtComent.Text = accion[0] + " " + accion[2];
            //                txtPass.Text = "";
            //                lblAdmin.Visible = true;
            //                txtPass.Visible = true;
            //                DdlidRespuesta.SelectedIndex = 2;
            //                TxtEvalua.Text = "False";
            //            }

            //            return;
            //        }

            //            txtComent.Visible = false;
            //            txtComent.Text = "";
            //            lblComment.Visible = false;
            //            txtPass.Visible = false;
            //            txtPass.Text = "";
            //            lblAdmin.Visible = false;
            //            DdlidRespuesta.SelectedIndex = 0;
            //            TxtEvalua.Text = "True";
            //            TxtTemperatura.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Mensaje("error", "Ops! Ha ocurrido un Error, Código:TID-UI-OPE-ING-000006-" + ex.Message, "");
            //}

        }

        protected void btnAccion42_Click(object sender, EventArgs e)
        {
            // botón recibir todo a satisfacción.
            // string OrdenCompra = DdlidMOC0.Text;
            // string Articulo = ddlidArticulo0.Text;
            // string Pregunta = DdlidPregunta.Text;
            // string Resp = DdlidRespuesta.Text;
            // string Comentario = txtComent.Text;
            // string AdminPass = txtPass.Text;
            // string Respuesta = "";
            // string Temperatura = TxtTemperatura.Text;
            // TxtEvalua.Text = "False";
            // string Vencimiento = "";
            // Single Temp;

            // try
            // {
            //     Single.TryParse(Temperatura, out Temp);

            //     if (OrdenCompra.Equals("--Seleccionar--"))
            //     {
            //         Mensaje("info", " Por favor seleccione una opción de orden de compra para continuar", "");
            //         return ;
            //     }

            //     else if (Resp.Equals("2"))
            //     {
            //         Mensaje("info", "Para recibir todos los articulos, TODOS deben cumplir con condiciones satisfactorias (respuesta SI)", "");
            //         return;
            //     }

            //     else
            //     {
            //         for (int i = 1; i < ddlidArticulo0.Items.Count; i++)
            //{
            //           Articulo = ddlidArticulo0.Items[i].Value;

            //             for (int j = 1; j < DdlidPregunta.Items.Count-3; j++)
            //    {
            //                 Pregunta = DdlidPregunta.Items[j].Value;
            //                 Resp = DdlidRespuesta.Items[1].Value;

            //                 //string CodLeido = OrdenCompra + ";" + Articulo + ";" + Pregunta + ";" + Resp + ";" + Comentario + ";" + AdminPass + ";" + Temperatura + ";" + TxtEvalua.Text;
            //                 string CodLeido = OrdenCompra + ";" + Articulo + ";" + Pregunta + ";" + Resp + ";" + Comentario + ";" + AdminPass + ";" + Temperatura + ";" + Vencimiento + ";" + TxtEvalua.Text + ";0";
            //                 string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            //                 Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion41");
            //    }
            //}

            //          Mensaje("ok", Respuesta, "");
            //          txtComent.Visible = false;
            //          txtComent.Text = "";
            //          lblComment.Visible = false;
            //          txtPass.Visible = false;
            //          txtPass.Text = "";
            //          lblAdmin.Visible = false;
            //          DdlidRespuesta.SelectedIndex = 0;
            //          DdlidPregunta.SelectedIndex   = 0;
            //          DdlidMOC0.SelectedIndex = 0;
            //          ddlidArticulo0.SelectedIndex = 0; 
            //          TxtEvalua.Text = "True";
            //          TxtTemperatura.Text = "";
            //          btnAccion42.Enabled = false;
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000007" + ex.Message, "");
            // }
        }

        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void BtnLimpiar0_Click(object sender, EventArgs e)
        {
            // botón limpiar, recepción
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
            btnFinalizarOC.Enabled = false;
        }

        protected void BtnLimpiar1_Click(object sender, EventArgs e)
        {
            // botón limpiar, ubicación. 
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void BtnLimpiar3_Click(object sender, EventArgs e)
        {
            // botón limpiar, ubicación. 
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void btnArticulosSegunUbicacion_Click(object sender, EventArgs e)
        {
            // botón para ver artículos según ubicación.

            if ((txtEtiquetaUbicación.Text.Substring(0, 2) == "01") || (txtEtiquetaUbicación.Text.Substring(0, 2) == "02") || (txtEtiquetaUbicación.Text.Length == 13) || (txtEtiquetaUbicación.Text.Length == 14))  // disponibilidad para artículos.
            {
                string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, txtEtiquetaUbicación.Text, UsrLogged.IdUsuario, "btnAccion1");
                txtInfoArticulo.Text = resultado;
                Mensaje("ok", resultado, "");
            }

            if ((txtEtiquetaUbicación.Text.Substring(0, 2) == "91"))  // disponibilidad para ubicaciones.
            {
                string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, txtEtiquetaUbicación.Text, UsrLogged.IdUsuario, "btnArticulosSegunUbicacion");
                txtInfoArticulo.Text = resultado;
                Mensaje("ok", resultado, "");
            }
            else
            {
                Mensaje("info", "Código de aplicación no permitido en este módulo", "");
            }
        }

        protected void DdlidMOC0_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCODBARRAS0.Focus();
        }

        protected void txtCODBARRAS0_TextChanged(object sender, EventArgs e)
        {
            txtCODBARRAS0.Text = txtCODBARRAS0.Text.Trim();
            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
            string CodLeido = txtCODBARRAS0.Text;
            string Resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion24");
            string[] Opcion = Resultado.Split(';');

            //MuestraInfoGS1(sender, "btnAccion31");
        }

        private void MuestraInfoGS1(object boton, string Btnaccion)
        {
            // muestra info del artículo en recepción.
            Control Ctr = (Control)boton;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Btnaccion);

            string[] Elementos = resultado.Split(';');
            if (Elementos[0].Contains("EXITOSAMENTE"))
            {
                txtidArticulo0.Text = Elementos[1];
                txtNombre0.Text = Elementos[2]; // descripción del artículo.
                TxtidarticuloERP.Text = Elementos[3];
                txtCantidad0.Text = Elementos[4];  // cantidad que representa el GTIN.
                txtFechaVencimiento0.Text = Elementos[5];
                txtLote0.Text = Elementos[6];
            }

            if (txtCantidad0.Text == "Digite la cantidad")
            {
                this.txtCantidad0.Attributes.Add("onclick", "this.value = '' "); // se le agrega este atributo al textbox, para que cuando
                                                                                 // se haga clic en el textbox se borre su contenido.
            }
            else
            {
                this.txtCantidad0.Attributes.Add("onclick", "");
            }

            Mensaje("ok", Elementos[0], "");
        }

        protected void txtCODBARRAS_TextChanged(object sender, EventArgs e)
        {
            txtCODBARRAS.Text = txtCODBARRAS.Text.Trim();
            MuestraInfoGS1_UB(sender, "btnAccion21");
        }

        protected void btnAccion21_Click(object sender, EventArgs e)
        {
            MuestraInfoGS1(sender, "btnAccion21");
        }

        private void MuestraInfoGS1_UB(object boton, String Btnaccion)
        {
            // muestra info del artículo en ubicación.
            Control Ctr = (Control)boton;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Btnaccion);

            string[] Elementos = resultado.Split(';');
            if (Elementos[0].Contains("EXITOSAMENTE"))
            {
                txtidArticulo.Text = Elementos[1];
                txtNombre.Text = Elementos[2]; // descripción del artículo.
                TxtidarticuloERP0.Text = Elementos[3];
                txtCantidad.Text = Elementos[4];  // cantidad que representa el GTIN.
                txtFechaVencimiento.Text = Elementos[5];
                txtLote.Text = Elementos[6];
            }

            Mensaje("ok", Elementos[0], "");
        }

        protected void ddlidArticulo0_SelectedIndexChanged(object sender, EventArgs e)
        {
            // este metodo presenta las preguntas disponibles para este artículo.
            //string OC = DdlidMOC0.SelectedValue.ToString();
            //DataSet DSTablas = new DataSet();
            //string SQL = " SELECT DISTINCT A." + e_VistaPregunta.idPregunta() + ", A." + e_VistaPregunta.Pregunta() +
            //           "   FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPreguntas() + " AS A" +
            //           "   WHERE A." + e_VistaPregunta.VerFormulario() + " = 1 " +
            //           "EXCEPT" +
            //           " SELECT DISTINCT A." + e_VistaPregunta.idPregunta() + ", A." + e_VistaPregunta.Pregunta() +
            //           "   FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPreguntas() + " AS A" +
            //           "    FULL OUTER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblADMRespuestasFormulario() + " AS B ON (A." + e_VistaPregunta.idPregunta() + " = B. " + e_TblADMRespuestasFormulario.idPregunta() + ")" +
            //           "   WHERE A." + e_VistaPregunta.VerFormulario() + " = 1" +
            //           "         AND B." + e_TblADMRespuestasFormulario.OrdenCompra() + " = " + OC +
            //           "         AND B." + e_TblADMRespuestasFormulario.Articulo() + " = " + ddlidArticulo0.SelectedValue.ToString();

            //DdlidPregunta.Items.Clear();
            //DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            //if (DSTablas.Tables[0].Rows.Count > 0)  // Carga las preguntas pendientes de la OC.
            //{
            //    DdlidPregunta.DataSource = DSTablas;
            //    DdlidPregunta.DataTextField = "Nombre";
            //    DdlidPregunta.DataValueField = "idPregunta";
            //    DdlidPregunta.DataBind();
            //    // if idpregunta > 5 btnAccion42.enabled = false;
            //}

            //DdlidPregunta.Items.Insert(0, new ListItem("--Seleccionar--"));
        }

        private bool HabilitaRecepcion(Int64 OC)
        {
            string SQL = "DECLARE @ArticulosOC INT," +
                             "        @PreguntasCalidad INT," +
                             "        @Cuentaregistros INT" +

                              " SELECT @ArticulosOC = COUNT (distinct Nombre) FROM traceid..Vista_ArticulosOC WHERE idMaestroOrdenCompra = " + OC +
                              " SELECT @PreguntasCalidad = COUNT(" + e_VistaPregunta.Pregunta() + ") FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPreguntas() + " where general = 1" +
                              " SELECT @Cuentaregistros = COUNT(a." + e_TblADMRespuestasFormulario.idPregunta() + ") " +
                              "   FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblADMRespuestasFormulario() + " as a " +
                              "     INNER JOIN TRACEID..ADMPreguntasCalidad as b on (a.idPregunta = b.idPregunta)" +
                              "   WHERE " + e_TblADMRespuestasFormulario.OrdenCompra() + " = " + OC +
                              "         AND general = 1" +

                             " IF (@ArticulosOC * @PreguntasCalidad) = @Cuentaregistros" +
                             "   SELECT 'TRUE' AS Resultado" +
                             " ELSE" +
                             "   SELECT 'FALSE' AS Resultado";

            string Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);
            if (Resultado == "TRUE")  // si todas las preguntas Generales del formulrio de calidad han sido contestadas, se desbloquea la pestaa de recibir artículo.
                return true;
            else
                return false;
        }

        private bool HabilitaCierreOC(string OC)
        {
            try
            {
                string SQL = "DECLARE @ArticulosOC INT," +
                                 "    @PreguntasCalidad INT," +
                                 "    @Cuentaregistros INT" +

                                 " SELECT @ArticulosOC = COUNT (distinct Nombre) FROM Traceid..Vista_ArticulosOC WHERE idMaestroOrdenCompra = " + OC +
                                 " SELECT @PreguntasCalidad = COUNT(idPregunta) FROM Traceid..Vista_Preguntas" +
                                 " SELECT @Cuentaregistros = COUNT(a.idPregunta) " +
                                 "   FROM Traceid..ADMRespuestasFormulario as a" +
                                 "     INNER JOIN TRACEID..ADMPreguntasCalidad as b on (a.idPregunta = b.idPregunta)" +
                                 "   WHERE OrdenCompra = " + OC +

                                 " IF (@ArticulosOC * @PreguntasCalidad) >= @Cuentaregistros" +
                                 "   SELECT 'TRUE' AS Resultado" +
                                 " ELSE" +
                                 "   SELECT 'FALSE' AS Resultado";

                string Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);
                if (Resultado == "TRUE")  // si todas las preguntas del formulrio de calidad han sido contestadas, se permite cerrar la OC.
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
                return false;
            }
        }

    }
}