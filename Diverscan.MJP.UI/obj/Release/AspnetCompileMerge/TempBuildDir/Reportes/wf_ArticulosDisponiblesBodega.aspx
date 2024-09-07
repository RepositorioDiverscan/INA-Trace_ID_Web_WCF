<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ArticulosDisponiblesBodega.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.ArticulosDisponibles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="articulos" style="margin-left: 20%">     

        <!--Titulo de Alertas de Vencimiento-->
        <h3 style="font-size: 25px !important;">Articulos Disponibles Bodegas</h3>
                <label>Seleccione una Bodega</label>
                 <select  v-model="ddlBodegas" class="form-control-sm" >
                    <option value="0" selected="selected" disabled="disabled">Seleccione una Bodega</option>
                     <option v-for="bodega in bodegas" :key="bodega.IdBodega" :value="bodega.IdBodega">
                        {{bodega.Nombre}}
                    </option>
                </select>
        <div style="margin:6px">
        </div>
            <div class="form-group col-8" style="padding: 0px">
                <button @click="buscarListaArticulos()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>
                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        
        <!--Tabla de Alertas-->
         <div class="table-responsive mt-5 mb-5" id="DivTablalistaArticulos" >
           <table class="table text-center" id="table_listaArticulos">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Id TID</th>
                        <th>Id ERP</th>
                        <th>Articulo</th>
                        <th>Unidades Inventario</th>
                        <th>Lote</th>
                        <th>Fecha</th>
                        <th>Ubicación</th>
                        <th>Unidades Medida</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(articulo, index) in listaArticulos" :key="index">
                        
                        <td>{{articulo.IdTID}}</td>
                        <td>{{articulo.IdERP}}</td>
                        <td>{{articulo.Articulo}}</td>
                        <td>{{articulo.UnidadesInventario}}</td>
                        <td>{{articulo.Lote}}</td>
                        <td>{{articulo.FechaVencimiento}}</td>
                        <td>{{articulo.Ubicacion}}</td>
                        <td>{{articulo.UnidadMedida}}</td>
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
   <script type="text/javascript" src="../Vue/vue.min.js"></script>

    <script type="text/javascript" src="ArticulosDisponibles.js"></script>

    

    
</asp:Content>