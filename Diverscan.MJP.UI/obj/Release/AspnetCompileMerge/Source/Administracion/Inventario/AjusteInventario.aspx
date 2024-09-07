<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjusteInventario.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.AjusteInventario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
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
            </AjaxSettings>
        </telerik:RadAjaxManager>

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
                                    <telerik:RadTab Text="Ajuste Inventario" Width="200px" Visible="true"></telerik:RadTab>
                                    <telerik:RadTab Text="Catálogo Ajustes" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Aplicar Ajuste" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_TRAIngresoSalidaArticulos">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Estado Transaccional</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <h1></h1>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="150px" OnClick="btnAgregar_Click" />
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar" Width="150px" OnClick="btnEditar_Click" />
                                                <h1></h1>
                                                <asp:CheckBox runat="server" ID="chkProcesado" Text="Procesado" />
                                                <table style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;">
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label35" runat="server">ID Registro</asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRegistro" runat="server" 
                                                                Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false"
                                                                OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                        </td>
                                                        <td style="width:130px"">
                                                            
                                                        </td>
                                                         <td style="width:130px"">
                                                             
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label37" runat="server" Text="Tipo Ajuste"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:DropDownList ID="ddTipoAjuste" runat="server" Width="200px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddTipoAjuste_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label38" runat="server" Text="Tipo Ajuste"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox runat="server" ID="txtTipoAjuste" Class="TexboxNormal" Width="150px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label40" runat="server" Text="Artículo"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:DropDownList ID="ddlidArticulo" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label39" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtFechaVencimiento" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label46" runat="server" Text="Lote"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtLote" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label41" runat="server" Text="Usuario"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:DropDownList ID="ddlIDUSUARIO" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label42" runat="server" Text="Método Acción"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:DropDownList ID="ddlidMetodoAccion" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                        <td style="width:130px"">

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label43" runat="server" Text="Tabla Campo Doc. Acción"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtlidTablaCampoDocumentoAccion" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label47" runat="server" Text="Campo Documento Acción "></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidCampoDocumentoAccion" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label44" runat="server" Text="Núm. Documento Acción"></asp:Label>    
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtNumDocumentoAccion" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label48" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCantidad" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label45" runat="server" Text="Ubicación"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidUbicacion" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:Label ID="Label49" runat="server" Text="Estado"></asp:Label>
                                                        </td>
                                                        <td style="width:130px"">
                                                            <asp:TextBox CssClass="TexboxNormal" ID="ddlIdEstado" runat="server" Width="305px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>

                                        </asp:UpdatePanel>
                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_TRAIngresoSalidaArticulos0">
                                            <h1 class="TituloPanelTitulo">Lista Estado Transaccional</h1>


                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowSelection="false" PageSize="3" OnPreRender="RadGrid1_PreRender" OnItemCommand="RadGrid_ItemCommand"
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
                                <telerik:RadPageView runat="server" ID="RadPageView2">
                                    <asp:Panel ID="Panel2" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_AjusteInventario">
                                            <h1 class="TituloPanelTitulo">Catalogo Inventario</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />

                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>

                                                <asp:Button ID="Button2" runat="server" Text="Agregar" Width="150px" OnClick="btnAgregar2_Click" />
                                                <asp:Button ID="Button3" runat="server" Text="Editar" Width="150px" OnClick="btnEditar2_Click" />

                                                <h1></h1>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="ID Ajuste Inventario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidAjusteInventario" runat="server" Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Código ERP"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtidCodigoERP" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Método Acción"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidMetodoAccion0" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Estado"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIdEstado0" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Fecha Creación"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="dtpFechaCreacion" runat="server" Width="305px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIDUSUARIO0" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>


                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Compañía"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIdCompania" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>


                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_AjusteInventario0">
                                            <h1 class="TituloPanelTitulo">Listado Catalogo Estado</h1>
                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize="15"
                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" OnPreRender="RagGrid2_PreRender" OnNeedDataSource="RadGrid_NeedDataSource">
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
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_AjustesAplicados">
                                            <h1 class="TituloPanelTitulo">Aplicar Ajuste</h1>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <%--  <asp:Button  ID="Button4" runat ="server" Text= "Agregar" Width ="150px" OnClick="BtnAgregar3_Click" />
                                            <asp:Button ID ="Button5"  runat ="server" Text= "Editar" Width ="150px"  OnClick="BtnEditar3_Click"/> --%>

                                                <asp:Button ID="Button7" runat="server" Text="Aplicar Ajuste" Width="150px" OnClick="Button7_Click" />

                                                <h1></h1>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="ID Ajuste Aplicado"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txidAjusteAplicado" runat="server" Width="85px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:Button runat="server" ID="Button6" Text="Buscar" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label29" runat="server" Text="Zona"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="didZona" runat="server" Width="200px"></asp:DropDownList>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" Text="idZona"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidZona" runat="server" Enabled="true" Width="60px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Artículo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddidArticulo" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlidArticulo_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" Text="Id Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidArticulo" runat="server" Width="60px"></asp:TextBox>
                                                        </td>

                                                    </tr>

                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Lote-FechaVencimiento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="dlLoteVencimiento" runat="server" Width="200px"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label30" runat="server" Text="Tipo Ajuste"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidAjusteInventario" runat="server" Width="200px"></asp:DropDownList>
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Usuario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIDUSUARIO00" runat="server" Width="200px"></asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label28" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCantidad0" runat="server" Width="65px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    </tr>   
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>

                                        </asp:UpdatePanel>

                                        <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_AjustesAplicados0">
                                            <h1 class="TituloPanelTitulo">Listado Ajustes Aplicados</h1>

                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGrid3" runat="server" AllowMultiRowSelection="false" PageSize="3" OnItemCommand="RadGrid_ItemCommand" OnPreRender="RadGrid3_PreRender"
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
