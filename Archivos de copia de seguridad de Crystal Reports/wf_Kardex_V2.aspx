<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Kardex_V2.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_Kardex_V2" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="ContentKardex2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
        function DisplayLoadingImage2() {
            document.getElementById("loading2").style.display = "block";
        }
        function DisplayLoadingImage3() {
            document.getElementById("loading3").style.display = "block";
        }
        function MuestraMensajeOk() {
            alert("Proceso Terminado exitosamente");
        }
    </script>

    <asp:Panel ID="Panel4" runat="server">
        <div id="RestrictionZoneIDKardexx" class="WindowContenedor">
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDKardexx" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Trazabilidad (Kardex)" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer" Height="100%">
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
                                                            <h1 class="TituloPanelTitulo">Seleccione los Elementos que Desea Visualizar</h1>
                                                            <%--<asp:Label ID="Label13" runat="server" Text="|| Trazabilidad Bodega:"></asp:Label>--%>
                                                            <asp:Label ID="Label13" runat="server" Text="|| Movimientos Internos:"></asp:Label>
                                                            <asp:CheckBox ID="chkVerTrazabilidad" AutoPostBack="true" runat="server" OnCheckedChanged="chkVerTrazabilidad_CheckedChanged" />
                                                            <%--<asp:Label ID="Label14" runat="server" Text=" || Ajustes Inventario:"></asp:Label>--%>
                                                            <asp:Label ID="Label14" runat="server" Text=" || Movimientos de Inventario:"></asp:Label>
                                                            <asp:CheckBox ID="chkVerAjustesInventario" AutoPostBack="true" runat="server" OnCheckedChanged="chkVerAjustesInventario_CheckedChanged" />
                                                            <%--<asp:Label ID="Label15" runat="server" Text=" || Despachos:"></asp:Label>--%>
                                                            <asp:Label ID="Label15" runat="server" Text=" || Traslados:"></asp:Label>
                                                            <asp:CheckBox ID="chkVerDespachos" AutoPostBack="true" runat="server" OnCheckedChanged="chkVerDespachos_CheckedChanged" />
                                                            <asp:Label ID="Label4" runat="server" Text=" ||"></asp:Label>
                                                            <h1></h1>
                                                        </td>
                                                    </tr>
                                                    <%-- Articulo  --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Seleccione un Artículo</h1>
                                                            <br />
                                                            <asp:Label ID="lbArticuloDDL" runat="server" Text="Artículo:"></asp:Label>
                                                            <asp:DropDownList ID="ddlIDArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlIDArticulo_SelectedIndexChanged"></asp:DropDownList>
                                                            <h1></h1>
                                                        </td>
                                                    </tr>

                                                    <%-- Modos de búsqueda --%>
                                                    <tr>
                                                        <td>

                                                            <%--<asp:Panel ID="PanelFechaRecepcion" runat="server" GroupingText="Búsqueda por rango de fechas">--%>
                                                            <h1 class="TituloPanelTitulo">Búsqueda por Rango de Fechas</h1>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha Inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <%--<asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>--%>
                                                            <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            <h1></h1>
                                                            <asp:Label ID="Label1" runat="server" Text="Lotes:"></asp:Label>
                                                            <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_SelectedIndexChanged"></asp:DropDownList>
                                                            <h1></h1>
                                                            <asp:Label ID="Label3" runat="server" Text="Filtrar por Lote:"></asp:Label>
                                                            <asp:CheckBox ID="ChkFiltrarPorLote" AutoPostBack="true" runat="server" OnCheckedChanged="ChkFiltrarPorLote_CheckedChanged" />
                                                            <h1></h1>
                                                            <asp:Label ID="Label10" runat="server" Text="ID ICG Artículo: "></asp:Label>
                                                            <asp:Label ID="lbIdArticuloSAP" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="Label12" runat="server" Text=" || "></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text="ID TRACEID Artículo: "></asp:Label>
                                                            <asp:Label ID="lbIdArticuloTID" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="Label5" runat="server" Text=" || "></asp:Label>
                                                            <asp:Label ID="Label8" runat="server" Text="Total Global: " Visible="false"></asp:Label>
                                                            <asp:Label ID="lbTotalGlobal" runat="server" Text="" Visible="false"></asp:Label>
                                                            <%-- </asp:Panel>--%>
                                                        </td>
                                                    </tr>
                                                </table>



                                                <%--===============================================================================================--%>
                                                <%-- Grid Trazabilidad Bodega || Nombre Visual: "Movimientos Internos"--%>
                                                <%--===============================================================================================--%>
                                                <asp:Panel ID="PanelGridTrazabilidadBodega" runat="server">
                                                    <h1></h1>                                                    
                                                    <%--<h1 class="TituloPanelTitulo">Trazabilidad Bodega</h1>--%>
                                                    <h1 class="TituloPanelTitulo">Movimientos Internos</h1>
                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel" OnClick="btnExportar_Click"  />
                                                    <%--<input type="button" value="Imprimir Reporte" onclick="javascript: imprimirDiv('gridTrazabilidadBodega')" />--%>
                                                    <h1></h1>
                                                    <div id="gridTrazabilidadBodegaReporte">
                                                        <telerik:RadGrid
                                                            ID="gridTrazabilidadBodega"
                                                            runat="server"
                                                            AllowMultiRowSelection="false"
                                                            PageSize="10"
                                                            AllowFilteringByColumn="True"
                                                            AllowPaging="True"
                                                            AllowSorting="True"
                                                            AutoGenerateColumns="False"
                                                            OnNeedDataSource="gridTrazabilidadBodega_NeedDataSource"
                                                            OnClientClick="DisplayLoadingImage1()">
                                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="false"></Selecting>
                                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                            </ClientSettings>
                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                            <GroupingSettings CaseSensitive="false" />
                                                            <MasterTableView>

                                                                <Columns>

                                                                    <telerik:GridBoundColumn UniqueName="AIdArticulo" Visible="false"
                                                                        SortExpression="IdArticulo" HeaderText="IdArticulo" DataField="IdArticulo"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="ANumeroOC" Visible="false"
                                                                        SortExpression="NumeroOC" HeaderText="NumeroOC SAP" DataField="NumeroOC"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdMaestroOC" Visible ="false"
                                                                        SortExpression="IdMaestroOC" HeaderText="NumeroOC TID" DataField="IdMaestroOC"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="ALote"
                                                                        SortExpression="Lote" HeaderText="Lote" DataField="Lote"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AFechaVencimiento"
                                                                        SortExpression="FechaVencimiento"
                                                                        HeaderText="Fecha Vencimiento"
                                                                        DataField="FechaVencimiento"
                                                                        DataFormatString="{0:dd/MM/yyyy}"
                                                                        DataType="System.DateTime"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AIdUbicacion" Visible="false"
                                                                        SortExpression="IdUbicacion" HeaderText="IdUbicacion" DataField="IdUbicacion"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AIdEstado" Visible="false"
                                                                        SortExpression="IdEstado" HeaderText="IdEstado" DataField="IdEstado"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AEstadoDescripcion" Visible="true"
                                                                        SortExpression="EstadoDescripcion" HeaderText="Tipo Movimiento" DataField="EstadoDescripcion"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="ANombreUsuario" Visible="true"
                                                                        SortExpression="NombreUsuario" HeaderText="Usuario" DataField="NombreUsuario"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="ADetalleMovimiento" Visible="true"
                                                                        SortExpression="DetalleMovimiento" HeaderText="Detalle Movimiento" DataField="DetalleMovimiento"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn
                                                                        UniqueName="AFechaRegistroDate"
                                                                        SortExpression="FechaRegistroDate"
                                                                        HeaderText="FechaRegistro"
                                                                        DataField="FechaRegistroDate"
                                                                        DataFormatString="{0:dd/MM/yyyy}"
                                                                        DataType="System.DateTime"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="ACantidad"
                                                                        SortExpression="Cantidad" HeaderText="Cantidad UI" DataField="Cantidad"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AUnidadMedida"
                                                                        SortExpression="UnidadMedida" HeaderText="Unidad Medida" DataField="UnidadMedida"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="AEtiquetaUbicacion"
                                                                        SortExpression="EtiquetaUbicacion" HeaderText="EtiquetaUbicacion" DataField="EtiquetaUbicacion"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                </Columns>

                                                                <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>
                                                </asp:Panel>

                                                <%--===============================================================================================--%>
                                                <%-- Grid Ajuste Inventario Nombre Visual: "Movimientos Inventario"--%>
                                                <%--===============================================================================================--%>

                                                <asp:Panel ID="PanelAjustesInventario" runat="server">
                                                    <h1></h1>
                                                    <%--<asp:Label ID="Label11" runat="server" Text="Ajustes de Inventario del Artículo:" Font-Bold="True"></asp:Label>--%>
                                                    <%--<h1 class="TituloPanelTitulo">Ajustes de Inventario del Artículo</h1>--%>
                                                    <h1 class="TituloPanelTitulo">Movimientos Inventario</h1>
                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnExportarExcelAjusteInventario" Text="Exportar a Excel" OnClick="btnExportarExcelAjusteInventario_Click" />
                                                    <h1></h1>
                                                    <telerik:RadGrid
                                                        ID="radGridAjustesInventario"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridAjustesInventario_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="#MovimientoRef"
                                                                    SortExpression="NumeroMovimiento" HeaderText="#MovimientoRef" DataField="NumeroMovimiento"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="UsuarioNombreCompleto"
                                                                    SortExpression="UsuarioNombreCompleto" HeaderText="Usuario" DataField="UsuarioNombreCompleto"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdInternoSAP"
                                                                    SortExpression="IdInternoSAP" HeaderText="ID SAP" DataField="IdInternoSAP"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdArticuloTID"
                                                                    SortExpression="IdArticulo" HeaderText="ID TID" DataField="IdArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticuloAI"
                                                                    SortExpression="NombreArticulo" HeaderText="Artículo" DataField="NombreArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Lote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad UI" DataField="Cantidad"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="Unidad Medida" DataField="UnidadMedida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="AjusteInventarioDescripcion"
                                                                    SortExpression="AjusteInventarioDescripcion" HeaderText="Tipo Movimiento" DataField="AjusteInventarioDescripcion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DescripcionDestino"
                                                                    SortExpression="DescripcionDestino" HeaderText="Ubicación" DataField="DescripcionDestino"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn
                                                                    UniqueName="FechaRegistroTrazabilidad"
                                                                    SortExpression="FechaRegistroTrazabilidad"
                                                                    HeaderText="Fecha Registro"
                                                                    DataField="FechaRegistroTrazabilidad"
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


                                                <%--===============================================================================================--%>
                                                <%-- Grid Despachos Artículo || Nombre Visual "Traslados" --%>
                                                <%--===============================================================================================--%>
                                                <asp:Panel ID="PanelDespachosArticulo" runat="server">
                                                    <%--<h1 class="TituloPanelTitulo">Despachos del Artículo</h1>--%>
                                                    <h1 class="TituloPanelTitulo">Traslados</h1>
                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnExportarDespachos" Text="Exportar a Excel" OnClick="btnExportarDespachos_Click" />
                                                    <h1></h1>
                                                    <h1></h1>
                                                    <%--  <asp:Label ID="Label9" runat="server" Text="Filtrar por lote despachos"></asp:Label>
                                                    <asp:CheckBox ID="chkFiltroLoteDespacho" AutoPostBack="true" runat="server" OnCheckedChanged="chkFiltroLoteDespacho_CheckedChanged" />
                                                    <h1></h1>
                                                    <asp:Label ID="Label8" runat="server" Text="Lotes de despacho    :"></asp:Label>
                                                    <asp:DropDownList ID="ddLotesDespachos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddLotesDespachos_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <h1></h1>
                                                    <telerik:RadGrid ID="radGridDespachosArticulo"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridDespachosArticulo_NeedDataSource">

                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="IdInternoSAPSolicitud"
                                                                    SortExpression="IdInternoSAPSolicitud" HeaderText="#Solicitud SAP" DataField="IdInternoSAPSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudTID"
                                                                    SortExpression="IdSolicitudTID" HeaderText="#Solicitud TID" DataField="IdSolicitudTID"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DIdAInterno"
                                                                    SortExpression="IdInterno" HeaderText="Codigo" DataField="IdInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DLote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DFechaVencimiento"
                                                                    SortExpression="FechaDespacho"
                                                                    HeaderText="Fecha Vencimiento"
                                                                    DataField="FechaVencimiento"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DCantidadSolicitada"
                                                                    SortExpression="CantidadSolicitada" HeaderText="Pedido UI" DataField="CantidadSolicitada"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="DCantidadDespachada"
                                                                    SortExpression="CantidadDespachada" HeaderText="Alistado UI" DataField="CantidadDespachada"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DUnidadMedida"
                                                                    SortExpression="CantidadUnidadAlisto" HeaderText="Unidades Alisto Solicitud" DataField="UnidadMedida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DCantidadUnidadAlisto" Visible="false"
                                                                    SortExpression="CantidadUnidadAlisto" HeaderText="Unidad Medida" DataField="CantidadUnidadAlistoSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="DEtiquetaUbicacionDespacho" Visible="false"
                                                                    SortExpression="EtiquetaUbicacionDespacho" HeaderText="Ubicacion Salida" DataField="EtiquetaUbicacionDespacho"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DDestinoSolicitud"
                                                                    SortExpression="DestinoSolicitud" HeaderText="Destino" DataField="DestinoSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DFechaRegistro"
                                                                    SortExpression="FechaDespacho"
                                                                    HeaderText="Fecha Despacho"
                                                                    DataField="FechaDespacho"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>

                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                        </MasterTableView>
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
