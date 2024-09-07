<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_ConsultarMaestroUbicaciones.aspx.cs" Inherits="Diverscan.MJP.UI.Consultas.Articulos.wf_ConsultarMaestroUbicaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
     <asp:Panel ID="Panel4" runat="server" >   
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                           
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
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Almacen" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Bodega" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Zona " Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Ubicacion " Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <%-- ALMACEN --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_OC">
                                                <h1 class="TituloPanelTitulo">Datos del Almacen</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <%-- BUSCAR --%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label39" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />   

                                                                <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click"/>
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                    </div>

                                                    <telerik:RadGrid ID="RadGridAlmacen" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridAlmacen_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridAlmacen_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idAlmacen"
                                                                    SortExpression="idAlmacen" HeaderText="Id Almacen" DataField="idAlmacen">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                    SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="nombre"
                                                                    SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="descripcion"
                                                                    SortExpression="descripcion" HeaderText="Descripcion" DataField="descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idCompania"
                                                                    SortExpression="idCompania" HeaderText="Id Compania" DataField="idCompania">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                                 <Triggers>     
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                            </asp:Panel>
                                    </telerik:RadPageView>

                                    <%-- BODEGA --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView2"  >
                                        <asp:Panel ID="Panel2" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel3">
                                                <h1 class="TituloPanelTitulo">Datos de la Bodega</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" >
                                                <ContentTemplate>

                                                    <%-- BUSCAR --%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearchBodega" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscarBodega" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarBodega_Click"  />   

                                                                <asp:Button runat="server" ID="btnRefrescarBodega" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescarBodega_Click"/>
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="Img1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                    </div>

                                                    <telerik:RadGrid ID="RadGridBodega" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridBodega_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridBodega_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idBodega"
                                                                    SortExpression="idBodega" HeaderText="Id Bodega" DataField="idBodega">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idAlmacen" Display ="false"
                                                                    SortExpression="idAlmacen" HeaderText="Id Almacen" DataField="idAlmacen">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreAlmacen"
                                                                    SortExpression="NombreAlmacen" HeaderText="Almacen" DataField="NombreAlmacen">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                    SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="nombre"
                                                                    SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="descripcion"
                                                                    SortExpression="descripcion" HeaderText="Descripcion" DataField="descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="secuencia"
                                                                    SortExpression="secuencia" HeaderText="Secuencia" DataField="secuencia">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                                 <Triggers>     
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                            </asp:Panel>
                                    </telerik:RadPageView>

                                    <%-- ZONA --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView3"  >
                                        <asp:Panel ID="Panel5" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel6">
                                                <h1 class="TituloPanelTitulo">Datos de la Zona</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>

                                                    <%-- BUSCAR --%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearchZona" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscarZona" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarZona_Click"  />   

                                                                <asp:Button runat="server" ID="btnRefrescarZona" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescarZona_Click"/>
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="Img2" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                    </div>

                                                    <telerik:RadGrid ID="RadGridZona" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridZona_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridZona_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idZona"
                                                                    SortExpression="idZona" HeaderText="Id Zona" DataField="idZona">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Abreviatura"
                                                                    SortExpression="Abreviatura" HeaderText="Abreviatura" DataField="Abreviatura">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="nombre" HeaderText="Nombre" DataField="nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="descripcion" HeaderText="Descripcion" DataField="descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="secuencia"
                                                                    SortExpression="secuencia" HeaderText="Secuencia" DataField="secuencia">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                                 <Triggers>     
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                            </asp:Panel>
                                    </telerik:RadPageView>

                                    <%-- UBICACION --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView4"  >
                                        <asp:Panel ID="Panel7" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel8">
                                                <h1 class="TituloPanelTitulo">Datos de la Ubicación</h1>
                                                   <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" >
                                                <ContentTemplate>

                                                    <%-- BUSCAR --%>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearchUbicacion" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscarUbicacion" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarUbicacion_Click"  />   

                                                                <asp:Button runat="server" ID="btnRefrescarUbicacion" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescarUbicacion_Click"/>
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="Img3" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                    </div>

                                                    <telerik:RadGrid ID="RadGridUbicacion" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridUbicacion_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridUbicacion_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>

                                                                <telerik:GridBoundColumn UniqueName="idUbicacion"
                                                                    SortExpression="idUbicacion" HeaderText="Id Ubicacion" DataField="idUbicacion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idBodega" Display="false"
                                                                    SortExpression="idBodega" HeaderText="Id Bodega" DataField="idBodega">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreBodega"
                                                                    SortExpression="NombreBodega" HeaderText="Nombre Bodega" DataField="NombreBodega">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idZona" Display="false"
                                                                    SortExpression="idZona" HeaderText="ID Zona" DataField="idZona">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreZona"
                                                                    SortExpression="NombreZona" HeaderText="Nombre Zona" DataField="NombreZona">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="estante"
                                                                    SortExpression="estante" HeaderText="Estante" DataField="estante">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="nivel"
                                                                    SortExpression="nivel" HeaderText="Nivel" DataField="nivel">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="columna"
                                                                    SortExpression="columna" HeaderText="Columna" DataField="columna">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="pos"
                                                                    SortExpression="pos" HeaderText="Posición" DataField="pos">
                                                                </telerik:GridBoundColumn>

                                                              <%--  <telerik:GridBoundColumn UniqueName="largo"
                                                                    SortExpression="largo" HeaderText="Largo" DataField="largo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="areaAncho"
                                                                    SortExpression="areaAncho" HeaderText="Area Ancho" DataField="areaAncho">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="alto"
                                                                    SortExpression="alto" HeaderText="Alto" DataField="alto">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="cara"
                                                                    SortExpression="cara" HeaderText="Cara" DataField="cara">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="profundidad"
                                                                    SortExpression="profundidad" HeaderText="Profundidad" DataField="profundidad">
                                                                </telerik:GridBoundColumn>--%>

                                                            <%--    <telerik:GridBoundColumn UniqueName="CapacidadPesoKilos"
                                                                    SortExpression="CapacidadPesoKilos" HeaderText="Capacidad Peso Kilos" DataField="CapacidadPesoKilos">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CapacidadVolumenM3"
                                                                    SortExpression="CapacidadVolumenM3" HeaderText="Capacidad Volumen M3" DataField="CapacidadVolumenM3">
                                                                </telerik:GridBoundColumn>--%>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripción" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Secuencia"
                                                                    SortExpression="Secuencia" HeaderText="Secuencia" DataField="Secuencia">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>

                                                </ContentTemplate>
                                                 <Triggers>     
                                                </Triggers>
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