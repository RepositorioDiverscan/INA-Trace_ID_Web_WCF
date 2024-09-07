<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_DemoEventos.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Eventos.wf_DemoEventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel4" runat="server">
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

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Lectura" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Control de Calidad" Width="200px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Recibir Articulo" Width="200px" Visible="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Ubicar Articulo" Width="200px" Visible="false"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">

                   <%-- LecturaDemoEvento --%>
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_">
                                            <h1 class="TituloPanelTitulo">Lectura Demo Evento</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick ="btnConsultar_Click" />
                                                <asp:Button ID="btnConsultarRegalia" runat="server" Text="Consultar Regalia" OnClick ="btnConsultarRegalia_Click" />
                                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick ="btnEnviar_Click" />
                                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick ="btnLimpiar_Click" />
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCodLeido" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="WMS" ID="chkWMS"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="Control Activos" ID="CheckBox1"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="Acces-o" ID="CheckBox2"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="Computo Móvil" ID="CheckBox3"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="Infraestructura" ID="CheckBox4"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" Text="RFID" ID="CheckBox5"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                   <%-- Control de Calidad --%>
                                 <telerik:RadPageView runat="server" ID="RadPageView4">
                                    <asp:Panel ID="Panel5" runat="server" Class="TabContainer"  >
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel6">
                                            <h1 class="TituloPanelTitulo">Formulario de control de calidad</h1>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                            <ContentTemplate>
                                              <%--  <asp:Button ID="Button1" runat="server" Text="btnAccion31" Width="150px" />
                                                <asp:Button ID="Button2" runat="server" Text="Recibir Articulo" Width="150px" OnClick="btnAccion32_Click" />--%>

                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" Text="Orden de Compra"></asp:Label>
                                                            <asp:TextBox ID="TxtEvalua" runat="server" Visible ="false" Text ="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                             <asp:DropDownList ID="DdlidMOC0" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlidMaestroOrdenCompra_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" Text="Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlidArticulo0" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" ></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" Text="Pregunta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlidPregunta" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                         
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="LblTemperatura" runat="server" Text="Temperatura:"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="TxtTemperatura" runat="server" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                      </td>
                                                    </tr>


                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Text="Respuesta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlidRespuesta" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="DdlidRespuesta_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>  
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblComment" runat="server" Text="Comentarios" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox CssClass="TexboxNormal" ID="txtComent" runat="server" Width="500px" TextMode="MultiLine" Height="200px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>  
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="lblAdmin" runat="server" Text="Administrador Autoriza" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox CssClass="TexboxNormal" ID="txtPass" runat="server" Visible="false" TextMode="Password" ></asp:TextBox>
                                                        </td>
                                                    </tr>                                                           
                                                </table>
                                                <h1></h1>
                                         <%-- los botones tienen id 4, aunque estan en la pestaña 2 para no crear errores a nivel de flujo--%>
                                                 <asp:Button ID="btnAccion41" runat="server" Text="Guardar Respuestas" Width="150px" onclick="btnAccion41_Click" />
                                                 <asp:Button ID="btnAccion42" runat="server" Text="Recibir todo a Satisfaccion" Width="175px" OnClick="btnAccion42_Click" /> 
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                        <%-- Recibir Articulo --%>
                                <telerik:RadPageView runat="server" ID="RadPageView3">
                                    <asp:Panel ID="Panel2" runat="server" Class="TabContainer" >
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Acciones">
                                            <h1 class="TituloPanelTitulo">Accion Recibir Producto</h1>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAccion31" runat="server" Text="btnAccion31" Width="150px" />
                                                <asp:Label ID="Label25" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="btnAccion32" runat="server" Text="Recibir Articulo" Width="150px" OnClick="btnAccion32_Click" />
                                                <asp:Label ID="Label26" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnLimpiar0" runat="server" Text="Limpiar Form" OnClick ="BtnLimpiar0_Click" />
                                                <h1></h1>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="LblOC" runat="server" Text="Orden de compra pendiente:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:DropDownList ID="DdlidMOC" runat="server"></asp:DropDownList>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCODBARRAS0" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Info Codigo Leido (GS1_128))"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtInfoCod0" runat="server" Width="500px" TextMode="MultiLine" Height="100px" ReadOnly ="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Num Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtidArticulo0" Width="50px" AutoPostBack="true" ReadOnly ="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtNombre0" Width="350px" AutoPostBack="true" ReadOnly ="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtFechaVencimiento0" Width="85px" AutoPostBack="true" ReadOnly ="true" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="Lote"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLote0" Width="85px" AutoPostBack="true" ReadOnly="true" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Ubicacion Sugerida"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionSugerida0" Width="200px" AutoPostBack="true" ReadOnly ="true" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCantidad0" Width="120px" Class="TexboxNormal"></asp:TextBox>    
                                                        </td>
                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                         <%-- Ubicar Articulo --%>
                                <telerik:RadPageView runat="server" ID="RadPageView2">
                                    <asp:Panel ID="Panel3" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Actividades">
                                            <h1 class="TituloPanelTitulo">Accion Ubicar Producto</h1>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>

                                                <asp:Button ID="btnAccion21" runat="server" Text="btnAccion21" Width="150px" Visible="false" />
                                                <asp:Label ID="Label28" runat="server" Text="|||"></asp:Label>
                                              <%--  <asp:Button ID="bntAccion22" runat="server" Text="bntAccion22" Width="150px" Visible="false" />
                                                <asp:Button ID="bntAccion23" runat="server" Text="Leer HH" Width="150px" Visible="true" OnClick="btnAccion23_Click" />--%>
                                                <asp:Button ID="bntAccion24" runat="server" Text="Aprobar Ubicación" Width="150px" Visible="true" OnClick="btnAccion24_Click" />
                                                <asp:Label ID="Label27" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnLimpiar1" runat="server" Text="Limpiar Form" OnClick="BtnLimpiar1_Click" />
                                                <h1></h1>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCODBARRAS" Width="500px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Info Codigo Leido (GS1_128))"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtInfoCod" runat="server" Width="500px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Num Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtidArticulo" Width="50px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtNombre" Width="350px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtFechaVencimiento" Width="85px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Lote"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLote" Width="85px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Ubicacion Sugerida"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionSugerida" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text="Ubicacion Leida"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionLeida" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCantidad" Width="50px" Class="TexboxNormal"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                       <tr>
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" Text="Codigo de Ubicación"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txNuevaUbiSugerida" Width="50px" Visible ="false" Class="TexboxNormal"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </table>


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
