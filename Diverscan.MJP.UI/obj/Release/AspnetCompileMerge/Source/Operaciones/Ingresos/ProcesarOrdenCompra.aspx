<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesarOrdenCompra.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Ingresos.ProcesarOrdenCompra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
                             <telerik:AjaxSetting AjaxControlID="RadGrid3">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid3"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                            <telerik:AjaxSetting AjaxControlID="RadListBox1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadListBox1"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                            <telerik:AjaxSetting AjaxControlID="RadListBox1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadListBox2"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                            <telerik:AjaxSetting AjaxControlID="RadListBox2">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadListBox2"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                            <telerik:AjaxSetting AjaxControlID="RadListBox2">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadListBox1"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                             <telerik:AjaxSetting AjaxControlID="RadAjaxLoadingPanel1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
                                    <telerik:RadTab Text="Orden Compra" Width="200px" Enabled ="false" ></telerik:RadTab>
                                    <telerik:RadTab Text="Orden Compra HH" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 3" Width="200px"  Visible ="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>

                   <%-- Panel Orden Compra --%>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1" Enabled ="false" >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_DetalleOrdenCompra">
                                                <h1 class="TituloPanelTitulo">Orden de Compra</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <asp:Button  ID="btnAccion1"  runat ="server" Text= "Accion 1" Width ="150px" Visible="false" />
                                                    <asp:Button  ID="btnAccion2"  runat ="server" Text= "Accion 2" Width ="150px" Visible="false" />
                                                    <h1></h1>
                                                    <table width="450px"  style="border-radius: 2px; border: 1px solid darkblue; border-collapse: initial; margin-left:2px;" id="Table1">                      
                                                        <tr>
                                                             <td>
                                                                <asp:Label ID="Label3" class="Labels" runat="server" Text="Num. Orden Compra"></asp:Label>                                                            
                                                                <asp:TextBox CssClass="TextBoxBusqueda" ID="ddlidMaestroOrdenCompra"  runat="server" AutoCompleteType="Disabled" Text="2" ></asp:TextBox>   
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
                                                            </td>
                                                        </tr>                                                     
                                                    </table>                                             
                                                </ContentTemplate>
                                                 <Triggers>  
                                                </Triggers>
                                            </asp:UpdatePanel> 
                                        <asp:Panel runat="server" ID="PanelRadlist">
                                        <table class="Lista" width ="100%">
                                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"></telerik:RadAjaxLoadingPanel>
                                             <div>
			                                <td id="Primero">
                                                <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBox1"  AutoPostBack="True"  CssClass ="RadListBox"
                                                    Height="200px" OnSelectedIndexChanged="RadListBox1_SelectedIndexChanged" OnDeleted="RadListBox1_Deleted"
                                                    OnDeleting="RadListBox1_Deleting" OnInserted="RadListBox1_Inserted" OnInserting="RadListBox1_Inserting"
                                                    OnTransferred="RadListBox1_Transferred" OnTransferring="RadListBox1_Transferring" ButtonSettings-ShowTransferAll ="false" 
                                                    TransferToID="RadListBox2" AllowTransfer="true" AutoPostBackOnTransfer="true" AutoDataBind  ="false"
                                                    SelectionMode="Single" AllowAutomaticUpdates="True"  LoadingPanelID="<%# RadAjaxLoadingPanel1.ClientID %>">
                                                   <itemtemplate>
                                                    <span class="detail-title">  <%# DataBinder.Eval(Container, "text")%></span>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="Qtxt11" Width="100px" MinValue="0"
                                                        MaxValue="10000" ShowSpinButtons="false" Value="1" NumberFormat-DecimalDigits="2" >
                                                    </telerik:RadNumericTextBox>    
                                                    </itemtemplate>            
                                                </telerik:RadListBox>
                                            </td>
                                            <td id="Segundo">                     
                                                <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBox2" AutoPostBack="True"  CssClass ="RadListBox"
                                                    Height="200px" OnSelectedIndexChanged="RadListBox1_SelectedIndexChanged" OnDeleted="RadListBox2_Deleted"
                                                    OnDeleting="RadListBox2_Deleting" OnInserted="RadListBox2_Inserted" OnInserting="RadListBox2_Inserting"
                                                    TransferToID="RadListBox1" AllowTransfer="false" AutoPostBackOnTransfer="true"
                                                    SelectionMode="Single" AllowAutomaticUpdates="True">
                                                   <itemtemplate>
                                                    <span class="detail-title">  <%# DataBinder.Eval(Container, "text")%></span>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="Qtxt12" Width="100px" MinValue="0"
                                                        MaxValue="10000" ShowSpinButtons="true" Value="1" NumberFormat-DecimalDigits="2" >
                                                    </telerik:RadNumericTextBox>    
                                                    </itemtemplate>            
                                                </telerik:RadListBox>
                                               </td>  
                                             </div>
                                         </table>
                                       </asp:Panel>
                                       <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleOrdenCompra0">
                                         <h1 class="TituloPanelTitulo">Listado de Orden de compra</h1>
                                       </asp:Panel>
                                         <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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

                            <%-- Orden Compra HH --%>
                                        <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Actividades">
                                            <h1 class="TituloPanelTitulo">Datos Actividad</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                        </asp:Panel>                                   
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>   
                                                 <asp:Button ID ="btnAccion21" runat ="server" Text ="btnAccion21" Width ="200px" Visible="false"/>
                                                 <asp:Button ID ="btnAccion22" runat ="server" Text ="btnAccion22" Width ="150px" Visible="false"/> 
                                                 <asp:Label ID="Label2" runat="server" Text="|||"></asp:Label>
                                                 <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar Form" OnClick="BtnLimpiar_Click" />                                     
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                   <tr>
                                                     <td>
                                                       <asp:Label ID="Label1" runat="server" Text="Artículo:"></asp:Label> 
                                                     </td>
                                                     <td>     
                                                       <asp:TextBox CssClass="TexboxNormal" ID="txtCodigoLeido" runat="server" Width="500px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                       <%--<asp:DropDownList ID="DDLidMaestroOrdenCompra" runat="server"></asp:DropDownList>--%>
                                                     </td>
                                                   </tr>                                                                                   
                                                   <tr>
                                                     <td>
                                                       <asp:Label ID="Label6" runat="server" Text="Información:"></asp:Label>  
                                                     </td>
                                                     <td>
                                                       <asp:TextBox CssClass="TexboxNormal" ID="txtInformacion" runat="server" Width="500px" TextMode="MultiLine" Height ="250px"></asp:TextBox>
                                                     </td>
                                                   </tr>                                                                                                                    
                                                 </table>                             
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="OPEINGDetalleOrdenCompra0">
                                                    <h1 class="TituloPanelTitulo">Listado Ordenes de Compra</h1>
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

                                    <%-- Tab #3 --%>
                                   <telerik:RadPageView runat="server" ID="RadPageView3">                                  
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Acciones">
                                                <h1 class="TituloPanelTitulo">Datos Acciones</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    </asp:Panel>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>                           
                                                 <asp:Button  ID="Button4" runat ="server" Text ="Agregar" Width ="150px" OnClick ="btnAgregar3_Click"/>
                                                 <asp:Button ID ="Button5" runat ="server" Text ="Editar" Width ="150px" OnClick ="btnEditar3_Click" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                                  
                                                                
                                              </ContentTemplate>
                                                 <Triggers>                                             
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Acciones0">
                                                <h1 class="TituloPanelTitulo">Listado de Acciones</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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
