<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_DespachosPorPedido.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_DespachosPorPedido" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>--%>
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
                                                      <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                    </div>
                                                    <%--===============================================================================================--%>
                                                    <%--Busqueda en Grid con despachos por pedido" --%>
                                                    <%--===============================================================================================--%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                        <%-- Visualización de elementos --%>
                                                        <tr>
                                                            <td>
                                                                <h1 class="TituloPanelTitulo">Pedidos Despacho</h1>
                                                                <br />
                                                                <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                                <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                                <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                                <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                                <asp:Button runat="server" ID="btnBuscarPorFechas" Text="Buscar" AutoPostBack="true" OnClick="btnBuscarPorFechas_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                                <asp:Label ID="Label2" runat="server" Text=" || Solo Pedidos Despachados:"></asp:Label>
                                                                <asp:CheckBox ID="chkVerSoloPedidosDespachados" AutoPostBack="true" runat="server" Checked="true" OnCheckedChanged="chkVerSoloPedidosDespachados_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>


                                                    <%--===============================================================================================--%>
                                                    <%-- Grid Solicitudes Despacho --%>
                                                    <%--===============================================================================================--%>
                                                    <h1></h1>
                                                    <h1 class="TituloPanelTitulo">Solicitudes Despacho</h1>
                                                    <%--OnClientClick="DisplayLoadingImage1()"--%>
                                                    <telerik:RadGrid ID="radGridPedidosDespacho"
                                                        OnNeedDataSource="radGridPedidosDespacho_NeedDataSource"
                                                        OnItemCommand="radGridPedidosDespacho_ItemCommand"
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

                                                                <telerik:GridBoundColumn UniqueName="NumeroSolicituTID"
                                                                    SortExpression="NumeroSolicituTID" HeaderText="#Solicitud TID" DataField="NumeroSolicituTID"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NumeroSolicitudERP"
                                                                    SortExpression="NumeroSolicitudERP" HeaderText="#SolicitudERP" DataField="NumeroSolicitudERP"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdInternoSolicitud"
                                                                    SortExpression="IdInternoSolicitud" HeaderText="#IdInternoSolicitud" DataField="IdInternoSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="IdDestinoPedido"
                                                                    SortExpression="IdDestinoPedido" HeaderText="#Destino" DataField="IdDestinoPedido"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="NombreDestino"
                                                                    SortExpression="NombreDestino" HeaderText="Destino" DataField="NombreDestino"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="EstadoSolicitud"
                                                                    SortExpression="EstadoSolicitud" HeaderText="Estado Solicitud" DataField="EstadoSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="SFechaPedido"
                                                                    SortExpression="FechaPedido"
                                                                    HeaderText="Fecha Pedido"
                                                                    DataField="FechaPedido"
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
                                                    <%--Búsqueda por número de pedido TID" --%>
                                                    <%--===============================================================================================--%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                        <%-- Visualización de elementos --%>
                                                        <tr>
                                                            <td>
                                                                <h1 class="TituloPanelTitulo">Busqueda por Número de Solicitud</h1>
                                                                <br />
                                                                <asp:Label ID="Label11" runat="server" Text="Número Solicitud:"></asp:Label>
                                                                <asp:TextBox ID="txtBuscarNumSolicitud" runat="server"></asp:TextBox>

                                                                <asp:Button runat="server" ID="btnBuscarDespachoPedido" Text="Buscar" AutoPostBack="true" OnClick="btnBuscarDespachoPedido_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                                <span>||</span>
                                                                <asp:Button runat="server" ID="btnExportarDetalleSolicitudDespacho" Text="Exportar a Excel" OnClick="btnExportarDetalleSolicitudDespacho_Click" />
                                                                <span>||</span>
                                                                <asp:Button runat="server" ID="btnExportarConFormatoCR" Text="Exportar con formato" OnClick="btnExportarConFormatoCR_Click" />
                                                                <hr />
                                                                <asp:Label ID="Label5" runat="server" Text="ID SAP Solicitud: "></asp:Label>
                                                                <asp:Label ID="lbIdInternoSolicitud" runat="server" Text=""></asp:Label>
                                                                <hr />

                                                                <asp:Label ID="Label15" runat="server" Text="Fecha del Solicitud: "></asp:Label>
                                                                <asp:Label ID="lbFechaPedido" runat="server" Text=""></asp:Label>
                                                                <hr />
                                                                <asp:Label ID="Label4" runat="server" Text="Destino del Solicitud: "></asp:Label>
                                                                <asp:Label ID="lbDestinoPedido" runat="server" Text=""></asp:Label>
                                                                <hr />

                                                                <asp:Label ID="Label1" runat="server" Text="Sub total: "></asp:Label>
                                                                <asp:Label ID="lbSubTotal" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <%--<input type="button" class="btn btn-primary" data-toggle="modal" value="Imprimir Reporte" data-target="#myModal">--%>
                                                    <br />
                                                    <%--===============================================================================================--%>
                                                    <%-- Grid Detalle Despachos Por Solicitud "Pedido" --%>
                                                    <%--===============================================================================================--%>
                                                    <h1></h1>
                                                    <h1 class="TituloPanelTitulo">Despachos Por Número Solicitud</h1>
                                                    <%--OnClientClick="DisplayLoadingImage1()"--%>
                                                    <telerik:RadGrid ID="radGridDetalleDespachoSolicitud"
                                                        runat="server"
                                                        AllowMultiRowSelection="false"
                                                        PageSize="10"
                                                        AllowFilteringByColumn="True"
                                                        AllowPaging="True"
                                                        AllowSorting="True"
                                                        AutoGenerateColumns="False"
                                                        OnNeedDataSource="radGridDetalleDespachoSolicitud_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="SCodigo"
                                                                    SortExpression="Codigo" HeaderText="Codigo" DataField="Codigo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SDescripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SSSCC"
                                                                    SortExpression="SSCC" HeaderText="SSCC" DataField="SSCCEtiqueta"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SBultos"
                                                                    SortExpression="Bultos" HeaderText="Bultos" DataField="BultosUnidadMedidaConcatenado"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SPedidoUI"
                                                                    SortExpression="PedidoUI" HeaderText="Pedido" DataField="PedidoUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SAlistadoUI"
                                                                    SortExpression="AlistadoUI" HeaderText="Alistado" DataField="AlistadoUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="SUnidadMedidaUI"
                                                                    SortExpression="UnidadMedidaUI" HeaderText="Und." DataField="UnidadMedidaUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SCosto"
                                                                    SortExpression="Costo" HeaderText="Costo" DataField="Costo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="STotal"
                                                                    SortExpression="Total" HeaderText="Total" DataField="Total"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>



                                                                <%--                                                                <telerik:GridBoundColumn UniqueName="SCodigo"
                                                                    SortExpression="Codigo" HeaderText="Codigo" DataField="Codigo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SLote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote" Visible="false"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SDescripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>                                                              

                                                                <telerik:GridBoundColumn UniqueName="SUnd" Visible="false"
                                                                    SortExpression="Und" HeaderText="UA Detalle" DataField="Und"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SSCC"
                                                                    SortExpression="SSCC" HeaderText="SSCC" DataField="SSCC"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" Visible="false">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="SSCCEtiqueta"
                                                                    SortExpression="SSCCEtiqueta" HeaderText="SSCC" DataField="SSCCEtiqueta"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SUbicacion" Visible="false"
                                                                    SortExpression="Ubicacion" HeaderText="Ubicacion" DataField="Ubicacion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SPedido" Visible="true"
                                                                    SortExpression="PedidoUI" HeaderText="Pedido Und.Inventario" DataField="PedidoUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="SAlistado"
                                                                    SortExpression="AlistadoUI" HeaderText="Alistado Und.Invetario" DataField="AlistadoUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SUnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="Und.Medida UI" DataField="UnidadMedidaUI"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="SAlistadoUF"
                                                                    SortExpression="AlistadoUF" HeaderText="Alistado Und.Físicas" DataField="AlistadoUF"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SEmpaqueUF"
                                                                    SortExpression="EmpaqueUF" HeaderText="Empaque Und.Físicas" DataField="EmpaqueUF"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SCosto"
                                                                    SortExpression="SCosto" HeaderText="Costo" DataField="Costo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="STotal"
                                                                    SortExpression="Total" HeaderText="Total" DataField="Total"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SFechaPedido" Visible="false"
                                                                    SortExpression="FechaPedido"
                                                                    HeaderText="Fecha Pedido"
                                                                    DataField="FechaPedido"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SMarcaModeloVehiculo" Visible="false"
                                                                    SortExpression="MarcaModeloVehiculo" HeaderText="Vehículo" DataField="MarcaModeloVehiculo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="SPlaca" Visible="false"
                                                                    SortExpression="Placa" HeaderText="Placa" DataField="Placa"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                </telerik:GridBoundColumn>--%>
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

        <%--============================================================--%>
        <%--MODAL REPORTE--%>
        <%--============================================================--%>
        <%--<input type="button" value="Imprimir Reporte" onclick="javascript: imprimirDiv('gridTrazabilidadBodega')" />--%>

        <!-- The Modal -->
        <%--        <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Modal Heading</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <button type="button" value="Imprimir Reporte" onclick="javascript: imprimirDiv('myModal')"></button>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        Modal body..
                    </div>

                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>--%>
    </div>
</asp:Content>
