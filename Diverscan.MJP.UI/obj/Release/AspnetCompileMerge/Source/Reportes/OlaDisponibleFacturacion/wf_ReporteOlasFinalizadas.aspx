<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ReporteOlasFinalizadas.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.OlaDisponibleFacturacion.WebForm1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="ola" style="margin-left: 20%">     

        <!--Titulo de Alertas de Vencimiento-->
        <h3 style="font-size: 25px !important;">Olas Finalizadas</h3>
        <div class="form-group col-2" style="padding: 0px">
                <label>Fecha de Inicio:</label>
                <input type="date" class="form-control" id="dtF1" v-model="dtF1" :max="fechaHoy" />
                <label>Fecha de Fin:</label>
                <input type="date" class="form-control" id="dtF2" v-model="dtF2" :max="fechaHoy"/>
             </div>

        <div style="margin:6px">   
            
        </div>
            <div class="form-group col-8" style="padding: 0px">
                <button @click="buscarOla()" type="button" class="btn btn-secondary text-center mb-3" style="font-size: 15px !important;"> 
                    <i class="fa-solid fa-magnifying-glass mr-1" style="font-size: 15px !important;"></i> Buscar
                </button>

                <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                      <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                </button>
             </div>
        
        <!--Tabla de Alertas-->
         <div class="table-responsive mt-5 mb-5" id="DivTablaOla" >
           <table class="table text-center" id="table_listaOla">
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>                       
                        <th class="text-center">Fecha</th>
                        <th class="text-center">Id Ola</th>
                        <th class="text-center">Días Finalizados</th>
                        <th class="text-center">Avance</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, index) in listaOla" :key="index">
                        
                        <td>{{FormatearFecha(a.Fecha)}}</td>
                        <td>{{a.IdOla}}</td>
                        <td>{{a.DiasFinalizados}}</td>
                        <td>{{a.Avance }}</td>
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

    <script type="text/javascript" src="../../Vue/vue.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="https://cdn.sheetjs.com/xlsx-latest/package/dist/xlsx.full.min.js"></script>
    <script  type="text/javascript" src="Ola.js"></script>

</asp:Content>