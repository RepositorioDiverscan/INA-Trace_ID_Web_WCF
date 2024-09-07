<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_GTIN14.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_GTIN14" %>
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
                            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                                <telerik:AjaxSetting AjaxControlID="RadGrid2">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid2"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>          
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
                                    <telerik:RadTab Text="GTIN14" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <%-- GTIN14 --%>
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_ADMGTIN14VariableLogistica">
                                            <h1 class="TituloPanelTitulo">Gtin</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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


                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                <%--<asp:Label ID="Label1" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnEditar_Click" Visible="false" />
                                                <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage1()" OnClick ="btnLimpiar_Click" />
                                                <h1></h1>

                                                
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                 </div>

                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="GTIN14"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidGTIN14VariableLogistica" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />--%>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label27" runat="server" Text="Artículo"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:DropDownList runat="server" ID="ddlDescripcion" CssClass="TexboxNormal" AutoPostBack="false" Width="300px"></asp:DropDownList>
                                                      </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Id Interno" Visible ="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtidInterno" runat="server" Width="300px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Compañia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlidCompania" CssClass="TexboxNormal" AutoPostBack="false" Width="300px" ></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                   

                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label5" runat="server" Text="GTIN14"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txtConsecutivoGTIN14" runat="server" Width="300px"></asp:TextBox>
                                                        <asp:Button ID="BtnGenera" runat="server" Text="Generar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="BtnGenera_Click" />
                                                      </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCantidad" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Contenido"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtContenido" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Unidad Medida"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtNombre" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="chkActivo"   Text = "Activo" Checked="true"></asp:CheckBox>
                                                            <asp:CheckBox runat="server" ID="ChkGenerado" Text = "Activo" Checked="false" Visible="false"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    

                                                </table>
                                                <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_ADMGTIN14VariableLogistica0">
                                            <h1 class="TituloPanelTitulo">Listado de GTIN 14</h1>
                                        </asp:Panel>
                                        <telerik:RadGrid ID="RadGridGTIN14" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridGTIN14_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="50" AllowMultiRowSelection="true" OnItemCommand="RadGridGTIN14_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idGTIN14VariableLogistica"
                                                                    SortExpression="idGTIN14VariableLogistica" HeaderText="Id GTIN14" DataField="idGTIN14VariableLogistica">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInterno"
                                                                    SortExpression="idInterno" HeaderText="Id Interno" DataField="idInterno">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="idCompania"
                                                                    SortExpression="idCompania" HeaderText="Id Compania" DataField="idCompania">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="ConsecutivoGTIN14"
                                                                    SortExpression="ConsecutivoGTIN14" HeaderText="GTIN14" DataField="ConsecutivoGTIN14">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Contenido"
                                                                    SortExpression="Contenido" HeaderText="Contenido" DataField="Contenido">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Unidad Medida" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Activo"
                                                                    SortExpression="Activo" HeaderText="Activo" DataField="Activo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                    SortExpression="FechaRegistro" HeaderText="Fecha Registro" DataField="FechaRegistro"
                                                                    DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
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


