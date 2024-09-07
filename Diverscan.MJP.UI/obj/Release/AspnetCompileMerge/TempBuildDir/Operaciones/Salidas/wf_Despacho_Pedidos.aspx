<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Despacho_Pedidos.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_Despacho_Pedidos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                                    <telerik:RadTab Text="Facturar Olas" Width="200px"></telerik:RadTab>
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

                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" Visible="false" />
                                                        </td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbFacturado" runat="server"
                                                                AutoPostBack="true" Text="Facturada" />
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" Style="margin-left: 25%"
                                                                AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <br />

                                                <asp:Panel runat="server" ID="PaneOlas" Visible="false">
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelPendiente" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado de Olas a Facturar</h1>

                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelAprobadas" Visible="false">

                                                        <h1 class="TituloPanelTitulo">Listado de Olas Facturadas</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <%--Revisar los datos del RadGrid--%>
                                                    <telerik:RadGrid ID="RGAprobarSalida" AllowPaging="true" Width="98%" OnNeedDataSource="RadGrid_NeedDataSource" Style="margin-left: 1%" PagerStyle-AlwaysVisible="true"
                                                        OnItemCommand="RGAprobarSalida_ItemCommand" OnClientClick="DisplayLoadingImage1()" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="checkFacturar">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="idRegistroOla"
                                                                    SortExpression="idRegistroOla" HeaderText="Ola" DataField="idRegistroOla">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaIngreso"
                                                                    SortExpression="FechaIngreso" HeaderText="Fecha" DataField="FechaIngreso">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Observacion"
                                                                    SortExpression="Observacion" HeaderText="Observacion" DataField="Observacion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Ruta"
                                                                    SortExpression="Ruta" HeaderText="Ruta" DataField="Ruta">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="PorcentajeAlistado"
                                                                    SortExpression="PorcentajeAlistado" HeaderText="Porcentaje Alistado" DataField="PorcentajeAlistado">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Facturado" Visible="true"
                                                                    SortExpression="Facturado" HeaderText="Facturado" DataField="Facturado">
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
                                                <asp:Panel runat="server" ID="PanelDetalle" Visible="false">
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel5">
                                                        <h1 class="TituloPanelTitulo">Detalles de la Ola</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>
                                                     <table style="border-radius: 10px; border: 1px solid grey; width: 98%; 
                                                        border-collapse: initial; margin-left: 1%; margin-bottom: 1%; margin-top: 1%;" id="Table2">                                                                                                      
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
                                                            <asp:Button runat="server" ID="btnFacturar" OnClientClick="DisplayLoadingImage1()"
                                                                Style="margin-left: 10px" Text="Facturar" OnClick="btnFacturar_Click" />
                                                            <asp:Label ID="labelSeparador" runat="server" Text="|||" Style="margin-left: 3px; margin-right: 3px;"></asp:Label>                                                         
                                                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click" />   
                                                            <asp:Label ID="label5" runat="server" Text="|||" Style="margin-left: 3px; margin-right: 3px;"></asp:Label>    
                                                            <asp:Button runat="server" ID="btnGenerarPdf" Text="Generar PDF" OnClick="btnGenerarPdf_Click" PostBackUrl="~/Operaciones/Salidas/wf_Despacho_Pedidos.aspx" />                                                           
                                                            <asp:Button runat="server" ID="btnCancelarFacturada" Visible="false" Text="Cancelar" Style="margin-left: 10px" OnClick="btnCancelarFacturada_Click" />
                                                           </td>                                                           
                                                        </tr>
                                                      </table>

                                                    <h1></h1>
                                                    <telerik:RadGrid ID="RGDetalleOla" AllowPaging="true" Width="98%" Style="margin-left: 1%" PagerStyle-AlwaysVisible="true"
                                                        OnClientClick="DisplayLoadingImage1()" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false" InsertItemPageIndexAction="ShowItemOnFirstPage" RenderMode="Lightweight"
                                                        OnNeedDataSource="RGDetalleOla_NeedDataSource">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idRegistroOla"
                                                                    SortExpression="idRegistroOla" HeaderText="Ola" DataField="idRegistroOla" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMaestroSolicitud"
                                                                    SortExpression="idMaestroSolicitud" HeaderText="Solicitud" DataField="idMaestroSolicitud" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInternoPanal"
                                                                    SortExpression="idInternoPanal" HeaderText="Identificador Artículo" DataField="idInternoPanal">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre Artículo" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreUsuario"
                                                                    SortExpression="NombreUsuario" HeaderText="Nombre del Alistador" DataField="NombreUsuario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadSolicitada"
                                                                    SortExpression="CantidadSolicitada" HeaderText="Cantidad Solicitada" DataField="CantidadSolicitada">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadAlistada"
                                                                    SortExpression="CantidadAlistada" HeaderText="Cantidad Alistada" DataField="CantidadAlistada">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadDisponible"
                                                                    SortExpression="CantidadDisponible" HeaderText="Cantidad Disponible" DataField="CantidadDisponible">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

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
