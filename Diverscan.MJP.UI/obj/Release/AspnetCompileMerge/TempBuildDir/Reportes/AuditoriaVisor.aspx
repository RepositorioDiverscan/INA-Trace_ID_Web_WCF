<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditoriaVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.AuditoriaVisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server">  
               

        <div id="RestrictionZoneID" class="WindowContenedor">             
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows >
                        <telerik:RadWindow  ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" 
                                            AutoSize="true">
                            <ContentTemplate >
                                <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Auditoria" Width="230px"></telerik:RadTab>                                  
                                </Tabs>
                                </telerik:RadTabStrip>
                                <%--KARDEX--%>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer">                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Reporte">
                                                <h1 class="TituloPanelTitulo">Datos para Generar Reporte</h1>
                                                <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnGenera" runat="server" Text="Vista Previa" Width="150px" OnClick="btnGenera_Click" Enabled="true" AutoPostBack="false" />                                     
                                                    <asp:Label ID="Label2" runat="server" Text="||"></asp:Label>
                                                    <div style="display: none">
                                                        <asp:Label ID="LblExporta" runat="server" Text="Formato a exportar" Enabled="false"></asp:Label>
                                                        <asp:DropDownList ID="ddlIdformatoexporta" runat="server" Enabled="false"></asp:DropDownList>
                                                    </div>
                                                    <asp:Button ID="BtnExporta" runat="server" Text="Exportar a Excel" Width="150px" OnClick="BtnExporta_Click" Enabled="false" />
                                                    <h1></h1>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                     <tr> 
                                                       <td>
                                                         <asp:Label ID="Label6" runat="server" Text="Artículo    :"></asp:Label>
                                                           <asp:DropDownList ID="ddlIDArticulo" Class="TexboxNormal" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                           <asp:Label ID="Label7" runat="server" Text="Código SAP:" Visible="false"></asp:Label>
                                                           <asp:DropDownList ID="ddlidERP" runat="server" Class="TexboxNormal" Width="200px" AutoPostBack="false" Visible="false"></asp:DropDownList>
                                                           <div style="display:none">
                                                               <asp:Label ID="Lblmensaje" runat="server" Text=" ** Cuando imprima, oprima Alt-Tab para ver la ventana de impresoras **" Visible ="true" Font-Bold ="true" Font-Size ="Medium" ForeColor="Red"  ></asp:Label>
                                                           </div>
                                                     </tr>
                                                     <tr>
                                                           <td>
                                                           <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha inicial:"></asp:Label>
                                                           <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                         </td>
                                                       </tr>
                                                     <tr>
                                                         <td>
                                                           <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                                                           <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack ="false" ></telerik:RadDatePicker>
                                                         </td>
                                                       </tr>
                                                     <tr>
                                                         <td>
                                                           <asp:Label ID="Label3" runat="server" Text="Motivo :"></asp:Label>
                                                           <asp:DropDownList ID="ddlIdmetodoaccion" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlIdmetodoaccion_SelectedIndexChanged"></asp:DropDownList>
                                                         </td>
                                                       </tr>
                                                     <tr>
                                                         <td>
                                                           <h1 class="TituloPanelTitulo">Vista Previa Reporte</h1> 
                                                         </td>
                                                       </tr>
                                                       <tr>
                                                         <td>
                                                         </td>
                                                       </tr>                                                
                                                     </table>                                             

                                                    <telerik:RadGrid ID="rdDatosReporte"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
                                                          AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource"
                                                        AutoGenerateColumns="False">
                                                          <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="false" ></Selecting>
                                                            <Scrolling AllowScroll="True"  UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>                                               
                                                          </ClientSettings>
                                                          <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                          <MasterTableView>

                                                              <Columns>
                                                                    <telerik:GridBoundColumn UniqueName="ArticuloSAP"
                                                                                SortExpression="ArticuloSAP" HeaderText="ArticuloSAP" DataField="ArticuloSAP">
                                                                            </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Articulo"
                                                                                SortExpression="Articulo" HeaderText="Articulo" DataField="Articulo">
                                                                            </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                                SortExpression="FechaRegistro" HeaderText="FechaRegistro" DataField="FechaRegistro">
                                                                            </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Zona"
                                                                                SortExpression="Zona" HeaderText="Zona" DataField="Zona">
                                                                            </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Motivo"
                                                                                SortExpression="Motivo" HeaderText="Motivo" DataField="Motivo">
                                                                            </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Operacion"
                                                                                SortExpression="Operacion" HeaderText="Operacion" DataField="Operacion">
                                                                        </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="SaldoInicial"
                                                                                SortExpression="SaldoInicial" HeaderText="SaldoInicial" DataField="SaldoInicial">
                                                                        </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                                        </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Saldo"
                                                                                SortExpression="Saldo" HeaderText="Saldo" DataField="Saldo">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn UniqueName="Lote"
                                                                            SortExpression="Lote" HeaderText="Lote" DataField="Lote">
                                                                        </telerik:GridBoundColumn> 
                                                                <telerik:GridBoundColumn UniqueName="FechaVencimiento"
                                                                    SortExpression="FechaVencimiento" HeaderText="FechaVencimiento" DataField="FechaVencimiento">
                                                                    </telerik:GridBoundColumn>      
                                                                    <telerik:GridBoundColumn UniqueName="OCDestino"
                                                                    SortExpression="OCDestino" HeaderText="OCDestino" DataField="OCDestino">
                                                                    </telerik:GridBoundColumn>                                                        
                                                        </Columns>

                                                            <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />                                               
                                                          </MasterTableView>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>                              
                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroSolicitud0">
                                        <%--<h1 class="TituloPanelTitulo">Vista Previa Reporte</h1>--%> 
                            </asp:Panel>
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
