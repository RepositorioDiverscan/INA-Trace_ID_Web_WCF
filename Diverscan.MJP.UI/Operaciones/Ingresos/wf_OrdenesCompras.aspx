<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="wf_OrdenesCompras.aspx.cs" inherits="Diverscan.MJP.UI.Operaciones.Ingresos.wf_OrdenesCompras" %>


<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


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



        <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnBuscar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridOPEINGOrdenesDeCompraARecibir"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridOPEINGOrdenesDeCompraARecibir">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridDetalleOrden"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridDetalleOrden">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridDetalleRechazo"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddBodega">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddBodega"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:radajaxmanager>

        <div id="RestrictionZoneID" class="WindowContenedor">

            <telerik:radwindowmanager rendermode="Lightweight" offsetelementid="offsetElement" id="RadWindowManager1" runat="server" enableshadow="true">
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
                                    <telerik:RadTab Text="Ordenes de Compras" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                                                  
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

             <%-- Maestro Orden compra --%>
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_OrdenesCompra">
                                            <h1 class="TituloPanelTitulo">Datos orden de compra</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                                                                                                                   
                                                <asp:Label ID="txtIdCompania" runat="server" Visible="false" Text="Label"></asp:Label>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Fecha Inicio de Búsqueda:" Enabled="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtFechaInicioBusqueda"  Class="TexboxNormal" runat="server">
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
                                                            <telerik:RadDatePicker ID="txtFechaFinBusqueda"  Class="TexboxNormal" runat="server">
                                                               <DateInput DateFormat="dd/MM/yyyy"> 
                                                               </DateInput> 
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label2" runat="server" Text="Número de Transacción:"></asp:Label> </td>
                                                        <td><asp:TextBox ID="txtNumeroOrdenCompra"  Class="TexboxNormal" runat="server"></asp:TextBox> </td>
                                                    </tr>                                                  
                                                     <tr>
                                                 <%--  <td><asp:Label ID="Label1" runat="server" Text="Bodega:"></asp:Label> </td>
                                                       <td><asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true"></asp:DropDownList> </td> 
                                                       <td><asp:Label ID="lblIdBodega" runat="server" BorderStyle="Solid" BackColor="Black" ForeColor="White"></asp:Label></td>  --%>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_onClick"  Text="Buscar" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
<%--                                                           <asp:Button ID="btn_procesar" runat="server" Text="Procesar" OnClientClick = "return confirm ('¿Seguro(a) de Procesar Orden de Compra?');"  OnClick="BtnProcesar_onClick" />--%>
                                                        </td>
                                                    </tr>   
                                                </table>
                                             
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleOrdenCompra">
                                                    <h1 class="TituloPanelTitulo">Listado Orden Compra</h1>
                                                </asp:Panel>
                             
                                        <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridOPEINGOrdenesDeCompraARecibir" 
                                            AllowFilteringByColumn="True" AllowSorting="True" Width="100%" HeaderStyle-HorizontalAlign="center" 
                                            ShowFooter="True" AllowPaging="True" runat="server"  
                                            OnNeedDataSource="RadGridArticulosDisponiblesBodega_NeedDataSource"
                                            OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" 
                                            DataKeyNames="idMaestroOrdenCompra"
                                            InsertItemPageIndexAction="ShowItemOnFirstPage" OnItemCommand="RadGrid1_ItemCommand" PageSize="50" PagerStyle-AlwaysVisible="true">

                                             <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                            </ClientSettings>
                                            <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed" BorderWidth="1px" />
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                <GroupingSettings CaseSensitive="false" />
               
                                            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True"
                                                ClientDataKeyNames="idMaestroOrdenCompra">                 
                                                <Columns>

                                                    <telerik:GridBoundColumn DataField="idMaestroOrdenCompra" HeaderText="ID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NumeroTransaccion" HeaderText="Número Transaccion" 
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">  
                                                     </telerik:GridBoundColumn>                  
                                                     <telerik:GridBoundColumn FilterControlWidth="120px" DataField="NombreProveedor" HeaderText="Proveedor"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" >
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn FilterControlWidth="70px" DataField="FechaCreacion" HeaderText="Fecha creación" DataFormatString="{0:dd/MM/yyyy}" >
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn FilterControlWidth="120px" DataField="FechaProcesamiento" HeaderText="Fecha procesamiento" DataFormatString="{0:dd/MM/yyyy}">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NombreUsuario" HeaderText="Usuario"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn FilterControlWidth="50px" DataField="PorcentajeRecepcion" HeaderText="% Recepcion">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
<%--                                                     <telerik:GridBoundColumn FilterControlWidth="50px" DataField="NumeroCertificado" HeaderText="Número certificado">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>--%>
                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Estado" HeaderText="Estado"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    </telerik:GridBoundColumn>

                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                    </asp:RadUpdatePanel>        

                                                     <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                    <h1 class="TituloPanelTitulo">Listado Detalle Orden Compra</h1>
                                                        <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
                                                </asp:Panel>

<%-- Detalle OC --%>

 <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridDetalleOrden" HeaderStyle-HorizontalAlign="center" 
                AllowFilteringByColumn="True" AllowSorting="True" Width="100%" 
                ShowFooter="True" AllowPaging="True" runat="server"  OnSelectedIndexChanged="RadGridDetalleOrden_SelectedIndexChanged"
                InsertItemPageIndexAction="ShowItemOnFirstPage" OnItemCommand="RadGrid1_ItemCommand"
                OnNeedDataSource="RadGridDetalleOrdenCompra_NeedDataSource" PagerStyle-AlwaysVisible="true">
                 <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true"></Selecting>
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" ></Scrolling>  <%--SaveScrollPosition="true" FrozenColumnsCount="2" --%>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="false" ></Resizing>
                </ClientSettings>
                <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed" BorderWidth="1px" />
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <GroupingSettings CaseSensitive="false" />
               
                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">

                    <columns>

                        <telerik:GridBoundColumn DataField="IdArticulo" UniqueName="IdArticulo">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdArticulo" HeaderText="Id Articulo"
                            UniqueName="IdArticulo2">
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdInterno" HeaderText="SKU"
                            UniqueName="IdInterno">
                            <footerstyle font-bold="true"></footerstyle>
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
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadRecibidos" HeaderText="Cantidad recibida">
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadPendientes" HeaderText="Cantidad Pendientes">
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="PorcentajeCumplimiento" HeaderText="% Recepcion">
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadBodega" HeaderText="Cantidad Bodega">
                            <footerstyle font-bold="true"></footerstyle>
                        </telerik:GridBoundColumn>

                    </columns>
                </MasterTableView>
            </telerik:RadGrid>

    </asp:Panel>
    </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </ContentTemplate>
                        <shortcuts>
                            <telerik:WindowShortcut CommandName="Maximize" Shortcut="Ctrl+F6"></telerik:WindowShortcut>
                            <telerik:WindowShortcut CommandName="Minimize" Shortcut="Ctrl+F7"></telerik:WindowShortcut>
                            <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                            <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                            <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                        </shortcuts>

    </telerik:RadWindow>

                </Windows>
            </telerik:RadWindowManager>

        </div>

    </asp:Panel>
</asp:Content>
