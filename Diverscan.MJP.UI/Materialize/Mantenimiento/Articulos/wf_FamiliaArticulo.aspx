<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_FamiliaArticulo.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_FamiliaArticulo" %>
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
                                    <telerik:RadTab Text="Familia Articulos" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 2" Width="200px" Visible ="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 3" Width="200px" Visible ="false"> </telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_FamiliaArticulo">
                                                <h1 class="TituloPanelTitulo">Mantenimiento Familia Articulos</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                       <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                        <asp:Label ID="Label1" runat="server" Text="|||"></asp:Label>
                                                    	<asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click"/> 
                                                        <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                        <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar"  OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar1_Click" />     
                                                    <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                 </div>


                                           <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                            <tr>
                                                 <td>
                                                    <asp:Label ID="Label15" runat="server" Text="ID Registro"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidFamilia" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar"  OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label ID="Label16" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="150px" ></asp:TextBox>
                                                </td>
                                                <td>
                                                      <asp:CheckBox runat="server" ID="chktoxico" Text="Toxico" />
                                                </td>
                                            </tr>
                                                                                          
                                              </table>  
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>

                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_FamiliaArticulo0">
                                                <h1 class="TituloPanelTitulo">Lista Estado Transaccional</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand" OnPreRender="RadGrid1_Prerender"  
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
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel7">
                                            <h1 class="TituloPanelTitulo">Titulo Tab 2</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    <asp:Button  ID="Button5" runat ="server" Text ="Button5" Width ="150px" />
                                                    <asp:Button ID ="Button6" runat ="server" Text ="Button6" Width ="150px" />      
                                            </asp:Panel>
                                          <h1></h1>                                     
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>                                   
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                            <tr>
                                                 <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Usuario"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="TextBox1" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label ID="Label9" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="TextBox2" Class="TexboxNormal" Width="150px" ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Apellidos"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="TextBox3" Width="250px" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>      
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox4" runat="server" Width="305px" ></asp:TextBox>  
                                                </td>
                                                  <td> 
                                                    <asp:Label ID="Label12" runat="server" Text="Rol"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                            </tr>  
                                             <tr>      
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="Pass"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox5" runat="server" Width="305px" visible ="false"></asp:TextBox>  
                                                </td>      
                                            </tr>                       
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Comentarios"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox6" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Bloqueado" />
                                                </td>
                                                                           
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox2" Text="Administración de programas" />
                                                </td>
                                            </tr>
                                            <tr>  
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox3" Text="Usuario programa provisional" Visible="True" />
                                                </td>
                                                <td></td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox4" Text="Recibir correo de pedidos"  Visible="True"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                      
                                                </td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox5" Text="Aprobador"  Visible="True"/>
                                                </td>
                                            </tr>                                              
                                              </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Roles">
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
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel5">
                                                <h1 class="TituloPanelTitulo">Titulo Tab 3</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                <%--    <asp:Button ID ="Button2" runat ="server" Text ="Boton 2" Width ="150px" />
                                                    <asp:Button ID ="Button3" runat ="server" Text ="Boton 3" Width ="150px" />   --%>   
                                                    </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    

                               
                                              
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