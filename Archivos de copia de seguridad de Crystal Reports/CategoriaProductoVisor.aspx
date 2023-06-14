<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriaProductoVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Inventario.CategoriaProductoVisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >                       

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
                                    <telerik:RadTab Text="Autorizaciones" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   

                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>                                               
                                                    <asp:Label ID="Label1" runat ="server" Text ="Nombre :"></asp:Label>
                                                    <asp:TextBox runat="server" id="_txtNombre"></asp:TextBox>
                                                        
                                                    <asp:Label ID="Label4" runat ="server" Text ="Descripcion :"></asp:Label>
                                                    <asp:TextBox runat="server" id="_txtDescripcion" Width="250"></asp:TextBox>
                                                                                                       
                                                    <asp:Button runat="server" ID="_btnAgregar" Text="Agregar"  OnClick="_btnAgregar_Click"/>
                                                    <asp:Button runat="server" ID="_btnActualizar" Text="Actualizar"  OnClick="_btnActualizar_Click"/>
                                                    
                                               
                                                    <telerik:RadGrid ID="RGCategoriaProducto" AllowPaging="True" Width="100%"  OnNeedDataSource ="RGCategoriaProducto_NeedDataSource"
                                                        OnItemCommand ="RadGrid_ItemCommand"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="3" AllowMultiRowSelection="true">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>

                                                         <MasterTableView>
                                                            <Columns>
                                                                
                                                                <telerik:GridBoundColumn UniqueName="IdCategoriaArticulo"
                                                                    SortExpression="IdCategoriaArticulo" HeaderText="Id Categoria" DataField="IdCategoriaArticulo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
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









