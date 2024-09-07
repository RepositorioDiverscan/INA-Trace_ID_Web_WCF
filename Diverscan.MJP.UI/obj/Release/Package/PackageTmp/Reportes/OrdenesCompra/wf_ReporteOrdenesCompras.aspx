<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ReporteOrdenesCompras.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.OrdenesCompra.wf_ReporteOrdenesCompras" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="orden" style="margin-left: 20%; padding:5px">
             
        <h1>Ordenes de Compra</h1>
        <div class="form-group col-2" style="padding: 0px">
            <label>Seleccione una Bodega</label>
                 <select  v-model="ddlBodegas" class="form-control-sm" >
                    <option value="0" selected="selected" disabled="disabled">Seleccione una Bodega</option>
                     <option v-for="bodega in bodegas" :key="bodega.IdBodega" :value="bodega.IdBodega">
                        {{bodega.Nombre}}
                    </option>
                </select>
            
                <label>Numero de Orden:</label>
                <input type="text" class="form-control" id="numero" v-model="numero" />
                <label>Fecha de Inicio:</label>
                <input type="date" class="form-control" id="dtF1" v-model="dtF1" :max="fechaHoy" />
                <label>Fecha de Fin:</label>
                <input type="date" class="form-control" id="dtF2" v-model="dtF2" :max="fechaHoy"/>
             </div>
        
            <div class="form-group col-8" style="padding: 0px">
                <button @click="ObtenerOrdenCompra()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>

                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        <!--Tabla de Encabezados Pedidos Originales-->
        <div class="table-responsive mt-5 mb-5" id="DivpedidoOrdenCompra" >
            <table class="table mb-4 text-center" id="table_pedidoOrdenCompra">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Numero de Orden</th>
                        <th>Numero Transaccion</th>
                        <th>Proveedor</th>
                        <th>Fecha Creacion</th>
                        <th>Fecha Procesameinto</th>
                        <th>Usuario</th>
                        <th>Numero Factura</th>
                        <th> % Recepcion</th>
                        <th>Procesada</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, key) in pedidoOrdenCompra" :key="key">
                        <td>{{a.IdMaestroOrdenCompra}}</td>
                        <td>{{a.NumTransaccion}}</td>                                            
                        <td>{{a.NombreProveedor}}</td>
                        <td>{{a.FechaCreacion}}</td>
                        <td>{{a.FechaProcesamiento}}</td>
                        <td>{{a.Usuario}}</td>                     
                        <td>{{a.NumeroFactura}}</td>                       
                        <td>{{a.PorcentajeRecepcion}}</td>
                        <td>{{a.Procesada}}</td>
                        <td>
                            <span title="Ver Detalle" @click="ObtenerDetalleOrdenCompra(a.IdMaestroOrdenCompra)">
                                <i class="fa-solid fa-eye text-primary" style="font-size: 20px !important; cursor: pointer;"></i>
                            </span>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>

         
        <!--Tabla de Detalle de Pedidos-->
        <div class="table-responsive mt-5 mb-5"  id="DivDetallePedidos" >
                <h1>Detalle de la Orden de Compra</h1>
                <button @click="generarExcel2() " type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel Detalle
                </button>
                <table  class="table mt-4 text-center col-12" id="table_pedidoDetalleOrdenCompra">
                    <thead style="background-color: #2E5C8E" class="text-white">
                        <tr>
                            <th>Sku</th>
                            <th>GTIN</th>
                            <th>Nombre</th>
                            <th>Cantidad por Recibir</th>
                            <th>Cantidad Recibidos</th>
                            <th>Cantidad Transito</th>
                            <th>Cantidad Bodega</th>
                           
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="a in pedidoDetalleOrdenCompra">
                            <td>{{a.IdInterno}}</td>
                            <td>{{a.Gtin}}</td>
                            <td>{{a.Nombre}}</td>
                            <td>{{a.CantidadxRecibir}}</td>
                            <td>{{a.CantidadRecibidos}}</td>
                            <td>{{a.CantidadTransito}}</td>
                           <td>{{a.CantidadBodega}}</td>
                        </tr>
                    </tbody>
                </table>
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

    


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="https://cdn.sheetjs.com/xlsx-latest/package/dist/xlsx.full.min.js"></script>
   <script type="text/javascript" src="../../Vue/vue.min.js"></script>
    <script type="text/javascript" src="../Ingresos/OrdenCompra.js"></script>
   
</asp:Content>