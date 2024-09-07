<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_recaudo.aspx.cs" Inherits="Diverscan.MJP.UI.Recaudo.Recaudo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>

        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";        }     

    </script>

    <asp:Panel ID="Panel" runat="server">
        <div id="RestrictionZoneID" class="WindowContenedor">
            <telerik:radwindowmanager rendermode="Lightweight" offsetelementid="offsetElement" id="RadWindowManager1" runat="server" enableshadow="true"> 
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>
            <Windows > 
                <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1">
                                <Tabs>
                                    <telerik:RadTab Text="Recaudo" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                                                   
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Id Jornada"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIdJornada" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                                                          
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio"  runat="server" AutoPostBack="false" >
                                                               
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" ></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Correo Recaudador"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMail" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>                                                      
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>   
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" onclick="btnBuscar_Click"/>                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Búsqueda por los campos con (*)"></asp:Label>
                                                        </td>                                                      
                                                    </tr>
                                                </table>
                                                <br /> 

                                                 <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridJornadas" AllowFilteringByColumn="true" FilterType="CheckList"
                                                            OnNeedDataSource="RadGridArticulos_NeedDataSource"  AllowPaging="true" PagerStyle-AlwaysVisible="true" AllowSorting="true"
                                                        OnItemCommand="RadGridJornadas_ItemCommand">
                    
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                        </ClientSettings>
                                                        <MasterTableView AutoGenerateColumns="False">
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="checkJornada">
                                                                </telerik:GridClientSelectColumn>
                                                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" 
                                                                    DataField="CorreoRecaudador" FilterControlWidth="120px" 
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                                                    HeaderText="Correo">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" 
                                                                    DataField="FechaInicio" FilterControlWidth="120px" 
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                                                    HeaderText="Fecha Inicio">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" 
                                                                    DataField="FechaFin" FilterControlWidth="120px" 
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                                                    HeaderText="Fecha Fin">
                                                                </telerik:GridBoundColumn>    
                                                                 <telerik:GridBoundColumn FilterDelay="200" ShowFilterIcon="true" 
                                                                    DataField="EstadoToShow" FilterControlWidth="120px" 
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                                                    HeaderText="Estado">
                                                                </telerik:GridBoundColumn>  
                                                                </Columns>
                                                            </MasterTableView>
                                                       </telerik:RadGrid>

                                                <asp:Button runat="server" ID="btnCerrarJornada" Text="Cerrar Jornada" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" onclick="btnCerrarJornada_Click"/>

                                                 <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" ShowStatusBar="true" AutoGenerateColumns="False"
                                                    PageSize="7" AllowSorting="True" AllowMultiRowSelection="False" AllowPaging="True"
                                                    OnDetailTableDataBind="RadGrid1_DetailTableDataBind" OnNeedDataSource="RadGrid1_NeedDataSource">
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                    <MasterTableView DataKeyNames="IdRecaudo" AllowMultiColumnSorting="True">
                                                        <DetailTables>
                                                            <telerik:GridTableView DataKeyNames="NumeroFactura" Name="Detalle" Width="100%">                                                               
                                                                <Columns>
                                                                    <telerik:GridBoundColumn SortExpression="NumeroFactura" HeaderText="NumeroFactura" HeaderButtonType="TextButton"
                                                                        DataField="NumeroFactura">
                                                                    </telerik:GridBoundColumn>
                                                                 <telerik:GridBoundColumn SortExpression="TipoDocumento" HeaderText="TipoDocumento" HeaderButtonType="TextButton"
                                                                        DataField="TipoDocumento">
                                                                    </telerik:GridBoundColumn>
                                                                   <telerik:GridBoundColumn SortExpression="Monto" HeaderText="Monto" HeaderButtonType="TextButton"
                                                                        DataField="Monto">
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                        <Columns>
                                                              <telerik:GridBoundColumn SortExpression="IdRecaudo" HeaderText="IdRecaudo" HeaderButtonType="TextButton"
                                                                DataField="IdRecaudo">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="IdCliente" HeaderText="IdCliente" HeaderButtonType="TextButton"
                                                                DataField="IdCliente">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="NombreCliente" HeaderText="Cliente" HeaderButtonType="TextButton"
                                                                DataField="NombreCliente">
                                                            </telerik:GridBoundColumn>
                                                                 <telerik:GridBoundColumn SortExpression="FechaRegistro" HeaderText="FechaRegistro" HeaderButtonType="TextButton"
                                                                DataField="FechaRegistro">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="MontoFacturas" HeaderText="Monto Facturas" HeaderButtonType="TextButton"
                                                                DataField="MontoFacturas">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="MontoNotasCredito" HeaderText="MontoNotasCredito" HeaderButtonType="TextButton"
                                                                DataField="MontoNotasCredito">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="MontoNotasCreditoManual" HeaderText="MontoNotasCreditoManual" HeaderButtonType="TextButton"
                                                                DataField="MontoNotasCreditoManual">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>

                                          
                                            <br />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </ContentTemplate>                     
                    </telerik:RadWindow>
            </Windows>
          </telerik:radwindowmanager>
        </div>    
    </asp:Panel>

     <script type="application/javascript">
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }
   </script>
</asp:Content>


