<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="wf_KardexMacro.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_KardexMacro" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   <%-- Kardex --%>

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>

    <asp:Panel ID="Panel4" runat="server">
        <div id="RestrictionZoneID" class="WindowContenedor">   <%-- Kardexx --%>
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">   <%--Kardexx --%>
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Kardex" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer" Height="100%">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <%-- IMAGEN DE CARGA 
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>   --%>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial;" id="Table1">

                                                    <%-- Visualización de elementos --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Seleccione un artículo</h1>                                                         

                                                            <h1></h1>
                                                        </td>
                                                    </tr>
                                                    <%-- Articulo  --%>
                                                    <tr>
                                                        <td>
                                                            <%--<h1 class="TituloPanelTitulo">Seleccione el criterio para el artículo</h1>--%>                                                                                                                                                                          
                                                            <asp:Label ID="lbIdInternoArticulo" runat="server" Text="Artículo:"></asp:Label>
                                                            <asp:DropDownList ID="ddlIdInternoArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlIdInternoArticulo_SelectedIndexChanged"></asp:DropDownList>
                                                            <h1></h1>
                                                        </td>
                                                    </tr>

                                                    <%-- Modos de búsqueda --%>
                                                    <tr>
                                                        <td>

                                                            <%--<asp:Panel ID="PanelFechaRecepcion" runat="server" GroupingText="Búsqueda por rango de fechas">--%>
                                                            <h1 class="TituloPanelTitulo">Búsqueda por rango de fechas</h1>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            <%--<asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click"></asp:Button>--%>                                                                                                                        
                                                            <h1></h1>
                                                            <asp:Label ID="Label8" runat="server" Text="Total Global: "></asp:Label>
                                                            <asp:Label ID="lbTotalGlobal" runat="server" Text=""></asp:Label>
                                                            <%-- </asp:Panel>--%>


                                                        </td>
                                                    </tr>
                                                </table>

                                                <%--===============================================================================================--%>
                                                <%-- Grid Ajuste Inventario --%>
                                                <%--===============================================================================================--%>

                                                <asp:Panel ID="PanelAjustesInventario" runat="server">
                                                    <h1></h1>
                                                    <%--<asp:Label ID="Label11" runat="server" Text="Ajustes de Inventario del Artículo:" Font-Bold="True"></asp:Label>--%>
                                                    <h1 class="TituloPanelTitulo">Movimientos Registrados</h1>
                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnExportarKardexMacro" Text="Exportar a Excel" OnClick="btnExportarKardexMacro_Click" />
                                                    <h1></h1>
                                                    <telerik:RadGrid
                                                        ID="radGridKardexMacro"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridKardexMacro_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad_Unidades_inventario"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="Unidad Medida" DataField="Unidad_Medida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DetalleMovimiento"
                                                                    SortExpression="AjusteInventarioDescripcion" HeaderText="Detalle Movimiento" DataField="Detalle_movimiento"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="EfectoInventario"
                                                                    SortExpression="DescripcionDestino" HeaderText="Num.Documento" DataField="Num_Documento"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn
                                                                    UniqueName="FechaRegistroTrazabilidad"
                                                                    SortExpression="FechaRegistroTrazabilidad"
                                                                    HeaderText="Fecha Registro"
                                                                    DataField="Fecha_Registro"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True"
                                                                    Visible="True">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>

                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <h1></h1>
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