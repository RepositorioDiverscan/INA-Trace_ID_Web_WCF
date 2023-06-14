using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Trazabilidad;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Negocio.Inventario;
using Diverscan.MJP.Negocio.InventarioBasico;
using Diverscan.MJP.UI.CrystalReportes.TomaFisica;
using Diverscan.MJP.AccesoDatos.DisponibleBodegaWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public partial class VisorInventariosBasico : System.Web.UI.Page
    {
        //Instanciar variables globales
        e_Usuario UsrLogged = new e_Usuario(); //Obtener las variables de sesión del usuario


        //Método de carga de la página
        protected void Page_Load(object sender, EventArgs e)
        {
            //Obtener el usuario que inicio sesión en el proyecto
            UsrLogged = (e_Usuario)Session["USUARIO"];

            //En caso de que sea nulo o se expire la sesión le pide las credenciales
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            //Si la sesión sigue activa ya ha cargado la página
            if (!IsPostBack)
            {
                //Esperar que todo se sincronice de forma correra
                ScriptManager.GetCurrent(Page).AsyncPostBackTimeout = 36000;
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //Obtener la fecha de hoy
                _rdpFechaAplicado.SelectedDate = today;
                //Invocar los métodos iniciales de carga
                FillDDBodega();
                loadArticulos();
                InvetarioEstaCerrado();
                loadDdlEstado();
            }
        }


        //Método para iniciar el panel
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {

                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
                }
            }
        }



        #region Métodos de Carga Inicial
        //Método para obtener las bodegas del sistema y agregarlas al ddBodega
        private void FillDDBodega()
        {
            //Instanciar la capa de negocio para obtener una lista de las bodegas
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            //Agregar al ddBodega la lista de Bodegas cons sus campos y valores
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }


        //Método para cargar los artículos de un Inventario
        private void loadArticulos()
        {
            //Comprobar que el ddlInventarios su valor no este vacio o nulo
            if (!string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue))
            {
                //Obtener el id del Inventario seleccionado
                int idInventario = int.Parse(_ddlInventariosBasicos.SelectedValue);

                //Comprobar que el idInventario sea mayor que 0
                if (idInventario > 0)
                {
                    //Obtener los artículos del inventario, extraerlos y agregar sus valores al _ddlArticulos
                    _articulos = N_CopiaSistemaArticulo.ObtenerArticulosInventarioBasico(idInventario);
                    var articulos = _articulos.OrderBy(X => X.NombreArticulo);
                    _ddlArticulos.DataTextField = "NombreArticulo";
                    _ddlArticulos.DataValueField = "IdArticulo";
                    _ddlArticulos.DataSource = articulos;
                    _ddlArticulos.DataBind();
                    _ddlArticulos.Items.Insert(0, new ListItem("--Todos--"));
                }
            }
        }


        //Método para comprobar si el inventario esta cerrado
        private void InvetarioEstaCerrado()
        {
            if (string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue)) { return; }
                
            long idInventarioBasico = int.Parse(_ddlInventariosBasicos.SelectedValue);

            if (idInventarioBasico < -1) { return;}
                
            if (_inventarioBasicoRecords.Count == 0){ return;}
               
            //Comprobar el estado del inventario para saber si está o no cerrado y habilitar el botón de Cerrar
            var estado = _inventarioBasicoRecords.FirstOrDefault(x => x.IdInventarioBasico == idInventarioBasico).Estado;
            estado = true ? _btnCerrar.Enabled = true : _btnCerrar.Enabled = false;
        }


        //Método para agregar los valores del estado del Inventario _ddlEstado
        private void loadDdlEstado()
        {
            var items = new List<string> { "--Seleccione--", "Sin Diferencia", "Con Diferencia", "--Todos--" };
            _ddlEstado.DataSource = items;
            _ddlEstado.DataBind();
        }
        #endregion Métodos de Carga Inicial



        #region Métodos de los campos del formulario
        //Método del campo de selección de Bodega
        protected void _ddBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtener la fecha y la bodega de los campos seleccionados
            var date = _rdpFechaAplicado.SelectedDate ?? DateTime.Now;
            var idBodega = Convert.ToInt32(ddBodega.SelectedValue);
            //Invocar métodos para obtener información de los inventarios
            LimpiarResultados();
            loadInventarios(date, date, idBodega);
            loadArticulos();
            mostrarArticulosPorUbicacion();
            InvetarioEstaCerrado();
           
        }


        //Método del campo de selección de Fecha Aplicada
        protected void _rdpFechaAplicado_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //Obtener la fecha y la bodega de los campos seleccionados
            var date = _rdpFechaAplicado.SelectedDate ?? DateTime.Now;
            var idBodega = Convert.ToInt32(ddBodega.SelectedValue);
            //Invocar métodos para obtener información de los inventarios
            LimpiarResultados();
            loadInventarios(date, date, idBodega);
            loadArticulos();
            mostrarArticulosPorUbicacion();
            InvetarioEstaCerrado();
         
        }


        //Método del campo de selección de Inventario
        protected void _ddlInventariosBasicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Invocar métodos para obtener información de los inventarios
            LimpiarResultados();
            loadArticulos();
            mostrarArticulosPorUbicacion();
            InvetarioEstaCerrado();
           
        }


        //Método del campo de selección de Inventario
        protected void _ddlArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Invocar métodos para obtener información de los inventarios
            LimpiarResultados();
            mostrarArticulosPorUbicacion();
            
        }


        //Método del campo de buscar un artículo
        protected void _txtArticuloBusqueda_TextChanged(object sender, EventArgs e)
        {
            //Recorrer los items del campo de selección de artículos 
            for (int i = _ddlArticulos.Items.Count - 1; i > 0; i--)
            {
                if (_ddlArticulos.Items[i].Text.ToUpper().Contains(_txtArticuloBusqueda.Text.ToUpper()))
                {
                    //Si el item contiene el artículo agregarlo al campo de selección de artículos
                    _ddlArticulos.SelectedValue = _ddlArticulos.Items[i].Value;
                    break;
                }
            }
        }


        //Método del campo de selección de estado
        protected void _ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtener el valor del estado
            string estado = _ddlEstado.SelectedValue;

            //Si el estado es mayor a 0
            if (_ddlEstado.SelectedIndex > 0)
            {
                //Crear un arreglo de los artículos del Inventario
                List<BodegaFisica_SistemaRecord> tomaFisicaFiltrada = new List<BodegaFisica_SistemaRecord>();

                switch (estado)
                {
                    //Obtener los artículos con diferencia
                    case "Con Diferencia":
                        tomaFisicaFiltrada = _bodegaFisica_SistemaRecordData.FindAll(x => x.UIDifenrenciaCantidad != 0);
                        break;

                    //Obtener los artículos sin diferencia
                    case "Sin Diferencia":
                        tomaFisicaFiltrada = _bodegaFisica_SistemaRecordData.FindAll(x => x.UIDifenrenciaCantidad == 0);
                        break;

                    //Por defecto mostrar todos
                    default:
                        tomaFisicaFiltrada = _bodegaFisica_SistemaRecordData;
                        break;
                }

                //Llenar la tabla de los artículos
                RGBodegaFisica_SistemaRecord.DataSource = tomaFisicaFiltrada;
                RGBodegaFisica_SistemaRecord.DataBind();
            }
        }


        //Método del campo del check 
        protected void ChkContado_OnCheckedChanged(object sender, EventArgs e)
        {
            //Crear un arreglo de los artículos del Inventario
            List<BodegaFisica_SistemaRecord> tomaFisicaFiltrada = new List<BodegaFisica_SistemaRecord>();

            //Comprobar si el check del contado esta chekeado
            if (_chkContado.Checked)
            {
                //Filtrar la cantidad sea mayor a 0
                tomaFisicaFiltrada = _bodegaFisica_SistemaRecordData.FindAll(x => x.CantidadBodega > 0);
            }
            else
            {
                //Regresar todas las tomas físicas
                tomaFisicaFiltrada = _bodegaFisica_SistemaRecordData;
            }

            //Llenar la tabla de los artículos
            RGBodegaFisica_SistemaRecord.DataSource = tomaFisicaFiltrada;
            RGBodegaFisica_SistemaRecord.DataBind();
        }
        #endregion Métodos de los campos del formulario



        #region Métodos de los botones
        //Método del botón de buscar y mostrar los artículos de la Toma Física
        protected void _btnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarResultados();
            mostrarArticulosPorUbicacion();
        }


        //Método del botón de Exportar para exportar la Toma Física por medio de un excel
        protected void _btnExportar_Click(object sender, EventArgs e)
        {
            FileExceptionWriter exceptionWriter = new FileExceptionWriter();
            try
            {
                //Instanciar la propiedades de descripción
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(BodegaFisica_SistemaRecord));
                PropertyDescriptor[] propertiesSelected = new PropertyDescriptor[9];

                //Crear las filas del excel
                propertiesSelected[0] = properties.Find("IdInterno", false);
                propertiesSelected[1] = properties.Find("NombreArticulo", false);
                propertiesSelected[2] = properties.Find("Lote", false);
                propertiesSelected[3] = properties.Find("FVToShow", false);
                propertiesSelected[4] = properties.Find("Etiqueta", false);
                propertiesSelected[5] = properties.Find("UnidadMedida", false);
                propertiesSelected[6] = properties.Find("CantidadBodegaParaMostrar", false);
                propertiesSelected[7] = properties.Find("CantidadSistemaParaMostrar", false);
                propertiesSelected[8] = properties.Find("DifenrenciaCantidad", false);
                properties = new PropertyDescriptorCollection(propertiesSelected);

                //Crear la ruta dónde se encuentra la plantilla
                var rutaVirtual = "~/temp/" + string.Format("Inventario.xlsx");
                var fileName = Server.MapPath(rutaVirtual);

                //Crear los encabezados del excel
                List<string> headers = new List<string>() { "SKU", "NombreArticulo", "Lote", "Fecha de Venc.", "Ubicación", "UnidadMedida", "CantidadBodega", "CantidadSistema", "Diferencia" };

                //Generar el reporte del Excel con toda la estructura y enviar el excel
                ExcelExporter.ExportData(_bodegaFisica_SistemaRecordData, fileName, properties, headers);
                Response.Redirect(rutaVirtual, false);
            }
            catch (Exception ex)
            {
                exceptionWriter.WriteException(ex, PathFileConfig.INVENTORYFILEPATHEXCEPTION);
                Mensaje("error", "Ha ocurrido un error, descripción del error: " + ex.Message, "");
            }
        }


        //Método del botón de Reporte para obtener un Reporte General
        protected void _btnGenerarReporteGeneral_Click(object sender, EventArgs e)
        {
            WSDisponibleBodega wSDisponibleBodega = new WSDisponibleBodega();

            CRTomaFiscaEn crTomaFiscaEn = new CRTomaFiscaEn();
            crTomaFiscaEn.Bodega = ddBodega.SelectedItem.Text;
            crTomaFiscaEn.FechaTomaFisica = _rdpFechaAplicado.SelectedDate.ToString();
            crTomaFiscaEn.NombreTomaFisica = _ddlInventariosBasicos.SelectedItem.Text;
            List<CRTomaFiscaEn> cRTomaFiscaEns = new List<CRTomaFiscaEn>();
            cRTomaFiscaEns.Add(crTomaFiscaEn);

            List<CRTomaFisicaDe> cRTomaFisicaDes = new List<CRTomaFisicaDe>();

            if (_ddlArticulos.SelectedValue == "--Todos--")
            {
                foreach (var articulo in _articulos)
                {
                    CRTomaFisicaDe crTomaFisicaDe = new CRTomaFisicaDe();
                    crTomaFisicaDe.NombreProducto = articulo.NombreArticulo;
                    crTomaFisicaDe.IdInterno = articulo.IdInterno;

                    var cantidadSistema = _articulosDisponiblesSistemaData.Where(x => x.IdArticulo == articulo.IdArticulo).Select(x => x.Cantidad).Sum();
                    var cantidadBodega = _articulosDisponiblesBodegaData.Where(x => x.IdArticulo == articulo.IdArticulo).Select(x => x.Cantidad).Sum();

                    try
                    {
                        var cantidadSap = wSDisponibleBodega.ObtenerDisponibleBodegaSap(articulo.IdInterno, "B01").onHand;
                        crTomaFisicaDe.CantidadSoap = cantidadSap.ToString();
                    }
                    catch (Exception)
                    {
                        crTomaFisicaDe.CantidadSoap = "Problemas al obtener la cantidad";
                    }
                    crTomaFisicaDe.CantidadSistema = cantidadSistema.ToString();
                    crTomaFisicaDe.CantidadBodega = cantidadBodega.ToString();

                    cRTomaFisicaDes.Add(crTomaFisicaDe);
                }
            }
            else
            {
                int idArticulo = int.Parse(_ddlArticulos.SelectedValue);

                var articulo = _articulos.Find(x => x.IdArticulo == idArticulo);
                CRTomaFisicaDe crTomaFisicaDe = new CRTomaFisicaDe();
                crTomaFisicaDe.NombreProducto = articulo.NombreArticulo;
                crTomaFisicaDe.IdInterno = articulo.IdInterno;

                var cantidadSistema = _articulosDisponiblesSistemaData.Where(x => x.IdArticulo == idArticulo).Select(x => x.Cantidad).Sum();
                var cantidadBodega = _articulosDisponiblesBodegaData.Where(x => x.IdArticulo == idArticulo).Select(x => x.Cantidad).Sum();

                WSDisponibleBodega disponibleBodega = new WSDisponibleBodega();
                NTransitoCantidadEntrada cantidadtrazabilidadentrada = new NTransitoCantidadEntrada();

                try
                {
                    var cantidadSap = wSDisponibleBodega.ObtenerDisponibleBodegaSap(articulo.IdInterno, "B01").onHand;
                    crTomaFisicaDe.CantidadSoap = cantidadSap;
                }
                catch (Exception)
                {
                    crTomaFisicaDe.CantidadSoap = "Problemas al obtener la cantidad";
                }
                var idBodega = Convert.ToInt32(ddBodega.SelectedValue);
                var cantidadTransito = cantidadtrazabilidadentrada.ObtenerCantidadTransitoEntrada(idArticulo, idBodega);
                crTomaFisicaDe.CantidadSistema = cantidadSistema.ToString();
                crTomaFisicaDe.CantidadBodega = cantidadBodega.ToString();
                cRTomaFisicaDes.Add(crTomaFisicaDe);
            }


            var path = HttpRuntime.AppDomainAppPath + @"CrystalReportes" + @"\TomaFisica" + @"\TomaFisica.pdf";
            ReportDocument report = new CrystalReportes.TomaFisica.CRTomaFisica();
            var tablename = report.Database.Tables[0];
            report.Database.Tables[0].SetDataSource(cRTomaFiscaEns);
            report.Database.Tables[1].SetDataSource(cRTomaFisicaDes);

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = path;
            CrExportOptions = report.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            report.Export();
            report.Close();
            report.Dispose();
            GC.Collect();

            Response.AddHeader("Content-Type", "application/octet-stream");
            Response.AddHeader("Content-Transfer-Encoding", "Binary");
            Response.AddHeader("Content-disposition", "attachment; filename=\"TomaFisica.pdf\"");
            Response.WriteFile(path);
            Response.End();
        }


        //Método del botón de Cerrar para finalizar una Toma Física
        protected void _btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                //Comprobar que el campo de selección del Inventario no este vacio o nulo
                if (string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue))
                {
                    return;
                }

          
                long idInventarioBasico = int.Parse(_ddlInventariosBasicos.SelectedValue);
                if (idInventarioBasico < -1)
                {
                    return;
                }

                //Cerrar la Toma Física y cargar los datos
                N_InventarioBasico.CerrarInventarioBasico(idInventarioBasico);
                var date = _rdpFechaAplicado.SelectedDate ?? DateTime.Now;
                var idBodega = Convert.ToInt32(ddBodega.SelectedValue);
                loadInventarios(date, date, idBodega);
                loadArticulos();
                InvetarioEstaCerrado();
                Mensaje("ok", "Inventario Cerrado exitosamente", "");
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un error, descripción del error: " + ex.Message, "");
            }


        }


        //Método del botón de realizar Ajuste 
        protected void _btnRealizarAjuste_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtener el ID del Inventario escogido
                long idInventarioBasico = int.Parse(_ddlInventariosBasicos.SelectedValue);

                //Comprobar que el campo de selección del Inventario no este vacio o nulo
                if (string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue) || idInventarioBasico < -1)
                {
                    return;
                }

                //Obtener una lista de los campos de la tabla
                List<long> idUbicacionesList = new List<long>();
                List<decimal> diferencias = new List<decimal>();
                List<string> lotes = new List<string>();
                List<DateTime> fVs = new List<DateTime>();
                List<string> etiquetas = new List<string>();
                List<string> nombreArticulos = new List<string>();
                List<long> idArticulos = new List<long>();

                //Recorrer cada uno item de la tabla 
                for (int i = 0; i < RGBodegaFisica_SistemaRecord.Items.Count; i++)
                {
                    //Obtener cada artículo recorrido
                    var item = RGBodegaFisica_SistemaRecord.Items[i];

                    //Definir que la primera columna sea un tipo checkbox
                    var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;

                    //Comprobar que no sea nulo y que este activo el check del artículo en ese caso, agregar a la lista los valores de la tabla
                    if (checkbox != null && checkbox.Checked)
                    {
                        idUbicacionesList.Add(long.Parse(item["IdUbicacion"].Text));
                        diferencias.Add(decimal.Parse(item["DifenrenciaCantidad"].Text));
                        lotes.Add(item["Lote"].Text);
                        fVs.Add(Convert.ToDateTime(item["FVToShow"].Text));
                        etiquetas.Add(item["Etiqueta"].Text);
                        nombreArticulos.Add(item["NombreArticulo"].Text);
                        idArticulos.Add(long.Parse(item["IdArticulo"].Text));
                    }
                }

                //Crear una lista de bodegas de entrada y salida
                List<BodegaSistema> listaBodegaSistemaSalida = new List<BodegaSistema>();
                List<BodegaSistema> listaBodegaSistemaEntrada = new List<BodegaSistema>();

                //Recorrer la lista de ID de las ubicaciones
                for (int i = 0; i < idUbicacionesList.Count; i++)
                {
                    //Obtener el ID de la Ubicación y Diferencia
                    var idUbicacion = idUbicacionesList[i];
                    var diferencia = diferencias[i];

                    //Comprobar que exista un ID de Ubicación
                    if (idUbicacion > 0)
                    {
                        //Comprobar que si exista una diferencia mayor a 0
                        if (Math.Abs(diferencia) > 0)
                        {
                            //Obtener los articulos disponibles en Bodega
                            var articulosBodega = _articulosDisponiblesBodegaData.Where(x => x.IdUbicacion == idUbicacion
                                && x.NombreArticulo == nombreArticulos[i]
                                && x.IdArticulo == idArticulos[i]
                                && x.Lote == lotes[i]
                                && x.FechaVencimiento == fVs[i]).ToList();

                            //Obtener los articulos del sistema
                            var articulosSistema = _articulosDisponiblesSistemaData.Where(x => x.IdUbicacion == idUbicacion
                                && x.NombreArticulo == nombreArticulos[i]
                                && x.IdArticulo == idArticulos[i]
                                && x.Lote == lotes[i]
                                && x.FechaVencimiento == fVs[i]).ToList();

                            //Comprobar si la diferencia es mayor o menor a 0
                            if (diferencia < 0) //Si es menor a 0, es de Salida
                            {
                                listaBodegaSistemaSalida.Add(new BodegaSistema(articulosBodega, articulosSistema));
                            }
                            else //Si el mayor a 0 es de Entrada
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
                //Invocar los métodos de ajustes y los artículos por ubicaciones
                string articulosSinIntegridad = "";
                realizarAjusteSalida(listaBodegaSistemaSalida, ref articulosSinIntegridad);
                realizarAjusteEntrada(listaBodegaSistemaEntrada);
                mostrarArticulosPorUbicacion();

                if (!string.IsNullOrEmpty(articulosSinIntegridad))
                {
                    Mensaje("info", "Articulos con problemas de trazabilidad", "");
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ha ocurrido un error, " + ex.Message, "");
            }
        }
        #endregion Métodos de los botones



        #region Métodos de acción de los campos del formulario
        //Método para obtener los inventarios en la fecha indicada y en la bodega
        private void loadInventarios(DateTime fechaInicio, DateTime fechaFin, int idBodega)
        {
            //Invocar la capa de negocios para obtener todos los inventarios de la fecha
            _inventarioBasicoRecords = N_InventarioBasico.ObtenerTodosInventarioBasicoRecords(fechaInicio, fechaFin, idBodega);
            //Obtener la información de la lista de los inventarios y pasarla al _ddlInventariosBasicos
            _ddlInventariosBasicos.DataSource = _inventarioBasicoRecords;
            _ddlInventariosBasicos.DataTextField = "Nombre";
            _ddlInventariosBasicos.DataValueField = "IdInventarioBasico";
            _ddlInventariosBasicos.DataBind();
        }


        //Método para mostar los artículos por sus ubicaciones
        private void mostrarArticulosPorUbicacion()
        {
            LimpiarResultados();

            //Comprobar que el valor de _ddlArticulos o _ddlInventariosBasicos no este vacio o nulo
            if (string.IsNullOrEmpty(_ddlArticulos.SelectedValue) || string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue))
            {
                lblEstado.Visible = false;
                _ddlEstado.Visible = false;
                return;
            }

            //Obtener el valor del inventario seleccionado
            int idInventario = int.Parse(_ddlInventariosBasicos.SelectedValue);

            if (idInventario > 0)
            {
                cargarArticuloByUbicaciones(idInventario);

                //Obtener la información de los artículos por ubicación de la bodega y el sistema
                _bodegaFisica_SistemaRecordData = obtenerBodegaFisica_SistemaRecord();
                RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
                RGBodegaFisica_SistemaRecord.DataBind();

                //Métodos para cargar los artículos por ubicaciones por el idInventario
                
                colocarCantidadTotales(_bodegaFisica_SistemaRecordData);

                //Mostrar los campos de estado y contado
                lblEstado.Visible = true;
                _ddlEstado.Visible = true;
                lblContado.Visible = true;
                _chkContado.Visible = true;
            }
        }


        //Metodo para obtener las bodegas fisicas del sistema
        private List<BodegaFisica_SistemaRecord> obtenerBodegaFisica_SistemaRecord()
        {
            //Invocarción de varios métodos para obtener los artículos del sistema y de las bodegas
            var articulosBodega = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesBodegaData);
            var articulosSistema = AgrupadorArticulos.AgruparArticulosPorUbicacion(_articulosDisponiblesSistemaData);
            var existencias = AgrupadorArticulos.ObtenerBodegaFisica_SistemaRecord(articulosSistema, articulosBodega);
            //Retornar las existencias del sistema y de las bodegas
            return existencias;
        }


        //Métodos para cargar artículos de las ubicaciones
        private void cargarArticuloByUbicaciones(long idInventario)
        {
            //Método para verificar que el valor del _ddlArticulos  sea diferente a todos
            if (_ddlArticulos.SelectedValue != "--Todos--")
            {
                //Obtener el idArticulo que se selecciono
                int idArticulo = int.Parse(_ddlArticulos.SelectedValue);

                if (idArticulo > 0)
                {
                    //Obtener el artículo seleccionado disponible en el sistema y el inventario
                    _articulosDisponiblesSistemaData = N_CopiaSistemaArticulo.ObtenerArticulosCopiaSistema(idInventario, idArticulo);
                    _articulosDisponiblesBodegaData = N_ArticulosInventarioBasico.ObtenerArticulosInventarioBasico(idInventario, idArticulo);
                }
            }
            else
            {
                //Obtener todos los artículos del sistema y del inventario
                _articulosDisponiblesSistemaData = N_CopiaSistemaArticulo.ObtenerTodosArticulosCopiaSistema(idInventario);
                _articulosDisponiblesBodegaData = N_ArticulosInventarioBasico.ObtenerTodosArticulosInventarioBasico(idInventario);
            }
        }


        //Método para colocar las cantitades totales
        private void colocarCantidadTotales(List<BodegaFisica_SistemaRecord> bodegaFisica_SistemaRecordData)
        {
            decimal cantidadTotalTomaFisica = 0, cantidadTotalSistema = 0;

            //Comprobar que el _ddlArticulos no este vacio
            if (string.IsNullOrEmpty(_ddlArticulos.SelectedValue) || _ddlArticulos.SelectedValue == "--Todos--")
            {
                return;
            }

            //Recorrer el arreglo de las bodegas fisicas  
            foreach (var record in bodegaFisica_SistemaRecordData)
            {
                //Sumar las cantidades fisicas y las que hay en total en el sistema
                cantidadTotalTomaFisica += (decimal)record.UICantidadBodegaParaMostrar;
                cantidadTotalSistema += (decimal)record.UICantidadSistemaParaMostrar;
            }

            //Instanciar la capa de negocios y invocar el método de obtener la cantidad transito entrada
            NTransitoCantidadEntrada cantidadtrazabilidadentrada = new NTransitoCantidadEntrada();
            var cantidadTransito = cantidadtrazabilidadentrada.ObtenerCantidadTransitoEntrada(int.Parse(_ddlArticulos.SelectedValue), Convert.ToInt32(ddBodega.SelectedValue));

            //Agregar la información a los textos
            _lblTotalTransito.Text = "Total Transito: " + cantidadTransito.ToString();
            _lblTotalTomaFisica.Text = "Total Bodega: " + cantidadTotalTomaFisica;
            _lblTotalSistema.Text = "Total Sistema: " + cantidadTotalSistema;
        }


        


        //Método para limpiar los resultados y los campos de texto
        private void LimpiarResultados()
        {
            RGBodegaFisica_SistemaRecord.DataSource = new List<BodegaFisica_SistemaRecord>();
            RGBodegaFisica_SistemaRecord.DataBind();
            _lblTotalTomaFisica.Text = "";
            _lblTotalSistema.Text = "";
        }


        //Método para Realizar un Ajuste de Salida
        private void realizarAjusteSalida(List<BodegaSistema> listaBodegaSistemaSalida, ref string articulosSinIntegridad)
        {
            int idBodega = int.Parse(ddBodega.SelectedValue);
            long idInventarioBasico = int.Parse(_ddlInventariosBasicos.SelectedValue);

            if (ddBodega.SelectedIndex <= 0 || string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue) || idInventarioBasico < -1)
            {
                Mensaje("error", "Por favor compruebe los campos si están completos", "");
                return;
            }

            //Crear un arreglo de clase ArticulosDisponibles
            List<List<ArticulosDisponibles>> ArticulosSistemaFiltradosList = new List<List<ArticulosDisponibles>>();

            //Recorrer la lista de las Bodegas
            for (int i = 0; i < listaBodegaSistemaSalida.Count; i++)
            {
                //Obtener la diferencia filtrata de los artículos 
                var articulosSistemaFiltrados = DiferenciaConjuntosBodegaSistemaGetter.Validar_ObtenerDiferencia(listaBodegaSistemaSalida[i].ArticulosBodega, listaBodegaSistemaSalida[i].ArticulosSistema, out bool tieneIntegridad);

                //Verificar si tiene Integridad los artículos
                if (tieneIntegridad)
                {
                    ArticulosSistemaFiltradosList.Add(articulosSistemaFiltrados);
                }
                else
                {
                    articulosSinIntegridad += " - " + listaBodegaSistemaSalida[i].ArticulosSistema[0].NombreArticulo + " - ";
                }
            }

            //Si la lista es mayor a 0
            if (ArticulosSistemaFiltradosList.Count > 0)
            {
                //Instanciar varias variables 
                List<TRAIngresoSalidaArticulosRecord> listaAGuardar = new List<TRAIngresoSalidaArticulosRecord>();
                var sumUno_RestaCero = false;
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                long idMetodoAccion = 8;
                string idTablaCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos";
                string idCampoDocumentoAccion = "TRACEID.dbo.TRAIngresoSalidaArticulos.idRegistro";
                bool procesado = false;
                var idEstado = 14;

                //Recorrer los artículos y añadir a la lista
                foreach (var articulosSistemaFiltrados in ArticulosSistemaFiltradosList)
                {
                    for (int x = 0; x < articulosSistemaFiltrados.Count; x++)
                    {
                        var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                        sumUno_RestaCero, articulosSistemaFiltrados[x].IdArticulo, articulosSistemaFiltrados[x].FechaVencimiento, articulosSistemaFiltrados[x].Lote,
                        idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, articulosSistemaFiltrados[x].IdRegistro.ToString(),
                        articulosSistemaFiltrados[x].IdUbicacion, articulosSistemaFiltrados[x].Cantidad, procesado, DateTime.Now, idEstado);

                        listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);
                    }
                }

                //Invocar la capa de Negocios el método de ajuste de salida
                N_AjusteInventarioBasico.AjusteSalida(listaAGuardar, idInventarioBasico, idUsuario, idBodega);
            }
        }


        //Método para Realizar un Ajuste de Entrada
        private void realizarAjusteEntrada(List<BodegaSistema> listaBodegaSistemaSalida)
        {
            int idBodega = int.Parse(ddBodega.SelectedValue);
            long idInventarioBasico = int.Parse(_ddlInventariosBasicos.SelectedValue);

            if (ddBodega.SelectedIndex <= 0 || string.IsNullOrEmpty(_ddlInventariosBasicos.SelectedValue) || idInventarioBasico < -1)
            {
                Mensaje("error", "Por favor compruebe los campos si están completos", "");
                return;
            }


            //Crear un arreglo de ICantidadPorUbicacionArticuloRecord
            List<List<ICantidadPorUbicacionArticuloRecord>> ArticulosBodegaFiltradosList = new List<List<ICantidadPorUbicacionArticuloRecord>>();

            for (int i = 0; i < listaBodegaSistemaSalida.Count; i++)
            {
                //Obtener la diferencia filtrata de los artículos 
                var articulosBodegaFiltrados = DiferenciaConjuntosBodegaSistemaGetter.Validar_ObtenerDiferencia(listaBodegaSistemaSalida[i].ArticulosSistema, listaBodegaSistemaSalida[i].ArticulosBodega, out bool tieneIntegridad);

                //Verificar si tiene Integridad los artículos
                if (tieneIntegridad)
                {
                    ArticulosBodegaFiltradosList.Add(articulosBodegaFiltrados);
                }
            }

            //Si la lista es mayor a 0
            if (ArticulosBodegaFiltradosList.Count > 0)
            {
                //Instanciar varias variables 
                var sumUno_RestaCero = true;
                var idUsuario = int.Parse(UsrLogged.IdUsuario);
                long idMetodoAccion = 8;
                string idTablaCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA";
                string idCampoDocumentoAccion = "TRACEID.dbo.LogAjustesTRA.IdLogAjustesTRA";
                string numDocumentoAccion = "";
                bool procesado = false;
                var idEstado = 12;

                List<TRAIngresoSalidaArticulosRecord> listaAGuardar = new List<TRAIngresoSalidaArticulosRecord>();

                //Recorrer los artículos y añadir a la lista
                foreach (var articulosBodegaFiltrados in ArticulosBodegaFiltradosList)
                {
                    for (int x = 0; x < articulosBodegaFiltrados.Count; x++)
                    {

                        var tRAIngresoSalidaArticulosRecord = new TRAIngresoSalidaArticulosRecord(
                        sumUno_RestaCero, articulosBodegaFiltrados[x].IdArticulo, articulosBodegaFiltrados[x].FechaVencimiento, articulosBodegaFiltrados[x].Lote,
                        idUsuario, idMetodoAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, numDocumentoAccion,
                        articulosBodegaFiltrados[x].IdUbicacion, articulosBodegaFiltrados[x].Cantidad, procesado, DateTime.Now, idEstado);

                        listaAGuardar.Add(tRAIngresoSalidaArticulosRecord);

                    }
                }
                //Invocar la capa de Negocios el método de ajuste de entrada
                N_AjusteInventarioBasico.AjusteEntrada(listaAGuardar, idInventarioBasico, idUsuario, idBodega);
            }
        }
        #endregion Métodos de acción de los campos del formulario



        #region Listas de la Página
        //Método de la Lista de Nombre de los Artículos
        private List<NombreIdArticuloRecord> _articulos
        {
            get
            {
                var data = ViewState["articulos"] as List<NombreIdArticuloRecord>;
                if (data == null)
                {
                    data = new List<NombreIdArticuloRecord>();
                    ViewState["articulos"] = data;
                }
                return data;
            }
            set
            {
                ViewState["articulos"] = value;
            }
        }


        //Método de la Lista de las Bodegas de la Toma Física
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


        //Método de la Lista de Artículos Disponibles
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


        //Método de la Lista de la Cantidad de los Artículos de Ubicaciones
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


        //Método de la Lista de Inventarios
        private List<InventarioBasicoRecord> _inventarioBasicoRecords
        {
            get
            {
                var data = ViewState["InventarioBasicoRecords"] as List<InventarioBasicoRecord>;
                if (data == null)
                {
                    data = new List<InventarioBasicoRecord>();
                    ViewState["InventarioBasicoRecords"] = data;
                }
                return data;
            }
            set
            {
                ViewState["InventarioBasicoRecords"] = value;
            }
        }
        #endregion Listas de la Página


        //Método de llenar la tabla 
        protected void RGBodegaFisica_SistemaRecord_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RGBodegaFisica_SistemaRecord.DataSource = _bodegaFisica_SistemaRecordData;
        }

        //Método para mostrar mensajes por medio de la página
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