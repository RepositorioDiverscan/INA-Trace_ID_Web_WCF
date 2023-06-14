<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_UnidadAlistoDefecto.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_UnidadAlistoDefecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>

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
                                    <telerik:RadTab Text="Unidades Alisto Defecto" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_UnidadAlistoDefecto">
                                            <h1 class="TituloPanelTitulo">Datos de las Unidades Alisto Defecto</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <br>
                                                <asp:Button ID="btnAgregar" runat="server" Text="Guardar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" Enabled="false" />
                                                <%--<asp:Label ID="Label36" runat="server" Text="|||"></asp:Label>--%>
                                                <%--<asp:Button ID ="btnEditar"  runat ="server" Text= "Guardar" Width ="150px"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click" Visible="false"/>--%>
                                                <asp:Label ID="Label35" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()" OnClick="btnLimpiar_Click" />

                                                <h1></h1>

                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Artículos"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidArticuloInterno" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlidArticuloInterno_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="GTIN13"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlGTIN13" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlGTIN13_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="GTIN Defecto"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtGTINDefecto" runat="server" Width="300px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                    <h1 class="TituloPanelTitulo">Artículos Sin Unidades Alisto Defecto</h1>
                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGridArticulosSinUnidadAlisto" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridArticulosSinUnidadAlisto_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="IdERP"
                                                                SortExpression="IdERP" HeaderText="IdERP" DataField="IdERP">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="IdArticulo"
                                                                SortExpression="IdArticulo" HeaderText="IdArticulo" DataField="IdArticulo">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="NombreMaestro"
                                                                SortExpression="NombreMaestro" HeaderText="NombreMaestro" DataField="NombreMaestro">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="GTIN13"
                                                                SortExpression="GTIN13" HeaderText="GTIN13" DataField="GTIN13">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="GTIN13Activo"
                                                                SortExpression="GTIN13Activo" HeaderText="GTIN13Activo" DataField="GTIN13Activo">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="EmpaqueMaestro"
                                                                SortExpression="EmpaqueMaestro" HeaderText="EmpaqueMaestro" DataField="EmpaqueMaestro">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="GTIN14"
                                                                SortExpression="GTIN14" HeaderText="GTIN14" DataField="GTIN14">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="GTIN14Activo"
                                                                SortExpression="GTIN14Activo" HeaderText="GTIN14Activo" DataField="GTIN14Activo">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Empaque"
                                                                SortExpression="Empaque" HeaderText="Empaque" DataField="Empaque">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_UnidadAlisto0">
                                                    <h1 class="TituloPanelTitulo">Listado Unidades Alisto Defecto</h1>
                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGridUnidadAlistoDefecto" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridUnidadAlistoDefecto_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridUnidadAlistoDefecto_ItemCommand">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="idArticulo"
                                                                SortExpression="idArticulo" HeaderText="idArticulo" DataField="idArticulo">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="GTIN"
                                                                SortExpression="GTIN" HeaderText="GTIN" DataField="GTIN">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad_Medida" DataField="Unidad_Medida">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Defecto"
                                                                SortExpression="Defecto" HeaderText="Defecto" DataField="Defecto">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
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
