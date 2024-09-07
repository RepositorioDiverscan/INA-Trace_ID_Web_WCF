<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_RegistroPromociones.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Promociones.wf_RegistroPromociones" %>

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
        function MuestraMensajeOk() {
            alert("Proceso Terminado exitosamente");
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
                                    <telerik:RadTab Text="Promociones" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                            <h1 class="TituloPanelTitulo">Crear Promociones</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Búsqueda Promoción"></asp:Label>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TxtCodigoPromo" Style="margin-left: 65px;" runat="server" Width="150px" AutoCompleteType="Disabled" Enabled="true"></asp:TextBox>
                                                            <asp:Button ID="Button1" runat="server" Text="Buscar" Style="margin-left: 10px;" Width="133px" OnClientClick="DisplayLoadingImage1()" OnClick="btnBusquedaPromocion_Click" />
                                                        </td>
                                                        <td></td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Promociones"></asp:Label>
                                                            <asp:DropDownList ID="ddPromociones" OnSelectedIndexChanged="ddPromociones_SelectedIndexChanged" Style="margin-left: 111px;" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true"></asp:DropDownList>
                                                            <!--  OnSelectedIndexChanged="ddPromociones_SelectedIndexChanged"-->

                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <asp:Panel runat="server" ID="PanelDatosIngreso" Visible="false">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel6">
                                                        <h1 class="TituloPanelTitulo">Datos Proveedor</h1>
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text="Búsqueda del Proveedor"></asp:Label>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="TxtBusquedaProveedor" Style="margin-left: 51px;" runat="server" Width="150px" AutoCompleteType="Disabled" Enabled="true"></asp:TextBox>
                                                                <asp:Button ID="btnBuscarProveedor" runat="server" Text="Buscar" Style="margin-left: 10px;" Width="133px" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarProveedor_Click" />
                                                            </td>
                                                            <td></td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Text="Proveedor"></asp:Label>
                                                                <asp:DropDownList ID="ddlProveedor" Style="margin-left: 127px;" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true"></asp:DropDownList>
                                                                

                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>



                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel2">
                                                        <h1 class="TituloPanelTitulo">Datos para la Promoción</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar Artículo" Width="150px" Style="margin-left: 15px;" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregarArticulo_Click" />
                                                    <asp:Button ID="btnEditarArticulo" runat="server" Text="Editar Artículo" Width="150px" Style="margin-left: 15px;" OnClientClick="DisplayLoadingImage1()" OnClick="btnEditarArticulo_Click" Visible="false" />
                                                    <asp:Label ID="lblSeparador" runat="server" Text="|||" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnEliminarArticulo" runat="server" Text="Eliminar Artículo" Width="150px" OnClientClick="DisplayLoadingImage1()" Visible="false" OnClick="btnEliminarArticulo_Click" />
                                                    <asp:Label ID="lblSeparador2" runat="server" Text="|||" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnCancelarArticulo" runat="server" Text="Cancelar" Width="150px" OnClientClick="DisplayLoadingImage1()" Visible="false" OnClick="btnCancelarArticulo_Click" />

                                                    <h1></h1>


                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Num.Detalle"></asp:Label>
                                                                <asp:TextBox CssClass="TextBoxBusqueda" Style="margin-left: 115px;" ID="txtIdPreLineaDetalleSolicitud" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <%-- <--%>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Búsqueda del artículo"></asp:Label>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="TxtReferencia" Style="margin-left: 65px;" runat="server" Width="150px" AutoCompleteType="Disabled" Enabled="true"></asp:TextBox>
                                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Style="margin-left: 10px;" Width="133px" OnClientClick="DisplayLoadingImage1()" OnClick="btnBusqueda_Click" />
                                                            </td>
                                                            <td></td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Artículo"></asp:Label>
                                                                <asp:DropDownList ID="ddlidArticuloInterno" Style="margin-left: 141px;" OnSelectedIndexChanged="ddlidArticuloInterno_SelectedIndexChanged" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true"></asp:DropDownList>
                                                                <!--<asp:Label ID="lbUnidadMedida" runat="server" Text="" Visible="false"></asp:Label>-->

                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                     <h1></h1>



                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; margin-top: 6px; margin-bottom: 5px; border-collapse: initial;" id="Table3">

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Presentacion"></asp:Label>
                                                                <asp:DropDownList ID="ddlpresentacion" Style="margin-left: 113px;" Class="TexboxNormal" runat="server" Width="300px"></asp:DropDownList>
                                                                <asp:Label ID="LabelGtin" runat="server" Text="." Visible="false"></asp:Label>

                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="Cantidad Presentacion"></asp:Label>
                                                                <asp:TextBox CssClass="TexboxNormal" Style="margin-left: 62px;" ID="txtCantidad" runat="server" Width="300px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </td>

                                                        </tr>

                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text="Tipo de Articulo"></asp:Label>
                                                                <asp:DropDownList ID="ddlTipo" Style="margin-left: 101px;" Class="TexboxNormal" runat="server" Width="300px"></asp:DropDownList>
                                                            </td>

                                                        </tr>
                                                    </table>


                                                    <h1></h1>
                                                    <asp:Button ID="btnAgregarSolicitud" runat="server" Text="Agregar Promoción" Width="150px" Style="margin-left: 15px;" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregarPromocion_Click" Enabled="false" Visible ="true" />
                                                    <h1></h1>

                                                    <asp:Panel runat="server" Visible="false" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleSolicitud0">
                                                        <h1 class="TituloPanelTitulo">Articulos de la Promoción </h1>
                                                    </asp:Panel>


                                                    <telerik:RadGrid
                                                        ID="RGPromociones"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                        runat="server"
                                                        AllowFilteringByColumn="true"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="10"
                                                        AllowMultiRowSelection="true"
                                                        OnItemCommand="RadGridPromociones_ItemCommand">
                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="IdPreLineaDetalleSolicitud" Visible="true"
                                                                    SortExpression="IdPreLineaDetalleSolicitud" HeaderText="Num Linea" DataField="IdPreLineaDetalleSolicitud"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idArticuloInterno"
                                                                    SortExpression="idArticuloInterno" HeaderText="Identificacion Interna del Articulo" DataField="idArticuloInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticuloInterno"
                                                                    SortExpression="NombreArticuloInterno" HeaderText="Nombre del Articulo" DataField="NombreArticuloInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="GTIN"
                                                                    SortExpression="GTIN" HeaderText="GTIN" DataField="GTIN"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="TipoArticulo"
                                                                    SortExpression="TipoArticulo" HeaderText="Tipo de Articulo" DataField="TipoArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </asp:Panel>

                                                <asp:Panel runat="server" ID="PanelDatoEdicion" Visible="false">
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel3">
                                                        <h1 class="TituloPanelTitulo">Promociones Creadas</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>
                                                    <h1></h1>

                                                    <telerik:RadGrid
                                                        ID="RGMaestroPromociones"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        OnClientClick="DisplayLoadingImage1()"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="10"
                                                        OnItemCommand="RGMaestroPromociones_ItemCommand"
                                                        AllowMultiRowSelection="True">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>
                                                                <%-- %>
                                                                 OnNeedDataSource="RadGrid_NeedDataSource"
                                                                 OnItemCommand="RGAprobarSalida_ItemCommand"
                                                                --%>

                                                                <telerik:GridButtonColumn CommandName="btnVerDetalle" Text="Detalle" UniqueName="btnVerDetalle" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                 <telerik:GridButtonColumn 
                                                                     CommandName="btnEliminar" Text="Eliminar Promoción" UniqueName="btnEliminar" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdMaestroPromocion"
                                                                    SortExpression="IdMaestroPromocion" HeaderText="Numero de Promoción" DataField="IdMaestroPromocion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInternoArticulo"
                                                                    SortExpression="idInternoArticulo" HeaderText="Identificador articulo" DataField="idInternoArticulo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreProveedor"
                                                                    SortExpression="NombreProveedor" HeaderText="Proveedor" DataField="NombreProveedor">
                                                                </telerik:GridBoundColumn>
                                                             

                                                                <telerik:GridBoundColumn UniqueName="fecha"
                                                                    SortExpression="fecha" HeaderText="Fecha Creacion" DataField="fecha"
                                                                    DataFormatString="{0:dd/MM/yyyy}"
                                                                    DataType="System.DateTime">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>

                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <asp:Panel runat="server" ID="PanelModificacionArticulo" Visible="false">

                                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel7">
                                                            <h1 class="TituloPanelTitulo">Edicion de Detalle para la Promoción</h1>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                        </asp:Panel>

                                                        <asp:Button ID="btnEditarPromocion" runat="server" Text="Aceptar" Width="150px" Style="margin-left: 15px;" OnClientClick="DisplayLoadingImage1()" OnClick="btnEditarPromo_Click" />
                                                        <asp:Label ID="Label3" runat="server" Text="|||" Visible="true"></asp:Label>
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnCancelarPromo_Click" />
                                                        <h1></h1>

                                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table6">

                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text="Num.Detalle"></asp:Label>
                                                                    <asp:TextBox CssClass="TextBoxBusqueda" Style="margin-left: 115px;" ID="TxtDetallePromo" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <%-- <--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text="Búsqueda del artículo"></asp:Label>
                                                                    <asp:TextBox CssClass="TexboxNormal" ID="TxtEdicionPromoPresentacion" Style="margin-left: 65px;" runat="server" Width="150px" AutoCompleteType="Disabled" Enabled="true"></asp:TextBox>
                                                                    <asp:Button ID="BtnEdicionPromoBusqueda" runat="server" Text="Buscar" Style="margin-left: 10px;" Width="133px" OnClientClick="DisplayLoadingImage1()" OnClick="btnBusqueda_Click" />
                                                                </td>
                                                                <td></td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="Artículo"></asp:Label>
                                                                    <asp:DropDownList ID="ddlEdicionPromoArticulo" Style="margin-left: 141px;"  Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true"></asp:DropDownList>
                                                                    

                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; margin-top: 6px; margin-bottom: 5px; border-collapse: initial;" id="Table7">

                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="Presentacion"></asp:Label>
                                                                    <asp:DropDownList ID="ddlEdicionPromoPresentacion" Style="margin-left: 113px;" Class="TexboxNormal" runat="server" Width="300px"></asp:DropDownList>
                                                                    <asp:Label ID="Label13" runat="server" Text="." Visible="false"></asp:Label>

                                                                </td>

                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text="Cantidad Presentacion"></asp:Label>
                                                                    <asp:TextBox CssClass="TexboxNormal" Style="margin-left: 62px;" ID="TxtEdicionPromoCantidad" runat="server" Width="300px" AutoCompleteType="Disabled"></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                        </table>


                                                        <h1></h1>

                                                    </asp:Panel>
                                          

                                                <asp:Panel runat="server" ID="PanelDetalleEncabezado" Visible="false">
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel5">
                                                        <h1 class="TituloPanelTitulo">Articulos de la Promoción</h1>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <telerik:RadGrid
                                                        ID="RGDetallePromo"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        OnClientClick="DisplayLoadingImage1()"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="10"
                                                        OnItemCommand="RGDetallePromo_ItemCommand"
                                                        AllowMultiRowSelection="True">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView>

                                                            <Columns>
                                                                <%-- %>
                                                                 OnNeedDataSource="RGDetallePromo_NeedDataSource"
                                                                 OnItemCommand="RGDetallePromo_ItemCommand"
                                                                --%>

                                                                <telerik:GridButtonColumn CommandName="btnEditarPromo" Text="Editar" UniqueName="btnEditarArticuloPromocion" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                <telerik:GridButtonColumn CommandName="btnEliminarPromo" Text="Eliminar" UniqueName="btnEliminaArticuloPromocion" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                <telerik:GridBoundColumn UniqueName="idDetallePromocion"
                                                                    SortExpression="idDetallePromocion" HeaderText="Identificador" DataField="idDetallePromocion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMaestroPromocion"
                                                                    SortExpression="idMaestroPromocion" HeaderText="Numero Promoción" DataField="idMaestroPromocion">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="idInternoSAP"
                                                                    SortExpression="idInternoSAP" HeaderText="Identificador SAP" DataField="idInternoSAP">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInternoArticulo"
                                                                    SortExpression="idInternoArticulo" HeaderText="Identificador Articulo" DataField="idInternoArticulo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre Articulo" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Gtin"
                                                                    SortExpression="Gtin" HeaderText="Gtin" DataField="Gtin">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="TipoArticulo"
                                                                    SortExpression="TipoArticulo" HeaderText="Tipo de Articulo" DataField="TipoArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                            </Columns>
                                                        </MasterTableView>

                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>



                                                </asp:Panel>



                                                </asp:Panel>
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
