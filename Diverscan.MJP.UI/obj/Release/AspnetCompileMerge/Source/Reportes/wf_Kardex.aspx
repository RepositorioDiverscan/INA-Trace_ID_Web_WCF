<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Kardex.aspx.cs" Inherits="Diverscan.MJP.UI.Reports.wf_Kardex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

  <%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <script type='text/javascript'>
           function DisplayLoadingImage1()
           {
               document.getElementById("loading1").style.display = "block";
           }
       </script>

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
                        <telerik:RadWindow  ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" 
                                            AutoSize="true">
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Lista Contabilización Stock" Width="230px"></telerik:RadTab>                                 
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
                                             <asp:Button ID="btnGenera" runat="server" Text="Vista Previa" Width="150px" OnClick="btnGenera_Click" Enabled="true" AutoPostBack="true" OnClientClick="DisplayLoadingImage1()" />
                                             <%--<asp:Label ID="Label1" runat="server" Text="||"></asp:Label>--%>
                                             <%--<asp:Button ID="BtnImprime" runat="server" Text="Imprimir" Width="150px" OnClick="BtnImprime_Click" Enabled="false" />--%>
                                             <asp:Label ID="LblExporta" runat="server" Text="|||"></asp:Label>
                                             <asp:Button ID="BtnExporta" runat="server" Text="Exportar a Excel" Width="150px" OnClick="btnExportar_Click" Enabled="false" AutoPostBack="true" /> 
                                            <%-- <div style="display: none">  
                                                 <asp:DropDownList ID="ddlIdformatoexporta" runat="server" Enabled="false"></asp:DropDownList>
                                             </div>--%>
                                             <h1></h1>
                                             <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                               <center>
                                                 <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               </center>
                                             </div>
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
                                               <asp:Label ID="LblSaldoGlobal" runat="server" Text="Saldo Global:" Visible="false"></asp:Label>
                                               <asp:TextBox ID="TxtSaldoGlobal" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                               <asp:TextBox ID="TxtUnidad" runat="server" Visible="false" Width="100px"></asp:TextBox>
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

                                         <%--<div style="display:block">
                                             <CR:crystalreportviewer ID="CRV" runat="server" AutoDataBind="false" Height="100%" Visible="true" HasExportButton="false"
                                                 HasPrintButton="False" HasCrystalLogo="False" HasToggleGroupTreeButton="false" ToolPanelView="None"
                                                 HasRefreshButton="False" AutoPostBack="false" EnableDatabaseLogonPrompt="false"
                                                 EnableParameterPrompt="false" />

                                         </div>--%>

                                         <telerik:RadGrid ID="rdDatosReporte" runat="server" AllowMultiRowSelection="false" PageSize="10" OnItemCommand="RadGrid_ItemCommand"
                                             AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False" OnNeedDataSource="RadGrid_NeedDataSource"
                                             AutoGenerateColumns="False">
                                             <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                 <Selecting AllowRowSelect="false"></Selecting>
                                                 <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                             </ClientSettings>
                                             <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                             <MasterTableView>

                                                 <Columns>

                                                     <telerik:GridBoundColumn UniqueName="ArticuloSAP"
                                                                              SortExpression="ArticuloSAP" 
                                                                              HeaderText="ArticuloSAP" 
                                                                              DataField="ArticuloSAP">
                                                     </telerik:GridBoundColumn>

                                                     <telerik:GridBoundColumn UniqueName="Articulo"
                                                                              SortExpression="Articulo" 
                                                                              HeaderText="Articulo" 
                                                                              DataField="Articulo"
                                                                              Display = "false">
                                                      </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn UniqueName="Motivo"
                                                                    SortExpression="Motivo" HeaderText="Motivo" DataField="Motivo">
                                                                </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn UniqueName="SaldoInicial"
                                                                    SortExpression="SaldoInicial" HeaderText="SaldoInicial" DataField="SaldoInicial">
                                                         </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn UniqueName="Operacion"
                                                                    SortExpression="Operacion" HeaderText="Operacion" DataField="Operacion" Visible ="false">
                                                         </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                               SortExpression ="Cantidad" 
                                                                               HeaderText="Cantidad" 
                                                                               DataField="Cantidad"
                                                                               Display ="false">
                                                         </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn UniqueName="Unid_inventario"
                                                                               SortExpression ="Unid_inventario" 
                                                                               HeaderText="Unidades Inventario" 
                                                                               DataField="Unid_inventario">
                                                         <%--<telerik:GridBoundColumn UniqueName="Unid_Inventario"
                                                                               SortExpression ="Unid_Inventario" 
                                                                               HeaderText="Unidades Inventario"
                                                                               DataField="Saldo_unidad">--%>
                                                         </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn UniqueName="Unidad"
                                                                               SortExpression ="Unidad" 
                                                                               HeaderText="Unidades Medida" 
                                                                               DataField="Unidad">
                                                         </telerik:GridBoundColumn>

                                                      <telerik:GridBoundColumn UniqueName="Saldo"
                                                                               SortExpression = "Saldo" 
                                                                               HeaderText = "Sumatoria Saldo"
                                                                               DataField="Saldo">
                                                      </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                               SortExpression = "FechaRegistro" 
                                                                               HeaderText = "FechaRegistro" 
                                                                               DataField="FechaRegistro"
                                                                               DataFormatString="{0:dd/MM/yyyy}" 
                                                                               DataType = "System.DateTime">
                                                       </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn UniqueName="Lote"
                                                                               SortExpression="Lote" 
                                                                               HeaderText="Lote" 
                                                                               DataField="Lote">
                                                      </telerik:GridBoundColumn> 
                                                      <telerik:GridBoundColumn UniqueName="FechaVencimiento"
                                                                               SortExpression="FechaVencimiento" 
                                                                               HeaderText="FechaVencimiento" 
                                                                               DataField="FechaVencimiento"
                                                                               DataFormatString="{0:dd/MM/yyyy}" 
                                                                               DataType = "System.DateTime">
                                                      </telerik:GridBoundColumn>   
                                                      
                                                      <telerik:GridBoundColumn UniqueName="Zona"
                                                                    SortExpression="Zona" HeaderText="Zona" DataField="Zona">
                                                                </telerik:GridBoundColumn>
                                                     
                                                      <telerik:GridBoundColumn UniqueName="Saldo_Actual"
                                                                               SortExpression = "Saldo_actual" 
                                                                               HeaderText="Saldo Actual Global" 
                                                                               DataField="Saldo_Actual">
                                                      </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn UniqueName="OCDestino"
                                                        SortExpression="OCDestino" HeaderText="OCDestino" DataField="OCDestino">
                                                      </telerik:GridBoundColumn>                                                        

                                                 </Columns>
                                                 <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="50" />
                                             </MasterTableView>
                                         </telerik:RadGrid>
                                       
                                      </ContentTemplate>                           
                                    </asp:UpdatePanel>
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

