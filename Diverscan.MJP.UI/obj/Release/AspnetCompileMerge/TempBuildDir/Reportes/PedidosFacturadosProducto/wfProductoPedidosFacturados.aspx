<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="wfProductoPedidosFacturados.aspx.cs" inherits="Diverscan.MJP.UI.Reportes.PedidosFacturadosProducto.wfReporteProductoPedidosFacturados" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">

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
                                    <telerik:RadTab Text="Facturas por Producto" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <%--comienza UpdatePanel--%>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <%--Revisar las imagenes de este div--%>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                        <img id="loading1" src="../../images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>

                                                <table style="border-radius: 10px; border: 1px solid grey; width: 98%; border-collapse: initial; margin-left: 1%; margin-top: 1%;" id="Table2">
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Bodega"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" 
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="SKU Articulo" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIdInterno" runat="server" AutoPostBack="true"
                                                                Class="TexboxNormal" Width="300px" ></asp:TextBox>                                                                                                                                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreArticulo" runat="server" AutoPostBack="true" Class="TexboxNormal"
                                                                Width="300px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>                                                        
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Lote" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLote" runat="server" AutoPostBack="true"
                                                                Class="TexboxNormal" Width="300px" AutoCompleteType="Disabled" />                                                                                                                                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaExp" runat="server" Text="Fecha de Vencimiento:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaExp" class="TexboxNormal" runat="server" AutoPostBack="false" DateInput-DateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>                                                                                                   
                                                    <tr>
                                                        <td></td>
                                                        <td>                                                            
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" Style="margin-left: 25%"
                                                                AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                <asp:Panel runat="server" ID="PedidosFacturados" Visible="false">                                           
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelPedidos" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado de Pedidos Facturados</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <%--Revisar los datos del RadGrid--%>                                                  

                                                <asp:Button runat="server" ID="btnReporte" Text="Reporte" AutoPostBack="false" onclick="btnReporte_Click" 
                                                    PostBackUrl="~/Reportes/PedidosFacturadosProducto/wfProductoPedidosFacturados.aspx" style="margin-bottom:10px"/>

                                                     <telerik:RadGrid ID="RGDMaestrosFacturados" AllowPaging="true" Width="98%"
                                                        OnNeedDataSource="RGDMaestrosFacturados_NeedDataSource" AllowFilteringByColumn="true"
                                                        PagerStyle-AlwaysVisible="true" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false">                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>                                                               
                                                                    <telerik:GridBoundColumn UniqueName="NumeroFactura"
                                                                        SortExpression="NumeroFactura" HeaderText="Numero Factura" DataField="NumeroFactura"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" Visible ="true">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Nombre" 
                                                                        SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdInternoCliente"
                                                                        SortExpression="IdInternoCliente" HeaderText="Código Cliente" DataField="IdInternoCliente"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="NombreCliente"
                                                                        SortExpression="NombreCliente" HeaderText="Nombre Cliente" DataField="NombreCliente"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Cantidad" 
                                                                        SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>                                           
                                                    <br />
                                                </asp:Panel>
                                                <h1></h1>                                              
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
</asp:content>
