<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="wf_Consultar_Inventario.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Administracion.wf_Consultar_Inventario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="inventario" style="width:80%; margin-left:250px; padding:10px">
             
        <h1>Inventario</h1>
        <div class="form-group col-2" style="padding: 0px">
                <label>Fecha de Inicio:</label>
                <input type="date" class="form-control" id="dtF1" v-model="dtF1" :max="fechaHoy" />
                <label>Fecha de Fin:</label>
                <input type="date" class="form-control" id="dtF2" v-model="dtF2" :max="fechaHoy"/>
             </div>
        
            <div class="form-group col-8" style="padding: 0px">
                <button @click=" ObtenerEncabezadoInventario()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>

                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        <!--Tabla de EncabezadosInventario-->
        <div class="table-responsive mt-5 mb-5" id="DivlistaEncabezadoInventario" >
            <table class="table mb-2 text-left" id="table_listaEncabezadoInventario">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Id Inventario</th>
                        <th>Familia</th>
                        <th>Tipo de Inventario</th>
                        <th>Fecha por Aplicar</th>
                        <th>Estado</th>
                        <th></th>
                        
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, key) in listaEncabezadoInventario" :key="key">
                        <td>{{a.IdInventarioBasico}}</td>
                        <td>{{a.Familia}}</td>
                        <td>{{a.TipoInventario}}</td>
                        <td>{{a.FechaPorAplicar}}</td>
                        <td>{{a.Estado}}</td>
                        
                        <td>
                            <span title="Ver Detalle" @click="  ObtenerDetalleInventario(a.IdInventarioBasico)">
                                <i class="fa-solid fa-eye text-primary" style="font-size: 20px !important; cursor: pointer;"></i>
                            </span>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>

         
        <!--Tabla de Detalle de Pedidos-->
        <div class="table-responsive mt-5 mb-5"  id="DivlistaDetalleInventario" >
                <h1>Detalle de la Orden de Compra</h1>
                <button @click="generarExcel2() " type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel Detalle
                </button>
                <table  class="table mt-4 text-center col-12" id="table_listaDetalleInventario">
                    <thead style="background-color: #2E5C8E" class="text-white">
                        <tr>
                            <th>Id Articulo</th>
                            <th>Nombre Articulo</th>
                            <th>Cantidad</th>
                            <th>Descripcion</th>
                            <th>Fecha Registro</th>
                            
                           
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="a in  listaDetalleInventario">
                            <td>{{a.IdArticulo}}</td>
                            <td>{{a.NombreArticulo}}</td>
                            <td>{{a.Cantidad}}</td>
                            <td>{{a.Descripcion}}</td>
                            <td>{{a.FechaHoraRegistro}}</td>
                            
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
    
    <script type="text/javascript"src="Inventario.js"></script>
</asp:Content>