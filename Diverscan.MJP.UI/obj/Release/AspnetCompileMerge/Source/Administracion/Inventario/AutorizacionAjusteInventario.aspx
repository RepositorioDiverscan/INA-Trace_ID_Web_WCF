<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutorizacionAjusteInventario.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.AutorizacionAjusteInventario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
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
             
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="false">
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows>
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true" style="width:1500px">
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Gestión de Ajustes" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   

                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../../images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
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

                                                        <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack ="false" >
                                                            <DateInput DateFormat="dd/MM/yyyy" /></telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack ="false" >
                                                            <DateInput DateFormat="dd/MM/yyyy" />
                                                            </telerik:RadDatePicker>                                                            
                                                        </td> 
                                                         
                                                         <td>
                                                             <asp:Label ID="LBLEstado" runat="server" Text="Estado"></asp:Label>
                                                             <telerik:RadDropDownList runat="server" ID ="RDDLEstado" OnSelectedIndexChanged="RDDLEstado_SelectedIndexChanged" AutoPostBack ="true"></telerik:RadDropDownList>
                                                         </td>

                                                         <td>
                                                              <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false"
                                                                OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />                                                            
                                                         </td>
                                                     </tr>
                                                   </table>

                                                     <telerik:RadGrid 
                                                         ID="RGSolicitudAjustesInventario" 
                                                         AllowPaging="True"
                                                         Width="100%" 
                                                         runat="server"
                                                         AutoGenerateColumns="False"
                                                         AllowSorting="True"
                                                         PageSize="100" 
                                                         PagerStyle-AlwaysVisible="true"
                                                         OnNeedDataSource ="RadGrid_NeedDataSource"                                                          
                                                         OnItemCommand="RGLogAjustesInventario_ItemCommand"                                                             
                                                         EnableDragToSelectRows="false"
                                                         AllowFilteringByColumn="true"
                                                         >

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
                                                                    SortExpression="FechaSolicitud" HeaderText="Fecha Solicitud" DataField="FechaSolicitud"
                                                                    DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
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
                                                     <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; 
                                                        border-collapse: initial;" id="Table3">                                                    
                                                        <tr>
                                                         
                                                          <td>
                                                              <telerik:RadDropDownList runat="server" ID ="_rddlCentroCosto" Visible="false"></telerik:RadDropDownList>
                                                          </td>
                                                         
                                                         <td>
                                                             <asp:Button runat="server"  ID ="_btnAprobar" Text="Aprobar" OnClick="btnAprobar_Click"   OnClientClick="DisplayLoadingImage1()"/>
                                                             <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="_btnAprobar"
                                                                ConfirmText="Está seguro que quieres llevar a cabo la aprobación">
                                                             </cc1:ConfirmButtonExtender>
                                                        
                                                             <asp:Button runat="server"  ID ="_btnRechazar" Text="Rechazar" OnClick="_btnRechazar_Click"   OnClientClick="DisplayLoadingImage1()"/>
                                                             <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="_btnRechazar"
                                                                ConfirmText="Está seguro que desea realizar el rechazo">
                                                             </cc1:ConfirmButtonExtender>

                                                             <asp:Button runat="server"  ID ="_btnCreateXML" Text="Exportar" OnClick="BtnExportar_Click" />
                                                         
                                                                                                                            
                                                         </td>
                                                       
                                                       </tr> 
                                                    </table>
                                                                                                      
                                                    <telerik:RadGrid ID="_rgArticulosXSolicitudDetalle" AllowPaging="True" Width="100%" 
                                                          OnNeedDataSource ="_rgArticulosXSolicitudDetalle_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" PagerStyle-AlwaysVisible="true">                                                         
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
                                                                    SortExpression="FechaVencimiento" HeaderText="Fecha de Venc." 
                                                                     DataField="FechaVencimiento" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="Unidad de Medida" DataField="UnidadMedida">
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