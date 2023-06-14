<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteArticulos.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.ReporteArticulos" %>

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

 <asp:Button ID="BtnGenerar" runat="server" OnClick="btnGenerar_onClick"  Text="Generar" />


                           <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Panel3">
                                                    <h1 class="TituloPanelTitulo">Listado de articulos</h1>
                                                </asp:Panel>
                           

                                                <telerik:RadGrid RenderMode="Lightweight" AutoGenerateColumns="false" ID="RadGridArticulos"  OnNeedDataSource="RadGridArticulo_NeedDataSource"
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
                    <telerik:GridBoundColumn FilterControlWidth="50px" DataField="IdInterno" HeaderText="Número de articulo">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Nombre" HeaderText="Nombre">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Descripcion" HeaderText="Descripción">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Contenido" HeaderText="Contenido">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="GTIN13" HeaderText="GTIN13">
                            <FooterStyle Font-Bold="true"></FooterStyle>
                        </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn FilterControlWidth="50px" DataField="GTIN14" HeaderText="GTIN14">
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
