using Diverscan.Visitas.Utilidades;
using System.Linq;
using System.Text;
using System;
using System.Data;
using Diverscan.MJP.Negocio;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Negocio.Administracion;
using System.Collections.Generic;
using System.Configuration;
using Diverscan.MJP.Negocio.App;
using Diverscan.MJP.Negocio.AjusteInventario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Negocio.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.Inventario;
using System.IO;
using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Negocio.InventarioBasico;
using Diverscan.MJP.Negocio.ProcesarSolicitud;
using Diverscan.MJP.Entidades.ProcesarSolicitud;
using Diverscan.MJP.Entidades.OrdenCompra;
using Diverscan.MJP.Entidades.PICKING;
using Diverscan.MJP.Negocio.OrdenCompra;
using Diverscan.MJP.Negocio.PICKING;
using Diverscan.MJP.Entidades.SSCC;
using Diverscan.MJP.Negocio.SSCC;
using Diverscan.MJP.Entidades.Alistos;
using Diverscan.MJP.Negocio.Alistos;
using Diverscan.MJP.Negocio.Devolutions;
using Diverscan.MJP.Entidades.Devolutions.DevolucionHeader;
using Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail;
using Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation;

using Diverscan.MJP.Negocio.Traslados;
using Diverscan.MJP.AccesoDatos.DetalleOrdenCompra;
using Diverscan.MJP.Negocio.MaestroArticulo;

using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Diverscan.MJP.Negocio.GTIN14VariableLogistic;
using Diverscan.MJP.AccesoDatos.Traslados;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.Negocio.OPESALMaestroSolicitud;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Existencias;
using Diverscan.MJP.Entidades.Articulo;
using System.Runtime.Serialization;
using Diverscan.MJP.AccesoDatos.RolUsuarioHH;
using Diverscan.MJP.AccesoDatos.Articulos.InfoArticulo;
using Diverscan.MJP.AccesoDatos.Certificación;
using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.AccesoDatos.Vehiculo;
using Diverscan.MJP.AccesoDatos.Despacho;
using Diverscan.MJP.AccesoDatos.RecpecionHH.ProductoRecibido;
using Diverscan.MJP.AccesoDatos.RecpecionHH;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion;
using Diverscan.MJP.AccesoDatos.RecpecionHH.RechazoProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.FinalizarProducto;
using Diverscan.MJP.Negocio.Sincronizador;
using Diverscan.MJP.Entidades.Rol;
using Diverscan.MJP.Entidades.Usuarios;
using Diverscan.MJP.Entidades.Pedidos;
using Diverscan.MJP.Entidades.Recepcion;
using Diverscan.MJP.Negocio.Recepcion;
using Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion;
using Diverscan.MJP.Negocio.Consulta;
using Diverscan.MJP.Entidades.Consultas;

namespace TRACEID.WCF
{
    public class Service1 : IService1
    {
        private readonly FileExceptionWriter _fileExceptionWriter = new FileExceptionWriter();

        public e_Usuario ValidarUsuario(string usuario, string contrasenna, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            try
            {
                if (servicePass.Equals(passService))
                {
                    e_Usuario eUsuario = n_Credenciales.ValidarUsuario(usuario, contrasenna);
                    return eUsuario;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public string CargarEjecutarAccion(string Pagina, string CodBarras, string idUsuario, string NombreObjeto)
        {
            Pagina = clUtilities.ConvertString(Pagina);
            CodBarras = clUtilities.ConvertString(CodBarras);
            NombreObjeto = clUtilities.ConvertString(NombreObjeto);
            return Diverscan.MJP.Negocio.UsoGeneral.n_SmartMaintenance.CargarEjecutarAccion(Pagina, CodBarras, idUsuario, NombreObjeto);
        }

        public List<e_MenuApp> ObtenerMenuApp(string idUsuario, string servicePassw)
        {
            try
            {
                var passService = ConfigurationManager.AppSettings["servicepass"];
                if (servicePassw.Equals(passService))
                {
                    List<e_MenuApp> result = n_App.ObtenerMenuApp(idUsuario);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Test(string idUsuario)
        {
            return "ok";
        }

        #region Inventario

        public long OtenerIdUbicacion(string etiqueta)
        {
            try
            {
                return UbicacionEtiquetaLoader.OtenerIdUbicacion(etiqueta);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public List<MotivoAjusteInventarioRecord> ObtenerMotivoInventario(bool tipoAjuste)
        {
            try
            {
                return MotivoAjusteInventarioLoader.ObtenerRegistros(tipoAjuste);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public GS1Data ExtraerGS1(string CodLeido, string idUsuario)
        {
            try
            {
                return GS1Extractor.ExtraerGS1(CodLeido, idUsuario);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public ResultGetInfoArticulo GetArticuloBaseInfo(string data)
        {
            ResultGetInfoArticulo resultGetInfoArticulo = new ResultGetInfoArticulo();
            try
            {
                ArticuloInfoGetter articuloInfoGetter = new ArticuloInfoGetter();
                resultGetInfoArticulo.ArticuloBaseInfo = articuloInfoGetter.GetArticuloBaseInfo(data);
                resultGetInfoArticulo.state = true;
                resultGetInfoArticulo.Description = "Exitoso";
            }
            catch (Exception ex)
            {
                resultGetInfoArticulo.state = false;
                resultGetInfoArticulo.Description = ex.Message;
            }
            return resultGetInfoArticulo;
        }

        public ArticuloRecord ObtenerArticuloPorIdArticulo(long idArticulo)
        {
            return N_DetalleArticulo.ObtenerArticuloPorIdArticulo(idArticulo);
        }

        public string InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord)
        {
            try
            {
                return N_SolicitudAjusteInventario.InsertarSolicitudAjusteInventarioYObtenerIdSolicitudHH(solicitudAjusteInventarioRecord, articuloXSolicitudAjusteRecord);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                using (StreamWriter writer = new StreamWriter(@"C:\ArchivoError\Archivo.txt"))
                {
                    writer.WriteLine(string.Format("{0}@{1}@{2}", "InserSolicitudAjusteInventarioRecord", ex.Message, ex.StackTrace));
                }

                return "Intente de nuevo";
            }
        }

        #endregion

        #region Inventarios Ciclicos

        public List<e_InventarioCiclicoRecord> GetInventariosCiclicosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return N_InventarioCiclico.GetInventariosCiclicos(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public List<MaestroArticuloRecord> GetArticulosPorInventariosCiclicos(int idCategoriaArticulo)
        {
            try
            {
                return N_MaestroArticulo.GetArticulosInventarioCiclico(idCategoriaArticulo);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario)
        {
            try
            {
                return N_TomaFisicaInventario.InsertarTomaFisicaInventario(tomaFisicaInventario);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public List<UbicacionesInventarioCiclicoRecord> ObtenerUbicacionesInventarioCiclico(long idInventario, int estado)
        {
            try
            {
                return N_UbicacionesInventarioCiclico.ObtenerUbicacionesInventarioCiclico(idInventario, estado);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public void Actualizar_UbicacionesRealizarInventarioCiclico(long idUbicacionesInventario, int estado)
        {
            try
            {
                N_UbicacionesInventarioCiclico.Update_UbicacionesRealizarInventarioCiclico(idUbicacionesInventario, estado);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        public List<ArticuloCiclicoRealizarRecord> ObtenerArticulosInventarioCiclicoRealizar(long idInventario, int estado)
        {
            try
            {
                return N_ArticulosInventarioCiclicoRealiazar.ObtenerArticulosInventarioCiclicoRealizar(idInventario, estado);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }


        public void ActualizarArticulosInventarioCiclicoRealizar(long idArticulosInventarioCiclico, int estado)
        {
            try
            {
                N_UbicacionesInventarioCiclico.ActualizarArticulosInventarioCiclicoRealizar(idArticulosInventarioCiclico, estado);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        #endregion

        #region Inventario Basico

        public List<InventarioBasicoRecord> ObtenerInventarioBasicoRecords(string fechaInicio, string fechaFin, string idBodega)
        {
            return N_InventarioBasico.ObtenerInventarioBasicoRecords(fechaInicio, fechaFin, idBodega);
        }

        public int InsertarTomaFisicaInventarioBasico(TomaFisicaInventario tomaFisicaInventario)
        {
            return N_ArticulosInventarioBasico.InsertarTomaFisicaInventario(tomaFisicaInventario);
        }

        #endregion

        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        #region Hand Held 

        public string GetVersionSistemaHH(string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                return n_Credenciales.GetVersionSistemaHH();
            }
            else
            {
                return null;
            }
        }


        //public string GetPurcharseOrder(string servicePass)
        //{
        //    var passService = ConfigurationManager.AppSettings["servicepass"];
        //    if (servicePass.Equals(passService))
        //    {
        //        n_GetPurchaseOrder getPurchaseOrder = new n_GetPurchaseOrder();
        //        return getPurchaseOrder.GetPurchaseOrder();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #region Roles de usuario "Hand Held" || Ultima modificacion: 18-01-2018

        //public string[] HHGetEsAdministrador(string nombreUsuario, string contrasena, string idCompania, string servicePassw)
        //{            
        //    string[] resultadoConsulta = new string[2];
        //    var passService = ConfigurationManager.AppSettings["servicepass"];
        //    if (servicePassw.Equals(passService))
        //    {
        //        Diverscan.MJP.Negocio.Roles.n_Roles _nRoles = new Diverscan.MJP.Negocio.Roles.n_Roles();
        //        resultadoConsulta = _nRoles.GetEsAdministrador(nombreUsuario, contrasena, idCompania);
        //        return resultadoConsulta;
        //    }
        //    else
        //    {                
        //        resultadoConsulta[0] = "-1";                
        //        resultadoConsulta[1] = "-1";
        //        resultadoConsulta[2] = "-1";
        //        return resultadoConsulta;
        //    }
        //}
        #endregion

        #region "Validación alisto de artículo no pronto a vencer 02-03-2018 11:47 a.m"
        public e_Usuario ValidarUsuarioAutorizador(string usuario, string contrasenna, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                e_Usuario eUsuario = n_Credenciales.ValidarUsuario(usuario, contrasenna);
                if (eUsuario == null)
                {
                    e_Usuario eUsuarioAux = new e_Usuario();
                    return eUsuarioAux;
                }
                else
                {
                    return eUsuario;
                }
            }
            else
            {
                return null;
            }

        }

        public string AlistarArticuloSSCCValidandoAutorizacionFechaVencimiento(string CodBarras, string idUsuario, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                CodBarras = clUtilities.ConvertString(CodBarras);
                n_ProcesarSolicitud n_ProcesarSolicitud = new n_ProcesarSolicitud();
                return n_ProcesarSolicitud.AsociarSSCCV2(CodBarras, idUsuario);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region Despachos "Detalle de despachos HH"
        n_DetalleSSCCArticuloSolicitud _n_DetalleSSCCArticuloSolicitud = new n_DetalleSSCCArticuloSolicitud();

        public List<e_SSCCSolicitud> ObtenerSSCCNoDespachadosSolicitud(string SSCCGenerado, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                return _n_DetalleSSCCArticuloSolicitud.ObtenerSSCCNoDespachadosSolicitud(SSCCGenerado);
            }
            else
            {
                return null;
            }
        }

        public List<e_ArticulosPendientesSolicitud> ObtenerArticulosPendientesSolicitud(string SSCCGenerado, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                return _n_DetalleSSCCArticuloSolicitud.ObtenerArticulosPendientesSolicitud(SSCCGenerado);
            }
            else
            {
                return null;
            }
        }

        public string ObtenerDestinoPorSSCCGenerado(string SSCCGenerado, string servicePass)
        {
            var passService = ConfigurationManager.AppSettings["servicepass"];
            if (servicePass.Equals(passService))
            {
                return _n_DetalleSSCCArticuloSolicitud.ObtenerDestinoPorSSCCGenerado(SSCCGenerado);
            }
            else
            {
                return null;
            }
        }

        public List<e_OrdenCompra> GetPurchaseOrderDetails(string PurchaseOrderId, string servicePass)
        {
            return PurchaseOrdersLoader.GetPurchaseOrderDetails(PurchaseOrderId);
        }


        #endregion

        #endregion

        #region ConsultaTraslados

        public List<PickingRecord> ObtenerDisponibilidadPIC()
        {
            try
            {
                return ArticulosPickingLoader.ObtenerDisponibilidadPIC();
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        #endregion

        #region ConsultaSSCC

        //public List<SSCCRecord> ObtenerSSCC(string Detalle_SSCC)
        //{
        //    try
        //    {
        //        return SSCCLoader.ObtenerSSCC(Detalle_SSCC);
        //    }
        //    catch (Exception ex)
        //    {
        //        clErrores cl = new clErrores();
        //        cl.escribirError(ex.Message, ex.StackTrace);
        //        throw ex;
        //    }
        //}

        #endregion
        public List<EstadoAlisto> StatusActualPedido(string idMaestroArticulo)
        {
            return EstadoAlistosLoader.StatusActualPedido(idMaestroArticulo);
        }

        //Estado Actual de la orden de compra
        public List<EstadoOrdenCompra> StatusActualOrden(string idMaestroArticulo)
        {
            return EstadoOrdenCLoader.StatusActualOrden(idMaestroArticulo);
        }

        public List<EGetPucharseOrder> GetPurchaseOrder(string servicespass, string fecha, string idBodega)
        {
            try
            {
                var passService = ConfigurationManager.AppSettings["servicepass"];
                if (servicespass.Equals(passService))
                {
                    n_GetPurchaseOrder _getPurchaseOrder = new n_GetPurchaseOrder(_fileExceptionWriter);
                    var resultado = _getPurchaseOrder.GetPurchaseOrder(fecha, idBodega);

                    return resultado;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultGetDetailPurchaseOrder GetDetailPurchaseOrder(string IdMOC, string TipoIngreso)
        {
            try
            {
                n_GetPurchaseOrder _getPurchaseOrder = new n_GetPurchaseOrder(_fileExceptionWriter);

                return _getPurchaseOrder.GetDetailPurchaseOrder(IdMOC, TipoIngreso);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResultGetDevolutionHeader GetDevolutionsOrders(string userEmail)
        {
            NDevolutions nDevolutions = new NDevolutions(_fileExceptionWriter);

            return nDevolutions.GetDevolutionsOrders(userEmail);
        }

        public ResultGetDevolutionDetail GetDevolutionsDetails(string idDevolutionOrder)
        {
            NDevolutions nDevolutions = new NDevolutions(_fileExceptionWriter);

            return nDevolutions.GetDevolutionDetailOrder(idDevolutionOrder);
        }

        public string InsertProductLocation(EDevolutionProduct productLocation)
        {
            NDevolutions nDevolutions = new NDevolutions(_fileExceptionWriter);

            return nDevolutions.InsertProductLocation(productLocation);
        }

        public string GetLocationId(string description)
        {

            n_Traslados _Traslados = new n_Traslados();
            return _Traslados.ObtenerIdUbicacion(description);
        }

        public string InsertArticleRR(List<EArticulos> eArticulos)
        {
            NDetalleOrdenC nDetalle = new NDetalleOrdenC();
            return nDetalle.InsertarArticuloRR(eArticulos);

        }

        public ResultGetGTIN14 GetProductDetailGTIN14(string gtin14)
        {
            NGTIN14VariableLogistic nGTIN14Variable = new NGTIN14VariableLogistic(_fileExceptionWriter);

            return nGTIN14Variable.GetProductDetailGTIN14(gtin14);
        }

        public string GetProductBaseByGTIN14(string gtin14)
        {
            NGTIN14VariableLogistic nGTIN14Variable = new NGTIN14VariableLogistic(_fileExceptionWriter);

            return nGTIN14Variable.GetProductBaseByGTIN14(gtin14);
        }

        public string UpdateCertificateOC(int idMOC, string certificate)
        {
            n_GetPurchaseOrder _getPurchaseOrder = new n_GetPurchaseOrder(_fileExceptionWriter);

            return _getPurchaseOrder.UpdateCertificateOC(idMOC, certificate);
        }

        public string UpdateBillOC(int idMOC, string numberBill)
        {
            n_GetPurchaseOrder _getPurchaseOrder = new n_GetPurchaseOrder(_fileExceptionWriter);

            return _getPurchaseOrder.UpdateBillOC(idMOC, numberBill);
        }

        public List<EGetPucharseOrder> GetOnePurchaseOrder(string IdInterno, string idBodega)
        {
            n_GetPurchaseOrder _getPurchaseOrder = new n_GetPurchaseOrder(_fileExceptionWriter);
            return _getPurchaseOrder.GetOnePurchaseOrder(IdInterno, idBodega);
        }

        public List<ETraslado> GetProductDetailFromGTIN(string GTIN)
        {
            NTraslado nTraslado = new NTraslado();
            return nTraslado.ProductDetailFromGtin(GTIN);
        }

        public string OutTransferProduct(int IdArticulo, string Lote, string FechaVencimiento, int IdUbicacionOrigen, int Cantidad, int IdUsuario, int IdMetodoAccionSalida)
        {
            DateTime myDate = DateTime.ParseExact(FechaVencimiento, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            NTraslado nTraslado = new NTraslado();
            return nTraslado.OutTransferProduct(IdArticulo, Lote, myDate, IdUbicacionOrigen, Cantidad, IdUsuario, IdMetodoAccionSalida);
        }

        public string InTransferProduct(int IdArticulo, string Lote, string FechaVencimiento, int IdUbicacionOrigen, int IdUbicacionDestino, int Cantidad, int IdUsuario, int IdMetodoAccionEntrada)
        {
            DateTime myDate = DateTime.ParseExact(FechaVencimiento, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            NTraslado nTraslado = new NTraslado();
            return nTraslado.InTransferProduct(IdArticulo, Lote, myDate, IdUbicacionOrigen, IdUbicacionDestino, Cantidad, IdUsuario, IdMetodoAccionEntrada);
        }

        public List<EMinPicking> GetMinPicking(int IdBodega)
        {
            n_MaestroArticulo n_MaestroArticulo = new n_MaestroArticulo();
            return n_MaestroArticulo.GetMinPicking(IdBodega);
        }

        //public List<EProductStorage> GetSuggestionStorage(int idProduct, int idBodega)
        //{

        //    n_MaestroArticulo n_MaestroArticulo = new n_MaestroArticulo();
        //    return n_MaestroArticulo.GetSuggestionStorage(idProduct, idBodega);
        //}

        public List<WSEArticulo> GetProductByZone(int idProducto, List<EZonaAndroid> zonas, int idBodega)
        {
            NConsultas nConsultas = new NConsultas();


            var articulos = nConsultas.ObtenerArticulosDisponiblesXzonasAndroid(idProducto, zonas, idBodega);
            //articulos = articulos.OrderBy(x => x.FUTSAlida).ToList<EArticulo>();
            if (articulos.Count > 0 && articulos[0].ConTrazabilidad)
                articulos.Sort((p1, p2) => DateTime.Compare(p1.FUTSAlida, p2.FUTSAlida));

            List<WSEArticulo> wsarticulos = new List<WSEArticulo>();
            foreach (var record in articulos)
                wsarticulos.Add(new WSEArticulo(record));
            return wsarticulos;
        }

        public List<ETraslado> ObtenerEstadoSalidaUsuario(int IdUsuario, int IdMetodoAccion)
        {
            NTraslado nTraslado = new NTraslado();
            return nTraslado.ObtenerEstadoSalidaUsuario(IdUsuario, IdMetodoAccion);
        }

        public List<EEncabezadoSalida> GetEncabezadoSalidas(int idUsuario)
        {
            NAlistos nAlistos = new NAlistos();
            return nAlistos.GetEncabezadoSalidas(idUsuario);
        }

        public List<ETareasUsuarioSolicitud> GetTareasPendientesPorUsuario(int idUsuario)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);
            return oPESALMaestroSolicitud.GetTareasPendientesPorUsuario(idUsuario);
        }

        public List<EDetalleSalidaOrdenUsuario> GetDetalleSalidaOrdenUsuario(int idUsuario, int idMaestroSalida)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            OPESALMaestroSolicitud oPESALMaestroSolicitud = new OPESALMaestroSolicitud(fileExceptionWriter);
            return oPESALMaestroSolicitud.GetDetalleSalidaOrdenUsuario(idUsuario, idMaestroSalida);
        }

        public string InsertSSCCCode(string SSCCCode, int idMaestroSolicitud)
        {
            NAlistos nAlistos = new NAlistos();
            return nAlistos.InsertSSCCCode(SSCCCode, idMaestroSolicitud);
        }

        public string IngresarArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, string FechaVencimiento, int cantidad, long idUbicacion, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {
            DateTime FechaVencimientoConver = DateTime.ParseExact(FechaVencimiento, "yyyy-MM-dd",
                                      System.Globalization.CultureInfo.InvariantCulture);
            NAlistos nAlistos = new NAlistos();
            return nAlistos.IngresarArticuloSSCC(idConsecutivoSSCC, idMaestroSolicitud, idArticulo, lote, FechaVencimientoConver, cantidad, idUbicacion, idLineaDetalleSolicitud, idUsuario, idMetodoAccionSalida);
        }

        public string RevertirArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo, string lote, string FechaVencimiento,
                                       int cantidad, long idUbicacionDestino, long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida)
        {
            DateTime FechaVencimientoConver = DateTime.ParseExact(FechaVencimiento, "yyyy-MM-dd",
                                      System.Globalization.CultureInfo.InvariantCulture);
            NAlistos nAlistos = new NAlistos();
            return nAlistos.RevertirArticuloSSCC(idConsecutivoSSCC, idMaestroSolicitud, idArticulo, lote, FechaVencimientoConver, cantidad,
                 idLineaDetalleSolicitud, idLineaDetalleSolicitud, idUsuario, idMetodoAccionSalida);
        }

        public ResultGetSSCC ObtenerSSCC(string SSCCConsecutivo)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.ObtenerSSCC(SSCCConsecutivo);
        }

        public ResultGetSSCCDespatch ObtenerSSCCDespacho(string SSCCConsecutivo)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.ObtenerSSCCDespacho(SSCCConsecutivo);
        }

        public string UbicarSSCC(int idUbicacion, string consecutivoSSCC)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.UbicarSSCC(idUbicacion, consecutivoSSCC);
        }

        public ResultWS TransferSSCC(int idUbicacion, int idSSCC)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.TransferSSCC(idUbicacion, idSSCC);
        }

        public ResultGetSSCCProducts GetSSCCProducts(string consecutivoSSCC)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.GetSSCCProducts(consecutivoSSCC);
        }

        public string RevertirArticuloSSCCCertificado(ERevertirArticuloSSCC articuloSSCC)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.RevertirArticuloSSCCCertificado(articuloSSCC);
        }

        public string CertificarLineaSSCC(string consecutivoSSCC, int idUsuario,
                                                    List<EDetalleSSCCOla> detalleSSCCOlaLista)
        {
            N_Certificacion nCertificacion = new N_Certificacion(_fileExceptionWriter);

            return nCertificacion.CertificarLineaSSCC(consecutivoSSCC, idUsuario, detalleSSCCOlaLista);
        }

        public string InventarioBodegaTradeBook(string idInternoProducto, string idInternoBodega)
        {
            NConsultas nConsultas = new NConsultas();

            return nConsultas.ObtenerInventarioBodegaTradeBook(idInternoProducto, idInternoBodega);

        }

        public string ObtenerExistenciaPorArticulo(ArticuloTrazaInfo articuloTrazaInfo)
        {
            ExistenciasPorArticulo existenciasPorArticulo = new ExistenciasPorArticulo();
            return existenciasPorArticulo.ObtenerExistenciaPorArticulo(articuloTrazaInfo).ToString();
        }

        public List<ERolUsuarioHH> ObtenerRolesUsuariosHH(int IdRol)
        {
            NRolUsuarioHH nRolUsuarioHH = new NRolUsuarioHH();
            return nRolUsuarioHH.ObtenerRolesUsuariosHH(IdRol);
        }

        public int ObtenerIdSolicitudAjusteRefencia(int idSolicitudAjusteRefencia)
        {
            try
            {
                return N_SolicitudAjusteInventario.GetIdSolicitudAjusteRefencia(idSolicitudAjusteRefencia);
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                using (StreamWriter writer = new StreamWriter(@"C:\ArchivoError\Archivo.txt"))
                {
                    writer.WriteLine(string.Format("{0}@{1}@{2}", "InserSolicitudAjusteInventarioRecord", ex.Message, ex.StackTrace));
                }

                return -1;
            }
        }

        public ResultGetVehiculo ObtenerVehiculoXPlaca(string placa)
        {
            NVehiculo nVehiculo = new NVehiculo(_fileExceptionWriter);
            return nVehiculo.GetVehiculoXPlaca(placa);
        }

        public String DescargarVehiculoXPlaca(string placa)
        {
            NVehiculo nVehiculo = new NVehiculo(_fileExceptionWriter);
            return nVehiculo.DescargarVehiculoXPlaca(placa);
        }

        public String CargarVehiculoXPlaca(string placa, long idSSCC, long idUbicacion,
                                                bool capacidadExcedida, bool sobreCargar)
        {
            NDespacho nDespacho = new NDespacho(_fileExceptionWriter);
            return nDespacho.CargarVehiculoXPlaca(placa, idSSCC, idUbicacion, capacidadExcedida, sobreCargar);
        }

        public ResultGetSSCCS CantidadSSCCActivosUsuario(long idUsuario)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.CantidadSSCCActivosUsuario(idUsuario);
        }

        public List<ESSCC> GenerarSSCC(int cantidad)
        {
            NSSCC nSSCC = new NSSCC(_fileExceptionWriter);
            return nSSCC.GenerarSSCC(cantidad);
        }

        public ResultadoInsertarProductoRecibido InsertProductsPO(EProductoRecibido productoRecibido)
        {
            NRecepcionHH nRecepcion = new NRecepcionHH();

            return nRecepcion.InsertarArticuloRecibidoOC(productoRecibido);
        }

        public ResultadoDevolucionProducto ReturnProductsPO(EDevolcionProducto devolcionProducto)
        {
            NRecepcionHH nRecepcion = new NRecepcionHH();

            return nRecepcion.ActualizarCantidadProductoRecibidoOC(devolcionProducto);
        }

        public ResultadoDetalleRecepcion GetReceptionDetailPO(long IdDetalleOrdenCompra)
        {
            NRecepcionHH nRecepcion = new NRecepcionHH();

            return nRecepcion.ObtenerDetalleRecepcionOC(IdDetalleOrdenCompra);
        }

        public ResultadoRechazoProducto InsertProductsRejectedPO(ERechazoProducto rechazoProducto)
        {
            NRecepcionHH nRecepcion = new NRecepcionHH();

            return nRecepcion.InsertarArticuloRechazadoOC(rechazoProducto);
        }

        public ResultadoFinalizarRecepcionProducto InsertFinishDetailPO(EFinalizarRecepcionProducto finalizarRecepcionProducto)
        {
            NRecepcionHH nRecepcion = new NRecepcionHH();

            return nRecepcion.InsertarRecepcionProductoFinalizada(finalizarRecepcionProducto);
        }

        public string GetEstadoSuspencion(int idTareaU)
        {
            return n_EstadoSuspencion.GetEstadoSuspencion(idTareaU);
        }

        public ResultadoObtenerUsuarios ObtenerUsuarios(string idbodega)
        {
            n_Sincronizador _Sincronizador = new n_Sincronizador(_fileExceptionWriter);
            return _Sincronizador.ObtenerUsuarios(idbodega);

        }

        public ResultadoObtenerRoles ObtenerRoles(string servicePass)
        {
            n_Sincronizador _Sincronizador = new n_Sincronizador(_fileExceptionWriter);
            return _Sincronizador.ObtenerRoles(servicePass);
        }

        public ResultadoObtenerPedidos ObtenerPedidos(string idbodega)
        {
            n_Sincronizador _Sincronizador = new n_Sincronizador(_fileExceptionWriter);
            return _Sincronizador.ObtenerPedidos(idbodega);
        }

        public ResultadoObtenerDetallePedido ObtenerDetallesPedidos(string idbodega)
        {
            n_Sincronizador _Sincronizador = new n_Sincronizador(_fileExceptionWriter);
            return _Sincronizador.ObtenerDetallesPedidos(idbodega);
        }

        public ResultadoIngresarPedidosRecibidos ingresarPedidosRecibidos(List<e_PedidoRecibido> lista_pedidos)
        {
            n_Sincronizador _Sincronizador = new n_Sincronizador(_fileExceptionWriter);
            return _Sincronizador.IngresarPedidosRecibidos(lista_pedidos);
        }

        public ResultadoObtenerDevolucionInmediata ObtenerDevolucionInmediata(string idbodega, string idCausa)
        {
            n_DevolucionInmediata _DevolucionInmediata = new n_DevolucionInmediata(_fileExceptionWriter);
            return _DevolucionInmediata.ObtenerDevolucionInmediata(idbodega, idCausa);
        }

        public string RecibirDevolucionInmediata(e_RecepcionDevolucion e_Recepcion, string idEstadoDevolucion)
        {
            n_DevolucionInmediata _DevolucionInmediata = new n_DevolucionInmediata(_fileExceptionWriter);
            return _DevolucionInmediata.RecibirDevolucionInmediata(e_Recepcion, idEstadoDevolucion);
        }

        public ResultadoObtenerTransportistas ObtenerTransportistas(string idBodega)
        {
            n_SolicitudDevolucion _SolicitudDevolucion = new n_SolicitudDevolucion(_fileExceptionWriter);
            return _SolicitudDevolucion.ObtenerTransportistas(idBodega);
        }

        public string IngresarSolicitudDevolucion(e_EncabezadoSolicitudDevolucion solicitudDevolucion)
        {
            n_SolicitudDevolucion _SolicitudDevolucion = new n_SolicitudDevolucion(_fileExceptionWriter);
            return _SolicitudDevolucion.IngresarSolicitudDevolucion(solicitudDevolucion);
        }

        public ResultadoObtenerSolicitudesDevolucion ObtenerSolicitudesDevolucion(string idBodega)
        {
            n_SolicitudDevolucion _SolicitudDevolucion = new n_SolicitudDevolucion(_fileExceptionWriter);
            return _SolicitudDevolucion.ObtenerSolicitudesDevolucion(idBodega);
        }

        public ResultadoObtenerDetallesSolicitud ObtenerDetallesSolicitud(long idSolicitud)
        {
            n_SolicitudDevolucion _SolicitudDevolucion = new n_SolicitudDevolucion(_fileExceptionWriter);
            return _SolicitudDevolucion.ObtenerDetallesSolicitud(idSolicitud);
        }



        public List<EGetArticulosXubicacion> GetArticuloXubi(string ubi)
        {
            try
            {
                n_GetArticulosXubicacion _getArticuloXubi = new n_GetArticulosXubicacion(_fileExceptionWriter);
                var resultado = _getArticuloXubi.GetArticulosXubicacion(ubi);

                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

