<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_Reporte_CantidadPedidaTiendas.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_Reporte_CantidadPedidaTiendas" %>

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
                                    <telerik:RadTab Text="Historico Pedidos Tienda" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OC">
                                                <h1 class="TituloPanelTitulo">Busqueda Tiendas</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <%--INCIO DEL CONTENIDO --%>

                                                       <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                <%-- Visualización de elementos --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Busqueda de Destinos </h1>
                                                             <br />
                                                             <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador01" runat="server" Text="|||"></asp:Label>

                                                             <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador02" runat="server" Text="|||"></asp:Label>

                                                              <asp:Label ID="LblArticulo" runat="server" Text="Destinos."></asp:Label>
                                                              <asp:DropDownList ID="ddlidDestino" Class="TexboxNormal" runat="server" Width="150px" AutoPostBack="True"></asp:DropDownList>
                                                              <%--<asp:Label ID="LblSeparador03" runat="server" Text="|||"></asp:Label>--%>
                                                             <br />
                                                             <br />
                                    
                                                             <asp:Button runat="server" ID="btnBuscarPorFechas" Text="Generar"  AutoPostBack="true" OnClick="btnBuscarPorFechas_Click" ></asp:Button>
                                                            <%--OnClick="btnBuscarPorFechas_Click" --%>
                                                             <asp:Label ID="LblSeparador04" runat="server" Text="|||"></asp:Label>
                                                               
                                                             
                                                             <asp:Button runat="server" ID="BtnLimpiar" Text="Limpiar"  OnClick="BtnLimpiar_Click" />
                                                            <%--OnClick="BtnLimpiar_Click" --%>

                                                            <asp:Label ID="Label3" runat="server" Text="|||"></asp:Label>
                                                            <asp:Button runat="server" ID="BtnExportar" Text="Exportar Excel"  OnClick="btnExportar_Click"/>
                                                            <%--OnClick="btnExportar_Click" --%>
                                                         </td>
                                                    </tr>
                                            </table>

                                                     <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas0">
                                                <h1 class="TituloPanelTitulo">Destinos</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridDestinosPedido" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridDestinosPedido_ItemCommand">
                                                          <%--OnNeedDataSource ="RadGridPersonalAlisto_NeedDataSource"
                                                              OnItemCommand="RadGridPersonalAlisto_ItemCommand"--%>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Cantidad_Solicitudes"
                                                                    SortExpression="Cantidad_Solicitudes" HeaderText="Cantidades Totales de Pedidos" DataField="Cantidad_Solicitudes">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="NombreSolicitud"
                                                                    SortExpression="NombreSolicitud" HeaderText="Nombre del Pedido" DataField="NombreSolicitud" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreDestino"
                                                                    SortExpression="NombreDestino" HeaderText="Destino" DataField="NombreDestino">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idDestino"
                                                                    SortExpression="idDestino" HeaderText="Identificador Destino" DataField="idDestino">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion" HeaderText="Fecha de Creacion Solicitud" DataField="FechaCreacion">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaProcesamiento"
                                                                    SortExpression="FechaProcesamiento" HeaderText="Fecha de Procesamiento de Solicitud" DataField="FechaProcesamiento">
                                                                </telerik:GridBoundColumn>

                                                               
                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>


                                                      <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                <h1 class="TituloPanelTitulo">Detalle Pedido Tienda</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridDetallePedidoTienda" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
                                                          <%--OnNeedDataSource ="RadGridPersonalAlisto_NeedDataSource"
                                                              OnItemCommand="RadGridPersonalAlisto_ItemCommand"--%>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idMaestroSolicitud"
                                                                    SortExpression="idMaestroSolicitud" HeaderText="Numero de Solicitud" DataField="idMaestroSolicitud">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Destino"
                                                                    SortExpression="Destino" HeaderText="Destino" DataField="Destino">
                                                                </telerik:GridBoundColumn>

                                                               
                                                                 <telerik:GridBoundColumn UniqueName="Referencia"
                                                                    SortExpression="Referencia" HeaderText="Referencia Bexim" DataField="Referencia">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre Articulo" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad Pedida" DataField="Cantidad">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion" HeaderText="Fecha Creacion Pedido" DataField="FechaCreacion">
                                                                </telerik:GridBoundColumn>


                                                                 <telerik:GridBoundColumn UniqueName="FechaProcesamiento"
                                                                    SortExpression="FechaProcesamiento" HeaderText="Fecha Procesamiento Solicitud" DataField="FechaProcesamiento">
                                                                </telerik:GridBoundColumn>

                                                                
                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>


                                                    <%--FIN DEL CONTENIDO --%>

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