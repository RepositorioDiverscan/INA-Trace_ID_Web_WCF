<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_CajaChica.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Ingresos.wf_CajaChica" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
                <telerik:AjaxSetting AjaxControlID="btnBuscar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridOPEINGIngresoCCaRecibir"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridOPEINGIngresoCCaRecibir">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridDetalleIngreso"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddBodega">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddBodega" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
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
                                    <telerik:RadTab Text="Ingreso Caja Chica" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                                <telerik:RadPageView runat="server" ID="RadPageView1">

                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

                                        <%-- Maestro Sin Orden compra --%>
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_OrdenesCompra">
                                            <h1 class="TituloPanelTitulo">Datos Ingreso por Caja Chica</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:Label ID="txtIdCompania" runat="server" Visible="false" Text="Label"></asp:Label>
                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Fecha Inicio de Búsqueda:" Enabled="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaInicioBusqueda" Class="TexboxNormal" runat="server">
                                                        <DateInput DateFormat="dd/MM/yyyy">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label22" runat="server" Text="Fecha Fin de Búsqueda:" Enabled="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaFinBusqueda" Class="TexboxNormal" runat="server">
                                                        <DateInput DateFormat="dd/MM/yyyy">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Número de Transacción:"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtNumeroTransaccion" Class="TexboxNormal" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <%--<asp:Button ID="btn_procesar" runat="server" Text="Procesar" />--%>
                                                </td>
                                            </tr>
                                        </table>

                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleIngresoCC">
                                            <h1 class="TituloPanelTitulo">Listado Ingreso por Caja Chica</h1>
                                        </asp:Panel>


                                        <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridOPEINGIngresoCCaRecibir"
                                            AllowFilteringByColumn="True" AllowSorting="True" Width="100%" HeaderStyle-HorizontalAlign="center"
                                            ShowFooter="True" AllowPaging="True" runat="server"
                                            OnNeedDataSource="RadGridArticulosDisponiblesBodega_NeedDataSource"
                                            DataKeyNames="IdMaestroIngresoCajaChica"
                                            InsertItemPageIndexAction="ShowItemOnFirstPage" 
                                            OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                            PageSize="50" 
                                            PagerStyle-AlwaysVisible="true"
                                            OnItemCommand="RadGrid1_ItemCommand">
                                            

                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                            </ClientSettings>

                                            <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed" BorderWidth="1px" />
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <GroupingSettings CaseSensitive="false" />

                                            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True"
                                                ClientDataKeyNames="IdMaestroIngresoCajaChica">
                                                <Columns>

                                                    <telerik:GridBoundColumn DataField="IdMaestroIngresoCajaChica" HeaderText="ID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NumeroTransaccion" HeaderText="Número Transaccion"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NumeroValeCajaChica" HeaderText="Número Vale Caja Chica"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn FilterControlWidth="120px" DataField="NombreProveedor" HeaderText="Proveedor"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn FilterControlWidth="70px" DataField="FechaCreacion" HeaderText="Fecha creación" DataFormatString="{0:dd/MM/yyyy}">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn FilterControlWidth="120px" DataField="FechaProcesamiento" HeaderText="Fecha procesamiento" DataFormatString="{0:dd/MM/yyyy}">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Usuario" HeaderText="Usuario"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="PorcentajeRecepcion" HeaderText="% Recepcion">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>
                                                    <%--<telerik:GridBoundColumn FilterControlWidth="50px" DataField="NumeroCertificado" HeaderText="Número certificado">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>--%>
                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Estado" HeaderText="Estado"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>

                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>



                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                            <h1 class="TituloPanelTitulo">Listado Detalle Ingreso por Caja Chica</h1>
                                        </asp:Panel>

                                        <%--Detalle Ingreso Caja Chica--%>
                                        <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridDetalleIngreso" HeaderStyle-HorizontalAlign="center"
                                            AllowFilteringByColumn="True" AllowSorting="True" Width="100%"
                                            ShowFooter="True" 
                                            AllowPaging="True" runat="server" 
                                            OnSelectedIndexChanged="RadGridDetalleIngreso_SelectedIndexChanged"
                                            InsertItemPageIndexAction="ShowItemOnFirstPage" 
                                            OnNeedDataSource="RadGridDetalleIngresoCC_NeedDataSource"
                                            OnItemCommand="RadGrid1_ItemCommand"
                                            PagerStyle-AlwaysVisible="true">

                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="false"></Resizing>
                                            </ClientSettings>

                                            <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed" BorderWidth="1px" />
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <GroupingSettings CaseSensitive="false" />



                                            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">

                                                <Columns>

                                                    <telerik:GridBoundColumn DataField="IdArticulo" UniqueName="IdArticulo">
                                                    </telerik:GridBoundColumn>

                                                     <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdArticulo" HeaderText="Id Articulo"
                                                         UniqueName="IdArticulo2">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdInterno" HeaderText="SKU"
                                                        UniqueName="IdInterno">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="70px" DataField="Nombre" HeaderText="Nombre del articulo"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="70px" DataField="numFactura" HeaderText="Número Factura"
                                                        UniqueName="numFactura">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="53px" DataField="GTIN" HeaderText="GTIN"
                                                        UniqueName="GTIN">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadxRecibir" HeaderText="Cantidad por recibir"
                                                        AutoPostBackOnFilter="true">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadTransito" HeaderText="Cantidad Transito">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadRecibidos" HeaderText="Cantidad recibida">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadPendientes" HeaderText="Cantidad Pendientes">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="PorcentajeCumplimiento" HeaderText="% Recepcion">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadBodega" HeaderText="Cantidad Bodega">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                    </telerik:GridBoundColumn>

                                                </Columns>
                                            </MasterTableView>

                                        </telerik:RadGrid>

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