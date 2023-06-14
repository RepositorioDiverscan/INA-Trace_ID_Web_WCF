<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReportePicking.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.ReportePicking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >   
                       <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" style="width: 983px; height: 476px; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px;">


                             <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>

                            <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel1">
                                                    <h1 class="TituloPanelTitulo">Generar reporte</h1>
                                                </asp:Panel>
  <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDBodega_SelectedIndexChanged"></asp:DropDownList>
 <asp:Button ID="BtnGenerar" runat="server"  OnClick="btnGenerar_onClick" Text="Generar" />


                           <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel3">
                                                    <h1 class="TituloPanelTitulo">Listado de articulos</h1>
                                                </asp:Panel>
                           

                                                <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridArticulos"  
                AllowFilteringByColumn="True" AllowSorting="True" Width="100%"  ShowFooter="True" AllowPaging="True" runat="server"   
                InsertItemPageIndexAction="ShowItemOnFirstPage" >
                 <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                <SelectedItemStyle BackColor="Blue" BorderColor="Blue" BorderStyle="Dashed"
                                   BorderWidth="1px" />
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                        <GroupingSettings CaseSensitive="false" />
               
                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
                 
                    <Columns>
                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdArticulo" HeaderText="Número de articulo">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="NombreArticulo" HeaderText="Nombre">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidMinPicking" HeaderText="CantidMinPicking">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="CantidadDisponible" HeaderText="CantidadDisponible">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Cosiente" HeaderText="Cosiente">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>
                   
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

                                            </ContentTemplate>
                                                                              
                                        </asp:UpdatePanel>

                           </div>

   </asp:Panel>
</asp:Content>
