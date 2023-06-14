<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventarioBasicoVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.InventarioBasicoVisor" %>

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
                                    <telerik:RadTab Text="Agregar Inventario" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>    
                                                    <asp:Label ID="Label17" runat="server" Text="Bodega" style ="margin-top:8px; margin-left:5px;"></asp:Label>  
                                                    <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="false" style ="margin-top:8px; margin-left:5px;" ></asp:DropDownList>
                                                    <h1></h1>    
                                                    <asp:Label runat="server" ID="_lblNombre" Text="Nombre: " style ="margin-top:8px; margin-left:5px;"></asp:Label>
                                                    <asp:TextBox runat ="server" ID="_txtNombre"  style ="margin-top:8px; "></asp:TextBox>                                                       
                                                    <asp:Label runat="server" ID="_lblFechaPorAplicar" Text="Fecha Por Aplicar"  style ="margin-top:8px; "></asp:Label>
                                                    <telerik:RadDatePicker runat ="server" ID="_rdpFechaPorAplicar"  style ="margin-top:8px; "></telerik:RadDatePicker>                                                     
                                                    <h1></h1>                                                     
                                                    <asp:Label runat="server" ID="_lblDescripcion" Text ="Descripción" style =" margin-left:5px;"></asp:Label>
                                                    <asp:TextBox runat ="server" ID="_txtDescripcion" Width="200"></asp:TextBox>
                                                    <asp:Button runat="server" ID="_btnAgregar" Text ="Agregar" OnClick="_btnAgregar_Click"/>
                                                    <h1></h1>
                                                      <asp:Panel ID="Consultas" runat="server" GroupingText="Consultas">
                                                          <asp:Label runat="server" ID="_lblFechaInicio" Text="FechaInicio: "></asp:Label>
                                                          <telerik:RadDatePicker runat ="server" ID="_rdpFechaInicio"></telerik:RadDatePicker> 
                                                          <asp:Label runat="server" ID="_lblFechaFinal" Text="FechaFinal: "></asp:Label>
                                                          <telerik:RadDatePicker runat ="server" ID="_rdpFechaFin"></telerik:RadDatePicker> 
                                                          <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click"/>
                                                      </asp:Panel>
                                                    <telerik:RadGrid ID="_rgInventarioBasicos" AllowPaging="True" Width="100%" OnNeedDataSource="_rgInventarioBasicos_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaPorAplicar"
                                                                    SortExpression="FechaPorAplicar" HeaderText="Fecha Por Aplicar" DataField="FechaPorAplicar">
                                                                </telerik:GridBoundColumn>       
                                                                
                                                                <telerik:GridBoundColumn UniqueName="EstadoToShow"
                                                                    SortExpression="EstadoToShow" HeaderText="Estado" DataField="EstadoToShow">
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