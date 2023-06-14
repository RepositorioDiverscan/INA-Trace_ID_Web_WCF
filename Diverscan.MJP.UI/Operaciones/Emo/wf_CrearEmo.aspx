<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_CrearEmo.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Emo.wf_CrearEmo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel14" runat="server">
        <div id="RestrictionZoneID" class="WindowContenedor">
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="false">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>
                <Windows>
                    <telerik:RadWindow ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true" style="width:1500px">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Gestion de Emos" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel  runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <br />
                                                <br />
                                               
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
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Transportista"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlTransportista" CssClass="TexboxNormal" Width="250px" ></asp:DropDownList>
                                                        </td>
                                                    </tr>                                                                                                     
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" Style="margin-left: 25%"
                                                                AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                            <asp:Button runat="server" ID="btnCreateEmo" Text="Crear Emo" Style="margin-left: 10%"
                                                                AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnCreateEmo_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                  <asp:Panel runat="server" ID="PedidosFacturados" >                                           
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelPedidos" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado de Pedidos Facturados</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    <%--Revisar los datos del RadGrid--%>                                                                                               
                                                     <telerik:RadGrid ID="RGMInvoiced" AllowPaging="true" Width="98%"
                                                        OnNeedDataSource="RGMInvoiced_NeedDataSource" AllowFilteringByColumn="true"
                                                        PagerStyle-AlwaysVisible="true" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="true">                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>          
                                                                    <telerik:GridClientSelectColumn UniqueName="checkEmo">
                                                                    </telerik:GridClientSelectColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdRecord"
                                                                        SortExpression="IdRecord" HeaderText="IdRecord" DataField="IdRecord"
                                                                        Display="false">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdSap" 
                                                                        SortExpression="IdSap" HeaderText="Id SAP" DataField="IdSap"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="RecordDate"
                                                                        SortExpression="RecordDate" HeaderText="Fecha de Registro" DataField="RecordDate"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="BillNumber"
                                                                        SortExpression="BillNumber" HeaderText="Número Factura" DataField="BillNumber"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="BillPrice" 
                                                                        SortExpression="BillPrice" HeaderText="Total" DataField="BillPrice"
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
                                                  <asp:Panel runat="server" ID="Panel4" >  
                                                       <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel5" Visible="true">
                                                        <h1 class="TituloPanelTitulo">Listado de Pedidos Por Transportista</h1>
                                                      </asp:Panel>
                                                         <%--Revisar los datos del RadGrid--%>                                                                                               
                                                     <telerik:RadGrid ID="RGOrdersDeny" AllowPaging="true" Width="98%"
                                                        OnNeedDataSource="RGOrdersDeny_NeedDataSource" PagerStyle-AlwaysVisible="true" 
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false" >                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>                                                                                                                                               

                                                                    <telerik:GridBoundColumn UniqueName="IdSap" 
                                                                        SortExpression="IdSap" HeaderText="Id SAP" DataField="IdSap"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="FechaCreacionPedido"
                                                                        SortExpression="FechaCreacionPedido" HeaderText="Fecha de Pedido" DataField="FechaCreacionPedido"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="FechaFactura"
                                                                        SortExpression="FechaFactura" HeaderText="Fecha de Facturación" DataField="FechaFactura"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="BillNumber"
                                                                        SortExpression="BillNumber" HeaderText="Número Factura" DataField="BillNumber"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="BillPrice" 
                                                                        SortExpression="BillPrice" HeaderText="Total" DataField="BillPrice"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridTemplateColumn UniqueName="IsEmo" HeaderText="EMO"> 
                                                                     <ItemTemplate> 
                                                                        <asp:Label ID="Label19" runat="server" 
                                                                            Text='<%# Convert.ToBoolean(Eval("IsEmo")) == true ? "Si" : "No" %>'/> 
                                                                     </ItemTemplate> 
                                                                    </telerik:GridTemplateColumn> 
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>                                           
                                                    <br />                                                                                            
                                                  </asp:Panel>
                                                <h1></h1>
                                                <asp:Panel runat="server" ID="Panel2" >                                           
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel3" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado Emos</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>
                                                     <table style="border-radius: 10px; border: 1px solid grey; width: 98%; border-collapse: initial;
                                                        margin-left: 1%; margin-top: 1%; margin-bottom:1%;" id="Table3">
                                                        <tr>                                                            
                                                            <td>                                                            
                                                                <asp:Button runat="server" ID="Button1" Text="Reporte Emo" Style="margin-left: 10px" 
                                                                    OnClick="btnReporteEmo_Click" PostBackUrl="~/Operaciones/Emo/wf_CrearEmo.aspx"/>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                    <%--Revisar los datos del RadGrid--%>                                                                                               
                                                     <telerik:RadGrid ID="RGEmo" AllowPaging="true" Width="98%"
                                                        OnNeedDataSource="RGEmo_NeedDataSource"   OnItemCommand="RGEmo_ItemCommand" AllowFilteringByColumn="true"
                                                        PagerStyle-AlwaysVisible="true" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false">                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>                                                                             

                                                                    <telerik:GridBoundColumn UniqueName="IdEmo"
                                                                        SortExpression="IdEmo" HeaderText="Número Emo" DataField="IdEmo" Display="false">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                        SortExpression="IdInterno" HeaderText="Número Emo" DataField="IdInterno">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="NombreTransportista" 
                                                                        SortExpression="NombreTransportista" HeaderText="Transportista" DataField="NombreTransportista"
                                                                        AllowFiltering="true" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="RecordDate"
                                                                        SortExpression="RecordDate" HeaderText="Fecha de Registro" DataField="RecordDate"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="TotalPeso"
                                                                        SortExpression="TotalPeso" HeaderText="Peso Total" DataField="TotalPeso"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="TotalMonto" 
                                                                        SortExpression="TotalMonto" HeaderText="Monto total" DataField="TotalMonto"
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
</asp:Content>
