<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Traslados.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Traslados.wf_Traslados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
                <script type='text/javascript'>
                    function DisplayLoadingImage1()
                        {
                            document.getElementById("loading1").style.display = "block";
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
                                    <telerik:RadTab Text="Traslados" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Detalle Alisto" Width="200px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 3" Width="200px"  Visible="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                                <h1 class="TituloPanelTitulo">Aplicar Traslados</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                            <ContentTemplate>
                                                <asp:Button  ID = "btnTrasladar" runat ="server" Text= "Trasladar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnTrasladar_Click" />
                                                <asp:Label ID="Label2" runat="server" Text="|||"></asp:Label>
                                                <asp:Button  ID = "BtnLimpiar" runat="server" Text="Limpiar form"  OnClientClick="DisplayLoadingImage1()" OnClick ="BtnLimpiar_Click" />
                                                <h1></h1>

                                                
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                 </div>


                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label1" runat="server" Text="COD BARRAS"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox runat="server" ID="txtCODBARRAS" Width="500px" AutoPostBack="true" OnTextChanged="txtCODBARRAS_TextChanged"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox runat="server" ID="TxtNombrearticulo" Width="500px" AutoPostBack="true" BackColor="Black" ForeColor="White" Font-Bold="true"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label20" runat="server" Text="Ubicación Actual:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox runat="server" ID="txtUbicacionActual" Width="200px" AutoPostBack="true" OnTextChanged="txtUbicacionActual_TextChanged"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label3" runat="server" Text="Ubicación a Mover:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox runat="server" ID="txtUbicacionMover" Width="200px" AutoPostBack="true" OnTextChanged="txtUbicacionMover_TextChanged"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                   <td>
                                                     <asp:TextBox ID="TxtIdarticulo" runat="server" Visible="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtBodega" runat="server" Visible="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtLote" runat="server" Visible="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtFV" runat="server" Visible="false"></asp:TextBox>
                                                   </td>
                                                </table>
                                                </ContentTemplate> 
                                                 <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroSolicitud0" Visible ="false" >
                                                <h1 class="TituloPanelTitulo">Aplica Traslados</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" OnPreRender="RadGrid1_PreRender">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="false">
                                                             <Selecting AllowRowSelect="false" ></Selecting> 
                                                            <Scrolling AllowScroll="True"  UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>                                               
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
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_DetalleSolicitud">
                                            <h1 class="TituloPanelTitulo">Datos Lineas Detalle Alisto</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>   
                                                    
                                                 <asp:Button  ID="btnAgregar2" runat ="server" Text ="Agregar" Width ="150px" OnClick ="btnAgregar2_Click"/>
                                                 <asp:Button ID ="btnEditar2" runat ="server" Text ="Editar" Width ="150px" OnClick ="btnEditar2_Click" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                    <tr>
                                                    <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Num. Linea Detalle Alisto"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidLineaDetalleSolicitud" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                    </td>
                                                    </tr>
                                                    <tr> 
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Maestro Solicitud"></asp:Label> 
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlidMaestroSolicitud" Class="TexboxNormal"  runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="Nombre Linea"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txtNombre0" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    </td>
                                                    </tr>                             
                                                    <tr> 
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Artículo"></asp:Label> 
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlidArticulo" Class="TexboxNormal"  runat="server" Width="400px" AutoPostBack="True"></asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                     <tr> 
                                                    <td>
                                                        <asp:Label ID="Label18" runat="server" Text="Destino"></asp:Label> 
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlidDestino0" Class="TexboxNormal"  runat="server" Width="400px" AutoPostBack="True"></asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Cantidad"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txtCantidad0" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="Descripcion"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txmDescripcion" runat="server" Width="400px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    </td>
                                                    </tr>
                                                 </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleSolicitud0">
                                                    <h1 class="TituloPanelTitulo">Listado Alistos</h1>
                                                </asp:Panel>                                    
                                         <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize ="15" 
                                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" >
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
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_DestinoRestricciones">
                                                <h1 class="TituloPanelTitulo">Restricciones horario Destino</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    <asp:Button ID ="BtnAgregar3" runat ="server" Text ="Agregar" Width ="150px" OnClick="BtnAgregar3_Click"/>
                                                    <asp:Button ID ="BtnEditar3" runat ="server" Text ="Editar" Width ="150px" OnClick="BtnEditar3_Click" />      
                                            <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                                <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Num. Restriccion Horario"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidDestinoRestriccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button4" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                                </tr>
                                                <tr> 
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Destino"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidDestino" Class="TexboxNormal"  runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox1" runat="server" Width="250px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                                </tr>  
                                                 <tr> 
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="Dia"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidDia" Class="TexboxNormal"  runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                </tr> 
                                                 <tr> 
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Hora Min"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidHoraMinima" Class="TexboxNormal"  runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                </tr>
                                                <tr> 
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="Hora Max"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidHoraMaxima" Class="TexboxNormal"  runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                </tr>                                                                                   
                                                </table>  
                                              
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_DestinoRestricciones0">
                                                <h1 class="TituloPanelTitulo">Listado Restricciones por Destino</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"     
                                                        AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" >
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                             <Selecting AllowRowSelect="false" ></Selecting>
                                                            <Scrolling AllowScroll="True"  UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>                                               
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

