<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ReporteRecargaGuanacaste.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.Alisto.RecargaGuanacaste.wf_ReporteRecargaGuanacaste" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
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
                                    <telerik:RadTab Text="Recarga Bodega Guanacaste" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--CUERPO-->
                                                <table width="98%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial; margin-left: 1%;margin-top:10px" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:" Style="margin-left: 10px"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false">
                                                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                            <asp:Label ID="Label2" runat="server" Text="|||"></asp:Label>

                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false">
                                                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- OnClick="btnBusqueda_Click"--%>
                                                            <asp:Button runat="server" ID="btnBusqueda" Style="margin-left: 10px" Text="Busqueda" OnClick="btnBusqueda_Click" OnClientClick="DisplayLoadingImage1()" />
                                                            <asp:Button runat="server" ID="btnGenerarReporte" Style="margin-left: 10px" Text="GenerarReporte" OnClick="btnGenerarReporte_Click" PostBackUrl="~/Reportes/Alisto/RecargaGuanacaste/wf_ReporteRecargaGuanacaste.aspx" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <h1></h1>

                                                <asp:Panel runat="server" ID="PanelRecargaGuanacaste" Visible="true">
                                                    <telerik:RadGrid
                                                        ID="RGRecargaGuanacaste"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        Style="margin-left: 1%"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PagerStyle-AlwaysVisible="true"
                                                        OnNeedDataSource="RGRecargaGuanacaste_NeedDataSource"
                                                        AllowMultiRowSelection="true"
                                                        PageSize="50"
                                                        EnableDragToSelectRows="false">
                                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>

                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="NumeroOla" Visible="true"
                                                                    SortExpression="NumeroOla" HeaderText="Número Ola" DataField="NumeroOla">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Fecha"
                                                                    SortExpression="Fecha" HeaderText="Fecha" DataField="Fecha">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Sku"
                                                                    SortExpression="Sku" HeaderText="Sku" DataField="Sku">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre de Artículo" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Unidades"
                                                                    SortExpression="Unidades" HeaderText="Unidades" DataField="Unidades">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <br />

                                                    <br />
                                                </asp:Panel>
                                                <h1></h1>
                                                <!--TERMINA-->
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
        </div>
    </asp:Panel>
</asp:Content>
