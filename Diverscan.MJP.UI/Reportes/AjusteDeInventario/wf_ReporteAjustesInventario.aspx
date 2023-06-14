<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_ReporteAjustesInventario.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.AjusteDeInventario.wf_ReporteAjustesInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
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
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Ajustes">
                                            <h1 class="TituloPanelTitulo">Ajustes de Invetario</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--CUERPO-->
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Promocion"></asp:Label>
                                                            <asp:DropDownList ID="ddlPromociones" Style="margin-left: 15px;" OnSelectedIndexChanged="ddlPromociones_SelectedIndexChanged"
                                                                Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false">
                                                                <DateInput DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                            <asp:Label ID="LblSeparador01" runat="server" Text="|||"></asp:Label>

                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false">
                                                                <DateInput DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                            <asp:Label ID="Label1" runat="server" Text="|||"></asp:Label>

                                                            <asp:Button runat="server" ID="btnBusqueda" Text="Busqueda" OnClick="btnBusqueda_Click" />

                                                        </td>
                                                        <td></td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnReporte" Text="Generar Reporte" AutoPostBack="false"
                                                                OnClientClick="DisplayLoadingImage1()" Visible="true" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <h1></h1>
                                                <asp:Panel runat="server" ID="PanelPromoc" Visible="false">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel5">
                                                        <h1 class="TituloPanelTitulo">Articulos de la Promocion</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    <telerik:RadGrid
                                                        ID="RGArticulosPromocion"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        runat="server"
                                                        AllowFilteringByColumn="true"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="10"
                                                        AllowMultiRowSelection="true">
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idDetallePromocion" Visible="true"
                                                                    SortExpression="idDetallePromocion" HeaderText="Identificador" DataField="idDetallePromocion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMaestroPromocion"
                                                                    SortExpression="idMaestroPromocion" HeaderText="Identificacion Promocion" DataField="idMaestroPromocion"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInternoArticulo"
                                                                    SortExpression="idInternoArticulo" HeaderText="Identificacion Articulo" DataField="idInternoArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Gtin"
                                                                    SortExpression="Gtin" HeaderText="Gtin Presentacion" DataField="Gtin"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                </asp:Panel>

                                                <asp:Panel runat="server" ID="PanelAjustes" Visible="false">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel2">
                                                        <h1 class="TituloPanelTitulo">Ajustes de Inventario</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>
                                                    <%-- OnNeedDataSource ="RadGrid_NeedDataSource" 
                                                         OnItemCommand="RGLogAjustesInventario_ItemCommand"--%>
                                                    <telerik:RadGrid ID="RGAjustes" AllowPaging="True" Width="100%"
                                                        OnItemCommand="RGAjustes_ItemCommand"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">

                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="checkDetalle">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridButtonColumn CommandName="btnVerArticulos" Text="Ver Articulos" UniqueName="btnVerArticulos" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                              
                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudAjusteInventario"
                                                                    SortExpression="IdSolicitudAjusteInventario" HeaderText="Id Solicitud" DataField="IdSolicitudAjusteInventario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaSolicitud"
                                                                    SortExpression="FechaSolicitud" HeaderText="Fecha Solicitud" DataField="FechaSolicitud">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Apellidos"
                                                                    SortExpression="Apellidos" HeaderText="Apellidos" DataField="Apellidos">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="MotivoInventario"
                                                                    SortExpression="MotivoInventario" HeaderText="Motivo Inventario" DataField="MotivoInventario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="TipoMotivo"
                                                                    SortExpression="TipoMotivo" HeaderText="Tipo" DataField="TipoMotivo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CentroCosto" Display="false"
                                                                    SortExpression="CentroCosto" HeaderText="CentroCosto" DataField="CentroCosto">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>

                                                    </telerik:RadGrid>
                                                    <h1></h1>

                                                    <asp:Button runat="server" ID="btnInformacion" Text="Visualizar Informacion"
                                                        AutoPostBack="false"
                                                        Style="margin-left: 10px; margin-right: 10px;"
                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btInformacion_Click" />
                                                    <asp:Label ID="Label2" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button runat="server" ID="btGenerar" Text="Generar Reporte" AutoPostBack="false"
                                                        Style="margin-left: 10px"
                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" />


                                                </asp:Panel>

                                                <asp:Panel runat="server" ID="PanelArticulosSolicitud" Visible="false">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel6">
                                                        <h1 class="TituloPanelTitulo">Articulos del Ajuste</h1>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>


                                                    <telerik:RadGrid
                                                        ID="RGArticulosSolicitud"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        runat="server"
                                                        PageSize="10">
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"
                                                            AllowFilteringByColumn="true"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                       
                                                        AllowMultiRowSelection="true"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudAjusteInventario" Visible="true"
                                                                    SortExpression="IdSolicitudAjusteInventario" HeaderText="Identificador Solicitud" DataField="IdSolicitudAjusteInventario"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="IdArticulo"
                                                                    SortExpression="IdArticulo" HeaderText="Identificacion Articulo" DataField="IdArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                    SortExpression="IdInterno" HeaderText="Identificador Articulo Interno" DataField="IdInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>


                                                </asp:Panel>

                                                <asp:Panel runat="server" ID="PanelVisualizacionInformacion" Visible="false">


                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel7">
                                                        <h1 class="TituloPanelTitulo">Reporte Transformaciones</h1>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>




                                                    <telerik:RadGrid
                                                        ID="RadGrid1"
                                                        AllowPaging="True"
                                                        Width="100%"
                                                        runat="server"
                                                        AllowFilteringByColumn="true"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="10"
                                                        AllowMultiRowSelection="true">
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="IdArticulo" Visible="true"
                                                                    SortExpression="IdArticulo" HeaderText="Identificar Articulo Sistema" DataField="IdArticulo"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                    SortExpression="IdInterno" HeaderText="Identificador Internoa" DataField="IdInterno"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadSalida"
                                                                    SortExpression="CantidadSalida" HeaderText="Cantidad Salida" DataField="CantidadSalida"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadEntrada"
                                                                    SortExpression="CantidadEntrada" HeaderText="Cantidad Entrada" DataField="CantidadEntrada"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="Sobrante"
                                                                    SortExpression="Sobrante" HeaderText="Diferencia" DataField="Sobrante"
                                                                    AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>


                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </asp:Panel>

                                                <!--TERMINA-->
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
