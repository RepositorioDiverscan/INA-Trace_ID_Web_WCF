<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_MaestroUbicaciones.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_MaestroUbicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
        function DisplayLoadingImage4() {
            document.getElementById("loading4").style.display = "block";
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
                                    <telerik:RadTab Text="Almacén" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Bodega" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Zona " Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Ubicación " Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <%-- Tab Almacen --%>
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Almacen">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Almacén</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />

                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchAlmacen" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>

                                                            <asp:Button runat="server" ID="btnBuscarAlmacen" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarAlmacen_Click" />

                                                        </td>
                                                    </tr>
                                                </table>

                                                <br>

                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                <%--<asp:Label ID="Label21" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnEditar_Click" Visible="false" />
                                                <asp:Label ID="Label20" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnlimpiarAlmacen" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()" OnClick="BtnlimpiarAlmacen_Click" />


                                                <h1></h1>

                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                     <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif"" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="ID Almacén"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidAlmacen" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />--%>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label40" runat="server" Text="Compañia"></asp:Label>
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidCompania" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>
                                                    </tr>
                                                    <tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Abreviatura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtAbreviatura" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtnombre" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Descripción"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtdescripcion" runat="server" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                </table>

                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_Almacen0">
                                                    <h1 class="TituloPanelTitulo">Almacenes</h1>

                                                </asp:Panel>

                                                <telerik:RadGrid ID="RadGridAlmacen" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridAlmacen_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridAlmacen_ItemCommand">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="idAlmacen"
                                                                SortExpression="idAlmacen" HeaderText="Id Almacen" DataField="idAlmacen">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="nombre"
                                                                SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="descripción"
                                                                SortExpression="descripcion" HeaderText="Descripción" DataField="descripcion">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idCompania"
                                                                SortExpression="idCompania" HeaderText="Id Compania" DataField="idCompania">
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

                                <%-- Tab Bodega --%>
                                <telerik:RadPageView runat="server" ID="RadPageView2">
                                    <asp:Panel ID="Panel2" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Bodega0">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Bodega</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />

                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table6">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label41" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchBodega" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>

                                                            <asp:Button runat="server" ID="btnBuscarBodega" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarBodega_Click" />

                                                        </td>
                                                    </tr>
                                                </table>

                                                <br>

                                                <asp:Button ID="btnAgregarBodega" runat="server" Text="Agregar" Width="150px" OnClientClick="DisplayLoadingImage2()" OnClick="btnAgregar2_Click" />
                                                <%--<asp:Label ID="Label34" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button ID="btnEditarBodega" runat="server" Text="Editar" Width="150px" OnClientClick="DisplayLoadingImage2()" OnClick="btnEditar2_Click" Visible="false" />
                                                <asp:Label ID="Label33" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Btnlimpiar2" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage2()" OnClick="Btnlimpiar2_Click" />

                                                <h1></h1>

                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                     <img id="loading2" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>


                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="ID Bodega"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidBodega" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="Button1" Text="Buscar"  OnClientClick="DisplayLoadingImage2()" OnClick="btnBuscar_Click"  />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Abreviatura"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtAbreviatura0" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtnombre0" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Descripción"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtdescripcion0" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Almacen"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidAlmacen" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Secuencia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtsecuencia" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        </td>

                                                    </tr>

                                                </table>
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_Bodega">
                                                    <h1 class="TituloPanelTitulo">Bodegas</h1>
                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGridBodega" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridBodega_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridBodega_ItemCommand">
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="idBodega"
                                                                SortExpression="idBodega" HeaderText="Id Bodega" DataField="idBodega">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idAlmacen" Display="false"
                                                                SortExpression="idAlmacen" HeaderText="Id Almacen" DataField="idAlmacen">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreAlmacen"
                                                                SortExpression="NombreAlmacen" HeaderText="Almacen" DataField="NombreAlmacen">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="nombre"
                                                                SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="descripcion"
                                                                SortExpression="descripcion" HeaderText="Descripcion" DataField="descripcion">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="secuencia"
                                                                SortExpression="secuencia" HeaderText="Secuencia" DataField="secuencia">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>

                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </asp:Panel>
                                </telerik:RadPageView>

                                <%-- Tab Zona --%>
                                <telerik:RadPageView runat="server" ID="RadPageView4">
                                    <asp:Panel ID="Panel5" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Zonas">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Zonas</h1>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />

                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                            <ContentTemplate>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table7">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label42" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchZona" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>

                                                            <asp:Button runat="server" ID="btnBuscarZona" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarZona_Click" />

                                                        </td>
                                                    </tr>
                                                </table>

                                                <br>

                                                <asp:Button ID="btnAgregarZona" runat="server" Text="Agregar" Width="150px" OnClientClick="DisplayLoadingImage4()" OnClick="btnAgregar4_Click" />
                                                <%--<asp:Label ID="Label38" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button ID="btnEditarZona" runat="server" Text="Editar" Width="150px" OnClientClick="DisplayLoadingImage4()" OnClick="btnEditar4_Click" Visible="false" />
                                                <asp:Label ID="Label37" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Btnlimpiar4" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage4()" OnClick="Btnlimpiar4_Click" />

                                                <h1></h1>

                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                     <img id="loading4" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif"" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4">

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="ID Zona"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidZona" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="Button9" Text="Buscar" OnClientClick="DisplayLoadingImage4()" OnClick="btnBuscar_Click"  />--%>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label27" runat="server" Text="Abreviatura"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtAbreviatura00" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label29" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtnombre00" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label30" runat="server" Text="Descripción"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtdescripcion00" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="Label32" runat="server" Text="Secuencia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtsecuencia00" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>

                                                    </tr>

                                                </table>

                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_Zonas0">
                                                    <h1 class="TituloPanelTitulo">Zonas</h1>

                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGridZona" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridZona_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridZona_ItemCommand">
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="idZona"
                                                                SortExpression="idZona" HeaderText="Id Zona" DataField="idZona">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Nombre"
                                                                SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                SortExpression="descripcion" HeaderText="Descripcion" DataField="descripcion">
                                                            </telerik:GridBoundColumn>

                                                            <%--       <telerik:GridBoundColumn UniqueName="secuencia"
                                                                    SortExpression="secuencia" HeaderText="Secuencia" DataField="secuencia">
                                                                </telerik:GridBoundColumn> --%>
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

                                <%-- Tab Ubicacion --%>
                                <telerik:RadPageView runat="server" ID="RadPageView3">
                                    <asp:Panel ID="Panel3" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroUbicaciones">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Ubicación</h1>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />

                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table8">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchUbicacion" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                            
                                                            <%--Boton buscar Ubicaciones--%>
                                                            <asp:Button runat="server" ID="btnBuscarUbicacion" Text="Buscar" AutoPostBack="false" OnClientClick="buscarUbicacion()" OnClick="btnBuscarUbicacion_Click" />

                                                        </td>
                                                    </tr>
                                                </table>

                                                <br>

                                                <asp:Button ID="btnAgregarUbicacion" runat="server" Text="Agregar" Width="150px" OnClientClick="DisplayLoadingImage3()" OnClick="btnAgregar3_Click" />
                                                <%--<asp:Label ID="Label36" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button ID="btnEditarUbicacion" runat="server" Text="Editar" Width="150px" OnClientClick="DisplayLoadingImage3()" OnClick="btnEditar3_Click" Visible="false" />
                                                <asp:Label ID="Label35" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Btnlimpiar3" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage3()" OnClick="Btnlimpiar3_Click" />

                                                <h1></h1>

                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                     <img id="loading3" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif"" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Ubicación"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidUbicacion" runat="server" Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="Button4" Text="Buscar"  OnClientClick="DisplayLoadingImage3()" OnClick="btnBuscar_Click"  />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Bodega"></asp:Label>
                                                            </t>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidBodega" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="Dd_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Zona"></asp:Label>
                                                            </t>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidZona" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true" MaxLength="3" OnSelectedIndexChanged="Dd_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Estante"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtestante" runat="server" Width="300px" MaxLength="3" placeholder="Ejemplo: 000" AutoPostBack="True" OnTextChanged="TxtMinValid_TextChanged"></asp:TextBox>
                                                           <%-- oninput="javascript: if (this.value.length> this.maxLength) this.value = this.value.slice (0, this.maxLength);"></asp:TextBox>   --%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="Columna"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtcolumna" runat="server" Width="300px" Visible="True" MaxLength="3" type="number" min="0" AutoPostBack="True" OnTextChanged="TxtMinValid_TextChanged" placeholder="Ejemplo: 000"></asp:TextBox>
                                                         <%--        oninput="javascript: if (this.value.length> this.maxLength) this.value = this.value.slice (0, this.maxLength);" onkeydown="return onlyNos(event)" placeholder="Ejemplo: 000"></asp:TextBox>  --%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Nivel"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtnivel" runat="server" Width="300px" MaxLength="3" min="0" type="number" AutoPostBack="True" OnTextChanged="TxtMinValid_TextChanged" placeholder="Ejemplo: 000"></asp:TextBox>
                                                          <%--  oninput="javascript: if (this.value.length> this.maxLength) this.value = this.value.slice (0, this.maxLength);" onkeydown="return onlyNos(event)" placeholder="Ejemplo: 000"></asp:TextBox>  --%>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Posición"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtpos" runat="server" Width="300px" Visible="True" AutoPostBack="True" OnTextChanged="TxtMinValid_TextChanged" placeholder="Ejemplo: 000"></asp:TextBox>
                                                         <%-- type="number" min="0" oninput="javascript: if (this.value.length> this.maxLength) this.value = this.value.slice (0, this.maxLength);" MaxLength="3" onkeydown="return onlyNos(event)" --%>    

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Text="Largo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtlargo" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                <td>
                                                     <asp:Label ID="Label23" runat="server" Text="Area Ancho"></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:TextBox CssClass="TexboxNormal" ID="txtareaAncho" runat="server" Width="300px" visible ="True"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label24" runat="server" Text="Alto"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtalto" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Text="Cara"></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:TextBox CssClass="TexboxNormal" ID="txtcara" runat="server" Width="300px" visible ="True"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label26" runat="server" Text="Profundidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtprofundidad" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" Text="Capacidad Peso Kilos"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCapacidadPesoKilos" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label44" runat="server" Text="Capacidad Volumen M3"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCapacidadVolumenM3" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label28" runat="server" Text="Descripción"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtdescripcion000" runat="server" Width="300px" Visible="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" Text="Secuencia" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtSecuencia000" Class="TexboxNormal" Width="300px" value="1" Visible="false"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroUbicaciones0">
                                                    <h1 class="TituloPanelTitulo">Listado Ubicaciones</h1>

                                                <%--Boton Exportar Excel--%>                                                
                                                <button onclick="getExcel()" type="button" class="btn btn-success mb-3" style="font-size: 15px !important; margin-top:12px; float:right; margin-right:10px">
                                                <i class="fa-solid fa-file-excel mr-1" style="font-size: 15px !important;"></i> Exportar Excel
                                                </button>

                                                </asp:Panel>

                                                <%--RadGrid Ubicaciones--%>
                                                <telerik:RadGrid ID="RadGridUbicacion" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()" OnNeedDataSource="RadGridUbicacion_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridUbicacion_ItemCommand">

                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="idUbicacion"
                                                                SortExpression="idUbicacion" HeaderText="Id Ubicacion" DataField="idUbicacion">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idBodega" Display="false"
                                                                SortExpression="idBodega" HeaderText="Id Bodega" DataField="idBodega">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreBodega"
                                                                SortExpression="NombreBodega" HeaderText="Nombre Bodega" DataField="Nombre de la Bodega">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idZona" Display="false"
                                                                SortExpression="idZona" HeaderText="ID Zona" DataField="idZona">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreZona"
                                                                SortExpression="NombreZona" HeaderText="Nombre Zona" DataField="Nombre de Zona">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="estante"
                                                                SortExpression="estante" HeaderText="Estante" DataField="Estante">
                                                            </telerik:GridBoundColumn>
                                                            
                                                            <telerik:GridBoundColumn UniqueName="columna"
                                                                SortExpression="columna" HeaderText="Columna" DataField="Columna">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="nivel"
                                                                SortExpression="nivel" HeaderText="Nivel" DataField="Nivel">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="pos"
                                                                SortExpression="pos" HeaderText="Posición" DataField="Posición">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="largo"
                                                                SortExpression="largo" HeaderText="Largo" DataField="Largo">
                                                            </telerik:GridBoundColumn>

                                                            <%--   <telerik:GridBoundColumn UniqueName="areaAncho"
                                                                    SortExpression="areaAncho" HeaderText="Area Ancho" DataField="areaAncho">
                                                                </telerik:GridBoundColumn>--%>

                                                            <telerik:GridBoundColumn UniqueName="alto"
                                                                SortExpression="alto" HeaderText="Alto" DataField="Alto">
                                                            </telerik:GridBoundColumn>

                                                            <%--<telerik:GridBoundColumn UniqueName="areaAncho"
                                                                <telerik:GridBoundColumn UniqueName="cara"
                                                                    SortExpression="cara" HeaderText="Cara" DataField="cara">
                                                                </telerik:GridBoundColumn>--%>

                                                            <telerik:GridBoundColumn UniqueName="profundidad"
                                                                SortExpression="profundidad" HeaderText="Profundidad" DataField="Profundidad">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="CapacidadPesoKilos"
                                                                SortExpression="CapacidadPesoKilos" HeaderText="Capacidad Peso Kilos" DataField="Capacidad de Peso en Kilos">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="CapacidadVolumenM3"
                                                                SortExpression="CapacidadVolumenM3" HeaderText="Capacidad Volumen M3" DataField="Capacidad de Volumen">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                SortExpression="Descripcion" HeaderText="Descripción" DataField="Descripcion">
                                                            </telerik:GridBoundColumn>

                                                            <%--<telerik:GridBoundColumn UniqueName="Secuencia"
                                                                    SortExpression="Secuencia" HeaderText="Secuencia" DataField="Secuencia">
                                                                </telerik:GridBoundColumn>--%>
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


    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
    <script type="text/jscript" src="../../Scripts/jsNotifications/jquery-1.7.2.min.js"></script>
    <script type="text/jscript" src="Scripts.js"></script>

</asp:Content>


