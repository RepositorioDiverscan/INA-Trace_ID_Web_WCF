<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_RelUbicacionVehiculo.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Relacion.wf_RelUbicacionVehiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >   
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"><AjaxSettings>
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
                             <telerik:AjaxSetting AjaxControlID="RadGrid3">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid3"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>     
                            <telerik:AjaxSetting AjaxControlID="RadGrid4">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid4"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>     
                             <telerik:AjaxSetting AjaxControlID="RadGrid5">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid5"></telerik:AjaxUpdatedControl>
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
                                    <telerik:RadTab Text="Ubicación Vehículo" Width="100px"></telerik:RadTab>
                                    <telerik:RadTab Text="Actividad" Width= "100px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Acciones" Width="100px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="A. Metodos" Width="110px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="M. Parametros" Width="130px" Visible="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                        <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_RelUbicacionVehiculo">
                                                <h1 class="TituloPanelTitulo">Ubicación Vehículo</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClick="btnAgregar_Click" />
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClick="btnEditar_Click"/>      
                                                    <h1></h1>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                        <tr>
                                                             <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Id Ubicación Vehículo"></asp:Label> 
                                                            </td>
                                                            <td >     
                                                                <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidUbicacionVehiculo" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label2" runat="server" Text="Ruta"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidRuta" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td>   
                                                           </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label1" runat="server" Text="Dia"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidDia" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td> 
                                               
                                                        </tr>                                                                                                
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Hora dia"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidHoraDia" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td> 
                                                        </tr>  
                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="Label30" runat="server" Text="Vehiculo"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlIdVehiculo" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td> 
                                                        </tr>
                                                        <tr>
                                                             <td>
                                                                <asp:Label ID="Label31" runat="server" Text="Ubicacion"></asp:Label> 
                                                            </td>
                                                            <td >     
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtidUbicacion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            </td>
                                                        </tr>                                                                                
                                                    </table>                                             
                                                </ContentTemplate>
                                                 <Triggers>  
                                                </Triggers>
                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_RelUbicacionVehiculo0">
                                                <h1 class="TituloPanelTitulo">Listado de ubicación de vehículos</h1>
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Actividades">
                                            <h1 class="TituloPanelTitulo">Datos Actividad</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>                                   
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>   
                                                    
                                                 <asp:Button  ID="btnAgregar2" runat ="server" Text ="Agregar" Width ="150px" OnClick ="btnAgregar2_Click"/>
                                                 <asp:Button ID ="btnEditar2" runat ="server" Text ="Editar" Width ="150px" OnClick ="btnEditar2_Click" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Num Actividad"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidActividad" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <td> 
                                                                <asp:Label ID="Label4" runat="server" Text="Proceso"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidProceso" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td>   
                                                           </tr>         
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Nombre"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtNombre0" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                        </td>
                                                    </tr>           
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Descripcion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txmDescripcion0" runat="server" Width="400px"  TextMode="MultiLine"  Height="75px" ></asp:TextBox>   
                                                        </td>
                                                    </tr>
                                                 
                                                 </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Actividades0">
                                                    <h1 class="TituloPanelTitulo">Listado Actividades</h1>
                                                </asp:Panel>                                    
                                         <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize ="15" 
                                                            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource">
                                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                                 <Selecting AllowRowSelect="false"></Selecting>
                                                                <Scrolling AllowScroll="True" UseStaticHeaders="false" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                            </ClientSettings>
                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                <MasterTableView>
                                                                    <PagerStyle Mode="NextPrevAndNumeric" PageSizeLabelText="Page Size: " PageSizes="15,20,30,50" />         
                                                                </MasterTableView>
                                                    </telerik:RadGrid>
                                      </asp:Panel>
                                    </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="RadPageView3">                                  
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Acciones">
                                                <h1 class="TituloPanelTitulo">Datos Acciones</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    </asp:Panel>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>                           
                                                 <asp:Button  ID="Button4" runat ="server" Text ="Agregar" Width ="150px" OnClick ="btnAgregar3_Click"/>
                                                 <asp:Button ID ="Button5" runat ="server" Text ="Editar" Width ="150px" OnClick ="btnEditar3_Click" />                                      
                                                 <h1></h1>
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                                  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Num Accion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                            <asp:Button runat="server" ID="Button6" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <td> 
                                                                <asp:Label ID="Label7" runat="server" Text="Actividad"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidActividad" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                            </td>   
                                                           </tr>         
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Nombre"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox1" runat="server" Width="300px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                        </td>
                                                    </tr>           
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label> 
                                                        </td>
                                                        <td >     
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TextBox2" runat="server" Width="400px"  TextMode="MultiLine"  Height="75px" ></asp:TextBox>   
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:Label ID="LabelFuente" runat="server" Text="Fuente"></asp:Label>
                                                         </td>
                                                         <td>
                                                            <asp:DropDownList runat="server" CssClass="TexboxNormal" ID ="ddlFuente" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="ddlFuente_SelectedIndexChanged"></asp:DropDownList>
                                                         </td>
                                                       </tr>
                                                     <tr> 
                                                        <td>
                                                          <asp:Label ID="LabelObjetosForm" runat="server" CssClass="TexboxNormal" Text="Objeto fuente"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList runat="server" ID ="ddlObjetoFuente" CssClass="TexboxNormal" Width="200px"></asp:DropDownList>
                                                        </td>                                              
                                                     </tr>
                                                      <tr> 
                                                        <td>
                                                          <asp:Label ID="Label27" runat="server" CssClass="TexboxNormal" Text="Evento"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList runat="server" ID ="ddlidEvento" CssClass="TexboxNormal" Width="200px"></asp:DropDownList>
                                                        </td>                                              
                                                     </tr>
                                                 <h1></h1>
                                                 </table>                               
                                              </ContentTemplate>
                                                 <Triggers>                                             
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Acciones0">
                                                <h1 class="TituloPanelTitulo">Listado de Acciones</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="RadPageView4"  >
                                            <asp:Panel ID="Panel5" runat="server" Class="TabContainer"  > 
                                                <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_AccionMetodos">
                                                    <h1 class="TituloPanelTitulo">Accion - Metodos</h1>
                                                       <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                </asp:Panel>                                   
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" >
                                                    <ContentTemplate>
                                                        <asp:Button  ID="btnAgregar4" runat ="server" Text= "Agregar" Width ="150px" OnClick="btnAgregar4_Click" />
                                                        <asp:Button ID ="btnEditar4"  runat ="server" Text= "Editar" Width ="150px"  OnClick="btnEditar4_Click"/>      
                                                        <h1></h1>
                                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4">                      
                                                            <tr>
                                                                 <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="Num. Metodo-Accion"></asp:Label> 
                                                                </td>
                                                                <td >     
                                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidMetodoAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                                    <asp:Button runat="server" ID="Button7" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label14" runat="server" Text="Accion"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidAccion" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                                </td>   
                                                            </tr>
                                                             <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label15" runat="server" Text="Nombre Metodo"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidMetodo" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                                </td>   
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td><asp:CheckBox runat="server" ID="chkAcumulaSalida" Text ="Acumula Salida"/></td>
                                                            </tr>                                                                                            
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text="Descripcion"></asp:Label>  
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox3" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td>
                                                                    <asp:Label ID="Label24" runat="server" Text="Secuencia"></asp:Label>  
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtSecuencia" runat="server" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label29" runat="server" Text="Nombre Parametro Destino"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:TextBox ID="txtidParametroAccionSalida" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="True"></asp:TextBox>
                                                                </td>   
                                                            </tr>
                                                              <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label25" runat="server" Text="Parametro Destino"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidParametroAccion" Class="TexboxNormal" runat="server" Width="700px" AutoPostBack="True"></asp:DropDownList>
                                                                </td>   
                                                            </tr>
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label28" runat="server" Text="Tipo Metodo"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidTipoMetodo" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                                </td>   
                                                            </tr>
                                                        </table>                                             
                                                    </ContentTemplate>
                                                     <Triggers>  
                                                    </Triggers>
                                                </asp:UpdatePanel>                         
                                                <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_AccionMetodos0">
                                                    <h1 class="TituloPanelTitulo">Listado de metodos</h1>
                            
                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGrid4"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="RadPageView5"  >
                                            <asp:Panel ID="Panel8" runat="server" Class="TabContainer"  > 
                                                <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_ParametroMetodo">
                                                    <h1 class="TituloPanelTitulo">Metodo- Parametros</h1>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                </asp:Panel>                                   
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" >
                                                    <ContentTemplate>
                                                        <asp:Button  ID="btnAgregar5" runat ="server" Text= "Agregar" Width ="150px" OnClick="btnAgregar5_Click" />
                                                        <asp:Button ID ="btnEditar5"  runat ="server" Text= "Editar" Width ="150px"  OnClick="btnEditar5_Click"/>      
                                                        <h1></h1>
                                                        <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table5">                      
                                                            <tr>
                                                                    <td>
                                                                    <asp:Label ID="Label17" runat="server" Text="Num. Parametro - Metodo"></asp:Label> 
                                                                </td>
                                                                <td >     
                                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidParametroAccion" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                                    <asp:Button runat="server" ID="Button8" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label26" runat="server" Text="Accion"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddvidAccion0" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"  OnSelectedIndexChanged="ddlidAccion0_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>   
                                                            </tr>
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label18" runat="server" Text="A. Metodo"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidMetodoAccion" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlidMetodoAccion_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>   
                                                                </tr>
                                                             <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label22" runat="server" Text="Nombre"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNombre" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlNombre_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>   
                                                                </tr>
                                                             <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label21" runat="server" Text="Tipo Parametro"></asp:Label>       
                                                                </t>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlidTipoParametro" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                                </td>   
                                                                </tr>
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label19" runat="server" Text="Numero"></asp:Label> 
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtNumero" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                                </td>
                                               
                                                            </tr>  
                                                            <tr>
                                                                <td> 
                                                                    <asp:Label ID="Label23" runat="server" Text="Valor"></asp:Label> 
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtValor" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                                </td>
                                               
                                                            </tr>                                                                                                  
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" Text="Descripcion"></asp:Label>  
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox4" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>   
                                                             <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:CheckBox CssClass="TexboxNormal" ID="chkMultipleValor" runat="server" Text="Multiple Valor"></asp:CheckBox>
                                                                </td>
                                                            </tr>                                                                             
                                                        </table>                                             
                                                    </ContentTemplate>
                                                        <Triggers>  
                                                    </Triggers>
                                                </asp:UpdatePanel>                         
                                                <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_ParametroMetodo0">
                                                    <h1 class="TituloPanelTitulo">Listado de parametros</h1>
                            
                                                </asp:Panel>
                                                <telerik:RadGrid ID="RadGrid5"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
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
