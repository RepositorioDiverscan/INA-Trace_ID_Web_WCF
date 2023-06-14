<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Despacho.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.Despacho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>--%>
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";}
     </script>

    <div id="RestrictionZoneIDespachosPedido" class="WindowContenedor">
        <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>

             <Windows>
                <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDespachosPedido" AutoSize="true">
                    <ContentTemplate>
                        <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                            <Tabs>
                                <telerik:RadTab Text="Despachos" Width="200px"></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>

                        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                            <telerik:RadPageView runat="server" ID="RadPageView1">
                                <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <%-- Inicio del contenido  --%>
                                    
                                         <ContentTemplate>
                                             <%-- IMAGEN DE CARGA --%>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <%--<img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >--%>                                        
                                                    </center>
                                                </div>

                                            <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                <%-- Visualización de elementos --%>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Despacho</h1>
                                                             <br />
                                                             <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador01" runat="server" Text="|||"></asp:Label>

                                                             <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                             <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                             <asp:Label ID="LblSeparador02" runat="server" Text="|||"></asp:Label>

                                                              <asp:Label ID="LblArticulo" runat="server" Text="Artículo"></asp:Label>
                                                              <asp:DropDownList ID="ddlidArticulo" Class="TexboxNormal" runat="server" Width="150px" AutoPostBack="True"></asp:DropDownList>
                                                              <%--<asp:Label ID="LblSeparador03" runat="server" Text="|||"></asp:Label>--%>
                                                             <br />
                                                             <br />
                                    
                                                             <asp:Button runat="server" ID="btnBuscarPorFechas" Text="Generar" OnClick="btnBuscarPorFechas_Click" AutoPostBack="true" ></asp:Button>
                                                            <%--OnClick="btnBuscarPorFechas_Click" --%>
                                                             <asp:Label ID="LblSeparador04" runat="server" Text="|||"></asp:Label>
                                                               
                                                             
                                                             <asp:Button runat="server" ID="BtnLimpiar" Text="Limpiar"  OnClick="BtnLimpiar_Click" />
                                                            <%--OnClick="BtnLimpiar_Click" --%>

                                                            <asp:Label ID="Label3" runat="server" Text="|||"></asp:Label>
                                                            <asp:Button runat="server" ID="BtnExportar" Text="Exportar Excel" OnClick="btnExportar_Click" />
                                                            <%--OnClick="btnExportar_Click" --%>
                                                         </td>
                                                    </tr>
                                            </table>
                                         
                                             <telerik:RadGrid ID="RadGridReporteDespacho" RenderMode="Auto" AllowPaging="True" OnNeedDataSource="RadGridReporteDespacho_NeedDataSource" 
                                                    runat="server" AutoGenerateColumns="False" AlwaysVisible="true" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true"  AllowFilteringByColumn="false" FilterType="CheckList">

                                                    <MasterTableView Style="color: black;">
                                                        <Columns>

                                                                <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="Solicitud"
                                                                SortExpression="Solicitud" HeaderText="Numero de Solicitud" DataField="Solicitud"
                                                                    >
                                                             </telerik:GridBoundColumn>


                                                            <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo">
                                                            </telerik:GridBoundColumn>

                                                            
                                                            <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="Referencia"
                                                                SortExpression="Referencia" HeaderText="Referencia Bexim" DataField="Referencia">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="SSCC"
                                                                SortExpression="SSCC" HeaderText="SSCC" DataField="SSCC">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="Destino"
                                                                SortExpression="Destino" HeaderText="Destino Pedido" DataField="Destino">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn ShowFilterIcon="false" UniqueName="FechaDespacho"
                                                                SortExpression="FechaDespacho" HeaderText="Fecha de Despacho" DataField="FechaDespacho">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                        <%--  <ClientEvents OnRowDblClick="OnRowDblClick" />--%>
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                        </ContentTemplate>


                                        
                                    <%-- Fin del contenido  --%>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>

                    </ContentTemplate>
                </telerik:RadWindow>
             </Windows>
        </telerik:RadWindowManager>
    </div>
</asp:Content>
