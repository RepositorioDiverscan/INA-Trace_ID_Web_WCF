<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_MaestroSectorBodega.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_MaestroSectorBodega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Panel ID="Panel4" runat="server" >   
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
     
          <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" style="width: 80%; height: 80%;
                position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 175px; border:thin; margin-left:1%">
              <h1 class="TituloPanelTitulo">Maestro Sectores</h1>
      <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">             
        <td>
            <tr>
              <td> 
                <asp:Label ID="Label4" runat="server" Text="Id Sector"></asp:Label>       
             </td>
             <td>
                <asp:TextBox CssClass="TextBoxBusqueda" ID="txtIdSector" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" />
             </td>
          </tr>
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
                    <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>       
                </td>
                <td>
                     <asp:TextBox CssClass="TexboxNormal" ID="txtName" runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td> 
                    <asp:Label ID="Label2" runat="server" Text="Descripción"></asp:Label>       
                </td>
                <td>
                     <asp:TextBox CssClass="TexboxNormal" ID="txtDecription" runat="server" Width="250px" />
                </td>
            </tr>
             <tr>
                <td> 
                  <asp:Label ID="Label3" runat="server" Text="Activo"></asp:Label>       
                </td>
                <td>
                      <asp:CheckBox runat="server" ID="chkActive" Text="Activo"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBuscar" runat="server"  OnClick="btnBuscar_Click" Text="Buscar" />
                </td>
                <td><asp:Button ID="btnAceptar" runat="server"  OnClick="btnAceptar_Click" Text="Aceptar" /> 
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLimpiar" runat="server"  OnClick="btnLimpiar_Click" Text="Limpiar" />
                </td>
            </tr>
        </td>
      </table>

      
          <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_SectorsWarehouse">
                <h1 class="TituloPanelTitulo">Listado de Sectores por Bodega</h1>
                            
            </asp:Panel>
                                                    
            <telerik:RadGrid ID="RadGridSectorsWareHouse" AllowPaging="True" Width="100%" 
                OnSelectedIndexChanged="RadGridSectorsWareHouse_SelectedIndexChanged" OnNeedDataSource ="RadGridSectorsWareHouse_NeedDataSource"
        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" PagerStyle-AlwaysVisible="true">
            <MasterTableView>
            <Columns>
        
                <telerik:GridBoundColumn UniqueName="IdSectorWarehouse"
                    SortExpression="IdSectorWarehouse" HeaderText="Id Sector" DataField="IdSectorWarehouse" Visible="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="IdInternoBodega"
                    SortExpression="IdInternoBodega" HeaderText="Id Bodega" DataField="IdInternoBodega" Visible="true">
                </telerik:GridBoundColumn>         

                <telerik:GridBoundColumn UniqueName="Name"
                    SortExpression="Name" HeaderText="Nombre" DataField="Name" Visible="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="Description"
                    SortExpression="Description" HeaderText="Descripción" DataField="Description"  Visible="true">
                </telerik:GridBoundColumn>   
                
                <telerik:GridBoundColumn UniqueName="Activo"
                    SortExpression="Activo" HeaderText="Activo" DataField="Activo"  Visible="true" HeaderStyle-Width="12%">
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
