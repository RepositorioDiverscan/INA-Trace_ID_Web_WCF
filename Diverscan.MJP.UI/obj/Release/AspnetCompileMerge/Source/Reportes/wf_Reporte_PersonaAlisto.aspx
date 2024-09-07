<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="wf_Reporte_PersonaAlisto.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_Reporte_PersonaAlisto" %>

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
                                    <telerik:RadTab Text="Historico Personas Alisto" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OC">
                                                <h1 class="TituloPanelTitulo"> Control Personas Alisto</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <%--INCIO DEL CONTENIDO --%>

                                                     <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                <%-- Visualización de elementos --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Busqueda de Personal </h1>
                                                             <br />
                                                             <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador01" runat="server" Text="|||"></asp:Label>

                                                             <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador02" runat="server" Text="|||"></asp:Label>

                                                              <asp:Label ID="LblArticulo" runat="server" Text="Persona."></asp:Label>
                                                              <asp:DropDownList ID="ddlIdUsuario" Class="TexboxNormal" runat="server" Width="150px" AutoPostBack="True"></asp:DropDownList>
                                                              <%--<asp:Label ID="LblSeparador03" runat="server" Text="|||"></asp:Label>--%>
                                                             <br />
                                                             <br />
                                    
                                                             <asp:Button runat="server" ID="btnBuscarPorFechas" Text="Generar" OnClick="btnBuscarPorFechas_Click" AutoPostBack="true" ></asp:Button>
                                                            <%--OnClick="btnBuscarPorFechas_Click" --%>
                                                             <asp:Label ID="LblSeparador04" runat="server" Text="|||"></asp:Label>
                                                               
                                                             
                                                             <asp:Button runat="server" ID="BtnLimpiar" Text="Limpiar"  OnClick="BtnLimpiar_Click" />
                                                            <%--OnClick="BtnLimpiar_Click" --%>

                                                            <asp:Label ID="Label3" runat="server" Text="|||"></asp:Label>
                                                            <asp:Button runat="server" ID="BtnExportar" Text="Exportar Excel" OnClick="btnExportar_Click"  />
                                                            <%--OnClick="btnExportar_Click" --%>
                                                         </td>
                                                    </tr>
                                            </table>


                                                     <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas0">
                                                <h1 class="TituloPanelTitulo">Listado Personal</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridPersonalAlisto" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridPersonalAlisto_ItemCommand">
                                                          <%--OnNeedDataSource ="RadGridPersonalAlisto_NeedDataSource"
                                                              OnItemCommand="RadGridPersonalAlisto_ItemCommand"--%>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Cantidad_Solicitudes"
                                                                    SortExpression="Cantidad_Solicitudes" HeaderText="Cantidades Totales" DataField="Cantidad_Solicitudes">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="IdUsuario"
                                                                    SortExpression="IdUsuario" HeaderText="Id Usuario" DataField="IdUsuario" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Encargada" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaAsignacion"
                                                                    SortExpression="FechaAsignacion" HeaderText="Fecha Tarea Asignada" DataField="FechaAsignacion">
                                                                </telerik:GridBoundColumn>

                                                               
                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                     

                                                          <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                <h1 class="TituloPanelTitulo">Detalle Pedido</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridDetallePersonalPedido" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
                                                          <%--OnNeedDataSource ="RadGridPersonalAlisto_NeedDataSource"
                                                              OnItemCommand="RadGridPersonalAlisto_ItemCommand"--%>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Solicitud"
                                                                    SortExpression="Solicitud" HeaderText="Numero de Solicitud" DataField="Solicitud">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Destino"
                                                                    SortExpression="Destino" HeaderText="Destino" DataField="Destino">
                                                                </telerik:GridBoundColumn>

                                                               
                                                                 <telerik:GridBoundColumn UniqueName="CantidadAlistado"
                                                                    SortExpression="CantidadAlistado" HeaderText="Cantidad Alistado" DataField="CantidadAlistado">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="CantidadPedido"
                                                                    SortExpression="CantidadPedido" HeaderText="Cantidad Pedida" DataField="CantidadPedido">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Referencia_Interno"
                                                                    SortExpression="Referencia_Interno" HeaderText="Referencia Bexim" DataField="Referencia_Interno">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>


                                                                 <telerik:GridBoundColumn UniqueName="SSCCAsociado"
                                                                    SortExpression="SSCCAsociado" HeaderText="SSCC Asociado" DataField="SSCCAsociado">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="CantidadUnidadAlisto"
                                                                    SortExpression="CantidadUnidadAlisto" HeaderText="Cantidad UnidadA listo" DataField="CantidadUnidadAlisto">
                                                                </telerik:GridBoundColumn>

                                                                
                                                                <telerik:GridBoundColumn UniqueName="Encargado"
                                                                    SortExpression="Encargado" HeaderText="Encargado" DataField="Encargado">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Alistado"
                                                                    SortExpression="Alistado" HeaderText="Alistado" DataField="Alistado">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Suspendido"
                                                                    SortExpression="Suspendido" HeaderText="Suspendido" DataField="Suspendido">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion" HeaderText="Fecha Creacion Pedido" DataField="FechaCreacion">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                    SortExpression="FechaRegistro" HeaderText="Fecha Registro Alisto" DataField="FechaRegistro">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="FechaAsignacion"
                                                                    SortExpression="FechaAsignacion" HeaderText="Fecha Asignacion" DataField="FechaAsignacion">
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
