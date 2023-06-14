<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_AprobarSalida.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_AprobarSalida" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server">



        <%--<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>--%>
        <%--<cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID = "btnHide">
</cc1:ModalPopupExtender>

<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    <div id="myModal" class="modal">
        Pregunta
    </div>
    <div class="bodyInfo">
        Esta Seguro?
        <br />
        <asp:Button ID="btnSi" runat="server" Text="Si" OnClick="btnSi_Click" />
        <asp:Button ID="btnHide" runat="server" Text="No" />
    </div>
</asp:Panel>--%>


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
                                    <telerik:RadTab Text="Asignar Tareas" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">


                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

                                        <%--comienza UpdatePanel--%>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../../images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>
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
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" class="TexboxNormal" runat="server" AutoPostBack="false" >
                                                                <DateInput DateFormat="dd/MM/yyyy"> 
                                                                   </DateInput> 
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" visible="false"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Búsqueda por los campos con (*)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 <br />   
                                                <telerik:RadGrid
                                                    ID="RGAprobarSalida"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    OnNeedDataSource="RadGrid_NeedDataSource"
                                                    OnItemCommand="RGAprobarSalida_ItemCommand"
                                                    OnClientClick="DisplayLoadingImage1()"                                                  
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="True">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView>

                                                        <Columns>
                                                            <%-- %><telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                            </telerik:GridClientSelectColumn>--%>

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
                                                                SortExpression="Fecha" HeaderText="Fecha Solicitud" DataField="Fecha"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>

                                                          <%--  <telerik:GridBoundColumn UniqueName="IdBodega"
                                                                SortExpression="IdBodega" HeaderText="Id Bodega" DataField="IdBodega">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Bodega" 
                                                                SortExpression="Bodega" HeaderText="Bodega" DataField="Bodega">
                                                            </telerik:GridBoundColumn>--%>

                                                            <telerik:GridBoundColumn UniqueName="DestinoNombre"
                                                                SortExpression="DestinoNombre" HeaderText="Destino" DataField="DestinoNombre">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn UniqueName="FechaEntrega"
                                                                SortExpression="FechaEntrega" HeaderText="Fecha entrega" DataField="FechaEntrega"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>

                                                               <telerik:GridBoundColumn UniqueName="PrioridadString" 
                                                                SortExpression="PrioridadString" HeaderText="Prioridad" DataField="PrioridadString">
                                                            </telerik:GridBoundColumn>
                                                       
                                                        </Columns>
                                                    </MasterTableView>

                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                            
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="TableSector">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" Text="Sector"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddSector" CssClass="TexboxNormal" Width="250px"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddSector_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                 <br />   

                                                <%--OnClientClick="DisplayLoadingImage1()"--%>
                                                <telerik:RadGrid ID="RadGridDetalleSalida"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    Visible="true"
                                                    OnNeedDataSource="RadGridDetalleSalida_NeedDataSource"
                                                    OnItemCommand="RadGridDetalleSalida_ItemCommand"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true"
                                                    AllowFilteringByColumn="True">
                                                    <GroupingSettings CaseSensitive="false" />                                                    
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridClientSelectColumn UniqueName="CheckUsuario">
                                                            </telerik:GridClientSelectColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdLineaDetalleSolicitud" Display="false" Visible="true"
                                                                SortExpression="IdLineaDetalleSolicitud" HeaderText="Id DS" DataField="IdLineaDetalleSolicitud"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>                                                       

                                                            <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Sector"
                                                                SortExpression="Sector" HeaderText="Sector" DataField="NombreSector">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Estado"
                                                                SortExpression="Estado" HeaderText="Estado" DataField="DetalleAlistado">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                                <br />                                               

                                                <table width="90%" style="border-radius: 10px; border: 1px solid grey; width: 90%; border-collapse: initial;" id="Table1">                                  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Seleccione el usuario" Width="120"></asp:Label>
                                                            <asp:DropDownList ID="ddlIdUsuario" Class="TexboxNormal" OnSelectedIndexChanged="ddlIdUsuario_SelectedIndexChanged" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Seleccione la prioridad" Width="120"></asp:Label>
                                                            <asp:DropDownList ID="ddPrioridad" Class="TexboxNormal" runat="server" Width="200px"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnAddTask" Text="+" AutoPostBack="false" OnClick="btnAddTask_Click" />
                                                            <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                            <asp:Button runat="server" ID="btnRemoveTask" Text="-" AutoPostBack="false"  OnClick="btnRemoveTask_Click" />
                                                        </td>
                                                       
                                                    </tr>                                                
                                                </table>

                                                 <br />

                                                 <telerik:RadGrid ID="RadGridTasks"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    Visible="true"
                                                 OnNeedDataSource="RadGridTasks_NeedDataSource"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true"
                                                    AllowFilteringByColumn="True">
                                                    <GroupingSettings CaseSensitive="false" />                                                    
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridClientSelectColumn UniqueName="CheckTask">
                                                            </telerik:GridClientSelectColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdTareaUsuario" Display="false" Visible="true"
                                                                HeaderText="Id DS" DataField="IdTareaUsuario"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>      
                                                            
                                                            <telerik:GridBoundColumn UniqueName="NombreUsuario"
                                                                SortExpression="NombreUsuario" HeaderText="Nombre Usuario" DataField="NombreUsuario"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                            </telerik:GridBoundColumn>
                                               
                                                            <telerik:GridBoundColumn UniqueName="IdLineaDetalle"
                                                                SortExpression="IdLineaDetalleSolicitud" HeaderText="Id Linea Detalle" DataField="IdLineaDetalle">
                                                            </telerik:GridBoundColumn>

                                                               <telerik:GridBoundColumn UniqueName="FechaAsignacion"
                                                                SortExpression="FechaAsignacion" HeaderText="Fecha Asignacion" DataField="FechaAsignacion" 
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="IdInternoSAP"
                                                                SortExpression="IdInternoSAP" HeaderText="Id Interno" DataField="IdInternoSAP">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="nombreDestino"
                                                                SortExpression="Destino" HeaderText="Destino" DataField="nombreDestino">
                                                            </telerik:GridBoundColumn>

                                                              <telerik:GridBoundColumn UniqueName="DescripcionPrioridad"
                                                                SortExpression="DescripcionPrioridad" HeaderText="Prioridad" DataField="DescripcionPrioridad">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>

                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
        
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
