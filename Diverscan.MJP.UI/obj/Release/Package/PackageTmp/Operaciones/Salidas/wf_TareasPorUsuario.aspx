<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_TareasPorUsuario.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_TareasPorUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server">
        <div id="RestrictionZoneIDD" class="WindowContenedor">

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinTareas" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDD" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Tareas pendientes por usuario" Width="200px"></telerik:RadTab>
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
                                                      <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Seleccione el usuario" Width="120"></asp:Label>
                                                            <asp:DropDownList ID="ddlIdUsuario" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlIdUsuario_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:Button runat="server" ID="Buscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="Buscar_Click" />
                                                        </td>
                                                    </tr>
                                                </table>

                                                <%--OnClientClick="DisplayLoadingImage1()"--%>
                                                <telerik:RadGrid ID="RadGridTareasUsuario"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    Visible="false"
                                                    OnNeedDataSource="RadGridTareasUsuario_NeedDataSource"
                                                    runat="server"   
                                                    PageSize="10"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"                                                 
                                                    AllowMultiRowSelection="True"
                                                    AllowFilteringByColumn="True">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="FechaAsignacion"
                                                                SortExpression="FechaAsignacion" HeaderText="Fecha de asignación" DataField="FechaAsignacion"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Nombre" 
                                                                SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            
                                                            <telerik:GridBoundColumn UniqueName="idMaestroSolicitud" 
                                                                SortExpression="idMaestroSolicitud" HeaderText="Maestro Solicitud" DataField="idMaestroSolicitud"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>
                                                            
                                                            <telerik:GridBoundColumn UniqueName="Destino" 
                                                                SortExpression="Destino" HeaderText="Destino Orden" DataField="Destino"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>


                                                            <telerik:GridBoundColumn UniqueName="Cantidad" 
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Unidad_Medida" 
                                                                SortExpression="Unidad_Medida" HeaderText="Unidad de médida" DataField="Unidad_Medida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Suspendido" 
                                                                SortExpression="Suspendido" HeaderText="Suspendido" DataField="Suspendido"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
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
                                        <%--Termina UpdatePanel--%>
                                        <h1></h1>
                                        <%-- <asp:Label ID="Label2" runat="server" Text="" Width="200"></asp:Label> 
                                              <asp:Button runat="server"  ID ="btnAprobar" Text="Asignar Tarea a Usuario"  OnClientClick = "DisplayLoadingImage1();" OnClick ="btnAprobar_Click"/> --%>
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
