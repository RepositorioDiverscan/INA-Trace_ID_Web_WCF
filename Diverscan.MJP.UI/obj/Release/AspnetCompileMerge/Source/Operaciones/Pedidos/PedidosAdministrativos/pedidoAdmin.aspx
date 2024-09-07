<%@ Page Language="C#" MasterPageFile="~/Site.Master"   AutoEventWireup="true" CodeBehind="pedidoAdmin.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Pedidos.PedidoCursos.pedidoCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pedidoAdmin" style="width:80%; margin-left:250px; padding:10px">

        <!--Tabla de Encabezados de Pedidos Administrativo disponibles en Bodega-->
        <div class="table-responsive mt-2 mb-5">
            <div>
                <h1>Pedidos Administrativo</h1>

                <table class="table mt-5 text-center col-12" id="tablaPedidosAdmin">
                    <thead class="text-white text-center" style="background-color: #356191;">
                        <tr>
                            <th>Solicitud</th>
                            <th>Bodega</th>
                            <th>Estado</th>
                            <th>Creación</th>
                            <th>Entrega</th>
                            <th>Usuario Solicitante</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="encabezado in listaEncabezados">
                            <td>{{encabezado.Solicitud}}</td>
                            <td>{{encabezado.Bodega}}</td>
                            <td>{{encabezado.Estado}}</td>
                            <td>{{encabezado.Creacion}}</td>
                            <td>{{encabezado.Entrega}}</td>
                            <td>{{encabezado.NombreUsuario}}</td>
                            <td>
                                <span title="Ver detalle" @click="ObtenerDetallePedidos(encabezado.Pedido)">
                                    <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>  
                                
                                 <span title="Aprobar Solicitud" class="ml-2 mr-2" @click="aprobarPedido(encabezado.Pedido)">
                                    <i class="fa-solid fa-dolly text-success" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>  

                                <span title="Anular Pedido"  style="cursor: pointer;" @click="anularPedido(encabezado.Pedido)">
                                    <i class="fa-solid fa-ban text-danger" style="font-size: 20px !important;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Tabla de Detalle de Pedidos Administrativo disponibles en Bodega-->
        <div class="table-responsive mt-2 mb-5" id="DivTablaDetalle">
            <div>
                <h1>Detalle del Pedido Administrativo</h1>

                  <table class="table mt-5 text-center col-12" id="tablaPedidosDetalle">
                    <thead class="text-white text-center" style="background-color: #356191;">
                        <tr>
                            <th>ID Interno Artículo</th>
                            <th>Nombre Artículo</th>
                            <th>Cantidad Solicitada</th>
                            <th>Cantidad en Bodega</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="detalle in listaDetalles">
                            <td>{{detalle.IdInternoArticulo}}</td>
                            <td>{{detalle.NombreArticulo}}</td>
                            <td>{{detalle.Cantidad}}</td>
                            <td>{{detalle.CantidadDisponibleBodega}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    

         <!--Modal para mostrar mensajes-->
        <div class="modal fade" id="ModalMensaje" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelMensaje" style="font-size: 20px !important;">
                            <i class="fa-solid fa-circle-info" style="font-size: 20px !important; color: rgb(46, 92, 142);"></i> 
                            Pedidos Administrativos
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
    <script src="../../../Vue/html2pdf.bundle.min.js"></script>
    <script src="pedidoAdmin.js"></script>
</asp:Content>