<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_OrdenesCompras.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Ingresos.wf_OrdenesCompras" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
                                                        <td><asp:Label ID="Label2" runat="server" Text="Número de order de compra:"></asp:Label> </td>
                                                        <td><asp:TextBox ID="txtNumeroOrdenCompra"  Class="TexboxNormal" runat="server"></asp:TextBox> </td>
                                                    </tr>                                                  
                                                     <tr>
                                                        <td><asp:Label ID="Label1" runat="server" Text="Bodega:"></asp:Label> </td>
                                                        <td>  <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true"></asp:DropDownList> </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_onClick"  Text="Buscar" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                           <asp:Button ID="btn_procesar" runat="server" Text="Procesar" OnClientClick = "return confirm ('¿Está seguro?');"  OnClick="BtnProcesar_onClick" />
                                                        </td>

                                                        <td>
                                                             <asp:Button ID="btnGuardarReporte" runat="server" Text="Guardar Reporte"  OnClick="BtnGuardarReporte_onClick" PostBackUrl="~/Operaciones/Ingresos/wf_OrdenesCompras.aspx"/>
                                                        </td>
                                                    </tr>   
                                                </table>
                                          
                                                <%--
                                                       OnClick ="btnAgregar2_Click" 
                                                         OnClick ="btnEditar2_Click"
                                                         OnClick ="BtnElimina_Click" OnClick ="Btnlimpiar2_Click"
                                                --%>
                                             
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleOrdenCompra">
                                                    <h1 class="TituloPanelTitulo">Listado Orden Compra</h1>
                                                </asp:Panel>

                                                <%-- OnNeedDataSource="RadGridOPEINGDetalleordencompra_NeedDataSource"
                                                    OnItemCommand="RadGridOPEINGDetalleOrdenCompra_ItemCommand"--%>
                                
                             
                                        <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridOPEINGOrdenesDeCompraARecibir" 
                                            AllowFilteringByColumn="True" AllowSorting="True" Width="100%"
                                            ShowFooter="True" AllowPaging="True" runat="server"  
                                            OnNeedDataSource="RadGridArticulosDisponiblesBodega_NeedDataSource"
                                            OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" DataKeyNames="idMaestroOrdenCompra" 
                                            InsertItemPageIndexAction="ShowItemOnFirstPage" OnItemCommand="RadGrid1_ItemCommand" PageSize="10">
                                             <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                                                        <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                                                    </ClientSettings>
                                            <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed"
                                      BorderWidth="1px" />
                                                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                                    <GroupingSettings CaseSensitive="false" />
               
                                            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True"
                                                ClientDataKeyNames="idMaestroOrdenCompra">                 
                                                <Columns>
                     
                                                     <telerik:GridBoundColumn DataField="idMaestroOrdenCompra" HeaderText="Número de orden"
                                                     UniqueName="idMaestroOrdenCompra">  
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn DataField="idInterno" HeaderText="Número de orden" >  
                                                     </telerik:GridBoundColumn>                       
                                                     <telerik:GridBoundColumn FilterControlWidth="120px" DataField="NombreProveedor" HeaderText="Proveedor"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" >
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn FilterControlWidth="50px" DataField="FechaCreacion" HeaderText="Fecha creación" DataFormatString="{0:dd/MM/yyyy}" >
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn FilterControlWidth="50px" DataField="FechaProcesamiento" HeaderText="Fecha procesamiento" DataFormatString="{0:dd/MM/yyyy}">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                         <telerik:GridBoundColumn FilterControlWidth="50px" DataField="NumeroFactura" HeaderText="Número factura">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn FilterControlWidth="50px" DataField="PorcentajeRecepcion" HeaderText="% Recepcion">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn FilterControlWidth="50px" DataField="NumeroCertificado" HeaderText="Número certificado">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                     </telerik:GridBoundColumn>
                                                     <telerik:GridCheckBoxColumn UniqueName="GridCheckBoxColumn" HeaderText="Procesada" DataField="Procesada">
                                                     </telerik:GridCheckBoxColumn> 
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                    </asp:RadUpdatePanel>        

                                                     <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel2">
                                                    <h1 class="TituloPanelTitulo">Listado Detalle Orden Compra</h1>
                                                        <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
                                                </asp:Panel>



 <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridDetalleOrden" 
                AllowFilteringByColumn="True" AllowSorting="True" Width="100%"
                ShowFooter="True" AllowPaging="True" runat="server"  OnSelectedIndexChanged="RadGridDetalleOrden_SelectedIndexChanged"
                InsertItemPageIndexAction="ShowItemOnFirstPage" OnItemCommand="RadGrid1_ItemCommand"
                OnNeedDataSource="RadGridDetalleOrdenCompra_NeedDataSource">
                 <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed"
                                   BorderWidth="1px" />
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
               
                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
                 
                    <Columns>

                       <telerik:GridBoundColumn DataField="IdArticulo" HeaderText="Id Articulo"
                         UniqueName="IdArticulo">  
                        </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn DataField="IdInterno" HeaderText="SKU"
                         UniqueName="IdInterno">  
                        </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn DataField="GTIN" HeaderText="GTIN"
                         UniqueName="GTIN">  
                        </telerik:GridBoundColumn>
           
                        <telerik:GridBoundColumn FilterControlWidth="120px" DataField="Nombre" HeaderText="Nombre del articulo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                        </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn FilterControlWidth="120px" DataField="CantidadxRecibir" HeaderText="Cantidad por recibir"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
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

                         <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadRechazados" HeaderText="Cantidad rechazada">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="DescripcionRechazo" HeaderText="Descripcion Rechazo">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

                                       
                 
<asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel3">
                                                    <h1 class="TituloPanelTitulo">Listado detalle rechazo articulo orden compra</h1>
                                                </asp:Panel>


                                                <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridDetalleRechazo" 
                AllowFilteringByColumn="True" AllowSorting="True" Width="100%"  ShowFooter="True" AllowPaging="True" runat="server"   
                InsertItemPageIndexAction="ShowItemOnFirstPage"  OnNeedDataSource="RadGridDetalleRechazo_NeedDataSource">

                 <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed"
                                   BorderWidth="1px" />
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
               
                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
                 
                    <Columns>
                    
                        <telerik:GridBoundColumn FilterControlWidth="120px" DataField="Nombre" HeaderText="Nombre del articulo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="120px" DataField="CantidadRechazados" HeaderText="Cantidad productos rechazados"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                        </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn FilterControlWidth="120px" DataField="DescripcionRechazo" HeaderText="Detalle de rechazo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" >
                        </telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>




                                          <%--  </ContentTemplate>
                                                                              
                                        </asp:UpdatePanel>--%>

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
