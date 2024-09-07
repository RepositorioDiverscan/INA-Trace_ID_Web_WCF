<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AprobarAlisto.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.AprobarAlisto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >   
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
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
                                   
                                    <telerik:RadTab Text="Acciones" Width="100px"></telerik:RadTab>
                                   
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                       
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
                                                  
                                            <tr>
                                                 <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Num Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            <asp:Button runat="server" ID="Button6" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                                  <tr>      
                                                       <td> 
                                                                <asp:Label ID="Label7" runat="server" Text="Actividad"></asp:Label>       
                                                            </t>
                                                        <td>
                                                                <asp:DropDownList ID="ddlidActividad" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                       </td>   
                                                 </tr>  
                                            <tr>
                                                <td> 
                                                                <asp:Label ID="Label1" runat="server" Text="Alistos"></asp:Label>       
                                                </td>
                                                <td>
                                                                <asp:DropDownList ID="ddlAlistos" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlAlistos_SelectedIndexChanged1"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>      
                                                <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Detalle Solicitud"></asp:Label>       
                                                            </t>
                                                <td>
                                                                <asp:DropDownList ID="ddDetalleSolicitud" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                            </tr>  
                                             <tr>      
                                                <td>
                                                            <asp:Label ID="Label3" runat="server" Text="id Linea Detalle Solicitud"></asp:Label> 
                                                </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidLineaDetalleSolicitud" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Registro"></asp:Label> 
                                                </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtRegistro" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                            </tr>
                                            <tr>  
                                                <td>
                                                            <asp:Label ID="Label8" runat="server" Text="id Maestro Solicitud"></asp:Label> 
                                                </td>
                                                <td >
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidMaestroSolicitud" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox3" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
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
                                                             <asp:Label ID="LabelFuente" runat="server" Text="Fuente"></asp:Label>
                                                </td>
                                                <td>
                                                            <asp:DropDownList runat="server" CssClass="TexboxNormal" ID ="ddlFuente" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="ddlFuente_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>      
                                                <td>
                                                          <asp:Label ID="LabelObjetosForm" runat="server" CssClass="TexboxNormal" Text="Objeto fuente"></asp:Label>
                                                </td>
                                                <td>
                                                          <asp:DropDownList runat="server" ID ="ddlObjetoFuente" CssClass="TexboxNormal" Width="200px"></asp:DropDownList>
                                                </td>
                                            </tr>  
                                             <tr>      
                                                <td>
                                                          <asp:Label ID="Label27" runat="server" CssClass="TexboxNormal" Text="Evento"></asp:Label>
                                                </td>
                                                <td>
                                                          <asp:DropDownList runat="server" ID ="ddlidEvento" CssClass="TexboxNormal" Width="200px"></asp:DropDownList>
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