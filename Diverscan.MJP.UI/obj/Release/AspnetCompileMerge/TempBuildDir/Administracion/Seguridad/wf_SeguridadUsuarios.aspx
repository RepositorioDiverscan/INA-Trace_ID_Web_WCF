
<%@ Page Title="" Language="C#"  MaintainScrollPositionOnPostBack="true" MasterPageFile="~/Site.Master" AutoEventWireup="true"   CodeBehind="wf_SeguridadUsuarios.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Seguridad.wf_SeguridadUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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

                            function DisplayLoadingImage3() {
                                document.getElementById("loading3").style.display = "block";
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
                                    <telerik:RadTab Text="Usuario" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Rol" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Accesos por Rol WEB" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Accesos por Rol HH" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <%-- Usuarios --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                  
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Usuarios">
                                                <h1 class="TituloPanelTitulo">Datos Usuario</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4"> 
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="Buscar Usuario" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px" ></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="Button1" Text="Buscar" AutoPostBack="true" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <br />

                                                    <asp:Button ID ="btnAgregar" runat ="server" Text ="Agregar" Width ="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                    <%--<asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>--%>
                                                    <asp:Button ID ="btnEditar" runat ="server" Text ="Editar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnEditar_Click" Visible="false"/>
                                                    <asp:Label ID="Label14" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar"  OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar1_Click" />
                                                    <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>

                                                  <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      

                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label24" runat="server" Text="Id Usuario" Width ="100px"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtIDUSUARIO" runat="server" Width="85px" AutoCompleteType="Disabled" Visible="true" Enabled="false"></asp:TextBox>  
                                                </td>
                                               </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text ="Compañía"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlIdCompania" CssClass="TexboxNormal" Width="250px" AutoPostBack="false"></asp:DropDownList>
                                                </td>
                                                </tr>
                                               <tr>
                                                 <td>
                                                   <asp:Label ID="Label3" runat="server" Text="Usuario"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtUsuario" runat="server" Width="250px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                            </tr>
                                            <tr>
                                              <td> 
                                                <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label> 
                                              </td>
                                              <td>
                                                <asp:TextBox runat="server" ID="txtNOMBRE_PILA" Class="TexboxNormal" Width="250px" ></asp:TextBox>
                                              </td>
                                            </tr>
                                              <tr>
                                              <td>
                                                <asp:Label ID="Label2" runat="server" Text="Apellidos"></asp:Label>
                                              </td>
                                              <td>
                                                <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtAPELLIDOS_PILA" Width="250px" ></asp:TextBox>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                <asp:Label ID="Label4" runat="server" Text="Email"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:TextBox CssClass="TexboxNormal" ID="txtEMAIL" runat="server" Width="250px" ></asp:TextBox>  
                                              </td>
                                                </tr>
                                                 <tr>
                                                <td> 
                                                  <asp:Label ID="Label15" runat="server" Text="Rol"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlidRol" CssClass="TexboxNormal" Width="250px" AutoPostBack="false"></asp:DropDownList>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td> 
                                                  <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" 
                                                        OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"/>
                                                </td>
                                            </tr>
                                               <tr>
                                                <td> 
                                                  <asp:Label ID="Label19" runat="server" Text="Sector"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddSector" CssClass="TexboxNormal" Width="250px" AutoPostBack="false"></asp:DropDownList>
                                                </td>
                                             </tr>

                                              <tr>
                                                <td>
                                                  <asp:Label ID="Label6" runat="server" Text="Comentarios"></asp:Label>  
                                                </td>
                                                <td>
                                                  <asp:TextBox CssClass="TexboxNormal" ID="txtCOMENTARIO" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                              </tr>
                                             <tr>
                                               <td></td>
                                               <td>
                                                 <asp:CheckBox runat="server" ID="chkESTA_BLOQUEADO" Text="Bloqueado"></asp:CheckBox>
                                               </td>
                                             </tr>

                                              <tr>
                                               <td></td>
                                               <td>
                                                 <asp:CheckBox runat="server" ID="chkCambiarContrasenna" Text="Cambiar Contraseña" AutoPostBack="true" OnCheckedChanged="chkCambiarContrasenna_CheckedChanged" Visible="false"></asp:CheckBox>
                                               </td>
                                             </tr>
                                             <tr>
                                                <td>
                                                  <asp:Label ID="Label7" runat="server" Text="Contraseña" Visible="true"></asp:Label>       
                                                </td>
                                                <td>
                                                  <asp:TextBox CssClass="TexboxNormal" ID="txtCONTRASENNA" runat="server" Width="250px" visible ="true" TextMode ="Password" ></asp:TextBox>  
                                                </td>
                                                 </tr>
                                            <tr>
                                              <td>
                                                <asp:Label ID="Label5" runat="server" Text="Repita Contraseña" Visible="true"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:TextBox CssClass="TexboxNormal" ID="txtRepitacontraseña" runat="server" Width="250px" visible ="true" TextMode ="Password"></asp:TextBox>  
                                              </td> 
                                            </tr>
                                              </table>  
                                                     <h1 class="TituloPanelTitulo">Detalle Usuarios</h1>
                                                        <telerik:RadGrid ID="RadGridUsuarios" AllowPaging="True" Width="100%"  OnNeedDataSource ="RadGridUsuarios_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="50" AllowMultiRowSelection="true" OnItemCommand="RadGridUsuarios_ItemCommand" PagerStyle-AlwaysVisible="true">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="IDUSUARIO"
                                                                    SortExpression="IDUSUARIO" HeaderText="Id Usuario" DataField="IDUSUARIO">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdCompania"
                                                                    SortExpression="IdCompania" HeaderText="Id Compania" DataField="IdCompania">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Usuario"
                                                                    SortExpression="Usuario" HeaderText="Usuario" DataField="Usuario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NOMBRE_PILA"
                                                                    SortExpression="NOMBRE_PILA" HeaderText="Nombre" DataField="NOMBRE_PILA">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="APELLIDOS_PILA"
                                                                    SortExpression="APELLIDOS_PILA" HeaderText="Apellidos" DataField="APELLIDOS_PILA">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="EMAIL"
                                                                    SortExpression="EMAIL" HeaderText="Correo" DataField="EMAIL">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdRol" Display="false"
                                                                    SortExpression="IdRol" HeaderText="Id Rol" DataField="IdRol">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreRol"
                                                                    SortExpression="NombreRol" HeaderText="Nombre Rol" DataField="NombreRol">
                                                                </telerik:GridBoundColumn>
                                              
                                                                <telerik:GridBoundColumn UniqueName="COMENTARIO"
                                                                    SortExpression="COMENTARIO" HeaderText="Comentario" DataField="COMENTARIO">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="ESTA_BLOQUEADO"
                                                                    SortExpression="ESTA_BLOQUEADO" HeaderText="Esta Bloqueado" DataField="ESTA_BLOQUEADO">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="NOMBRE_BODEGA"
                                                                    SortExpression="NOMBRE_BODEGA" HeaderText="Bodega" DataField="NOMBRE_BODEGA">
                                                                </telerik:GridBoundColumn>

                                                                  <telerik:GridBoundColumn UniqueName="NOMBRE_SECTOR"
                                                                    SortExpression="NOMBRE_SECTOR" HeaderText="Sector" DataField="NOMBRE_SECTOR" >
                                                                </telerik:GridBoundColumn>                                                             
                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid> 
                                              
                                                </ContentTemplate>
                                                 <Triggers>
                                                   <asp:AsyncPostBackTrigger ControlID="txtSearch" />
                                                </Triggers>



                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Usuarios0">
                            
                            
                                            </asp:Panel>
                                        </asp:Panel>
                                    </telerik:RadPageView>

                                    <%-- Rol--%>
                                    <telerik:RadPageView runat="server" ID="RadPageView3">                                  
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Roles">
                                                <h1 class="TituloPanelTitulo">Datos Rol</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                         
                                                    </asp:Panel>

                                            
                                            <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5"> 
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Text="Buscar Rol" Width ="100px"></asp:Label> 
                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtBuscarRoles" runat="server" AutoPostBack="true"  Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                    <asp:Button runat="server" ID="btnBuscarRoles" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage3()" OnClick="btnBuscarRoles_Click"  />   
                                                        
                                                </td>
                                            </tr>
                                            </table>

                                            <br>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    <asp:Button ID ="Button3" runat ="server" Text ="Agregar" Width ="150px" OnClientClick="DisplayLoadingImage3()" OnClick ="btnAgregar3_Click" />
                                                    <%--<asp:Label ID="Label20" runat="server" Text="|||"></asp:Label>--%>
                                                    <asp:Button ID ="Button4" runat ="server" Text ="Editar" Width ="150px" OnClientClick="DisplayLoadingImage3()" OnClick ="btnEditar3_Click" Visible="false" /> 
                                                    <asp:Label ID="Label22" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar3" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage3()" OnClick ="Btnlimpiar3_Click" />

                                                    <h1></h1>
                                                     
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading3" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">  
                                                  <tr>
                                                 <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Rol Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRol" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" ></asp:TextBox>   
                                                    <%--<asp:Button runat="server" ID="Button6" Text="Buscar" OnClientClick="DisplayLoadingImage3()" OnClick="btnBuscar_Click"  />--%>
                                                </td>
                                                 </tr>

                                               <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text ="Compañía"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlIdCompania0" CssClass="TexboxNormal" Width="250px" AutoPostBack="false"></asp:DropDownList>
                                                </td>
                                               </tr>
                                            
                                               <tr>
                                                 <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtNombre" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                               
                                              </tr>   
                                                    
                                              <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtDescripcion0" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                          
                                            </tr>

                                        </table>
                                        <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Roles0">
                                            <h1 class="TituloPanelTitulo">Detalles del Rol</h1>
                                        </asp:Panel>
                                                    <telerik:RadGrid ID="RadGridRoles" AllowPaging="True" Width="100%"  OnNeedDataSource ="RadGridRoles_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridRoles_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                               <%-- <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>--%>

                                                                <telerik:GridBoundColumn UniqueName="IdRol"
                                                                    SortExpression="IdRol" HeaderText="Id Rol" DataField="IdRol">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Descripcion"
                                                                    SortExpression="Descripcion" HeaderText="Descripción" DataField="Descripcion">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="IdCompania"
                                                                    SortExpression="IdCompania" HeaderText="IdCompania" DataField="IdCompania">
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

                                    <%--Accesos por Rol WEB--%>
                                    <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_AccesosporUsuario0">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Accesos Rol WEB</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;"/>                  
                                        </asp:Panel>
                                        <%--<h1></h1>--%>                                     
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                           <ContentTemplate>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading2" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>

                                             <table width="100%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial;" id="Table3">                      
                               
                                            <tr>
                                              <td>
                                                <asp:Label ID="Label18" runat="server" Text="Rol"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:DropDownList ID="ddlidRol0" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlidRol0_SelectedIndexChanged"></asp:DropDownList>
                                              </td>
                                            </tr>
                                                 <tr>
                                              <td>
                                                <asp:Label ID="lblIdFormularioSinAsignar" runat="server" Text="" Visible ="false"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:Label ID="lblIdFormularioAsignado" runat="server" Text="" Visible ="false"></asp:Label>       
                                              </td>
                                            </tr>
                                          </table>
                                               <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table6">
                                                   <tr>
                                                       <td valign="top" width="400px">
                                                           <asp:Panel runat="server"  Width="400px"  CssClass="TituloPanelVistaDetalle" ID="Panel5">
                                                               <h1 class="TituloPanelTitulo">Formularios Sin Asignar</h1>
                                                           </asp:Panel>
                                                           <telerik:RadGrid ID="RadGridFormulariosSinAsignar" AllowPaging="True" Width="400px" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridFormulariosSinAsignar_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" AllowMultiRowSelection="true" OnItemCommand="RadGridFormulariosSinAsignar_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idFormulario" 
                                                                    SortExpression="idFormulario" HeaderText="Id Formulario" DataField="idFormulario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>
                                                       </td>
                                                        
                                                       <td style="vertical-align:top;" align="center">
                                                           <table>
                                                               <tr>
                                                                   <td>
                                                                       <asp:Button runat="server" ID="btnAsignar" Text=">>>" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnAsignar_Click"  />
                                                                   </td>
                                                               </tr>

                                                               <tr>
                                                                   <td>
                                                                       <asp:Button runat="server" ID="btnSinAsignar" Text="<<<" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnSinAsignar_Click"  />
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </td>

                                                       <td valign="top" width="400px">
                                                            <asp:Panel runat="server"  Width="400px"  CssClass="TituloPanelVistaDetalle" ID="Vista_AccesosporUsuario">
                                                               <h1 class="TituloPanelTitulo">Formularios Asignados</h1>
                                                           </asp:Panel>
                                                           <telerik:RadGrid ID="RadGridFormulariosAsignados" AllowPaging="True" Width="400px" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridFormulariosAsignados_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" AllowMultiRowSelection="true" OnItemCommand="RadGridFormulariosAsignados_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idFormulario"
                                                                    SortExpression="idFormulario" HeaderText="Id Formulario" DataField="idFormulario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
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

                                        </ContentTemplate>
                                      </asp:UpdatePanel>

                                      </asp:Panel>
                                    </telerik:RadPageView>

                                    <%--Accesos por Rol HH--%>
                                    <telerik:RadPageView runat="server" ID="RadPageView4">
                                      <asp:Panel ID="Panel6" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_AccesosporUsuario00">
                                            <h1 class="TituloPanelTitulo">Mantenimiento Accesos Rol HH</h1>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;"/>                  
                                        </asp:Panel>
                                        <%--<h1></h1>--%>                                     
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                           <ContentTemplate>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="Img1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>

                                             <table width="100%" style="border-radius: 10px; border: 1px solid grey; border-collapse: initial;" id="Table7">                      
                               
                                            <tr>
                                              <td>
                                                <asp:Label ID="Label13" runat="server" Text="Rol"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:DropDownList ID="ddlidRol00" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlidRol00_SelectedIndexChanged"></asp:DropDownList>
                                              </td>
                                            </tr>
                                                 <tr>
                                              <td>
                                                <asp:Label ID="lblIdFormularioSinAsignarHH" runat="server" Text="" Visible ="false"></asp:Label>       
                                              </td>
                                              <td>
                                                <asp:Label ID="lblIdFormularioAsignadoHH" runat="server" Text="" Visible ="false"></asp:Label>       
                                              </td>
                                            </tr>
                                          </table>
                                               <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table8">
                                                   <tr>
                                                       <td valign="top" width="400px">
                                                           <asp:Panel runat="server"  Width="400px"  CssClass="TituloPanelVistaDetalle" ID="Panel8">
                                                               <h1 class="TituloPanelTitulo">Formularios Sin Asignar</h1>
                                                           </asp:Panel>
                                                           <telerik:RadGrid ID="RadGridFormulariosSinAsignarHH" AllowPaging="True" Width="400px" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridFormulariosSinAsignarHH_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" AllowMultiRowSelection="true" OnItemCommand="RadGridFormulariosSinAsignarHH_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idFormulario" 
                                                                    SortExpression="idFormulario" HeaderText="Id Formulario" DataField="idFormulario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>   
                                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="true"></Selecting>
                                                                
                                                            </ClientSettings>
                                                    </telerik:RadGrid>
                                                       </td>
                                                        
                                                       <td style="vertical-align:top;" align="center">
                                                           <table>
                                                               <tr>
                                                                   <td>
                                                                       <asp:Button runat="server" ID="btnAsignarHH" Text=">>>" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnAsignarHH_Click"  />
                                                                   </td>
                                                               </tr>

                                                               <tr>
                                                                   <td>
                                                                       <asp:Button runat="server" ID="btnSinAsignarHH" Text="<<<" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnSinAsignarHH_Click"  />
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </td>

                                                       <td valign="top" width="400px">
                                                            <asp:Panel runat="server"  Width="400px"  CssClass="TituloPanelVistaDetalle" ID="Panel9">
                                                               <h1 class="TituloPanelTitulo">Formularios Asignados</h1>
                                                           </asp:Panel>
                                                           <telerik:RadGrid ID="RadGridFormulariosAsignadosHH" AllowPaging="True" Width="400px" OnClientClick="DisplayLoadingImage1()"  OnNeedDataSource ="RadGridFormulariosAsignadosHH_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" AllowMultiRowSelection="true" OnItemCommand="RadGridFormulariosAsignadosHH_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="idFormulario"
                                                                    SortExpression="idFormulario" HeaderText="Id Formulario" DataField="idFormulario">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
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

                                        </ContentTemplate>
                                      </asp:UpdatePanel>

                                      </asp:Panel>
                                    </telerik:RadPageView>

                                    <%-- Rol--%>
                                    <%--<telerik:RadPageView runat="server" ID="RadPageView3">                                  
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Roles">
                                                <h1 class="TituloPanelTitulo">Datos Rol</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                         
                                                    </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    <asp:Button ID ="Button3" runat ="server" Text ="Agregar" Width ="150px" OnClientClick="DisplayLoadingImage3()" OnClick ="btnAgregar3_Click" />
                                                    <asp:Label ID="Label20" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID ="Button4" runat ="server" Text ="Editar" Width ="150px" OnClientClick="DisplayLoadingImage3()" OnClick ="btnEditar3_Click" /> 
                                                    <asp:Label ID="Label22" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar3" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage3()" OnClick ="Btnlimpiar3_Click" />
                                                     
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading3" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>

                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">  
                                                  <tr>
                                                 <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Rol Accion"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidRol" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button6" Text="Buscar" OnClientClick="DisplayLoadingImage3()" OnClick="btnBuscar_Click"  />
                                                </td>
                                                 </tr>
                                            
                                               <tr>
                                                 <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtNombre" runat="server" Width="150px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                </td>
                                               
                                              </tr>         
                                              <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtDescripcion0" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                             </tr>
                                             <tr>
                                               <td>
                                                 <asp:Label ID="Label24" runat="server" Text="Compañia:"></asp:Label>
                                               </td>
                                               <td>
                                                 <asp:DropDownList ID="ddlidCompania0" runat="server"></asp:DropDownList>
                                               </td>
                                             </tr>
                                           </table>
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Roles0">
                                                <h1 class="TituloPanelTitulo">Titulo para Detalles del GRID</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand" OnPreRender="RadGrid3_Prerender"   
                                                        AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource">
                                                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                             <Selecting AllowRowSelect="false" ></Selecting>
                                                            <Scrolling AllowScroll="True"  UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>                                               
                                                        </ClientSettings>
                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                            <MasterTableView>
                                                                <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="1, 3,10,15" />                                               
                                                            </MasterTableView>
                                                </telerik:RadGrid>
                                        </asp:Panel>                           
                                    </telerik:RadPageView>--%>
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

