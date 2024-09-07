<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteFaltantesDespacho.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.FaltantesDespacho.ReporteFaltantesDespacho" %>
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
                                    <telerik:RadTab Text="Faltantes Despacho" Width="200px"></telerik:RadTab>
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
                                                            <asp:Label ID="LblDateInit" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPDateInit" class="TexboxNormal" runat="server" AutoPostBack="false" DateInput-DateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblDateFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPDateFinal" class="TexboxNormal" runat="server" AutoPostBack="false" DateInput-DateFormat="dd/MM/yyyy">
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
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelArticulos" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado Articulos Despachados</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico"
                                                            Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <%--Revisar los datos del RadGrid--%>                                                  

                                              <%--  <asp:Button runat="server" ID="btnReporte" Text="Reporte" AutoPostBack="false" onclick="btnReporte_Click" 
                                                    PostBackUrl="~/Reportes/FaltantesDespacho/ReporteFaltesDespachos.aspx" style="margin-bottom:15px;"/>--%>

                                                     <telerik:RadGrid RenderMode="Lightweight" ID="RGDArticulosDespachados" runat="server"
                                                        ShowStatusBar="true" AutoGenerateColumns="False" AllowPaging="true" Width="98%"
                                                        OnNeedDataSource="RGDArticulosDespachados_NeedDataSource" 
                                                         OnDetailTableDataBind="RGDArticulosDespachados_DetailTableDataBind"
                                                         OnClientClick="DisplayLoadingImage1()" 
                                                         AllowSorting="True" AllowPading="True" PagerStyle-AlwaysVisible="true">                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                         <PagerStyle Mode="NumericPages"></PagerStyle>
                                                        <MasterTableView DataKeyNames="IdArticulo" AllowMultiColumnSorting="True">
                                                            <DetailTables>
                                                            <telerik:GridTableView DataKeyNames="IdInterno" Name="Articulo" 
                                                                Width="100%" ShowStatusBar="true" AllowSorting="True">                                                               
                                                                <Columns>
                                                                  <telerik:GridBoundColumn SortExpression="IdInterno" HeaderText="Id Ola" HeaderButtonType="TextButton"
                                                                        DataField="IdInterno">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="NombreOla" HeaderText="Ola" HeaderButtonType="TextButton"
                                                                     DataField="NombreOla">
                                                                  </telerik:GridBoundColumn>  
                                                                  <telerik:GridBoundColumn SortExpression="CantidadSolicitada" HeaderText="Cantidad Solicitada" HeaderButtonType="TextButton"
                                                                     DataField="CantidadSolicitada">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="CantidadAlistada" HeaderText="Cantidad Despachada" HeaderButtonType="TextButton"
                                                                     DataField="CantidadAlistada">
                                                                  </telerik:GridBoundColumn>     
                                                                    <telerik:GridBoundColumn SortExpression="CantidadDisponible" HeaderText="Cantidad Disponible" HeaderButtonType="TextButton"
                                                                     DataField="CantidadDisponible">
                                                                  </telerik:GridBoundColumn>   
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                            <Columns>                                                               
                                                                    <telerik:GridBoundColumn UniqueName="IdArticulo"
                                                                        SortExpression="IdArticulo" HeaderText="ID Articulo" DataField="IdArticulo"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" Visible ="false">
                                                                    </telerik:GridBoundColumn>

                                                                   <telerik:GridBoundColumn UniqueName="IdInternoArticulo"
                                                                        SortExpression="IdInternoArticulo" HeaderText="SKU" DataField="IdInternoArticulo"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" Visible ="true">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="NombreArticulo" 
                                                                        SortExpression="NombreArticulo" HeaderText="Nombre" DataField="NombreArticulo"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="CantidadSolicitada"
                                                                        SortExpression="CantidadSolicitada" 
                                                                        HeaderText="Cantidad Solicitada" 
                                                                        DataField="CantidadSolicitada">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="CantidadDespachada"
                                                                        SortExpression="CantidadDespachada" 
                                                                        HeaderText="Cantidad Despachada" 
                                                                        DataField="CantidadDespachada">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Diferencia"
                                                                            SortExpression="Diferencia" 
                                                                            HeaderText="Diferencia" 
                                                                            DataField="Diferencia">
                                                                        </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="false">
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

