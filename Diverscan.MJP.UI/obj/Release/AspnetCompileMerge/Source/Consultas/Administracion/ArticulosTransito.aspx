<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticulosTransito.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Administracion.ArticulosTransito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="ktransito" style="margin-left: 20%">     

        <!--Titulo de Asignacion de Certificacion-->
        <h3 style="font-size: 25px !important;">Articulo Transito Bodega </h3>
        <div class="form-group col-2" style="padding: 0px">
            <label>Seleccione una Bodega</label>
                 <select  v-model="ddlBodegas" class="form-control-sm" >
                    <option value="0" selected="selected" disabled="disabled">Seleccione una Bodega</option>
                     <option v-for="bodega in bodegas" :key="bodega.IdBodega" :value="bodega.IdBodega">
                        {{bodega.Nombre}}
                    </option>
                </select>
             </div>
            <div class="form-group col-8" style="padding: 0px">
                <button @click="buscarArticuloTransito()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>
                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        
        <!--Tabla de Alertas-->
         <div class="table-responsive mt-5 mb-5" id="DivTablalistaArticulo" >
           <table class="table text-center" id="table_listaArticulo" >
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Nombre Usuario</th>
                        <th>Apellidos</th>
                        <th>Ubicacion</th>
                        <th>Nombre Articulo</th>
                        <th>SKU</th>
                        <th>Cantidades en Transito</th>
                        <th>Lote</th>
                        <th>Fecha Vencimiento</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, index) in listaArticulo" :key="index">
                        <td>{{a.NombreUsuario}}</td>
                        <td>{{a.ApellidosUsuario}}</td>
                        <td>{{a.UbicacionArticulo}}</td>
                        <td>{{a.NombreArticulo}}</td>
                        <td>{{a.IdInterno}}</td>
                        <td>{{a.CantidadTransito}}</td>
                        <td>{{a.Lote}}</td>
                        <td>{{a.FechaVencimiento}}</td>
                    </tr>
                </tbody>
            </table>
        </div>


         <!--Modal para mostrar mensajes en la pantalla <!--Fin del div del Vuejs-->
        <div class="modal fade" id="MensajeModal" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                 <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabel">Espacio requerido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
              <div class="modal-body">
                {{mensaje}}
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
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
   
    <script type="text/javascript"src="ArticulosTransito.js"></script>
    
   

    
</asp:Content>