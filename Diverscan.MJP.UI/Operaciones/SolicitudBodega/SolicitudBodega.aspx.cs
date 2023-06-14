using Diverscan.MJP.AccesoDatos.SolicitudBODEGA;
using Diverscan.MJP.AccesoDatos.Traslados;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Negocio.OPESALPreDetalleSolicitud;
using Diverscan.MJP.Negocio.UsoGeneral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Solicitud_Bodega
{
    public partial class SolicitudBodega : System.Web.UI.Page
    {
        private readonly ISolicitudBodega SolicitudDeBodega;

       
        public SolicitudBodega()
        {
            SolicitudDeBodega = new SolicitudBodegaDBA();
        }


        e_Usuario UsrLogged = new e_Usuario();
        string Pagina = "";
        n_WMS wms = new n_WMS();
        public static DataTable DTPreDetalleSolicitud = new DataTable();
        private e_Articulo articuloData
        {
            get
            {
                var data = ViewState["articuloData"] as e_Articulo;
                if (data == null)
                {
                    data = new e_Articulo();
                    ViewState["articuloData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articuloData"] = value;
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
                CrearDSPreDetalleSolicitud();
                RadGridOPESALPreDetalleSolicitud.GroupingSettings.CaseSensitive = false;
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                _rdpFechaAplicado.SelectedDate = today;
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
                //string[] Msj = n_SmartMaintenance.CargarDDL
                //(ddlidArticuloInterno, e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
                string[] Msj = new string[2] { "Prueba"," esto es una prueba"};
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-SAL-000004" + ex.Message, "");
            }
        }

        
        protected void txtBusquedad_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnBusqueda_Click(sender,e);
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
                //ddlidArticuloInterno.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                //ddlidArticuloInterno.DataTextField = "Nombre";
                //ddlidArticuloInterno.DataValueField = "idArticuloInterno";
                //ddlidArticuloInterno.DataBind();
                //ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));

                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);
                e_Articulo articulo = new e_Articulo();
                if (DSDatos.Tables[0].Rows.Count > 0)
                {
                    articulo.IdArticulo = Convert.ToInt32(DSDatos.Tables[0].Rows[0]["idArticulo"].ToString());
                    articulo.IdInterno = Convert.ToInt32(DSDatos.Tables[0].Rows[0]["idArticuloInterno"].ToString());
                    articulo.GTIN = DSDatos.Tables[0].Rows[0]["GTIN"].ToString();
                    articulo.Nombre = DSDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    txtArticulo.Text = articulo.Nombre;
                    articuloData = articulo;
                    //txtCantidad.Focus();
                }
                else
                    {
                        txtArticulo.Text = "";
                        Mensaje("error", "Producto no encontrado", "Alerta");
                    }                
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

                //articuloData[0] = ddlidArticuloInterno.SelectedValue.ToString();

                //SQL = "SP_Agrupaciones_GTIN14 " + valor;
                //ddlpresentacion.DataSource = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                //ddlpresentacion.DataTextField = "Nombre";
                //ddlpresentacion.DataValueField = "idArticulo";
                //ddlpresentacion.DataValueField = "Nombre";
                //ddlpresentacion.DataBind();
                //ddlpresentacion.Items.Insert(0, new ListItem("--Seleccionar--"));              
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
                txtArticulo.Text = ""+articuloData.Nombre;                
                   
                if (String.IsNullOrEmpty(txtArticulo.Text.ToString()))//Valida si se encontro un artículo
                {
                    Mensaje("info", "Debe buscar un artículo valido", "");
                    lbUnidadMedida.Text = "";
                }
                else if (!String.IsNullOrEmpty(txtCantidad.Text.ToString()))//Se valida la cantidad ingresada sea valida               
                {
                    string Cantidad = txtCantidad.Text.ToString().Trim().Replace(",", ".");
                    bool existeArticulo = false;

                    foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                    {
                        if (DRPreDetalleSolicitud["idArticuloInterno"].ToString() == ""+articuloData.IdInterno)
                        {
                            existeArticulo = true;
                        }

                        if (DRPreDetalleSolicitud["GTIN"].ToString() == articuloData.GTIN)
                        {
                            existeArticulo = true;
                        }
                    }
                    if (!existeArticulo)
                    {
                        int NumeroLinea = 1 + (DTPreDetalleSolicitud.Rows.Count);
                        decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                        e_OPESALPreDetalleSolicitudArticulo detalleArticulo = 
                            n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno(
                                wms.getIdCompania(UsrLogged.IdUsuario), "" + articuloData.IdInterno,
                                cantidadIngresada, articuloData.GTIN);
                        //Se obtienen los detalles del idInterno articulo seleleccionado
                        //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada,
                        //detalleArticulo))//Valida que el artículo a agregar sea correcto                        
                            DTPreDetalleSolicitud.Rows.Add(NumeroLinea, "" + articuloData.IdInterno, articuloData.Nombre,
                                Cantidad, detalleArticulo.Unidad_Medida, detalleArticulo.UnidadesAlistoDetalle,
                                detalleArticulo.CantidadGtin, detalleArticulo.Gtin);

                            LimpiarPreDetalleSolicitud();
                            CargarPreDetalleSolicitud("", true);
                            Mensaje("ok", "El artículo se ingresó correctamente", "");
                            btnAgregarSolicitud.Enabled = true;
                            lbUnidadMedida.Text = "";
                            TxtReferencia.Text = "";
                            txtArticulo.Text = "";    
                            limpia_agregaarticulo();                       
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

        private void limpia_agregaarticulo()
        {
            //string[] Msj = n_SmartMaintenance.CargarDDL(ddlidArticuloInterno,
            //e_TablasBaseDatos.VistaArticulosInternos(), UsrLogged.IdUsuario, true);
            string[] Msj = new string[2] { "prueba", " esto es una prueba" };
            if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
            //ddlidArticuloInterno.Items.Insert(0, new ListItem("--Seleccionar--"));
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
                Mensaje("info", "Por favor ingrese una cantidad válida, " +
                    "la misma no debe contener caracteres especiales, ni letras", "");
                txtCantidad.Text = "";
                return false;
            }
        } //Verifica si la cantidad ingresada es posible convertirla a decimal
        private void LimpiarPreDetalleSolicitud()
        {
            txtIdPreLineaDetalleSolicitud.Text = "";
            //ddlidArticuloInterno.SelectedValue = "--Seleccionar--";
            //ddlpresentacion.SelectedValue = "--Seleccionar--";
            txtCantidad.Text = "";
            txtArticulo.Text = "";

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

        protected void RadGridOPESALPreDetalleSolicitud_NeedDataSource(object sender,
            Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
                    articuloData.IdInterno = Convert.ToInt32(item["idArticuloInterno"].Text.Replace("&nbsp;", ""));
                    articuloData.GTIN = item["GTIN"].Text.Replace("&nbsp;", "");
                    articuloData.Nombre = item["NombreArticuloInterno"].Text.Replace("&nbsp;", "");
                    txtArticulo.Text = articuloData.Nombre;
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

        protected void btnEditarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string IdPreLineaDetalleSolicitud = txtIdPreLineaDetalleSolicitud.Text.ToString().Trim();
               
                string Cantidad = txtCantidad.Text.ToString().Trim().Replace(",", ".");
                bool existeArticulo = false;              
                //string[] textos = codigo_leido.Split('-');               

                if (EsValidaCantidadArticulo(Cantidad))//Valida que la cantidad editada sea válida
                {
                    foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                    {
                        if (DRPreDetalleSolicitud["idArticuloInterno"].ToString() == articuloData.IdInterno.ToString())
                        {
                            existeArticulo = true;
                        }
                    }

                    //if (EsNumeroEntero(Cantidad))
                    //{
                    if (existeArticulo)//Verifica que el artículo ya esté en el grid
                    {
                        decimal cantidadIngresada = decimal.Parse(Cantidad, CultureInfo.InvariantCulture.NumberFormat);
                        e_OPESALPreDetalleSolicitudArticulo detalleArticulo = 
                            n_OPESALPreDetalleSolicitud.GetDetallesArticuloPorIdInterno( 
                                wms.getIdCompania(UsrLogged.IdUsuario), ""+articuloData.IdInterno, 
                                cantidadIngresada, articuloData.GTIN);//Se obtienen los detalles del idInterno articulo seleleccionado
                        //if (EsValidoElArticuloAInsertarEnDetalleSolicitud(cantidadIngresada, detalleArticulo))//Valida que el artículo a agregar sea correcto                        
                            foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
                            {
                                if (DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString() == IdPreLineaDetalleSolicitud)
                                {
                                    DRPreDetalleSolicitud["idArticuloInterno"] = articuloData.IdInterno;
                                    DRPreDetalleSolicitud["NombreArticuloInterno"] = articuloData.Nombre;
                                    DRPreDetalleSolicitud["Cantidad"] = Cantidad;
                                    DRPreDetalleSolicitud["UnidadesAlisto"] = detalleArticulo.UnidadesAlistoDetalle;
                                    DRPreDetalleSolicitud["CantidadGtin"] = detalleArticulo.CantidadGtin;
                                    DRPreDetalleSolicitud["Gtin"] = detalleArticulo.Gtin;

                                }
                            }
                            LimpiarPreDetalleSolicitud();
                            CargarPreDetalleSolicitud("", true);
                            Mensaje("ok", "El artículo se editó correctamente", "");
                            lbUnidadMedida.Text = "";                       
                    }
                    else
                    {
                        Mensaje("error", "El artículo no existe", "");
                    }
                    //}
                    //else
                    //{
                    //    Mensaje("error", "La cantidad no es válida.", "");
                    //}
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

        protected void btnAgregarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTPreDetalleSolicitud.Rows.Count > 0 && !string.IsNullOrEmpty(txtNombre.Text))
                {
                    string SQL = "";
                    //string idDestino = ddlidDestino.SelectedValue;
                    string Nombre = txtNombre.Text.ToString().Trim();
                    string Comentarios = txtComentarios.Text.ToString().Trim();
                    string idDestino = "2";

                    string IdCompania = wms.getIdCompania(UsrLogged.IdUsuario);
                    string idUsuario = UsrLogged.IdUsuario;
                    string idInterno = "";
                    var date = _rdpFechaAplicado.SelectedDate ?? DateTime.Now;
                    string idInternoSAP = "";

                    string idMaestroSolicitud = SolicitudDeBodega.InsertarPreMaestro(Convert.ToInt32(idUsuario), Nombre, Comentarios, IdCompania,
                        idDestino, idInterno, idInternoSAP, date);
                    if (idMaestroSolicitud != null || idMaestroSolicitud != "")
                    {
                        IngresarDetalle(idDestino, IdCompania, idUsuario, idMaestroSolicitud);
                        Mensaje("ok", "Solicitud creada correctamente, N°  " + idMaestroSolicitud, "");
                        DTPreDetalleSolicitud.Clear();
                        LimpiarSolicitud();
                        LimpiarPreDetalleSolicitud();
                        CargarPreDetalleSolicitud("", true);
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

        private void IngresarDetalle(string idDestino, string IdCompania, string idUsuario, string idMaestroSolicitud)
        {
            foreach (DataRow DRPreDetalleSolicitud in DTPreDetalleSolicitud.Rows)
            {
                string numLinea = DRPreDetalleSolicitud["IdPreLineaDetalleSolicitud"].ToString();
                string idArticuloInterno = DRPreDetalleSolicitud["idArticuloInterno"].ToString();
                string NombreArticuloInterno = DRPreDetalleSolicitud["NombreArticuloInterno"].ToString();
                string Cantidad = DRPreDetalleSolicitud["Cantidad"].ToString();
                string gtin = DRPreDetalleSolicitud["Gtin"].ToString();
                string Descripcion = "";

                SolicitudDeBodega.InsertarPreDetalle(NombreArticuloInterno, Convert.ToInt32(idMaestroSolicitud), idDestino, idArticuloInterno, Convert.ToDecimal(Cantidad), Descripcion,
                IdCompania, Convert.ToInt32(idUsuario), Convert.ToInt32(numLinea), gtin);
            }
        }

        private void LimpiarSolicitud()
        {
            txtNombre.Text = "";
            txtComentarios.Text = "";
            btnAgregarSolicitud.Enabled = false;
        }

        protected void _rdpFechaAplicado_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (_rdpFechaAplicado.SelectedDate < DateTime.Now)
            {
                Mensaje("info", "La fecha seleccionada es menor a la del día de hoy, intente nuevamente", "");
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                _rdpFechaAplicado.SelectedDate = today;
                return;
            }
        }
    }
}