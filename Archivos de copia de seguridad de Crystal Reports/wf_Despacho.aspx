<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Despacho.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_Despacho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                <script type='text/javascript'>
                    function DisplayLoadingImage1()
                            {
                                document.getElementById("loading1").style.display = "block";
                            }
                    function DisplayLoadingImage2()
                            {
                                document.getElementById("loading2").style.display = "block";
                            }
                    function DisplayLoadingImage3()
                            {
                                document.getElementById("loading3").style.display = "block";
                            }
                    function DisplayLoadingImage4()
                            {
                                document.getElementById("loading4").style.display = "block";
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
                                    <telerik:RadTab Text="Asociar SSCC" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Asociar Vehiculo" Width="200px" > </telerik:RadTab>
                                    <telerik:RadTab Text="Devolver Articulo SSCC" Width="200px" > </telerik:RadTab>
                                    <telerik:RadTab Text="Aprobar Despacho" Width="200px" ></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                   <%-- Asociar SSCC --%>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="a0">
                                                <h1 class="TituloPanelTitulo">Asociar SSCC</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <h1></h1>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                   <asp:Button  ID="btnAccion21" runat ="server" Text ="Asociar SSCC" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick ="btnAccion21_Click"/>
                                                   <asp:Label ID="Label11" runat="server" Text="|||"></asp:Label>
                                                   <asp:Button ID="BtnLimpiarform" runat="server" Text = "Limpiar form"  OnClientClick="DisplayLoadingImage1()" OnClick = "BtnLimpiarform_Click" />
                                                   <asp:Button ID="BtnAsociaSSCCubicacion" runat="server" Text="---" Visible ="false"  OnClientClick="DisplayLoadingImage1()" />
                                                   <%--<asp:Image ID="Imgespera" ImageUrl="~/Images/espera02.gif" Visible ="false" />--%>
                                                 <h1></h1>

                                                    
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">                      
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Ubicacion Leida"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionLeida" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <t>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="SSCC Leido"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtSSCCLeido" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                 </table>          
                                                </ContentTemplate>
                                                 <Triggers>
                                                    
                                                </Triggers>

                                            </asp:UpdatePanel>                         
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="a">
                                                <h1 class="TituloPanelTitulo">Lista Ubicaciones SSCC</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand" OnPreRende="RadGrid1_Prerender"  
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

                      <%-- Asociar Vehículo --%>
                                   <telerik:RadPageView runat="server" ID="RadPageView3">                                  
                                        <asp:Panel ID="Panel3" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Panel5">
                                          <h1 class="TituloPanelTitulo">Asociar Vehiculo</h1>
                                       <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                       <%--    <asp:Button ID ="Button2" runat ="server" Text ="Boton 2" Width ="150px" />
                                                <asp:Button ID ="Button3" runat ="server" Text ="Boton 3" Width ="150px" />   --%>   
                                          </asp:Panel>
                                            <h1></h1>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                             <ContentTemplate>   
                                               <asp:Button  ID="buton2" runat ="server" Text ="Ver InfoSSCC" Width ="150px" OnClientClick="DisplayLoadingImage3()" OnClick ="VerInfoSSCC"/>
                                               <asp:Label ID="Label14" runat="server" Text="|||"></asp:Label>
                                               <asp:Button  ID="btnAsociarVehiculo" runat ="server" Text ="Asociar SSCC Vehiculo" OnClientClick="DisplayLoadingImage3()" OnClick="AsociarSSCCVehiculo" Width ="150px" />
                                               <asp:Label ID="Label15" runat="server" Text="|||"></asp:Label>
                                               <asp:Button ID="Btnlimpiar" runat="server" Text="Limpiar form" OnClientClick="DisplayLoadingImage3()" OnClick="Btnlimpiar_Click" />
                                               <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="Img1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                               <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">                      
                                                 <tr>
                                                  <td>
                                                    <asp:Label ID="Label8" runat="server" Text="SSCC Leido"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox runat="server" ID="txtSSCCVehiculo" Width="500px" OnTextChanged="txtSSCCVehiculo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Ubicacion Leida"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtUbicacionParqueo" runat="server" Width="305px" ></asp:TextBox> 
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Vehículo"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:DropDownList ID="ddlIdvehiculo" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                  </td>
                                                 <tr>
                                                  <td>
                                                    <asp:Label ID="Label16" runat="server" Text="Ruta"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:DropDownList ID="ddlIdruta" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                  </td>
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Información de SSCC"></asp:Label>  
                                                  </td>
                                                  <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txmInfoSSCC" runat="server" Width="300px" TextMode="MultiLine" Height="300px"></asp:TextBox>
                                                  </td>
                                                </tr>  
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Destino"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtDestino" runat="server" Width="305px" ></asp:TextBox> 
                                                  </td>
                                                </tr>     
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label5" runat="server" Text="PesoKilos"></asp:Label>
                                                  </td>
                                                         <td>
                                                               <asp:TextBox CssClass="TexboxNormal" ID="txtPesoKilos" runat="server" Width="305px" ></asp:TextBox> 
                                                        </td>
                                                      </tr>
                                                      <tr>
                                                        <td>
                                                          <asp:Label ID="Label10" runat="server" Text="DimsensionSSCCM3"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox CssClass="TexboxNormal" ID="txtDimsensionSSCCM3" runat="server" Width="305px" ></asp:TextBox> 
                                                        </td>
                                                      </tr> 

                                                     <%-- <tr>
                                                         <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Equivalencia"></asp:Label>
                                                        </td>
                                                         <td>
                                                               <asp:TextBox CssClass="TexboxNormal" ID="txtEquivalencia" runat="server" Width="305px" ></asp:TextBox> 
                                                        </td>
                                                       </tr> --%>
                                                     
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
                                    </telerik:RadPageView>

                    <%-- Devolución artículo SSCC --%>
                             <telerik:RadPageView runat="server" ID="RadPageView4">
                                     <asp:Panel ID="Panel6" runat="server" Class="TabContainer"  >                                        
                                       <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista">
                                         <h1 class="TituloPanelTitulo">Devolver artículos del SSCC</h1>
                                         <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                       </asp:Panel>
                                       <asp:UpdatePanel runat="server" ID="UpdatePanel4" >
                                         <ContentTemplate>  
                                           <asp:Button  ID="btnDevolver" runat ="server" Text= "Devolver Artículo" Width ="150px" OnClientClick="DisplayLoadingImage4()" OnClick="btnDevolver_Click" />
                                           <h1></h1>    <%--esto se usa para que el boton no se pegue al borde de la tabla --%>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading4" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                           <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4">     
                                             <tr>
                                               <td>
                                                 <asp:Label ID="Label9" runat="server" Text="Cod Barras SSCC"></asp:Label>
                                               </td>
                                               <td>
                                                 <asp:TextBox runat="server" ID="txtCODBARRAS" Width="500px"></asp:TextBox>
                                               </td>
                                             </tr>
                                             <tr>
                                               <td>
                                                 <asp:Label ID="Label20" runat="server" Text="Cod Barras ARTÍCULO"></asp:Label>
                                               </td>
                                               <td>
                                                 <asp:TextBox runat="server" ID="txtCodBarArticulo" Width="500px"></asp:TextBox>
                                               </td>
                                             </tr>
                                             <tr> 
                                               <td>
                                                 <asp:Label ID="Label13" runat="server" Text="Ubicación a mover"></asp:Label>
                                               </td>
                                               <td>
                                                 <asp:TextBox ID="txtUbicacionmover" runat="server" Width="500px"></asp:TextBox>
                                               </td>
                                             </tr> 
                                           </table>
                                         </ContentTemplate>
                                         <Triggers>
                                         </Triggers>        
                                       </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_xxx" Visible ="false" >
                                           <h1 class="TituloPanelTitulo">Lista de SSCC</h1> 
                                         </asp:Panel>
                                         <telerik:RadGrid ID="RadGrid4"  runat="server"  AllowMultiRowSelection="false" PageSize ="10"  onitemcommand="RadGrid_ItemCommand"   
                                                          AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  
                                                          OnNeedDataSource ="RadGrid_NeedDataSource" Visible ="false">
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

                   <%-- Aprobar despacho --%>
                                    <telerik:RadPageView runat="server" ID="RadPageView2">
                                      <asp:Panel ID="Panel2" runat="server" Class="TabContainer">    
                                        <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_AlistosRealizados">
                                            <h1 class="TituloPanelTitulo">Aprobar Despacho</h1>
                                                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                                       
                                            </asp:Panel>
                                          <h1></h1>                                     
                                          <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                             <ContentTemplate>                                   
                                 
                                                     <asp:Button ID ="btnAccion31" runat ="server" Text ="Aprobar Despacho" Width ="150px"  OnClientClick="DisplayLoadingImage2()" OnClick="btnAccion31_Click"/>
                                                     <asp:Button ID ="Button1" runat ="server" Text ="Verificar SSCC" Width ="150px" OnClick="btnInfoSSCC" Visible ="false"/>
                                                   
                                            <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading2" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="SSCC Leido"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtSSCCLeido0" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </table>                 
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                         <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_AlistosRealizados0">
                                                    <h1 class="TituloPanelTitulo">Listado Alistos realizados</h1>
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

                                </telerik:RadMultiPage>
                            </ContentTemplate>
              <%-- Fin Pestaña devolver artículo SSCC --%>

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