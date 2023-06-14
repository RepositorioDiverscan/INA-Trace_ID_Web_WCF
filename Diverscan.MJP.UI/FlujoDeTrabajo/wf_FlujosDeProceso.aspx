<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_FlujosDeProceso.aspx.cs" Inherits="Diverscan.MJP.UI.FlujoDeTrabajo.wf_FlujosDeProceso"  EnableViewState="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


                                          

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    
    <script type = "text/javascript">

        function serializeToJSON2() {
            var diagram1 = $find("<%=RadDiagram1.ClientID%>").get_kendoWidget();
            var json = diagram1.save();//the diagram shapes and connections are saved in the json variable
            var jsonStr = Sys.Serialization.JavaScriptSerializer.serialize(json);
            document.getElementById("<%=JsonText.ClientID%>").value = jsonStr;
        }
        
        function loadFromJSON(value) {
            var json = document.getElementById("<%=JsonText.ClientID%>").value;
            loadDiagram(json);
        }

        function loadDiagram(json) { //load the JSON in the diagram
            var diagram = $find("<%=RadDiagram1.ClientID%>").get_kendoWidget();
            diagram.load(Sys.Serialization.JavaScriptSerializer.deserialize(json));
        }

        function layoutBtn2_click() {
            var diagram = $find("<%=RadDiagram1.ClientID%>").get_kendoWidget();
            diagram.layout({
                type: "tree",
                subtype: "left",
                roots: [diagram.getShapeById("nadal_winner")],
                verticalSeparation: 5,
                horizontalSeparation: 10,
                animate: true
            });
        }


     </script>

     <asp:Panel ID="Panel4" runat="server" >   
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
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
                             <telerik:AjaxSetting AjaxControlID="RadGrid3">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid3"></telerik:AjaxUpdatedControl>
                                </UpdatedControls>
                            </telerik:AjaxSetting>  
                            
                             <telerik:AjaxSetting AjaxControlID="RadDiagram1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadDiagram1"></telerik:AjaxUpdatedControl>
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
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true"  AutoSize="true"  >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Flujos Trabajo" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Flujo Actividades" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Flujo Acciones" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                         <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_Procesos">
                                                <h1 class="TituloPanelTitulo">Procesos</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>     
                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px" OnClick="btnAgregar_Click"  AutoPostBack="false" />
                                                    <asp:Button  ID="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClick="btnEditar_Click"   AutoPostBack="false"/>      
                                        <h1></h1>
                                                                         
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <input id="Button2" type="button" value="Ordenar"   onclick = "layoutBtn2_click()"/>
                                                    <input id="Button7" type="button" value="Guardar"   onclick = "serializeToJSON2()"/>
                                                    <input id="Button3" type="button" value="Recuperar" onclick = "loadFromJSON()"/>
                                                    <asp:TextBox ID="JsonText" runat="server"></asp:TextBox>
                                                    <h1></h1>
                                                      <div id="toolbox">
                                                       <telerik:RadListView ID="ShapesList" runat="server">
                                                        <ItemTemplate>
                                                            <div class="item" data-data='{"id":"<%# DataBinder.Eval(Container.DataItem, "ID") %>","content":{"text": "<%# DataBinder.Eval(Container.DataItem, "Text") %>", "color": "#fff"}, "fill": {"color": "<%# DataBinder.Eval(Container.DataItem, "Background") %>"}}'>
                                                                <svg width="170" height="30">
                                                                    <g transform="scale(1,1)">
                                                                        <path stroke="gray" stroke-dasharray="" stroke-width="0" fill="<%# DataBinder.Eval(Container.DataItem, "Background") %>" d="<%# DataBinder.Eval(Container.DataItem, "d") %>"/>
                                                                        <text stroke="none" x="80" y="15" fill="#fff" text-anchor="middle" font-variant="normal" font-size="15" font-weight="normal" dominant-baseline="central"><%# DataBinder.Eval(Container.DataItem, "Text") %></text>
                                                                    </g>
                                                                </svg>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadListView>
                                                       </div>
                                                    
                                                    <telerik:RadDiagram ID="RadDiagram1" runat="server"  SwitchGridVisibility="True" 
                                                            EnableViewState="false" BorderWidth ="1" BorderColor="LightBlue" BackColor="#e9e9e9">
                                                            <LayoutSettings Enabled="true" Type="Tree" Subtype="Left" ></LayoutSettings>
                                                            <ClientEvents OnLoad="diagram_load" OnChange="diagram_change" />
                                                            <BindingSettings>
                                                                <ShapeSettings
                                                                    DataFillColorField="Background"
                                                                    DataContentTextField="Content"
                                                                    DataIdField="ShapeId"
                                                                    DataWidthField="Width"
                                                                    DataHeightField="Height" />
                                                                <ConnectionSettings
                                                                    DataFromShapeIdField="FromShapeId"
                                                                    DataToShapeIdField="ToShapeId" />
                                                            </BindingSettings>        
                                                    </telerik:RadDiagram>
                                                    
                                            
                                

                                        </ContentTemplate>
                                         <Triggers> 
                                        </Triggers>
                                    </asp:UpdatePanel>                         
                                           <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Procesos0">
                                        <h1 class="TituloPanelTitulo">Listado de procesos</h1>
                            
                                    </asp:Panel>
                                    <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand" OnPreRender="RadGrid1_Prerender"   
                                                AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False"  OnNeedDataSource="RadGrid_NeedDataSource" >
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
                                         <telerik:RadGrid ID="RadGrid2" runat="server" EnableLinqExpressions="false" AllowMultiRowSelection="false" PageSize ="15" OnPreRender="RadGrid2_Prerender" 
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
                                                 
                                                 </table>                               
                                              </ContentTemplate>
                                                 <Triggers>                                             
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_Acciones0">
                                                <h1 class="TituloPanelTitulo">Listado de Acciones</h1>
                            
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid3"  runat="server"  AllowMultiRowSelection="false" PageSize ="3"  onitemcommand="RadGrid_ItemCommand"  OnPreRender="RadGrid3_Prerender"  
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
