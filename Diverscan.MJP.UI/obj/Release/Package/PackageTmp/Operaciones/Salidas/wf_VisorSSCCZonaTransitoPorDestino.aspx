<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_VisorSSCCZonaTransitoPorDestino.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_VisorSSCCZonaTransitoPorDestino" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <div>
        <asp:Panel ID="Panel4" runat="server">
            <div id="RestrictionZoneIDespachosPedido" class="WindowContenedor">
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows>
                        <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDespachosPedido" AutoSize="true">
                            <ContentTemplate>
                                <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                    <Tabs>
                                        <telerik:RadTab Text="Despachos por Pedido" Width="200px"></telerik:RadTab>
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
                                                      <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                    </div>
                                                    <%--===============================================================================================--%>
                                                    <%--Busqueda en Grid Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado" --%>
                                                    <%--===============================================================================================--%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                        <tr>
                                                            <td>
                                                                <h1 class="TituloPanelTitulo">Parámetros de Búsqueda</h1>
                                                                <br />
                                                                <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                                <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                                <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                                <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                                <asp:TextBox ID="txtTextoBuscar" runat="server"></asp:TextBox>
                                                                <asp:Button runat="server" ID="btnBuscarPorFechas" Text="Buscar" AutoPostBack="true" OnClick="btnBuscarPorFechas_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>


                                                    <%--===============================================================================================--%>
                                                    <%-- Grid Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado--%>
                                                    <%--===============================================================================================--%>
                                                    <h1></h1>
                                                    <h1 class="TituloPanelTitulo">Destinos Solicitudes</h1>
                                                    <%--OnClientClick="DisplayLoadingImage1()"--%>
                                                    <telerik:RadGrid ID="radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado"
                                                        OnNeedDataSource="radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado_NeedDataSource"
                                                        OnItemCommand="radGridDestinos_Solicitud_Rango_Fecha_Con_SSCCAsociado_ItemCommand"
                                                        runat="server"
                                                        AllowMultiRowSelection="true"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False">
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>

                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" Visible="false">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdMaestroSolicitudTID"
                                                                    SortExpression="IdMaestroSolicitudTID" HeaderText="#Solicitud TID" DataField="IdMaestroSolicitudTID"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudSAP"
                                                                    SortExpression="IdSolicitudSAP" HeaderText="#Solicitud ERP" DataField="IdSolicitudSAP"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreDestino"
                                                                    SortExpression="NombreDestino" HeaderText="Destino" DataField="NombreDestino"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion"
                                                                    HeaderText="Fecha Pedido"
                                                                    DataField="FechaCreacion"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10" />
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <%--===============================================================================================--%>
                                                    <%-- Grid SSCC_Zona_Transito_Por_Destino_Solicitud" --%>
                                                    <%--===============================================================================================--%>
                                                    <h1></h1>
                                                    <h1 class="TituloPanelTitulo">SSCC Zona Tránsito</h1>
                                                    <telerik:RadGrid ID="radGridSSCC_Zona_Transito_Por_Destino_Solicitud"
                                                        runat="server"
                                                        AllowMultiRowSelection="True"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridSSCC_Zona_Transito_Por_Destino_Solicitud_NeedDataSource"
                                                        OnItemCommand="radGridSSCC_Zona_Transito_Por_Destino_Solicitud_ItemCommand">

                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="IdConsecutivoSSCC"
                                                                    SortExpression="IdConsecutivoSSCC" HeaderText="IdSSCC" DataField="IdConsecutivoSSCC"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SSCCGenerado"
                                                                    SortExpression="SSCCGenerado" HeaderText="SSCC" DataField="SSCCGenerado"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdMaestroSolicitud"
                                                                    SortExpression="IdMaestroSolicitud" HeaderText="#Solicitud TID" DataField="IdMaestroSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10" />
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <%--===============================================================================================--%>
                                                    <%-- Grid Articulos_SSCC_Procesado" --%>
                                                    <%--===============================================================================================--%>
                                                    <h1></h1>
                                                    <h1 class="TituloPanelTitulo">Artículos SSCC Seleccionado</h1>
                                                    <telerik:RadGrid ID="radGridArticulos_SSCC_Procesado"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridArticulos_SSCC_Procesado_NeedDataSource">
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="NombreArticulo" DataField="NombreArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadUI"
                                                                    SortExpression="CantidadUI" HeaderText="Cantidad" DataField="CantidadUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" DataType="System.Decimal">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="UnidadMedidaUI"
                                                                    SortExpression="UnidadMedidaUI" HeaderText="UnidadMedidaUI" DataField="UnidadMedidaUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10" />
                                                        </MasterTableView>
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
    </div>
</asp:Content>

