<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="wf_SolicitudDeOlas.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.SolicitudDeOlas.wf_SolicitudDeOlas" %>


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
                                    <telerik:RadTab Text="Solicitud" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                            <h1 class="TituloPanelTitulo">Datos de la Solicitud</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--CUERPO-->
                                            
                                                <table width="98%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial; margin-left:1%;" id="Table1">
                                                     <tr>
                                                        <td>
                                <%--      <asp:Label ID="Label7" runat="server" Text="Rutas"   Style="margin-left: 10px"></asp:Label>
                                    <asp:DropDownList ID="ddlRutas" Style="margin-left: 15px;" OnSelectedIndexChanged="ddlRutas_SelectedIndexChanged"
                                        Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true">
                                    </asp:DropDownList>  --%>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"   Style="margin-left: 10px"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false">
                                                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                            <asp:Label ID="Label2" runat="server" Text="|||"></asp:Label>

                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false">
                                                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>                                                           
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- OnClick="btnBusqueda_Click"--%>
                                                            <asp:Button runat="server" ID="btnBusqueda" Style="margin-left: 10px" Text="Busqueda" OnClick="btnBusqueda_Click"  />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <h1></h1>

                                                <asp:Panel runat="server" ID="PanelPreMaestro" Visible="true">

                                                    <telerik:RadGrid
                                                        ID="RGPreMaestro"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        style="margin-left:1%"
                                                        runat="server"                                                        
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"                                                        
                                                        PagerStyle-AlwaysVisible="true"
                                                        OnItemCommand="RGPreMaestro_ItemCommand"
                                                        OnNeedDataSource ="RGPreMaestro_NeedDataSource"
                                                        AllowMultiRowSelection="true"
                                                        PageSize="50"
                                                        EnableDragToSelectRows="false"
                                                        >
                                                       <ClientSettings EnablePostBackOnRowClick="true"  EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>

                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="checkDetalle">
                                                                </telerik:GridClientSelectColumn>

                                                                 <telerik:GridButtonColumn CommandName="btnVerDetalle" Text="Ver Detalle" UniqueName="btnVerDetalle" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMaestroSolicitud" Visible="true"
                                                                    SortExpression="idMaestroSolicitud" HeaderText="Identificador" DataField="idMaestroSolicitud"
                                                                   >
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                   >
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreCliente"
                                                                    SortExpression="NombreCliente" HeaderText="Nombre Cliente" DataField="NombreCliente">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Comentarios"
                                                                    SortExpression="Comentarios" HeaderText="Comentarios" DataField="Comentarios"
                                                                   >
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                    SortExpression="FechaCreacion" HeaderText="Fecha Creacion" DataField="FechaCreacion"
                                                                    DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                                </telerik:GridBoundColumn>

                                                         <%--   <telerik:GridBoundColumn UniqueName="Ruta"
                                                                    SortExpression="Ruta" HeaderText="Ruta" DataField="Ruta">
                                                                </telerik:GridBoundColumn> --%>

                                                                <telerik:GridBoundColumn UniqueName="Direccion"
                                                                    SortExpression="Direccion" HeaderText="Direccion" DataField="Direccion">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>                                                        
                                                    </telerik:RadGrid>
                                                    <br />

                                                    <asp:Panel runat="server" ID="Panel7" Visible="true">
                                                          <asp:Button runat="server" ID="btnEliminarPedido" Text="Eliminar Pedido" AutoPostBack="false"
                                                                        Style="margin-left: 10px"
                                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnEliminarPedido_Click" />                                                 
                                                    </asp:Panel>
                                                    <br />

                                                   <asp:Panel runat="server" ID="PanelObservacion" Visible="true">
                                                        <table width="98%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial; margin-left:1%;" id="Tabla1" >
                                                               <tr style="width:100%">
                                                                <td style="width:1%;">
                                                                    <asp:Label ID="Label3" runat="server"  Text="Observaciones" Style="margin-right:2%; margin-left: 10px;"></asp:Label>
                                                           
                                                                </td>
                                                                   <td style="width:30%">
                                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtComentarios" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                                                   </td>                                                                                                              
                                                            </tr>
                                                            <tr>
                                                                 <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="Seleccione la prioridad" Width="120"></asp:Label>                                                            
                                                                 </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddPrioridad" Class="TexboxNormal" runat="server" Width="200px"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                 <td>
                                                                    <asp:Button runat="server" ID="btnAsignar" Text="Crear Ola" AutoPostBack="false" Style="margin-left: 10px"
                                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnAsignar_Click"/>                                                 
                                                                 </td>
                                                            </tr>
                                                        </table>
                                                  </asp:Panel>                                                
                                                </asp:Panel>                                               

                                                 <asp:Panel runat="server" ID="PanelDetallesMaestro" Visible="false">
                                                      <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel2">
                                                        <h1 class="TituloPanelTitulo">Detalle de Pedidos</h1>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                     <telerik:RadGrid
                                                        ID="RGDetalleMaestro"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        style="margin-left:1%"
                                                        runat="server"
                                                        AllowFilteringByColumn="False"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                         PageSize="50"
                                                        PagerStyle-AlwaysVisible="true"
                                                        AllowMultiRowSelection="true">
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>    
                                                                <telerik:GridBoundColumn UniqueName="idPreLineaDetalleSolicitud"
                                                                    SortExpression="idPreLineaDetalleSolicitud" HeaderText="Identificador" DataField="idPreLineaDetalleSolicitud"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInterno"
                                                                    SortExpression="idInterno" HeaderText="SKU" DataField="idInterno"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <br />
                                                    <asp:Button runat="server" ID="BtnCerrar" Text="Cerrar" AutoPostBack="false"
                                                        Style="margin-left: 10px"
                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="BtnCerrar_Click" />
                                                 </asp:Panel>
                                                <h1></h1>

                                                <asp:Panel runat="server" ID="PanelOlasActivas" Visible="true">
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel5">
                                                        <h1 class="TituloPanelTitulo">Olas Pendientes de Aprobacion</h1>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    <telerik:RadGrid
                                                        ID="RGOlasPendientes"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        style="margin-left:1%"
                                                        runat="server"
                                                        AllowFilteringByColumn="False"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="50"
                                                        PagerStyle-AlwaysVisible="true"
                                                        OnItemCommand="RGOlasPendientes_ItemCommand"
                                                        AllowMultiRowSelection="true"
                                                        OnNeedDataSource ="RGOlasPendientes_NeedDataSource" >
                                                        <%--    OnItemCommand="RadGridPromociones_ItemCommand"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="checkAprobacion">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridButtonColumn CommandName="btnAgregarAOla" Text="Agregar Solicitudes" UniqueName="btnAgregarAOla" HeaderText="">
                                                                </telerik:GridButtonColumn>

                                                                <telerik:GridButtonColumn CommandName="btnEliminaOla" Text="Elimina Solicitud" UniqueName="btnEliminaOla" HeaderText="">
                                                                </telerik:GridButtonColumn>


                                                                <telerik:GridBoundColumn UniqueName="idRegistroOla"
                                                                    SortExpression="idRegistroOla" HeaderText="Numero de Ola" DataField="idRegistroOla"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Observacion"
                                                                    SortExpression="Observacion" HeaderText="Observaciones" DataField="Observacion"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idPreMaestroSolicitud"
                                                                    SortExpression="idPreMaestroSolicitud" HeaderText="Identificador Solicitud" DataField="idPreMaestroSolicitud"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaIngreso"
                                                                    SortExpression="FechaIngreso" HeaderText="Fecha de Ola" DataField="FechaIngreso"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>                                                                 

                                                                <telerik:GridBoundColumn UniqueName="Estado"
                                                                    SortExpression="Estado" HeaderText="Estado" DataField="Estado"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>                                                               

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <br />
                                                    <asp:Button runat="server" ID="btnAprobarOla" Text="Aprobar Ola" AutoPostBack="false" Style="margin-left: 10px"
                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnAprobarOla_Click" />

                                                    <asp:Button runat="server" ID="btnEditarOla" Text="Editar Ola" AutoPostBack="false"
                                                        Style="margin-left: 10px"
                                                        OnClientClick="DisplayLoadingImage1()" Visible="false" OnClick="btnEditarOla_Click" />
                                                    <asp:Label ID="LabelSeparadorEdicion" runat="server" Text="|||"  Visible="false" Style="margin-left: 2px; margin-right: 2px;"></asp:Label>
                                                     <asp:Button runat="server" ID="btnCancelarEdicion" Text="Cancelar" AutoPostBack="false"
                                                        
                                                        OnClientClick="DisplayLoadingImage1()" Visible="false" OnClick="btnCancelarEdicion_Click" />
                                                    <h1></h1>
                                                    <asp:Label ID="LabelMensaje" runat="server" Style="color: red; margin-left: 10px;" Visible="False" Text="Seleccione las solicitudes nuevas y a la Ola donde van a pertenecer..."></asp:Label>

                                                </asp:Panel>


                                                <%--OLAS ELIMIAR SOLICITUD--%>
                                                <h1></h1>
                                                <asp:Panel runat="server" ID="PanelEliminaOla" Visible="false">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel6">
                                                        <h1 class="TituloPanelTitulo">Elimina Solicitudes de Ola</h1>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                    <telerik:RadGrid
                                                        ID="RGEliminaSolicitudOlas"
                                                        AllowPaging="True"
                                                         Width="98%"
                                                        style="margin-left:1%"
                                                        runat="server"
                                                        AllowFilteringByColumn="False"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="50"
                                                        PagerStyle-AlwaysVisible="true"
                                                        AllowMultiRowSelection="true">
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"
                                                        --%>

                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridClientSelectColumn UniqueName="checkElimina">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdOla"
                                                                    SortExpression="IdOla" HeaderText="Numero de Ola" DataField="IdOla"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idPreMaestroSolicitud"
                                                                    SortExpression="idPreMaestroSolicitud" HeaderText="Identificador Solicitud" DataField="idPreMaestroSolicitud"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre Solicitud" DataField="Nombre"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaIngreso"
                                                                    SortExpression="FechaIngreso" HeaderText="Fecha de Ola" DataField="FechaIngreso"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>
                                                              
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnEliminarSolicitudOla" Text="Eliminar Solicitud de Ola" AutoPostBack="false"
                                                        Style="margin-left: 10px" OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnEliminarSolicitudOla_Click" />
                                                      <asp:Label ID="LblSeparador01" runat="server" Text="|||"  Style="margin-left: 10px"></asp:Label>
                                                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false"
                                                        Style="margin-left: 10px" OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnCancelar_Click" />

                                                    <h1></h1>
                                                    <asp:Label ID="Label1" runat="server" Style="color: red; margin-left: 10px;" Visible="true" Text="Seleccione las Solicitudes a Eliminar de la Ola..."></asp:Label>


                                                </asp:Panel>


                                                <%--OLAS APROBADAS--%>
                                                <h1></h1>
                                                <asp:Panel runat="server" ID="PanelAprobadas" Visible="true">

                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel3">
                                                        <h1 class="TituloPanelTitulo">Ola Aprobadas</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>

                                                      <asp:Button runat="server" ID="btnEliminarOla" Text="Eliminar Ola" AutoPostBack="false"
                                                                        Style="margin-left: 15px; margin-bottom: 10px;"
                                                                        OnClientClick="DisplayLoadingImage1()" Visible="true" OnClick="btnEliminarOla_Click" /> 
                                                    <telerik:RadGrid
                                                        ID="RGOlasAprobadas"
                                                        AllowPaging="True"
                                                        Width="98%"
                                                        style="margin-left:1%"
                                                        runat="server"
                                                        AllowFilteringByColumn="False"
                                                        AutoGenerateColumns="False"
                                                        AllowSorting="True"
                                                        PageSize="50"
                                                        FilterType="CheckList"
                                                        RenderMode="Lightweight"
                                                       
                                                        PagerStyle-AlwaysVisible="true"
                                                        OnNeedDataSource="RGOlasAprobadas_NeedDataSource"
                                                        OnItemCommand="RGOlasAprobadas_ItemCommand"
                                                        >
                                                        <%--
                                                              OnNeedDataSource="RadGridPromociones_NeedDataSource"
                                                              OnItemCommand="RadGridPromociones_ItemCommand"

                                                            AutoGenerateColumns="False"
                                                             PageSize="10"
                    
                                                             <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridArticulosDisponibles" AllowFilteringByColumn="true" FilterType="CheckList"
                                                                 OnNeedDataSource="RadGridArticulos_NeedDataSource" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true">
                                                       --%>

                                                        <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idRegistroOla" 
                                                                    SortExpression="idRegistroOla" HeaderText="Numero de Ola" DataField="idRegistroOla"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Observacion"
                                                                    SortExpression="Observacion" HeaderText="Observaciones" DataField="Observacion"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idPreMaestroSolicitud"
                                                                    SortExpression="idPreMaestroSolicitud" HeaderText="Identificador Solicitud" DataField="idPreMaestroSolicitud"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaIngreso"
                                                                    SortExpression="FechaIngreso" HeaderText="Fecha de Ola" DataField="FechaIngreso"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                                </telerik:GridBoundColumn>                                                                 

                                                                <telerik:GridBoundColumn UniqueName="Estado"
                                                                    SortExpression="Estado" HeaderText="Estado" DataField="Estado"
                                                                    AllowFiltering="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
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
