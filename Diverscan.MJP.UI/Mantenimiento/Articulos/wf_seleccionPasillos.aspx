<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_seleccionPasillos.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_seleccionPasillos" %>

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
           <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow" 
               style="width: 80%; height: 70%; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px;
                margin-left:1%; margin-top:2%">
            <br />
         <label>Bodega:</label>
         <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownBod_SelectedIndexChanged"></asp:DropDownList>
       
                   <br />
                   <br />
                   <br />
    
         <table>
             <tr>
             <th> <label>Pasillo: </label><asp:DropDownList runat="server" ID="ddEstante" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownPas_SelectedIndexChanged"></asp:DropDownList></th>
             <th> </th>
             <th> <label>Sub-Sector: </label><asp:DropDownList runat="server" ID="ddSubSector" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDropDownSector_SelectedIndexChanged"></asp:DropDownList></th>
             </tr>
             
             <tr>
             <td>
               <telerik:RadGrid ID="RadGridPasillo" AllowPaging="True" Width="100%" OnNeedDataSource="RadGridPasillo_NeedDataSource"
                            AllowFilteringByColumn="True" runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
            <MasterTableView>
            <Columns>
        
                  <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                      </telerik:GridClientSelectColumn>  

                <telerik:GridBoundColumn UniqueName="idUbicacion"
                    SortExpression="idUbicacion" HeaderText="idUbicacion" FilterControlWidth="50px" DataField="idUbicacion" Visible="true">
                </telerik:GridBoundColumn>

                 <telerik:GridBoundColumn UniqueName="descripcion"
                    SortExpression="descripcion" HeaderText="Descripción" FilterControlWidth="50px" DataField="descripcion" Visible="true">
                </telerik:GridBoundColumn>

                 <telerik:GridBoundColumn UniqueName="estante"
                    SortExpression="estante" HeaderText="Estante" FilterControlWidth="50px" DataField="estante" Visible="true">
                </telerik:GridBoundColumn>

            </Columns>
        </MasterTableView>   
            <ClientSettings EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>                                                                
            </ClientSettings>
                 </telerik:RadGrid>  
             </td>

             <td>
                 <asp:Button ID="btn_ingresar" runat="server" Text="-->>" OnClientClick = "DisplayLoadingImage1()"  OnClick="BtnIngresar_onClick" />
              <br /> 
            <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="Cargando" >
              <br /> 
               <asp:Button ID="btn_regresar" runat="server" Text="<<--" OnClientClick = "DisplayLoadingImage1()"  OnClick="BtnRegresar_onClick"/>
          </td>
                  <td>
           <telerik:RadGrid ID="RadGridSubSector" AllowPaging="True" Width="100%" OnNeedDataSource="RadGridSubSector_NeedDataSource"
                        AllowFilteringByColumn="True"  runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" >
            <MasterTableView>
            <Columns>
        
                   <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>  

                <telerik:GridBoundColumn UniqueName="idUbicacion"
                    SortExpression="IdSectorWarehouse" HeaderText="IdUbicacion" DataField="idUbicacion" Visible="true">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="idSectorBodega"
                    SortExpression="idSectorBodega" HeaderText="IdSector" FilterControlWidth="50px" DataField="idSectorBodega" Visible="true" AllowFiltering="true">
                </telerik:GridBoundColumn>       
                
                  <telerik:GridBoundColumn UniqueName="descripcion"
                    SortExpression="descripcion" HeaderText="Descripción" FilterControlWidth="50px" DataField="descripcion" Visible="true" AllowFiltering="true">
                </telerik:GridBoundColumn>   

                  <telerik:GridBoundColumn UniqueName="estante"
                    SortExpression="estante" HeaderText="Estante"  FilterControlWidth="50px" DataField="estante" Visible="true" AllowFiltering="true">
                </telerik:GridBoundColumn>   

            </Columns>
        </MasterTableView>   
            <ClientSettings EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true"></Selecting>                                                                
            </ClientSettings>
                 </telerik:RadGrid>  
              </td>
                </tr>
         </table>

           </div>

       </ContentTemplate> 
    </asp:UpdatePanel>

 </asp:Panel>
</asp:Content>