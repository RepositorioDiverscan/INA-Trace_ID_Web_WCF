<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrazabilidadVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.TrazabilidadVisor" %>

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
                                    <telerik:RadTab Text="Trazabilidad" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <h1></h1>
                            <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                <center>
                                    <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                </center>
                            </div>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <asp:Label ID="Label6" runat="server" Text="Artículo    :"></asp:Label>
                                                <asp:DropDownList ID="ddlIDArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                <h1></h1>
                                                <asp:RadioButtonList runat="server" ID="_rblPanelBusqueda" OnSelectedIndexChanged="_rblPanelBusqueda_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True">Fecha Recepción</asp:ListItem>
                                                    <asp:ListItem>Lote</asp:ListItem>
                                                    <asp:ListItem>Fecha Vencimiento</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <h1></h1>
                                                <asp:Panel ID="PanelFechaRecepcion" runat="server" GroupingText="Fecha Recepción">
                                                    <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                    <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                    <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                    <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                    <%--<asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>--%>
                                                    <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click"></asp:Button>
                                                    <h1></h1>
                                                    <asp:Label ID="Label1" runat="server" Text="Lotes:"></asp:Label>
                                                    <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_SelectedIndexChanged"></asp:DropDownList>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelLote" runat="server" GroupingText="Lote" Visible="false">
                                                    <asp:Label ID="LBLLote" runat="server" Text="Lote:"></asp:Label>
                                                    <asp:TextBox ID="txtLote" runat="server"></asp:TextBox>
                                                    <%--<asp:Button ID="btnBuscarLote" runat="server" Text="Buscar" OnClick="btnBuscarLote_Click" OnClientClick="DisplayLoadingImage1()"/>--%>
                                                    <asp:Button ID="btnBuscarLote" runat="server" Text="Buscar" OnClick="btnBuscarLote_Click" />
                                                </asp:Panel>

                                                <asp:Panel ID="PanelFechaVencimiento" runat="server" GroupingText="Fecha Vencimiento" Visible="false">
                                                    <asp:Label ID="LblFechaVencimiento" runat="server" Text="Fecha Vencimiento:"></asp:Label>
                                                    <telerik:RadDatePicker ID="RDPFechaVencimiento" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                    <asp:Button ID="btnFechaVencimiento" runat="server" Text="Buscar" OnClick="btnFechaVencimiento_Click" />
                                                </asp:Panel>
                                                <asp:Button runat="server" ID="btnExportar" Text="Exportar" OnClick="_btnExportar_Click" />

                                                <telerik:RadGrid ID="rdDatosReporte" runat="server" AllowMultiRowSelection="false" PageSize="10"
                                                    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" OnNeedDataSource="rdDatosReporte_NeedDataSource">
                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <MasterTableView>

                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="Etiqueta"
                                                                SortExpression="Etiqueta" HeaderText="Etiqueta" DataField="Etiqueta"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad Medida" DataField="Unidad_Medida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>


                                                        </Columns>

                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                                <asp:Button runat="server" ID="btnExportarDespachos" Text="Exportar Despachos" OnClick="btnExportarDespachos_Click" />
                                                <telerik:RadGrid ID="RadGridDespachos" runat="server" AllowMultiRowSelection="false" PageSize="10"
                                                    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" OnNeedDataSource="RadGridDespachos_NeedDataSource">
                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <MasterTableView>

                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="NombreDespacho"
                                                                SortExpression="NombreDespacho" HeaderText="NombreDespacho" DataField="NombreDespacho"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="FechaDespacho"
                                                                SortExpression="FechaDespacho" HeaderText="Fecha Despacho" DataField="FechaDespacho"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                SortExpression="UnidadMedida" HeaderText="UnidadMedida" DataField="UnidadMedida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>

                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
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
