<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ArticulosDisponiblesBodega.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.ArticulosDisponibles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel5" runat="server">

        <div id="RestrictionZoneIDDisponibilidadArticulosBodega" class="WindowContenedor">

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDDisponibilidadArticulosBodega" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Artículos Disponibles Bodega" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer" Height="100%">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>


                                                <%-- Modos de búsqueda --%>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Visualizar artículos</h1>
                                                            <h1></h1>
                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" OnClick="btnRefrescar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            <label>||</label>
                                                            <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel" OnClick="btnExportar_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <%--===============================================================================================--%>
                                                <%-- Grid Articulos Disponibles Bodega --%>
                                                <%--===============================================================================================--%>
                                                <asp:Panel ID="PanelArticulosDisponiblesBodega" runat="server">
                                                    <h1 class="TituloPanelTitulo">Artículos Disponibles</h1>
                                                    <telerik:RadGrid
                                                        ID="RadGridArticulosDisponiblesBodega"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="RadGridArticulosDisponiblesBodega_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="DIdTID"
                                                                    SortExpression="IdTID" HeaderText="IdTID" DataField="IdTID"  
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DIdERP"
                                                                    SortExpression="IdERP" HeaderText="IdERP" DataField="IdERP" 
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DArticulo"
                                                                    SortExpression="Articulo" HeaderText="Artículo" DataField="Articulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DLote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="DUbicacion"
                                                                    SortExpression="Ubicacion" HeaderText="Ubicación" DataField="Ubicacion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="DUnidadesInventario"
                                                                    SortExpression="Lote" HeaderText="UnidadesInventario" DataField="UnidadesInventario" 
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DUnidadMedida"
                                                                    SortExpression="Lote" HeaderText="UnidadMedida" DataField="UnidadMedida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </asp:Panel>
                                                <%--===============================================================================================--%>
                                                <%-- Grid Articulos Disponibles Bodega Totales --%>
                                                <%--===============================================================================================--%>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Visualizar artículos</h1>
                                                            <h1></h1>
                                                            <asp:Button runat="server" ID="btnRefrescar1" Text="Refrescar" OnClick="btnRefrescar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            <label>||</label>
                                                            <%--<asp:Button runat="server" ID="btnExportarExportarExcelTotales" Text="Exportar a Excel" OnClick="btnExportarExportarExcelTotales_Click"></asp:Button>--%>
                                                            <asp:Button runat="server" ID="btnExportarExportarExcelTotales" Text="Exportar a Excel"></asp:Button>

                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="PanelArticulosDisponiblesBodegaTotales" runat="server">
                                                    <h1 class="TituloPanelTitulo">Artículos Disponibles Totales</h1>
                                                    <telerik:RadGrid ID="RadGridArticulosDisponiblesBodegaTotales"
                                                        runat="server"
                                                        AllowMultiRowSelection="False"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="RadGridArticulosDisponiblesBodegaTotales_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="DTIdERP"
                                                                    SortExpression="IdERP" HeaderText="IdERP" DataField="IdERP" 
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DTArticulo"
                                                                    SortExpression="Articulo" HeaderText="Artículo" DataField="Articulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DTTotalPorArticulo"
                                                                    SortExpression="TotalPorArticulo" HeaderText="Total" DataField="TotalPorArticulo" 
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DTUnidadMedida"
                                                                    SortExpression="Lote" HeaderText="UnidadMedida" DataField="UnidadMedida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>

                                                        </MasterTableView>
                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                    </telerik:RadGrid>
                                                </asp:Panel>
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

