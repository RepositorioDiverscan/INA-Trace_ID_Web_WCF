<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historico_OrdenC.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.Historico_OrdenC"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="ContentPedidosBodega" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
      <asp:Panel ID="Panel4Pedidos" runat="server">
        <div id="RestrictionZoneIDBodega" class="WindowContenedor">
           <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>
              <Windows>   
                   <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDBodega" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Historico de Ordenes de Compra" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                 <telerik:RadPageView runat="server" ID="RadPageView1">
                                      <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                        <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="http://138.59.16.4/Sitio_WEB/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>



                                             <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio: " Width="85px"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                   
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Final: " Width="85px"></asp:Label> 
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                 
                                                    </tr>
                                                    <tr>
                                                          <td>
                                                           <asp:Label ID="Label39" runat="server" Text="Buscar: " Width="85px"></asp:Label>
                                                           <asp:TextBox ID="TxtOC" runat="server"></asp:TextBox>
                                                        </td>
                                   
                                                   </tr>
                                                        </br>
                                                  
                                                    <tr>
                                                         <td>
                                                           <asp:Button runat="server" ID="btnBuscar"  OnClick="btnBuscar_Click" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()"  />
                                                              <asp:Label ID="Label15" runat="server" Text=" || "></asp:Label>
                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" />
                                                              <asp:Label ID="Label1" runat="server" Text=" || "></asp:Label>
                                                             <asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnExportar_Click" />

                                                        </td>
                                                    </tr>
                                                </table>
                                             <%--OnNeedDataSource="RadGrid_NeedDataSource"--%>  
                                                <h1 class="TituloPanelTitulo">Ordenes de Compra</h1>
                                                 <telerik:RadGrid ID="RGOrdenCompra"                                                  
                                                    runat="server"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    AllowFilteringByColumn="true"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="True"
                                                    OnNeedDataSource="RGOrdenCompra_NeedDataSource"
                                                    >

                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <GroupingSettings CaseSensitive="false" ></GroupingSettings>
                                                    <MasterTableView>

                                                        <Columns>
                                                           

                                                            <telerik:GridBoundColumn UniqueName="Orden_Compra"
                                                                SortExpression="Orden_Compra" HeaderText="Numero de OC" DataField="Orden_Compra"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                           <telerik:GridBoundColumn UniqueName="Producto"
                                                                SortExpression="Producto" HeaderText="Nombre del Producto" DataField="Producto"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad_Recibida"
                                                                SortExpression="Cantidad_Recibida" HeaderText="Cantidad Recibida" DataField="Cantidad_Recibida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn UniqueName="Cantidad_Rechazada"
                                                                SortExpression="Cantidad_Rechazada" HeaderText="Cantidad Rechazada" DataField="Cantidad_Rechazada"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn UniqueName="Articulos_OC"
                                                                SortExpression="Articulos_OC" HeaderText="Articulos de la OC" DataField="Articulos_OC"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                SortExpression="FechaRegistro" HeaderText="Fecha Registro en Bodega" DataField="FechaRegistro"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                SortExpression="FechaCreacion" HeaderText="Fecha Creacion OC" DataField="FechaCreacion"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                </telerik:RadGrid>




                                        </ContentTemplate>
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
