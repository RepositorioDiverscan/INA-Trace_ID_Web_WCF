<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_MaestroOrdenCompra.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Ingreso.wf_MaestroOrdenCompra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                <script type='text/javascript'>
                            function DisplayLoadingImage1() {
                                document.getElementById("loading1").style.display = "block";
                            }
                            function DisplayLoadingImage2() {
                                document.getElementById("loading2").style.display = "block";
                            }
                            function DisplayLoadingImage3() {
                                document.getElementById("loading3").style.display = "block";
                            }
                            function DisplayLoadingImage4() {
                                document.getElementById("loading4").style.display = "block";
                            }
                </script>

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
                                    <telerik:RadTab Text="Maestro Orden Compra" Width="200px"></telerik:RadTab>
                               
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                        
                                 <%-- Maestro Orden compra --%>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OrdenesCompra">
                                                <h1 class="TituloPanelTitulo">Datos orden de compra</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>
                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClientClick="DisplayLoadingImage1()"  OnClick="btnAgregar_Click"  />
                                                    <asp:Label ID="Label5" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px" OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click"/>  
                                                    <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()"  OnClick ="Btnlimpiar1_Click" />    
                                                    <h1></h1>

                                                    <%--
                                                        OnClick="btnAgregar_Click"
                                                        OnClick="btnEditar_Click"
                                                        OnClick ="Btnlimpiar1_Click"
                                                    --%>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                      
                                                        <tr>
                                                        <td>
                                                          <asp:Label ID="Label3" runat="server" Text="Num. Maestro OC"></asp:Label> 
                                                        </td>
                                                        <td>     
                                                          <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidMaestroOrdenCompra" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>
                                                          <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClientClick="DisplayLoadingImage1()"   />
                                                        </td>
                                                      </tr>
                                                      <tr>
                                                        <td> 
                                                          <asp:Label ID="Label2" runat="server" Text="Compania" Visible = "false"></asp:Label>       
                                                        </td>
                                                        <td>
                                                          <asp:TextBox ID="txtIdCompania" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false" Visible ="true"></asp:TextBox>
                                                         </td>   
                                                      </tr>
                                                      <tr>
                                                        <td> 
                                                          <asp:Label ID="Label13" runat="server" Text="Proveedor"></asp:Label>       
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList ID="ddlIdProveedor" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlIdProveedor_SelectedIndexChanged" ></asp:DropDownList>
                                                            <%--OnSelectedIndexChanged="ddlIdProveedor_SelectedIndexChanged" --%>
                                                        </td>   
                                                      </tr>
                                                      <tr>
                                                        <td> 
                                                          <asp:Label ID="Label1" runat="server" Text="Orden Compra Proveedor:"></asp:Label> 
                                                        </td>
                                                        <td>
                                                          <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                        </td>
                                                      </tr>   
                                                      <tr>
                                                        <td> 
                                                          <asp:Label ID="Label14" runat="server" Text="Fecha Emisión:" Enabled ="false"></asp:Label> 
                                                        </td>
                                                        <td>
                                                          <telerik:RadDatePicker ID="dtpFechaCreacion" runat="server"></telerik:RadDatePicker>
                                                        </td>
                                                      </tr>  
                                                      <tr>
                                                        <td> 
                                                          <asp:Label ID="Label22" runat="server" Text="Fecha Recepción:" Enabled ="false"></asp:Label> 
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtFechaDespacho" runat="server"></telerik:RadDatePicker>                                                                   
                                                          </td>                                            
                                                        </tr>                                                                                   
                                                        <tr>
                                                        <td>
                                                          <asp:Label ID="Label6" runat="server" Text="Comentario"></asp:Label>  
                                                        </td>
                                                        <td>
                                                          <asp:TextBox CssClass="TexboxNormal" ID="txmComentario" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                        </td>
                                                             <%--<td style="display:none"><asp:TextBox CssClass="TexboxNormal" ID="txtProcesada" runat="server" Visible="false"></asp:TextBox></td>--%>
                                                             <%--<td style="display:none"><asp:TextBox CssClass="TexboxNormal" ID="i" runat="server"></asp:TextBox></td>--%>
                                                      </tr>                                                                
                                                    </table>   
                                                                                                
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel7">
                                                      <h1 class="TituloPanelTitulo">Datos Detalle Ordene de Compra</h1>
                                                      <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    </asp:Panel>      
                                                    
                                                <asp:Button  ID="Button3" runat ="server" Text ="Agregar" Width ="150px" OnClientClick="DisplayLoadingImage2()"  OnClick ="btnAgregar2_Click" 
                                                  />
                                               <%-- <asp:Label ID="Label28" runat="server" Text="|||" Visible = "false"></asp:Label>--%>
                                                <asp:Button ID ="Button4" runat ="server" Text ="Editar" Width ="150px" OnClientClick="DisplayLoadingImage2()"   OnClick ="btnEditar2_Click"
                                                    Visible = "false" />
                                                <asp:Label ID="Label30" runat="server" Text="|||" Visible="false"></asp:Label>
                                                <asp:Button ID ="BtnElimina" runat ="server" Text ="Eliminar" Width ="150px" OnClientClick="DisplayLoadingImage2()" OnClick ="BtnElimina_Click"
                                                    Visible = "false" />
                                                <asp:Label ID="Label4" runat="server" Text="|||" ></asp:Label>
                                                <asp:Button ID="Button5" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage2()"  OnClick ="Btnlimpiar2_Click"
                                                    />                                      
                                                 <h1></h1>

                                                    <%--
                                                       OnClick ="btnAgregar2_Click" 
                                                         OnClick ="btnEditar2_Click"
                                                         OnClick ="BtnElimina_Click" OnClick ="Btnlimpiar2_Click"
                                                    --%>

                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                   <tr>
                                                     <td> 
                                                       <asp:Label ID="Label10" runat="server" Text="Artículo"></asp:Label>       
                                                     </td>
                                                     <td>
                                                       <asp:DropDownList ID="ddlidArticuloInterno" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                       <asp:TextBox runat="server" ID="TxtNumFilaGrid" Class="TexboxNormal" Width="10px" Visible="false" ></asp:TextBox>
                                                     </td>   
                                                   </tr>
                                                   <tr>
                                                     <td> 
                                                       <asp:Label ID="Label23" runat="server" Text="Cant Recibir"></asp:Label> 
                                                     </td>
                                                     <td>
                                                       <asp:TextBox runat="server" ID="txtCantidadxRecibir" Class="TexboxNormal" Width="80px" ></asp:TextBox>
                                                     </td>
                                                   </tr>  
                                                 </table>

                                                 <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleOrdenCompra">
                                                    <h1 class="TituloPanelTitulo">Listado Detalle Orden Compra</h1>
                                                 </asp:Panel>
                                               
                                                <%-- OnNeedDataSource="RadGridOPEINGDetalleordencompra_NeedDataSource"
                                                    OnItemCommand="RadGridOPEINGDetalleOrdenCompra_ItemCommand"--%>
                                                <telerik:RadGrid
                                                    ID="RadGridOPEINGDetalleOrdenCompra"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                    OnNeedDataSource="RadGridOPEINGDetalleordencompra_NeedDataSource"
                                                    OnItemCommand="RadGridOPEINGDetalleOrdenCompra_ItemCommand"
                                                    runat="server"
                                                    AllowFilteringByColumn="true"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true"
                                                    EnableDragToSelectRows="false"
                                                    >
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="IdDetalleOC"
                                                                SortExpression="IdDetalleOC"
                                                                HeaderText="Id Detalle" DataField="IdDetalleOC"
                                                                AllowFiltering="True"
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idArticuloInterno"
                                                                SortExpression="idArticuloInterno" 
                                                                HeaderText="idArticuloInterno" 
                                                                DataField="idArticuloInterno"
                                                                AllowFiltering="True" 
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreArticuloInterno"
                                                                SortExpression="NombreArticuloInterno" 
                                                                HeaderText="Nombre del Articulo"
                                                                DataField="NombreArticuloInterno"
                                                                AllowFiltering="True" 
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" 
                                                                HeaderText="Cantidad" 
                                                                DataField="Cantidad"
                                                                AllowFiltering="True" 
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                          <%--  <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                SortExpression="UnidadMedida" 
                                                                HeaderText="Unidad Medida" 
                                                                DataField="UnidadMedida"
                                                                AllowFiltering="True" 
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="UnidadesAlisto"
                                                                SortExpression="UnidadesAlisto" 
                                                                HeaderText="Unidades Alisto" 
                                                                DataField="UnidadesAlisto"
                                                                AllowFiltering="True" 
                                                                AutoPostBackOnFilter="True" 
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>--%>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>


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