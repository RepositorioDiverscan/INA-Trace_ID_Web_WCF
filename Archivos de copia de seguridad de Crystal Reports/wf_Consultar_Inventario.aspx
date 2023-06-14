<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="wf_Consultar_Inventario.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Administracion.wf_Consultar_Inventario" %>

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
                                    <telerik:RadTab Text="Consulta Inventario" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OC">
                                                <h1 class="TituloPanelTitulo">Inventario</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                               <%--INCIO DEL CONTENIDO --%>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label39" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>

                                                      <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas0">
                                            <h1 class="TituloPanelTitulo">Consulta Maestro Inventario</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridMaestroInventario" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" 
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridMaestroInvetario_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                

                                                                  <telerik:GridBoundColumn UniqueName="IdInventarioBasico"
                                                                    SortExpression="IdInventarioBasico" HeaderText="Numero de Inventario" DataField="IdInventarioBasico">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre Inventario" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaPorAplicar"
                                                                    SortExpression="FechaPorAplicar" HeaderText="Fecha de Aplicacion" DataField="FechaPorAplicar">
                                                                </telerik:GridBoundColumn>
                                                             
                                                                <telerik:GridBoundColumn UniqueName="Estado"
                                                                    SortExpression="Estado" HeaderText="Estado" DataField="Estado">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <br>

                                                    <%-- DETALLE iNVENTARIO --%>
                                                     
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas1">
                                                    <h1 class="TituloPanelTitulo">Consulta Detalle Inventario</h1> 
                                                    </asp:Panel>
                                            <telerik:RadGrid ID="RadGridDetalleInvetario" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" 
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                

                                                                  <telerik:GridBoundColumn UniqueName="idArticulo"
                                                                    SortExpression="idArticulo" HeaderText="Id Articulo TraceID" DataField="idArticulo">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="Referencia_Interno"
                                                                    SortExpression="Referencia_Interno" HeaderText="Referencia Bexim" DataField="Referencia_Interno">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Descripcion" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                                </telerik:GridBoundColumn>
                                                             
                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion Ubicacion" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaHoraRegistro"
                                                                    SortExpression="FechaHoraRegistro" HeaderText="Fecha de Hora Registro" DataField="FechaHoraRegistro">
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