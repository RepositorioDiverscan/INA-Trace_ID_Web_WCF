
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Diverscan.MJP.Entidades
{
    public static class e_TablasBaseDatos
    {
        public static String TblCompania() { return "ADMCompania";}
        public static String TblTipoDestino() { return "ADMTipoDestino"; }
        public static String TblDestino() { return "ADMDestino"; }
        public static String TblMaestroSolicitud() { return "OPESALMaestroSolicitud"; }
        public static String TblSolicitantes() { return "ADMSolicitante"; }
        public static String TblEstado() { return "ADMEstado"; }
        public static String TblProceso() { return "ADMProceso"; }
        public static String TblUsuarios() { return "Usuarios"; }
        public static String TblMaestroAlisto() { return "MaestroAlisto"; }
        public static String TblDevolucion() { return "Devolucion"; }
        public static String TblTipoDevolucion() { return "TipoDevolucion"; }
        public static String TblTransportista() { return "ADMtransportista"; }
        public static String TblVehiculos() { return "ADMVehiculo"; }
        public static String TblTiposVehiculo() { return "ADMTipoVehiculo"; }
        public static String TblColores() { return "ADMColor"; }
        public static String TblMarcasVehiculos() { return "ADMMarcaVehiculo"; }
        public static String TblActividades() { return "ADMActividad"; }
        public static String TblAcciones() { return "ADMAcciones"; }
        public static String TblFlujos() { return "Flow_Collection"; }
        public static String TblShapes() { return "Flow_Shapes"; }
        public static String TblConnectios() { return "Flow_Connection"; }
        public static String TblAlmacenes() { return "ADMUBIAlmacen"; }
        public static String TblBodegas() { return "ADMUBIBodega"; }
        public static String TblEstantes() { return "ADMMaestroArticulo"; }
        public static String TblZonas() { return "ADMUBIZonas"; }
        public static String TblUnidadesMedida() { return "Unidades_de_Medida"; }
        public static String TblUnidadesEmpaque() { return "Unidades_de_Empaque"; }
        public static String TblTiposEmpaque() { return "Tipos_de_Empaque"; }
        public static String TblMaestroArticulos() { return "ADMMAestroArticulo"; }
        public static String TblMaestroUbicaciones() { return "ADMMaestroUbicacion"; }
        public static String TblMaestroOrdenesCompra() { return "OPEINGMaestroOrdenCompra"; }
        public static String TblDetalleOrdenesCompra() { return "OPEINGDetalleOrdenCompra"; }
        public static String TblProveedores() { return "ADMProveedor"; }
        public static String TblMetodoAccion() { return "Flow_MetodoAccion"; }
        public static String TblParametrosMetodo() { return "Flow_ParametroWF"; }
        public static String TblTipoParametro() { return "Flow_TipoParametro"; }
        public static String TblHorasDia() { return "ADMHorasDia"; }
        public static String TblDestinoRestriccionHorario() { return "ADMDestinoRestriccionHorario"; }
        public static String TblDiaSemana() { return "ADMDiaSemana"; }
        public static String TblHorarioBodega() { return "ADMHorarioBodega"; }
        public static String TblRutas() { return "ADMRuta"; }
        public static String TblBodegaDestino() { return "ADMDuracionBodegaDestino"; }
        public static String TblDetalleSolicitud() { return "OPESALDetalleSolicitud"; }
        public static String TblEventos() { return "Flow_Eventos"; }
        public static String TblGS1AI() { return "GS1IdentificadoresAplicacion"; }
        public static String TblTransaccion() { return "TRAIngresoSalidaArticulos"; }
        public static String TblLogTransacciones() { return "LogTransacciones"; }
        public static String TblMDReferencias() { return "MDReferencias"; }
        public static String TblMDRELkeys() { return "MDRELkeys"; }
        public static String TblEstadoTransaccional() { return "ADMEstadoTransaccional"; }
        public static String TblRoles() { return "SEGRol"; }
        public static String TblRolesWMS() { return "ADMRol"; }
        public static String RELRolMetodoAccion() { return "RELRolMetodoAccion"; }
        public static String VistaDetalleMetodosImplementados() { return "Vista_DetalleMetodosImplementados"; }
        public static String VistaDetalleUbicaciones() { return "Vista_UbicacionesDetalle"; }
        public static String VistaRutaDetalles() { return "Vista_DetallesRuta"; }
        public static String VistaCodigosUbicacion() { return "Vista_CodigosMaestroUbicacion"; }
        public static String VistaDDLParametrosDestino() { return "Vista_DDLParametrosDestino"; }
        public static String VistaObjetosFuente() { return "Vista_ObjetosFuente"; }
        public static String VistaAcciones() { return "Vista_Acciones"; }
        public static String VistaDDLTiposMetodos() { return "Vista_DDLTiposMetodos"; }
        public static String VistaTablasEstadoTransaccional() { return "Vista_TablasEstadoTransaccional"; }
        public static String VistaCampoWMS() { return "Vista_CampoWMS"; }
        public static String VistaCampoERP() { return "Vista_CampoERP"; }
        public static String VistaCamposEstadoTra() { return "Vista_CamposEstadoTra"; }
        public static String VistaPalabras() { return "Palabras"; }
        public static String VistaPendientesAlisto() { return "Vista_PendientesAlisto"; }
        public static String TblTipoTraslado(){ return "ADMTipoTraslado"; }
        public static String TblConsecutivosSSCC() { return "ADMConsecutivosSSCC"; }
        public static String TblSSCCTRA() { return "RELSSCCTRA"; }
        public static String RelUbicacionVehiculo() { return "RelUbicacionVehiculo"; }
        public static String TblDetalleTraslado() { return "ADMDetalleTraslado"; }
        public static String TBLMDTipoPalabra() { return "MDTipoPalabra"; }
        public static String TBLMDContexto() { return "MDContexto"; }
        public static String TBLMDSinonimo() { return "MDSinonimo"; }
        public static String TBLMDSignoPuntuacion() { return "MDSignoPuntuacion"; }
        public static String TBLMDTipoContexto() { return "MDTipoContexto"; }
        public static String TBLADMGTIN14VariableLogistica() { return "ADMGTIN14VariableLogistica"; }
        public static String TBLGTIN14VariableLogistica() { return "Vista_ADMGTIN14VariableLogistica"; }
        public static String TBLMDObjetoHistoria() { return "MDObjetoHistoria"; }
        public static String TBLADMAjusteInventario() { return "ADMAjusteInventario"; }
        public static String TBLRelTiempoMaxUsuarioAccion() { return "RelTiempoMaxUsuarioAccion"; }
        public static String VistaTareasUsuario() { return "Vista_TareasUsuario"; }
        public static String TblGestorImpresion() { return "OPGestorImpresion"; }
        public static String TblAnden() { return "OPEINGAnden"; }
        public static String VistaRecibidor() { return "Vista_Recibidor"; }
        public static String TblCalendarioAnden() { return "OPEINGCalendarioAnden"; }
        public static String TblFamiliaArticulo() { return "ADMFamiliaArticulo"; }
        public static String TblCategoriaArticulo() { return "ADMCategoriaArticulo"; }
        public static String TblRelAccionTiempo() { return "RelAccionTiempo"; }
        public static String TblADMAlistosAprobados() { return "ADMAlistosAprobados"; }
        public static String TblOPEINGAnden() { return "OPEINGAnden"; }
        public static String TblADMRespuestasFormulario() { return "ADMRespuestasFormulario"; }
        public static String VistaPreguntas() { return "Vista_Preguntas"; }
        public static String VistaRespuestas() { return "Vista_Respuestas"; }
        public static String Basedatos() { return "BaseDatos"; }
        public static string TblFormatoexporta() { return "formatoexporta"; }
        public static string VistaMetodoAccion() { return "Vista_MetodoAccionTotal"; }
        public static string TblBitacoraAjustesAplicados() { return "BitacoraAjustesAplicados"; }
        public static string VistaMaestroOrdenCompraProveedor() { return "Vista_MaestroOrdenCompraProveedor"; }
        public static string TblADMProveedor() { return "ADMProveedor"; }
        public static String TblContratos_VendedorEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_vendedor_empresa]"; }
        public static String TblContratos_TipoRelacion() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_tipo_relacion]"; }
        public static String TblContratos_Paises() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_paises]"; }
        public static String TblContratos_SegmentacionEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_segmentacion_empresa]"; }
        public static String TblContratos_ImpuestoEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_impuesto_empresa]"; }
        public static String TblContratos_IndustriaEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_industria_empresa]"; }
        public static String TblContratos_TipoEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_tipo_empresa]"; }
        public static String TblContratos_DescuentoEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_descuento_empresa]"; }
        public static String TblContratos_CreditoEmpresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_credito_empresa]"; }
        public static String TblContratos_Contacto() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_contactos]"; }
        public static String TblContratos_Empresa() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_empresas]"; }
        public static String TblContratos_Productos() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[Productos]"; }
        public static String TblContratos_Contrato() { return "[PROD_DIV_APP_DIVERSCAN].[dbo].[tbl_Contrato]"; }
        public static String VistaPendientesDetalleOrdenCompra() { return "Vista_PendientesDetalleOrdenCompra"; }
        public static String VistaPendientesDetalleAlisto() { return "Vista_PendientesDetalleAlisto"; }
        public static String VistaBodegaCompania() { return "Vista_BodegaCompania"; }
        public static String VistaRelacionGTIN13GTIN14() { return "Vista_RelacionGTIN13_GTIN14"; }
        public static String VistaArticulosSegunUbicacion() { return "Vista_ArticulosSegunUbicacion"; }
        public static String TBLAccesosporRol() { return "ADMAccesosporRol"; }
        public static String VistaMenuHH() { return "Vista_MenuHH"; }
        public static String TBLProgramasHH() { return "ProgramasHH"; }
        public static String VistaArticulosSAP() { return "Vista_MaestroArticulosSAP"; }
        public static String VistaMaestroUbicaciones() { return "Vista_MaestroUbicaciones"; }
        public static String TBLTareasAsignadasUsuarios() { return "TareasUsuario"; }
        public static String VistaAlistosPendientes() { return "Vista_AlistosPendientes"; }
        public static String TBLOpearticulosDespachados() { return "OPEArticulosDespachados"; }
        public static String VistaMaestroSolicitudDestino() { return "Vista_MaestroSolicitudDestino"; }
        public static String TBLOPEArticulosRechazadosOC() { return "OPEArticulosRechazadosOC"; }
        public static String VistaDetalleOrdenCompraCEDI() { return "Vista_DetalleOrdenCompraCEDI"; }
        public static String VistaNombresArticulos() { return "Vista_NombresArticulos"; }
        public static String VistaRolesSinAdmin() { return "Vista_RolesSinAdmin"; }
        public static String VistaDestinosBexim() { return "Vista_Destinos_bexim"; }
        public static String VistaUsuariosSinAdmin() { return "Vista_UsuariosSinAdmin"; }
        public static String VistaUbicacionesDetalle() { return "Vista_UbicacionesDetalle"; }
        public static String TraVehiculoTrasladoSSSCC() { return "TraVehiculoTrasladoSSSCC"; }
        public static String VistaArticulosInternos() { return "Vista_ArticulosInternos"; }
        public static String VistaArticulosInternosVD() { return "Vista_ArticulosInternos_VD"; }
        public static String VistaDisponibilidadArticulos() { return "Vista_DisponibilidadArticulos"; }
    }

    public static class e_VistasSINC
    {
        public static String VistaSINCReferencias() { return "Vista_SINCReferencias"; }
        public static String VistaSINCRELkeys() { return "Vista_SINCRELkeys"; }
        public static String VistaSINCAcciones() { return "Vista_SINCAcciones"; }
        public static String VistaSINCLogTransacciones() { return "Vista_SINCLogTransacciones"; }
    }

 


    public static class e_TblZonasFields
    {
        public static String idZona() { return "idZona"; }
        public static String Abreviatura() { return "Abreviatura"; }
        public static String Nombre() { return "Nombre"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String Estado() { return "Estado"; }
        public static String FechaRegistro() { return "FechaRegistro"; }
        public static String secuencia() { return "secuencia"; }
    }

    public static class e_TblDetalleSolicitudFields
    {
        public static String idLineaDetalleSolicitud() { return "idLineaDetalleSolicitud"; }
        public static String Nombre() { return "Nombre"; }
        public static String idMaestroSolicitud() { return "idMaestroSolicitud"; }
        public static String idDestino() { return "idDestino"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String Cantidad() { return "Cantidad"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String IdCompania() { return "IdCompania"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String Procesado() { return "Procesado"; }
    }

    public static class e_VistaDestinoBexim
    {

        public static String  idDestino () { return "idDestino";  }
        public static String Nombre() { return "Nombre"; }


    }

    public static class e_TblGestorImpresionFields
    {
        public static String idGestorImpresion() { return "idGestorImpresion"; }
        public static String CodigoInterno() { return "CodigoInterno"; }
        public static String Estado() { return "Estado"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String TipoEtiqueta() { return "TipoEtiqueta"; }
        public static String Cantidad() { return "Cantidad"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
        public static String Lote() { return "Lote"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String CodigoEtiqueta() { return "CodigoEtiqueta"; }
        public static String idCataLogo() { return "idCataLogo"; }
        public static String idCompania() { return "idCompania"; }
    }

    public static class e_TblCompaniaFields
    {
        public static String IdCompania() { return "IdCompania"; }
        public static String Nombre() { return "Nombre"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String Codigo() { return "Codigo"; }
    }

    public static class e_TblSSCCTRAFields
    {
        public static String idRELSSCCTRA() { return "idRELSSCCTRA"; }
        public static String idRegistro() { return "idRegistro"; }
        public static String idConsecutivoSSCC() { return "idConsecutivoSSCC"; }
        public static String artDevuelto() { return "artDevuelto"; }
        public static String ubicacionDevuelto() { return "ubicacionDevuelto"; }
        public static String idMaestroSolicitud() { return "idMaestroSolicitud"; }
    }

    public static class e_TblConsecutivosSSCCFields
    {
        public static String idConsecutivoSSCC() { return "idConsecutivoSSCC"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String ConsecutivoSSCC() { return "ConsecutivoSSCC"; }
        public static String TipoCodigo() { return "TipoCodigo"; }
        public static String SSCCGenerado() { return "SSCCGenerado"; }
        public static String idCompania() { return "idCompania"; }
        public static String FechaRegistro() { return "FechaRegistro"; }
        public static String Procesado() { return "Procesado"; }
        public static String FechaProcesado() { return "FechaProcesado"; }
    }

    public static class e_VistaPendientesAlistoFields
    {
        public static String idRegistro() { return "idRegistro"; }
        public static String SumUno_RestaCero() { return "SumUno_RestaCero"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String Nombre() { return "Nombre"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
        public static String Lote() { return "Lote"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String idTablaCampoDocumentoAccion() { return "idTablaCampoDocumentoAccion"; }
        public static String idCampoDocumentoAccion() { return "idCampoDocumentoAccion"; }
        public static String NumDocumentoAccion() { return "NumDocumentoAccion"; }
        public static String idUbicacion() { return "idUbicacion"; }
        public static String Cantidad() { return "Cantidad"; }
        public static String idEstado() { return "idEstado"; }
        public static String ETIQUETA() { return "ETIQUETA"; }
        public static String idDia() { return "idDia"; }
        public static String TiempoEstimado() { return "TiempoEstimado"; }
        public static String idCompania() { return "idCompania"; }
    }

    public static class e_TblTipoTrasladoFields
    {
        public static String idTipoTraslado() { return "idTipoTraslado"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String Nombre() { return "Nombre"; }
        public static String Comentarios() { return "Comentarios"; }
    }

    public static class e_TblDetalleTrasladoFields
    {
        public static String idDetalleTraslado() { return "idDetalleTraslado"; }
        public static String idTipoTraslado() { return "idTipoTraslado"; }
        public static String idZona() { return "idZona"; }
        public static String OrigenDestino() { return "OrigenDestino"; }
    }

    public static class e_TablaObjetoHistoria
    {
        public static String idObjetoHistoria() { return "idObjetoHistoria"; }
        public static String DICCIONARIO() { return "Diccionario"; }
        public static String Dependiente() { return "Dependiente"; }
        public static String Independiente() { return "Independiente"; }
        public static String Valor() { return "Valor"; }
        public static String max_length() { return "max_length"; }
        public static String is_identity() { return "is_identity"; }
        public static String Precision() { return "Precision"; }
    }

    public static class e_VistaTareaUsuarioFields
    {
        public static String IdTarea() { return "IdTarea"; }
        public static String idRegistro() { return "idRegistro"; }
        public static String TiempoEstimado() { return "TiempoEstimado"; }
        public static String IDUSUARIO() { return "IDUSUARIO"; }
        public static String HHDisponiblesParaTarea() { return "HHDisponiblesParaTarea"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String num_solicitud() {return "num_solicitud";}
        public static String Lote() {return "Lote";}
        public static String FechaVencimiento() {return "FechaVencimiento";}
        public static String idArticulo() {return "idArticulo";}
        public static String Cantidad() { return "Cantidad"; }
        public static String idUbicacion() { return "idUbicacion"; }
        public static String Secuencia() { return "Secuencia"; }
        public static string idBodega() { return "idBodega"; }
    }

    public static class e_TBLADMGTIN14VariableLogisticaFields
    {
        public static String idGTIN14VariableLogistica() { return "idGTIN14VariableLogistica"; }
        public static String ConsecutivoGTIN14() { return "ConsecutivoGTIN14"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String Cantidad() { return "Cantidad"; }
    }

    public static class e_VistaPalabrasFields
    {
        public static String DICCIONARIO() { return "DICCIONARIO"; }
        public static String Dependiente() { return "Dependiente"; }
        public static String Independiente() { return "Independiente"; }
        public static String max_length() { return "max_length"; }
        public static String Precision() { return "Precision"; }
        public static String is_identity() { return "is_identity"; }
    }

    public static class e_TblEstadoTransaccionalFields
    {
        public static String IdEstadoTransaccional() { return "IdEstadoTransaccional"; }
        public static String BaseDatos() { return "BaseDatos"; }
        public static String idTabla() { return "idTabla"; }
        public static String IdCampo() { return "IdCampo"; }
        public static String IdEstado() { return "IdEstado"; }
        public static String SumUno_RestaCero() { return "SumUno_RestaCero"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
    }

    public static class e_TblMDRELkeysFields
    {
        public static String IdRelKey() { return "IdRelKey"; }
        public static String IdCompania() { return "IdCompania"; }
        public static String CampoDestino() { return "CampoDestino"; }
        public static String IdDestino() { return "IdDestino"; }
        public static String CampoOrigen() { return "CampoOrigen"; }
        public static String IdOrigen() { return "IdOrigen"; }
    }

    public static class e_TblMDReferenciasFields
    {
        public static String IdReferencia() { return "IdReferencia"; }
        public static String IdCompania() { return "IdCompania"; }
        public static String TablaDestino() { return "TablaDestino"; }
        public static String CampoDestino() { return "CampoDestino"; }
        public static String TablaOrigen() { return "TablaOrigen"; }
        public static String CampoOrigen() { return "CampoOrigen"; }
        public static String PrimaryKey() { return "PrimaryKey"; }
        public static String Envia() { return "Envia"; }
    }

    public static class e_TblLogTransaccionesFields
    {
        public static String IdLog() { return "IdLog"; }
        public static String TipoTrn() { return "TipoTrn"; }
        public static String Tabla() { return "Tabla"; }
        public static String PK() { return "PK"; }
        public static String Campo() { return "Campo"; }
        public static String ValorOriginal() { return "ValorOriginal"; }
        public static String ValorNuevo() { return "ValorNuevo"; }
        public static String Usuario() { return "Usuario"; }
        public static String PKNombre() { return "PKNombre"; }
        public static String PKId() { return "PKId"; }
        public static String Procesado() { return "Procesado"; }
    }

    public static class e_VistaAccionesFields
    {
        public static String idAccion() { return "txtidAccion"; }
        public static String idActividad() { return "ddlidActividad"; }
        public static String Nombre() { return "txtNombre"; }
        public static String Descripcion() { return "txmDescripcion"; }
        public static String Fuente() { return "ddlFuente"; }
        public static String ObjetoFuente() { return "ddlObjetoFuente"; }
        public static String idEvento() { return "ddlidEvento"; }
    }

    public static class e_TblTransaccionFields
    {
        public static String idRegistro() { return "idRegistro"; }
        public static String SumUno_RestaCero() { return "SumUno_RestaCero"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
        public static String Lote() { return "Lote"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String idTablaCampoDocumentoAccion() { return "idTablaCampoDocumentoAccion"; }
        public static String idCampoDocumentoAccion() { return "idCampoDocumentoAccion"; }
        public static String NumDocumentoAccion() { return "NumDocumentoAccion"; }
        public static String idUbicacion() { return "idUbicacion"; }
        public static String Cantidad() { return "Cantidad"; }
        public static String Procesado() { return "Procesado"; }
        public static String idEstado() { return "idEstado"; }
        public static String Fecharegistro() { return "FechaRegistro"; }
    }

    public static class e_TblEventosFields
    {
        public static String idEvento() { return "idEvento"; }
        public static String Nombre() { return "Nombre"; }
        public static String Descripcion() { return "Descripcion"; }
    }

    public static class e_TblAccionesFields
    {
        public static String idAccion() { return "idAccion"; }
        public static String idActividad() { return "idActividad"; }
        public static String Nombre() { return "Nombre"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String Fuente() { return "Fuente"; }
        public static String ObjetoFuente() { return "ObjetoFuente"; }
        public static String idEvento() { return "idEvento"; }
    }

    public static class e_VistaCodigosUbicacionFields
    {
        public static String idUbicacion() { return "idUbicacion"; }
        public static String CODUBI() { return "CODUBI"; }
        public static String ETIQUETA() { return "ETIQUETA"; }
        public static String CODIGOGS1UBICACION() { return "CODIGOGS1UBICACION"; }
        public static String idCompania() { return "idCompania"; }
        public static String idBodega() { return "idBodega"; }
        public static String idZona() { return "idZona"; }
    }
    
    public static class e_TblGS1AIFields
    {
        public static String idIdentificadorAplicacion() { return "idIdentificadorAplicacion"; }
        public static String Nombre() { return "Nombre"; }
        public static String Estructura() { return "Estructura"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String CantidadDigitos() { return "CantidadDigitos"; }
        public static String Variable() { return "Variable"; }
        public static String EsFecha() { return "EsFecha"; }
        public static String EsNumero() { return "EsNumero"; }
        public static String EsTexto() { return "EsTexto"; }
    }

    public static class e_VistaRutaDetalles 
    {
        public static String idRuta() { return "idRuta"; }
        public static String NombreRuta() { return "NombreRuta"; }
        public static String DescripcionRuta() { return "DescripcionRuta"; }
        public static String ComentariosRuta() { return "ComentariosRuta"; }
        public static String IdVehiculo() { return "TipoVehiculo"; }
        public static String TipoVehiculo() { return "NombreVehiculo"; }
        public static String NombreVehiculo() { return "NombreVehiculo"; }
        public static String Modelo() { return "Modelo"; }
        public static String Placa() { return "Placa"; }
        public static String Comentario() { return "Comentario"; }
        public static String CapacidadPeso() { return "CapacidadPeso"; }
        public static String CapacidadVolumen() { return "CapacidadVolumen"; }
        public static String Color() { return "Color"; }
        public static String MarcaCarro() { return "MarcaCarro"; }
        public static String idTransportista() { return "idTransportista"; }
        public static String NombreTransportista() { return "NombreTransportista"; }
        public static String Telefono() { return "Telefono"; }
        public static String Correo() { return "Correo"; }
        public static String ComentariosTransportista() { return "ComentariosTransportista"; }
        public static String NombreCompañia() { return "NombreCompañia"; }
        public static String idDestino() { return "idDestino"; }
        public static String NombreDestino() { return "NombreDestino"; }
        public static String Direccion() { return "Direccion"; }
        public static String DescripcionDestino() { return "DescripcionDestino"; }
        public static String Estado() { return "Estado"; }
        public static String TipoDestino() { return "TipoDestino"; }
      
    }

     public static class e_VistaUbicacionesDetalle
     {
     
        public static String idAlmacen() { return "idAlmacen"; }
        public static String idCompania() { return "idCompania"; }
        public static String NombreAlmacen() { return "NombreAlmacen"; }
        public static String AbreviaturaAlmacen() { return "AbreviaturaAlmacen"; }
        public static String DescripcionAlmacen() { return "DescripcionAlmacen"; }
        public static String idBodega() { return "idBodega"; }
        public static String SecuenciaBodega() { return "SecuenciaBodega"; }
        public static String NombreBodega() { return "NombreBodega"; }
        public static String AbreviaturaBodega() { return "AbreviaturaBodega"; }
        public static String DescripcionBodega() { return "DescripcionBodega"; }
        public static String idZona() { return "idZona"; }
        public static String NombreZona() { return "NombreZona"; }
        public static String AbreviaturaZona() { return "AbreviaturaZona"; }
        public static String DescripcionZona() { return "DescripcionZona"; }
        public static String idUbicacion() { return "idUbicacion"; }
        public static String SecuenciaUbicacion() { return "SecuenciaUbicacion"; }
        public static String CodUbicacion() { return "CodUbicacion"; }
        public static String Etiqueta() { return "Etiqueta"; }
        public static String estante() { return "estante"; }
        public static String columna() { return "columna"; }
        public static String nivel() { return "nivel"; }
        public static String pos() { return "pos"; }
        public static String largo() { return "largo"; }
        public static String areaAncho() { return "areaAncho"; }
        public static String alto() { return "alto"; }
        public static String cara() { return "cara"; }
        public static String profundidad() { return "profundidad"; }
        public static String CapacidadPesoKilos() { return "CapacidadPesoKilos"; }
        public static String CapacidadVolumenM3() { return "CapacidadVolumenM3"; }
        public static String TipoImpresion() { return "TipoImpresion"; }
        public static String DescripcionUbicacion() { return "DescripcionUbicacion"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String idInterno() { return "idInterno"; }
        public static String NombreArticulo() { return "NombreArticulo"; }
        public static String NombreHH() { return "NombreHH"; }
        public static String GTIN() { return "GTIN"; }
        public static String idUnidadMedida() { return "idUnidadMedida"; }
        public static String idTipoEmpaque() { return "idTipoEmpaque"; }
        public static String idEtiqueta() { return "idEtiqueta"; }
        public static String DuracionHoraAlisto() { return "DuracionHoraAlisto"; }
        public static String PesoKilos() { return "PesoKilos"; }
        public static String DimensionUnidadM3() { return "DimensionUnidadM3"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
        public static String Lote() { return "Lote"; }
        public static String idEstado() { return "idEstado"; }
        public static String NombreEstado() { return "NombreEstado"; }
        public static String CantidadEstado() { return "CantidadEstado"; }
        public static String idBodegaUbicacion() { return "idBodegaUbicacion"; }
        public static String Granel() { return "Granel"; }
     }

    public static class e_VistaCamposAccion
    {
        public static String IdCompania() { return "IdCompania"; }
        public static String NombreCompania() { return "NombreCompania"; }
        public static String idProceso() { return "idProceso"; }
        public static String NombreProceso() { return "NombreProceso"; }
        public static String idActividad() { return "idActividad"; }
        public static String NombreActividad() { return "NombreActividad"; }
        public static String idAccion() { return "idAccion"; }
        public static String NombreAccion() { return "NombreAccion"; }
        public static String Fuente() { return "Fuente"; }
        public static String ObjetoFuente() { return "ObjetoFuente"; }
        public static String idEvento() { return "idEvento"; }
        public static String NombreEvento() { return "NombreEvento"; }
        public static String idParametroAccionSalida() { return "idParametroAccionSalida"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String NombreMetodo() { return "NombreMetodo"; }
        public static String idTipoMetodo() { return "idTipoMetodo"; }
        public static String NombreTipoMetodo() { return "NombreTipoMetodo"; }
        public static String SecuenciaMetodo() { return "SecuenciaMetodo"; }
        public static String AcumulaSalidaMetodo() { return "AcumulaSalidaMetodo"; }
        public static String IdParametroDestino() { return "IdParametroDestino"; } // en el objeto es IdParametroAccion
        public static String idTipoParametro() { return "idTipoParametro"; }
        public static String NombreTipoParametro() { return "NombreTipoParametro"; }
        public static String idParametroAccion() { return "idParametroAccion"; }
        public static String NombreParametroAccion() { return "NombreParametroAccion"; }
        public static String ValorParametro() { return "ValorParametro"; }
        public static String NumeroParametro() { return "NumeroParametro"; }
        public static String MultipleValor() { return "MultipleValor"; }
    }

    public static class e_TblParametrosFields
    {
        public static String idParametroAccion() { return "idParametroAccion"; }
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String idTipoParametro() { return "idTipoParametro"; }
        public static String Nombre() { return "Nombre"; }
        public static String Numero() { return "Numero"; }
        public static String Valor() { return "Valor"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String MultipleValor() { return "MultipleValor"; }
    }

    public static class e_TblMetodoAccionFields
    {
        public static String idMetodoAccion() { return "idMetodoAccion"; }
        public static String idAccion() { return "idAccion"; }
        public static String idMetodo() { return "idMetodo"; }
        public static String Nombre() { return "Nombre"; }
        public static String Descripcion() { return "Descripcion"; }
        public static String AcumulaSalida() { return "AcumulaSalida"; }
        public static String Secuencia() { return "Secuencia"; }
        public static String idParametroAccion() { return "idParametroAccion"; } // llamado idParametroDestino en la vista general
    }

    public static class e_TblShapesFields
    {
        public static String idShape() {return "idShape";}
        public static String idflow() { return "idflow"; }
        public static String content_color() { return "content_color"; }
        public static String fill_color() { return "fill_color"; }
        public static String type_() { return "type_"; }
        public static String x() {return "x";}
        public static String y() {return "y";}
        public static String source_table() { return "source_table"; }
        public static String source_table_key() { return "source_table_key"; }
    }

    public static class e_TblConnectiosFields
    {
        public static String idconnection() { return "idconnection"; }
        public static String idflow() { return "idflow"; }
        public static String nombre() { return "nombre"; }
        public static String idfrom() { return "idfrom"; }
        public static String idto() { return "idto"; }
    }

    public static class e_TblMaestroArticulosFields
    {
        public static String idArticulo() { return "idArticulo"; }
        public static String idCompania() { return "idCompania"; }
        public static String Nombre() { return "Nombre"; }
        public static String NombreHH() { return "NombreHH"; }
        public static String GTIN() { return "GTIN"; }
        public static String idUnidadMedida() { return "idUnidadMedida"; }
        public static String idTipoEmpaque() { return "idTipoEmpaque"; }
        public static String idEtiqueta() { return "idEtiqueta"; }
        public static String DuracionHoraAlisto() { return "DuracionHoraAlisto"; }
        public static String idBodega() { return "idBodega"; }
        public static String PesoKilos() { return "PesoKilos"; }
        public static String DimensionUnidadM3() { return "DimensionUnidadM3"; }
        public static String Equivalencia() { return "Equivalencia"; }
        public static String Granel() { return "Granel"; }
        public static String TemperaturaMaxima() { return "TemperaturaMaxima"; }
        public static String TemperaturaMinima() { return "TemperaturaMinima"; }
        public static String DiasMinimosVencimiento() { return "DiasMinimosVencimiento"; }
        public static String idInterno() { return "idInterno"; }
        public static String Contenido() { return "Contenido"; }
        public static String Unidad_Medida() { return "Unidad_Medida"; }
        public static String DiasMinimosVencimientoRestaurantes() { return "DiasMinimosVencimientoRestaurantes"; }
        public static String FechaRegistro() { return "FechaRegistro"; }
    }

    public static class e_TblDetalleOrdenesCompraFields
    {
        public static String idDetalleOrdenCompra() { return "idDetalleOrdenCompra"; }
        public static String idMaestroOrdenCompra() { return "idMaestroOrdenCompra"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String CantidadxRecibir() { return "CantidadxRecibir"; }
        public static String CantidadRecibida() { return "CantidadRecibida"; }
        public static String FechaRecepcion() { return "FechaRecepcion"; }
        public static String RecibidoOK() { return "RecibidoOK"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String Procesado() { return "Procesado"; }
        public static String Nombre() { return "Nombre"; }
        public static String Comentario() { return "Comentario"; }
        public static String idCompania() { return "idCompania"; }

    }

    public static class e_VistaPregunta
    {
        public static String idPregunta() { return "idPregunta"; }
        public static String Pregunta() { return "Nombre"; }
        public static String VerFormulario() { return "Ver_Formulario"; }
    }

    public static class e_VistaRespuesta
    {
        public static String idRespuesta() { return "idRespuesta"; }
        public static String Respuesta() { return "Respuesta"; }
    }

    public static class e_TblADMRespuestasFormulario
    {
        public static String OrdenCompra() { return "OrdenCompra"; }
        public static String Articulo() { return "Articulo"; }
        public static String idPregunta() { return "IdPregunta"; }
        public static String idRespuesta() { return "IdRespuesta"; }
        public static String Comentarios() { return "Comentarios"; }
        public static String Usuario() { return "Usuario"; } 
        public static String UsuarioAutoriza() { return "UsuarioAutoriza"; }
        public static String idCompania() { return "idCompania"; }
    }

    public static class e_BaseDatos
    {
        public static String LinkServer() { return (ConfigurationManager.AppSettings["server"].ToString().ToUpper()); }
        public static String NombreBD() { return (ConfigurationManager.AppSettings["bd"].ToString().ToUpper()); }
        public static String Esquema() { return "dbo"; }    
    }

   public static class e_TblUsuarios
   {
	  public static String IdUsario() {return "IdUsuario";}
      public static String Nombre() { return "Nombre"; }
	  public static String IdCompania() { return "IdCompania"; }
	  public static String Usuario() { return "Usuario"; } 
	  public static String Contrasenna() { return "Contrasenna"; }
	  public static String IdRol() { return "IdRol"; }
	  public static String Email() { return "Email"; }
	  public static String Comentario() { return "Comentario"; }
	  public static String Esta_Bloqueado() { return "Esta_Bloqueado"; }
	  public static String Nombre_Pila() { return "Nombre_Pila"; }
	  public static String Apellidos_Pila() { return "Apellidos_Pila"; }
	  public static String HorasProductivas() { return "HorasProductivas"; }
    }

   public static class e_TblRoles
   {
      public static String IdRol() { return "IdRol"; }
      public static String Nombre() { return "Nombre"; }
      public static String Descripcion() { return "Descripcion"; }
   }

   public static class e_TblRELRolMetodoAccion
   {
       public static String IdRolaccion() { return "IdRolaccion"; }
       public static String IdRol() { return "IdRol"; }
       public static String IdMetodoAccion() { return "IdMetodoAccion"; }
       public static String Nombre() { return "Nombre"; }
       public static String Descripcion() { return "Descripcion"; }
       public static String PorcentajeTiempoDedicar() { return "PorcentajeTiempoDedicar"; }
   }

   public static class e_TblMaestroUbicacion
   {
      public static String idUbicacion() {return "idUbicacion";}
      public static String idBodega() { return "idBodega"; }
      public static String idZona() { return "idZona"; }
      public static String estante() { return "estante"; }
      public static String nivel() { return "nivel"; }
      public static String columna() { return "columna"; }
      public static String pos() { return "pos"; }
      public static String largo() { return "largo"; }
      public static String areaAncho() { return "areaAncho"; }
      public static String alto() { return "alto"; }
      public static String cara() { return "cara"; }
      public static String profundidad() { return "profundidad"; }
      public static String CapacidadPesoKilos() { return "CapacidadPesoKilos"; }
      public static String CapacidadVolumenM3() { return "CapacidadVolumenM3"; }
      public static String TipoImpresion() { return "TipoImpresion"; }
      public static String Descripcion() { return "Descripcion"; }
      public static String FechaCreacion() { return "FechaCreacion"; }
      public static String Secuencia() { return "Secuencia"; }
   }

   public static class e_TBLTareasAsignadasUsuarios
   {
       public static String idRegistroTareaAsignada() { return "idRegistroTareaAsignada"; }
       public static String idMetodoAccion() { return "idMetodoAccion"; }
       public static String idUsuario() { return "idUsuario"; }
       public static String HorasExtra() { return "HorasExtra"; }
       public static String HorasDisponibles() { return "HorasDisponibles"; }
       public static String idTarea() { return "idTarea"; }
       public static String TiempoEstimadoHora() { return "TiempoEstimadoHora"; }
       public static String FechaRegistro() { return "FechaRegistro"; }
       public static String Num_Solicitud() { return "Num_Solicitud"; }
       public static String Alistado() { return "Alistado"; }
       public static String Suspendido() { return "Suspendido"; }
       public static String Fecha_Despacho() { return "Fecha_Despacho"; }
       public static String Lote() { return "Lote"; }
       public static String Fechavencimiento() { return "Fechavencimiento"; }
       public static String IdArticulo() { return "IdArticulo"; }
       public static String idRegistro() { return "idRegistro"; }
       public static String Cantidad() { return "Cantidad"; }
       public static String idUbicacion() { return "idUbicacion"; }
       public static String Secuencia() { return "Secuencia"; }
       public static String idBodega() { return "idBodega"; }
   }

   public static class e_VistaRelacionGTIN13GTIN14
   {
       public static String idArticulo() { return "idArticulo"; }
       public static String idinterno() { return "idinterno"; }
       public static String idCompania() { return "idCompania"; }
       public static String Nombre() { return "Nombre"; }
       public static String Empaque() { return "Empaque"; }
       public static String GTIN13() { return "GTIN13"; }
       public static String GTIN14() { return "GTIN14"; }
       public static String Equivalencia() { return "Equivalencia"; }
       public static String Granel() { return "Granel"; }
       public static String Descripcion() { return "Descripcion"; }
       public static String Cantidad() { return "Cantidad"; }
       public static String empaque_maestro() { return "empaque_maestro"; }
       public static String contenido() { return "contenido"; }
       public static String unidad_medida() { return "unidad_medida"; }
   }

   public static class e_VistaTareasUsuario
   {
       public static String idMetodoAccion() { return "idMetodoAccion"; }
       public static String IdTarea() { return "IdTarea"; }
       public static String idRegistro() { return "idRegistro"; }
       public static String TiempoEstimado() { return "TiempoEstimado"; }
       public static String IDUSUARIO() { return "IDUSUARIO"; }
       public static String num_solicitud() { return "num_solicitud"; }
       public static String HHDisponiblesParaTarea() { return "HHDisponiblesParaTarea"; }
       public static String Lote() { return "Lote"; }
       public static String Fechavencimiento() { return "Fechavencimiento"; }
       public static String IdArticulo() { return "IdArticulo"; }
   }

   public static class e_VistaMaestroSolicitudField
   {
        public static String idRegistro()  { return "idRegistro"; }
	    public static String SumUno_RestaCero() { return "SumUno_RestaCero"; }
        public static String idArticulo() { return "idArticulo"; }
	    public static String Nombre() { return "Nombre"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
	    public static String Lote() { return "Lote"; }
	    public static String idUsuario() { return "idUsuario"; }
	    public static String Num_Solicitud() { return "Num_Solicitud"; }
	    public static String idMetodoAccion() { return "idMetodoAccion"; }
		public static String idTablaCampoDocumentoAccion() { return "idTablaCampoDocumentoAccion"; }
		public static String idCampoDocumentoAccion() { return "idCampoDocumentoAccion"; }
		public static String NumDocumentoAccion() { return "NumDocumentoAccion"; }
		public static String idUbicacion() { return "idUbicacion"; }
		public static String Cantidad()  { return "Cantidad"; }
		public static String Procesado()  { return "Procesado"; }
		public static String FechaRegistro()  { return "FechaRegistro"; }
		public static String idEstado()  { return "idEstado"; }
		public static String Etiqueta()  { return "Etiqueta"; }
		public static String idDia()  { return "idDia"; }
		public static String TiempoEstimado()  { return "TiempoEstimado"; }
		public static String idCompania()  { return "idCompania"; }
   }

   public static class e_TBLdestino
   {
       public static String idDestino() {return "idDestino";}
       public static String Nombre() {return "Nombre";}
       public static String idTipoDestino() {return "idTipoDestino";}
       public static String Direccion() {return "Direccion";}
       public static String Descripcion() { return "Descripcion"; }
       public static String Estado() { return "Estado"; }
       public static String idCompania() { return "idCompania"; }
       public static String idUsuario() { return "idUsuario"; }
       public static String idInterno() { return "idInterno"; }
   }

     public static class e_TBLOpearticulosDespachados
   {
         public static String idcompania() {return "idcompania";}
         public static String idusuario() {return "idusuario";}
         public static String idmaestrosolicitud() {return "idmaestrosolicitud";}
         public static String idlineadetallesolicitud() {return "idlineadetallesolicitud";}
         public static String idarticulo() {return "idarticulo";}
         public static String cantidad() {return "cantidad";}
         public static String fechavencimiento() {return "fechavencimiento";}
         public static String idConsecutivoSSCC() { return "idConsecutivoSSCC"; }
         public static String lote() { return "lote"; }
         public static String Sincronizado() { return "Sincronizado"; }
         public static String FechaRegistro() { return "FechaRegistro"; }
     }

     public static class e_TblMaestroSolicitudField
     {
         public static String idMaestroSolicitud() { return "idMaestroSolicitud"; }
         public static String idUsuario() { return "idUsuario"; }
         public static String FechaCreacion() { return "FechaCreacion"; }
         public static String Nombre() { return "Nombre"; }
         public static String Comentarios() { return "Comentarios"; }
         public static String idcompania() { return "idcompania"; }
         public static String idDestino() { return "idDestino"; }
         public static String idInterno() { return "idInterno"; }
         public static String Procesada() { return "Procesada"; }
         public static String FechaProcesado() { return "FechaProcesamiento"; }
     }

     public static class e_VistaMaestroSolicitudDestinoField
     {
         public static String idMaestroSolicitud() { return "idMaestroSolicitud"; }
         public static String Nombre() { return "Nombre"; }
         public static String Comentarios() { return "Comentarios"; }
         public static String idCompania() { return "idCompania"; }
         public static String idUsuario() { return "idUsuario"; }  
         public static String FechaCreacion() { return "FechaCreacion"; }
         public static String FechaProcesamiento() { return "FechaProcesamiento"; }
         public static String Procesada() { return "Procesada"; }
         public static String idInterno() { return "idInterno"; }
         public static String idInternoSAP() { return "idInternoSAP"; }
         public static String Destino() { return "Destino"; }
         public static String IdBodega() { return "IdBodega"; }
        
     }

    public static class e_TBLOPEArticulosRechazadosOCField 
    {
        public static String idArticulosRechazadosOC() {return "idArticulosRechazadosOC";}
        public static String idCompania() { return "idCompania"; }
        public static String idUsuario() { return "idUsuario"; }
        public static String idMaestroOrdenCompra() { return "idMaestroOrdenCompra"; }
        public static String idArticulo() { return "idArticulo"; }
        public static String Cantidad() { return "Cantidad"; }
        public static String Lote() { return "Lote"; }
        public static String FechaVencimiento() { return "FechaVencimiento"; }
        public static String Sincronizado() { return "Sincronizado"; }
        public static String FechaRegistro() { return "FechaRegistro"; }
    }

    public static class e_vistaDetalleOrdenCompraCEDI
    {
        public static String OCTraceid() { return "OC_Traceid"; }
        public static String OCERP() { return "OC_ERP"; }
        public static String Proveedor() { return "Proveedor"; }
        public static String Fecharecepcion() { return "Fecharecepcion"; }
        public static String Nombre() { return "Nombre"; }
        public static String CantidadxRecibir() { return "CantidadxRecibir"; }
    }

    public static class e_VistaArticulosSegunUbicacion
    {
        public static String idArticulo() { return "idArticulo"; }
        public static String Nombre() { return "Nombre"; } 
        public static String FechaVencimiento() { return "FechaVencimiento"; } 
        public static String Lote() { return "Lote"; } 
        public static String Cantidad() { return "Cantidad"; } 
        public static String idUbicacion() { return "idUbicacion"; } 
        public static String ddlidBodega() { return "ddlidBodega"; } 
        public static String ddlidZona() { return "ddlidZona"; } 
        public static String txtdescripcion() { return "txtdescripcion"; } 
        public static String ETIQUETA() { return "ETIQUETA"; } 
        public static String idMetodoAccion() { return "idMetodoAccion"; } 
        public static String idEstado() { return "idEstado"; }
        public static String Estado_Nombre() { return "idEstado"; }
        public static String idRegistro() { return "idRegistro"; }
        public static String NumDocumentoAccion() { return "NumDocumentoAccion"; }
        public static String idTablaCampoDocumentoAccion() { return "idTablaCampoDocumentoAccion"; }
        public static String PesoKilos() { return "Pesokilos"; }
        public static String Granel() { return "Granel"; }
    }

    public static class e_TraVehiculoTrasladoSSSCC
    {
       public static String ConsecutivoSSCC()  { return "ConsecutivoSSCC"; }
       public static String idVehiculo() { return "idVehiculo"; }
       public static String idDia() { return "idDia"; }
       public static String idHoraDia() { return "idHoraDia"; }
       public static String PesoKilos() { return "PesoKilos"; }
       public static String DimensionM3() { return "DimensionM3"; }
       public static String Equivalencia() { return "Equivalencia"; }
       public static String Fecha() { return "Fecha"; }
       public static String IdUbicacionParqueo() { return "IdUbicacionParqueo"; }
    }
}
