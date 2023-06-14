<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="VisorInventariosBasico.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.VisorInventariosBasico" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" style="width:100%; height:100%;">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }        
    </script>
    <!--Panel del Resultado-->
    <asp:Panel ID="Panel4" runat="server" style="width:100%; height:100%;">
        <!--Div que contiene todo-->
        <div id="RestrictionZoneID" class="WindowContenedor" >
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1"  runat="server" EnableShadow="true" Style="width:100%; height:100%;">
                <Windows>
                    <telerik:RadWindow ID="WinUsuarios"  runat="server" VisibleStatusbar="false" style="width:100%; height:100%;" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true" >
                        <ContentTemplate>
                            <!--Encabezados de las ventanas-->
                            <telerik:RadTabStrip  AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"   MultiPageID="RadMultiPage1" SelectedIndex="0" >
                                <Tabs>
                                    <telerik:RadTab Text="Resultados Inventario" Width="25%" ></telerik:RadTab>                                    
                                </Tabs>
                            </telerik:RadTabStrip>

                            <!--Contenido de los Tabs-->
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >                                  
                                <telerik:RadPageView runat="server" ID="RadPageView1"  style="width:100%; height:100%;" >
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                           
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" style="width:100%; height:100%;" >
                                            <ContentTemplate>
                                                <!--Modal de Carga de la pantalla-->
                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                    <center>
                                                        <img id="loading1" src="../../Images/loading.gif" alt="Cargando..." style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--Panel de las Tomas Físicas-->
                                                <asp:Panel ID="PanelBaseTomaFisica" runat="server" GroupingText="Inventario Toma Fisica" style="margin-left: 15px;">
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 98%; border-collapse: initial;" id="Table2">
                                                        <!--Campo de selección de Bodega-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Text="Bodega" style ="margin-top:8px; margin-left:5px;"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddBodega" AutoPostBack="true" OnSelectedIndexChanged="_ddBodega_SelectedIndexChanged" onchange="DisplayLoadingImage1()" ></asp:DropDownList>
                                                            </td>                                                             
                                                        </tr>

                                                        <!--Campo de Fecha de Aplicación-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label style =" margin-top:2px; margin-left:5px;" runat="server" ID="_lblFechaAplicado" Text="Fecha Aplicado" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker runat="server" ID="_rdpFechaAplicado" AutoPostBack="true" OnSelectedDateChanged="_rdpFechaAplicado_SelectedDateChanged" onchange="DisplayLoadingImage1()">
                                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                                                </telerik:RadDatePicker>
                                                            </td>                                                             
                                                        </tr>
                                                            
                                                        <!--Campo de selección de Inventarios-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Inventarios" style ="margin-top:8px; margin-left:5px;"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="_ddlInventariosBasicos" AutoPostBack="true" OnSelectedIndexChanged="_ddlInventariosBasicos_SelectedIndexChanged" onchange="DisplayLoadingImage1()"></asp:DropDownList>
                                                            </td>                                                             
                                                        </tr>
                                                        
                                                        <!--Campo de selección de Artículos-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Artículos" style ="margin-top:8px; margin-left:5px;"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="_ddlArticulos" AutoPostBack="true" OnSelectedIndexChanged="_ddlArticulos_SelectedIndexChanged" onchange="DisplayLoadingImage1()" ></asp:DropDownList>
                                                            </td>                                                             
                                                        </tr>

                                                        <!--Campo de búsqueda de Artículos-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Buscar Artículo" style ="margin-top:8px; margin-left:5px;"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID ="_txtArticuloBusqueda" OnTextChanged="_txtArticuloBusqueda_TextChanged" AutoPostBack="true" OnClientClick="DisplayLoadingImage1()"></asp:TextBox>
                                                            </td>                                                             
                                                        </tr>

                                                        <!--Campo de Estado de los artículos-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEstado" runat="server" Text="Estado" style ="margin-top:8px; margin-left:5px;" Visible ="false"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="_ddlEstado" AutoPostBack="true" OnSelectedIndexChanged="_ddlEstado_SelectedIndexChanged" onchange="DisplayLoadingImage1()" Visible ="false"></asp:DropDownList>                                                            
                                                            </td>
                                                        </tr>

                                                        <!--Campo de Contado-->
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblContado" runat="server" Text="Contado" style ="margin-top:8px; margin-left:5px;" Visible ="false"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="_chkContado" AutoPostBack="true" OnCheckedChanged="ChkContado_OnCheckedChanged" onchange="DisplayLoadingImage1()" Visible ="false"></asp:CheckBox>
                                                            </td>
                                                        </tr>

                                                        <!--Campo de botón de búsqueda-->
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="_btnBuscar" Text="Buscar"  OnClick ="_btnBuscar_Click"  OnClientClick="DisplayLoadingImage1()"/>
                                                            </td>                                                             
                                                        </tr>
                                                        
                                                        <!--Campo de selección de botones-->
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" ID="_btnExportar" Text="Exportar"   OnClick ="_btnExportar_Click" /> 
                                                                <asp:Button runat="server" ID="_btnCerrar" Text="Cerrar"  style ="margin-top:8px; margin-left:5px;" OnClick ="_btnCerrar_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel> 

                                                <!--Panel de la Tabla de Resultados-->    
                                                <asp:Panel ID="PanelResultados" runat="server" GroupingText="Resultados" style="margin-left: 15px; margin-top: 15px;">
                                                    <!--Tabla de Resultados de los Inventarios-->
                                                    <telerik:RadGrid ID="RGBodegaFisica_SistemaRecord"   Width="98%" AllowRowResize="true" OnNeedDataSource ="RGBodegaFisica_SistemaRecord_NeedDataSource"
                                                        PageSize="100" runat="server" AutoGenerateColumns="False" AllowSorting="True"  AllowMultiRowSelection="true"  
                                                        AllowFilteringByColumn="false" PagerStyle-AlwaysVisible="true">

                                                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="false" EnablePostBackOnRowClick="true">
                                                                <Selecting AllowRowSelect="True"></Selecting>
                                                                <Scrolling AllowScroll="True" SaveScrollPosition="true" FrozenColumnsCount="2"  UseStaticHeaders="true"></Scrolling>
                                                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="false" ></Resizing>
                                                            </ClientSettings>
                                                            
                                                            <MasterTableView>
                                                                <Columns>                 
                                                                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                    </telerik:GridClientSelectColumn>
                                                                
                                                                    <telerik:GridBoundColumn UniqueName="IdUbicacion" Display="false"
                                                                    SortExpression="IdUbicacion" HeaderText="ID Ubicación" DataField="IdUbicacion">
                                                                    </telerik:GridBoundColumn>
                                                                         
                                                                    <telerik:GridBoundColumn UniqueName="IdArticulo" Display="false"
                                                                    SortExpression="IdArticulo" HeaderText="ID Artículo" DataField="IdArticulo">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdInterno" Display="true"
                                                                    SortExpression="IdInterno" HeaderText="SKU" DataField="IdInterno">
                                                                    </telerik:GridBoundColumn>
                                                                                 
                                                                    <telerik:GridBoundColumn UniqueName="NombreArticulo" ShowFilterIcon="true"
                                                                    SortExpression="NombreArticulo" HeaderText="Nombre Artículo" DataField="NombreArticulo">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Lote" ShowFilterIcon="true"
                                                                    SortExpression="Lote" HeaderText="Lote" DataField="Lote" Visible ="true">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="FVToShow" ShowFilterIcon="true"
                                                                    SortExpression="FVToShow" HeaderText="Fecha Vencimiento" DataField="FVToShow" Visible ="true"
                                                                    DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>
                                                                                     
                                                                    <telerik:GridBoundColumn UniqueName="Etiqueta" ShowFilterIcon="true"
                                                                    SortExpression="Etiqueta" HeaderText="Ubicación" DataField="Etiqueta">
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="UICantidadBodegaParaMostrar" ShowFilterIcon="true"
                                                                    SortExpression="UICantidadBodegaParaMostrar" HeaderText="Cantidad Bodega UI" DataField="UICantidadBodegaParaMostrar">
                                                                    </telerik:GridBoundColumn>  

                                                                    <telerik:GridBoundColumn UniqueName="UICantidadSistemaParaMostrar" ShowFilterIcon="true"
                                                                    SortExpression="UICantidadSistemaParaMostrar" HeaderText="Cantidad Sistema UI" DataField="UICantidadSistemaParaMostrar">
                                                                    </telerik:GridBoundColumn>   

                                                                    <telerik:GridBoundColumn UniqueName="UIDifenrenciaCantidad" ShowFilterIcon="false"
                                                                    SortExpression="UIDifenrenciaCantidad" HeaderText="Diferencia UI" DataField="UIDifenrenciaCantidad">
                                                                    </telerik:GridBoundColumn>  

                                                                    <telerik:GridBoundColumn UniqueName="CantidadBodegaParaMostrar" Display="false"
                                                                    SortExpression="CantidadBodegaParaMostrar" HeaderText="Cantidad Bodega" DataField="CantidadBodegaParaMostrar">
                                                                    </telerik:GridBoundColumn>  
                                                                
                                                                    <telerik:GridBoundColumn UniqueName="CantidadSistemaParaMostrar" Display="false"
                                                                    SortExpression="CantidadSistemaParaMostrar" HeaderText="Cantidad Sistema" DataField="CantidadSistemaParaMostrar">
                                                                    </telerik:GridBoundColumn> 

                                                                    <telerik:GridBoundColumn UniqueName="DifenrenciaCantidad" Display="false"
                                                                    SortExpression="DifenrenciaCantidad" HeaderText="Diferencia" DataField="DifenrenciaCantidad">
                                                                    </telerik:GridBoundColumn>   
                                                                </Columns>
                                                            </MasterTableView>   
                                                    </telerik:RadGrid>

                                                    <!--Mensajes de Errores-->
                                                    <br />
                                                    <asp:Label runat="server" ID="_lblTotalTomaFisica" ForeColor="Red"></asp:Label> <br />
                                                    <asp:Label runat="server" ID="_lblTotalSistema"  ForeColor="Red"></asp:Label> <br />
                                                   
                                                    <asp:Label runat="server" ID="_lblTotalTransito"  ForeColor="Red"></asp:Label> <br />
                                                    <asp:Label runat="server" ID="_lblDiferenciaSAP"  ForeColor="Red"></asp:Label> <br />
                                                    <!--Botón de Realizar Ajuste-->
                                                    <asp:Button  runat="server" ID="_btnRealizarAjuste" Text="Realizar Ajuste" OnClick="_btnRealizarAjuste_Click" OnClientClick="DisplayLoadingImage1()"/>
                                                    <asp:CheckBox runat="server" id ="_cBIgualSAP" Text="IgualSap" Visible ="False" />
                                                </asp:Panel>
                                            </ContentTemplate>                                          
                                        </asp:UpdatePanel>                                                                                          
                                    </asp:Panel>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </ContentTemplate>
                    </telerik:RadWindow>        
                </Windows>
            </telerik:RadWindowManager> 
        </div>                      
    </asp:Panel>
</asp:Content>
