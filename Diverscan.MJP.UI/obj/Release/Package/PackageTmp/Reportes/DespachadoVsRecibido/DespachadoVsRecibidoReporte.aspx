<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DespachadoVsRecibidoReporte.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.DespachadoVsRecibido.DespachadoVsRecibidoReporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="despachadoVsRecibidoReporte" style="margin-left: 20%">     

        <!--Titulo del Reporte-->
        <h3 style="font-size: 25px !important;">Reportes Despachado Vs Recibido</h3>

        <div >   
             <div class="form-group col-2" style="padding: 0px">
                <label>Fecha de Inicio:</label>
                <input type="date" class="form-control" id="dtFechaInicio" v-model="dtFechaInicio" :max="fechaHoy" />
             </div>

             <div class="form-group col-2" style="padding: 0px">
                <label>Fecha de Fin:</label>
                <input type="date" class="form-control" id="dtFechaFin" v-model="dtFechaFin" :max="fechaHoy"/>
             </div>

            <label>Seleccione una Bodega</label>
            <select  v-model="ddlBodegas" class="form-control-sm m-3">
                <option value="0" selected="selected" disabled="disabled">Seleccione una Bodega</option>
                <option v-for="bodega in listaBodegas" :key="bodega.IdBodega" :value="bodega.IdBodega">
                    {{bodega.Nombre}}
                </option>
            </select>

            <div class="form-group col-8 m-3 p-0">
                <button @click="buscarReporte()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>
             
                <button @click="generarExcel()" type="button" class="btn btn-success text-center mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        </div>


        <!--Tabla de Reportes-->
         <div class="table-responsive mt-5 mb-5" id="DivTablaReportes">

            <table class="table text-center" id="table_reportes">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>Id Pedido</th>
                        <th>Fecha de entrega</th>
                        <th>Cantidad Despachada</th>
                        <th>Cantidad Recibida</th>
                        <th>Id Interno</th>
                        <th>Nombre Artículo</th>
                        <th>Profesor que Solicita</th>
                    </tr>
                </thead>

                <tbody>
                    <tr v-for="(reporte, index) in listaReporte" :key="index">
                        <td>{{reporte.IdPedidoOriginal}}</td>
                        <td>{{reporte.FechaEntrega}}</td>
                        <td>{{reporte.CantidadDespachada}}</td>
                        <td>{{reporte.CantidadRecibida}}</td>
                        <td>{{reporte.IdInterno}}</td>
                        <td>{{reporte.NombreArticulo}}</td>
                        <td>{{reporte.ProfesorSolicita}}</td>
                    </tr>
                </tbody>
            </table>
        </div>


         <!-- Modal para mostrar mensajes en la pantalla-->
        <div class="modal fade" id="MensajeModal" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                 <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabel">Control de Bodegas</h5>
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
    </div> <!--Fin del div del Vuejs-->


  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    
    <script src="https://cdn.sheetjs.com/xlsx-latest/package/dist/xlsx.full.min.js"></script>

    <script src="../../Vue/vue.min.js"></script>
    <script src="ReporteDespachadoVsRecibido.js"></script>
    
</asp:Content>
