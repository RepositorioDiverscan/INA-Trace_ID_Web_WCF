<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SugerenciasAlmacenamiento.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.SugerenciasAlmacenamiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:Panel ID="Panel4" runat="server" >   
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
     

    <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" 
        style="width: 983px; height: 476px; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 175px;">
      <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
        <td>
           <tr>
              <td> 
                <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>       
             </td>
             <td>
                <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="false" ></asp:DropDownList>
             </td>
          </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnGenerar" runat="server"  OnClick="btnGenerar_onClick" Text="Generar" />
                </td>
                <td></td>
            </tr>
        </td>
      </table>

      
          <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroArticulos0">
                <h1 class="TituloPanelTitulo">Listado de Artículos</h1>
                            
            </asp:Panel>
                                                    
            <telerik:RadGrid ID="RadGridArticulos" AllowPaging="True" Width="100%" 
                OnSelectedIndexChanged="RadGridArticulos_SelectedIndexChanged"
        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
            <MasterTableView>
            <Columns>
        
                <telerik:GridBoundColumn UniqueName="idArticulo"
                    SortExpression="idArticulo" HeaderText="Id Articulo" DataField="IdArticulo" Visible="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="idInterno"
                    SortExpression="idInterno" HeaderText="Id Interno" DataField="IdInterno" Visible="true">
                </telerik:GridBoundColumn>         

                <telerik:GridBoundColumn UniqueName="Nombre"
                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre" Visible="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="GTIN"
                    SortExpression="GTIN" HeaderText="GTIN" DataField="GTIN"  Visible="true">
                </telerik:GridBoundColumn>                          
                      
                <telerik:GridBoundColumn UniqueName="Contenido"
                    SortExpression="Contenido" HeaderText="Contenido" DataField="Contenido"  Visible="true">
                </telerik:GridBoundColumn>                
        
                 <telerik:GridBoundColumn UniqueName="MinimoPicking"
                    SortExpression="MinimoPicking" HeaderText="Minimo Picking" DataField="MinPicking" Visible="true">
                </telerik:GridBoundColumn>             

            </Columns>
        </MasterTableView>   
            <ClientSettings EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>                                                                
            </ClientSettings>
                 </telerik:RadGrid> 

            <br />
            <br />
            <telerik:RadGrid ID="RadGridProductStorage" AllowPaging="True" Width="100%" 
                runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
                    <MasterTableView>
                    <Columns>
        
                <telerik:GridBoundColumn UniqueName="Description"
                    SortExpression="MinimoPicking" HeaderText="Ubicación" DataField="Description" Visible="true">
                </telerik:GridBoundColumn>  

                <telerik:GridBoundColumn UniqueName="Lot"
                    SortExpression="GTIN" HeaderText="Lote" DataField="Lot"  Visible="true">
                </telerik:GridBoundColumn>                          
                      
                <telerik:GridBoundColumn UniqueName="DateExp"
                    SortExpression="Contenido" HeaderText="Fecha Exp." DataField="DateExp"  Visible="true">
                </telerik:GridBoundColumn>                                               

                <telerik:GridBoundColumn UniqueName="Quantity"
                    SortExpression="MinimoPicking" HeaderText="Cantidad" DataField="Quantity" Visible="true">
                </telerik:GridBoundColumn>  

            </Columns>
        </MasterTableView>   
            <ClientSettings EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>                                                                
            </ClientSettings>
                 </telerik:RadGrid> 

            </div>
           </ContentTemplate>
                                                                              
         </asp:UpdatePanel>
        
  </asp:Panel>
</asp:Content>

