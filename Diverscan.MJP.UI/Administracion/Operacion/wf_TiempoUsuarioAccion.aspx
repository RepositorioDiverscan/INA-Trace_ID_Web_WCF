<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_TiempoUsuarioAccion.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Operacion.TiempoUsuarioAccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

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
                                    <telerik:RadTab Text="TiempoMaxUsuarioAccion" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Accesos por Rol" Visible="false" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Rol" Width="200px"  Visible="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                  
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_TiempoUsuarioAccion">
                                                <h1 class="TituloPanelTitulo">Usuario Accion</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <asp:Button ID ="btnAgregar" runat ="server" Text ="Agregar" Width ="150px" OnClick="btnAgregar_Click" />
                                                    <asp:Button ID ="btnEditar" runat ="server" Text ="Editar" Width ="150px" OnClick="btnEditar_Click" />
                                                    <h1></h1>

                                                  <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label9" runat="server" Text="ID Tiempo Usuario Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidTiempoMaxUsuarioAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                               </tr>

                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtNombre0" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                            </tr>
                                              <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text ="Usuario"></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:DropDownList ID="ddlIDUSUARIO" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="dtpFecha" Class="TexboxNormal" Width="150px" ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Comentarios"></asp:Label>
                                                
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtComentario" Width="250px" Height="50px" ></asp:TextBox>
                                                </td>
                                            </tr>        
                                            <tr>      
                                                
                                                  <td> 
                                                    <asp:Label ID="Label5" runat="server" Text="Metodo Accion"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidMetodoAccion" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                           
                      
                                              </table>  
                                              
                                                </ContentTemplate>
                                                 <Triggers>
                                                   
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_TiempoUsuarioAccion0">
                                                <h1 class="TituloPanelTitulo">Detalle Usuarios</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand"   
                                                        AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource">
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


                                <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_RolMetodoAccion">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Accesos Rol</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                         
                                            </asp:Panel>
                                          <h1></h1>                                     
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>                                   
                                           
                                                    <asp:Button ID ="Button2" runat ="server" Text ="Agregar usuario" Width ="150px" OnClick="btnAgregar2_Click" />
                                                    <asp:Button ID ="Button5" runat ="server" Text ="Editar usuario" Width ="150px" OnClick="btnEditar_Click" />

                                                     <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                               <tr>
                                                 <td>
                                                    <asp:Label ID="Label17" runat="server" Text="Rol Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRolMetodoAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button7" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                            
                                            <tr>      

                                                
                                                  <td> 
                                                    <asp:Label ID="Label21" runat="server" Text="Accion"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidAccion" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                            </tr>  
                                                                 
                                            <tr>

                                                <tr>      
                               
                                                  <td> 
                                                    <asp:Label ID="Label18" runat="server" Text="Rol"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidRol0" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                            </tr>  
                                                                 
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label23" runat="server" Text="Comentarios"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtDescripcion" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                          
                                            </tr>


                                            
                                                                                  
                                              </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_RolMetodoAccion0">
                                                    <h1 class="TituloPanelTitulo">Listado roles</h1>
                                                </asp:Panel>                                    
                                         <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize ="15" 
                                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource">
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
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Roles">
                                                <h1 class="TituloPanelTitulo">Datos Rol</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                         
                                                    </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    <asp:Button ID ="Button3" runat ="server" Text ="Agregar" Width ="150px" OnClick ="btnAgregar3_Click" />
                                                    <asp:Button ID ="Button4" runat ="server" Text ="Editar" Width ="150px" OnClick ="btnEditar3_Click" /> 
                                              
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">  
                                                  <tr>
                                                 <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Rol Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRol" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button6" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                                 </tr>
                                            
                                               <tr>
                                                 <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtNombre" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                               
                                              </tr>   
                                                    
                                              <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtDescripcion0" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                          
                                            </tr>

                                                        </table>
  
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Roles0">
                                                <h1 class="TituloPanelTitulo">Titulo para Detalles del GRID</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand"   
                                                        AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource">
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
