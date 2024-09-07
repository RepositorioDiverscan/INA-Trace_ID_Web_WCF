<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorInventarios.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.VisorInventarios" %>

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
                                    <telerik:RadTab Text="Inventarios" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                     <telerik:RadDatePicker ID="_rdpB_Fecha" runat="server" AutoPostBack ="true" OnSelectedDateChanged="_rdpB_Fecha_SelectedDateChanged" ></telerik:RadDatePicker>  

                                                    <asp:DropDownList runat="server" ID="_ddlInventariosDisponibles" OnSelectedIndexChanged="_ddlInventariosDisponibles_SelectedIndexChanged" AutoPostBack="true"
                                                        ></asp:DropDownList>
                                                    <asp:DropDownList runat="server" ID="_ddlArticulosCategoria"></asp:DropDownList>
                                                                                                        
                                                    <asp:Button  runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click"/>
                                                    
                                                    <telerik:RadGrid ID="RGBodegaFisica_SistemaRecord" AllowPaging="True" Width="100%"  OnNeedDataSource ="RGBodegaFisica_SistemaRecord_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                               
                                                                 <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>
                                                                                          
                                                                <telerik:GridBoundColumn UniqueName="IdUbicacion"
                                                                    SortExpression="IdUbicacion" HeaderText="IdUbicacion" DataField="IdUbicacion">
                                                                </telerik:GridBoundColumn>
                                                                                     
                                                                <telerik:GridBoundColumn UniqueName="Etiqueta"
                                                                    SortExpression="Etiqueta" HeaderText="Etiqueta" DataField="Etiqueta">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadBodega"
                                                                    SortExpression="CantidadBodega" HeaderText="Cantidad Bodega" DataField="CantidadBodega">
                                                                </telerik:GridBoundColumn>  
                                                                  
                                                                <telerik:GridBoundColumn UniqueName="CantidadSistema"
                                                                    SortExpression="CantidadSistema" HeaderText="Cantidad Sistema" DataField="CantidadSistema">
                                                                </telerik:GridBoundColumn> 

                                                                 <telerik:GridBoundColumn UniqueName="DifenrenciaCantidad"
                                                                    SortExpression="DifenrenciaCantidad" HeaderText="Difenrencia" DataField="DifenrenciaCantidad">
                                                                </telerik:GridBoundColumn>   

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                  
                                                            <asp:Label runat="server" ID="_lblTotalTomaFisica" ForeColor="Red"></asp:Label>
                                                       <h1></h1>
                                                            <asp:Label runat="server" ID="_lblTotalSistema"  ForeColor="Red"></asp:Label>
                                                        

                                                    <asp:Button  runat="server" ID="_btnRealizarAjuste" Text="RealizarAjuste" OnClick="_btnRealizarAjuste_Click"/>
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
