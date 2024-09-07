<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_DiferenciaInventario.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.wf_DiferenciaInventario" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <script type="text/javascript">
        function imprimirDiv(divID) {
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
                "<html><head><title></title></head><body>" +
                divElements + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;


        }
    </script> --%>    
    <asp:Panel ID="Panel4" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid2"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGrid3">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid3"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div id="RestrictionZoneIDDiferenciaInventario" class="WindowContenedor">

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneIDDiferenciaInventario" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Reporte Diferencia Inventario" Width="230px"></telerik:RadTab>
                                    <telerik:RadTab Text="Detalle Solicitud" Width="200px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 3" Width="200px" Visible="false"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Reporte">
                                            <h1 class="TituloPanelTitulo">Datos para Generar Reporte</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <asp:Button ID="btnGenera" runat="server" Text="Vista Previa" Width="150px" OnClick="btnGenera_Click" Enabled="true" />
                                                <%--<asp:Label ID="Label1" runat="server" Text="||"></asp:Label>--%>

                                                <%--<asp:Label ID="Label2" runat="server" Text="||"></asp:Label>--%>

                                                <div style="display: none">
                                                    <asp:Button ID="BtnImprime" runat="server" Text="Imprimir" Width="150px" OnClick="BtnImprime_Click" Enabled="false" />
                                                    <asp:Label ID="LblExporta" runat="server" Text="Formato a exportar" Enabled="false"></asp:Label>
                                                    <asp:DropDownList ID="ddlIdformatoexporta" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>

                                                <asp:Button ID="BtnExporta" runat="server" Text="Exportar a Excel" Width="150px" OnClick="BtnExporta_Click" Enabled="false" />
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <div style="display: none">
                                                                <asp:Label ID="Lblmensaje" runat="server" Text=" ** Cuando imprima, oprima Alt-Tab para ver la ventana de impresoras **" Visible="true" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton ID="rdbTodos" Text="Todos" GroupName="rdbOpcion" Checked="true" runat="server" />

                                                            <asp:RadioButton ID="rdbDiferencias" Text="Solo Diferentes" GroupName="rdbOpcion" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblArticulo" runat="server" Text="Artículo:"></asp:Label>
                                                            <asp:DropDownList ID="DdlIdarticuloInterno" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Vista Previa Reporte</h1>
                                                        </td>
                                                        <td>
                                                            <%--<input type="button" value="Print 1st Div" onclick="javascript: imprimirDiv('gridReporteDiferenciaInventario')" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%--                                                      <CR:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="false" Height="25px" Position="relative" Visible="false"
                                                          HasExportButton="False" HasPrintButton="False" HasCrystalLogo="False" HasToggleGroupTreeButton="false"
                                                          ToolPanelView="None" HasRefreshButton="False" />--%>                                                            
                                                            <%--</asp:Panel>--%>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>


                                                                                                            <asp:GridView ID="gvDiferenciaInventario" Visible="false"
                                                                AllowPaging="True" CellPadding="4" ForeColor="#333333"
                                                                OnRowCommand="gvDiferenciaInventario_RowCommand"
                                                                OnPageIndexChanging="gvDiferenciaInventario_PageIndexChanging"
                                                                runat="server" AutoGenerateColumns="False"
                                                                GridLines="None"
                                                                PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt"
                                                                PageSize="10" DataKeyNames="CodigoInterno"
                                                                FooterStyle="15">
                                                                <Columns>
                                                                    <asp:BoundField DataField="CodigoInterno" HeaderText="Codigo Interno">
                                                                        <HeaderStyle Width="100px" />
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="NombreArticulo" HeaderText="Nombre del Articulo">
                                                                        <HeaderStyle Width="100px" />
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DisponiblesInventario" HeaderText="Disponibles en el Inventario">
                                                                        <HeaderStyle Width="100px" />
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DisponiblesERP" HeaderText="Disponibles en el ERP">
                                                                        <HeaderStyle Width="100px" />
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div id="gridReporteDiferenciaInventario">
                                                            <%--<asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel5">--%>
                                                            <telerik:RadGrid ID="rdDatosReporteDiferenciaInventario" Visible="true" runat="server" AllowMultiRowSelection="false" PageSize="10" OnItemCommand="RadGrid_ItemCommand"
                                                                AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" OnNeedDataSource="RadGrid_NeedDataSource">
                                                                <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                                    <Selecting AllowRowSelect="false"></Selecting>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                                </ClientSettings>
                                                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                <MasterTableView>
                                                                    <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                                                </MasterTableView>
                                                            </telerik:RadGrid>

                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroSolicitud0">
                                            <%--<h1 class="TituloPanelTitulo">Vista Previa Reporte</h1>--%>
                                        </asp:Panel>

                                        <%--<telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
                                                        AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  
                                                        OnNeedDataSource = "RadGrid_NeedDataSource" Visible ="false">
                                             <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                               <Selecting AllowRowSelect="false" ></Selecting>
                                                 <Scrolling AllowScroll="True"  UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>                                               
                                               </ClientSettings>
                                               <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                               <MasterTableView>
                                                 <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />                                               
                                               </MasterTableView>
                                      </telerik:RadGrid>  --%>
                                    </asp:Panel>
                                </telerik:RadPageView>



                                <telerik:RadPageView runat="server" ID="RadPageView2">
                                    <asp:Panel ID="Panel2" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_DetalleSolicitud">
                                            <h1 class="TituloPanelTitulo">Datos Lineas Detalle Solicitud</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>

                                                <asp:Button ID="btnAgregar2" runat="server" Text="Agregar" Width="150px" OnClick="btnAgregar2_Click" />
                                                <asp:Button ID="btnEditar2" runat="server" Text="Editar" Width="150px" OnClick="btnEditar2_Click" />
                                                <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" Width="150px" />
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Num. Linea Detalle Solicitud"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidLineaDetalleSolicitud" runat="server" Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Maestro Solicitud"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidMaestroSolicitud" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Nombre Linea"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtNombre0" runat="server" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Artículo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%--<asp:DropDownList ID="DdlIdarticuloInterno" Class="TexboxNormal"  runat="server" Width="400px" AutoPostBack="True"></asp:DropDownList>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" Text="Destino"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidDestino0" Class="TexboxNormal" runat="server" Width="400px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCantidad" runat="server" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Descripcion"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txmDescripcion" runat="server" Width="400px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleSolicitud0">
                                            <h1 class="TituloPanelTitulo">Listado Destinos</h1>
                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize="15"
                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" OnNeedDataSource="RadGrid_NeedDataSource">
                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="false"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                            </ClientSettings>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <MasterTableView>
                                                <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="15,20,30,50" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPageView>
                                <telerik:RadPageView runat="server" ID="RadPageView3">
                                    <asp:Panel ID="Panel3" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_DestinoRestricciones">
                                            <h1 class="TituloPanelTitulo">Restricciones horario Destino</h1>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <asp:Button ID="BtnAgregar3" runat="server" Text="Agregar" Width="150px" OnClick="BtnAgregar3_Click" />
                                                <asp:Button ID="BtnEditar3" runat="server" Text="Editar" Width="150px" OnClick="BtnEditar3_Click" />
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Num. Restriccion Horario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidDestinoRestriccion" runat="server" Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:Button runat="server" ID="Button4" Text="Buscar" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Destino"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidDestino" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox1" runat="server" Width="250px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Dia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidDia" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Hora Min"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidHoraMinima" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="Hora Max"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidHoraMaxima" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DestinoRestricciones0">
                                            <h1 class="TituloPanelTitulo">Listado Restricciones por Destino</h1>
                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGrid3" runat="server" AllowMultiRowSelection="false" PageSize="10" OnItemCommand="RadGrid_ItemCommand"
                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" OnNeedDataSource="RadGrid_NeedDataSource">
                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="false"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                            </ClientSettings>
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                            <MasterTableView>
                                                <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
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
