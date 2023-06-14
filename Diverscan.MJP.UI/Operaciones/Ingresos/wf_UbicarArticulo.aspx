<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_UbicarArticulo.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Ingresos.wf_UbicarArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
                    <script type='text/javascript'>
                        function DisplayLoadingImage1()
                        {
                            document.getElementById("loading1").style.display = "block";
                        }
                        function DisplayLoadingImage2() {
                            document.getElementById("loading2").style.display = "block";
                        }
                        function DisplayLoadingImage3() {
                            document.getElementById("loading3").style.display = "block";
                        }
                        function DisplayLoadingImage4() {
                            document.getElementById("loading4").style.display = "block";
                        }

                        function Confirm() {
                            var confirm_value = document.createElement("INPUT");
                            confirm_value.type = "hidden";
                            confirm_value.name = "confirm_value";
                            if (confirm("Desea finalizar la Orden de compra?")) {
                                confirm_value.value = "Si";
                            } else {
                                confirm_value.value = "No";
                            }
                            document.forms[0].appendChild(confirm_value);
                        }
                </script>
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

            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="True">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>

                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">      <%--Title =" Proceso de Recepción "--%>
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Recibir Articulo" Width="200px" Visible="true"></telerik:RadTab>
                                    <telerik:RadTab Text="Ubicar Articulo" Width="200px" Visible="true"></telerik:RadTab>
                                    <telerik:RadTab Text="Consultar Articulo" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Control de Calidad" Width="200px" Visible="false"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                   <%-- Control de Calidad --%>
                                <%-- <telerik:RadPageView runat="server" ID="RadPageView4">
                                    <asp:Panel ID="Panel5" runat="server" Class="TabContainer"  >
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel6">
                                            <h1 class="TituloPanelTitulo">Formulario de control de calidad</h1>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                            <ContentTemplate>
                                              <%--  <asp:Button ID="Button1" runat="server" Text="btnAccion31" Width="150px" />
                                                <asp:Button ID="Button2" runat="server" Text="Recibir Articulo" Width="150px" OnClick="btnAccion32_Click" />
                                                                                <%-- los botones tienen id 4, aunque estan en la pestaña 2 para no crear errores a nivel de flujo
                                                <asp:Button ID="btnAccion41" runat="server" Text="Guardar Respuestas" Width="150px" OnClientClick="DisplayLoadingImage2()" onclick="btnAccion41_Click" />
                                                <asp:Label ID="Label36" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="btnAccion42" runat="server" Text="Recibir todo a Satisfaccion" Width="175px" OnClientClick="DisplayLoadingImage2();" OnClick="btnAccion42_Click" /> 
                                                <asp:Label ID="Label37" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Button100" runat="server" Text="Limpiar" OnClientClick="DisplayLoadingImage2()" OnClick ="BtnLimpiar3_Click" />

                                                <h1></h1>

                                              
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading2" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>
                                                </center>
                                                 </div>


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
                                                      <asp:DropDownList ID="ddlidArticulo0" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlidArticulo0_SelectedIndexChanged" ></asp:DropDownList>
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
                                                      <asp:Label ID="Label22" runat="server" Text="Respuesta"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:DropDownList ID="DdlidRespuesta" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="DdlidRespuesta_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                  </tr>  

                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="LbCodigoBarras" runat="server" Text="COD Barras" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:TextBox ID="txtCodigosBarras1" runat="server" Class="TexboxNormal" Width="300px" Visible="false"></asp:TextBox>
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
                                                      <asp:Label ID="LblLogin" runat="server" Text="Login Administrador:" Visible="false"></asp:Label>
                                                      <asp:TextBox CssClass="TexboxNormal" ID="TxtLogin" runat="server" Visible="false" ></asp:TextBox>
                                                      <asp:Label ID="LblPaswword" runat="server" Text="Password Administrador:" Visible="false"></asp:Label>
                                                      <asp:TextBox CssClass="TexboxNormal" ID="txtPass" runat="server" Visible="false" TextMode="Password" ></asp:TextBox>
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
                                                      <asp:CheckBox ID="CkMinimavidautil" runat="server" Text ="Fecha vencimiento manual" OnCheckedChanged="CkMinimavidautil_CheckedChanged" AutoPostBack="true"/>
                                                    </td>
                                                      <td>
                                                        <asp:TextBox ID="TxtCodGS1" runat="server" Width="300px"></asp:TextBox>  Text="Código de barras"
                                                        <%--<asp:Button ID="BtnDescomponefechavencimiento" runat="server" Text="..." OnClick="BtnDescomponefechavencimiento_Click" Visible="false" />
                                                        <telerik:RadDatePicker ID="RDPFechaVencimiento" runat="server" AutoPostBack ="true" Enabled ="false" Visible="false"></telerik:RadDatePicker>--%>
                                                    <%--  </td>
                                                  </tr> 
                                        <%--          <tr>
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
         
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>--%>

                        <%-- Recibir Articulo --%>
                                <telerik:RadPageView runat="server" ID="RadPageView3">
                                    <asp:Panel ID="Panel2" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_Acciones">
                                            <h1 class="TituloPanelTitulo">Accion Recibir Producto</h1>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAccion31" runat="server" Text="Ver Info Artículo" Width="210px" OnClientClick="DisplayLoadingImage3()" OnClick="Accion" Visible = "false"/>     <%--OnClick="btnAccion31_Click"--%>
                                                <asp:Label ID="Label25" runat="server" Text="|||" Visible = "false"></asp:Label>
                                                <asp:Button ID="btnAccion32" runat="server" Text="Recibir Articulo" Width="150px" OnClientClick="DisplayLoadingImage3()" OnClick="btnAccion32_Click" Enabled="true" />
                                                <asp:Label ID="Label26" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="btnFinalizarOC" runat="server" Text="Finalizar OC" Width="110px" OnClientClick = "Confirm()" OnClick="btnFinalizarOC_Click" Enabled="false" />
                                                <asp:Label ID="Label31" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnLimpiar0" runat="server" Text="Limpiar Form" OnClientClick="DisplayLoadingImage3()" OnClick ="BtnLimpiar0_Click" />
                                                <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                  <center>
                                                    <%--<img id="loading3" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >--%>
                                                    <%--<asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                  </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table3">
                                                    
                                                  <tr>
                                                    <td>
                                                      <asp:Label ID="LblOC" runat="server" Text="Orden de compra pendiente:"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <asp:DropDownList ID="DdlidMOC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlidMOC_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>

                                                  </tr>
                                                  <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCODBARRAS0" Width="500px" OnTextChanged ="txtCODBARRAS0_TextChanged" AutoPostBack ="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Info Codigo Leido (GS1_128))" Visible ="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtInfoCod0" runat="server" Width="500px" TextMode="MultiLine" Height="100px" ReadOnly ="true" Visible ="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Id Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox runat="server" ID="txtidArticulo0" Width="50px" AutoPostBack="true" ReadOnly ="true"></asp:TextBox>
                                                          <asp:Label ID="Label39" runat="server" Text="    ´ ||   ERP:"></asp:Label>
                                                          <asp:TextBox runat="server" ID="TxtidarticuloERP" Width="50px" AutoPostBack="true" ReadOnly ="true" ></asp:TextBox>
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
                                                            <asp:Label ID="Label17" runat="server" Text="Ubicacion Sugerida" Visible ="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtUbicacionSugerida0" Width="200px" AutoPostBack="true" ReadOnly ="true" Visible ="false"  ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCantidad0" Width="120px" Class="TexboxNormal" ReadOnly="false"></asp:TextBox>    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" Text="Total:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtTotal" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label33" runat="server" Text="Pendiente:"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="TxtPendiente" runat="server"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label41" runat="server" Text="Rechazado:"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="TxtTotalRechazado" runat="server"></asp:TextBox>
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

                                                <asp:Button ID="btnAccion21" runat="server" Text="Ver Info Artículo" Width="150px" Visible="false"  OnClientClick="DisplayLoadingImage4()" OnClick="btnAccion21_Click"/>
                                                <asp:Label ID="Label28" runat="server" Text="|||" Visible = "false"></asp:Label>
                                                <asp:Button ID="bntAccion23" runat="server" Text="Leer HH" Width="150px" Visible="false"  OnClientClick="DisplayLoadingImage4()" OnClick="btnAccion23_Click" />
                                                <asp:Button ID="bntAccion24" runat="server" Text="Aprobar Ubicación" Width="150px" Visible="true"  OnClientClick="DisplayLoadingImage4()" OnClick="btnAccion24_Click" />
                                                <asp:Label ID="Label27" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="BtnLimpiar1" runat="server" Text="Limpiar Form"  OnClientClick="DisplayLoadingImage4()" OnClick="BtnLimpiar1_Click" />
                                                
                                                <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading4" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" >
                                               <%--          <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                </center>
                                                 </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="COD BARRAS"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCODBARRAS" Width="500px" OnTextChanged ="txtCODBARRAS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Info Codigo Leido (GS1_128))" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtInfoCod" runat="server" Width="500px" TextMode="MultiLine" Height="100px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Num Articulo"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox runat="server" ID="txtidArticulo" Width="50px" AutoPostBack="true"></asp:TextBox>
                                                          <asp:Label ID="Label40" runat="server" Text="    ´ ||   ERP:"></asp:Label>
                                                          <asp:TextBox runat="server" ID="TxtidarticuloERP0" Width="50px" AutoPostBack="true" ReadOnly ="true" ></asp:TextBox>
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
                                                            <asp:Label ID="Label11" runat="server" Text="Fecha Vencimiento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtFechaVencimiento" Width="85px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Lote"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLote" Width="85px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Text="Ubicacion Sugerida"></asp:Label>
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
                                                            <asp:Label ID="Label23" runat="server" Text="Cantidad"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCantidad" Width="50px" Class="TexboxNormal"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label34" runat="server" Text="Codigo de Ubicación" Visible="false"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox runat="server" ID="txNuevaUbiSugerida" Width="50px" Visible ="false" Class="TexboxNormal"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label35" runat="server" Text="Total:"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="txtTotalArticulos" runat="server"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label38" runat="server" Text="Pendiente"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="txtTotalArticulosPorUbicar" runat="server"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </telerik:RadPageView>

                        <%-- Consultar Articulo --%>
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                  <asp:Panel ID="Panel1" runat="server" Class="TabContainer" >
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroArticulos" >
                                            <h1 class="TituloPanelTitulo">Consulta Articulo</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                            <ContentTemplate>
                                                <asp:Button ID="btnAccion1" runat="server" Text="btnAccion1" Width="150px" Visible="false"  OnClientClick="DisplayLoadingImage1()" />
                                                <asp:Button ID="btnAccion2" runat="server" Text="Disponibilidad" Width="150px" Visible="false"  OnClientClick="DisplayLoadingImage1()" OnClick="Accion"/>
                                                <asp:Label ID="Label29" runat="server" Text="|||" visible="false"></asp:Label>
                                                <asp:Button ID="btnArticulosSegunUbicacion" runat="server" Text="Consultar" Width="150px" Visible="true"  OnClientClick="DisplayLoadingImage1()" OnClick ="btnArticulosSegunUbicacion_Click" />
                                                <asp:Label ID="Label24" runat="server" Text="|||"></asp:Label>
                                                <asp:Button ID="Btnlimpiar" runat="server" Text="Limpiar"  OnClientClick="DisplayLoadingImage1()" OnClick ="Btnlimpiar_Click" />
                                               
                                                <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                  <center>
                                                   <%-- <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >
                                                     <asp:Image runat="server" ID="Image1" src="../../Images/loading.gif" style="width:80px;height:80px;/>--%>
                                                  </center>
                                                </div>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                     <tr>
                                                        <td>
                                                          <asp:Label ID="Label2" runat="server" Text="Articulo" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList ID="ddlidArticulo" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" Visible="false"></asp:DropDownList>
                                                        </td>
                                                     </tr>
                                                    <tr>
                                                      <td>
                                                        <asp:Label ID="Label30" runat="server" Text="Código de barras:"></asp:Label>
                                                      </td>
                                                      <td>
                                                        <asp:TextBox ID="txtEtiquetaUbicación" runat="server" Class="TexboxNormal" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                      </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Info Elemento"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="txtInfoArticulo" runat="server" Width="500px" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
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
