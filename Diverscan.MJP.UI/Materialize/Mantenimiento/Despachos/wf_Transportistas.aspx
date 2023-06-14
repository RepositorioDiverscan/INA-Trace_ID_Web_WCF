<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Transportistas.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Despachos.wf_Transportistas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
                <script type='text/javascript'>
                        function DisplayLoadingImage1() {
                            document.getElementById("loading1").style.display = "block";
                        }
                        function DisplayLoadingImage2() {
                            document.getElementById("loading2").style.display = "block";
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
                                    <telerik:RadTab Text="Transportistas" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Vehiculos" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <%-- Transportistas --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Transportistas">
                                                <h1 class="TituloPanelTitulo">Datos del transportista</h1>
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
                                                                <asp:TextBox ID="txtSearchTransportistas" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscarTransportistas" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarTransportistas_Click"  />   
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br>
                                                        <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                        <%--<asp:Label ID="Label24" runat="server" Text="|||"></asp:Label>--%>
                                                    	<asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click" Visible="false"/>  
                                                        <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                        <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar"  OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar1_Click" />    
                                                    <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                 </div>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                        <tr>
                                                             <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Id Transportista"></asp:Label> 
                                                            </td>
                                                            <td >     
                                                                <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidTransportista" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" ></asp:TextBox>   
                                                                <%--<asp:Button runat="server" ID="btnBuscar" Text="Buscar"  OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />--%>
                                                            </td>
                                                        </tr>
                                                        <tr> <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Compania"></asp:Label> 
                                                        </td>
                                                          <td>
                                                                <asp:DropDownList ID="ddlIdCompania" Class="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                          </td>
                                                      </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                            </td>
                                                
                                                             </tr>
                                                        <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Telefono"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtTelefono" Width="300px"  ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                        <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Correo"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtCorreo" Width="300px"   ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                        <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Comentarios"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtComentarios" Width="300px"   ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                    </table>
                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Transportistas0">
                                                <h1 class="TituloPanelTitulo">Listado Transportistas</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGridTransportistas" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridTransportistas_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridTransportistas_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idTransportista"
                                                                    SortExpression="idTransportista" HeaderText="Id Transportista" DataField="idTransportista">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdCompania"
                                                                    SortExpression="IdCompania" HeaderText="Id Compania" DataField="IdCompania">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Telefono"
                                                                    SortExpression="Telefono" HeaderText="Telefono" DataField="Telefono">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Correo"
                                                                    SortExpression="Correo" HeaderText="Correo" DataField="Correo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Comentarios"
                                                                    SortExpression="Comentarios" HeaderText="Comentarios" DataField="Comentarios">
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

                                    <%-- Vehiculos --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Vehiculos">
                                            <h1 class="TituloPanelTitulo">Datos Vehiculo</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>

                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3"> 
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtSearchVehiculos" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                            <asp:Button runat="server" ID="btnBuscarVehiculos" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscarVehiculos_Click"  />
                                                        
                                                        </td>
                                                    </tr>
                                                </table>

                                                <br>
                                                    
                                                    <asp:Button  ID="btnAgregar2" runat ="server" Text ="Agregar" Width ="150px"  OnClientClick="DisplayLoadingImage2()" OnClick ="btnAgregar2_Click"/>
                                                    <%--<asp:Label ID="Label26" runat="server" Text="|||"></asp:Label>--%>
                                                    <asp:Button ID ="btnEditar2" runat ="server" Text ="Editar" Width ="150px"  OnClientClick="DisplayLoadingImage2()" OnClick ="btnEditar2_Click" Visible="false" />  
                                                    <asp:Label ID="Label25" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar2" runat="server" Text="Limpiar"  OnClientClick="DisplayLoadingImage2()" OnClick ="Btnlimpiar2_Click" />   
                                                                                   
                                                 <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading2" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                 </div>


                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                  
                                                    <tr>
                                                    <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Id Vehículo"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidVehiculo" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" ></asp:TextBox>   
                                                    <%--<asp:Button runat="server" ID="Button1" Text="Buscar"  OnClientClick="DisplayLoadingImage2()" OnClick="btnBuscar_Click"  />--%>
                                                    </td>
                                                    </tr>
                                                     <tr> 
                                                         <td>
                                                            <asp:Label ID="Label16" runat="server" Text="Compania"></asp:Label> 
                                                         </td>
                                                         <td>
                                                             <asp:DropDownList ID="ddlIdCompania0" Class="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                          </td>
                                                     </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="Transportista"></asp:Label>       
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlidTransportista" CssClass="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                    </td>   
                                                    </tr>
                                                     <tr>
                                                    <td> 
                                                        <asp:Label ID="Label14" runat="server" Text="Tipo Vehiculo"></asp:Label>       
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIdTipoVehiculo" CssClass="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                    </td>   
                                                    </tr>
                                                     <tr>
                                                     <td> 
                                                        <asp:Label ID="Label22" runat="server" Text="Marca Vehiculo"></asp:Label>       
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIdMarcaVehiculo" CssClass="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td> 
                                                        <asp:Label ID="Label12" runat="server" Text="Color"></asp:Label>       
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlidcolor" CssClass="TexboxNormal"  runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Placa"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txtPlaca" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    </td>
                                                    </tr>                             
                                                    <tr>
                                                      <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Modelo (año - tipo)"></asp:Label> 
                                                      </td>
                                                      <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtModelo" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                      </td>                               
                                                    </tr>
                                                     <tr>
                                                      <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Capacidad volumen (m3)"></asp:Label> 
                                                      </td>
                                                      <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCapacidadVolumen" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                      </td>                               
                                                    </tr>
                                                     <tr>
                                                      <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Capacidad peso (kg)"></asp:Label> 
                                                      </td>
                                                      <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtCapacidadPeso" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                      </td>                               
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Comentario"></asp:Label> 
                                                    </td>
                                                    <td >     
                                                        <asp:TextBox CssClass="TexboxNormal" ID="txtComentario" runat="server" Width="400px"  TextMode="MultiLine"  Height="75px" ></asp:TextBox>   
                                                    </td>
                                                    </tr>
                                                 
                                                 </table>
                                                 <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Vehiculos0">
                                                    <h1 class="TituloPanelTitulo">Listado vehiculos</h1>
                                                </asp:Panel>                                    
                                         <telerik:RadGrid ID="RadGridVehiculos" AllowPaging="True" Width="100%" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridVehiculos_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridVehiculos_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="IdVehiculo"
                                                                    SortExpression="IdVehiculo" HeaderText="Id Vehiculo" DataField="IdVehiculo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idCompania"
                                                                    SortExpression="idCompania" HeaderText="Compania" DataField="idCompania">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idTransportista" Display="false"
                                                                    SortExpression="idTransportista" HeaderText="Id Transportista" DataField="idTransportista">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreTransportista"
                                                                    SortExpression="NombreTransportista" HeaderText="Transportista" DataField="NombreTransportista">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="idTipoVehiculo" Display="false"
                                                                    SortExpression="idTipoVehiculo" HeaderText="Id Tipo Vehiculo" DataField="idTipoVehiculo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreTipoVehiculo"
                                                                    SortExpression="NombreTipoVehiculo" HeaderText="Tipo Vehiculo" DataField="NombreTipoVehiculo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idMarcaVehiculo" Display="false"
                                                                    SortExpression="idMarcaVehiculo" HeaderText="Id Marca Vehiculo" DataField="idMarcaVehiculo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreMarcaVehiculo"
                                                                    SortExpression="NombreMarcaVehiculo" HeaderText="Marca Vehiculo" DataField="NombreMarcaVehiculo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idColor" Display="false"
                                                                    SortExpression="idColor" HeaderText="Id Color" DataField="idColor">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreColor"
                                                                    SortExpression="NombreColor" HeaderText="Color" DataField="NombreColor">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Placa"
                                                                    SortExpression="Placa" HeaderText="Placa" DataField="Placa">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Modelo"
                                                                    SortExpression="Modelo" HeaderText="Modelo" DataField="Modelo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CapacidadVolumen"
                                                                    SortExpression="CapacidadVolumen" HeaderText="Capacidad Volumen" DataField="CapacidadVolumen">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="CapacidadPeso"
                                                                    SortExpression="CapacidadPeso" HeaderText="Capacidad Peso" DataField="CapacidadPeso">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Comentario"
                                                                    SortExpression="Comentario" HeaderText="Comentario" DataField="Comentario">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>
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
