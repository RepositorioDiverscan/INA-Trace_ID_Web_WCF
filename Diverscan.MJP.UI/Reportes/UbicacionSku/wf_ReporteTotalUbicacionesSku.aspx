<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ReporteTotalUbicacionesSku.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.UbicacionSku.wf_ReporteTotalUbicacionesSku" %>
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
                                    <telerik:RadTab Text="Total Ubicaciones Sku" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <%--<asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                            <h1 class="TituloPanelTitulo" style="margin-left:20px!important">Filtrado</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>--%>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--CUERPO-->

                                                <table width="98%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial; margin-left: 1%;margin-top:10px" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Zonas" Style="margin-left: 10px"></asp:Label>
                                                            <asp:DropDownList ID="ddlZonas" Style="margin-left: 35px;"
                                                                Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlZonas_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <h1></h1>
                                                            <asp:Label ID="Label2" runat="server" Text="Ubicación" Style="margin-left: 10px"></asp:Label>
                                                            <asp:DropDownList ID="ddlUbicacion" Style="margin-left: 15px;"
                                                                Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- OnClick="btnBusqueda_Click"--%>
                                                             <asp:Label ID="Label1" runat="server" Text="IdInterno" Style="margin-left: 10px; margin-top:20px; margin-bottom:20px"></asp:Label>
                                                            <asp:TextBox ID="txtSKU" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="200px" Style="margin-left: 20px;"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBusqueda" Style="margin-left: 10px;margin-top:10px" Text="Busqueda" OnClick="btnBusqueda_Click" OnClientClick="DisplayLoadingImage1()" />
                                                            <asp:Button runat="server" ID="btnGenerarReporte" Style="margin-left: 10px" Text="GenerarReporte" OnClick="btnGenerarReporte_Click" PostBackUrl="~/Reportes/UbicacionSku/wf_ReporteTotalUbicacionesSku.aspx" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <h1></h1>

                                                <asp:Panel runat="server" ID="PanelInventario" Visible="true">
                                                    <telerik:RadGrid
                                                        ID="RGInventario"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        Style="margin-left: 1%"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PagerStyle-AlwaysVisible="true"
                                                        OnNeedDataSource="RGInventario_NeedDataSource"
                                                        AllowMultiRowSelection="true"
                                                        PageSize="50"
                                                        EnableDragToSelectRows="false">
                                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>

                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="IdInterno" Visible="true"
                                                                    SortExpression="IdInterno" HeaderText="Sku" DataField="IdInterno">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre de Artículo" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Ubicación" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadDisponible"
                                                                    SortExpression="CantidadDisponible" HeaderText="Cantidad Disponible" DataField="CantidadDisponible">
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
