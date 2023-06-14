<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoDemandaProveedor.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.HistoricoDemandaProveedor" %>

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
                                    <telerik:RadTab Text="Histórico Demanda Proveedor" Width="200px"></telerik:RadTab>
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
                                                      <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                <asp:Label ID="Label6" runat="server" Text="Proveedor :"></asp:Label>
                                                <asp:DropDownList ID="ddlProveedor" Class="TexboxNormal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
                                                <h1></h1>
                                                <asp:Label ID="Label1" runat="server" Text="Artículos:" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlIdInternoArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false" Visible="false"></asp:DropDownList>
                                                <h1></h1>
                                                <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                <h1></h1>
                                                <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                <asp:Button runat="server" ID="btnExportar" Text="Exportar" OnClick="btnExportar_Click"></asp:Button>

                                                <telerik:RadGrid ID="RadGridDemanda" runat="server" AllowMultiRowSelection="false" PageSize="10"
                                                    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" OnNeedDataSource="RadGridDemanda_NeedDataSource">
                                                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="false"></Selecting>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                    </ClientSettings>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="IdInternoArticulo"
                                                                SortExpression="IdInternoArticulo" HeaderText="ID Articulo ERP" DataField="IdInternoArticulo">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Articulo" DataField="NombreArticulo">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad Medida" DataField="Unidad_Medida">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
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
</asp:Content>
