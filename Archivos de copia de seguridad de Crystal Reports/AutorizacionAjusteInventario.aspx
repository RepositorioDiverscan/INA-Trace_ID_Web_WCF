<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutorizacionAjusteInventario.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.AutorizacionAjusteInventario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >                       


        
<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID = "btnAprobarHide">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none" BackColor="#96B7E8" Height="100" Width="200" HorizontalAlign="Center">
    <div id="myModal" class="modal"">
        Aprobar
    </div>
    <div>
        <br />
        Esta Seguro?
        <br />
        <br />       
        <asp:Button ID="btnSi" runat="server" Text="Si" OnClick="btnSi_Click" Height="35" Width="60" />
        <asp:Button ID="btnAprobarHide" runat="server" Text="No" Height="35" Width="60" />
    </div>
</asp:Panel>



         <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe1" runat="server"
    PopupControlID="pnlPopup2" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground" CancelControlID = "btnRechazarHide">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none" BackColor="#96B7E8" Height="100" Width="200" HorizontalAlign="Center">
    <div id="Div1" class="modal1"">
        Rechazar
    </div>
    <div>
        <br />
        Esta Seguro?
        <br />
        <br />       
        <asp:Button ID="btnRechazoSi" runat="server" Text="Si" OnClick="btnRechazoSi_Click" Height="35" Width="60" />
        <asp:Button ID="btnRechazarHide" runat="server" Text="No" Height="35" Width="60" />
    </div>
</asp:Panel>



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
                                    <telerik:RadTab Text="Gestion de Ajustes" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   

                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                               
                                                    <tr>
                                                        <asp:RadioButtonList ID="_rblSearchType" runat="server" Font-Size="Large" OnSelectedIndexChanged="_rblSearchType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Selected="True">Búsqueda completa</asp:ListItem>
                                                            <asp:ListItem>Búsqueda por ID Solicitud de Ajuste Inventario</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </tr>
                                                    <tr>

                                                    </tr>
                                                     <tr>
                                                          <td>
                                                        <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                              <telerik:RadTextBox ID="TxtIdSolicitudAjusteInventario" runat="server" Visible="false"></telerik:RadTextBox>

                                                        <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>                                                            
                                                        </td> 
                                                         
                                                         <td>
                                                             <asp:Label ID="LBLEstado" runat="server" Text="Estado"></asp:Label>
                                                             <telerik:RadDropDownList runat="server" ID ="RDDLEstado" OnSelectedIndexChanged="RDDLEstado_SelectedIndexChanged" AutoPostBack ="true"></telerik:RadDropDownList>
                                                         </td>

                                                         <td>
                                                             <telerik:RadButton runat="server" Text="Buscar" ID ="btnBuscar"  OnClick="btnBuscar_Click"></telerik:RadButton>
                                                         </td>

                                                    </tr>
                                                   

                                                     <telerik:RadGrid ID="RGSolicitudAjustesInventario" AllowPaging="True" Width="100%" 
                                                          OnNeedDataSource ="RadGrid_NeedDataSource" 
                                                         OnItemCommand="RGLogAjustesInventario_ItemCommand"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10">

                                                         <ClientSettings  EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdSolicitudAjusteInventario"
                                                                    SortExpression="IdSolicitudAjusteInventario" HeaderText="Id Solicitud" DataField="IdSolicitudAjusteInventario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaSolicitud"
                                                                    SortExpression="FechaSolicitud" HeaderText="Fecha Solicitud" DataField="FechaSolicitud">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Apellidos"
                                                                    SortExpression="Apellidos" HeaderText="Apellidos" DataField="Apellidos">
                                                                </telerik:GridBoundColumn>                                                             

                                                                  <telerik:GridBoundColumn UniqueName="MotivoInventario"
                                                                    SortExpression="MotivoInventario" HeaderText="Motivo Inventario" DataField="MotivoInventario">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="TipoMotivo"
                                                                    SortExpression="TipoMotivo" HeaderText="Tipo" DataField="TipoMotivo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CentroCosto" Display="false"
                                                                    SortExpression="CentroCosto" HeaderText="CentroCosto" DataField="CentroCosto">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>   
                                                            
                                                    </telerik:RadGrid>

                                                     <tr>
                                                          <td>
                                                              <telerik:RadDropDownList runat="server" ID ="_rddlCentroCosto" Visible="false"></telerik:RadDropDownList>
                                                          </td>
                                                         <h1></h1>
                                                         <td>
                                                             <asp:Button runat="server"  ID ="_btnAprobar" Text="Aprobar" OnClick="btnAprobar_Click"/>
                                                             <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="_btnAprobar"
                                                                ConfirmText="Está seguro que quieres llevar a cabo la aprobación">
                                                             </cc1:ConfirmButtonExtender>
                                                         </td>

                                                         <td>
                                                             <asp:Button runat="server"  ID ="_btnRechazar" Text="Rechazar" OnClick="_btnRechazar_Click"/>
                                                             <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="_btnRechazar"
                                                                ConfirmText="Está seguro que desea ralizar el rechazo">
                                                             </cc1:ConfirmButtonExtender>
                                                         </td>
                                                     </tr> 


                                                                                                      
                                                    <telerik:RadGrid ID="_rgArticulosXSolicitudDetalle" AllowPaging="True" Width="100%" 
                                                          OnNeedDataSource ="_rgArticulosXSolicitudDetalle_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10">                                                         
                                                         <MasterTableView>
                                                            <Columns>  
                                                                
                                                                 <telerik:GridBoundColumn UniqueName="IdSolicitudAjusteInventario"
                                                                    SortExpression="IdSolicitudAjusteInventario" HeaderText="Id Solicitud" DataField="IdSolicitudAjusteInventario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CodigoInterno"
                                                                    SortExpression="CodigoInterno" HeaderText="Codigo" DataField="CodigoInterno">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre Articulo" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>                                  

                                                                 <telerik:GridBoundColumn UniqueName="Lote"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="FechaVencimiento"
                                                                    SortExpression="FechaVencimiento" HeaderText="FechaVencimiento" DataField="FechaVencimiento">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="UnidadMedida" DataField="UnidadMedida">
                                                                </telerik:GridBoundColumn>
                                                                


                                                                <telerik:GridBoundColumn UniqueName="EtiquetaActual"
                                                                    SortExpression="EtiquetaActual" HeaderText="Ubicacion" DataField="EtiquetaActual">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadUI"
                                                                    SortExpression="CantidadUI" HeaderText="Cantidad UI" DataField="CantidadUI">
                                                                </telerik:GridBoundColumn> 


                                                                <telerik:GridBoundColumn UniqueName="CantidadToShow" Display="false"
                                                                    SortExpression="CantidadToShow" HeaderText="Cantidad Presentación" DataField="CantidadToShow">
                                                                </telerik:GridBoundColumn> 

                                                                <telerik:GridBoundColumn UniqueName="PrecioUnidad" Display="false"
                                                                    SortExpression="PrecioUnidad" HeaderText="CostoUnidad" DataField="PrecioUnidad">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="PrecioXCantidad" Display="false"
                                                                    SortExpression="PrecioXCantidad" HeaderText="CostoTotal" DataField="PrecioXCantidad">
                                                                </telerik:GridBoundColumn>
                                                               
                                                            </Columns>
                                                        </MasterTableView>   
                                                            
                                                    </telerik:RadGrid>

                                                    <asp:Label runat="server" ID="_lblCostoAjuste"  ForeColor="Red" Visible="false"></asp:Label>
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