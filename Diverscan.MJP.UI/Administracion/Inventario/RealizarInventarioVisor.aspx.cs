using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.CustomEvent;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class RealizarInventarioVisor : System.Web.UI.Page, IUbicacionEtiquetaViewer
    {
        e_Usuario UsrLogged = new e_Usuario();
        public event EventHandler UbicacionEtiqueta;
        protected void Page_Load(object sender, EventArgs e)
        {
            UbicacionEtiquetaLoader ubicacionEtiquetaLoader = new UbicacionEtiquetaLoader(this, new UbicacionEtiquetaDataAcces());

            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                _txtS_CodigoBarras.Enabled = false;
                _txtS_UbicacionActual.Enabled = false;
                _btnEnviar.Enabled = false;
                loadInventarios();
                loadArticulos();
                loadUbicaciones();
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
        
     

        protected void _btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_txtS_CodigoBarras.Text))
            {
                Mensaje("info", "Debe de ingresar el codigo de barras", "");
                return;
            }
            if (string.IsNullOrEmpty(_txtS_UbicacionActual.Text))
            {
                Mensaje("info", "Debe de ingresar la ublicacion", "");
                return;
            }
            if (string.IsNullOrEmpty(_ddlInventariosDisponibles.SelectedValue))
                return;
            long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
            if (idInventario < 1)
                return;
            if (string.IsNullOrEmpty(_ddlArticulosCategoria.SelectedValue))
                return;
            long idArticuloSeleccionado = int.Parse(_ddlArticulosCategoria.SelectedValue);
            if (idArticuloSeleccionado < 1)
                return;

            try
            {
                var ubicacion = _txtS_UbicacionActual.Text.Trim();
                var newEtiqueta = String.Format("({0}){1}", ubicacion.Substring(0, 2), ubicacion.Substring(2));
                var idUbicacionActual = getIdUbicacion(newEtiqueta);
                if (idUbicacionActual < 1)
                    return;

                var codigoBarrar = _txtS_CodigoBarras.Text.Trim();
                string codLeido = codigoBarrar + ";" + ubicacion + ";" + "" + ";1";
                var gs1Data = GS1Extractor.ExtraerGS1(codLeido, UsrLogged.IdUsuario);
                if (!string.IsNullOrEmpty(gs1Data.IdArticulo) || !string.IsNullOrEmpty(gs1Data.IdUbicacion) ||
                    !string.IsNullOrEmpty(gs1Data.Lote) || !string.IsNullOrEmpty(gs1Data.Cantidad) || !string.IsNullOrEmpty(gs1Data.FechaVencimiento))
                {
                    long idArticulo = int.Parse(gs1Data.IdArticulo);
                    long idArticuloSelected = long.Parse(_ddlArticulosCategoria.SelectedValue);                    
                    if (idArticulo == idArticuloSelected)
                    {
                        string lote = gs1Data.Lote;
                        DateTime fechaVencimiento = DateTime.Parse(gs1Data.FechaVencimiento);
                        int cantidad = Convert.ToInt32(Single.Parse(gs1Data.Cantidad));

                        var extraInfoArticulo = N_DetalleArticulo.ObtenerArticuloPorIdArticulo(idArticulo);
                        if (extraInfoArticulo.EsGranel)
                            cantidad = gs1Data.Peso;

                        TomaFisicaInventario tomaFisicaInventario = new TomaFisicaInventario(idInventario, idArticulo, idUbicacionActual, lote, fechaVencimiento, cantidad, int.Parse(UsrLogged.IdUsuario));
                        N_TomaFisicaInventario.InsertarTomaFisicaInventario(tomaFisicaInventario);

                        Mensaje("info", "Enviado con exito", "");
                    }
                    else
                        Mensaje("error", "El Articulo Seleccionado no coincide con los parametros del inventario. " + "Nombre del articulo: " + gs1Data.NombreArticulo, "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", ex.Message, "");
            }
            finally
            {
                Limpiar();
            }
        }

        private void Limpiar()
        {
            _txtS_CodigoBarras.Text = "";
            _txtS_UbicacionActual.Text = "";
        }


        #region Ubicacion

        private long getIdUbicacion(string etiqueta)
        {
            long idUbicacion = 0;
            UbicacionEtiquetaEvent ubicacionEtiquetaEvent = new UbicacionEtiquetaEvent(etiqueta);
            if (UbicacionEtiqueta != null)
                UbicacionEtiqueta(this, ubicacionEtiquetaEvent);

            if (ViewState.Count > 0)
            {
                idUbicacion = (long)ViewState["IdUbicacion"];
            }
            return idUbicacion;
        }

        public void SetIdUbicacion(long idUbicacion)
        {
            ViewState["IdUbicacion"] = idUbicacion;
        }

        public void ShowMessage(string message)
        {
            Mensaje("error", message, "");
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

        protected void _ddlInventariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadArticulos();
            loadUbicaciones();
        }
           
        private void loadInventarios()
        {
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var inventarios = N_InventarioCiclico.GetInventariosCiclicos(today, today);
            _inventarioCiclicoData = inventarios;
            _ddlInventariosDisponibles.DataTextField = "NombreCategoria";
            _ddlInventariosDisponibles.DataValueField = "IdInventario";
            _ddlInventariosDisponibles.DataSource = inventarios;
            _ddlInventariosDisponibles.DataBind();
        }

        private void loadArticulos()
        {
            if (_ddlInventariosDisponibles.Items.Count > 0)
            {
                long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
                if (idInventario > 0)
                {
                    var articulos = N_ArticulosInventarioCiclicoRealiazar.ObtenerArticulosInventarioCiclicoRealizar(idInventario, 1).OrderBy(X => X.Nombre).ToList();
                    _articuloCiclicoRealizarRecord = articulos;
                    _ddlArticulosCategoria.DataTextField = "Nombre";
                    _ddlArticulosCategoria.DataValueField = "IdArticulo";
                    _ddlArticulosCategoria.DataSource = articulos;
                    _ddlArticulosCategoria.DataBind();                    

                    if (articulos.Count > 0)
                    {
                        _txtS_CodigoBarras.Enabled = true;
                        _txtS_UbicacionActual.Enabled = true;
                        _btnEnviar.Enabled = true;
                    }
                    else
                    {
                        _txtS_CodigoBarras.Enabled = false;
                        _txtS_UbicacionActual.Enabled = false;
                        _btnEnviar.Enabled = false;
                    }
                }
            }
        }
                
        private  List<UbicacionesInventarioCiclicoRecord> _ubicacionesPorArticulo
        {
            get
            {
                var data = ViewState["UbicacionesPorArticulo"] as List<UbicacionesInventarioCiclicoRecord>;
                if (data == null)
                {
                    data = new List<UbicacionesInventarioCiclicoRecord>();
                    ViewState["UbicacionesPorArticulo"] = data;
                }
                return data;
            }
            set
            {
                ViewState["UbicacionesPorArticulo"] = value;
            }
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

        private List<ArticuloCiclicoRealizarRecord> _articuloCiclicoRealizarRecord
        {
            get
            {
                var data = ViewState["ArticuloCiclicoRealizarRecord"] as List<ArticuloCiclicoRealizarRecord>;
                if (data == null)
                {
                    data = new List<ArticuloCiclicoRealizarRecord>();
                    ViewState["ArticuloCiclicoRealizarRecord"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ArticuloCiclicoRealizarRecord"] = value;
            }
        }
        
        protected void _txtS_CodigoBarras_TextChanged(object sender, EventArgs e)
        {
            _txtS_UbicacionActual.Focus();
        }


        #region ubicaciones sugeridas

        private void mostrarUbicaciones(List<UbicacionesInventarioCiclicoRecord> ubicaciones)
        {
            _ddlUbicaciones.DataTextField = "Etiqueta";
            _ddlUbicaciones.DataValueField = "IdUbicacionesInventario";
            _ddlUbicaciones.DataSource = ubicaciones;
            _ddlUbicaciones.DataBind();
        }

        private void loadUbicaciones()
        {
            _ddlUbicaciones.Items.Clear();
            if (_ddlInventariosDisponibles.Items.Count > 0)
            {
                long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
                if (idInventario > 0 && _ddlArticulosCategoria.Items.Count > 0)
                {
                    long idArticulo = long.Parse(_ddlArticulosCategoria.SelectedValue);
                    if (idArticulo > 0)
                    {
                        var ubicaciones0 = N_UbicacionesInventarioCiclico.ObtenerUbicacionesInventarioCiclico(idInventario, 1);
                        var ubicaciones = ubicaciones0.Where(x => x.IdArticulo == idArticulo).ToList();                       
                        mostrarUbicaciones(ubicaciones);
                    }
                }
            }
        }

         protected void _ddlArticulosCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadUbicaciones();
        }

         private List<UbicacionesInventarioCiclicoRecord> obtenerSiguienteArticulo()
         {             
             long idArticuloSeleccionado = int.Parse(_ddlArticulosCategoria.SelectedValue);
             long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
             if (idInventario > 0)
             {
                 var ubicaciones = N_UbicacionesInventarioCiclico.ObtenerUbicacionesInventarioCiclico(idInventario, 1);
                 if (ubicaciones.Count > 0)
                 {
                     var ubicacionesCerradas = N_UbicacionesInventarioCiclico.ObtenerUbicacionesInventarioCiclico(idInventario, 2);
                     if (ubicacionesCerradas.Count > 0)
                     {
                         var ubicacionesSeleccionado = ubicacionesCerradas.Where(x => x.IdArticulo == idArticuloSeleccionado);
                         if (ubicacionesSeleccionado.Count() > 0)
                         {     
                             var ubicacionesPorBodega = ubicaciones.Where(x => x.IdBodega == ubicacionesSeleccionado.First().IdBodega).ToList();
                             var ubicacionesAgrupadas = aguparUbicacionesPorArticulo(ubicacionesPorBodega);
                             return obtenerArticuloPorCercania(ubicacionesAgrupadas, ubicacionesSeleccionado.Average(x => x.Secuencia));
                         }
                     }
                 }
             }
             return null;
         }

         private Dictionary<long, List<UbicacionesInventarioCiclicoRecord>> aguparUbicacionesPorArticulo( List<UbicacionesInventarioCiclicoRecord> listUbicaciones)
         {
             Dictionary<long, List<UbicacionesInventarioCiclicoRecord>> ubicacionesPorArticulo = new Dictionary<long, List<UbicacionesInventarioCiclicoRecord>>();

             for (int x = 0; x < listUbicaciones.Count; x++)
             {
                 if (ubicacionesPorArticulo.ContainsKey(listUbicaciones[x].IdArticulo))
                 {
                     ubicacionesPorArticulo[listUbicaciones[x].IdArticulo].Add(listUbicaciones[x]);
                 }
                 else
                 {
                     ubicacionesPorArticulo.Add(listUbicaciones[x].IdArticulo, new List<UbicacionesInventarioCiclicoRecord> { listUbicaciones[x] });
                 }
             }
             return ubicacionesPorArticulo;
         }

         private List<UbicacionesInventarioCiclicoRecord> obtenerArticuloPorCercania(Dictionary<long, List<UbicacionesInventarioCiclicoRecord>> ubicacionesAgrupadas, double secuencia)
         {
             double minValue = double.MaxValue;
             long KeyFound = 0;
             foreach (var obj in ubicacionesAgrupadas)
             {
                 var diff = Math.Abs(obj.Value.Average(x => x.Secuencia) - secuencia);
                 if (minValue > diff && diff > 0)
                 {
                     minValue = diff;
                     KeyFound = obj.Key;
                 }
             }
             if (KeyFound > 0)
                 return ubicacionesAgrupadas[KeyFound];
             return null;
         }

         protected void _btnCerrarArticulo_Click(object sender, EventArgs e)
         {
             if (_ddlArticulosCategoria.Items.Count > 0)
             {
                 long idArticulosInventarioCiclico = _articuloCiclicoRealizarRecord.First(x=>x.IdArticulo == long.Parse(_ddlArticulosCategoria.SelectedValue)).IdArticulosCiclicoRealizar;
                 N_UbicacionesInventarioCiclico.ActualizarArticulosInventarioCiclicoRealizar(idArticulosInventarioCiclico, 2);

                 foreach (ListItem obj in _ddlUbicaciones.Items)
                 {                      
                     long idUbicacionesInventario = long.Parse(obj.Value);
                     N_UbicacionesInventarioCiclico.Update_UbicacionesRealizarInventarioCiclico(idUbicacionesInventario, 2);
                 }
                 _ddlUbicaciones.Items.Clear();

                 var siguienteArticulo = obtenerSiguienteArticulo();
                 _ddlArticulosCategoria.Items.Remove(_ddlArticulosCategoria.SelectedItem);
                 if (siguienteArticulo == null)                 
                     loadArticulos();
                 else
                     _ddlArticulosCategoria.SelectedValue = siguienteArticulo.First().IdArticulo.ToString();

                 loadUbicaciones();
             }   
         }

         protected void _btnCerrar_Click(object sender, EventArgs e)
         {
             if (_ddlUbicaciones.Items.Count > 0)
             {
                 long idUbicacionesInventario = long.Parse(_ddlUbicaciones.SelectedValue);
                 N_UbicacionesInventarioCiclico.Update_UbicacionesRealizarInventarioCiclico(idUbicacionesInventario, 2);
                 loadUbicaciones();
             }
         }

         protected void _txtArticuloBusqueda_TextChanged(object sender, EventArgs e)
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

        //private void cargarTodasUbicaciones()
        //{
        //    if (_ddlInventariosDisponibles.Items.Count > 0)
        //    {
        //        long idInventario = long.Parse(_ddlInventariosDisponibles.SelectedValue);
        //        if (idInventario > 0 && _ddlArticulosCategoria.Items.Count > 0)
        //        {
        //            var ubicaciones = N_UbicacionesInventarioCiclico.ObtenerUbicacionesInventarioCiclico(idInventario, 1);
        //            _ubicacionesPorArticulo = ubicaciones;


        //            Dictionary<long, List<UbicacionesInventarioCiclicoRecord>> ubicacionesPorArticulo = new Dictionary<long, List<UbicacionesInventarioCiclicoRecord>>();

        //            for (int x = 0; x < ubicaciones.Count; x++)
        //            {
        //                if (ubicacionesPorArticulo.ContainsKey(ubicaciones[x].IdArticulo))
        //                {
        //                    ubicacionesPorArticulo[ubicaciones[x].IdArticulo].Add(ubicaciones[x]);
        //                }
        //                else
        //                {
        //                    ubicacionesPorArticulo.Add(ubicaciones[x].IdArticulo, new List<UbicacionesInventarioCiclicoRecord> { ubicaciones[x] });
        //                }
        //            }

        //            Dictionary<int, Dictionary<long, List<UbicacionesInventarioCiclicoRecord>>> bodegasArticulos = new Dictionary<int, Dictionary<long, List<UbicacionesInventarioCiclicoRecord>>>();

        //            foreach (KeyValuePair<long, List<UbicacionesInventarioCiclicoRecord>> Par in ubicacionesPorArticulo)
        //            {
        //                if (bodegasArticulos.ContainsKey(Par.Value[0].IdBodega))
        //                {
        //                    bodegasArticulos[Par.Value[0].IdBodega].Add(Par.Value[0].IdArticulo, Par.Value);
        //                }
        //                else
        //                {
        //                    bodegasArticulos.Add(Par.Value[0].IdBodega, new Dictionary<long, List<UbicacionesInventarioCiclicoRecord>>());
        //                    bodegasArticulos[Par.Value[0].IdBodega].Add(Par.Value[0].IdArticulo, Par.Value);
        //                }
        //            }


        //            //var oredered = ubicacionesPorArticulo.OrderBy(x => x.Value.Average(a => a.Secuencia));

        //            //_ubicacionesPorArticulo = ubicacionesPorArticulo;
        //        }
        //    }
        //}

        //private void cargarBodegas()
        //{
        //    var bodegas = _ubicacionesPorArticulo.GroupBy(x => x.IdBodega).Select(g => g.First()).ToList();
        //    _ddlBodegas.DataTextField = "NombreBodega";
        //    _ddlBodegas.DataValueField = "IdBodega";
        //    _ddlBodegas.DataSource = bodegas;
        //    _ddlBodegas.DataBind();
        //}       

        #endregion

       
    }
}