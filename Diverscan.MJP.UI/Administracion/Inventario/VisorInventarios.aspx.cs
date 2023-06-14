using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.Inventario;
using Diverscan.MJP.Negocio.TRAIngresoSalida;
using Diverscan.MJP.Negocio.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class VisorInventarios : System.Web.UI.Page
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
            if (_ddlInventariosDisponibles.Items.Count > 0)
            {
                int idInventario = int.Parse(_ddlInventariosDisponibles.SelectedValue);
                var inventario = _inventarioCiclicoData.FirstOrDefault(x => x.IdInventario == idInventario);
                if (inventario.IdInventario > 0)
                {
                    var inventarios = N_MaestroArticulo.GetArticulosInventarioCiclico(inventario.IdCategoriaArticulo);
                    _ddlArticulosCategoria.DataTextField = "Nombre";
                    _ddlArticulosCategoria.DataValueField = "IdArticulo";
                    _ddlArticulosCategoria.DataSource = inventarios;
                    _ddlArticulosCategoria.DataBind();
                }
            }
        }

        

        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarExistencias();
        }


        private void BuscarExistencias()
        {
            if (_ddlArticulosCategoria.Items.Count > 0 && _ddlInventariosDisponibles.Items.Count > 0)
            {
                int idArticulo = int.Parse(_ddlArticulosCategoria.SelectedValue);
                int idInventario = int.Parse(_ddlInventariosDisponibles.SelectedValue);


                //var bodegaSistema = TRAIngresoSalidaArticulosLoader.ObtenerExistencias(idArticulo, idInventario);
                var bodegaSistema = SetExistencias(idArticulo, idInventario);


                _bodegaFisica_SistemaRecordData = bodegaSistema;
                RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
                RGBodegaFisica_SistemaRecord.DataBind();

                colocarCantidadTotales(bodegaSistema);
            }
        }

        private void colocarCantidadTotales(List<BodegaFisica_SistemaRecord> bodegaFisica_SistemaRecordData)
        {
            var cantidadTotalTomaFisica = 0;
            var cantidadTotalSistema = 0;

            foreach (var record in bodegaFisica_SistemaRecordData)
            {
                cantidadTotalTomaFisica += record.CantidadBodega;
                cantidadTotalSistema += record.CantidadSistema;
            }
            _lblTotalTomaFisica.Text = "Total Bodega :" + cantidadTotalTomaFisica.ToString();
            _lblTotalSistema.Text = "Total Sistema :" + cantidadTotalSistema.ToString();
        }


    

        protected void _ddlInventariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadArticulos();
        }

        protected void _rdpB_Fecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            loadInventarios();
        }

        protected void RGBodegaFisica_SistemaRecord_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
        }


        


        #region Resultados de Inventario y sistema

        private List<BodegaFisica_SistemaRecord> SetExistencias(long idArticulo, long idInventario)
        {
            var articulosSistema = TRAIngresoSalidaArticulosLoader.ObtenerArticulosDisponibles(idArticulo);
            _articulosDisponiblesSistemaData = articulosSistema;
            var articulosInventario = TRAIngresoSalidaArticulosLoader.ObtenerCantidadArticulosInventario(idInventario, idArticulo);
            _articulosDisponiblesBodegaData = articulosInventario;
                       
            var articulosBodega = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesBodegaData);
            var articulosSistemaAgrupados = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesSistemaData);
            var existencias = AgrupadorArticulos.ObtenerBodegaFisica_SistemaRecord(articulosSistemaAgrupados,articulosBodega);
            return existencias;
        }
        #endregion

        #region RealizarAjuste

        protected void _btnRealizarAjuste_Click(object sender, EventArgs e)
        {
            List<long> idUbicacionesList = new List<long>();
            List<int> diferencias = new List<int>();
            List<string> etiquetas = new List<string>();
            for (int i = 0; i < RGBodegaFisica_SistemaRecord.Items.Count; i++)
            {
                var item = RGBodegaFisica_SistemaRecord.Items[i];
                var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    idUbicacionesList.Add(long.Parse(item["IdUbicacion"].Text));
                    diferencias.Add(int.Parse(item["DifenrenciaCantidad"].Text));
                    etiquetas.Add(item["Etiqueta"].Text);
                }
            }


            for (int i = 0; i < idUbicacionesList.Count; i++)
            {
                var idUbicacion = idUbicacionesList[i];
                var diferencia = diferencias[i];
                if (idUbicacion > 0)
                {
                    if (Math.Abs(diferencia) > 0)
                    {
                        var articulosBodega = _articulosDisponiblesBodegaData.Where(x => x.IdUbicacion == idUbicacion).ToList();
                        var articulosSistema = _articulosDisponiblesSistemaData.Where(x => x.IdUbicacion == idUbicacion).ToList();
                        if (diferencia > 0)
                        {
                            AjusteSalida(articulosBodega, articulosSistema);
                        }
                        else
                        {
                            AjusteEntrada(articulosBodega, articulosSistema);
                        }
                        BuscarExistencias();
                    }
                    else
                    {
                        var etiqueta = etiquetas[i];
                        Mensaje("info", "La ubicación no tiene articulos para ajustar:  " + etiqueta, "");
                    }
                }
            }
        }

        protected void _btnRealizarAjuste_ClickV2(object sender, EventArgs e)
        {
            List<long> idUbicacionesList = new List<long>();
            List<int> diferencias = new List<int>();
            for (int i = 0; i < RGBodegaFisica_SistemaRecord.Items.Count; i++)
            {
                var item = RGBodegaFisica_SistemaRecord.Items[i];
                var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    idUbicacionesList.Add(long.Parse(item["IdUbicacion"].Text));
                    diferencias.Add(int.Parse(item["DifenrenciaCantidad"].Text));
                }
            }




            for (int i = 0; i < RGBodegaFisica_SistemaRecord.Items.Count; i++)
            {
                var item = RGBodegaFisica_SistemaRecord.Items[i];
                var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    var idUbicacion = long.Parse(item["IdUbicacion"].Text);
                    var diferencia = int.Parse(item["DifenrenciaCantidad"].Text);
                    if (idUbicacion > 0)
                    {
                        if (Math.Abs(diferencia) > 0)
                        {
                            var articulosBodega = _articulosDisponiblesBodegaData.Where(x => x.IdUbicacion == idUbicacion).ToList();
                            var articulosSistema = _articulosDisponiblesSistemaData.Where(x => x.IdUbicacion == idUbicacion).ToList();
                            if (diferencia > 0)
                            {
                                AjusteSalida(articulosBodega, articulosSistema);
                            }
                            else
                            {
                                AjusteEntrada(articulosBodega, articulosSistema);
                            }
                            BuscarExistencias();
                        }
                        else
                        {
                            var etiqueta = item["Etiqueta"].Text.Replace("&nbsp;", "");
                            Mensaje("info", "La ubicación no tiene articulos para ajustar:  " + etiqueta, "");
                        }
                    }
                }
            }
        }

        //El sistema tiene mas articulos en relacion al inventario
        private void AjusteSalida(List<ICantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema)
        {
            int cantidadFantanteNOEncontrada = 0;
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                int cantidadBodega = articulosBodega[i].Cantidad;
                for (int x = 0; x < articulosSistema.Count && cantidadBodega > 0; x++)
                {
                    if (articulosSistema[x].Lote == articulosBodega[i].Lote &&
                        articulosSistema[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        articulosSistema.RemoveAt(x);
                        cantidadBodega--;
                        x--;
                    }
                }
                cantidadFantanteNOEncontrada +=  cantidadBodega;
            }

            while (articulosSistema.Count > 0 && cantidadFantanteNOEncontrada > 0)
            {
                articulosSistema.RemoveAt(0);
                cantidadFantanteNOEncontrada--;
            }
            if (articulosSistema.Count > 0)
            {
                for (int x = 0; x < articulosSistema.Count; x++)
                {
                    var sumUno_RestaCero = false;
                    var idUsuario = int.Parse(UsrLogged.IdUsuario);
                    long idMetodoAccion = 8;
                    string idTablaCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos";
                    string idCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos.idRegistro";
                    bool procesado = false;
                    var idEstado = 14;
                    var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                       sumUno_RestaCero,
                                       articulosSistema[x].IdArticulo,
                                        articulosSistema[x].FechaVencimiento,
                                        articulosSistema[x].Lote,
                                        idUsuario,
                                       idMetodoAccion,
                                       idTablaCampoDocumentoAccion,
                                       idCampoDocumentoAccion,
                                       articulosSistema[x].IdRegistro.ToString(),
                                       articulosSistema[x].IdUbicacion,
                                       articulosSistema[x].Cantidad,
                                       procesado,
                                       DateTime.Now,
                                       idEstado);
                    N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                }
                List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteData = new List<ArticuloXSolicitudAjusteRecord>();
                foreach (var art in articulosSistema)
                    articuloXSolicitudAjusteData.Add(new ArticuloXSolicitudAjusteRecord(art.IdArticulo, art.Lote, art.FechaVencimiento, art.IdUbicacion, art.IdUbicacion, art.Cantidad));
                //El 10 es el id de ajuste de salida por toma fisica
                RegistarEnSolicitudAjusteYObtenerIdSolicitud(10, articuloXSolicitudAjusteData);
            }
        }
        
        //El inventario tiene mas articulos en relacion al sistema
        private void AjusteEntrada(List<ICantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema)
        {

            for (int i = 0; i < articulosBodega.Count; i++)
            {
                for (int x = 0; x < articulosSistema.Count; x++)
                {
                    if (articulosSistema[x].Lote == articulosBodega[i].Lote &&
                       articulosSistema[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        articulosBodega[i].Cantidad--;
                        articulosSistema.RemoveAt(x);
                        x--;
                        if (articulosBodega[i].Cantidad == 0)
                        {
                            articulosBodega.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            var cantidadSinCoincidencia = articulosSistema.Count;
            if (cantidadSinCoincidencia > 0)
            {
                for (int i = 0; i < articulosBodega.Count; i++)
                {
                    var dif = articulosBodega[i].Cantidad - cantidadSinCoincidencia;
                    if (dif > 0)
                        articulosBodega[i].Cantidad -= cantidadSinCoincidencia;
                    else
                    {
                        cantidadSinCoincidencia = Math.Abs(dif);
                        articulosBodega.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (articulosBodega.Count > 0)
            {
                List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteData = new List<ArticuloXSolicitudAjusteRecord>();
                for (int p = 0; p < articulosBodega.Count; p++)
                {
                    for (int n = 0; n < articulosBodega[p].Cantidad; n++)
                    {
                        articuloXSolicitudAjusteData.Add(new ArticuloXSolicitudAjusteRecord(articulosBodega[p].IdArticulo, articulosBodega[p].Lote,
                            articulosBodega[p].FechaVencimiento, articulosBodega[p].IdUbicacion, articulosBodega[p].IdUbicacion, 1));
                    }
                }
                //el 2 es el id de entrada
                var idSolicitudAjusteInventario = RegistarEnSolicitudAjusteYObtenerIdSolicitud(2, articuloXSolicitudAjusteData);

                var IDLogAjustesTRA = N_LogAjustesTRA.InsertLogAjustesTRA_Y_ObtenerIDLogAjustesTRA(idSolicitudAjusteInventario);
                if (IDLogAjustesTRA > 0)
                {

                    for (int x = 0; x < articulosBodega.Count; x++)
                    {

                         var sumUno_RestaCero = true;
                        var idUsuario = int.Parse(UsrLogged.IdUsuario);
                        long idMetodoAccion = 8;
                        string idTablaCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA";
                        string idCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA.IdLogAjustesTRA";
                        string numDocumentoAccion = IDLogAjustesTRA.ToString();
                        bool procesado = false;
                        var idEstado = 12;
                        for (int n = 0; n < articulosBodega[x].Cantidad; n++)
                        {
                            var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                                               sumUno_RestaCero,
                                               articulosBodega[x].IdArticulo,
                                                articulosBodega[x].FechaVencimiento,
                                                articulosBodega[x].Lote,
                                                idUsuario,
                                               idMetodoAccion,
                                               idTablaCampoDocumentoAccion,
                                               idCampoDocumentoAccion,
                                               numDocumentoAccion,
                                               articulosBodega[x].IdUbicacion,
                                               1,
                                               procesado,
                                               DateTime.Now,
                                               idEstado);
                            N_TRAIngresoSalida.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord);
                        }
                    }                    
                }
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

        #region Data

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
    }
}