using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class wf_Kardex_V2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            } else
            {
                IdBodega = _eUsuario.IdBodega;
            }

            if (!IsPostBack)
            {
                loadArticulos();               
                inicializacionElementos();              
            }
        }

        private int IdBodega
        {
            get
            {
                var idBodega = -1;
                var data = ViewState["IdBodega "];
                if (data != null)
                {
                    var result = int.TryParse(data.ToString(), out idBodega);
                    if (result)
                        ViewState["IdBodega "] = idBodega;
                }
                return idBodega;
            }
            set
            {
                ViewState["IdBodega "] = value;
            }
        }

        #region "Objetos Requeridos y variables"
        private n_KardexReportes _kardexReportes = new n_KardexReportes();
        private static bool seCargaronDatos = false;
        #endregion

        #region "Variables de Funcionamiento"
        private static string idSAPArticuloSeleccionado; //Permite almacenar el IdInterno del Artículo Seleccionado
        private static string nombreArticuloSeleccionado;
        private static string loteSeleccionado;
        private static long idArticuloSeleccionado;
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        private static string totalGlobal;
        private static string unidadMedidaTotalGlobal;
        private static string fechaActualTexto; //dd-mm-yyyy       

        #endregion

        #region "Botones de búsqueda"
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            //Se obtiene el IdArticulo para ejecutar las consultas
            if (validarFechasRangoParaCargarGrid())
            {                
                lbIdArticuloTID.Text = "";                
                idArticuloSeleccionado = Convert.ToInt64(ddlIDArticulo.SelectedValue);
                if (ChkFiltrarPorLote.Checked == false)
                {
                    cargarLotesDDL();
                }
                seCargaronDatos = false;
                cargarGridTrazabilidadArticulo(ChkFiltrarPorLote.Checked);
                cargarGridAjustesInventario(ChkFiltrarPorLote.Checked);
                cargarGridDespachosArticulo(ChkFiltrarPorLote.Checked);
                if (seCargaronDatos)
                {
                    Mensaje("ok", "Datos cargados correctamente", "");
                }
                else
                {
                    Mensaje("info", "No se encontraron datos para este artículo", "");
                }
            }       
        }


        #endregion

        #region "WebForm"


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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
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

        //Visualización de elementos
        protected void chkVerTrazabilidad_CheckedChanged(object sender, EventArgs e)
        {
            PanelGridTrazabilidadBodega.Visible = chkVerTrazabilidad.Checked;
        }

        protected void chkVerAjustesInventario_CheckedChanged(object sender, EventArgs e)
        {
            PanelAjustesInventario.Visible = chkVerAjustesInventario.Checked;
        }

        protected void chkVerDespachos_CheckedChanged(object sender, EventArgs e)
        {
            PanelDespachosArticulo.Visible = chkVerDespachos.Checked;
        }

        private void inicializacionElementos()
        {
            chkVerAjustesInventario.Checked = true;
            chkVerDespachos.Checked = true;
            chkVerTrazabilidad.Checked = true;
            lbIdArticuloTID.Text = "";
            lbIdArticuloSAP.Text = "";

            try
            {
                RDPFechaFinal.SelectedDate = DateTime.Now;
                RDPFechaInicial.SelectedDate = DateTime.Now;
            }
            catch (Exception) { }


        }
        private void inicializarVariables()
        {
            idSAPArticuloSeleccionado = ""; //Permite almacenar el IdInterno del Artículo Seleccionado
            loteSeleccionado = "";
            idArticuloSeleccionado = -1;
            fechaInicioSeleccionada = DateTime.Now;
            fechaFinSeleccionada = DateTime.Now;
        }
        private bool validarFechasRangoParaCargarGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                Mensaje("error", "Las fechas ingresadas tienen un formato incorrecto", "");
                return false;
            }
        }
        private bool validarFechasRangoParaNeedDataGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void ddlIDArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLotesDDL();
            nombreArticuloSeleccionado = ddlIDArticulo.SelectedItem.ToString();
        }

        //Filtrar por lote las consultas
        protected void ChkFiltrarPorLote_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFiltrarPorLote.Checked)
            {
                Mensaje("info", "Filtro por lote activado", "");
                cargarGridTrazabilidadArticulo(ChkFiltrarPorLote.Checked);
                cargarGridAjustesInventario(ChkFiltrarPorLote.Checked);
                cargarGridDespachosArticulo(ChkFiltrarPorLote.Checked);
                //numeroOCLote = _kardexReportes.ObtenerNumeroOC(Convert.ToInt64(ddlIDArticulo.SelectedValue), ddlLotes.SelectedValue);
                //lbNumeroOC.Text = numeroOCLote;
            }
            else
            {
                Mensaje("info", "Filtro por lote desactivado", "");
                lbIdArticuloTID.Text = "";
                cargarGridTrazabilidadArticulo(ChkFiltrarPorLote.Checked);
                cargarGridAjustesInventario(ChkFiltrarPorLote.Checked);
                cargarGridDespachosArticulo(ChkFiltrarPorLote.Checked);
                //numeroOCLote = "";                
            }
        }

        #endregion

        #region "Exportar a EXCEL"

        //-->Trazabilidad Bodega || Movimientos Internos
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (_TrazabilidadBodegaArticulos.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_TrazabilidadBodegaArticulos));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[9];
                propertiesSelected[0] = properties.Find("Lote", false);
                propertiesSelected[1] = properties.Find("FechaVencimientoExport", false);
                propertiesSelected[2] = properties.Find("EstadoDescripcion", false);
                propertiesSelected[3] = properties.Find("DetalleMovimiento", false);
                propertiesSelected[4] = properties.Find("Cantidad", false);
                propertiesSelected[5] = properties.Find("NombreUsuario", false);
                propertiesSelected[6] = properties.Find("UnidadMedida", false);
                propertiesSelected[7] = properties.Find("EtiquetaUbicacion", false);
                propertiesSelected[8] = properties.Find("FechaRegistroExport", false);
                string nombreArticulo = "";
                try
                {
                    nombreArticulo = nombreArticuloSeleccionado.Split('-')[0];
                    nombreArticulo = nombreArticulo.Replace('/', ' ');
                    nombreArticulo = nombreArticulo.Replace('\\', ' ');
                    nombreArticulo = nombreArticulo.Replace(';', ' ');
                    nombreArticulo = nombreArticulo.Replace('.', ' ');
                }
                catch (Exception){}                
                DateTime fechaIni;
                DateTime fechaFin;
                try
                {
                    fechaIni = RDPFechaInicial.SelectedDate.Value;
                    fechaFin = RDPFechaFinal.SelectedDate.Value;
                }
                catch (Exception)
                {
                    fechaIni = DateTime.Now;
                    fechaFin = DateTime.Now;
                    //throw;
                }
                //DateTime.Now.ToString("yyyy-MM-dd h:mm tt");


                //string nombreArchivo = "Trazabilidad_Bodega Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                string nombreArchivo = "MovimientosInternos Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                fechaActualTexto = DateTime.Now.ToShortDateString();
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //Separado por celdas

                //List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text, "Total Global:" + lbTotalGlobal.Text };
                List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text};
                List<string> detalle = new List<string>() { "Lote","Fecha Vencimiento", "Tipo Movimiento", "Detalle Movimiento", "Cantidad UI", "Unidad Medida", "Etiqueta", "Fecha Registro" };
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> headers = new List<List<string>>();
                headers.Add(encabezado);
                headers.Add(saltoLinea);
                headers.Add(detalle);
                ExcelExporter.ExportData(_TrazabilidadBodegaArticulos, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);

            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }

        private List<e_TrazabilidadBodegaArticulos> _TrazabilidadBodegaArticulos
        {
            get
            {
                var data = ViewState["DataTrazabilidadBodega"] as List<e_TrazabilidadBodegaArticulos>;
                if (data == null)
                {
                    data = new List<e_TrazabilidadBodegaArticulos>();
                    ViewState["DataTrazabilidadBodega"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataTrazabilidadBodega"] = value;
            }
        }

        //-->Ajustes Inventario || Movimientos Inventario
        protected void btnExportarExcelAjusteInventario_Click(object sender, EventArgs e)
        {
            if (_AjustesInventarioArticulo.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_Ajustes_Inventario_Articulo));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[8];
                propertiesSelected[0] = properties.Find("NumeroMovimiento", false);
                propertiesSelected[1] = properties.Find("UsuarioNombreCompleto", false);
                propertiesSelected[2] = properties.Find("Lote", false);
                propertiesSelected[3] = properties.Find("Cantidad", false);
                propertiesSelected[4] = properties.Find("UnidadMedida", false);
                propertiesSelected[5] = properties.Find("AjusteInventarioDescripcion", false);
                propertiesSelected[6] = properties.Find("DescripcionDestino", false);                
                propertiesSelected[7] = properties.Find("FechaRegistroTrazabilidadExport", false);

                string nombreArticulo = "";
                try
                {
                    nombreArticulo = nombreArticuloSeleccionado.Split('-')[0];
                    nombreArticulo = nombreArticulo.Replace('/', ' ');
                    nombreArticulo = nombreArticulo.Replace('\\', ' ');
                    nombreArticulo = nombreArticulo.Replace(';', ' ');
                    nombreArticulo = nombreArticulo.Replace('.', ' ');

                }
                catch (Exception) { }
                DateTime fechaIni;
                DateTime fechaFin;
                try
                {
                    fechaIni = RDPFechaInicial.SelectedDate.Value;
                    fechaFin = RDPFechaFinal.SelectedDate.Value;
                }
                catch (Exception)
                {
                    fechaIni = DateTime.Now;
                    fechaFin = DateTime.Now;
                    //throw;
                }


                //string nombreArchivo = "Ajustes de Inventario Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " a " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                string nombreArchivo = "Movimientos Inventario Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " a " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                fechaActualTexto = DateTime.Now.ToShortDateString();
                properties = new PropertyDescriptorCollection(propertiesSelected);
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);
                //List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text, "Total Global:" + lbTotalGlobal.Text };
                List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text };
                List<string> headers = new List<string>() { "#MovimientoRef", "Usuario", "Lote", "Cantidad UI", "Unidad Medida", "Tipo Movimiento", "Ubicación", "Fecha Registro" };
                List<string> saltoLinea = new List<string>() { };
                List<List<string>> datosReporte = new List<List<string>>();
                datosReporte.Add(encabezado);
                datosReporte.Add(saltoLinea);
                datosReporte.Add(headers);

                ExcelExporter.ExportData(_AjustesInventarioArticulo, fileName, properties, datosReporte);
                Response.Redirect(rutaVirtual, false);
            }

            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }

        private List<e_Ajustes_Inventario_Articulo> _AjustesInventarioArticulo
        {
            get
            {
                var data = ViewState["DataAjustesInventarios"] as List<e_Ajustes_Inventario_Articulo>;
                if (data == null)
                {
                    data = new List<e_Ajustes_Inventario_Articulo>();
                    ViewState["DataAjustesInventarios"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataAjustesInventarios"] = value;
            }
        }


        //-->Despachos || Traslados
        protected void btnExportarDespachos_Click(object sender, EventArgs e)
        {
            if (_ArticulosDespachadosPorLoteRangoFecha.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(e_ArticulosDespachadosPorLoteRangoFecha));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[9];
                propertiesSelected[0] = properties.Find("IdInternoSAPSolicitud", false);
                propertiesSelected[1] = properties.Find("IdSolicitudTID", false);
                propertiesSelected[2] = properties.Find("Lote", false);
                propertiesSelected[3] = properties.Find("FechaVencimientoExport", false);
                propertiesSelected[4] = properties.Find("CantidadSolicitada", false);
                propertiesSelected[5] = properties.Find("CantidadDespachada", false);
                propertiesSelected[6] = properties.Find("UnidadMedida", false);
                propertiesSelected[7] = properties.Find("DestinoSolicitud", false);
                propertiesSelected[8] = properties.Find("FechaDespachoExport", false);
                string nombreArticulo = "";
                try
                {
                    nombreArticulo = nombreArticuloSeleccionado.Split('-')[0];
                    nombreArticulo = nombreArticulo.Replace('/', ' ');
                    nombreArticulo = nombreArticulo.Replace('\\', ' ');
                    nombreArticulo = nombreArticulo.Replace(';', ' ');
                    nombreArticulo = nombreArticulo.Replace('.', ' ');
                }
                catch (Exception) { }

                DateTime fechaIni;
                DateTime fechaFin;
                try
                {
                    fechaIni = RDPFechaInicial.SelectedDate.Value;
                    fechaFin = RDPFechaFinal.SelectedDate.Value;
                }
                catch (Exception)
                {
                    fechaIni = DateTime.Now;
                    fechaFin = DateTime.Now;
                    //throw;
                }
                //DateTime.Now.ToString("yyyy-MM-dd h:mm tt");


                //string nombreArchivo = "Despachos Artículo Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                string nombreArchivo = "Traslados Artículo Art-" + nombreArticulo + " " + fechaIni.ToString("yyyy-MM-dd") + " - " + fechaFin.ToString("yyyy-MM-dd") + ".xlsx";
                properties = new PropertyDescriptorCollection(propertiesSelected);                
                var rutaVirtual = "~/Documentos/" + string.Format(nombreArchivo);
                var fileName = Server.MapPath(rutaVirtual);                                               
                List<string> encabezado = new List<string>() { "Fecha Generado:" + fechaActualTexto, "Id Articulo SAP:" + lbIdArticuloSAP.Text, "Id Articulo TID:" + lbIdArticuloTID.Text};
                List<string> columnasConentido = new List<string>() { "#Solicitud SAP", "#Solicitud TID", "Lote", "Fecha Vencimiento", "Pedido UI","Alistado UI", "Unidad Medida", "Destino Solicitud", "Fecha Despacho" };
                List<string> saltolinea = new List<string>() { };
                List<List<string>> datosReporte = new List<List<string>>();
                datosReporte.Add(encabezado);
                datosReporte.Add(saltolinea);
                datosReporte.Add(columnasConentido);
                ExcelExporter.ExportData(_ArticulosDespachadosPorLoteRangoFecha, fileName, properties, datosReporte);
                Response.Redirect(rutaVirtual, false);
            }
            else
            {
                Mensaje("info", "No hay datos que exportar", "");
            }
        }

        private List<e_ArticulosDespachadosPorLoteRangoFecha> _ArticulosDespachadosPorLoteRangoFecha
        {
            get
            {
                var data = ViewState["DataArticulosDespachadosPorLoteRangoFecha"] as List<e_ArticulosDespachadosPorLoteRangoFecha>;
                if (data == null)
                {
                    data = new List<e_ArticulosDespachadosPorLoteRangoFecha>();
                    ViewState["DataArticulosDespachadosPorLoteRangoFecha"] = data;
                }
                return data;
            }
            set
            {
                ViewState["DataArticulosDespachadosPorLoteRangoFecha"] = value;
            }
        }
        #endregion

        #region "Lotes"
        private void cargarLotesDDL()
        {
            //Limpiar el DDL
            ddlLotes.DataSource = null;
            ddlLotes.DataBind();
            //Se cargan los lotes al momento de buscar               
            var lotes = _kardexReportes.ObtenerLotesTrazabilidadBodegaArticulo(idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada);
            ddlLotes.DataTextField = "DataToShow";
            ddlLotes.DataValueField = "Lote";
            ddlLotes.DataSource = lotes;
            ddlLotes.DataBind();

            if (lotes.Count > 0)
            {
                loteSeleccionado = ddlLotes.SelectedValue;

            }
            else
            {
                loteSeleccionado = ""; //Sin lotes
            }
        }
        protected void ddlLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se obtiene el lote seleccionado en si el indice ha cambiado
            // A su ves también el artículo por si el mismo a cambiado
            idArticuloSeleccionado = Convert.ToInt64(ddlIDArticulo.SelectedValue);
            loteSeleccionado = ddlLotes.SelectedValue;

        }
        #endregion

        #region "Articulos"
        private void loadArticulos()
        {
            var articulos = n_ArticuloGTIN.ObtenerArticulos();
            ddlIDArticulo.DataTextField = "Nombre_GTIN";
            ddlIDArticulo.DataValueField = "IdArticulo";
            ddlIDArticulo.DataSource = articulos;
            ddlIDArticulo.DataBind();
            nombreArticuloSeleccionado = ddlIDArticulo.SelectedItem.ToString();
        }
        #endregion

        #region "Grid Trazabilidad Bodega || Nombre Visual: Movimientos Internos"

        protected void gridTrazabilidadBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (validarFechasRangoParaNeedDataGrid())
                {
                    var listaMovimientoBodega = _kardexReportes.ObtenerTrazabilidadArticuloBodega(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada, 
                        loteSeleccionado, ChkFiltrarPorLote.Checked, ref idSAPArticuloSeleccionado,
                        ref totalGlobal, ref unidadMedidaTotalGlobal, IdBodega);
                    gridTrazabilidadBodega.DataSource = listaMovimientoBodega;
                    _TrazabilidadBodegaArticulos = listaMovimientoBodega;
                    lbIdArticuloSAP.Text = idSAPArticuloSeleccionado;
                    lbIdArticuloTID.Text = idArticuloSeleccionado.ToString();
                }
            }
            catch (Exception)
            {
                //Mensaje("error", "Algo salió mal, al obtener datos de [Trazabilidad Bodega]", "");
                Mensaje("error", "Algo salió mal, al obtener datos de [Movimientos Internos]", "");
            }
        }

        private void cargarGridTrazabilidadArticulo(bool filtroPorLote)
        {
            try
            {
                if (validarFechasRangoParaCargarGrid())
                {
                    var listaMovimientoBodega = _kardexReportes.ObtenerTrazabilidadArticuloBodega(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada,
                        loteSeleccionado, filtroPorLote, ref idSAPArticuloSeleccionado,
                        ref totalGlobal, ref unidadMedidaTotalGlobal,IdBodega);
                    gridTrazabilidadBodega.DataSource = listaMovimientoBodega;
                    gridTrazabilidadBodega.DataBind();
                    _TrazabilidadBodegaArticulos = listaMovimientoBodega;
                    lbIdArticuloSAP.Text = idSAPArticuloSeleccionado;
                    lbIdArticuloTID.Text = idArticuloSeleccionado.ToString();
                    try
                    {
                        lbTotalGlobal.Text = totalGlobal + " " + unidadMedidaTotalGlobal.Split('-')[1];
                    }
                    catch (Exception)
                    {
                        lbTotalGlobal.Text = totalGlobal + " " + unidadMedidaTotalGlobal;
                    }
                    

                    if (listaMovimientoBodega.Count < 1)
                    {
                        //Mensaje("info", "No se encontraron datos de [Trazabilidad Bodega] para este artículo", "");
                        //Mensaje("info", "No se encontraron datos para este artículo", "");
                    }
                    else
                    {
                        //Mensaje("ok", "Datos de [Trazbilidad Bodega] cargados correctamente", "");
                        //Mensaje("ok", "Datos cargados correctamente", "");
                        seCargaronDatos = true;

                    }
                }
            }
            catch (Exception)
            {
                Mensaje("error", "Algo salió mal, al obtener datos de [Trazabilidad Bodega]", "");
            }

        }

        #endregion    

        #region "Grid Ajuste Inventarios || Nombre Visual: Movimientos Inventario"

        protected void radGridAjustesInventario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {      
            try
            {
                if (validarFechasRangoParaNeedDataGrid())
                {
                    var listaAjustesInventario = _kardexReportes.ObtenerAjustesInventarioArticulo(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada, 
                        loteSeleccionado, ChkFiltrarPorLote.Checked, IdBodega);
                    radGridAjustesInventario.DataSource = listaAjustesInventario;
                    _AjustesInventarioArticulo = listaAjustesInventario;
                }
            }
            catch (Exception)
            {
                //Mensaje("error", "Algo salió mal, al obtener datos de [Ajustes Inventario]", "");
                Mensaje("error", "Algo salió mal, al obtener datos de [Movimientos Inventario]", "");
            }

        }

        private void cargarGridAjustesInventario(bool filtroPorLote)
        {
            try
            {
                radGridAjustesInventario.DataSource = null;
                radGridAjustesInventario.DataBind();
                if (validarFechasRangoParaCargarGrid())
                {

                    var listaAjustesInventario = _kardexReportes.ObtenerAjustesInventarioArticulo(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada,
                        loteSeleccionado, ChkFiltrarPorLote.Checked, IdBodega);
                    radGridAjustesInventario.DataSource = listaAjustesInventario;
                    radGridAjustesInventario.DataBind();
                    // lbNumeroOC.Text = numOC.ToString();
                    _AjustesInventarioArticulo = listaAjustesInventario;

                    if (listaAjustesInventario.Count < 1)
                    {
                        //Mensaje("info", "No se encontraron [Ajustes de Inventario]", "");
                    }
                    else
                    {
                        //Mensaje("ok", "Datos de [Ajustes de Inventario] cargados correctamente", "");
                        seCargaronDatos = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje("error", "Algo salió mal, al obtener [Ajustes de Inventario]", "");
            }
        }

        #endregion

        #region "Grid Despacho Articulos || Nombre Visual: Traslados"

        protected void radGridDespachosArticulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (validarFechasRangoParaNeedDataGrid())
                {

                    var listaArticulosDespachados = _kardexReportes.ObtenerArticulosDespachadosPorLoteRangoFecha(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada,
                        loteSeleccionado, ChkFiltrarPorLote.Checked,IdBodega);
                    radGridDespachosArticulo.DataSource = listaArticulosDespachados;
                    _ArticulosDespachadosPorLoteRangoFecha = listaArticulosDespachados;
                }
            }
            catch (Exception)
            {
                //Mensaje("error", "Algo salió mal, al obtener Despachos del Artículo", "");
                Mensaje("error", "Algo salió mal, al obtener Traslados", "");
            }

        }

        private void cargarGridDespachosArticulo(bool filtroPorLote)
        {

            try
            {

                radGridDespachosArticulo.DataSource = null;
                radGridDespachosArticulo.DataBind();

                if (validarFechasRangoParaCargarGrid())
                {
                    var listaArticulosDespachados = _kardexReportes.ObtenerArticulosDespachadosPorLoteRangoFecha(
                        idArticuloSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada, 
                        loteSeleccionado, filtroPorLote, IdBodega);
                    radGridDespachosArticulo.DataSource = listaArticulosDespachados;
                    radGridDespachosArticulo.DataBind();
                    _ArticulosDespachadosPorLoteRangoFecha = listaArticulosDespachados;
                    if (listaArticulosDespachados.Count < 1)
                    {
                        //Mensaje("info", "No se encontraron [Despachos del artículo]", "");
                    }
                    else
                    {
                        //Mensaje("ok", "Datos de [Despachos del artículo] cargados correctamente", "");
                        seCargaronDatos = true;
                    }
                }
            }
            catch (Exception)
            {
                Mensaje("error", "Algo salió mal, al obtener [Traslados]", "");
            }
        }

        #endregion

    }

}

