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

namespace Diverscan.MJP.UI.Operaciones.Eventos
{
    public partial class wf_DemoEventos : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        string Patron = "[ABCDEFGHIJKLMNÑOPRSTUVWXYZ|°¬!#$%&/()=?¡¨*_:;,.´+¿'¬\\><-~{}`" + (char)34 + "]"; // patron de caracteres a evaluar en el Regex, para evaluar 
                                                                                                           // si en algún textbox hay letras, cuando se deben aceptar numeros.

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
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
                }
            }
        }

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
                foreach (Control c in Panel3.Controls)
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
                foreach (Control c in Panel5.Controls)
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000004-" + ex.Message, "");
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
                //string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(DdlidMOC0, e_TablasBaseDatos.VistaMaestroOrdenCompraProveedor(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidArticulo0, e_TablasBaseDatos.TblMaestroArticulos(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(DdlidMOC, e_TablasBaseDatos.VistaMaestroOrdenCompraProveedor(), UsrLogged.IdUsuario, true);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
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
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            Mensaje("ok", resultado, "");

            if (txtCantidad0.Text == "Digite la cantidad")
            {
                this.txtCantidad0.Attributes.Add("onclick", "this.value = '' "); // se le agrega este atributo al textbox, para que cuando
                                                                                 // se haga clic en el textbox se borre su contenido.
            }
            else
            {
                this.txtCantidad0.Attributes.Add("onclick", "");
            }
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
                    else if (int.Parse(txtCantidad0.Text) <= 0)
                    {
                        Mensaje("error", "Debe poner una cantidad valida...", "");
                        return;
                    }

                    //procedimiento que arma el nuevo código GS1
                     string codleido = txtCODBARRAS0.Text + ";" + txtCantidad0.Text + ";" + DdlidMOC.SelectedValue.ToString();
                     string respuesta = n_WMS.ProcesarCodigoGS1ArticuloGranel(codleido, UsrLogged.IdUsuario);
                     string [] mensaje = respuesta.Split (';');

                     if (mensaje [0] == "0")
                     {
                         Mensaje("error", mensaje [1], "");
                         return;
                     }

                     txtCODBARRAS0.Text= mensaje [0];
                }

               // Recibe el artículo.
                string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                string CodLeido = txtCODBARRAS0.Text + ";OC" + DdlidMOC.SelectedValue.ToString();
                string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion32");

                Mensaje("ok", (Respuesta.Replace("\n", "|| ")), "");

            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message , "");
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
            if (txtUbicacionLeida.Text.Equals(txtUbicacionSugerida.Text) && !string.IsNullOrEmpty(txtUbicacionLeida.Text))
            {
                string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                string CodLeido = txtCODBARRAS.Text;
                string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "bntAccion22");
                Mensaje("ok", Respuesta, "");
            }
            else
                Mensaje("info","La ubicacion leida es diferente a la ubicacion sugerida o ambas no son válidas.","");
        }

       protected void DdlidRespuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
          // combo de respuestas en pestaña de control de calidad, si se elije NO, muestra un texto para poner un mensaje y el password del administrador.
           string Prueba = DdlidRespuesta.SelectedItem.Text.ToString();
           if (Prueba.Equals("No"))
            {
                lblComment.Visible = true;
                txtComent.Visible = true;
                lblAdmin.Visible = true;
                txtPass.Visible = true;
            }
           else
           {
                lblComment.Visible = false;
                txtComent.Visible = false;
                lblAdmin.Visible = false;
                txtPass.Visible = false;
            }
        }

        protected void ddlidMaestroOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
         // combo de ordenes de compra en pestaña de control de calidad, según las OC se cargan los artículos que pertenecen a esa OC.
          string OC = DdlidMOC0.SelectedValue.ToString();
          CargarNombreTablas(OC, ddlidArticulo0);
        }
          
        private void CargarNombreTablas(String OC, DropDownList DDL)
        {
            DDL.Items.Clear();
            DataSet DSTablas = new DataSet();
            string SQL = " SELECT distinct Nombre,idarticulo FROM Vista_ArticulosOC WHERE idMaestroOrdenCompra ='" + OC + "'";
            DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            if (DSTablas.Tables[0].Rows.Count > 0)
            {
                DDL.DataSource = DSTablas;
                DDL.DataTextField = "Nombre";
                DDL.DataValueField = "idarticulo";
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
        }

        protected void btnAccion41_Click(object sender, EventArgs e)
        {
           // botón guardar respuesta
            string OrdenCompra = DdlidMOC0.Text;
            string Articulo = ddlidArticulo0.Text;
            string Pregunta = DdlidPregunta.Text;
            string Resp = DdlidRespuesta.Text;
            string Comentario = txtComent.Text;
            string AdminPass = txtPass.Text;
            string Temperatura = TxtTemperatura.Text;

            try
            {
                if (OrdenCompra.Equals("--Seleccionar--") || Articulo.Equals("--Seleccionar--") || Pregunta.Equals("--Seleccionar--") || Resp.Equals("--Seleccionar--"))
                {
                    Mensaje("info", " Por favor seleccione una opción de: Orden de compra, Artículo o Pregunta, para continuar", "");
                    return;
                }

                if (Resp.Equals("2") && (string.IsNullOrEmpty(AdminPass)) || Resp.Equals("2") && (string.IsNullOrEmpty(Comentario)))
                {
                    Mensaje("ok", "el campo Comentario y/o autorización de administrador no pueden estar vacios", "");
                    return;
                }

                else
                {
                    string CodLeido = OrdenCompra + ";" + Articulo + ";" + Pregunta + ";" + Resp + ";" + Comentario + ";" + AdminPass + ";" + Temperatura + ";" + TxtEvalua.Text;
                    string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                    string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion41");
                    Mensaje("ok", Respuesta, "");

                    string[] accion = Respuesta.Split(';');
                    if (accion.Count() > 1)
                    {
                        if (accion[1] == "False")
                        {
                            txtComent.Visible = true;
                            lblComment.Visible = true;
                            txtComent.Text = accion[0] + " " + Temperatura;
                            txtPass.Text = "";
                            lblAdmin.Visible = true;
                            txtPass.Visible = true;
                            DdlidRespuesta.SelectedIndex = 2;
                            TxtEvalua.Text = "False";
                        }
                        return;
                    }
                   
                        txtComent.Visible = false;
                        txtComent.Text = "";
                        lblComment.Visible = false;
                        txtPass.Visible = false;
                        txtPass.Text = "";
                        lblAdmin.Visible = false;
                        DdlidRespuesta.SelectedIndex = 0;
                        TxtEvalua.Text = "True";
                        TxtTemperatura.Text = "";
                    

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000006-" + ex.Message, "");
            }

        }

        protected void btnAccion42_Click(object sender, EventArgs e)
        {
           // botón recibir todo a satisfacción.
            string OrdenCompra = DdlidMOC0.Text;
            string Articulo = ddlidArticulo0.Text;
            string Pregunta = DdlidPregunta.Text;
            string Resp = DdlidRespuesta.Text;
            string Comentario = txtComent.Text;
            string AdminPass = txtPass.Text;
            string Respuesta = "";
            string Temperatura = TxtTemperatura.Text;
            TxtEvalua.Text = "False"; 
            Single Temp;
          
            try
            {
                Single.TryParse(Temperatura, out Temp);

                if (OrdenCompra.Equals("--Seleccionar--"))
                {
                    Mensaje("info", " Por favor seleccione una opción de orden de compra para continuar", "");
                    return ;
                }

                else if (Resp.Equals("2"))
                {
                    Mensaje("info", "Para recibir todos los articulos, TODOS deben cumplir con condiciones satisfactorias (respuesta SI)", "");
                    return;
                }

                //else if (string.IsNullOrEmpty(Temperatura) || Regex.IsMatch(Temperatura.ToUpper (), Patron))
                //{
                //    Mensaje("info", "Para recibir todos los articulos, el valor de la temperatura debe ser numérico", "");
                //    return;
                //}
                else
                {
                    for (int i = 1; i < ddlidArticulo0.Items.Count; i++)
			        {
                      Articulo = ddlidArticulo0.Items[i].Value;

                        for (int j = 1; j < DdlidPregunta.Items.Count; j++)
			            {
                            Pregunta = DdlidPregunta.Items[j].Value;
                            Resp = DdlidRespuesta.Items[1].Value;

                            string CodLeido = OrdenCompra + ";" + Articulo + ";" + Pregunta + ";" + Resp + ";" + Comentario + ";" + AdminPass + ";" + Temperatura + ";" + TxtEvalua.Text; 
                            string Pagina = "~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx";
                            Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion41");
			            }
			        }

                 Mensaje("ok", Respuesta, "");
                 txtComent.Visible = false;
                 txtComent.Text = "";
                 lblComment.Visible = false;
                 txtPass.Visible = false;
                 txtPass.Text = "";
                 lblAdmin.Visible = false;
                 DdlidRespuesta.SelectedIndex = 0;
                 DdlidPregunta.SelectedIndex = 0;
                 DdlidMOC0.SelectedIndex = 0;
                 ddlidArticulo0.SelectedIndex = 0; 
                 TxtEvalua.Text = "True";
                 TxtTemperatura.Text = "";

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000007" + ex.Message, "");
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string CodLeido = txtCodLeido.Text;
            string Pagina = "~/HH/Operaciones/Eventos/wf_DemoEventos.aspx";
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnConsultar");
            Mensaje("ok", Respuesta, "");
        }

        protected void btnConsultarRegalia_Click(object sender, EventArgs e)
        {
            string CodLeido = txtCodLeido.Text;
            string Pagina = "~/HH/Operaciones/Eventos/wf_DemoEventos.aspx";
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnConsultarRegalia");
            Mensaje("ok", Respuesta, "");
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string CodLeido = "201230456;1;2;3";
            string Pagina = "~/HH/Operaciones/Eventos/wf_DemoEventos.aspx";
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnEnviar");
            Mensaje("ok", Respuesta, "");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAccion1_Click(object sender, EventArgs e)
        {
           // botón limpiar, disponibilidad
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
        }

        protected void BtnLimpiar1_Click(object sender, EventArgs e)
        {
           // botón limpiar, ubicación. 
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }
    }       
}