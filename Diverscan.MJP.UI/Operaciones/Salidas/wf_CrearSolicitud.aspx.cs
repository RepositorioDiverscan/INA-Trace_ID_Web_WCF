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
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.LogicaWMS;
using System.Globalization;
using Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Negocio.OPESALPreDetalleSolicitud;

namespace Diverscan.MJP.UI.Operaciones.Salidas
{
    public partial class wf_CrearSolicitud : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        // VarlorCantidadSeleccionada se usa para guardar el valor cuando se pasa de RadListBox1 a RadListBox2
        double VarlorCantidadSeleccionada = 0;
        // IdArticuloSeleccionado se usa para guardar el idarticulo cuando se pasa de RadListBox2 a RadListBox1
        string IdDetalleOrdenCompra = "";
        string gtin_leido = "";
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 95;
        string Pagina = "";
        RadGridProperties radGridProperties = new RadGridProperties();
        public static DataTable DTPreDetalleSolicitud = new DataTable();


        #region "Objetos requeridos"
        n_WMS wms = new n_WMS();
        #endregion


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
                CrearDSPreDetalleSolicitud();
                RadGridOPESALPreDetalleSolicitud.GroupingSettings.CaseSensitive = false;
            }
        }
        private void CrearDSPreDetalleSolicitud()
        {
            try
            {
                if (!DTPreDetalleSolicitud.Columns.Contains("IdPreLineaDetalleSolicitud"))
                {
                    DTPreDetalleSolicitud.Columns.Add("IdPreLineaDetalleSolicitud", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("idArticuloInterno", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("NombreArticuloInterno", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("Cantidad", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("UnidadMedida", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("UnidadesAlisto", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("CantidadGtin", typeof(string));
                    DTPreDetalleSolicitud.Columns.Add("Gtin", typeof(string));


                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
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

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidDestino, e_TablasBaseDatos.TblDestino(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-SAL-000004" + ex.Message, "");
            }
        }

      

    #region Solicitud

    protected void btnAgregarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTPreDetalleSolicitud.Rows.Count > 0 && ddlidDestino.SelectedValue != "--Seleccionar--" && !string.IsNullOrEmpty(txtNombre.Text))
                {
                    string SQL = "";
                    string idDestino = ddlidDestino.SelectedValue;
                    string Nombre = txtNombre.Text.ToString().Trim();
                    string Comentarios = txmComentarios.Text.ToString().Trim();

                    string IdCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                    string idUsuario = UsrLogged.IdUsuario;
                    string idInterno = "";
                    string idInternoSAP = "";

                    SQL = "EXEC SP_InsertarMaestroSolicitud '" + idUsuario + "','" + Nombre + "','" + Comentarios + "','" + IdCompania + "','" + idDestino + "','" + idInterno + "','" + idInternoSAP + "'";
                    string idMaestroSolicitud = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    if (!string.IsNullOrEmpty(idMaestroSolicitud))
                    {
                        foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                        {
                            string numLinea = DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString();
                            string idArticuloInterno = DRPreDetalleSolicitud["idArticuloInterno"].ToString();
                            string NombreArticuloInterno = DRPreDetalleSolicitud["NombreArticuloInterno"].ToString();
                            string Cantidad = DRPreDetalleSolicitud["Cantidad"].ToString();
                            string gtin = DRPreDetalleSolicitud["Gtin"].ToString();
                            string Descripcion = "";
                            

                            SQL = "EXEC SP_InsertarPreDetalleSolicitud '" + NombreArticuloInterno + "','" + idMaestroSolicitud + "','" + idArticuloInterno + "','" + Cantidad + "','" + Descripcion + "','" + IdCompania + "','" + idUsuario + "','" + numLinea + "','" + gtin + "'";
                            n_ConsultaDummy.PushData(SQL, idUsuario);
                        }

                        DTPreDetalleSolicitud.Clear();
                        LimpiarSolicitud();
                        LimpiarPreDetalleSolicitud();
                        CargarPreDetalleSolicitud("", true);
                        TxtSolicitud.Text = "Solicitud N° " + idMaestroSolicitud;
                        Mensaje("ok", "Solicitud creada correctamente, N° " + idMaestroSolicitud, "");
                    }
                    else
                    {
                        Mensaje("error", "Error al insertar la solicitud", "");
                    }
                }
                else
                {
                    Mensaje("error", "Falta de agregar datos requeridos", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarSolicitud()
        {
            ddlidDestino.SelectedValue = "--Seleccionar--";
            txtNombre.Text = "";
            txmComentarios.Text = "";

            btnAgregarSolicitud.Enabled = false;
        }

        private bool EsNumeroEntero(string Entero)
        {
            int EsNumero;
            if (Int32.TryParse(Entero, out EsNumero))
                return true;
            else
                return false;
        }

        #endregion Solicitud

        #region PreDetalleSolicitud

        #region "Controles PreDetalleSolicitud"

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string idArticuloInterno = ddlidArticuloInterno.SelectedValue;
                string NombreArticuloInterno = ddlidArticuloInterno.SelectedItem.Text;
                string Cantidad = txtCantidad.Text.ToString().Trim().Replace(",", ".");

                string codigo_leido = ddlpresentacion.SelectedItem.ToString();
                string[] textos = codigo_leido.Split('-');

                string gtin_leido_v2 = textos[0];

                LabelGtin.Text = gtin_leido_v2;




                if (ddlidArticuloInterno.SelectedIndex == 0)//Valida si se seleccionó un artículo
                {
                    Mensaje("info", "Debe seleccionar un artículo", "");
                    lbUnidadMedida.Text = "";
                }
                else if (EsValidaCantidadArticulo(Cantidad))//Se valida la cantidad ingresada sea valida               
                {

                    bool existeArticulo = false;

                    foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                    {
                        //if (DRPreDetalleSolicitud["idArticuloInterno"].ToString() == idArticuloInterno)
                        //{
                        //    existeArticulo = true;
                        //}

                        if (DRPreDetalleSolicitud["GTIN"].ToString() == gtin_leido_v2)
                        {
                            existeArticulo = true;
                        }
                    }
                    if (!existeArticulo)
                    {
                        int NumeroLinea = 1 + (DTPreDetalleSolicitud.Rows.Count);
                        decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                        e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, cantidadIngresada, gtin_leido_v2);//Se obtienen los detalles del idInterno articulo seleleccionado
                        //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto 
                        if (true)
                        {
                            DTPreDetalleSolicitud.Rows.Add(NumeroLinea, idArticuloInterno, NombreArticuloInterno, Cantidad, detalleArticulo.Unidad_Medida, detalleArticulo.UnidadesAlistoDetalle, detalleArticulo.CantidadGtin, detalleArticulo.Gtin);
                            
                            LimpiarPreDetalleSolicitud();
                            CargarPreDetalleSolicitud("", true);
                            Mensaje("ok", "El artículo se ingresó correctamente", "");
                            btnAgregarSolicitud.Enabled = true;
                            lbUnidadMedida.Text = "";
                            TxtReferencia.Text = "";

                            limpia_agregaarticulo();

                        }
                    }
                    else
                    {
                        Mensaje("error", "El artículo ya existe", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                lbUnidadMedida.Text = "";
            }
        }

       private void limpia_agregaarticulo ()
        {
            string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));

        }

        //protected void btnEditarArticulo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string IdPreLineaDetalleSolicitud = txtIdPreLineaDetalleSolicitud.Text.ToString().Trim();
        //        string idArticuloInterno = ddlidArticuloInterno.SelectedValue;
        //        string NombreArticuloInterno = ddlidArticuloInterno.SelectedItem.Text;
        //        string Cantidad = txtCantidad.Text.ToString().Trim().Replace(",", ".");
        //        bool existeArticulo = false;

        //        string codigo_leido = ddlpresentacion.SelectedItem.ToString();
        //        string[] textos = codigo_leido.Split('-');

        //        string gtin_leido_v2 = textos[0];


        //        if (EsValidaCantidadArticulo(Cantidad))//Valida que la cantidad editada sea válida
        //        {
        //            foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
        //            {
        //                if (DRPreDetalleSolicitud["idArticuloInterno"].ToString() == idArticuloInterno)
        //                {
        //                    existeArticulo = true;
        //                }
        //            }

        //            //if (EsNumeroEntero(Cantidad))
        //            //{
        //            if (existeArticulo)//Verifica que el artículo ya esté en el grid
        //            {
        //                decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
        //                e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, cantidadIngresada, gtin_leido_v2);//Se obtienen los detalles del idInterno articulo seleleccionado
        //                //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto 
        //                if(true)
        //                {
        //                    foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
        //                    {
        //                        if (DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString() == IdPreLineaDetalleSolicitud)
        //                        {
        //                            DRPreDetalleSolicitud["idArticuloInterno"] = idArticuloInterno;
        //                            DRPreDetalleSolicitud["NombreArticuloInterno"] = NombreArticuloInterno;
        //                            DRPreDetalleSolicitud["Cantidad"] = Cantidad;
        //                            DRPreDetalleSolicitud["UnidadesAlisto"] = detalleArticulo.UnidadesAlistoDetalle;
        //                            DRPreDetalleSolicitud["CantidadGtin"] = detalleArticulo.CantidadGtin;
        //                            DRPreDetalleSolicitud["Gtin"] = detalleArticulo.Gtin;

        //                        }
        //                    }
        //                    LimpiarPreDetalleSolicitud();
        //                    CargarPreDetalleSolicitud("", true);
        //                    Mensaje("ok", "El artículo se editó correctamente", "");
        //                    lbUnidadMedida.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                Mensaje("error", "El artículo no existe", "");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    Mensaje("error", "La cantidad no es válida.", "");
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
        //    }
        //}

        protected void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string IdPreLineaDetalleSolicitud = txtIdPreLineaDetalleSolicitud.Text.ToString().Trim();

                foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                {
                    if (DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString() == IdPreLineaDetalleSolicitud)
                    {
                        DRPreDetalleSolicitud.Delete();
                        break;
                    }
                }

                int NumeroLinea = 1;

                foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                {
                    DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"] = NumeroLinea;
                    NumeroLinea++;
                }

                if (DTPreDetalleSolicitud.Rows.Count == 0)
                {
                    btnAgregarSolicitud.Enabled = false;
                }

                LimpiarPreDetalleSolicitud();
                CargarPreDetalleSolicitud("", true);
                Mensaje("ok", "El artículo se eliminó correctamente", "");
                lbUnidadMedida.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnCancelarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarPreDetalleSolicitud();
                CargarPreDetalleSolicitud("", true);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void ddlidArticuloInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string idInterno = ddlidArticuloInterno.SelectedValue.ToString();
            //CargarGTIN(idInterno, ddlGTIN);
            try
            {
                
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                int valor = Convert.ToInt32(ddlidArticuloInterno.SelectedValue.ToString());
                // SQL = "SELECT MA.idArticulo,cast(GT.ConsecutivoGTIN14 as nvarchar(max)) + '-(' + Convert(varchar(100), GT.Cantidad) + ')' + '-' + GT.Nombre as Nombre" +
                //SQL = "SELECT idArticulo,  Nombre FROM Vista_Agrupaciones_GTIN14 where idArticulo = " + valor;
                SQL = "SP_Agrupaciones_GTIN14 " + valor;
                ddlpresentacion.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlpresentacion.DataTextField = "Nombre";
                //ddlpresentacion.DataValueField = "idArticulo";
                ddlpresentacion.DataValueField = "Nombre";
                ddlpresentacion.DataBind();
                ddlpresentacion.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }

         

            //string SQL = "select idArticulo, cast(idArticulo as nvarchar(max)) + '-' + Nombre as Nombre from ADMMaestroArticulo ";

            



            //try
            //{
            //    string idArticuloInterno = ddlidArticuloInterno.SelectedValue.ToString();
            //    e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, 0);//Se obtiene la Unidad de alisto para el artículo seleccionado 
            //    lbUnidadMedida.Text = detalleArticulo.Unidad_Medida;

            //    if (detalleArticulo.UnidadesAlistoDetalle.Equals("Sin unidad alisto asignada") && detalleArticulo.Granel == false)//Valida si tiene una unidad de alisto por defecto asignada
            //    {
            //       // Mensaje("error", "El artículo seleccionado no tiene definida una [unidad de alisto por defecto]", "");
            //    }                
            //}
            //catch (Exception)
            //{
            //    lbUnidadMedida.Text = "";
            //}
        }

        //protected void ddlpresentacion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
          

        //}

            #endregion

            #region "Validacion PreDetalleSolicitud"
            private bool EsValidoElArticuloAInsertarEnDetalleSolicitud(decimal cantidadIngresada, e_OPESALPreDetalleSolicitudArticulo detalleArticulo)
        {

            bool esCorrectoElArticulo = false;
            if (detalleArticulo.Granel == true)//Artículos granel
            {
                if (cantidadIngresada < 0)
                {
                    Mensaje("error", "La cantidad ingresada debe ser mayor a cero", "");
                }
                else
                {
                    return true;
                }
            }
            else//Artículos no granel
            {
                //if (detalleArticulo.UnidadesAlistoDetalle.Equals("Sin unidad alisto asignada") && detalleArticulo.Granel == false)//Valida si tiene una unidad de alisto por defecto asignada
                //{
                //    Mensaje("error", "El artículo seleccionado no tiene definida una [unidad de alisto por defecto]", "");
                //}
                //else
                if (cantidadIngresada < detalleArticulo.CantidadUnidadMedida && detalleArticulo.UnidadesAlisto > 1)//Si la cantidad ingresada esta en medio de la cantidad mínima y otra posibleme cantidad de alisto
                {
                    Mensaje("error", "La cantidad ingresada es: " + cantidadIngresada.ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] .La cantidad de alisto anterior es: " + (detalleArticulo.CantidadUnidadMedida / (detalleArticulo.UnidadesAlisto) * (detalleArticulo.UnidadesAlisto - 1)).ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] y la siguiente es: " + detalleArticulo.CantidadUnidadMedida + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "]", "");
                }
                else if (cantidadIngresada < detalleArticulo.CantidadUnidadMedida && detalleArticulo.UnidadesAlisto == 1)//Si la cantidad ingresada es menor a la mínima que se puede alistar
                {
                    Mensaje("error", "La cantidad ingresada es: " + cantidadIngresada.ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] y es menor a la mínima de alisto que es: " + detalleArticulo.CantidadUnidadMedida + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "]", "");
                }
                else
                {
                    esCorrectoElArticulo = true;
                }
            }
            return esCorrectoElArticulo;
        }

        private bool EsValidaLaEdicionDelArticulo(decimal cantidadIngresada, e_OPESALPreDetalleSolicitudArticulo detalleArticulo)
        {
            bool esCorrectoElArticulo = false;
            if (detalleArticulo.Granel == true)//Artículos granel
            {
                if (cantidadIngresada < 0)
                {
                    Mensaje("error", "La cantidad ingresada debe ser mayor a cero", "");
                }
                else
                {
                    return true;
                }
            }
            else//Artículos no granel
            {
                if (cantidadIngresada < detalleArticulo.CantidadUnidadMedida && detalleArticulo.UnidadesAlisto > 1)//Si la cantidad ingresada esta en medio de la cantidad mínima y otra posibleme cantidad de alisto
                {
                    Mensaje("error", "La cantidad ingresada es: " + cantidadIngresada.ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] .La cantidad posible de alisto anterior es: " + (detalleArticulo.CantidadUnidadMedida / (detalleArticulo.UnidadesAlisto) * (detalleArticulo.UnidadesAlisto - 1)).ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] y la siguiente es: " + detalleArticulo.CantidadUnidadMedida + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "]", "");
                }
                else if (cantidadIngresada < detalleArticulo.CantidadUnidadMedida && detalleArticulo.UnidadesAlisto == 1)//Si la cantidad ingresada es menor a la mínima que se puede alistar
                {
                    Mensaje("error", "La cantidad ingresada es: " + cantidadIngresada.ToString() + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "] y es menor a la mínima de alisto que es: " + detalleArticulo.CantidadUnidadMedida + " [" + detalleArticulo.Unidad_Medida.Split('-')[1] + "]", "");
                }
                else
                {
                    esCorrectoElArticulo = true;
                }
            }
            return esCorrectoElArticulo;
        }

        private bool EsValidaCantidadArticulo(string cantidadIngresada)
        {
            try
            {
                float cantidad = float.Parse(cantidadIngresada, CultureInfo.InvariantCulture.NumberFormat);
                if (cantidad > 0)
                {
                    txtCantidad.Text = "";
                    return true;
                }
                else
                {
                    txtCantidad.Text = "";
                    Mensaje("info", "Por favor ingrese una cantidad mayor a cero", "");
                    return false;
                }
            }
            catch (Exception)
            {
                Mensaje("info", "Por favor ingrese una cantidad válida, la misma no debe contener caracteres especiales, ni letras", "");
                txtCantidad.Text = "";
                return false;
            }
        } //Verifica si la cantidad ingresada es posible convertirla a decimal
        #endregion

        #region "Grid PreDetalleSolicitud"

        private void CargarPreDetalleSolicitud(string buscar, bool pestana)
        {
            try
            {
                RadGridOPESALPreDetalleSolicitud.DataSource = DTPreDetalleSolicitud;
                if (pestana)
                {
                    RadGridOPESALPreDetalleSolicitud.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                //int valor = Convert.ToInt32(ddlidArticuloInterno.SelectedValue.ToString());
                // SQL = "SELECT MA.idArticulo,cast(GT.ConsecutivoGTIN14 as nvarchar(max)) + '-(' + Convert(varchar(100), GT.Cantidad) + ')' + '-' + GT.Nombre as Nombre" +
                //SQL = "SELECT idArticulo,  Nombre FROM Vista_Agrupaciones_GTIN14 where idArticulo = " + valor;
                SQL = "SP_BusquedaArticulo_EnSolicitud '" + TxtReferencia.Text + "'";
                ddlidArticuloInterno.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlidArticuloInterno.DataTextField = "Nombre";
                //ddlpresentacion.DataValueField = "idArticulo";
                ddlidArticuloInterno.DataValueField = "idArticuloInterno";
                ddlidArticuloInterno.DataBind();
                ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }


            protected void RadGridOPESALPreDetalleSolicitud_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarPreDetalleSolicitud("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridOPESALPreDetalleSolicitud_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtIdPreLineaDetalleSolicitud.Text = item["IdPreLineaDetalleSolicitud"].Text.Replace("&nbsp;", "");
                    ddlidArticuloInterno.SelectedValue = item["idArticuloInterno"].Text.Replace("&nbsp;", "");
                    txtCantidad.Text = item["Cantidad"].Text.Replace("&nbsp;", "");

                    btnAgregarArticulo.Visible = false;
                    btnEditarArticulo.Visible = true;
                    lblSeparador.Visible = true;
                    btnEliminarArticulo.Visible = true;
                    lblSeparador2.Visible = true;
                    btnCancelarArticulo.Visible = true;
                    lbUnidadMedida.Text = "";
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        #endregion

        #region "Métodos PreDetalleSolicitud"

        private void LimpiarPreDetalleSolicitud()
        {
            txtIdPreLineaDetalleSolicitud.Text = "";
            ddlidArticuloInterno.SelectedValue = "--Seleccionar--";
            ddlpresentacion.SelectedValue = "--Seleccionar--";
            txtCantidad.Text = "";

            btnAgregarArticulo.Visible = true;
            btnEditarArticulo.Visible = false;
            lblSeparador.Visible = false;
            btnEliminarArticulo.Visible = false;
            lblSeparador2.Visible = false;
            btnCancelarArticulo.Visible = false;
        }

        private void CargarGTIN(String idInterno, DropDownList DDL)
        {
            //DDL.Items.Clear();
            //DataSet DSTablas = new DataSet();
            //string SQL = "SELECT GTIN,idarticulo FROM Vista_Articulos WHERE txtIdCodInterno = " + idInterno;
            //DSTablas = n_ConsultaDummy.GetDataSet(SQL, "0");
            //if (DSTablas.Tables[0].Rows.Count > 1)  // Carga los GTINes del artículo.
            //{
            //    DDL.DataSource = DSTablas;
            //    DDL.DataTextField = "GTIN";
            //    DDL.DataValueField = "idarticulo";
            //    DDL.DataBind();
            //    DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
            //}
            //else
            //{
            //    DDL.DataSource = DSTablas;
            //    DDL.DataTextField = "GTIN";
            //    DDL.DataValueField = "idarticulo";
            //    DDL.DataBind();
            //    DDL.SelectedIndex = 0;
            //}
        }

        #endregion

        #endregion

    }
}