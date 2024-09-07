<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrazabilidadProducto.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.TrazabilidadProducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    
    <asp:Panel ID="Panel4" runat="server">

        <div id="TrazabilityProduct" class="WindowContenedor">

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManagerTrazabilityProduct" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="TrazabilityProduct" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Trazabilidad Articulo" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">


                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

                                        
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="SKU Articulo" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="200px"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" AutoPostBack="true"/>
                                                            <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" class="TexboxNormal" runat="server" AutoPostBack="false">
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
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" 
                                                                DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Búsqueda por los campos con (*)"></asp:Label>
                                                        </td>
                                                        <td>                                                         
                                                            <asp:Button runat="server" ID="btnReporte" Text="Generar Reporte" AutoPostBack="false"
                                                                OnClientClick="DisplayLoadingImage1()" OnClick="btnReporte_Click" Visible="true" PostBackUrl="~/Reportes/TrazabilidadProducto.aspx" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblArticulo" runat="server" Font-Bold="true" Text="Artículo" Visible="false"/>
                                                        </td>
                                                        <td>                                                         
                                                            <asp:TextBox ID="txtArticulo" runat="server" AutoPostBack="true"
                                                                Class="TexboxNormal" Width="300px" ReadOnly="true" Visible="false"/>
                                                        </td>
                                                    </tr>

                                                </table>

                                                <telerik:RadGrid
                                                    ID="RGRProductTrazability"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    OnNeedDataSource="RGRProductTrazability_NeedDataSource"
                                                    AllowMultiRowSelection="True">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView>

                                                        <Columns>                                                      
                                                            <telerik:GridBoundColumn UniqueName="FechaVencimiento"
                                                                SortExpression="FechaVencimiento" HeaderText="Fecha de Vencimiento"
                                                                DataField="FechaVencimiento"  DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Lote"
                                                                SortExpression="Lote" HeaderText="Lote" DataField="Lote">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Ubicacion"
                                                                SortExpression="Ubicacion" HeaderText="Ubicacion" DataField="Ubicacion">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                SortExpression="FechaRegistro" HeaderText="Fecha de Registro" DataField="FechaRegistro"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="MetodoAccion"
                                                                SortExpression="MetodoAccion" HeaderText="Operación" DataField="MetodoAccion">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreEstado"
                                                                SortExpression="NombreEstado" HeaderText="Tipo" DataField="NombreEstado">
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
