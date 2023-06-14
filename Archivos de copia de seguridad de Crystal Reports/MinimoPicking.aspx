<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MinimoPicking.aspx.cs" Inherits="Diverscan.MJP.UI.Inventario.WebForm1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


               

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnCargarGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridMinimoPicking"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pickingBodega"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="pickingBodega">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pickingBodega" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridMinimoPicking">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridMinimoPicking"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
             
               
            </AjaxSettings>
        </telerik:RadAjaxManager>
 
    <asp:Panel ID="Panel4" runat="server">
           
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
                                    <telerik:RadTab Text="Mínimo de Picking" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultipage1" SelectedIndex="0" CssClass="outerMultiPage">

                                <telerik:RadPageView runat="server" ID="RadPageView1">

                                    <asp:Panel ID="Panel1" runat="server" CssClass="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="VistaPicking">
                                            <h1 class="TituloPanelTitulo" >Picking Mínimo</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:Label ID="txtAlmacen" runat="server" Visible="false" Text="Label"></asp:Label>
                                        
                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                            <tr>
                                                <td>
                                                     <asp:Label ID="Label14" runat="server" Text="Bodega:" Enabled="false" style="margin-left:15px"></asp:Label>
                                                </td>
                                                <td>
                                                   <td>  <asp:DropDownList runat="server" ID="pickingBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" ></asp:DropDownList> </td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCargarGrid" runat="server" Text="Ver inventario"  OnClick="BtnCargarPicking" />
                                                </td>
                                            </tr>
                                        </table>

                                        
                                        <asp:Panel runat="server" CssClass="TituloPanelInventario" ID="Vista_PanelInventario">
                                            <h1 class="TituloPanelTitulo">Listado Inventario</h1>
                                        </asp:Panel>

                                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridMinimoPicking" AllowFilteringByColumn="true"
                                            FilterType="CheckList" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true" 
                                             OnNeedDataSource="RadGridMinimoPicking_NeedDataSource" ShowFooter="true" >
                                            
                                           <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                <Selecting AllowRowSelect="true"></Selecting>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                            </ClientSettings>
                                            <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed" BorderWidth="1px" />                                         
               
                                        <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True" ClientDataKeyNames="IdArticulo">                 
                                            <Columns>                     
                                                    <telerik:GridBoundColumn DataField="IdArticulo" HeaderText="Código Artículo"
                                                    UniqueName="IdArticulo">  
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NombreArticulo" HeaderText="Nombre del artículo" FilterControlWidth="125px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >  
                                                    </telerik:GridBoundColumn>                       
                                                    <telerik:GridBoundColumn FilterControlWidth="120px" DataField="CantidMinPicking" HeaderText="Picking Mínimo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadDisponible" HeaderText="Cantidad Disponible">
                                                    </telerik:GridBoundColumn> 
                                                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Cosiente" HeaderText="Cosiente"> 
                                                    </telerik:GridBoundColumn>
                                                    
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

                                    </asp:Panel>

                                </telerik:RadPageView>

                            </telerik:RadMultiPage> 
                               
                        </ContentTemplate>
                      

                    </telerik:RadWindow>
                </Windows>

            </telerik:RadWindowManager>
        </div>
    </asp:Panel>


</asp:Content>

