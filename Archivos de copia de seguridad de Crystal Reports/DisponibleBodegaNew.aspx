<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisponibleBodegaNew.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Administracion.DisponibleBodegaNew" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    
         
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                        </AjaxSettings>
                </telerik:RadAjaxManager>
        <asp:Panel ID="Panel4" runat="server">
         
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div  id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" style="width: 983px; height: 476px; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px;">
                                           
                    <div class="rwContent" style="margin-top:10px"> 
                        <asp:Label ID="Label1" runat="server" Text="Proveedores:"></asp:Label>    
                        <asp:DropDownList ID="DDropDownProve" Class="TexboxNormal" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownProve_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Label ID="Label2" runat="server" Text="Articulos:"></asp:Label>    
                        <asp:DropDownList ID="DropDownListArticulo" Class="TexboxNormal" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownArti_SelectedIndexChanged" ></asp:DropDownList>
              
                          <br />
                          <br />

                        <asp:Label ID="Label3" runat="server" Text="Zonas:"></asp:Label>    
                        <asp:DropDownList ID="DropDownListZona" Class="TexboxNormal" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownZonas_SelectedIndexChanged" ></asp:DropDownList>
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar"   OnClick="BtnAceptar_onClick"/>
                        <asp:CheckBox ID="CheckSinArticulo" runat="server" OnCheckedChanged="CheckSinArticulo_CheckedChanged" AutoPostBack="true" Text="Sin articulo" />
                        <asp:Button ID="btnGenerearReporte" runat="server" Text="Generar Reporte" OnClick="btnGenerarReporte" PostBackUrl="~/Consultas/Administracion/DisponibleBodegaNew.aspx"/> 
                        <br />
                    </div>
                    <div class="RadGrid RadGrid_Outlook" style="margin-top:5px">
                       
                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridZonas" AllowFilteringByColumn="true"
                        FilterType="CheckList" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true"
                         OnNeedDataSource="RadGridZonas_NeedDataSource" AllowAutomaticDeletes="false" >

                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                <Selecting AllowRowSelect="true"></Selecting>
                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="idZona" ClientDataKeyNames="idZona"  AllowAutomaticDeletes="true" >
                                <Columns>
                                    <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="false" DataField="Nombre" HeaderText="Zonas">
                                    </telerik:GridBoundColumn>
                   
                                        <telerik:GridTemplateColumn UniqueName="Export1">
                                            <ItemTemplate>
                                                <telerik:RadButton ID="btnExport1" runat="server" Text="Eliminar" OnClick="Delete_Click" Width="100px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
           
                        </telerik:RadGrid>
                    </div>
                    <br />
       
                    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridArticulosDisponibles" AllowFilteringByColumn="true" FilterType="CheckList"
                         OnNeedDataSource="RadGridArticulos_NeedDataSource" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true">
                    
                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                            <Selecting AllowRowSelect="true"></Selecting>
                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False"  DataKeyNames="IdArticulo" ClientDataKeyNames="IdArticulo">
                            <Columns>
                                  <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="IdInterno" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="SKU">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Nombre" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Articulos">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Cantidad" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Cantidad">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Lote"  FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Lote">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Descripcion" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Descripción">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="FechaVencimientoToShow" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Fecha vencimiento" DataFormatString="{0:dd/MM/yyyy}">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                   </telerik:RadGrid>
                    <br />
               </div> 
            </ContentTemplate> 
        </asp:UpdatePanel>
        
    </asp:Panel>
   
</asp:Content>


