<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoDemandaV2.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.HistoricoDemandaV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server">

        <div id="RestrictionZoneID" class="WindowContenedor">

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Historico Demanda" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <%-- IMAGEN DE CARGA --%>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                        <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                    <%-- Visualización de elementos --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Parámetros de Búsqueda</h1>
                                                            <h1></h1>
                                                            <asp:Label ID="lbIdInternoArticulo" runat="server" Text="Artículo:"></asp:Label>
                                                            <asp:DropDownList ID="ddlIdInternoArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlIdInternoArticulo_SelectedIndexChanged"></asp:DropDownList>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <%--<asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>--%>
                                                            <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <h1></h1>
                                                <h1 class="TituloPanelTitulo">Parámetros de Búsqueda</h1>
                                                <br />
                                                <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel" OnClick="btnExportar_Click"></asp:Button>
                                                <br />
                                                <telerik:RadGrid
                                                    ID="RadGridDemanda" runat="server"
                                                    AllowMultiRowSelection="True"
                                                    PageSize="10"
                                                    AllowFilteringByColumn="True"
                                                    AllowPaging="True"
                                                    AllowSorting="True"
                                                    AutoGenerateColumns="False"
                                                    OnNeedDataSource="RadGridDemanda_NeedDataSource"
                                                    OnItemCommand="RadGridDemanda_ItemCommand">

                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="IdInternoArticulo"
                                                                SortExpression="IdInternoArticulo" HeaderText="ID Articulo ERP" DataField="IdInternoArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Articulo" DataField="NombreArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Fecha"
                                                                SortExpression="Fecha" HeaderText="Fecha" DataField="Fecha" DataType="System.DateTime"
                                                                DataFormatString="{0:MM/dd/yyyy}"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="DiaSemana"
                                                                SortExpression="DiaSemana" HeaderText="Dia" DataField="DiaSemana"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad Medida" DataField="Unidad_Medida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                                <asp:Label runat="server" ID="lblCantidadTotal" Visible="false"></asp:Label>


                                                <%--                                                <asp:Panel ID="PanelDetalleDespacho" runat="server" GroupingText="Detalle Demanda">
                                                </asp:Panel>--%>
                                                <br />
                                                <h1 class="TituloPanelTitulo">Detalle Demanda</h1>
                                                <br />
                                                <asp:Button runat="server" ID="btnExportarDetalle" Text="Exportar Detalle" OnClick="btnExportarDetalle_Click" />

                                                <asp:Label runat="server" ID="lblNombreArticulo"></asp:Label>
                                                &nbsp&nbsp
                                                <asp:Label runat="server" ID="lblFecha"></asp:Label>
                                                <br />
                                                <telerik:RadGrid ID="RadGridDetalleDemanda"
                                                    runat="server"
                                                    AllowMultiRowSelection="True"
                                                    PageSize="10"
                                                    AllowFilteringByColumn="True"
                                                    AllowPaging="True"
                                                    AllowSorting="True"
                                                    AutoGenerateColumns="False"
                                                    OnNeedDataSource="RadGridDetalleDemanda_NeedDataSource">
                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="NombreDespacho"
                                                                SortExpression="NombreDespacho" HeaderText="Punto Ventas" DataField="NombreDespacho"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad Medida" DataField="Unidad_Medida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True">
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
