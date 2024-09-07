<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Contrato.aspx.cs" Inherits="Diverscan.MJP.UI.AppsDiverscan.Contratos.Mantenimiento.wf_Contrato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                    <telerik:RadTab Text="Contratos" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 2" Width="200px" Visible ="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Tab 3" Width="200px" Visible ="false"> </telerik:RadTab>
                               </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Contratos_Contrato">
                                                <h1 class="TituloPanelTitulo">Mantenimiento Empresa</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate> 
                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClick="btnAgregar_Click" />
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClick="btnEditar_Click"/>      
                                                    <h1></h1>
                                                  <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                             <tr>
                                                 <td>
                                                    <asp:Label ID="Label3" runat="server" Text="ID Contrato"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidContrato" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                     <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>

                                             <tr>
                                                 <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Empresa"></asp:Label> 
                                                </td>
                                                <td >     
                                                 
                                                      <asp:DropDownList ID="ddlIdEmpresa" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>    
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Telefono"></asp:Label> 
                                                </td>
                                                <td >     
                                                   <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtTelefono" Width="250px" ></asp:TextBox>               
                                                </td>
                                            </tr>


                                                <tr>
                                                 <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Email"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtEmail" Width="250px" ></asp:TextBox>
                                                    
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Producto"></asp:Label> 
                                                </td>
                                                <td >     
                                                      <asp:DropDownList ID="ddlidProducto" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>                           
                                                </td>
                                            </tr>                            
                                                      
                                              <tr>
                                     
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Numero Serie"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtNumSerie" Width="250px" ></asp:TextBox>
                                                </td>

                                             <td>
                                                    <asp:Label ID="Label23" runat="server" Text="Fecha Inicio"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtFechaInicio" Width="250px" ></asp:TextBox>
                                                </td>
                                            </tr>
  
                                               <tr>

                                                 <td>
                                                    <asp:Label ID="Label22" runat="server" Text="Fecha Expira"></asp:Label> 
                                                </td>
                                                <td >     
                                                    
                                                          <asp:Calendar ID = "Calendar1" runat = "server" SelectionMode="DayWeekMonth"></asp:Calendar>
                                                         <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtFechaExpira" Width="250px" ></asp:TextBox>
                                                                             
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Tiempo Faltante"></asp:Label> 
                                                </td>
                                                <td >     
                                                   
                                                   <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtTiempoFaltante" Width="250px" ></asp:TextBox>      
                                                </td>
                                            </tr>

                                             <tr>

                                                <td>
                                                    <asp:Label ID="Label24" runat="server" Text="Tiempo Respuesta"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtTiempoRespuesta" Width="250px" ></asp:TextBox>               
                                                </td>

                                                   <td>
                                                    <asp:Label ID="Label29" runat="server" Text="Segmentación Empresa"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtTiempoResolucion" Width="250px" ></asp:TextBox>               
                                                </td>
                                            </tr>


                                                   <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Contrato Firmado" ID="chkContratoFirmado"/>
                                                </td>

                                                  </tr>


                                                   <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Contrato Garantia" ID="chkContratoGarantia"/>
                                                </td>

                                                  </tr>


                                                 <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Sustitucion Equipo" ID="chkSustitucionEquipo"/>
                                                </td>

                                                  </tr>

                                                   <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Visita a Sitio" ID="chkVisitaSitio"/>
                                                </td>

                                                  </tr>
                                            
                                             <tr>

                                                <td>
                                                    <asp:Label ID="Label30" runat="server" Text="Hora Atención"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtHoraAtencion" Width="250px" ></asp:TextBox>               
                                                </td>

                                                   <td>
                                                    <asp:Label ID="Label31" runat="server" Text="Visitas por año"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtNumVisitasAno" Width="250px" ></asp:TextBox>               
                                                </td>
                                            </tr>



                                               <tr>

                                                <td>
                                                    <asp:Label ID="Label32" runat="server" Text="Horas Consumo"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtHorasConsumo" Width="250px" ></asp:TextBox>               
                                                </td>

                                                   <td>
                                                    <asp:Label ID="Label33" runat="server" Text="Localidades Contrato"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtLocalidadesContrato" Width="250px" ></asp:TextBox>               
                                                </td>
                                            </tr>



                                                <tr>

                                                <td>
                                                    <asp:Label ID="Label345" runat="server" Text="Monto Cancelar"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtMontoCancelar" Width="250px" ></asp:TextBox>               
                                                </td>

                                                   <td>
                                                    <asp:Label ID="Label35" runat="server" Text="Sevicios Terceros"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtServiciosTerceros" Width="250px" ></asp:TextBox>               
                                                </td>
                                            </tr>

                                                      
                                                <tr>

                                                <td>
                                                    <asp:Label ID="Label36" runat="server" Text="Dia Facturación"></asp:Label> 
                                                </td>
                                                <td >     
                                                       <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtDiaFacturacion" Width="250px" ></asp:TextBox>               
                                                </td>

                                            </tr>


                                                 <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Cabezales" ID="chkCabezales"/>
                                                </td>

                                               </tr>


                                                         <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Entregas Periodicas" ID="chkEntregasPeriodicas"/>
                                                </td>

                                                  </tr>


                                                  <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Cotización" ID="chkCotizacion"/>
                                                </td>

                                                  </tr>

                                                   <tr>
                                                <td>
                                                   <asp:CheckBox runat="server" Text="Servicio Critico" ID="chkServicioCritico"/>
                                                </td>

                                                  </tr>
                                                
                                                   <tr>

                                                 <td>
                                                    <asp:Label ID="Label25" runat="server" Text="Vendedor Empresa"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:DropDownList ID="ddlIdVendedorEmpresa" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>                
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text="Contacto"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:DropDownList ID="ddlIdContacto" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>                
                                                </td>

                                           
                                            </tr>
  


                                              </table>
                
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>

                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Contratos_Contrato0">
                                                <h1 class="TituloPanelTitulo">Lista Vendedores Empresa</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand"   
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
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel7">
                                            <h1 class="TituloPanelTitulo">Titulo Tab 2</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    <asp:Button  ID="Button5" runat ="server" Text ="Button5" Width ="150px" />
                                                    <asp:Button ID ="Button6" runat ="server" Text ="Button6" Width ="150px" />      
                                            </asp:Panel>
                                          <h1></h1>                                     
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>                                   
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                            <tr>
                                                 <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Usuario"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="TextBox1" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button1" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label ID="Label9" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="TextBox2" Class="TexboxNormal" Width="150px" ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Apellidos"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="TextBox3" Width="250px" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>      
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox4" runat="server" Width="305px" ></asp:TextBox>  
                                                </td>
                                                  <td> 
                                                    <asp:Label ID="Label12" runat="server" Text="Rol"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                            </tr>  
                                             <tr>      
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="Pass"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox5" runat="server" Width="305px" visible ="false"></asp:TextBox>  
                                                </td>      
                                            </tr>                       
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Comentarios"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox6" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Bloqueado" />
                                                </td>
                                                                           
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox2" Text="Administración de programas" />
                                                </td>
                                            </tr>
                                            <tr>  
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox3" Text="Usuario programa provisional" Visible="True" />
                                                </td>
                                                <td></td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox4" Text="Recibir correo de pedidos"  Visible="True"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox5" Text="Aprobador"  Visible="True"/>
                                                </td>
                                            </tr>                                              
                                              </table>                               
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Roles">
                                                    <h1 class="TituloPanelTitulo">Listado roles</h1>
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
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel5">
                                                <h1 class="TituloPanelTitulo">Titulo Tab 3</h1>
                                                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                    <asp:Button ID ="Button2" runat ="server" Text ="Boton 2" Width ="150px" />
                                                    <asp:Button ID ="Button3" runat ="server" Text ="Boton 3" Width ="150px" />      
                                                    </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                                <ContentTemplate>
                                                    

                                                  <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                            <tr>
                                                 <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Usuario"></asp:Label> 
                                                </td>
                                                <td >     
                                                    <asp:TextBox CssClass="TextBoxBusqueda" ID="TextBox7" runat="server" Width="85px" AutoCompleteType="Disabled" ></asp:TextBox>   
                                                    <asp:Button runat="server" ID="Button4" Text="Buscar" OnClick="btnBuscar_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label ID="Label16" runat="server" Text="Nombre"></asp:Label> 
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="TextBox8" Class="TexboxNormal" Width="150px" ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Text="Apellidos"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="TexboxNormal" ID="TextBox9" Width="250px" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>      
                                                <td>
                                                    <asp:Label ID="Label18" runat="server" Text="Email"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox10" runat="server" Width="305px" ></asp:TextBox>  
                                                </td>
                                                  <td> 
                                                    <asp:Label ID="Label19" runat="server" Text="Rol"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                                </td>   
                                            </tr>  
                                             <tr>      
                                                <td>
                                                    <asp:Label ID="Label20" runat="server" Text="Pass"></asp:Label>       
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox11" runat="server" Width="305px" visible ="false"></asp:TextBox>  
                                                </td>      
                                            </tr>                       
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label21" runat="server" Text="Comentarios"></asp:Label>  
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="TextBox12" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="Bloqueado" />
                                                </td>
                                                                           
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox7" Text="Administración de programas" O />
                                                </td>
                                            </tr>
                                            <tr>  
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="CheckBox8" Text="Usuario programa provisional" Visible="True" />
                                                </td>
                                                <td></td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox9" Text="Recibir correo de pedidos"  Visible="True"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td >
                                                    <asp:CheckBox runat="server" ID="CheckBox10" Text="Aprobador"  Visible="True"/>
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
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand"   
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
