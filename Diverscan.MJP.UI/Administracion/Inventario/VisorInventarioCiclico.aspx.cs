using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.Inventario;
using Diverscan.MJP.Negocio.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class VisorInventarioCiclico : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            if (!IsPostBack)
            {
                loadInventarios();
                loadArticulos();
                _rdpB_Fecha.SelectedDate = DateTime.Now;
                
            }
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        }

        protected void _rdpB_Fecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            loadInventarios();
            loadArticulos();
        }

        protected void _ddlInventariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadArticulos();
        }

        protected void RGBodegaFisica_SistemaRecord_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
        }

        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            mostrarArticulosPorUbicacion();
        }

        private void mostrarArticulosPorUbicacion()
        {
            limpiarResultados();
            if (_ddlArticulosCategoria.Items.Count < 1 && _ddlInventariosDisponibles.Items.Count < 1)
                return;          
            if (string.IsNullOrEmpty(_ddlInventariosDisponibles.SelectedValue))
                return;
            
            int idInventario = int.Parse(_ddlInventariosDisponibles.SelectedValue);
            if (idInventario < 1)
                return;

            cargarArticuloByUbicaciones(idInventario);
            var bodegaSistema = obtenerBodegaFisica_SistemaRecord();
            _bodegaFisica_SistemaRecordData = bodegaSistema;
            RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
            RGBodegaFisica_SistemaRecord.DataBind();
            colocarCantidadTotales(bodegaSistema);
        }

        private void colocarCantidadTotales(List<BodegaFisica_SistemaRecord> bodegaFisica_SistemaRecordData)
        {
            decimal cantidadTotalTomaFisica = 0;
            decimal cantidadTotalSistema = 0;

            foreach (var record in bodegaFisica_SistemaRecordData)
            {
                if (record.EsGranel)
                {
                    cantidadTotalTomaFisica += (decimal)(record.CantidadBodega / 1000d);
                    cantidadTotalSistema += (decimal)(record.CantidadSistema / 1000d);
                }
                else
                {
                    cantidadTotalTomaFisica += record.CantidadBodega;
                    cantidadTotalSistema += record.CantidadSistema;
                }
            }

            _lblTotalTomaFisica.Text = "Total Bodega: " + cantidadTotalTomaFisica;
            _lblTotalSistema.Text = "Total Sistema: " + cantidadTotalSistema;
            mostrarCantidadSAP();
        }

        private void mostrarCantidadSAP()
        {
            _lblTotalSAP.Text = "";
            if (string.IsNullOrEmpty(_ddlArticulosCategoria.SelectedValue))
                return;
            if (_ddlArticulosCategoria.SelectedValue == "--Todos--")                            
                return;           
            int idArticulo = int.Parse(_ddlArticulosCategoria.SelectedValue);
            if (idArticulo < 1)
                return;

            N_CantidadArticuloSAP n_CantidadArticuloSAP = new N_CantidadArticuloSAP();
            var cantidadSAP = N_CantidadArticuloSAP.ObtenerCantidadArticuloSAP(idArticulo);
            _lblTotalSAP.Text = "Cantidad SAP: " + cantidadSAP;
        }

        private void loadInventarios()
        {
            DateTime fecha = _rdpB_Fecha.SelectedDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var inventarios = N_InventarioCiclico.GetInventariosCiclicos(fecha, fecha);
            _inventarioCiclicoData = inventarios;
            _ddlInventariosDisponibles.DataTextField = "NombreCategoria";
            _ddlInventariosDisponibles.DataValueField = "IdInventario";
            _ddlInventariosDisponibles.DataSource = inventarios;
            _ddlInventariosDisponibles.DataBind();
            loadArticulos();
        }

        private void loadArticulos()
        {
            _ddlArticulosCategoria.DataSource = new  List<MaestroArticuloRecord>();
            limpiarResultados();
            _ddlArticulosCategoria.DataBind();
            if (_ddlInventariosDisponibles.Items.Count > 0)
            {
                int idInventario = int.Parse(_ddlInventariosDisponibles.SelectedValue);
                var inventario = _inventarioCiclicoData.FirstOrDefault(x => x.IdInventario == idInventario);
                if (inventario.IdInventario > 0)
                {
                    var articulos = N_MaestroArticulo.GetArticulosInventarioCiclico(inventario.IdCategoriaArticulo).OrderBy(X => X.Nombre);
                    
                    _ddlArticulosCategoria.DataTextField = "Nombre";
                    _ddlArticulosCategoria.DataValueField = "IdArticulo";
                    _ddlArticulosCategoria.DataSource = articulos;
                    _ddlArticulosCategoria.DataBind();
                    _ddlArticulosCategoria.Items.Insert(0, new ListItem("--Todos--"));
                }
            }
        }
        
        private void cargarArticuloByUbicaciones(long idInventario)
        {            
            if (_ddlArticulosCategoria.SelectedValue != "--Todos--")
            {
                if (string.IsNullOrEmpty(_ddlArticulosCategoria.SelectedValue))
                    return;
                int idArticulo = int.Parse(_ddlArticulosCategoria.SelectedValue);
                if (idArticulo > 0)
                {
                    var articiculosSistemaCopia = N_CopiaSistemaArticuloCiclico.ObtenerArticulosCopiaSistema(idInventario, idArticulo);
                    _articulosDisponiblesSistemaData = articiculosSistemaCopia;
                    var articulosInventarioBasico = TRAIngresoSalidaArticulosLoader.ObtenerCantidadArticulosInventario(idInventario, idArticulo);
                    _articulosDisponiblesBodegaData = articulosInventarioBasico;
                }
            }
            else
            {
                var articiculosSistemaCopia = N_CopiaSistemaArticuloCiclico.ObtenerTodosArticulosCopiaSistema(idInventario);
                _articulosDisponiblesSistemaData = articiculosSistemaCopia;
                var articulosInventarioBasico = TRAIngresoSalidaArticulosLoader.ObtenerCantidadTodosArticulosInventario(idInventario);
                _articulosDisponiblesBodegaData = articulosInventarioBasico;
            }
        }

        private void limpiarResultados()
        {
            RGBodegaFisica_SistemaRecord.DataSource = new List<BodegaFisica_SistemaRecord>();
            _lblTotalTomaFisica.Text = "";
            _lblTotalSistema.Text = "";
        }


        private List<BodegaFisica_SistemaRecord> obtenerBodegaFisica_SistemaRecord()
        {
            var articulosBodega = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesBodegaData);
            var articulosSistema = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesSistemaData);
            var existencias = AgrupadorArticulos.ObtenerBodegaFisica_SistemaRecord(articulosSistema,articulosBodega);
            return existencias;
        }
        
        private List<e_InventarioCiclicoRecord> _inventarioCiclicoData
        {
            get
            {
                var data = ViewState["e_InventarioCiclicoRecord"] as List<e_InventarioCiclicoRecord>;
                if (data == null)
                {
                    data = new List<e_InventarioCiclicoRecord>();
                    ViewState["e_InventarioCiclicoRecord"] = data;
                }
                return data;
            }
            set
            {
                ViewState["e_InventarioCiclicoRecord"] = value;
            }
        }

        private List<BodegaFisica_SistemaRecord> _bodegaFisica_SistemaRecordData
        {
            get
            {
                var data = ViewState["bodegaFisicaSistemaRecordData"] as List<BodegaFisica_SistemaRecord>;
                if (data == null)
                {
                    data = new List<BodegaFisica_SistemaRecord>();
                    ViewState["bodegaFisicaSistemaRecordData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["bodegaFisicaSistemaRecordData"] = value;
            }
        }

        private List<ArticulosDisponibles> _articulosDisponiblesSistemaData
        {
            get
            {
                var data = ViewState["articulosDisponiblesSistemaData"] as List<ArticulosDisponibles>;
                if (data == null)
                {
                    data = new List<ArticulosDisponibles>();
                    ViewState["articulosDisponiblesSistemaData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articulosDisponiblesSistemaData"] = value;
            }
        }

        private List<ICantidadPorUbicacionArticuloRecord> _articulosDisponiblesBodegaData
        {
            get
            {
                var data = ViewState["articulosDisponiblesBodegaData"] as List<ICantidadPorUbicacionArticuloRecord>;
                if (data == null)
                {
                    data = new List<ICantidadPorUbicacionArticuloRecord>();
                    ViewState["articulosDisponiblesBodegaData"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articulosDisponiblesBodegaData"] = value;
            }
        }
        
        #region Ajuste Inventario

        protected void _btnRealizarAjuste_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_ddlInventariosDisponibles.SelectedValue))
                return;
            long idInventarioCiclico = int.Parse(_ddlInventariosDisponibles.SelectedValue);
            if (idInventarioCiclico < -1)
                return;

            List<long> idUbicacionesList = new List<long>();
            List<decimal> diferencias = new List<decimal>();
            List<string> etiquetas = new List<string>();
            List<string> nombreArticulos = new List<string>();
            for (int i = 0; i < RGBodegaFisica_SistemaRecord.Items.Count; i++)
            {
                var item = RGBodegaFisica_SistemaRecord.Items[i];
                var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    idUbicacionesList.Add(long.Parse(item["IdUbicacion"].Text));
                    diferencias.Add(decimal.Parse(item["DifenrenciaCantidad"].Text));
                    etiquetas.Add(item["Etiqueta"].Text);
                    nombreArticulos.Add(item["NombreArticulo"].Text);
                }
            }
            List<BodegaSistema> listaBodegaSistemaSalida = new List<BodegaSistema>();
            List<BodegaSistema> listaBodegaSistemaEntrada = new List<BodegaSistema>();

            for (int i = 0; i < idUbicacionesList.Count; i++)
            {
                var idUbicacion = idUbicacionesList[i];
                var diferencia = diferencias[i];
                if (idUbicacion > 0)
                {
                    if (Math.Abs(diferencia) > 0)
                    {
                        var articulosBodega = _articulosDisponiblesBodegaData.Where(x => x.IdUbicacion == idUbicacion && x.NombreArticulo == nombreArticulos[i]).ToList();
                        var articulosSistema = _articulosDisponiblesSistemaData.Where(x => x.IdUbicacion == idUbicacion && x.NombreArticulo == nombreArticulos[i]).ToList();
                        if (diferencia < 0)
                        {
                            listaBodegaSistemaSalida.Add(new BodegaSistema(articulosBodega, articulosSistema));                            
                        }
                        else
                        {
                            listaBodegaSistemaEntrada.Add(new BodegaSistema(articulosBodega, articulosSistema));                            
                        }
                    }
                    else
                    {
                        var etiqueta = etiquetas[i];
                        Mensaje("info", "La ubicación no tiene articulos para ajustar:  " + etiqueta, "");
                    }
                }
            }
            string articulosSinIntegridad = "";
            realizarAjusteSalida(listaBodegaSistemaSalida, ref articulosSinIntegridad);
            if (!string.IsNullOrEmpty(articulosSinIntegridad))
                Mensaje("info", "Articulos con problemas de trazabilidad", "");
            realizarAjusteEntrada(listaBodegaSistemaEntrada);

            mostrarArticulosPorUbicacion();
        }

        private void realizarAjusteSalida(List<BodegaSistema> listaBodegaSistemaSalida, ref string articulosSinIntegridad)
        {
            if (string.IsNullOrEmpty(_ddlInventariosDisponibles.SelectedValue))
                return;
            long idInventarioCiclico = int.Parse(_ddlInventariosDisponibles.SelectedValue);
            if (idInventarioCiclico < -1)
                return;

            List<List<ArticulosDisponibles>> ArticulosSistemaFiltradosList = new List<List<ArticulosDisponibles>>();

            for (int i = 0; i < listaBodegaSistemaSalida.Count; i++)
            {
                var tieneIntegridad = true;
                var articulosSistemaFiltrados = DiferenciaConjuntosBodegaSistemaGetter.Validar_ObtenerDiferencia(listaBodegaSistemaSalida[i].ArticulosBodega, listaBodegaSistemaSalida[i].ArticulosSistema, out tieneIntegridad);
                if (tieneIntegridad)
                {
                    ArticulosSistemaFiltradosList.Add(articulosSistemaFiltrados);
                }
                else
                {
                    articulosSinIntegridad += " - " + listaBodegaSistemaSalida[i].ArticulosSistema[0].NombreArticulo + " - ";
                }
            }
            if (ArticulosSistemaFiltradosList.Count > 0)
            {
                List<TRAIngresoSalidaArticulosRecord> listaAGuardar = new List<TRAIngresoSalidaArticulosRecord>();
                var sumUno_RestaCero = false;
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                long idMetodoAccion = 8;
                string idTablaCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos";
                string idCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos.idRegistro";
                bool procesado = false;
                var idEstado = 14;
                foreach (var articulosSistemaFiltrados in ArticulosSistemaFiltradosList)
                {
                    for (int x = 0; x < articulosSistemaFiltrados.Count; x++)
                    {
                        var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                        sumUno_RestaCero, articulosSistemaFiltrados[x].IdArticulo, articulosSistemaFiltrados[x].FechaVencimiento, articulosSistemaFiltrados[x].Lote,
                        idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, articulosSistemaFiltrados[x].IdRegistro.ToString(),
                        articulosSistemaFiltrados[x].IdUbicacion, articulosSistemaFiltrados[x].Cantidad, procesado, DateTime.Now, idEstado);

                        listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                        //N_AjusteInventarioBasico.AjusteSalida(tRAIngresoSalidaArticulosRecord, idInventarioBasico);
                    }
                }
                N_AjusteInventarioCiclico.AjusteSalida(listaAGuardar, idInventarioCiclico, idUsuario);
            }           
        }

        private void realizarAjusteEntrada(List<BodegaSistema> listaBodegaSistemaSalida)
        {
            if (string.IsNullOrEmpty(_ddlInventariosDisponibles.SelectedValue))
                return;
            long idInventarioBasico = int.Parse(_ddlInventariosDisponibles.SelectedValue);
            if (idInventarioBasico < -1)
                return;

            List<List<ICantidadPorUbicacionArticuloRecord>> ArticulosBodegaFiltradosList = new List<List<ICantidadPorUbicacionArticuloRecord>>();

            for (int i = 0; i < listaBodegaSistemaSalida.Count; i++)
            {
                var tieneIntegridad = true;
                var articulosBodegaFiltrados = DiferenciaConjuntosBodegaSistemaGetter.Validar_ObtenerDiferencia(listaBodegaSistemaSalida[i].ArticulosSistema, listaBodegaSistemaSalida[i].ArticulosBodega, out tieneIntegridad);
                if (tieneIntegridad)
                {
                    ArticulosBodegaFiltradosList.Add(articulosBodegaFiltrados);
                }
            }

            if (ArticulosBodegaFiltradosList.Count > 0)
            {
                var sumUno_RestaCero = true;
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                long idMetodoAccion = 8;
                string idTablaCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA";
                string idCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA.IdLogAjustesTRA";
                string numDocumentoAccion = "";
                bool procesado = false;
                var idEstado = 12;
                int cantidad = 1;

                List<TRAIngresoSalidaArticulosRecord> listaToSave = new List<TRAIngresoSalidaArticulosRecord>();

                foreach (var articulosBodegaFiltrados in ArticulosBodegaFiltradosList)
                {
                    for (int x = 0; x < articulosBodegaFiltrados.Count; x++)
                    {
                        if (articulosBodegaFiltrados[x].EsGranel)
                        {
                            cantidad = articulosBodegaFiltrados[x].Cantidad;
                            var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                sumUno_RestaCero, articulosBodegaFiltrados[x].IdArticulo, articulosBodegaFiltrados[x].FechaVencimiento, articulosBodegaFiltrados[x].Lote,
                                idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, numDocumentoAccion,
                                articulosBodegaFiltrados[x].IdUbicacion, cantidad, procesado, DateTime.Now, idEstado);
                            listaToSave.Add(tRAIngresoSalidaArticulosRecord);
                        }
                        else
                        {
                            for (int n = 0; n < articulosBodegaFiltrados[x].Cantidad; n++)
                            {
                                var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                    sumUno_RestaCero, articulosBodegaFiltrados[x].IdArticulo, articulosBodegaFiltrados[x].FechaVencimiento, articulosBodegaFiltrados[x].Lote,
                                    idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, numDocumentoAccion,
                                    articulosBodegaFiltrados[x].IdUbicacion, cantidad, procesado, DateTime.Now, idEstado);
                                listaToSave.Add(tRAIngresoSalidaArticulosRecord);
                            }
                        }
                    }                   
                }
                N_AjusteInventarioCiclico.AjusteEntrada(listaToSave, idInventarioBasico, idUsuario);
            }
        }

        private long RegistarEnSolicitudAjusteYObtenerIdSolicitud(long idAjusteInventarioMotivo, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteData)
        {
            try
            {
                int idUsuario = int.Parse(UsrLogged.IdUsuario);
                int estado = 2;
                SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord = new SolicitudAjusteInventarioRecord(idUsuario, idAjusteInventarioMotivo, estado);
                var idSolicitud = N_SolicitudAjusteInventario.InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(solicitudAjusteInventarioRecord, articuloXSolicitudAjusteData);

                Mensaje("info", "El Ajuste fue registrado con exito", "");
                return idSolicitud;
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
                return 0;
            }
        }
        #endregion

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

        protected void _btnExportar_Click(object sender, EventArgs e)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(BodegaFisica_SistemaRecord));
            PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[6];
            propertiesSelected[0] = properties.Find("NombreArticulo", false);
            propertiesSelected[1] = properties.Find("Etiqueta", false);
            propertiesSelected[2] = properties.Find("UnidadMedida", false);
            propertiesSelected[3] = properties.Find("CantidadBodegaParaMostrar", false);
            propertiesSelected[4] = properties.Find("CantidadSistemaParaMostrar", false);
            propertiesSelected[5] = properties.Find("DifenrenciaCantidad", false);
            properties = new PropertyDescriptorCollection(propertiesSelected);
            var rutaVirtual = "~/temp/" + string.Format("InventarioCiclico.xlsx");
            var fileName = Server.MapPath(rutaVirtual);
            List<string> headers = new List<string>() { "NombreArticulo", "Ubicación", "UnidadMedida", "CantidadBodega", "CantidadSistema", "Diferencia" };
            ExcelExporter.ExportData(_bodegaFisica_SistemaRecordData, fileName, properties, headers);
            Response.Redirect(rutaVirtual, false);
        }
            
        protected void _txtArticuloBusqueda_TextChanged1(object sender, EventArgs e)
        {
            for (int i = _ddlArticulosCategoria.Items.Count - 1; i > 0; i--)
            {
                if (_ddlArticulosCategoria.Items[i].Text.ToUpper().Contains(_txtArticuloBusqueda.Text.ToUpper()))
                {
                    _ddlArticulosCategoria.SelectedValue = _ddlArticulosCategoria.Items[i].Value;
                    break;
                }
            }
        }
    }
}