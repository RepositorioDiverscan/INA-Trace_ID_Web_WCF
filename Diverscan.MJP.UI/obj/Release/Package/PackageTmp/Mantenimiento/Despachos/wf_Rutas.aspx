<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Rutas.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Despachos.wf_Rutas" %>
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
                                    <telerik:RadTab Text="Ruta" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Rutas">
                                                <h1 class="TituloPanelTitulo">Datos de la Ruta</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5"> 
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label39" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />   
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>

                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                    <%--<asp:Label ID="Label36" runat="server" Text="|||"></asp:Label>--%>
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click" Visible="false"/>   
                                                    <asp:Label ID="Label35" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()" OnClick ="btnLimpiar_Click" /> 

                                                    <h1></h1>
                                                    
                                                    <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                   <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif"" style="width:80px;height:80px;/>--%>
                                                    </center>
                                                    </div>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                        <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="ID Ruta"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRuta" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" ></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click"  />--%>
                                                        </td>
                                                        </tr>
                                                        <tr> 
                                                        <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Dia Semana"></asp:Label> 
                                                        </td>
                                                        <td>
                                                         <asp:DropDownList ID="ddlidDia" Class="TexboxNormal"  runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td> 
                                                            <asp:Label ID="Label24" runat="server" Text="Nombre"></asp:Label> 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtDescripcion" Width="300px"   ></asp:TextBox>
                                                        </td>
                                                        </tr> 
                                                         <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Comentarios"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtComentarios" Width="300px"   ></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td> 
                                                        <asp:Label ID="Label40" runat="server" Text="Compania"></asp:Label>       
                                                        </t>
                                                        <td>
                                                        <asp:DropDownList ID="ddlidCompania" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                        </td>   
                                                        </tr>
                                                        <tr>
                                                    </table>
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Rutas0">
                                                <h1 class="TituloPanelTitulo">Listado Rutas</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridRutas" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridRutas_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridRutas_ItemCommand" PagerStyle-AlwaysVisible="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idRuta"
                                                                    SortExpression="idRuta" HeaderText="Id Ruta" DataField="idRuta">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idDia" Display="false"
                                                                    SortExpression="idDia" HeaderText="Id Dia" DataField="idDia">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NombreDia"
                                                                    SortExpression="NombreDia" HeaderText="Nombre Dia" DataField="NombreDia">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Comentarios"
                                                                    SortExpression="Comentarios" HeaderText="Comentarios" DataField="Comentarios">
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