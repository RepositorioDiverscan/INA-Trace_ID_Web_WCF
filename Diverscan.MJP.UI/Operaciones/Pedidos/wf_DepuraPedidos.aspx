<%@ Page Title="WMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_DepuraPedidos.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Pedidos.wf_DepuraPedidos"%>


<%--@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" --%>
<%--@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" --%>

  
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="DepuraPedido" runat="server" ContentPlaceHolderID="MainContent">
    <div id="GestionPedido" style="width:80%; margin-left:250px; padding:10px">

        <h1>Depuracion de pedidos</h1>

        <!--Tabla de Encabezados Pedidos Originales-->
        <div class="table-responsive mt-5 mb-5">
            <table class="table mb-4 text-center">   
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Solicitud</th>
                        <th>Nombre Destino</th>
                        <th>Fecha Creación</th>
                        <th>Fecha Entrega</th>
                        <th>Acción</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pedido in pedidosEncabezados">
                        <td>{{pedido.IdInterno}}</td>
                        <td>{{pedido.NombreDestino}}</td>
                        <td>{{pedido.FechaCreacion}}</td>
                        <td>{{pedido.FechaEntrega}}</td>
                        <td>
                            <span title="Ver detalle" @click="CargarDetalles(pedido.IdPedidoOriginal)">
                                <i class="fa-solid fa-eye text-info" style="font-size: 20px !important; cursor: pointer;"></i>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


        <!--Tabla de Detalle de Pedidos-->
        <div class="table-responsive mt-5 mb-5">
            <div id="tablaDetallePedidos">

                <h1>Detalle del Pedido</h1>

                <table  class="table mt-4 text-center col-12">
                    <thead style="background-color: #2E5C8E" class="text-white">
                        <tr>
                            <th>ID Interno</th>
                            <th>Nombre</th>
                            <th>Cantidad Solicitada</th>
                            <th style="width: 15%;">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="pedido in pedidosDetalle">
                            <td>{{pedido.IdArticuloInterno}}</td>
                            <td>{{pedido.Nombre}}</td>
                            <td>{{pedido.CantidadSolicitada}}</td>
                            <td>
                                <span title="Modificar Cantidad" @click="MostrarModalModificarCantidad(pedido)">
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
                        <form>
                            <div class="form-group col-6 mx-auto text-center">
                                <label for="">Ingrese la cantidad a modificar: </label>
                                <input type="tel" name="txt_cantidadModificar" id="txt_cantidadModificar" minlength="1" maxlength="3"
                                    v-model="txt_cantidadModificar" class="form-control" @keypress="ValidarSoloNumeros(event)"/>
                            </div>
                        </form>
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
    

        <%--<script type='text/javascript'>
              function mensajeError(texto) {
                  Swal.fire(
                      {
                          position: 'top-right',
                          icon: 'error',
                          title: '!! Error !!',
                          text: texto,
                          showConfirmButton: true,
                          closeOnConfirm: true
                      });
              }

              function mensajeExito(texto) {
                  Swal.fire(
                      {
                          position: 'top-right',
                          icon: 'success',
                          title: '!! Éxito !!',
                          text: texto,
                          showConfirmButton: true,
                          closeOnConfirm: true
                      });
              }

              function mensajeInfo(texto) {
                  Swal.fire(
                      {
                          position: 'top-right',
                          icon: 'question',
                          title: '!! Info !!',
                          text: texto,
                          showConfirmButton: true,
                          closeOnConfirm: true
                      });
              }
          </script>

         <script src="~/Scripts/js/jquery.dataTables.min.js" type="text/javascript"></script>
         <script src="~/Scripts/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>
         <script src="~/Scripts/js/datatables-pedidos.js" type="text/javascript"></script>
         <script src="~/Scripts/js/pedidos.js" type="text/javascript"></script>
         <script src="~/Scripts/js/script.js" type="text/javascript"></script>
         <script src="https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.8.1/baguetteBox.min.js"></script>
         <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
         <script src="sweetalert2.min.js" type="text/javascript"></script>

   

       <div style="width:80%; margin-left:250px; padding:10px;">     <%-- font-size:20px;  text-align:center; 
         <h1>Depuración de Pedidos</h1>
   
         <div class="table-responsive" id="GridEncabezado">
             <asp:Button id = "BtnLimpia" runat="server" Text="Limpiar" OnClick="btnLimpia_onClick"/>
             <br/>
             <br/>
             <asp:gridview id="PreSolicitudGridView" autogeneratecolumns="False" emptydatatext="No data available." allowpaging="True" 
                           runat="server"  Font-Size="Larger" GridLines="Vertical" OnSelectedIndexChanged="Grid1_SelectedIndexChanged"
                           CssClass="table table-bordered"
                           style = "border-radius: 10px; background: rgba(93, 151, 230, 0.79); color: black; height: 100px; font-size: 500px; width: 100%; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; text-align: center;">
                <Columns>
                    <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" InsertVisible="False" ReadOnly="True" SortExpression="Solicitud" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
                    <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
               <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />  
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />  
                    <asp:ButtonField buttontype="Button" commandname="Select" headertext="Acciones" text="Ver Detalle"/>
                </Columns>
             </asp:gridview>
         </div>
         <br />

         <div class="table-responsive" id="GridDetalle">
             <asp:gridview id="PreSolicitudDetalleGridView" autogeneratecolumns="False" emptydatatext="No data available." 
                    allowpaging="True" runat="server" style = "border-radius: 10px; border: 1px solid grey; text-align: center;
                    background: rgba(93, 151, 230, 0.79); color: black; height: 50px; font-size: 50px; width: 100%"  GridLines="Vertical"
                     OnSelectedIndexChanged="Grid2_SelectedIndexChanged" On DataKeyNames="CantidadOriginal, idPreLineaDetalleSolicitud">
                <Columns>
                    <asp:BoundField DataField="Línea" HeaderText="Línea" />
                    <asp:BoundField DataField="idArticulo" HeaderText="idArticulo"/>
                    <asp:BoundField DataField="Artículo" HeaderText="Artículo" />
                <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" /> 
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"  />
                    <asp:ButtonField buttontype="Button" commandname="Select" headertext="Acciones" text="Modifica Cantidad"/>
          
                   <asp:TemplateField> 
                        <ItemTemplate> 
                            <asp:ImageButton ID="DeleteButton" runat="server" OnClientClick="return MuestraVentana();"/>  OnClick="Grid2_SelectedIndexChanged"
                        </ItemTemplate> 
                    </asp:TemplateField>  
                </Columns>
             </asp:gridview>
         </div>
         <br />
         <div class="card shadow mb-5" style="margin-left:300px;">
           <div class="card shadow mb-auto">
             <div class="card-body">
               <div id="Modifica" style="text-align: center; width: 100%">
                  <asp:Table CssClass="table table-responsive" id="TblCantidad" width="50%" cellspacing="0" style = "background-color: lightcyan;"
                           visible ="false" runat="server">
                     <asp:TableRow>
                       <asp:TableCell CssClass="text-center">
                           <label style="margin-right:100px; font: bolder;">Cantidad a Modificar</label>
                       </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableCell CssClass="text-center">
                            <asp:TextBox ID="txtCantidadActualiza"  CssClass="TexboxNormal" runat="server" style="text-align: center;"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button type="button" id="btnCantidad" class="btn btn-primary" runat="server" Text="Modificar" OnClick="btnCambiar_onClick"></asp:Button>
                            <asp:TextBox ID="TxtIdMaestro"  visible="false" runat="server"></asp:TextBox>
                        </asp:TableCell>
                     </asp:TableRow>
                   </asp:table>
                   
                   <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                      <textbox
                   </div>
               
               </div>
             </div>
           </div>
         </div>
       </div>--%>

    </div>
    <!--Scripts de Vue y Ajax-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script src="../../Vue/vue.min.js"></script>
    <script src="DepurarPedido.js"></script>

 </asp:Content>
