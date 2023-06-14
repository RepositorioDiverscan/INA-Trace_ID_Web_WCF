<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_MantenimientoProvedor.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Ingresos.Provedores.wf_MantenimientoProvedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


                <script type='text/javascript'>
                            function DisplayLoadingImage1() {
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
                                    <telerik:RadTab Text="Provedores" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Actividad" Width="200px" Visible ="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Acciones" Width="200px" Visible ="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                        
                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Provedores">
                                                <h1 class="TituloPanelTitulo">Mantenimiento Provedor</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                    <asp:Label ID="Label15" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px" OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click"/>                                                    
                                                    <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar1_Click" />
                                                             
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
                                                                <asp:Label ID="Label3" runat="server" Text="Provedor"></asp:Label> 
                                                            </td>
                                                            <td >     
                                                                <asp:TextBox CssClass="TextBoxBusqueda" ID="txtIdProveedor" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label2" runat="server" Text="Compania"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlIdCompania" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                           </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                            </td>
                                               
                                                        </tr>                                                                                                
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Descripción"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtDescripcion" runat="server" Width="300px" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text="Fecha Creacion"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtFechaCreacion" runat="server" Width="120px" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td> 
                                                                <asp:Label ID="Label14" runat="server" Text="Usuario"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlIDUSUARIO" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                           </tr>
                                                        
                                                        <tr>
                                                            <td>
                                                                  <asp:CheckBox runat="server" Text="Producto OK" ID="chkSincronizaERP"/>
                                                            </td>   
                                                        </tr>   
                                                                                                                                  
                                                    </table>                                             
                                                </ContentTemplate>
                                                 <Triggers>  
                                                </Triggers>
                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Provedores0">
                                                <h1 class="TituloPanelTitulo">Listado de Artículos</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand" OnPreRender="RadGrid1_Prerender"   
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
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Actividades">
                                            <h1 class="TituloPanelTitulo">Datos Actividad</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>   
                                                    
                                                 <asp:Button  ID="btnAgregar2" runat ="server" Text ="Agregar" Width ="150px" />
                                                 <asp:Button ID ="btnEditar2" runat ="server" Text ="Editar" Width ="150px" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Num Actividad"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidActividad" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            <asp:Button runat="server" ID="Button1" Text="Buscar"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <td> 
                                                                <asp:Label ID="Label4" runat="server" Text="Proceso"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidProceso" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                           </tr>         
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Nombre"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtNombre0" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                        </td>
                                                    </tr>           
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Descripcion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txmDescripcion0" runat="server" Width="400px"  TextMode="MultiLine"  Height="75px" ></asp:TextBox>   
                                                        </td>
                                                    </tr>
                                                 
                                                 </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Actividades0">
                                                    <h1 class="TituloPanelTitulo">Listado Actividades</h1>
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
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Acciones">
                                                <h1 class="TituloPanelTitulo">Datos Acciones</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    </asp:Panel>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>                           
                                                 <asp:Button  ID="Button4" runat ="server" Text ="Agregar" Width ="150px"/>
                                                 <asp:Button ID ="Button5" runat ="server" Text ="Editar" Width ="150px" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                                  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Num Accion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            <asp:Button runat="server" ID="Button6" Text="Buscar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <td> 
                                                                <asp:Label ID="Label7" runat="server" Text="Actividad"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidActividad" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                           </tr>         
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Nombre"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox1" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                        </td>
                                                    </tr>           
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox2" runat="server" Width="400px"  TextMode="MultiLine"  Height="75px" ></asp:TextBox>   
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:Label ID="LabelFormulario" runat="server" Text="Formulario de aplicacion"></asp:Label>
                                                         </td>
                                                         <td>
                                                            <asp:DropDownList runat="server" CssClass="TexboxNormal" ID ="ddlidFormulario" Width="600px" AutoPostBack="false"></asp:DropDownList>
                                                         </td>
                                                       </tr>
                                                     <tr> 
                                                        <td>
                                                          <asp:Label ID="LabelObjetosForm" runat="server" CssClass="TexboxNormal" Text="Objeto sobre la accion"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList runat="server" ID ="ddlidObjetoFormulario" CssClass="TexboxNormal" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                                        </td>                                              
                                                     </tr>
                                                 <h1></h1>
                                                 </table>                               
                                              </ContentTemplate>
                                                 <Triggers>                                             
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Acciones0">
                                                <h1 class="TituloPanelTitulo">Listado de Acciones</h1>
                            
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

