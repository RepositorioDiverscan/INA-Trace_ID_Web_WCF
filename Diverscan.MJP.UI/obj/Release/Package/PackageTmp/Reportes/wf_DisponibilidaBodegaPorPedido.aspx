<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_DisponibilidaBodegaPorPedido.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_DisponibilidaBodegaPorPedido" %>

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
                                    <telerik:RadTab Text="Disponibilidad En Bodega Por Pedido" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">


                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

                                        <%--comienza UpdatePanel--%>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <h1 class="TituloPanelTitulo">Solicitud por Bodega</h1>
                                                <asp:Button runat="server" ID="btnExportarExcelPedidosBodega" Text="Exportar a Excel" OnClick="btnExportarExcelPedidosBodega_Click" Visible="false" />
                                                <br />
                                                <%--Grid Pedidos por Bodega--%>
                                                <%-- Width="100%" --%>
                                                <telerik:RadGrid ID="RGPedidos"                                                    
                                                    OnNeedDataSource="RadGrid_NeedDataSource"
                                                    OnItemCommand="RGAprobarSalida_ItemCommand"                                                   
                                                    runat="server"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    AllowFilteringByColumn="true"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true"
                                                    >

<%--                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>--%>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView>

                                                        <Columns>
                                                            <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                            </telerik:GridClientSelectColumn>

                                                            <telerik:GridButtonColumn CommandName="btnVerDetalle" Text="Detalle" UniqueName="btnVerDetalle" HeaderText="">
                                                            </telerik:GridButtonColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdMaestroSolicitud"
                                                                SortExpression="IdMaestroSolicitud" HeaderText="Solicitud #" DataField="IdMaestroSolicitud"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Solicitud"
                                                                SortExpression="Solicitud" HeaderText="Solicitud ERP" DataField="Solicitud"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Fecha"
                                                                SortExpression="Fecha" HeaderText="Fecha Solicitud" DataField="Fecha"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdBodega"
                                                                SortExpression="IdBodega" HeaderText="Id Bodega" DataField="IdBodega"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Bodega"
                                                                SortExpression="Bodega" HeaderText="Bodega" DataField="Bodega"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Destino"
                                                                SortExpression="Destino" HeaderText="Destino" DataField="Destino"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                SortExpression="IdInterno" HeaderText="IdInterno" DataField="IdInterno"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                    </MasterTableView>

                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                                <br />
                                                <%--Grid detalle por pedido bodega--%>
                                                <h1 class="TituloPanelTitulo">Detalle Solicitud por Bodega</h1>
                                                <label>#Solicitud TID:</label><asp:Label ID="lbNumSolicitudTID" runat="server"></asp:Label>
                                                <label> || </label>
                                                <%--<label>#Pedido ERP:</label>--%>
                                                <asp:Label ID="lbNumeroPedidoERP" runat="server"></asp:Label>
                                                <label> || </label>
                                                <label>Bodega:</label><asp:Label ID="lbBodega" runat="server"></asp:Label>
                                                <label> || </label>
                                                <label>#Destino:</label><asp:Label ID="lbDestino" runat="server"></asp:Label>
                                                <br />
                                                <asp:Button runat="server" ID="btnExportarExcelDetallePedidoBodega" Text="Exportar a Excel" OnClick="btnExportarExcelDetallePedidoBodega_Click" />                                               
                                                <telerik:RadGrid ID="RadGridPedidoBodega"
                                                    OnNeedDataSource="RadGridDetalleSalida_NeedDataSource"
                                                    runat="server"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    AllowFilteringByColumn="true"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true">

                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="IdInternoArticulo" Visible="false"
                                                                SortExpression="IdInternoArticulo" HeaderText="Id DS" DataField="IdInternoArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Nombre" DataField="NombreArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                SortExpression="UnidadMedida" HeaderText="Unidad Medida" DataField="UnidadMedida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="CantidadEnPedido"
                                                                SortExpression="CantidadEnPedido" HeaderText="Pedido UI" DataField="CantidadEnPedido"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="CantidadEnBodega"
                                                                SortExpression="CantidadEnBodega" HeaderText="Cantidad Bodega UI" DataField="CantidadEnBodega"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="DiferenciaBodegaPedido"
                                                                SortExpression="DiferenciaBodegaPedido" HeaderText="Disponible UI" DataField="DiferenciaBodegaPedido"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="TipoArticulo"
                                                                SortExpression="TipoArticulo" HeaderText="Tipo Artículo" DataField="TipoArticulo"
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
                                        <%--Termina UpdatePanel--%>
                                        <h1></h1>
                                        <%-- <asp:Label ID="Label2" runat="server" Text="" Width="200"></asp:Label> 
                                              <asp:Button runat="server"  ID ="btnAprobar" Text="Asignar Tarea a Usuario"  OnClientClick = "DisplayLoadingImage1();" OnClick ="btnAprobar_Click"/> --%>
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
