<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_CrearAlisto.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Salidas.wf_CrearAlisto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                <script type='text/javascript'>
                    function DisplayLoadingImage1()
                    {
                        document.getElementById("loading1").style.display = "block";
                    }

                    function Confirma(parameter)
                    {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        if (confirm("¿Desea " + parameter + " ?"))
                        {
                            confirm_value.value = "Si";
                        }
                        else
                        {
                            confirm_value.value = "No";
                        }
                        document.forms[0].appendChild(confirm_value);
                        confirm_value.clear;
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
                            <telerik:AjaxSetting AjaxControlID="RadGrid3">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid3"></telerik:AjaxUpdatedControl>
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
                        <telerik:RadWindow  ID="WinUsuarios"  runat="server" VisibleStatusbar="false"      VisibleOnPageLoad="true"     RestrictionZoneID="RestrictionZoneID"  AutoSize="true"     AutoPostBack="true" >
                            <ContentTemplate >
                               <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Maestro Alisto" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>

                   <%--Maestro Alisto--%>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                                <h1 class="TituloPanelTitulo">Datos del Maestro Alisto</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1"   AutoPostBack="true" defaultfocus="TxtSSCC" >
                                            <ContentTemplate>
                                               <%-- <asp:Button  ID="btnObtenerAlisto" runat ="server" Text= "Info Artículo" Width ="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnObtenerAlisto_Click" />
                                                <asp:Label ID="Label22" runat="server" Text="|||"></asp:Label>--%>
                                                <asp:Button  ID="btnAccion11" runat ="server" Text= "Alistar Artículo" Width ="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnAccion11_Click" />
                                                <asp:Label ID="Label23" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnTraeTarea" runat="server" Text="Actualiza Tarea" OnClientClick="DisplayLoadingImage1();" OnClick = "BtnTraeTarea_Click" />
                                                <asp:Label ID="Label26" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnSiguientetarea" runat="server" Text="Siguiente Tarea" OnClientClick="Confirma('Traer la siguiente tarea');" OnClick="BtnSiguientetarea_Click" />
                                                <asp:Label ID="Label28" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnCierraSSCC" runat="server" Text="Cerrar SSCC" OnClientClick="Confirma('Cerrar SSCC');" OnClick="BtnCierraSSCC_Click"/>
                                                <asp:Label ID="Label22" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnCierraAlisto" runat="server" Text="Cerrar Alisto" OnClientClick="Confirma('Cerrar el Alisto');" OnClick="BtnCierraAlisto_Click"/>
                                                <asp:Label ID="Label30" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Btnlimpiar" runat="server" Text="Limpiar form" OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar_Click" />
                                                <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                  <center>
                                                    <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="Espere..." >
                                                    <%--<asp:Image ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;" runat ="server" visible ="false" AutoPostBack ="true"/>--%>
                                                  </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label29" runat="server" Text="Bodega:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:DropDownList ID="ddlidbodega" runat="server" OnSelectedIndexChanged="ddlidbodega_SelectedIndexChanged" AutoPostBack="true" Class="TexboxNormal"></asp:DropDownList>
                                                    </td>
                                                  </tr> 
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label20" runat="server" Text="Num.Pedido:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:DropDownList ID="ddlidMaestroSolicitud" runat="server" AutoPostBack="true" Class="TexboxNormal" OnSelectedIndexChanged="ddlidMaestroSolicitud_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label24" runat="server" Text="SSCC Leído"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox ID="TxtSSCC" runat="server" Width="500px" OnTextChanged="TxtSSCC_TextChanged" AutoPostBack ="true"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label27" runat="server" Text="Ubicación Leída"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox ID="TxtUbicacion" runat="server" Width="500px" AutoPostBack="true" OnTextChanged="TxtUbicacion_TextChanged"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="Label1" runat="server" Text="COD BARRAS"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox runat="server" ID="txtCODBARRAS" Width="500px" AutoPostBack="true" OnTextChanged="txtCODBARRAS_TextChanged"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                <%-- <tr>
                                                  <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Info Codigo Leído (GS1_128))"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox CssClass="TexboxNormal" ID="txtInfoCod" runat="server" Width="500px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                  </td>
                                                </tr>--%>
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Id Articulo"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox runat="server" ID="txtidArticulo" Width="50px" AutoPostBack="true" Enabled ="false" ></asp:TextBox>
                                                    <asp:Label ID="Label3" runat="server" Text="    ´ ||   ERP:"></asp:Label>
                                                    <asp:TextBox runat="server" ID="TxtidarticuloERP" Width="50px" AutoPostBack="true" Enabled="false" ></asp:TextBox>
                                                  </td>
                                                  <td>
                                                   
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Nombre"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox runat="server" ID="TextBox2" Width="350px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                  </td>
                                                </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtFechaVencimiento" Width="85px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Lote"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLote" Width="85px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" Text="Ubicación:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionSugerida" Width="200px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label21" runat="server" Text="Cantidad"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox runat="server" ID="txtCantidad" Width="50px" Class="TexboxNormal" Enabled="false"></asp:TextBox>
                                                        <asp:Label ID="Label25" runat="server" Text=" ´ || Pendiente Alistar:"></asp:Label>
                                                        <asp:TextBox ID="TxtPendientealistar" runat="server" Text="0" Enabled="false" Width="50px"></asp:TextBox>
                                                        <asp:Label ID="LblMensaje" runat="server" Text="´<<< Contenido del GTIN superior a lo pendiente por alistar" Font-Bold="true" ForeColor="Red" Font-Size ="Larger" Visible="false"></asp:Label>
                                                      </td>
                                                    </tr>
                                                     <tr>
                                                   <td>
                                                     <asp:Label ID="Label17" runat="server" Text="Tarea" Visible ="true"></asp:Label> 
                                                   </td>
                                                   <td >     
                                                     <asp:TextBox ID="TxmTarea" runat="server" Width="500px" TextMode="MultiLine" Height="100px"  CssClass="TexboxNormal" Enabled="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtEtiquetatarea" runat="server" Text="" Visible ="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtIdarticulotarea" runat="server" Visible ="false"></asp:TextBox>
                                                     <asp:TextBox ID="TxtIdregistroTRA" runat="server" Visible = "false"></asp:TextBox>
                                                     <asp:Button ID="BtnValidaSSCC" runat="server" Visible="false"/>
                                                     <asp:TextBox ID="TxtPasaSiguienteTarea" runat="server" Visible ="false"></asp:TextBox>
                                                   </td>
                                                 </tr>
                                              </table>
                                            </ContentTemplate>
                                                 <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroSolicitud0">
                                                <h1 class="TituloPanelTitulo">Listado Maestro Alisto</h1> 
                                            </asp:Panel>
                                            <telerik:RadGrid ID="RadGrid1"  runat="server"  AllowMultiRowSelection="false" PageSize ="10" AllowFilteringByColumn="True" AllowPaging ="True"  
                                                              onitemcommand="RadGrid_ItemCommand" 
                                                              OnPreRender="RadGrid1_Prerender" 
                                                              OnNeedDataSource="RadGrid_NeedDataSource"
                                                              AllowSorting="True" Culture="es-ES" ItemStyle-Wrap="False">
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

