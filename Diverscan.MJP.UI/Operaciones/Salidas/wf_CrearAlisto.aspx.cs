using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using Diverscan.MJP.Negocio.MotorDecisiones;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Entidades;
using System.Web;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_CrearAlisto : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;
        string Pagina;
        RadGridProperties radGridProperties = new RadGridProperties();
        string Idmaestrosolicitud = "";
        string idUbicacion = "";

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
                if (ddlidbodega.SelectedIndex == -1)
                  CargarDDLS();

                txtCODBARRAS.Attributes.Add("onchange", "DisplayLoadingImage1();");
                ddlidbodega.Attributes.Add("onchange", "DisplayLoadingImage1();");
                ddlidMaestroSolicitud.Attributes.Add("onchange", "DisplayLoadingImage1()");
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

                case "pregunta":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "('" + sMensaje + "');", true);
                    break;
            }
        }

       
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
            //this.Panel2.Unload += new EventHandler(UpdatePanel2_Unload);
            //this.Panel3.Unload += new EventHandler(UpdatePanel3_Unload);
        }
       

        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        //void UpdatePanel2_Unload(object sender, EventArgs e)
        //{
        //    this.RegisterUpdatePanel2(sender as UpdatePanel);
        //}

        //void UpdatePanel3_Unload(object sender, EventArgs e)
        //{
        //    this.RegisterUpdatePanel3(sender as UpdatePanel);
        //}

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
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
        }
     
        private void CargarDDLS()
        {
            //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(15).Costa Rica:Grupo Diverscan. 
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidbodega ,e_TablasBaseDatos.VistaBodegaCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error..." + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-SAL-000001" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-SAL-000002" + ex.Message, "");
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

        protected void btnAccion11_Click(object sender, EventArgs e)
        {
           //TraceID.(2016). Operaciones/Ingresos/wf_UbicarArticulo.En Trace ID Codigos documentados(16).Costa Rica:Grupo Diverscan.
           // botón alistar artículo.
            string mensaje = "No exitoso-";

            try
            {
                if (string.IsNullOrEmpty(TxtSSCC.Text))
                {
                    Mensaje("info", "SSCC vacío", "");
                    return;
                }

                if (string.IsNullOrEmpty(txtCODBARRAS.Text))
                {
                    Mensaje("info", "Código de barras vacío", "");
                    return;
                }


                int raya = TxmTarea.Text.IndexOf("\n");
                string Idtarea = TxmTarea.Text.Substring(0, raya).Trim();

                // asocia el SSCC con los artículos solicitados.
                string Pagina = "~/HH/Operaciones/Salidas/wf_ProcesarDespacho.aspx";
                string CodLeido = TxtSSCC.Text + ";" + txtCODBARRAS.Text + ";" + TxtUbicacion.Text + ";" + TxtEtiquetatarea.Text + ";" + ddlidMaestroSolicitud.SelectedItem.Value.ToString() + ";" + Idtarea;

                string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion21");  //"btnAccion11"

                string[] Resultado = Respuesta.Split(';');

                if (Single.Parse(Resultado[1]) > 0)   // la cantidad pendiente por alistar.
                {
                    Muestra_Tareas(sender, "0");
                    TxtPendientealistar.Text = Resultado[1];
                    Limpiainfoarticulo();
                    mensaje = Resultado[0];  // "Transacción Exitosa"
                }

                if (Single.Parse(Resultado[1]) == 0)
                {
                    Muestra_Tareas(sender, "0");
                    TxtPendientealistar.Text = "0";
                    TxtUbicacion.Text = "";
                    TextBox2.Text = "";
                    txtidArticulo.Text = "";
                    TxtidarticuloERP.Text = "";
                    txtCantidad.Text = "";
                    txtFechaVencimiento.Text = "";
                    txtLote.Text = "";
                    txtCODBARRAS.Text = "";
                    mensaje = Resultado[0];  // "Transacción Exitosa"
                }

            }
            catch (Exception ex)
            {
                mensaje += ex.Message;
            }

            Mensaje("ok", mensaje, ""); 
        }

        protected void RadGrid1_Prerender(object sender, EventArgs e)
        {
            //radGridProperties.FormatearColumnas(RadGrid1);
            if (TxtSSCC.Text == "")
            {
                TxtSSCC.Focus();
                return;
            }

            if (TxtUbicacion.Text == "")
            {
                TxtUbicacion.Focus();
                return;
            }

            if (txtCODBARRAS.Text == "")
            {
                txtCODBARRAS.Focus();
                return;
            }
        }

        #endregion //EventosFrontEnd

        protected void btnObtenerAlisto_Click(object sender, EventArgs e)
        {
           // botón para ver disponibilidad del artículo o los datos asociados al mismo en recepción/ubicación/CrearAlisto.
            InfoArticulo(sender);
        }

        #endregion //TabsControl

        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
            TxtSSCC.ReadOnly = false;
            ddlidMaestroSolicitud.Items.Clear();
            LblMensaje.Visible = false;
        }

        protected void Muestra_Tareas(object limpia, string BotonActualizaTarea)
        {
            TxmTarea.Text = "";

            if (ddlidMaestroSolicitud.SelectedItem == null)
            {
                Mensaje("info","Problemas con el número de solicitud.","");
                return;
            }

            if (string.IsNullOrEmpty(UsrLogged.IdUsuario.ToString()))
            {
                Mensaje("info","Problemas con IdUsuario.","");
                return;
            }

            string Pagina2 = "~/HH/Operaciones/Tareas/GeneraTarea.aspx";
            string Codleido = UsrLogged.IdUsuario.ToString() + ";" + ddlidMaestroSolicitud.SelectedItem.Value.ToString() + ";" + BotonActualizaTarea + ";" + ddlidbodega.SelectedItem.Value.ToString();
            string[] respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina2, Codleido, UsrLogged.IdUsuario, "btnAccion12").Split(';'); //"btnAccion11"
            if (respuesta.Count() > 1)
            {
                string idTarea = respuesta[0];
                string Nombre = respuesta[1];
                string Lote = respuesta[2];
                TxtEtiquetatarea.Text = respuesta[3];
                string Cantidad = respuesta[4];
                string Estante = respuesta[5];  // pasillo
                string Zona = respuesta[6];
                string Nivel = respuesta[6];
                string Columna = respuesta[7];
                string Posicion = respuesta[8];
                TxtIdarticulotarea.Text = respuesta[9];
                DateTime fv = DateTime.Parse(respuesta[11]);

                string year = fv.Year.ToString();
                string month = fv.Month.ToString();
                string day = fv.Day.ToString();

                if (month.Length == 1)
                    month = "0" + month;

                if (day.Length == 1)
                    day = "0" + day;

                string FechaVencimiento = year + month + day;

                TxmTarea.Text = idTarea + "\n" +
                                "Artículo: " + Nombre + "\n" +
                                "Cantidad: " + Cantidad + " [" + respuesta[16] + " " + respuesta[15] + "] \n" +
                                "Pasillo.: " + Estante + "\n" +
                                "Columna.: " + Columna + "\n" +
                                "Nivel...: " + Nivel + "\n" +
                                "Posición: " + Posicion + "\n";
                TxtPendientealistar.Text = respuesta[14];
                TxtIdregistroTRA.Text = respuesta[13];
                Idmaestrosolicitud = respuesta[12];
                TxtSSCC.Focus();
                TxtPasaSiguienteTarea.Text = respuesta[9] + ";" + Lote + ";" + FechaVencimiento + ";" + Idmaestrosolicitud + ";" + UsrLogged.IdUsuario.ToString();
            }
            else
            {
                TxmTarea.Text = respuesta[0];
                TxtPendientealistar.Text = "";
                TxtIdregistroTRA.Text = "";
                TxtPasaSiguienteTarea.Text = "";
                TxtEtiquetatarea.Text = "";
                TxtIdarticulotarea.Text = "";
                if (respuesta[0].Contains("No hay tareas registradas"))  // si se llegó al final de las tareas se cierra el SSCC.
                {
                    if (TxtSSCC.Text == "")
                    {
                        Mensaje("info", "SSCC en blanco", "");
                        return;
                    }

                    string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
                    string SSCC = TxtSSCC.Text;
                    string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, SSCC, UsrLogged.IdUsuario, "BtnCierraSSCC");
                    Mensaje("ok", resultado, "");

                        if (resultado.Equals("SSCC Cerrado con exito"))
                        {
                            CargarDDLS();
                            // botón limpiar
                            Control Ctr = (Control)limpia;
                            var Panel = Ctr.Parent.Parent.Parent;
                            n_SmartMaintenance.LimpiarForm(Panel);
                        }
                        else
                            TxtSSCC.Text = "";
                    }
              }
        }

        protected void BtnTraeTarea_Click(object sender, EventArgs e)
        {
            Muestra_Tareas(sender, "1");
        }

        protected void Btncerrarsolicitudd_Click(object sender, EventArgs e)
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
            string respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina,"1", UsrLogged.IdUsuario, "btnAccion1");
            Mensaje("ok", respuesta, "");
        }

        protected void TxtSSCC_TextChanged(object sender, EventArgs e)
        {
           string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
           string codleido = TxtSSCC.Text;
           string respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, codleido, UsrLogged.IdUsuario, "BtnValidaSSCC");

           if (respuesta.Contains("SSCC ya está Cerrado") || respuesta.Contains("SSCC no válido"))
           {
               TxtSSCC.Text = "";
               Mensaje("ok", respuesta + "-No se puede usar", "");
               return;
           }

           TxtUbicacion.Focus();
           TxtSSCC.ReadOnly = true;
        }

        protected void TxtUbicacion_TextChanged(object sender, EventArgs e)
        {
            if (TxtUbicacion.Text != TxtEtiquetatarea.Text)
            {
                Mensaje("error", "Ubicación Leída diferente a la ubicación de la tarea...", "");
                TxtUbicacion.Text = "";
                TxtUbicacion.Focus();
                return;
            }

           txtCODBARRAS.Focus();
        }

        protected void txtCODBARRAS_TextChanged(object sender, EventArgs e)
        {
            InfoArticulo(sender);
        }

        private void InfoArticulo(object boton)
        {
            Control Ctr = (Control)boton;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, "btnObtenerAlisto");

            string[] Elementos = resultado.Split(';');
            if (Elementos[0].Contains("EXITOSAMENTE"))
            {
                if (Elementos[1] != TxtIdarticulotarea.Text)
                {
                    txtCODBARRAS.Text = "";
                    txtCODBARRAS.Focus();
                    Mensaje("error", "Código de artículo Leído diferente al código de artículo de la tarea...", "");
                    return;
                }

                txtidArticulo.Text = Elementos[1];
                TextBox2.Text = Elementos[2]; // descripción del artículo.
                TxtidarticuloERP.Text = Elementos[3];
                txtCantidad.Text = Elementos[4];  // cantidad que representa el GTIN.
                txtFechaVencimiento.Text = Elementos[5];
                txtLote.Text = Elementos[6];


                //if (txtCantidad.Text )
               
                int cantidadGTIN = 0;
                Single cantidadpendiente = 0;
                if (txtCantidad.Text.IndexOf(".") > 0)
                    cantidadGTIN = int.Parse(txtCantidad.Text.Substring(0, txtCantidad.Text.IndexOf(".")));
                else
                    cantidadGTIN = int.Parse(txtCantidad.Text);

                cantidadpendiente = Single.Parse(TxtPendientealistar.Text);

                if (cantidadGTIN > cantidadpendiente)
                {
                    LblMensaje.Visible = true;
                    //txtCODBARRAS.Text = "";
                }
                else
                {
                    LblMensaje.Visible = false;
                }
            }

            Mensaje("ok", Elementos[0], "");
        }

        protected void BtnSiguientetarea_Click(object sender, EventArgs e)
        { 
            string codleido = TxtPasaSiguienteTarea.Text;
            string respuesta = "";
            if (string.IsNullOrEmpty(codleido))
            {
                respuesta = "Cargue las tareas primero...";
            }
            else
            {
               string confirmValue = Request.Form["confirm_value"];

               if (confirmValue.Length > 2)
                 confirmValue = confirmValue.Substring(confirmValue.Length - 2, 2);

               if (confirmValue == "Si")
               {
                 Control Ctr = (Control)sender;
                 respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, codleido, UsrLogged.IdUsuario, Ctr.ID.ToString());
                 Muestra_Tareas(sender, "0");
               }
            }

            Mensaje("ok", respuesta, "");
        }

        protected void ddlidbodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaSolicitud();
        }

        private void CargaSolicitud()
        {
            try
            {
                string SQL = "";
                bool exito = false;
                string idbodega = ddlidbodega.SelectedItem.Value;

                SQL = "SELECT " + e_VistaMaestroSolicitudDestinoField.idMaestroSolicitud() + "," + e_VistaMaestroSolicitudDestinoField.Nombre() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaMaestroSolicitudDestino() +
                      "  WHERE " + e_VistaMaestroSolicitudDestinoField.idCompania() + " = 'AMCO'" +
                      "        AND " + e_VistaMaestroSolicitudDestinoField.IdBodega() + " = " + idbodega +
                      "        AND " + e_VistaMaestroSolicitudDestinoField.idUsuario() + " = " + UsrLogged.IdUsuario;
                exito = n_WMS.CargarDropGood(ddlidMaestroSolicitud, SQL, UsrLogged.IdUsuario);
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
        }

        protected void ddlidMaestroSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
           Limpiainfoarticulo();
           TxmTarea.Text = "";
           Muestra_Tareas(sender, "0");
        }

        protected void Limpiainfoarticulo()
        {
            txtidArticulo.Text = "";
            TxtidarticuloERP.Text = "";
            TextBox2.Text = "";
            txtFechaVencimiento.Text = "";
            txtLote.Text = "";
            txtCantidad.Text = "";
            txtCODBARRAS.Text = "";
        }

        protected void BtnCierraSSCC_Click(object sender, EventArgs e)
        {
            if (TxtSSCC.Text == "")
            {
                Mensaje("info", "SSCC en blanco", "");
                return;
            }
            
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue.Length > 2)
                confirmValue = confirmValue.Substring(confirmValue.Length - 2, 2);

            if (confirmValue == "Si")
            {
                string Pagina = "~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx";
                string SSCC = TxtSSCC.Text;
                string respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, SSCC, UsrLogged.IdUsuario, "BtnCierraSSCC");
                Mensaje("ok", respuesta, "");

                if (respuesta.Equals("SSCC Cerrado con exito"))
                {
                    CargarDDLS();
                    // botón limpiar
                    Control Ctr = (Control)sender;
                    var Panel = Ctr.Parent.Parent.Parent;
                    n_SmartMaintenance.LimpiarForm(Panel);
                }
                else
                    TxtSSCC.Text = "";
            }
        }

        protected void BtnCierraAlisto_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            string respuesta = "";

            if (confirmValue.Length > 2)
                confirmValue = confirmValue.Substring(confirmValue.Length - 2, 2);

            if (confirmValue == "Si")
            {
                Control Ctr = (Control)sender;
                string codleido = ddlidMaestroSolicitud.SelectedValue.ToString() + ";" + UsrLogged.IdUsuario + ";" +  ddlidbodega.SelectedItem.Value;
                respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, codleido, UsrLogged.IdUsuario, Ctr.ID.ToString());
            }

           Mensaje("ok", respuesta, "");
        }
    }
}