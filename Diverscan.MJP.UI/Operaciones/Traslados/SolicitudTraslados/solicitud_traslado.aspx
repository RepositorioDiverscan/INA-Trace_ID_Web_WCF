<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="solicitud_traslado.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Traslados.SolicitudTraslados.solicitud_traslado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="solicitudTraslado" style="width:80%; margin-left:250px; padding:10px">
    
        <!--Tabla de Artículos disponibles en Bodega-->
        <div class="table-responsive mt-2 mb-5">
            <div>
                <h1>Lista de Artículos disponibles en bodega</h1>

                <table class="table mt-5 text-center col-12" id="tablaArticulosBodega">
                    <thead class="text-white text-center" style="background-color: #356191;">
                        <tr>
                            <th>Acciones</th>
                            <th>Nombre Artículo</th>
                            <th>ID Interno</th>
                            <th>Bodega</th>
                            <th>Cantidad</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="articulo in listaArticulosBodega">
                            <td>
                                <span title="Agregar a Solicitud" style="cursor: pointer;" @click="mostrarModalCantidadSolicitar(articulo)">
                                    <i class="fa-solid fa-circle-plus" style="font-size: 20px !important; color: #356191;"></i>
                                </span>
                            </td>
                            <td>{{articulo.Articulo}}</td>
                            <td>{{articulo.IdInterno}}</td>
                            <td>{{articulo.Bodega}}</td>
                            <td>{{articulo.Cantidad}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Tabla de Solicitudes a Bodegas-->
        <div class="table-responsive mt-2 mb-5">
            <div>
                <h1>Lista de Solicitudes realizadas</h1>

                <table class="table mt-2 text-center col-12" id="tablaSolicitudesBodega">
                    <thead class="text-white text-center" style="background-color: #356191;">
                        <tr>
                            <th>Nombre del Solicitante</th>
                            <th>Nombre del Gestionador</th>
                            <th>Bodega Origen</th>
                            <th>Bodega Destino</th>
                            <th>Estado</th>
                            <th>Fecha Registro</th>
                            <th>Detalle</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="encabezado in listaEncabezadosSolicitudes">
                            <td>{{encabezado.NombreSolicitante}}</td>
                            <td>{{encabezado.NombreProcesa}}</td>
                            <td>{{encabezado.NombreBodegaOrigen}}</td>
                            <td>{{encabezado.NombreBodegaDestino}}</td>
                            <td>{{encabezado.Estado}}</td>
                            <td>{{encabezado.FechaRegistro}}</td>
                            <td>
                                <span title="Observar Solicitud" style="cursor: pointer;" @click="obtenerDetalleSolicitudes(encabezado.IdSolicitudTrasladoBodega)">
                                    <i class="fa-solid fa-eye" style="font-size: 20px !important; color: #356191;"></i>
                                </span>

                                 <span title="Actualizar Solicitud" class="ml-2" style="cursor: pointer;" @click="obtenerArticulosBodegaEspecifica(encabezado.IdBodegaDestino, encabezado.IdSolicitudTrasladoBodega)">
                                    <i class="fa-solid fa-pencil text-warning" style="font-size: 20px !important;"></i>
                                </span>

                                 <span title="Eliminar Solicitud" class="ml-2" style="cursor: pointer;" @click="eliminarSolicitud(encabezado.IdSolicitudTrasladoBodega)">
                                    <i class="fa-solid fa-trash text-danger" style="font-size: 20px !important;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Modal de mostrar artículos de una bodega en especifico-->
        <div class="modal fade" id="ModalArticulosBodegaEspecifica" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelMens" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> Control de Bodegas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                         <table class="table mt-5 text-center col-12" id="tablaArticulosBodegaEspecifica">
                            <thead class="text-white text-center" style="background-color: #356191;">
                                <tr>
                                    <th>Acciones</th>
                                    <th>Nombre Artículo</th>
                                    <th>ID Interno</th>
                                    <th>Bodega</th>
                                    <th>Cantidad</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="articuloBodega in listaArticulosBodegaEspecifica">
                                    <td>
                                        <span title="Agregar a Solicitud" style="cursor: pointer;" @click="mostrarModalCantidadAct(articuloBodega)">
                                            <i class="fa-solid fa-circle-plus" style="font-size: 20px !important; color: #356191;"></i>
                                        </span>
                                    </td>
                                    <td>{{articuloBodega.Articulo}}</td>
                                    <td>{{articuloBodega.IdInterno}}</td>
                                    <td>{{articuloBodega.Bodega}}</td>
                                    <td>{{articuloBodega.Cantidad}}</td>
                                </tr>
                            </tbody>
                        </table>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <!--Modal de solicitud de ver detalle-->
        <div class="modal fade" id="ModalDetalleSolicitud" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelMensaje2" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> Control de Bodegas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <table class="table mt-2 text-center col-12" id="tablaSolicitudesDetalle">
                            <thead class="text-white text-center" style="background-color: #356191;">
                                <tr>
                                    <th>Nombre del Artículo</th>
                                    <th>Cantidad Solicitada</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="detalle in listaDetalleSolicitudes">
                                    <td>{{detalle.NombreArticulo}}</td>
                                    <td>{{detalle.Cantidad}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <!--Modal de solicitud de cantidad-->
        <div class="modal fade" id="ModalCantidadSolicitar" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelMensaje1" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> Control de Bodegas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="form-group col-8 mx-auto" >
                            <label>Ingrese la cantidad a solicitar:</label>
                            <input type="tel" name="txt_cantidadTB" v-model="txt_cantidadTB" id="txt_cantidadTB"  maxlength="3" class="form-control"
                             @keypress="validaNumeros(event)"/>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" @click="crearSolicitudTrasladoBodega()">Solicitar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


           <!--Modal de solicitud de cantidad a actualizar-->
        <div class="modal fade" id="ModalCantidadActualizar" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="stticBackdropLabelMensaje1" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> Control de Bodegas
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="form-group col-8 mx-auto" >
                            <label>Ingrese la cantidad a solicitar:</label>
                            <input type="tel" name="txt_cantidadAct" v-model="txt_cantidadAct" id="txt_cantidadAct"  maxlength="3" class="form-control"
                             @keypress="validaNumeros(event)"/>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" @click="actualizarSolicitudTrasladoBodega()">Ingresar a Solicitud</button>
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
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    <script src="../../../Vue/vue.min.js"></script>
    <script src="solicitudTraslado.js"></script>

</asp:Content>