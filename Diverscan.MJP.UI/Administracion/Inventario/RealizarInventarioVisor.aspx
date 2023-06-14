<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealizarInventarioVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.RealizarInventarioVisor" %>

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
                                    <telerik:RadTab Text="Realizar Inventario Ciclico" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    
                                                    <asp:DropDownList runat="server" ID="_ddlInventariosDisponibles" OnSelectedIndexChanged="_ddlInventariosDisponibles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                                    
                                                    <h1></h1>
                                                    <asp:TextBox runat="server" ID ="_txtArticuloBusqueda" OnTextChanged ="_txtArticuloBusqueda_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:DropDownList runat="server" ID="_ddlArticulosCategoria" OnSelectedIndexChanged="_ddlArticulosCategoria_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:Button runat="server" ID="_btnCerrarArticulo" Text="X" OnClick="_btnCerrarArticulo_Click"/>
                                                    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="_btnCerrarArticulo"
                                                                ConfirmText="Está seguro que quieres cerrar el articulo">
                                                             </cc1:ConfirmButtonExtender>
                                                     <h1></h1>
                                                    <asp:DropDownList runat="server" ID="_ddlUbicaciones"></asp:DropDownList>
                                                    <asp:Button runat="server" ID="_btnCerrar" Text="X" OnClick="_btnCerrar_Click"/>
                                                      <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="_btnCerrar"
                                                                ConfirmText="Está seguro que quieres cerrar la ubicacion">
                                                             </cc1:ConfirmButtonExtender>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <h1></h1>
                                                        <tr>
                                                        <td>
                                                          <asp:Label ID="Label4" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox runat="server" ID="_txtS_CodigoBarras" Width="500px" OnTextChanged ="_txtS_CodigoBarras_TextChanged" AutoPostBack ="true"></asp:TextBox>
                                                        </td>
                                                          </tr>
                                                          <tr>
                                                            <td>
                                                              <asp:Label ID="_lblS_UbicacionActual" runat="server" Text="Ubicación:"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:TextBox runat="server" ID="_txtS_UbicacionActual" Width="200px"></asp:TextBox>
                                                            </td>
                                                          </tr>
                                                    </table>
                                                    <asp:Button  runat="server" ID="_btnEnviar" Text="Enviar" OnClick="_btnEnviar_Click"/>
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