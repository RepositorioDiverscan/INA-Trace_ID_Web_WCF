<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ConsultarMaestroArticulo.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Articulos.wf_ConsultarMaestroArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--div principal del vuejs-->
    <div id="maestro" style="margin-left: 20%">

        <h3 style="font-size: 25px !important;">Maestro Articulo</h3>
          <p>
            </p>

        <!--Select para filtrar los articulos por bodega-->
        <div class="form-group col-2 d-flex flex-column" style="padding: 0px">

        </div>

        <!--Tabla de Alertas-->
        <div class="table-responsive mt-5 mb-5" id="DivtablaMaestroArticulo" >
           <button @click="generarExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important;"> 
                   <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
              </button>
           <table class="table text-center" id="table_listaMaestro" >
                <thead style="background-color: #2E5C8E" class="text-white">
                    <tr>
                        <th>IdArticulo</th>
                        <th>SKU</th>
                        <th>Nombre</th>
                        <th>GTIN</th>
                        <th>Familia Articulo</th>
                        <th>Contenido</th>
                        <th>Unidad Medida</th>
                        <th>Tipo Activo</th>
                        <th>Fecha Registro </th>
                       
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(a, index) in listaMaestro" :key="index">
                        
                        <td>{{a.IdArticulo}}</td>
                        <td>{{a.IdInterno}}</td>
                        <td>{{a.Nombre}}</td>
                        <td>{{a.Gtin}}</td>
                        <td>{{a.NombreFamilia}}</td>
                        <td>{{a.Contenido}}</td>
                        <td>{{a.Unidad_Medida}}</td>
                        <td>{{a.Activo}}</td>
                        <td>{{a.FechaRegistro}}</td>                      
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

   
    
    <script type="text/javascript" src="../../Scripts/ScriptsSite/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/ScriptsSite/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=5f3d8e561653486db3102230"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="https://cdn.sheetjs.com/xlsx-latest/package/dist/xlsx.full.min.js"></script>

    <script type="text/javascript" src="../../Vue/vue.min.js"></script>
    <script type="text/javascript" src="MaestroArticulo.js"></script>
    

    
</asp:Content>