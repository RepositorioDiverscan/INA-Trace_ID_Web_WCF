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
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
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
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.MaestroArticulo;
using Diverscan.MJP.Negocio.GS1;


namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_MaestroArticulo : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        public int ToleranciaAgregar = 80;
        string Pagina = "";
        RadGridProperties radGridProperties = new RadGridProperties();
        Controls controls = new Controls();

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
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000003" + ex.Message, "");
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
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                //Msj = n_SmartMaintenance.CargarDDL(ddlidUnidadMedida, e_TablasBaseDatos.TblUnidadesMedida(), UsrLogged.IdUsuario, false);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                //Msj = n_SmartMaintenance.CargarDDL(ddlidTipoEmpaque, e_TablasBaseDatos.TblTiposEmpaque(), UsrLogged.IdUsuario, false);
                //if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidBodega, e_TablasBaseDatos.VistaBodegaCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidFamilia, e_TablasBaseDatos.TblFamiliaArticulo(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlidCategoriaArticulo, e_TablasBaseDatos.TblCategoriaArticulo(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000004" + ex.Message, "");
            }
        }

        protected void BtnGeneraGTIN_Click(object sender, EventArgs e)
        {
            
            n_MaestroArticulo genera = new n_MaestroArticulo();
            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
            int Pos = 1;
            GTIN.ValorLeido = genera.GeneraGTIN();
            // extrae cada digito del gtin 
            List<EntidadesGS1.e_Digito> Digitos = new List<EntidadesGS1.e_Digito>();
            foreach (Char CR in GTIN.ValorLeido.Reverse())  // extrictamente necesario hacerlo reverse para calcular el código verificador.
            {
                EntidadesGS1.e_Digito D = new EntidadesGS1.e_Digito();
                D.NumDigito = Pos;
                D.Valor = int.Parse(CR.ToString());
                Digitos.Add(D);
                GTIN.Digitos = Digitos;
                Pos++;
            }
            //
            int DV = CargarEntidadesGS1.GS1128_DigitoVerificador(GTIN);
            txtGTIN.Text = GTIN.ValorLeido + DV.ToString();
            chkGenerado.Checked = true;
            
        }

        #region TabsControl

        #region ControlRadGrid

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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000005" + ex.Message, "");
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
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo: TID-UI-MAN-ART-000006" + ex.Message, "");
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
            Control Ctr = (Control)sender;
            var Panel = Ctr.Parent.Parent.Parent;
            string resultado = n_SmartMaintenance.CargarEjecutarAccion(Pagina, Panel, UsrLogged.IdUsuario, Ctr.ID.ToString());
            Mensaje("ok", resultado, "");

            //controls.ClearFormControls(UpdatePanel1);  ver esto con smartmantenimice
        }

        protected void RadGrid1_Prerender(object sender, EventArgs e)
        {
           // radGridProperties.FormatearColumnas(RadGrid1);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarMaestroArticulo();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            EditarMaestroArticulo();
        }

        protected void Btnlimpiar1_Click(object sender, EventArgs e)
        {
            LimpiarMaestroArticulo();
            txtSearch.Text = "";
            CargarMaestroArticulo("", true);
            
        }

        #region MaestroArticulo

        private void CargarMaestroArticulo(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarMaestroArticulo '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridMaestroArticulo.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridMaestroArticulo.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void Mensaje1(string sTipo, string sMensaje)
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

        private void AgregarMaestroArticulo()
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            try
            {
               
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string Resultado = "";

                string idInterno = txtidInterno.Text.ToString().Trim();
                string idCompania = ddlidCompania.Text.ToString().Trim();
                string Nombre = txtNombre.Text.ToString().Trim();
                //string NombreHH = txtNombre.Text.ToString().Trim();
                string NombreHH = txtNombreHH.Text.ToString().Trim();
                string GTIN = txtGTIN.Text.ToString().Trim();
                string idBodega = ddlidBodega.Text.ToString().Trim();
                 string PesoKilos = txtPesoKilos.Text.ToString().Trim();
                //string PesoKilos = "1";
                //string DimensionUnidadM3 = txtDimensionUnidadM3.Text.ToString().Trim();
                string DimensionUnidadM3 = "1";
                string idFamilia = ddlidFamilia.Text.ToString().Trim();
                bool Granel = chkGranel.Checked;
                /*string TemperaturaMaxima = txtTemperaturaMaxima.Text.ToString().Trim();
                string TemperaturaMinima = txtTemperaturaMinima.Text.ToString().Trim();*/
                string TemperaturaMaxima = "1";
                string TemperaturaMinima = "1";
                string Contenido = txtContenido.Text.ToString().Trim();
                string UnidadMedida = txtUnidadMedida.Text.ToString().Trim();
                /*string DiasMinimosVencimiento = txtDiasMinimosVencimiento.Text.ToString().Trim();
                string DiasMinimosVencimientoRestaurantes = txtDiasMinimosVencimientoRestaurantes.Text.ToString().Trim();*/
                string DiasMinimosVencimiento = "1";
                string DiasMinimosVencimientoRestaurantes = "1";
                //double MinimoPicking = Convert.ToDouble(txtMinPicking.Text.ToString().Trim());
                //string idCategoriaArticulo = ddlidCategoriaArticulo.Text.ToString().Trim();
                string idCategoriaArticulo = "1";
                string empaque = txtEmpaque.Text;
                bool Activo = chkActivo.Checked;
                bool generado = chkGenerado.Checked;


                if (txtGTIN.Text.Length > 13)
                {

                    Mensaje1("error", "El GTIN ingresado tiene mas de 13 numeros, intente de nuevo ");
                }
                //else if (txtGTIN.Text.Length < 13)
                //{
                //    Mensaje1("error", "El GTIN ingresado tiene menos de 13 numeros, intente de nuevo ");
                //}
                else
                {

                    if (ValidarCamposMaestroArticulo())
                    {
                        SQL = "EXEC SP_InsertarMaestroArticulo '" + idInterno + "', '" + idCompania + "', '" + Nombre + "', '"
                            + NombreHH + "', '" + GTIN + "', '" + idBodega + "', '" + PesoKilos + "', '" + DimensionUnidadM3 + "', '"
                            + idFamilia + "', '" + Granel + "', '" + TemperaturaMaxima + "', '" + TemperaturaMinima + "', '" + Contenido + "', '"
                            + UnidadMedida + "', '" + DiasMinimosVencimiento + "', '" + DiasMinimosVencimientoRestaurantes + "', '"
                            + idCategoriaArticulo + "', '" + Activo + "','" + empaque + "'," + TxtEquivalencia.Text + ",'" 
                            + generado + "','" + chkTrazable.Checked + "'";
                        Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);

                        if (Resultado == "Ok")
                        {
                            Resultado = "Artículo insertado correctamente";
                            Mensaje("ok", Resultado, "");
                            CargarDDLS();
                            LimpiarMaestroArticulo();
                            CargarMaestroArticulo("", true);
                        }
                        else if (Resultado == "GTIN")
                        {
                            Resultado = "El GTIN ya esta registrado";
                            Mensaje("error", Resultado, "");
                        }
                        else
                        {
                            Resultado = "Error al registrar el artículo";
                            Mensaje("error", Resultado, "");
                        }
                    }
                    else
                    {
                        Resultado = "Existen campos requeridos en blanco";
                        Mensaje("error", Resultado, "");
                    }

                }
                //Mensaje("info", Resultado, "");

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
              
                fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);
            }
        }
        private void EditarMaestroArticulo()
        {
             try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string Resultado = "";

                string idArticulo = txtidArticulo.Text.ToString().Trim();
                string idInterno = txtidInterno.Text.ToString().Trim();
                string idCompania = ddlidCompania.Text.ToString().Trim();
                string Nombre = txtNombre.Text.ToString().Trim();
                string NombreHH = txtNombreHH.Text.ToString().Trim();
                //string NombreHH = txtNombre.Text.ToString().Trim();
                string GTIN = txtGTIN.Text.ToString().Trim();
                string idBodega = ddlidBodega.Text.ToString().Trim();
                string PesoKilos = txtPesoKilos.Text.ToString().Trim().Replace(",",".");
                //string DimensionUnidadM3 = txtDimensionUnidadM3.Text.ToString().Trim().Replace(",", ".");
                string DimensionUnidadM3 = "1";
                string idFamilia = ddlidFamilia.Text.ToString().Trim();
                bool Granel = chkGranel.Checked;
                //string TemperaturaMaxima = txtTemperaturaMaxima.Text.ToString().Trim().Replace(",", ".");
                //string TemperaturaMinima = txtTemperaturaMinima.Text.ToString().Trim().Replace(",", ".");
                string TemperaturaMaxima = "1";
                string TemperaturaMinima = "1";
                string Contenido = txtContenido.Text.ToString().Trim().Replace(",", ".");
                string UnidadMedida = txtUnidadMedida.Text.ToString().Trim();
                //string DiasMinimosVencimiento = txtDiasMinimosVencimiento.Text.ToString().Trim();
                //string DiasMinimosVencimientoRestaurantes = txtDiasMinimosVencimientoRestaurantes.Text.ToString().Trim();
                string DiasMinimosVencimiento = "1";
                string DiasMinimosVencimientoRestaurantes = "1";
                // double MinimoPicking = Convert.ToDouble(txtMinPicking.Text.ToString().Trim());
                //string idCategoriaArticulo = ddlidCategoriaArticulo.Text.ToString().Trim();
                string idCategoriaArticulo = "1";
                bool Activo = chkActivo.Checked;
                bool trazabilidad = chkTrazable.Checked;

                if (txtGTIN.Text.Length > 13)
                {
                    Mensaje1("error", "El GTIN editado tiene mas de 13 numeros, intente de nuevo ");
                }              
                else
                {
                    if (ValidarCamposMaestroArticulo() && !string.IsNullOrEmpty(idArticulo))
                    {
                        SQL = "EXEC SP_EditarMaestroArticulo '" + idArticulo + "', '" + idInterno + "', '" + idCompania + "', '" + Nombre + "', '" +
                                                                 NombreHH + "', '" + GTIN + "', '" + idBodega + "', '" +
                                                                 PesoKilos + "', '" + DimensionUnidadM3 + "', '" + idFamilia + "', '" +
                                                                 Granel + "', '" + TemperaturaMaxima + "', '" + TemperaturaMinima + "', '" +
                                                                 Contenido + "', '" + UnidadMedida + "', '" + DiasMinimosVencimiento + "', '" +
                                                                 DiasMinimosVencimientoRestaurantes + "', '" + idCategoriaArticulo +
                                                                 "', '" + Activo + "', '" + trazabilidad +"'";
                        Resultado = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);

                        if (Resultado == "Ok")
                        {
                            Resultado = "Artículo editado correctamente";
                            Mensaje("ok", Resultado, "");
                            CargarDDLS();
                            LimpiarMaestroArticulo();
                            CargarMaestroArticulo("", true);
                        }
                        else if (Resultado == "GTIN")
                        {
                            Resultado = "El GTIN ya está registrado";
                            Mensaje("error", Resultado, "");
                        }
                        else
                        {
                            Resultado = "Error al editar el artículo";
                            Mensaje("error", Resultado, "");
                        }
                    }
                    else
                    {
                        Resultado = "Existen campos requeridos en blanco";
                        Mensaje("error", Resultado, "");
                    }

                    //Mensaje("info", Resultado, "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                fileExceptionWriter.WriteException(ex,PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);
            }
        }
        private bool ValidarCamposMaestroArticulo()
        {
            bool resultado = true;

            if (string.IsNullOrEmpty(txtidInterno.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (ddlidCompania.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtNombre.Text.ToString().Trim()))
            {
                resultado = false;
            }
           
            else if (string.IsNullOrEmpty(txtGTIN.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (ddlidBodega.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }
            
           
            else if (ddlidFamilia.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }
           
            else if (string.IsNullOrEmpty(txtContenido.Text.ToString().Trim()))
            {
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtUnidadMedida.Text.ToString().Trim()))
            {
                resultado = false;
            }
            
            /*else if (ddlidCategoriaArticulo.SelectedValue == "--Seleccionar--")
            {
                resultado = false;
            }*/

            else if (string.IsNullOrEmpty(txtEmpaque.Text.ToString().Trim()))
            {
                resultado = false;
            }

            return resultado;
        }
        private void LimpiarMaestroArticulo()
        {
            txtidArticulo.Text = "";
            txtidInterno.Text = "";
            ddlidCompania.SelectedValue = "--Seleccionar--";
            txtNombre.Text = "";
            txtNombreHH.Text = "";
            txtGTIN.Text = "";
            ddlidBodega.SelectedItem.Value = "--Seleccionar--";
            txtPesoKilos.Text = "";
            txtDimensionUnidadM3.Text = "";
            ddlidFamilia.SelectedItem.Value = "--Seleccionar--";
            chkGranel.Checked = false;
            txtTemperaturaMaxima.Text = "";
            txtTemperaturaMinima.Text = "";
            txtContenido.Text = "";
            txtUnidadMedida.Text = "";
            txtDiasMinimosVencimiento.Text = "";
            txtDiasMinimosVencimientoRestaurantes.Text = "";
            ddlidCategoriaArticulo.SelectedValue = "--Seleccionar--";
            chkActivo.Checked = false;

            btnAgregar.Visible = true;
            btnEditar.Visible = false;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarMaestroArticulo();
                CargarMaestroArticulo(txtSearch.Text.ToString().Trim(), true);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        protected void RadGridMaestroArticulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarMaestroArticulo(txtSearch.Text.ToString().Trim(), false);
                //CargarMaestroArticulo("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        protected void RadGridMaestroArticulo_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    btnAgregar.Visible = false;
                    btnEditar.Visible = true;

                    txtidArticulo.Text = item["idArticulo"].Text.Replace("&nbsp;", "");
                    txtidInterno.Text = item["idInterno"].Text.Replace("&nbsp;", "");
                    ddlidCompania.SelectedValue = item["idCompania"].Text.Replace("&nbsp;", "");
                    txtNombre.Text = item["Nombre"].Text.Replace("&nbsp;", "");
                    txtNombreHH.Text = item["NombreHH"].Text.Replace("&nbsp;", "");
                    txtGTIN.Text = item["GTIN"].Text.Replace("&nbsp;", "");
                    
                    txtPesoKilos.Text = item["PesoKilos"].Text.Replace("&nbsp;", "");
                    txtDimensionUnidadM3.Text = item["DimensionUnidadM3"].Text.Replace("&nbsp;", "");
                                   
                    bool granel = false;
                    chkGranel.Checked = bool.TryParse(item["Granel"].Text.Replace("&nbsp;", ""), out granel);
                    //txtTemperaturaMaxima.Text = item["TemperaturaMaxima"].Text.Replace("&nbsp;", "");
                    //txtTemperaturaMinima.Text = item["TemperaturaMinima"].Text.Replace("&nbsp;", "");
                    txtContenido.Text = item["Contenido"].Text.Replace("&nbsp;", "");
                    txtUnidadMedida.Text = item["Unidad_Medida"].Text.Replace("&nbsp;", "");
                    txtEmpaque.Text = item["Empaque"].Text.Replace("&nbsp;", "");
                    //txtDiasMinimosVencimiento.Text = item["DiasMinimosVencimiento"].Text.Replace("&nbsp;", "");
                    //txtDiasMinimosVencimientoRestaurantes.Text = item["DiasMinimosVencimientoRestaurantes"].Text.Replace("&nbsp;", "");
                    //ddlidCategoriaArticulo.SelectedValue = item["idCategoriaArticulo"].Text.Replace("&nbsp;", "");
                    bool activo = false;
                    chkActivo.Checked = bool.TryParse(item["Activo"].Text.Replace("&nbsp;", ""),out activo);
                    chkTrazable.Checked = bool.TryParse(item["ConTrazabilidad"].Text.Replace("&nbsp;", ""), out activo);
                    //txtMinPicking.Text = item["MinimoPicking"].Text.Replace("&nbsp;", "");

                    ddlidBodega.SelectedItem.Value = item["NombreBodega"].Text.Replace("&nbsp;", "");
                    ddlidFamilia.SelectedItem.Value = item["NombreFamilia"].Text.Replace("&nbsp;", "");

                    //txtidArticulo
                    //txtidInterno
                    //ddlidCompania
                    //txtNombre
                    //txtNombreHH
                    //txtGTIN
                    //ddlidBodega
                    //txtPesoKilos
                    //txtDimensionUnidadM3
                    //ddlidFamilia
                    //chkGranel
                    //txtTemperaturaMaxima
                    //txtTemperaturaMinima
                    //txtContenido
                    //txtUnidadMedida
                    //txtDiasMinimosVencimiento
                    //txtDiasMinimosVencimientoRestaurantes
                    //ddlidCategoriaArticulo
                    //chkActivo
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        #endregion MaestroArticulo


        //private List<Diverscan.MJP.Entidades.MaestroArticulo.e_MaestroArticulo> ListMaestroArticulo
        //{
        //    get
        //    {
        //        var data = ViewState["ListMaestroArticulo"] as List<Diverscan.MJP.Entidades.MaestroArticulo.e_MaestroArticulo>;
        //        if (data == null)
        //        {
        //            data = new List<Diverscan.MJP.Entidades.MaestroArticulo.e_MaestroArticulo>();
        //            ViewState["ListMaestroArticulo"] = data;
        //        }
        //        return data;
        //    }
        //    set
        //    {
        //        ViewState["ListMaestroArticulo"] = value;
        //    }
        //}

        //protected void RadGridMaestroArticulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    n_WMS wms = new n_WMS();

        //    string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
        //    var MaestroArticulo = n_MaestroArticulo.GetMaestroArticulo(idCompania);
        //    ListMaestroArticulo = MaestroArticulo;

        //    RadGridMaestroArticulo.DataSource = ListMaestroArticulo;
        //}

        //protected void RadGridMaestroArticulo_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "RowClick")
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        txtidArticulo.Text = item["IdArticulo"].Text.Replace("&nbsp;", "");
                
                
                
        //        ddlidCompania.SelectedValue = item["IdCompania"].Text.Replace("&nbsp;", "");
        //        txtNombre.Text = item["Nombre"].Text.Replace("&nbsp;", "");
        //        txtNombreHH.Text = item["NombreHH"].Text.Replace("&nbsp;", "");
        //        txtGTIN.Text = item["GTIN"].Text.Replace("&nbsp;", "");
        //        ddlidUnidadMedida.SelectedItem.Text = item["Unidad_Medida"].Text.Replace("&nbsp;", "");
        //        ddlidTipoEmpaque.SelectedItem.Text = item["TiposEmpaqueNombre"].Text.Replace("&nbsp;", "");
        //        ddlidBodega.SelectedItem.Text = item["BodegaNombre"].Text.Replace("&nbsp;", "");
        //        ddlidFamilia.SelectedItem.Text = item["FamiliaNombre"].Text.Replace("&nbsp;", "");
        //        chkGranel.Checked = bool.Parse(item["Granel"].Text);
        //        txtTemperaturaMaxima.Text = item["TemperaturaMaxima"].Text.Replace("&nbsp;", "");
        //        txtTemperaturaMinima.Text = item["TemperaturaMinima"].Text.Replace("&nbsp;", "");
        //        txtDiasMinimosVencimiento.Text = item["DiasMinimosVencimiento"].Text.Replace("&nbsp;", "");
        //        txtidInterno.Text = item["IdInterno"].Text.Replace("&nbsp;", "");
        //        txtContenido.Text = item["Contenido"].Text.Replace("&nbsp;", "");
        //        txtUnidadMedida.Text = item["UnidadesMedidaNombre"].Text.Replace("&nbsp;", "");
        //        txtDiasMinimosVencimientoRestaurantes.Text = item["DiasMinimosVencimientoRestaurantes"].Text.Replace("&nbsp;", "");
        //    }
        //}

        //protected void btnBuscarMaestroArticulo_Click(object sender, EventArgs e)
        //{
        //    n_WMS wms = new n_WMS();

        //    string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

        //    var roles = Diverscan.MJP.Negocio.MaestroArticulo.n_MaestroArticulo.GetListMaestroArticulo(txtSearch.Text, idCompania);
        //    ListMaestroArticulo = roles;

        //    RadGridMaestroArticulo.DataSource = ListMaestroArticulo;
        //    RadGridMaestroArticulo.DataBind();
        //}
       
        #endregion //EventosFrontEnd

        #endregion //TabsControl
    }
}