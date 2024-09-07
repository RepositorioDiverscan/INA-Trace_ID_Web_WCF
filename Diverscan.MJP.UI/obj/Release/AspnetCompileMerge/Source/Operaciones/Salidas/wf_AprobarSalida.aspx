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
        <%--comienza UpdatePanel--%>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" 
                    style="width: 78%; height: 70%; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px;
                    margin-left:20px; margin-top:20px; margin-bottom:20px;">
                                                
                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                    <center>
                        <img id="loading1" src="../../images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" />                                        
                    </center>
                </div>
                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"></asp:DropDownList>
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
                    <h1>Olas aprobadas</h1>
                <telerik:RadGrid
                    ID="RGAprobarSalida"
                    AllowPaging="True"
                    Width="100%"
                    OnNeedDataSource="RadGrid_NeedDataSource"
                    OnItemCommand="RGAprobarSalida_ItemCommand"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    PageSize="50"                                                                                                       
                    AllowMultiRowSelection="false"
                    EnablePostBackOnRowClick="true">
                                                                                                                                                                     
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

                                <telerik:GridBoundColumn UniqueName="Comentarios"
                                SortExpression="Comentarios" HeaderText="Comentarios" DataField="Comentarios">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="IdInterno"
                                SortExpression="IdInterno" HeaderText="Id Ola" DataField="IdInterno">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                SortExpression="FechaCreacion" HeaderText="Fecha Solicitud" DataField="FechaCreacion"
                                DataFormatString="{0:dd/MM/yyyy}"
                                DataType="System.DateTime">
                            </telerik:GridBoundColumn>

                            <%--<telerik:GridBoundColumn UniqueName="IdBodega"
                                SortExpression="IdBodega" HeaderText="Id Bodega" DataField="IdBodega">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="Bodega" 
                                SortExpression="Bodega" HeaderText="Bodega" DataField="Bodega">
                            </telerik:GridBoundColumn>--%>

                            <telerik:GridBoundColumn UniqueName="Nombre"
                                SortExpression="Nombre" HeaderText="Destino" DataField="Nombre">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="PorcentajeAlistado"
                                SortExpression="PorcentajeAlistado" HeaderText="Porcentaje Alisto" DataField="PorcentajeAlistado">
                            </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="PorcentajeAsignado"
                                SortExpression="PorcentajeAsignado" HeaderText="Porcentaje Asignado" DataField="PorcentajeAsignado">
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
                    <ClientSettings EnablePostBackOnRowClick="false">
                        <Selecting AllowRowSelect="true"></Selecting>
                    </ClientSettings>
                                                    
                </telerik:RadGrid>

                <br />   
                                            
                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="TableSector">
                        <tr>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="Sector" style ="margin-top:8px; margin-left:5px;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddSector" CssClass="TexboxNormal" Width="250px" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddSector_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="_lblchkAsignado" runat="server" Text="Sin Asignar" style ="margin-top:8px; margin-left:5px;" Visible ="false"></asp:Label> 
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="_chkAsignado" AutoPostBack="true" OnCheckedChanged="ChkAsignado_OnCheckedChanged" onchange="DisplayLoadingImage1()" Visible ="false"></asp:CheckBox>
                                </td>
                            </tr>
                    </table>

                    <br />

                <%--OnClientClick="DisplayLoadingImage1()"--%>
                    <h1>Detalle Olas aprobadas</h1>
                <telerik:RadGrid ID="RadGridDetalleSalida"
                    AllowPaging="True"
                    Width="100%"
                    Visible="true"
                    OnNeedDataSource="RadGridDetalleSalida_NeedDataSource"
                    OnItemCommand="RadGridDetalleSalida_ItemCommand"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    PageSize="150"
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

                            <telerik:GridBoundColumn UniqueName="IdArticulo"
                                SortExpression="IdArticulo" HeaderText="SKU" DataField="IdArticulo"
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

                                <telerik:GridBoundColumn UniqueName="NombreUsuario"
                                SortExpression="NombreUsuario" HeaderText="Nombre Alistador" DataField="NombreUsuario">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true"></Selecting>
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="50,100,300,1000" />
                </telerik:RadGrid>

                <br />

                <table width="90%" style="border-radius: 10px; border: 1px solid grey; width: 90%; border-collapse: initial;" id="Table1">
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Seleccione el usuario" Width="120"></asp:Label>
                            <asp:DropDownList ID="ddlIdUsuario" Class="TexboxNormal"
                                OnSelectedIndexChanged="ddlIdUsuario_SelectedIndexChanged" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnAddTask" Text="+" AutoPostBack="false" OnClick="btnAddTask_Click" />
                            <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                            <asp:Button runat="server" ID="btnRemoveTask" Text="-" AutoPostBack="false"  OnClick="btnRemoveTask_Click" />
                        </td>

                    </tr>
                </table>
                    <br />
                    <h1>Tareas Olas</h1>
                    <telerik:RadGrid ID="RadGridTasks"
                    AllowPaging="True"
                    Width="100%"
                    Visible="true"
                    OnNeedDataSource="RadGridTasks_NeedDataSource"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowSorting="True"
                    PageSize="50"
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
