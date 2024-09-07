<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="wfTrazabilidadPorPerdido.aspx.cs" inherits="Diverscan.MJP.UI.Reportes.ProductosPedidoFacturado.wfTrazabilidadPorPerdido" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>

    <asp:Panel ID="Panel4" runat="server">
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
                                    <telerik:RadTab Text="Detalle Producto por Factura" Width="200px"></telerik:RadTab>
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
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" class="TexboxNormal" runat="server" AutoPostBack="false" DateInput-DateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        
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

                                                <asp:Panel runat="server" ID="Olas" Visible="false">   
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel2" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado de Olas</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                         <br /> 
                                                <telerik:RadGrid ID="RGMaestroSolicitud" AllowPaging="True" Width="100%" 
                                                    OnNeedDataSource="RGMaestroSolicitud_NeedDataSource" OnItemCommand="RGMaestroSolicitud_ItemCommand"
                                                    OnClientClick="DisplayLoadingImage1()" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="True" PageSize="50" AllowPading="True" PagerStyle-AlwaysVisible="true">
                                                    <GroupingSettings CaseSensitive="true" />
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridButtonColumn CommandName="btnVerDetalle" Text="Detalle" UniqueName="btnVerDetalle" HeaderText="">
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridBoundColumn UniqueName="IdMaestroSolicitud"
                                                                SortExpression="IdMaestroSolicitud" HeaderText="Solicitud #" DataField="IdMaestroSolicitud">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="Nombre"
                                                                SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                SortExpression="IdInterno" HeaderText="Id Interno" DataField="IdInterno">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                SortExpression="Fecha" HeaderText="Fecha Solicitud" DataField="FechaCreacion"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="Nombre"
                                                                SortExpression="Nombre" HeaderText="Destino" DataField="Nombre">
                                                            </telerik:GridBoundColumn>                                                          
                                                             <telerik:GridBoundColumn UniqueName="FechaEntrega"
                                                                SortExpression="FechaEntrega" HeaderText="Fecha entrega" DataField="FechaEntrega"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>                                                          
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="false"><Selecting AllowRowSelect="true"></Selecting></ClientSettings>
                                                </telerik:RadGrid>
                                                <br />
                                                    </asp:Panel>
                                                   <asp:Panel runat="server" ID="Pedidos" Visible="false">   
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelPedidos" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado de Pedidos Facturados por Ola</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>                                                  
                                                
                                                     <asp:Button runat="server" ID="btnReporteTrazabilidad" Text="Generar PDF" onclick="btnReporteTrazabilidad_Click" 
                                                         PostBackUrl="~/Reportes/ProductosPedidoFacturado/wfTrazabilidadPorPerdido.aspx" />

                                                     <%--Grid nuevo --%>
                                               
                                                 <telerik:RadGrid RenderMode="Lightweight" ID="RGArticulosPedido" runat="server" ShowStatusBar="true" AutoGenerateColumns="False"
                                                    PageSize="10" AllowSorting="True" AllowMultiRowSelection="False" AllowPaging="True"
                                                    OnDetailTableDataBind="RGArticulosPedido_DetailTableDataBind" OnNeedDataSource="RGArticulosPedido_NeedDataSource">
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                    <MasterTableView DataKeyNames="IdMaestroSolicitud" AllowMultiColumnSorting="True">
                                                        <DetailTables>
                                                            <telerik:GridTableView DataKeyNames="IdInternoArticulo" Name="Articulos" 
                                                                Width="100%" ShowStatusBar="true" AllowSorting="True">                                                               
                                                                <Columns>
                                                                  <telerik:GridBoundColumn SortExpression="IdInternoArticulo" HeaderText="SKU" HeaderButtonType="TextButton"
                                                                        DataField="IdInternoArticulo">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="Nombre" HeaderText="Nombre Articulo" HeaderButtonType="TextButton"
                                                                     DataField="Nombre">
                                                                  </telerik:GridBoundColumn>  
                                                                  <telerik:GridBoundColumn SortExpression="Lote" HeaderText="Lote" HeaderButtonType="TextButton"
                                                                     DataField="Lote">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="FechaVencimiento" HeaderText="Fecha Vencimiento" HeaderButtonType="TextButton"
                                                                     DataField="FechaVencimiento" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="Cantidad" HeaderText="Cantidad" HeaderButtonType="TextButton"
                                                                     DataField="Cantidad">
                                                                  </telerik:GridBoundColumn>      
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                        <Columns>                                                            
                                                            <telerik:GridBoundColumn SortExpression="IdRegistroOla" HeaderText="Numero de Ola" HeaderButtonType="TextButton"
                                                                DataField="IdRegistroOla">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="NumeroFactura" HeaderText="Numero de Factura" HeaderButtonType="TextButton"
                                                                DataField="NumeroFactura" >
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="NombreCliente" HeaderText="Nombre Cliente" HeaderButtonType="TextButton"
                                                                DataField="NombreCliente">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="FechaCreacion" HeaderText="Fecha de Creacion" HeaderButtonType="TextButton"
                                                                DataField="FechaCreacion"  DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Nombre" HeaderText="Nombre" HeaderButtonType="TextButton"
                                                                DataField="Nombre">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="Comentarios" HeaderText="Comentarios" HeaderButtonType="TextButton"
                                                                DataField="Comentarios">
                                                            </telerik:GridBoundColumn>                                                              
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                     <%--Grid nuevo --%>                                              
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
            </telerik:radwindowmanager>
        </div>
    </asp:Panel>
</asp:Content>

