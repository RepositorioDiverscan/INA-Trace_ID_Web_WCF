using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Negocio.ProcesarSolicitud;
using Diverscan.MJP.Entidades.ProcesarSolicitud;
using Diverscan.MJP.Entidades.OrdenCompra;
using Diverscan.MJP.Entidades.PICKING;
using Diverscan.MJP.Entidades.SSCC;
using Diverscan.MJP.Entidades.Alistos;
using Diverscan.MJP.Entidades.Devolutions.DevolucionHeader;
using Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail;
using Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation;
using Diverscan.MJP.AccesoDatos.DetalleOrdenCompra;
using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Diverscan.MJP.AccesoDatos.Traslados;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.Entidades.Articulo;
using Diverscan.MJP.AccesoDatos.RolUsuarioHH;
using Diverscan.MJP.AccesoDatos.Articulos.InfoArticulo;
using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.AccesoDatos.Vehiculo;
using Diverscan.MJP.AccesoDatos.RecpecionHH.ProductoRecibido;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion;
using Diverscan.MJP.AccesoDatos.RecpecionHH.RechazoProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.FinalizarProducto;
using Diverscan.MJP.Entidades.Usuarios;
using Diverscan.MJP.Entidades.Rol;
using Diverscan.MJP.Entidades.Pedidos;
using Diverscan.MJP.Entidades.Recepcion;
using Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion;
using Diverscan.MJP.Entidades.Consultas;
using Diverscan.MJP.AccesoDatos.Devolutions;
using Diverscan.MJP.AccesoDatos.Encargado;
using Diverscan.MJP.AccesoDatos.Despacho;

namespace TRACEID.WCF
{
    [ServiceContract]
    public interface IService1
    {
        //[OperationContract]
        //[WebInvoke(Method = "SET",
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "IngresoEvento/{cedula}")]
        //    bool IngresoEvento(string cedula);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ValidarUsuario")]
        e_Usuario ValidarUsuario(string usuario, string contrasenna, string servicePass);


        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "StatusActualOrden/{idMaestroArticulo}")]
        //List<EstadoOrdenCompra> StatusActualOrden(string idMaestroArticulo);


        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPurchaseOrder")]
        List<EGetPucharseOrder> GetPurchaseOrder(string servicespass, string fecha, string idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetDetailPurchaseOrder")]
        ResultGetDetailPurchaseOrder GetDetailPurchaseOrder(string IdMOC, string TipoIngreso);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "CargarEjecutarAccion")]
        string CargarEjecutarAccion(string Pagina, string CodBarras, string idUsuario, string NombreObjeto);

        //---------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        //[OperationContract]

        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ObtenerMenuApp/{idUsuario}/{servicePassw}")]
        //List<e_MenuApp> ObtenerMenuApp(string idUsuario, string servicePassw);

        //[OperationContract]
        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "Test/{idUsuario}")]
        //string Test(string idUsuario);

        //#region Inventario

        //[OperationContract]
        //long OtenerIdUbicacion(string etiqueta);


        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetInventoryReason")]
        List<MotivoAjusteInventarioRecord> ObtenerMotivoInventario(bool tipoAjuste);


        //[OperationContract]
        //GS1Data ExtraerGS1(string CodLeido, string idUsuario);

        //[OperationContract]
        //ArticuloRecord ObtenerArticuloPorIdArticulo(long idArticulo);


        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetArticuloBaseInfo")]
        ResultGetInfoArticulo GetArticuloBaseInfo(string data);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertarSolicitudAjusteInventarioYObtenerIdSolicitud")]
        string InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord,
            List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord);


        //#endregion

        //#region Inventario Ciclico

        //[OperationContract]
        //List<e_InventarioCiclicoRecord> GetInventariosCiclicosPorFecha(DateTime fechaInicio, DateTime fechaFin);

        //[OperationContract]
        //List<MaestroArticuloRecord> GetArticulosPorInventariosCiclicos(int idCategoriaArticulo);

        //[OperationContract]
        //int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario);

        //[OperationContract]
        //List<UbicacionesInventarioCiclicoRecord> ObtenerUbicacionesInventarioCiclico(long idInventario, int estado);

        //[OperationContract]
        //void Actualizar_UbicacionesRealizarInventarioCiclico(long idUbicacionesInventario, int estado);

        //[OperationContract]
        //List<ArticuloCiclicoRealizarRecord> ObtenerArticulosInventarioCiclicoRealizar(long idInventario, int estado);

        //[OperationContract]
        //void ActualizarArticulosInventarioCiclicoRealizar(long idArticulosInventarioCiclico, int estado);
        //#endregion

        //#region Inventario Basico

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerInventarioBasicoRecords")]
        List<InventarioBasicoRecord> ObtenerInventarioBasicoRecords(string fechaInicio, string fechaFin, string idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertarTomaFisicaInventarioBasico")]
        int InsertarTomaFisicaInventarioBasico(TomaFisicaInventario tomaFisicaInventario);

        //#endregion

        //[OperationContract]
        //DateTime GetDateTime();

        //[OperationContract]
        //[WebGet(
        //BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "GetVersionSistemaHH/{servicePass}")]
        //string GetVersionSistemaHH(string servicePass);


        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetVersionSistemaHH")]
        string GetVersionSistemaHH(string servicePass);

        //#region Roles de usuario "Hand Held" || Ultima modificacion: 18-01-2018
        ////[OperationContract]
        ////[WebGet(
        ////    BodyStyle = WebMessageBodyStyle.Wrapped,
        ////    ResponseFormat = WebMessageFormat.Json,
        ////    UriTemplate = "HHGetEsAdministrador/{nombreUsuario}/{contrasena}/{idCompania}/{servicePassw}")]       
        ////string[] HHGetEsAdministrador(string nombreUsuario, string contrasena, string idCompania, string servicePassw);

        //#endregion

        //#region "Validación alisto de artículo no pronto a vencer 02-03-2018 11:47 a.m"
        //[OperationContract]
        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ValidarUsuario/{usuario}/{contrasenna}/{servicePass}")]
        //e_Usuario ValidarUsuarioAutorizador(string usuario, string contrasenna, string servicePass);

        //[OperationContract]
        //[WebGet(
        //BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "AlistarArticuloSSCCValidandoAutorizacionFechaVencimiento/{CodBarras}/{idUsuario}/{servicePass}")]
        //string AlistarArticuloSSCCValidandoAutorizacionFechaVencimiento(string CodBarras, string idUsuario, string servicePass);
        //#endregion

        //#region Despachos "Detalle de despachos HH"
        //[OperationContract]
        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ObtenerSSCCNoDespachadosSolicitud/{SSCCGenerado}/{servicePass}")]
        //List<e_SSCCSolicitud> ObtenerSSCCNoDespachadosSolicitud(string SSCCGenerado, string servicePass);


        //[OperationContract]
        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ObtenerArticulosPendientesSolicitud/{SSCCGenerado}/{servicePass}")]
        //List<e_ArticulosPendientesSolicitud> ObtenerArticulosPendientesSolicitud(string SSCCGenerado, string servicePass);


        //[OperationContract]
        //[WebGet(
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ObtenerDestinoPorSSCCGenerado/{SSCCGenerado}/{servicePass}")]
        //string ObtenerDestinoPorSSCCGenerado(string SSCCGenerado, string servicePass);

        //#endregion

        //#region ConsultasHandHeld
        //#endregion
        //[OperationContract]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "GetPurchaseOrderDetails/{PurchaseOrderId}/{servicePass}")]
        //List<e_OrdenCompra> GetPurchaseOrderDetails(string PurchaseOrderId, string servicePass);



        //#region ConsultaTrasladosPIC
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "ObtenerDisponibilidadPIC")]
        //List<PickingRecord> ObtenerDisponibilidadPIC();
        //#endregion

        //#region ConsultaTrasladosSSCC
        //[OperationContract]
        //List<SSCCRecord> ObtenerSSCC(string Detalle_SSCC);
        //#endregion

        //[OperationContract]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "StatusActualPedido/{idMaestroArticulo}")]
        //List<EstadoAlisto> StatusActualPedido(string idMaestroArticulo);



        [OperationContract]
        [WebInvoke(Method = "POST",
          BodyStyle = WebMessageBodyStyle.Wrapped,
          ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "GetDevolutionsOrders")]
        ResultGetDevolutionHeader GetDevolutionsOrders(string userEmail);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "GetDevolutionsDetails")]
        ResultGetDevolutionDetail GetDevolutionsDetails(string idDevolutionOrder);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "InsertProductLocation")]
        string InsertProductLocation(EDevolutionProduct productLocation);


        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetLocationId")]
        string GetLocationId(string description, int idWarehouse);

        [OperationContract]
        [WebInvoke(Method = "POST",
                BodyStyle = WebMessageBodyStyle.Wrapped,
                ResponseFormat = WebMessageFormat.Json,
                UriTemplate = "InsertArticleRR")]
        string InsertArticleRR(List<EArticulos> eArticulos);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetProductDetailGTIN14")]
        ResultGetGTIN14 GetProductDetailGTIN14(string gtin14);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetProductBaseByGTIN14")]
        string GetProductBaseByGTIN14(string gtin14);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UpdateCertificateOC")]
        string UpdateCertificateOC(int idMOC, string certificate);

        [OperationContract]
        [WebInvoke(Method = "POST",
              BodyStyle = WebMessageBodyStyle.Wrapped,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "GetOnePurchaseOrder")]
        List<EGetPucharseOrder> GetOnePurchaseOrder(string IdInterno, string idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.Wrapped,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "GetProductDetailFromGTIN")]
        List<ETraslado> GetProductDetailFromGTIN(string GTIN);

        [OperationContract]
        [WebInvoke(Method = "POST",
          BodyStyle = WebMessageBodyStyle.Wrapped,
          ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "OutTransferProduct")]
        string OutTransferProduct(int IdArticulo, string Lote, string FechaVencimiento, int IdUbicacionOrigen, int Cantidad, int IdUsuario, int IdMetodoAccionSalida);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "InTransferProduct")]
        string InTransferProduct(int IdArticulo, string Lote, string FechaVencimiento, int IdUbicacionOrigen, int IdUbicacionDestino, int Cantidad, int IdUsuario, int IdMetodoAccionEntrada);

        [OperationContract]
        [WebInvoke(Method = "POST",
          BodyStyle = WebMessageBodyStyle.Wrapped,
          ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "GetMinPicking")]
        List<EMinPicking> GetMinPicking(int IdBodega);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //  BodyStyle = WebMessageBodyStyle.Wrapped,
        //  ResponseFormat = WebMessageFormat.Json,
        //  UriTemplate = "GetSuggestionStorage")]
        //List<EProductStorage> GetSuggestionStorage(int idProduct, int idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetProductByZone")]
        List<WSEArticulo> GetProductByZone(int idProduct, List<EZonaAndroid> zonas, int idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UpdateBillOC")]
        string UpdateBillOC(int idMOC, string numberBill);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerEstadoSalidaUsuario")]
        List<ETraslado> ObtenerEstadoSalidaUsuario(int IdUsuario, int IdMetodoAccion);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetEncabezadoSalidas")]
        List<EEncabezadoSalida> GetEncabezadoSalidas(int idUsuario);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetTareasPendientesPorUsuario")]
        List<ETareasUsuarioSolicitud> GetTareasPendientesPorUsuario(int idUsuario);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetDetalleSalidaOrdenUsuario")]
        List<EDetalleSalidaOrdenUsuario> GetDetalleSalidaOrdenUsuario(int idUsuario, int idMaestroSalida);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertSSCCCode")]
        string InsertSSCCCode(string SSCCCode, int idMaestroSolicitud);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "IngresarArticuloSSCC")]
        string IngresarArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo,
            string lote, string FechaVencimiento, int cantidad, long idUbicacion, long idLineaDetalleSolicitud,
            int idUsuario, int idMetodoAccionSalida);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RevertirArticuloSSCC")]
        string RevertirArticuloSSCC(long idConsecutivoSSCC, long idMaestroSolicitud, long idArticulo,
            string lote, string FechaVencimiento, int cantidad, long idUbicacionDestino,
            long idLineaDetalleSolicitud, int idUsuario, int idMetodoAccionSalida);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerSSCC")]
        ResultGetSSCC ObtenerSSCC(string SSCCConsecutivo);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UbicarSSCC")]
        string UbicarSSCC(int idUbicacion, string consecutivoSSCC);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TransferSSCC")]
        ResultWS TransferSSCC(int idUbicacion, int idSSCC);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetSSCCProducts")]
        ResultGetSSCCProducts GetSSCCProducts(string consecutivoSSCC);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "CertificarLineaSSCC")]
        string CertificarLineaSSCC(string consecutivoSSCC, int idUsuario,
                                                    List<EDetalleSSCCOla> detalleSSCCOlaLista);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RevertirArticuloSSCCCertificado")]
        string RevertirArticuloSSCCCertificado(ERevertirArticuloSSCC articuloSSCC);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InventarioBodegaTradeBook")]
        string InventarioBodegaTradeBook(string idInternoProducto, string idInternoBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "ObtenerExistenciaPorArticulo")]
        string ObtenerExistenciaPorArticulo(ArticuloTrazaInfo articuloTrazaInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "ObtenerRolesUsuariosHH")]
        List<ERolUsuarioHH> ObtenerRolesUsuariosHH(int IdRol);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerIdSolicitudAjusteRefencia")]
        int ObtenerIdSolicitudAjusteRefencia(int idSolicitudAjusteRefencia);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "ObtenerVehiculoXPlaca")]
        //ResultGetVehiculo ObtenerVehiculoXPlaca(string placa);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //BodyStyle = WebMessageBodyStyle.Wrapped,
        //ResponseFormat = WebMessageFormat.Json,
        //UriTemplate = "DescargarVehiculoXPlaca")]
        //string DescargarVehiculoXPlaca(string placa);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerSSCCDespacho")]
        ResultGetSSCCDespatch ObtenerSSCCDespacho(string SSCCConsecutivo);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "CantidadSSCCActivosUsuario")]
        ResultGetSSCCS CantidadSSCCActivosUsuario(long idUsuario);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GenerarSSCC")]
        List<ESSCC> GenerarSSCC(int cantidad);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertProductsPO")]
        ResultadoInsertarProductoRecibido InsertProductsPO(EProductoRecibido productoRecibido);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ReturnProductsPO")]
        ResultadoDevolucionProducto ReturnProductsPO(EDevolcionProducto devolcionProducto);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetReceptionDetailPO")]
        ResultadoDetalleRecepcion GetReceptionDetailPO(long IdDetalleOrdenCompra);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertProductsRejectedPO")]
        ResultadoRechazoProducto InsertProductsRejectedPO(ERechazoProducto rechazoProducto);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "InsertFinishDetailPO")]
        ResultadoFinalizarRecepcionProducto InsertFinishDetailPO(EFinalizarRecepcionProducto finalizarRecepcionProducto);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetEstadoSuspencion")]
        string GetEstadoSuspencion(int idTareaU);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerUsuarios")]
        ResultadoObtenerUsuarios ObtenerUsuarios(string idbodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerRoles")]
        ResultadoObtenerRoles ObtenerRoles(string servicePass);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerPedidos")]
        ResultadoObtenerPedidos ObtenerPedidos(string idbodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerDetallesPedidos")]
        ResultadoObtenerDetallePedido ObtenerDetallesPedidos(string idbodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ingresarPedidosRecibidos")]
        ResultadoIngresarPedidosRecibidos ingresarPedidosRecibidos(List<e_PedidoRecibido> lista_pedidos);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerDevolucionInmediata")]
        ResultadoObtenerDevolucionInmediata ObtenerDevolucionInmediata(string idbodega, string idCausa);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RecibirDevolucionInmediata")]
        string RecibirDevolucionInmediata(e_RecepcionDevolucion e_Recepcion, string idEstadoDevolucion);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerTransportistas")]
        ResultadoObtenerTransportistas ObtenerTransportistas(string idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "IngresarSolicitudDevolucion")]
        string IngresarSolicitudDevolucion(e_EncabezadoSolicitudDevolucion solicitudDevolucion);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerSolicitudesDevolucion")]
        ResultadoObtenerSolicitudesDevolucion ObtenerSolicitudesDevolucion(string idBodega);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerDetallesSolicitud")]
        ResultadoObtenerDetallesSolicitud ObtenerDetallesSolicitud(long idSolicitud);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetArticuloXubicacion")]
        List<EGetArticulosXubicacion> GetArticuloXubi(string ubi);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RecibirSolicitudDevolucion")]
        string RecibirSolicitudDevolucion(EDetalleRecibirSolicitudDevolucion detalle, int usuario);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetLocationIdDevolutionState")]
        int GetLocationIdDevolutionState(bool state, int warehouse);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ObtenerEncargado")]
        EEncargado ObtenerEncargado(int idBodega, string buscar);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "AsignarPedidoEncargado")]
        string AsignarPedidoEncargado(EAsignarDespacho input);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "PedidoOTraslado")]
        byte PedidoOTraslado(string input);
    }
}