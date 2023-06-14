<%@ Page Title="WMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="depuracionPedidoCurso.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Pedidos.wf_DepuraPedidos"%>


<%--@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" --%>
<%--@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" --%>

  
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="DepuraPedido" runat="server" ContentPlaceHolderID="MainContent">
    <div id="GestionPedido" style="width:80%; margin-left:250px; padding:10px">

        <h1>Depuracion de Pedidos Cursos</h1>

        <!--Tabla de Encabezados Pedidos Originales-->
        <div class="table-responsive mt-5 mb-5">
            <table class="table mb-4 text-center" id="tableEncabezados">   
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>ID Pedido</th>
                        <th>Solicitud</th>
                        <th>Estado</th>
                        <th>Entrega</th>
                        <th>Acción</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pedido in listaEncabezados">
                        <td>{{pedido.Pedido}}</td>
                        <td>{{pedido.Solicitud}}</td>
                        <td>{{pedido.Estado}}</td>
                        <td>{{pedido.Entrega}}</td>
                        <td>
                            <span title="Ver detalle" @click="CargarDetalles(pedido.Pedido)">
                                <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


        <!--Tabla de Detalle de Pedidos-->
        <div class="table-responsive mt-5 mb-5" id="TablaPedidosDetalle" >
            <div id="tablaDetallePedidos">

                <h1>Detalle del Pedido</h1>

                <table  class="table mt-4 text-center col-12" id="tablaDetalle">
                    <thead style="background-color: #2E5C8E" class="text-white">
                        <tr>
                            <th>ID Interno</th>
                            <th>Nombre Artículo</th>
                            <th>Cantidad Solicitada</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="detalle in listaDetalle">
                            <td>{{detalle.IdInternoArticulo}}</td>
                            <td>{{detalle.NombreArticulo}}</td>
                            <td>{{detalle.Cantidad}}</td>
                            <td>
                                <span title="Modificar Cantidad" @click="mostrarModalModificarDetalle(detalle)">
                                    <i class="fa-solid fa-pen-to-square text-secondary" style="font-size: 20px !important; cursor: pointer;"></i>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!--Modal para mostrar la cantidad que se desea ingresar en Solicitud de Alisto -->
        <div class="modal fade" id="ModalCantidadModificar" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabelCantidadAlisto">Modificar Cantidad</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div>
                            <div class="form-group col-6 mx-auto text-center">
                                <label for="">Ingrese la cantidad a modificar: </label>
                                <input type="text" name="txt_cantidadModificar" id="txt_cantidadModificar" minlength="1" maxlength="3"
                                    v-model="txt_cantidadModificar" class="form-control" @keypress="ValidarSoloNumeros(event)"/>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" @click="ModificarCantidad('Sumar')">Sumar Cantidad</button>
                        <button type="button" class="btn btn-danger" @click="ModificarCantidad('Restar')">Restar Cantidad</button>
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


    <!--Scripts de Vue y Ajax-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    <script src="../../../Vue/vue.min.js"></script>
    <script src="DepurarPedidoCurso.js"></script>

 </asp:Content>
