using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Mantenimiento.Promociones
{
    public partial class wf_RegistroPromociones : System.Web.UI.Page
    {
        public static DataTable DTPromociones = new DataTable();
        public static DataTable DTDetalleEdicionPromo = new DataTable();
        e_Usuario UsrLogged = new e_Usuario();
        n_WMS wms = new n_WMS();
        string Pagina = "";
        string GtinEdicionPromo;

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
                TipoDeArticulo();
                ObtenerListadoProveedores();
                // RadGridOPESALPreDetalleSolicitud.GroupingSettings.CaseSensitive = false;

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

        private void CrearDSPreDetalleSolicitud()
        {
            try
            {
                if (!DTPromociones.Columns.Contains("IdPreLineaDetalleSolicitud"))
                {
                    DTPromociones.Columns.Add("IdPreLineaDetalleSolicitud", typeof(string));
                    DTPromociones.Columns.Add("idArticuloInterno", typeof(string));
                    DTPromociones.Columns.Add("NombreArticuloInterno", typeof(string));
                    DTPromociones.Columns.Add("Cantidad", typeof(string));
                    DTPromociones.Columns.Add("GTIN", typeof(string));
                    DTPromociones.Columns.Add("TipoArticulo", typeof(string));


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

        private void CargarDDLS()
        {
            try
            {

                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");


            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-SAL-000004" + ex.Message, "");
            }
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


                SQL = "SP_ObtieneListadoProveedoresPromocionBusqueda " + "'" + TxtBusquedaProveedor.Text + "'";
                ddlProveedor.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "IdProveedor";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ha ocurrido un Error." + ex.Message, "");
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


                SQL = "SP_BusquedaArticulo_EnSolicitud '" + TxtReferencia.Text + "'";
                ddlidArticuloInterno.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlidArticuloInterno.DataTextField = "Nombre";
                ddlidArticuloInterno.DataValueField = "idArticuloInterno";
                ddlidArticuloInterno.DataBind();
                ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        private void LimpiaTipo()
        {
            ddlTipo.Items.Remove("--Seleccione--");
            ddlTipo.Items.Remove("Regalia");
            ddlTipo.Items.Remove("Descuento");
        }

        private void TipoDeArticulo()
        {

            ddlTipo.Items.Insert(0, new ListItem("--Seleccione--", null));
            ddlTipo.Items.Insert(1, new ListItem("Regalia", "Regalia"));
            ddlTipo.Items.Insert(2, new ListItem("Descuento", "Descuento"));


        }

        public void ObtenerListadoProveedores()
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "SP_ObtieneListadoProveedoresPromocion";
                ddlProveedor.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "IdProveedor";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {

                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnBusquedaPromocion_Click(object sender, EventArgs e)
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "ObtenerIdArticulo " + TxtCodigoPromo.Text + "";
                ddPromociones.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddPromociones.DataTextField = "Nombre";
                ddPromociones.DataValueField = "idInterno";
                ddPromociones.DataBind();
                ddPromociones.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void ddPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PanelDatosIngreso.Visible = false;

                PanelDatoEdicion.Visible = false;

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                SQL = "EXEC SP_ValidarExistenciaPromocion '" + ddPromociones.SelectedValue + "'";
                var validacion = n_ConsultaDummy.GetUniqueValue(SQL, UsrLogged.IdUsuario);

                if (validacion == "0")
                {

                    PanelDatosIngreso.Visible = true;
                }
                else
                {
                    PanelDatoEdicion.Visible = true;
                    PanelDetalleEncabezado.Visible = false;
                    CargaEncabezadoPromocion();


                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        public void CargaPresentacio(int valor)
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


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
        }

        protected void ddlidArticuloInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                int valor = Convert.ToInt32(ddlidArticuloInterno.SelectedValue.ToString());

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
        }

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
                string tipo = ddlTipo.SelectedValue;
                LabelGtin.Text = gtin_leido_v2;


                if (tipo == null || tipo == "--Seleccione--")
                {
                    Mensaje("info", "Debe seleccionar el tipo de artículo", "");
                }
                else
                {
                    if (ddlidArticuloInterno.SelectedIndex == 0)//Valida si se seleccionó un artículo
                    {
                        Mensaje("info", "Debe seleccionar un artículo", "");

                    }
                    else if (EsValidaCantidadArticulo(Cantidad))//Se valida la cantidad ingresada sea valida               
                    {

                        bool existeArticulo = false;

                        foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
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
                            int NumeroLinea = 1 + (DTPromociones.Rows.Count);
                            decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                            e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, cantidadIngresada, gtin_leido_v2);//Se obtienen los detalles del idInterno articulo seleleccionado
                                                                                                                                                                                                                                           //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto 
                            if (true)
                            {
                                DTPromociones.Rows.Add(NumeroLinea, idArticuloInterno, NombreArticuloInterno, Cantidad, detalleArticulo.Gtin, tipo);

                                LimpiarPreDetalleSolicitud();
                                CargarPreDetalleSolicitud("", true);
                                Mensaje("ok", "El artículo se ingresó correctamente", "");
                                btnAgregarSolicitud.Enabled = true;

                                TxtReferencia.Text = "";

                                limpia_agregaarticulo();
                                LimpiaTipo();
                                TipoDeArticulo();

                            }
                        }
                        else
                        {
                            Mensaje("error", "El artículo ya existe", "");
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");

            }
        }

        private void limpia_agregaarticulo()
        {
            string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));

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
        private void LimpiarPreDetalleSolicitud()
        {
            txtIdPreLineaDetalleSolicitud.Text = "";
            ddlidArticuloInterno.SelectedValue = "--Seleccionar--";
            CargaPresentacio(0);
            txtCantidad.Text = "";

            btnAgregarArticulo.Visible = true;
            btnEditarArticulo.Visible = false;
            lblSeparador.Visible = false;
            btnEliminarArticulo.Visible = false;
            lblSeparador2.Visible = false;
            btnCancelarArticulo.Visible = false;
        }

        private void CargarPreDetalleSolicitud(string buscar, bool pestana)
        {
            try
            {
                RGPromociones.DataSource = DTPromociones;
                if (pestana)
                {
                    RGPromociones.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridPromociones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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

        protected void RadGridPromociones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtIdPreLineaDetalleSolicitud.Text = item["IdPreLineaDetalleSolicitud"].Text.Replace("&nbsp;", "");
                    ddlidArticuloInterno.SelectedValue = item["idArticuloInterno"].Text.Replace("&nbsp;", "");
                    CargaPresentacio(Convert.ToInt32(ddlidArticuloInterno.SelectedValue));
                    ddlidArticuloInterno.SelectedValue = ddlidArticuloInterno.SelectedValue;
                    txtCantidad.Text = item["Cantidad"].Text.Replace("&nbsp;", "");
                    ddlTipo.SelectedValue = item["TipoArticulo"].Text.Replace("&nbsp;", "");

                    btnAgregarArticulo.Visible = false;
                    btnEditarArticulo.Visible = true;
                    lblSeparador.Visible = true;
                    btnEliminarArticulo.Visible = true;
                    lblSeparador2.Visible = true;
                    btnCancelarArticulo.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnEditarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string IdPreLineaDetalleSolicitud = txtIdPreLineaDetalleSolicitud.Text.ToString().Trim();
                string idArticuloInterno = ddlidArticuloInterno.SelectedValue;
                string NombreArticuloInterno = ddlidArticuloInterno.SelectedItem.Text;
                string Cantidad = txtCantidad.Text.ToString().Trim().Replace(",", ".");
                string tipo = ddlTipo.SelectedValue.ToString();
                bool existeArticulo = false;

                string codigo_leido = ddlpresentacion.SelectedItem.ToString();
                string[] textos = codigo_leido.Split('-');

                string gtin_leido_v2 = textos[0];


                if (EsValidaCantidadArticulo(Cantidad))//Valida que la cantidad editada sea válida
                {
                    foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
                    {
                        if (DRPreDetalleSolicitud["idArticuloInterno"].ToString() == idArticuloInterno)
                        {
                            existeArticulo = true;
                        }
                    }

                    //if (EsNumeroEntero(Cantidad))
                    //{
                    if (existeArticulo)//Verifica que el artículo ya esté en el grid
                    {
                        decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                        e_OPESALPreDetalleSolicitudArticulo detalleArticulo = n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(wms.getIdCompania(UsrLogged.IdUsuario), idArticuloInterno, cantidadIngresada, gtin_leido_v2);//Se obtienen los detalles del idInterno articulo seleleccionado
                        //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto 
                        if (true)
                        {
                            foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
                            {
                                if (DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString() == IdPreLineaDetalleSolicitud)
                                {
                                    DRPreDetalleSolicitud["idArticuloInterno"] = idArticuloInterno;
                                    DRPreDetalleSolicitud["NombreArticuloInterno"] = NombreArticuloInterno;
                                    DRPreDetalleSolicitud["Cantidad"] = Cantidad;
                                    DRPreDetalleSolicitud["Gtin"] = detalleArticulo.Gtin;
                                    DRPreDetalleSolicitud["TipoArticulo"] = tipo;

                                }
                            }
                            LimpiarPreDetalleSolicitud();
                            CargarPreDetalleSolicitud("", true);
                            LimpiaTipo();
                            TipoDeArticulo();
                            Mensaje("ok", "El artículo se editó correctamente", "");

                        }
                    }
                    else
                    {
                        Mensaje("error", "El artículo no existe", "");
                    }

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string IdPreLineaDetalleSolicitud = txtIdPreLineaDetalleSolicitud.Text.ToString().Trim();

                foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
                {
                    if (DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString() == IdPreLineaDetalleSolicitud)
                    {
                        DRPreDetalleSolicitud.Delete();
                        break;
                    }
                }

                int NumeroLinea = 1;

                foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
                {
                    DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"] = NumeroLinea;
                    NumeroLinea++;
                }

                if (DTPromociones.Rows.Count == 0)
                {
                    btnAgregarSolicitud.Enabled = false;
                }

                LimpiarPreDetalleSolicitud();
                CargarPreDetalleSolicitud("", true);
                Mensaje("ok", "El artículo se eliminó correctamente", "");

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

        protected void btnAgregarPromocion_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTPromociones.Rows.Count > 0 && !string.IsNullOrEmpty(ddPromociones.SelectedValue))
                {
                    string SQL = "";
                    //string idDestino = ddlidDestino.SelectedValue;


                    string IdCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                    string idUsuario = UsrLogged.IdUsuario;
                    string idInterno = ddPromociones.SelectedValue;
                    string Nombre = ddPromociones.SelectedItem.ToString();
                    int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
                    SQL = "EXEC SP_InsertarEncabezadoPromocion '" + idInterno + "','" + Nombre + "'," + idProveedor + "";
                    string idMaestroPromocion = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    if (!string.IsNullOrEmpty(idMaestroPromocion))
                    {
                        foreach (DataRow DRPreDetalleSolicitud in DTPromociones.Rows)
                        {
                            string numLinea = DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString();
                            string idArticuloInterno = DRPreDetalleSolicitud["idArticuloInterno"].ToString();
                            string NombreArticuloInterno = DRPreDetalleSolicitud["NombreArticuloInterno"].ToString();
                            string Cantidad = DRPreDetalleSolicitud["Cantidad"].ToString();
                            string gtin = DRPreDetalleSolicitud["Gtin"].ToString();
                            string tipo = DRPreDetalleSolicitud["TipoArticulo"].ToString();


                            SQL = "EXEC SP_InsercionDetallePromocion '" + idMaestroPromocion + "','" + idArticuloInterno + "','" + NombreArticuloInterno + "','" + Cantidad + "','" + gtin + "','" + tipo + "'";
                            n_ConsultaDummy.PushData(SQL, idUsuario);
                        }

                        DTPromociones.Clear();
                        LimpiarSolicitud();
                        LimpiarPreDetalleSolicitud();
                        CargarPreDetalleSolicitud("", true);
                        LimpiaIngreso();
                        TipoDeArticulo();
                        //TxtSolicitud.Text = "Solicitud N° " + idMaestroSolicitud;
                        Mensaje("ok", "Promocion Creada, N° " + idMaestroPromocion, "");
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

            ddPromociones.SelectedValue = "--Seleccionar--";

            btnAgregarSolicitud.Enabled = false;
            btnAgregarSolicitud.Visible = false;
        }

        private void LimpiaIngreso()
        {
            TxtCodigoPromo.Text = "";
            PanelDatosIngreso.Visible = false;
        }

        private void CargaEncabezadoPromocion()
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


                SQL = "SP_ConsultarEncabezadoPromociones '" + ddPromociones.SelectedValue + "'";
                RGMaestroPromociones.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                RGMaestroPromociones.DataBind();

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RGMaestroPromociones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                GridDataItem item = (GridDataItem)e.Item;

                string idMaestroPromo = item["IdMaestroPromocion"].Text.Replace("&nbsp;", "");
                if (e.CommandName == "btnVerDetalle")
                {
                   

                    SQL = "SP_ConsultarDetallePromocion '" + idMaestroPromo + "'";
                    RGDetallePromo.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                    RGDetallePromo.DataBind();

                    PanelDetalleEncabezado.Visible = true;

                }
                else if (e.CommandName == "btnEliminar")
                {
                    SQL = "SP_EliminaPromocionEncabezado " + Convert.ToInt32(idMaestroPromo) + "";
                    n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                    Mensaje("ok", "Promocion eliminada " , "");
                    PanelDatoEdicion.Visible = false;
                    TxtReferencia.Text = "";
                   

                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RGDetallePromo_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnEditarPromo")
                {
                    PanelModificacionArticulo.Visible = true;
                    GridDataItem item = (GridDataItem)e.Item;

                    llenaFormulario();
                    TxtDetallePromo.Text = item["idDetallePromocion"].Text.Replace("&nbsp;", "");



                    string valor = item["idInternoSAP"].Text.Replace("&nbsp;", "");
                    ddlEdicionPromoArticulo.SelectedValue = valor;
                    CargaPresentacio2(Convert.ToInt32(ddlEdicionPromoArticulo.SelectedValue));
                    ddlEdicionPromoArticulo.SelectedValue = ddlEdicionPromoArticulo.SelectedValue;
                    TxtEdicionPromoCantidad.Text = item["Cantidad"].Text.Replace("&nbsp;", "");
                    ddlTipo.SelectedValue = item["TipoArticulo"].Text.Replace("&nbsp;", "");

                }
                else if (e.CommandName == "btnEliminarPromo")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    TxtDetallePromo.Text = item["idDetallePromocion"].Text.Replace("&nbsp;", "");

                    n_WMS wms = new n_WMS();
                    DataSet DSDatos = new DataSet();
                    string SQL = "";
                    string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


                    SQL = "SP_EliminarDetallePromo " + TxtDetallePromo.Text + "";

                    var consulta = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                    PanelDetalleEncabezado.Visible = false;
                    PanelModificacionArticulo.Visible = false;
                    Mensaje("ok", "Se elimino el Articulo existosamente", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        public void CargaPresentacio2(int valor)
        {
            try
            {

                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


                SQL = "SP_Agrupaciones_GTIN14 " + valor;
                ddlEdicionPromoPresentacion.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                ddlEdicionPromoPresentacion.DataTextField = "Nombre";
                //ddlpresentacion.DataValueField = "idArticulo";
                ddlEdicionPromoPresentacion.DataValueField = "Nombre";
                ddlEdicionPromoPresentacion.DataBind();
                ddlEdicionPromoPresentacion.Items.Insert(0, new ListItem("--Seleccionar--"));
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void llenaFormulario()
        {
            n_WMS wms = new n_WMS();
            DataSet DSDatos = new DataSet();
            string SQL = "";
            string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


            SQL = "SELECT idArticuloInterno, Nombre FROM Vista_ArticulosInternos ";
            ddlEdicionPromoArticulo.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

            ddlEdicionPromoArticulo.DataTextField = "Nombre";
            ddlEdicionPromoArticulo.DataValueField = "idArticuloInterno";
            ddlEdicionPromoArticulo.DataBind();
            ddlEdicionPromoArticulo.Items.Insert(0, new ListItem("--Seleccionar--"));
        }
        protected void btnCancelarPromo_Click(object sender, EventArgs e)
        {
            PanelModificacionArticulo.Visible = false;
        }

        protected void btnEditarPromo_Click(object sender, EventArgs e)
        {
            try
            {
                string idDetallePromo = TxtDetallePromo.Text.ToString().Trim();

                string NombreArticuloInterno = ddlEdicionPromoArticulo.SelectedItem.Text;
                string idArticulo = ddlEdicionPromoArticulo.SelectedValue;
                string Cantidad = TxtEdicionPromoCantidad.Text.ToString().Trim().Replace(",", ".");
                bool existeArticulo = false;

                string codigo_leido = ddlEdicionPromoPresentacion.SelectedItem.ToString();
                string[] textos = codigo_leido.Split('-');

                string gtin_leido_v2 = textos[0];


                if (EsValidaCantidadArticulo(Cantidad))//Valida que la cantidad editada sea válida
                {


                    //if (EsNumeroEntero(Cantidad))
                    //{

                    decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);

                    n_WMS wms = new n_WMS();
                    DataSet DSDatos = new DataSet();
                    string SQL = "";
                    string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


                    SQL = "SP_EdicionPromocionArticulos " + idDetallePromo + ",'" +
                        idArticulo + "'," + cantidadIngresada + ",'" + gtin_leido_v2 + "','" + ddlTipo.SelectedValue.ToString() + "'";
                    RGDetallePromo.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                    ddlEdicionPromoPresentacion.DataBind();
                    PanelDetalleEncabezado.Visible = false;
                    PanelModificacionArticulo.Visible = false;
                    Mensaje("ok", "Se edito el Articulo existosamente", "");

                    TxtDetallePromo.Text = "";
                    ddlEdicionPromoArticulo.SelectedValue = "--Seleccionar--";
                    CargaPresentacio2(0);
                    TxtEdicionPromoCantidad.Text = "";

                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

       
    }
}
