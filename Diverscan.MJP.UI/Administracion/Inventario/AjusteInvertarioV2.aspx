<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjusteInvertarioV2.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.AjusteInvertarioV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >   
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

                    <div id="RestrictionZoneID" class="WindowContenedor">
             
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows >
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Solicitar Ajuste Invertario" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Ajustes Solicitados" Width="200px"></telerik:RadTab> 
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >

                                     <telerik:RadPageView runat="server" ID="RadPageView2">
                                        <asp:Panel ID="Panel2" runat="server" Class="TabContainer">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                <ContentTemplate>

                                                    <asp:RadioButtonList runat="server" ID="_rblSolicitudTipo" OnSelectedIndexChanged="_rblSolicitudTipo_SelectedIndexChanged" AutoPostBack="true">                                                        
                                                        <asp:ListItem Selected ="True">Ajuste De Entrada</asp:ListItem>
                                                        <asp:ListItem>Ajuste De Salida</asp:ListItem>
                                                        <%--<asp:ListItem>Ajuste De Traslado</asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                     <h1></h1>                                                        
                                                            <asp:label ID="Label5" runat ="server" Text="Motivo: "></asp:label>                                                        
                                                            <asp:DropDownList runat="server" ID ="_ddlMotivoSolicitud"></asp:DropDownList>
                                                     <h1></h1>
                                                    <asp:Button  ID = "_btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClick="_btnAgregar_Click"/>
                                                    <asp:Label ID="Label3" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button  ID = "_btnLimpiarSolicitud" runat="server" Text="Limpiar form" OnClick ="_btnLimpiarSolicitud_Click" />

                                                     <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                        <h1></h1>
                                                        <tr>
                                                        <td>
                                                          <asp:Label ID="Label4" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox runat="server" ID="_txtS_CodigoBarras" Width="500px"></asp:TextBox>
                                                        </td>
                                                          </tr>
                                                          <tr>
                                                            <td>
                                                              <asp:Label ID="_lblS_UbicacionActual" runat="server" Text="Ubicación:"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:TextBox runat="server" ID="_txtS_UbicacionActual" Width="200px"></asp:TextBox>
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td>
                                                              <asp:Label ID="_lblS_UbicacionMover" runat="server" Text="Ubicación a Mover:" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:TextBox runat="server" ID="_txtS_UbicacionMover" Width="200px" Visible="false"></asp:TextBox>
                                                            </td>
                                                          </tr>
                                                         <tr>                                                       
                                                       
                                                             <td>
                                                                <asp:Button runat="server" Id="_btnSolicitarAjuste" Text="Solicitar" OnClick="_btnSolicitarAjuste_Click"/>
                                                            </td>
                                                    </tr>
                                                    </table>

                                                    <telerik:RadGrid ID="_rgArticulosXSolicitud" AllowPaging="True" Width="100%"  OnNeedDataSource ="_rgArticulosXSolicitud_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="CodigoInterno"
                                                                    SortExpression="CodigoInterno" HeaderText="Codigo" DataField="CodigoInterno">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="Unidad Inventario" DataField="UnidadMedida">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadToShow"
                                                                    SortExpression="CantidadToShow" HeaderText="Cantidad" DataField="CantidadToShow">
                                                                </telerik:GridBoundColumn>                                                                                                                            

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </telerik:RadPageView>



                                     <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                                <h1 class="TituloPanelTitulo">Aplicar Traslados</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                            <ContentTemplate>                                                
                                                 <tr>
                                                          <td>
                                                        <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        <telerik:RadDatePicker ID="_rdpA_fechaInicio" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="_rdpA_fechaFin" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>                                                            
                                                        </td> 
                                                        <td>
                                                             <asp:Label ID="LBLEstado" runat="server" Text="Estado"></asp:Label>
                                                             <telerik:RadDropDownList runat="server" ID ="RDDLEstado"></telerik:RadDropDownList>
                                                         </td>
                                                         <td>
                                                             <telerik:RadButton runat="server" Text="Buscar" ID ="btnBuscar"  OnClick="btnBuscar_Click"></telerik:RadButton>
                                                         </td>

                                                    </tr>

                                                 <telerik:RadGrid ID="RGSolicitudAjustesInventario" AllowPaging="True" Width="100%" 
                                                          OnNeedDataSource ="RGSolicitudAjustesInventario_NeedDataSource" 
                                                         OnItemCommand="RGLogAjustesInventario_ItemCommand"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10">

                                                         <ClientSettings  EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>

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

                                                                 <telerik:GridBoundColumn UniqueName="CentroCosto"
                                                                    SortExpression="CentroCosto" HeaderText="CentroCosto" DataField="CentroCosto">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>   
                                                            
                                                    </telerik:RadGrid>
                                             
                                                 <telerik:RadGrid ID="_rgArticulosXSolicitudDetalle" AllowPaging="True" Width="100%"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10">                                                         
                                                         <MasterTableView>
                                                            <Columns>  
                                                                
                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudAjusteInventario"
                                                                    SortExpression="IdSolicitudAjusteInventario" HeaderText="Id Solicitud" DataField="IdSolicitudAjusteInventario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CodigoInterno"
                                                                    SortExpression="CodigoInterno" HeaderText="Codigo" DataField="CodigoInterno">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>                                  

                                                                 <telerik:GridBoundColumn UniqueName="Lote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaVencimiento"
                                                                    SortExpression="FechaVencimiento" HeaderText="FechaVencimiento" DataField="FechaVencimiento">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                                </telerik:GridBoundColumn> 

                                                                <telerik:GridBoundColumn UniqueName="EtiquetaActual"
                                                                    SortExpression="EtiquetaActual" HeaderText="Ubicacion" DataField="EtiquetaActual">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="PrecioUnidad"
                                                                    SortExpression="PrecioUnidad" HeaderText="CostoUnidad" DataField="PrecioUnidad">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="PrecioXCantidad"
                                                                    SortExpression="PrecioXCantidad" HeaderText="CostoTotal" DataField="PrecioXCantidad">
                                                                </telerik:GridBoundColumn>
                                                               
                                                            </Columns>
                                                        </MasterTableView>   
                                                            
                                                    </telerik:RadGrid>
                                                    <asp:Label runat="server" ID="_lblCostoAjuste"  ForeColor="Red"></asp:Label>
                                                </ContentTemplate>                                               
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
