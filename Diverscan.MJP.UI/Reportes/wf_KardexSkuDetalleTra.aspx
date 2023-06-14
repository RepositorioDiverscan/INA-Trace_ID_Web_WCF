<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_KardexSkuDetalleTra.aspx.cs" Inherits="Diverscan.MJP.UI.wf_KardexSkuDetalleTra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="kardex" style="margin-left:20%">     

        <!--Titulo de Asignacion de Certificacion-->
        <h3 style="font-size: 25px !important;">Kardex</h3>
        <div class="form-group col-2" style="padding: 0px">
            <label>Seleccione una Bodega</label>
                 <select  v-model="ddlBodegas" class="form-control-sm" >
                    <option value="0" selected="selected" disabled="disabled">Seleccione una Bodega</option>
                     <option v-for="bodega in bodegas" :key="bodega.IdBodega" :value="bodega.IdBodega">
                        {{bodega.Nombre}}
                    </option>
                </select>
                <label>SKU:</label>
                <input type="text" class="form-control" id="sku" v-model="sku" />
                 <label>Lote:</label>
                <input type="text" class="form-control" id="lote" v-model="lote" />
                <label>Transito:</label>
                <input type="checkbox" id="transito" name="checkbox" data-name="Checkbox" v-model="transito" />
            <br />
                <label>Fecha de Inicio:</label>
                <input type="date" class="form-control" id="dtF1" v-model="dtF1" :max="fechaHoy" />
                <label>Fecha de Fin:</label>
                <input type="date" class="form-control" id="dtF2" v-model="dtF2" :max="fechaHoy"/>
             </div>
        <div style="margin:6px">   
            
        </div>
            <div class="form-group col-8" style="padding: 0px">
                <button @click="buscarKardex()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>

                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        
        <!--Tabla de Encabezados Pedidos Originales-->
        <div class="table-responsive mt-5 mb-5" id="DivTablalistakardex" >
        <table class="table text-center" id="table_listakardex">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                         <th>Id Trazabilidad</th>
                        <th>Id Ubicacion</th>
                        <th>Ubicacion</th>
                        <th>Id Metodo Accion</th>
                        <th>Metodo Descripcion</th>
                        <th>Cantidad</th>
                        <th>Saldo</th>
                        <th>Fecha Registro</th>
                        <th>Lote</th>
                        <th>Fecha Vencimiento</th>
                        <th>Id Interno</th>
                        <th>Nombre</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, key) in listakardex" :key="key">
                         <td>{{a.IdTrazabilidad}}</td>
                        <td>{{a.IdUbicacion}}</td>
                        <td>{{a.Ubicacion}}</td>
                        <td>{{a.IdMetodoAccion}}</td>
                        <td>{{a.MetodoDescripcion}}</td>
                        <td>{{a.Cantidad}}</td>
                        <td>{{a.Saldo}}</td>
                        <td>{{a.FechaRegistro}}</td>
                        <td>{{a.Lote}}</td>
                        <td>{{a.FechaVencimiento}}</td>
                        <td>{{a.IdInterno}}</td>
                        <td>{{a.Nombre}}</td>

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
    <script type="text/javascript"src="kardex.js"></script>
    
   

    
</asp:Content>