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
using Diverscan.MJP.Utilidades.general;


namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_Despacho : System.Web.UI.Page
    {

        e_Usuario UsrLogged = new e_Usuario();
        // VarlorCantidadSeleccionada se usa para guardar el valor cuando se pasa de RadListBox1 a RadListBox2
        double VarlorCantidadSeleccionada = 0;
        // IdArticuloSeleccionado se usa para guardar el idarticulo cuando se pasa de RadListBox2 a RadListBox1
        string IdDetalleOrdenCompra = "";
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        //string SqlDia = "Select A.Nombre From ADMDiaSemana A ";
        RadGridProperties radGridProperties = new RadGridProperties();

        protected void Page_Load(object sender, EventArgs e)
        {
            Pagina = Page.AppRelativeVirtualPath.ToString();
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }


            if (!IsPostBack)
            {
                CargarDDLS();
                txtSSCCVehiculo.Attributes.Add("onchange", "DisplayLoadingImage1();");
                //CargarUnidadesEmpaque();
                ////GetComboUnidadEmpaque();
                // CargarNombreBases("TRACEID", ddlBaseDatos);
               // CargarDrop(ddDia, SqlDia, "Nombre");

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
            this.Panel6.Unload += new EventHandler(UpdatePanel4_Unload);

            //this.UpdatePanelAccesosRoles.Unload += new EventHandler(UpdatePanel_Unload2);
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

        void UpdatePanel4_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel4(sender as UpdatePanel);
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

        public void RegisterUpdatePanel4(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel4 });
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
        /// 
        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid DG = (RadGrid)sender;
            Control Parent = DG.Parent;
            n_SmartMaintenance.CargarGrid(Parent, UsrLogged.IdUsuario);
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlIdvehiculo, e_TablasBaseDatos.TblVehiculos(), UsrLogged.IdUsuario,true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlIdruta, e_TablasBaseDatos.TblRutas(), UsrLogged.IdUsuario,true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidMetodoAccion, e_TablasBaseDatos.TblMetodoAccion(), UsrLogged.IdUsuario);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                //Msj = n_SmartMaintenance.CargarDDL(ddlidTablaCampoDocumentoAccion, e_TablasBaseDatos.TblTransaccion(), UsrLogged.IdUsuario);
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
        /// Para que esto funcione el boton debe estar contenido en una Panel (Parent), 
        /// luego en un update Panel (Parent), y luego en el Panel (Parent) contenedor, la idea es usar el mismo boton
        /// para cualquier accion, segun el Patron de Programacion 1 / Diverscan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnAccion21_Click(object sender, EventArgs e)
        {
           // botón asociar SSCC
            string Pagina = "~/HH/Operaciones/Salidas/wf_Despacho.aspx";
            string ssccLeido = txtSSCCLeido.Text;
            string UbicacionLeida = txtUbicacionLeida.Text;
            string CodLeido = ssccLeido + ";" + UbicacionLeida;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "BtnAsociaSSCCubicacion");
            Mensaje("ok", Respuesta.Replace("\n", "|| "), "");
        }

        protected void btnAccion31_Click(object sender, EventArgs e)
        {
           // botón aprobar despacho.
            string Pagina = "~/HH/Operaciones/Salidas/wf_ProcesarDespacho.aspx";
            string CodLeido = txtSSCCLeido0.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAccion31");

            Mensaje("ok", Respuesta, "");
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
            string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TblFamiliaArticulo(), ToleranciaAgregar, UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TblFamiliaArticulo(), UsrLogged.IdUsuario);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
        }

        public void Accion(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            Mensaje("ok", resultado, "");
        }

        protected void btnInfoSSCC(object sender, EventArgs e)
        {
           //string destino = "";
           //txmInfoSSCC.Text = n_WMS.InfoSSCC(txtSSCCLeido0.Text, out destino);
           
        }
        
         protected void btnDevolver_Click(object sender, EventArgs e)
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_Despacho.aspx";
            string CodLeido = txtCODBARRAS.Text + ";" + txtCodBarArticulo.Text + ";" + txtUbicacionmover.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnDevolver");
            Mensaje("ok", Respuesta, "");
        }

         protected void RadGrid3_Prerender(object sender, EventArgs e)
         {
             radGridProperties.FormatearColumnas(RadGrid3);
         }

        
        #endregion //EventosFrontEnd


        #endregion //TabsControl


        #region Metodos

        protected void VerInfoSSCC(object sender, EventArgs e) 
        {
            MuestraInfoSSCC();
        }

        protected void AsociarSSCCVehiculo(object sender, EventArgs e)
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_Despacho.aspx";
            string CodLeido = txtUbicacionParqueo.Text + ";" + txtSSCCVehiculo.Text + ";" + txtDimsensionSSCCM3.Text + ";0;" + txtPesoKilos.Text + ";" + ddlIdvehiculo.SelectedValue.ToString () + ";" + ddlIdruta.SelectedValue.ToString();
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "btnAsociarVehiculo");
            Mensaje("ok", Respuesta, "");
        }

        private void CargarDrop(DropDownList DDL, string query, string value)
        {
            try
            {
                DDL.Items.Clear();
                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = n_ConsultaDummy.GetDataSet(query, "0");
                if (DSBaseDatos != null)
                {
                    if (DSBaseDatos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                        {
                            string name = dsRowEvento["Nombre"].ToString();
                            DDL.Items.Add(name);
                        }
                        DDL.DataBind();
                        DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
                    }                   
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion 

        protected void BtnLimpiarform_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            n_SmartMaintenance.LimpiarForm(Panel);
        }

        protected void txtSSCCVehiculo_TextChanged(object sender, EventArgs e)
        {
            MuestraInfoSSCC();
        }

        private void MuestraInfoSSCC()
        {
            string Pagina = "~/HH/Operaciones/Salidas/wf_Despacho.aspx";
            string CodLeido = txtSSCCVehiculo.Text;
            string Respuesta = n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodLeido, UsrLogged.IdUsuario, "buton2");

            string[] splito = Respuesta.Split(';');
            txmInfoSSCC.Text = splito[0].ToString();
            txtDestino.Text = splito[1];
            txtPesoKilos.Text = splito[2];
            txtDimsensionSSCCM3.Text = splito[3];

            Mensaje("ok", "Operación Realizada", "");
        }
    }
}

/*  AsociarSSCCVehiculo
         try
         {

             DataSet RutaVehiculo = new DataSet();
             string[] idHora;
             string SqlidUbicacion = "Select A.idUbicacion From Vista_CodigosMaestroUbicacion A Where A.Etiqueta = '" + txtUbicacionParqueo.Text + "'";
             string idUbicacion = n_ConsultaDummy.GetUniqueValue(SqlidUbicacion, "0");
             if (!String.IsNullOrEmpty(idUbicacion))
             {

                 string SqlDiaSemana = "SELECT DatePart(WeekDay, GetDate()) As Dia , CONVERT(VARCHAR, getdate(), 108) As Hora";
                 DataSet DiaHora = n_ConsultaDummy.GetDataSet2(SqlDiaSemana, "0");
                 idHora = DiaHora.Tables[0].Rows[0][1].ToString().Split(':');
                 idHora[0] = idHora[0].ToString().Replace("0", "").Trim();
                 string SqlRelUbicacionVehiculo = "Select A.idRuta , A.IdVehiculo From RelUbicacionVehiculo A Where A.idUbicacion = '" + idUbicacion + "' and A.idDia = '" + DiaHora.Tables[0].Rows[0][0].ToString() + "' and A.idHoradia = '" + idHora[0].ToString() + "'";
                 RutaVehiculo = n_ConsultaDummy.GetDataSet2(SqlRelUbicacionVehiculo, "0");

                 if (RutaVehiculo.Tables[0].Rows.Count > 0)
                 {
                     string idDia = DiaHora.Tables[0].Rows[0][0].ToString();
                     string Ruta = RutaVehiculo.Tables[0].Rows[0][0].ToString();
                     string idVehiculo = RutaVehiculo.Tables[0].Rows[0][1].ToString();
                     string InsertTraVehiculoSSCC = "Insert Into TraVehiculoTrasladoSSSCC(ConsecutivoSSCC,idVehiculo,idDia,idHoraDia,PesoKilos,DimensionM3,Equivalencia) Values(" + txtSSCCVehiculo.Text.ToString() + "," + idVehiculo + "," + idDia + "," + idHora[0] + ",(" + txtPesoKilos.Text.ToString().Replace(",", ".").Trim() + "),(" + txtDimsensionSSCCM3.Text.ToString().Replace(",", ".").Trim() + "),(" + txtEquivalencia.Text.ToString().Replace(",", ".").Trim() + "))";

                     switch (n_WMS.ObtenerDisponibilidadCamion(idVehiculo,Convert.ToDecimal(txtPesoKilos.Text),Convert.ToDecimal(txtDimsensionSSCCM3.Text)))
                     {
                        
                         case "Solicitud Precesada":
                             if(n_ConsultaDummy.PushData(InsertTraVehiculoSSCC, "0"))                             
                             Mensaje("ok","SSCC Asociado con exito","");
                             else
                             Mensaje("info" , "Error al asociar SSCC","");
                             break;

                         default:
                              Mensaje("info" , "Error al asociar SSCC","");
                             break;
                          
                     }
                 }
             }
             else 
             {
                 Mensaje("info", "Error ingrese la ubicación correcta", "");
                   
             }

             txtSSCCVehiculo.Text = "";
             txtUbicacionParqueo.Text = "";
             txmInfoSSCC.Text = "";
             txtDestino.Text = "";
             txtPesoKilos.Text = "";
             txtDimsensionSSCCM3.Text = "";
             txtEquivalencia.Text = "";
             }

         catch (Exception ex)
         {
                
             throw;
         }
 * 
 *--> VerInfoSSCC
            string respuesta = "";
            string SSCC = txtSSCCVehiculo.Text.ToString();
            string Destino = "";
            decimal PesoKilos = 0;
            decimal DimsensionSSCCM3 = 0;
            decimal Equivalencia = 0;
            respuesta = n_WMS.InfoSSCC(SSCC, out Destino, out PesoKilos, out DimsensionSSCCM3, out Equivalencia);
            txmInfoSSCC.Text = respuesta;
            txtDestino.Text = Destino;
            txtPesoKilos.Text = PesoKilos.ToString();
            txtDimsensionSSCCM3.Text = DimsensionSSCCM3.ToString();
            txtEquivalencia.Text = Equivalencia.ToString();
          //  respuesta = n_WMS.InfoSSCC(
           */
