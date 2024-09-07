<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ConsultarSolicitud.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Salidas.wf_ConsultarSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
     <asp:Panel ID="Panel4" runat="server" >   
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                           
                        </AjaxSettings>
                    </telerik:RadAjaxManager>

                    <div id="RestrictionZoneID" class="WindowContenedor">
             
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows >
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Solicitud" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OC">
                                                <h1 class="TituloPanelTitulo">Datos de la Solicitud</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                           <%-- BUSCAR --%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>                                                            
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label39" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />   

                                                                <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click"/>

                                                                <asp:Button runat="server" ID="btnExportar" Text="Exportar" AutoPostBack="false" OnClick="btnExportar_Click" Enabled="false"/>
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                    </div>

                                                    <%-- MAESTRO SOLICITUD --%>
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas0">
                                                <h1 class="TituloPanelTitulo">Listado Solicitud</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridSolicitud" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridSolicitud_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" AllowFilteringByColumn="True" OnItemCommand="RadGridSolicitud_ItemCommand">
                                                <GroupingSettings CaseSensitive="false" />
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idMaestroSolicitud"
                                                                    SortExpression="idMaestroSolicitud" HeaderText="Id SOL" DataField="idMaestroSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                              <%--  <telerik:GridBoundColumn UniqueName="idInterno"
                                                                    SortExpression="idInterno" HeaderText="Id Interno" DataField="idInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>--%>

                                                              <%--  <telerik:GridBoundColumn UniqueName="idInternoSAP"
                                                                    SortExpression="idInternoSAP" HeaderText="Id Interno SAP" DataField="idInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="idCompania"
                                                                    SortExpression="idCompania" HeaderText="Id Compania" DataField="idCompania"
                                                                     AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>--%>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idDestino" Display="false"
                                                                    SortExpression="idDestino" HeaderText="Id Destino" DataField="idDestino"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreDestino"
                                                                    SortExpression="NombreDestino" HeaderText="Nombre Destino" DataField="NombreDestino"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion" HeaderText="Fecha Registro" DataField="FechaCreacion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idUsuario" Display="false"
                                                                    SortExpression="idUsuario" HeaderText="Id Usuario" DataField="idUsuario"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreUsuario"
                                                                    SortExpression="NombreUsuario" HeaderText="Usuario" DataField="NombreUsuario"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Comentarios"
                                                                    SortExpression="Comentarios" HeaderText="Comentario" DataField="Comentarios"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <br>

                                                    <%-- DETALLE SOLICITUD --%>
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                    <h1 class="TituloPanelTitulo">Listado Detalle Solicitud</h1> 
                                                    </asp:Panel>
                                            <telerik:RadGrid ID="RadGridDetalleSolicitud" 
                                                AllowPaging="True" 
                                                Width="100%"
                                                OnClientClick="DisplayLoadingImage1()" 
                                                Visible="false"  
                                                OnNeedDataSource ="RadGridDetalleSolicitud_NeedDataSource"
                                                runat="server" 
                                                AutoGenerateColumns="False" 
                                                AllowSorting="True" 
                                                PageSize="10" 
                                                AllowMultiRowSelection="true"

                                                AllowFilteringByColumn="True"

                                                OnItemCommand="RadGridDetalleSolicitud_ItemCommand">
                                                <GroupingSettings CaseSensitive="false" />
                                                         <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idPreLineaDetalleSolicitud" Display="false"
                                                                    SortExpression="idPreLineaDetalleSolicitud" HeaderText="Id DS" DataField="idPreLineaDetalleSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMaestroSolicitud" Display="false"
                                                                    SortExpression="idMaestroSolicitud" HeaderText="Id SOL" DataField="idMaestroSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre" Display="false"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <%--<telerik:GridBoundColumn UniqueName="idArticulo"
                                                                    SortExpression="idArticulo" HeaderText="Id Articulo" DataField="idArticulo">
                                                                </telerik:GridBoundColumn>--%>

                                                                <telerik:GridBoundColumn UniqueName="idInterno"
                                                                    SortExpression="idInterno" HeaderText="Id Interno Articulo" DataField="idInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadDespachada"
                                                                    SortExpression="CantidadDespachada" HeaderText="Cantidad Despachada" DataField="CantidadDespachada"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadPendiente"
                                                                    SortExpression="CantidadPendiente" HeaderText="Cantidad Pendiente" DataField="CantidadPendiente"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                    SortExpression="Unidad_Medida" HeaderText="Unidad Medida" DataField="Unidad_Medida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                                 <Triggers>     
                                                </Triggers>
                                            </asp:UpdatePanel>                         
                                            
                                            </asp:Panel>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </ContentTemplate>
                            <Shortcuts>
                                <telerik:WindowShortcut CommandName="Maximize" Shortcut="Ctrl+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Minimize" Shortcut="Ctrl+F7"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                            </Shortcuts>
                           
                        </telerik:RadWindow>
        
                    </Windows>
                </telerik:RadWindowManager> 

               </div>                      

   </asp:Panel>
</asp:Content>