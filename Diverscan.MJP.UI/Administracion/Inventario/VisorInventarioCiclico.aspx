<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorInventarioCiclico.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.VisorInventarioCiclico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
     </script>

     <asp:Panel ID="Panel4" runat="server" >                       

        <div id="RestrictionZoneID" class="WindowContenedor">
             
                <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                    <Shortcuts>
                        <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                        <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                    </Shortcuts>

                    <Windows >
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Resultados Inventario Ciclico" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                   
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                      <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                   <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                    </center>
                                                    </div>

                                                     <telerik:RadDatePicker ID="_rdpB_Fecha" runat="server" AutoPostBack ="true" OnSelectedDateChanged="_rdpB_Fecha_SelectedDateChanged" ></telerik:RadDatePicker>  

                                                    <asp:DropDownList runat="server" ID="_ddlInventariosDisponibles" OnSelectedIndexChanged="_ddlInventariosDisponibles_SelectedIndexChanged" AutoPostBack="true"
                                                        ></asp:DropDownList>
                                                    <asp:DropDownList runat="server" ID="_ddlArticulosCategoria"></asp:DropDownList>

                                                    <asp:TextBox runat="server" ID ="_txtArticuloBusqueda" OnTextChanged ="_txtArticuloBusqueda_TextChanged1" AutoPostBack="true"></asp:TextBox>
                                                                                                        
                                                    <asp:Button  runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click"/>
                                                     <asp:Button runat="server" ID="_btnExportar" Text="Exportar"  OnClick ="_btnExportar_Click"/>
                                                    
                                                     <telerik:RadGrid ID="RGBodegaFisica_SistemaRecord" AllowPaging="True" Width="100%"  OnNeedDataSource ="RGBodegaFisica_SistemaRecord_NeedDataSource"
                                                            runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                               
                                                                 <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdUbicacion" Display="false"
                                                                    SortExpression="IdUbicacion" HeaderText="IdUbicacion" DataField="IdUbicacion">
                                                                </telerik:GridBoundColumn>
                                                                                          
                                                                <telerik:GridBoundColumn UniqueName="NombreArticulo"
                                                                    SortExpression="NombreArticulo" HeaderText="NombreArticulo" DataField="NombreArticulo">
                                                                </telerik:GridBoundColumn>
                                                                                     
                                                                <telerik:GridBoundColumn UniqueName="Etiqueta"
                                                                    SortExpression="Etiqueta" HeaderText="Ubicacion" DataField="Etiqueta">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                    SortExpression="UnidadMedida" HeaderText="UnidadMedida" DataField="UnidadMedida">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CantidadBodegaParaMostrar"
                                                                    SortExpression="CantidadBodegaParaMostrar" HeaderText="Cantidad Bodega" DataField="CantidadBodegaParaMostrar">
                                                                </telerik:GridBoundColumn>  
                                                                  
                                                                <telerik:GridBoundColumn UniqueName="CantidadSistemaParaMostrar"
                                                                    SortExpression="CantidadSistemaParaMostrar" HeaderText="Cantidad Sistema" DataField="CantidadSistemaParaMostrar">
                                                                </telerik:GridBoundColumn> 

                                                                 <telerik:GridBoundColumn UniqueName="DifenrenciaCantidad"
                                                                    SortExpression="DifenrenciaCantidad" HeaderText="Difenrencia" DataField="DifenrenciaCantidad">
                                                                </telerik:GridBoundColumn>   

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                            </ClientSettings>
                                                    </telerik:RadGrid>
                                                   
                                                           <asp:Label runat="server" ID="_lblTotalTomaFisica" ForeColor="Red"></asp:Label>
                                                       <h1></h1>
                                                            <asp:Label runat="server" ID="_lblTotalSistema"  ForeColor="Red"></asp:Label>
                                                        <h1></h1>
                                                        <asp:Label runat="server" ID="_lblTotalSAP"  ForeColor="Red"></asp:Label>                              
                                                    <h1></h1>
                                                        <asp:Button  runat="server" ID="_btnRealizarAjuste" Text="RealizarAjuste" OnClick="_btnRealizarAjuste_Click" OnClientClick="DisplayLoadingImage1()"/>                                                                                     
                                                </ContentTemplate>                                          
                                            </asp:UpdatePanel>                                                                                          
                                            </asp:Panel>
                                    </telerik:RadPageView>

                                </telerik:RadMultiPage>
                            </ContentTemplate>
                            <Shortcuts>
                                <telerik:WindowShortcut CommandName="Maximize" Shortcut="Ctrl+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Minimize" Shortcut="Ctrl+F7"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                            </Shortcuts>
                           
                        </telerik:RadWindow>
        
                    </Windows>
                </telerik:RadWindowManager> 

               </div>                      

   </asp:Panel>
</asp:Content>
