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

                <div  id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow"
                    style="width: 983px; height: 476px; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px;">
                                           
                    <div class="rwContent" style="margin-top:10px">
                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                     
                            <tr>
                                <td>
                                     <asp:Label ID="Label1" runat="server" Text="Proveedores:"/>   
                                </td>
                                <td>
                                      <asp:DropDownList ID="DDropDownProve" Class="TexboxNormal" runat="server" Width="250px" 
                                          AutoPostBack="true" OnSelectedIndexChanged="DDropDownProve_SelectedIndexChanged"/>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                     <asp:Label ID="Label5" runat="server" Text="Articulos:"/>   
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListArticulo" Class="TexboxNormal" runat="server" Width="250px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="DDropDownArti_SelectedIndexChanged"/>
                                </td>                          
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="IdInterno:"/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIdInterno" Class="TexboxNormal" runat="server" Width="250px" AutoPostBack="true"/>
                                    &nbsp;
                                    <asp:Button ID="btnBuscarIdInterno" runat="server" Text="Buscar Articulo" OnClick="btnBuscarIdInterno_Click" />
                                </td>                             
                             </tr>
                             <tr>
                                <td>
                                     <asp:Label runat="server" ID="lblNombreArticulo"></asp:Label>
                                </td>                               
                            </tr>
                             <tr>
                                 <td>
                                     <asp:Label ID="Label3" runat="server" Text="Zonas:"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="DropDownListZona" Class="TexboxNormal" runat="server" Width="250px" AutoPostBack="true"
                                         OnSelectedIndexChanged="DDropDownZonas_SelectedIndexChanged" />
                                     &nbsp; &nbsp;
                                     <asp:CheckBox ID="CheckSinArticulo" runat="server" OnCheckedChanged="CheckSinArticulo_CheckedChanged"
                                         AutoPostBack="true" Text="Sin articulo" />
                                     &nbsp; &nbsp; &nbsp; &nbsp;
                                     <asp:Button ID="btnAceptar" runat="server" Text="Buscar Existencias" OnClick="BtnAceptar_onClick" />
                                     &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                     <asp:Button ID="btnGenerearReporte" runat="server" Text="Generar Reporte" OnClick="btnGenerarReporte"
                                         PostBackUrl="~/Consultas/Administracion/DisponibleBodegaNew.aspx" />
                                 </td>
                             </tr>
                        </table>
                         <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                            <center>
                                    <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                            </center>
                        </div>
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
                         OnNeedDataSource="RadGridArticulos_NeedDataSource" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true" PageSize="50" >
                    
                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                            <Selecting AllowRowSelect="true"></Selecting>
                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False"  DataKeyNames="IdArticulo" ClientDataKeyNames="IdArticulo">
                            <Columns>
                                  <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="IdInterno" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="SKU">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="idArticulo" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Numero Articulo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Nombre" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Articulos">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Cantidad" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Cantidad">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Lote"  FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Lote">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Descripcion" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Descripción">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="FechaVencimientoToShow" 
                                    FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                    HeaderText="Fecha vencimiento">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                   </telerik:RadGrid>

                    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridArticulosIdInterno" AllowFilteringByColumn="true" FilterType="CheckList"
                        OnNeedDataSource="RadGridArticulosIdInterno_NeedDataSource" AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true">
                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                            <Selecting AllowRowSelect="true"></Selecting>
                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False"  DataKeyNames="IdArticulo" ClientDataKeyNames="idInterno">
                            <Columns>
                                  <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="idArticulo" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="SKU">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" DataField="Nombre" FilterControlWidth="120px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderText="Articulos">
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


