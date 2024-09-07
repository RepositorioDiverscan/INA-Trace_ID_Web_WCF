<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionPedido.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.GestionPedido.GestionPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="maincontext" style="width:80%; margin-left:250px; padding:10px">
             
        <h1>Lista de Pedidos Originales</h1>

        <!--Tabla de Encabezados Pedidos Originales-->
        <div class="table-responsive mt-5 mb-5">
            <table class="table mb-4 text-center">   

                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>ID Interno</th>
                        <th>Nombre Destino</th>
                        <th>Fecha Creación</th>
                        <th>Fecha Entrega</th>
                        <th>Tipo de Pedido</th>
                        <th>Profesor</th>
                        <th>Estado</th>
                        <th>Acciones</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(pedido, key) in pedidosEncabezados" :key="key">
                        <td>{{pedido.IdInterno}}</td>
                        <td>{{pedido.NombreDestino}}</td>
                        <td>{{pedido.FechaCreacion}}</td>
                        <td>{{pedido.FechaEntrega}}</td>
                        <td>{{pedido.Tipo}}</td>
                        <td>{{pedido.Profesor}}</td>
                        <td>{{pedido.Estado}}</td>
                        <td>
                            <span title="Ver Detalle" @click="ObtenerDetallePedidos(pedido)">
                                <i class="fa-solid fa-eye text-primary" style="font-size: 20px !important; cursor: pointer;"></i>
                            </span>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>


        <!--Tabla de Detalle de Pedidos-->
        <div class="table-responsive mt-5 mb-5">
            <div id="tablaDetallePedidos">

                <h1>Detalle del Pedido Original</h1>

                <table  class="table mt-4 text-center col-12">
                    <thead style="background-color: #2E5C8E" class="text-white">
                        <tr>
                            <th>ID Interno</th>
                            <th>Nombre</th>
                            <th>Cantidad Solicitada</th>
                            <th>Cantidad a Resolver</th>
                            <th>Cantidad Disponible</th>
                            <th style="width: 15%;">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="pedido in pedidosDetalle">
                            <td>{{pedido.IdArticuloInterno}}</td>
                            <td>{{pedido.Nombre}}</td>
                            <td>{{pedido.CantidadSolicitada}}</td>
                            <td>{{pedido.CantidadResolver}}</td>
                            <td>{{pedido.CantidadDisponible}}</td>
                            <td>
                                <span title="Alistar Artículo" @click="MostrarCantidadAlistar(pedido)">
                                    <i class="fa-solid fa-dolly text-primary" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                <span title="Solicitud Traslado Bodega" class="ml-3 mr-3" @click="MostrarBodegas(pedido)">
                                    <i class="fa-solid fa-truck-arrow-right text-warning" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                <span title="Agregar a Caja Chica"  @click="MostrarCantidadCajaChica(pedido)">
                                    <i class="fa-solid fa-cart-shopping text-success" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Tabla de Encabezados Solicitud Alisto-->
        <div class="table-responsive mt-5 mb-5">
            <div >

                <h1>Lista de Solicitudes de Alisto</h1>


                <table  class="table mt-4 text-center col-12">
                    <thead class="text-white bg-primary">
                        <tr>
                            <th>Solicitud</th>
                            <th>Fecha Registro</th>
                            <th>Fecha Creación</th>
                            <th>Número de Pedido</th>
                            <th>Estado Solicitud</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="alisto in listaEncabezadosSolicitudAlisto">
                            <td>{{alisto.IdMaestroSolicitud}}</td>
                            <td>{{alisto.FechaRegistro}}</td>
                            <td>{{alisto.FechaCreacion}}</td>
                            <td>{{alisto.IdPedidoOriginal}}</td>
                            <td>{{alisto.Estado}}</td>
                            <td>

                                <span class="mr-3" title="Eliminar Solicitud de Alisto" @click="EliminarSolicitudAlisto(alisto.IdMaestroSolicitud)">
                                    <i class="fa-solid fa-trash text-danger" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                <span title="Ver detalle" @click="MostrarDetalleSolicitudAlisto(alisto.IdMaestroSolicitud)">
                                    <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Tabla de Encabezados Solicitudes de Traslado Bodega-->
        <div class="table-responsive mt-5 mb-5">
            <div id="tablaEncabezados">

                <h1>Lista de Solicitudes de Traslado Bodega</h1>

                <table  class="table mt-4 text-center col-12">
                    <thead class="text-white bg-warning">
                        <tr>
                            <th>Solicitud</th>
                            <th>Nombre Solicitante</th>
                            <th>Bodega Origen</th>
                            <th>Bodega Destino</th>
                            <th>Número de Pedido</th>
                            <th>Estado Solicitud</th>
                            <th>Fecha Registro</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="encabezado in listaEncabezadosSolicitudes">
                            <td>{{encabezado.IdSolicitudTrasladoBodega}}</td>
                            <td>{{encabezado.NombreSolicitante}}</td>
                            <td>{{encabezado.NombreBodegaOrigen}}</td>
                            <td>{{encabezado.NombreBodegaDestino}}</td>
                            <td>{{encabezado.IdPedidoOriginal}}</td>
                            <td>{{encabezado.Estado}}</td>
                            <td>{{encabezado.FechaRegistro}}</td>
                            <td>
                                <span class="mr-3" title="Eliminar Solicitud de Traslado" @click="eliminarSolicitudTraslado(encabezado.IdSolicitudTrasladoBodega)">
                                    <i class="fa-solid fa-trash text-danger" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                <span title="Ver detalle" @click="obtenerDetalleSolicitudes(encabezado.IdSolicitudTrasladoBodega)">
                                    <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

   
        <!--Tabla de Encabezados de Caja Chica-->
        <div class="table-responsive mt-5 mb-5">
               <div id="tablaEncabezadosCC">

                <h1>Lista de Ordenes Caja Chica</h1>


                <table  class="table mt-4 text-center col-12">
                    <thead class="text-white bg-success">
                        <tr>
                            <th>Solicitud</th>
                            <th>Pedido</th>
                            <th>Nombre Solicitante</th>
                            <th>Estado Solicitud</th>
                            <th>Fecha Registro</th>
                            <th>Fecha Cierre</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="encabezadoCC in listaEncabezadosCC">
                            <td>{{encabezadoCC.IdCaja}}</td>
                            <td>{{encabezadoCC.Pedido}}</td>
                            <td>{{encabezadoCC.NombreUsuario}}</td>
                            <td>{{encabezadoCC.Estado}}</td>
                            <td>{{encabezadoCC.Registro}}</td>
                            <td>{{encabezadoCC.Cierre}}</td>
                            <td>
                                <span title="Procesar Caja Chica" class="text-success" @click="ProcesarCajaChica(encabezadoCC.IdCaja)">
                                    <i class="fa-solid fa-clipboard-check" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                <span title="Eliminar Caja Chica" class="text-danger mr-3 ml-3" @click="EliminarCajaChica(encabezadoCC.IdCaja)">
                                    <i class="fa-solid fa-trash" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>

                                 <span title="Ver detalle" @click="MostrasDetalleCC(encabezadoCC.IdCaja)">
                                    <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Modal para mostrar las bodegas de Solicitud de Traslado Bodega-->
        <div class="modal fade" id="ModalBodegas" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg .modal-dialog-scrollable modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabel">Bodegas</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <!--Tabla de Bodegas que tienen Cantidad-->
                        <div class="table-responsive ">
                            <table class="table mt-4 text-center" >
                            <thead class="text-white bg-warning">
                                <tr>
                                    <th>Bodega</th>
                                    <th>Artículo</th>
                                    <th>Cantidad Disponible</th>
                                    <th>Cantidad a Solicitar</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr v-for="(solicitud, index) in bodegasSolicitudes" :key="index">
                                    <td>{{solicitud.NombreBodega}}</td>
                                    <td>{{solicitud.NombreArticulo}}</td>
                                    <td>{{solicitud.CantidadDisponible}}</td>
                                    <td>
                                        <input type="tel" name="txt_cantidadTB" id="txt_cantidadTB" minlength="1" maxlength="3"
                                        v-model="txt_cantidadTB" class="form-control" @keypress="ValidarSoloNumeros(event)"/>
                                    </td>
                                    <td>
                                        <span title="Agregar a solicitud" @click="AgregarArticuloSolicitud(solicitud)">
                                            <i class="fa-solid fa-plus text-warning" style="font-size: 20px !important; cursor: pointer;"></i>
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                         </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar el detalle de las Solicitud de Traslado Bodega-->
        <div class="modal fade" id="ModalDetalleSolicitudes" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg .modal-dialog-scrollable modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelDetalle">Detalle de la Solicitud de Traslado</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="table-responsive">
                            <table class="table mt-4 text-center" >
                            <thead class="text-white  bg-warning">
                                <tr>
                                    <th>Detalle</th>
                                    <th>Artículo</th>
                                    <th>Cantidad Solicitada</th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr v-for="(detalle, index) in listaDetalleSolicitudes" :key="index">
                                    <td>{{detalle.IdDetalle}}</td>
                                    <td>{{detalle.NombreArticulo}}</td>
                                    <td>{{detalle.Cantidad}}</td>                                    
                                </tr>
                            </tbody>
                        </table>
                         </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar el detalle de la caja chica-->
        <div class="modal fade" id="ModalDetalleSolicitudesCC" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg .modal-dialog-scrollable modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelDetalleCC">Detalle de Orden de Caja Chica</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="table-responsive">
                            <table class="table mt-4 text-center" >
                            <thead class="text-white bg-success">
                                <tr>
                                    <th>Detalle</th>
                                    <th>Artículo</th>
                                    <th>Cantidad Solicitada</th>
                                    <th>Fecha Registro</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr v-for="(detalle, index) in listaDetalleCC" :key="index">
                                    <td>{{detalle.Detalle}}</td>
                                    <td>{{detalle.NombreArticulo}}</td>
                                    <td>{{detalle.Cantidad}}</td>
                                    <td>{{detalle.Fecha}}</td>
                                    <td>
                                         <span title="Eliminar Artículo" class="text-danger" @click="EliminarArticuloCajaChica(detalle.Detalle)">
                                            <i class="fa-solid fa-trash" style="font-size: 20px !important; cursor: pointer;"></i>
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                         </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar la cantidad que se desea ingresar en Solicitud de Alisto -->
        <div class="modal fade" id="ModalCantidadCJ" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelCantidadCJ">Solicitud de Caja Chica</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <form>
                            <div class="form-group col-6 mx-auto text-center">
                                <label for="">Ingrese la cantidad a solicitar: </label>
                                <input type="tel" name="txt_cantidadCJ" id="txt_cantidadCJ" minlength="1" maxlength="3"
                                    v-model="txt_cantidadCJ" class="form-control" @keypress="ValidarSoloNumeros(event)"/>
                            </div>
                        </form>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" @click="ComprarCajaChica()">Solicitar Compra</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar el detalle de las Solicitudes de Alisto-->
        <div class="modal fade" id="ModalDetalleSolicitudesAlisto" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg .modal-dialog-scrollable modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelDetalleAlisto">Detalle de Solicitud de Alisto</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="table-responsive">
                            <table class="table mt-4 text-center" >
                            <thead class="text-white bg-primary">
                                <tr>
                                    <th>Detalle</th>
                                    <th>Artículo</th>
                                    <th>Cantidad</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr v-for="(alisto, index) in listaDetalleSolicitudAlisto" :key="index">
                                    <td>{{alisto.IdDetalle}}</td>
                                    <td>{{alisto.Nombre}}</td>
                                    <td>{{alisto.Cantidad}}</td>
                                    <td>
                                         <span title="Eliminar Artículo" class="text-danger" @click="EliminarArticuloSolicitudAlisto(alisto)">
                                            <i class="fa-solid fa-trash" style="font-size: 20px !important; cursor: pointer;"></i>
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                         </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar la cantidad que se desea ingresar en Solicitud de Alisto -->
        <div class="modal fade" id="ModalCantidadAlisto" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelCantidadAlisto">Solicitud de Alisto</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div>
                            <div class="form-group col-6 mx-auto text-center">
                                <label for="">Ingrese la cantidad a solicitar a alistar: </label>
                                <input type="tel" name="txt_cantidadAlisto" id="txt_cantidadAlisto" minlength="1" maxlength="3"
                                    v-model="txt_cantidadAlisto" class="form-control" @keypress="ValidarSoloNumeros(event)"/>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" @click="IngresarSolicitudAlisto()">Solicitar Alisto</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>


        <!--Modal para mostrar mensajes-->
        <div class="modal fade" id="ModalMensaje" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelMensaje" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> Control de Bodegas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        {{mensaje}}
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>

   </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script src="../../Vue/vue.min.js"></script>
    <script src="GestionPedido.js"></script>    
</asp:Content>