<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_MaestroArticulo.aspx.cs" Inherits="Diverscan.MJP.UI.Mantenimiento.Articulos.wf_MaestroArticulo" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                <script type='text/javascript'>
                            function DisplayLoadingImage1() {
                                document.getElementById("loading1").style.display = "block";
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
                                    <telerik:RadTab Text="Maestro Articulos" Width="200px"></telerik:RadTab>
                                    <telerik:RadTab Text="Actividad" Width="200px" Visible ="false"></telerik:RadTab>
                                    <telerik:RadTab Text="Acciones" Width="200px" Visible ="false"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  > 
                                        
                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_MaestroArticulos">
                                                <h1 class="TituloPanelTitulo">Maestro Articulos</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                                <ContentTemplate>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table4"> 
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" Text="Buscar" Width ="100px"></asp:Label> 
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>  
                                                     
                                                                <asp:Button runat="server" ID="btnBuscarMaestroArticulo" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click"  />   
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>
                                            
                                                    <br>

                                                    <asp:Button  ID="btnAgregar" runat ="server" Text= "Agregar" Width ="150px"  OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregar_Click" />
                                                    <%--<asp:Label ID="Label16" runat="server" Text="|||"></asp:Label>--%>
                                                    <asp:Button ID ="btnEditar"  runat ="server" Text= "Editar" Width ="150px"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditar_Click" Visible="false"/>   
                                                    <asp:Label ID="Label29" runat="server" Text="|||"></asp:Label>
                                                    <asp:Button ID="Btnlimpiar1" runat="server" Text="Limpiar" OnClick ="Btnlimpiar1_Click" OnClientClick="DisplayLoadingImage1()" />    
                                                    <h1></h1>

                                                <div style="background-position:center; background-position-x:center; background-position-y:center; z-index:1000; position:absolute; margin-left:auto; margin-right:auto; left:0; right:0; ">
                                                <center>
                                                     <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                </center>
                                                </div>


                                                   <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">                      
                                                        <tr>
                                                             <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Num. Articulo" Width="200px"></asp:Label> 
                                                            </td>
                                                            <td >     
                                                                <asp:TextBox CssClass="TextBoxBusqueda" ID="txtidArticulo" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false" />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label23" runat="server" Text="Id Interno" Width="200px"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtidInterno" runat="server" Width="85px" ></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label2" runat="server" Text="Compania" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidCompania" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                           </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label1" runat="server" Text="Nombre" Width="200px"></asp:Label> 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px" ></asp:TextBox>
                                                            </td>
                                               
                                                        </tr>                                                                                                
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Nombre HH" Width="200px" Visible="true"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtNombreHH" runat="server" Width="300px" Visible="true" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text="GTIN" Width="200px"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtGTIN" runat="server" Width="300px"></asp:TextBox>
                                                                <asp:Button  ID="BtnGeneraGTIN" runat ="server" Text= "Generar" Width ="60px"  OnClientClick="DisplayLoadingImage1()" OnClick="BtnGeneraGTIN_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label17" runat="server" Text="Bodega" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidBodega" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td> 
                                                                <asp:Label ID="Label14" runat="server" Text="Peso en Kilos" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtPesoKilos" runat="server" Width="300px" Text="0" ></asp:TextBox>
                                                                <%--<asp:DropDownList ID="ddlidUnidadMedida" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>--%>
                                                            </td>   
                                                           </tr>
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label15" runat="server" Text="Dimension Unidad M3" Width="200px" Visible="false"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtDimensionUnidadM3" runat="server" Width="300px" text="0" Visible="false"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="ddlidTipoEmpaque" Class="TexboxNormal" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>--%>
                                                            </td>   
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label18" runat="server" Text="Familia" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidFamilia" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false"></asp:DropDownList>
                                                            </td>   
                                                        </tr>    

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="chkGranel" Text="¿Es granel?"></asp:CheckBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" Text="Temperatura Maxima" Width="200px" Visible="false"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtTemperaturaMaxima" runat="server" Width="300px" Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label21" runat="server" Text="Temperatura Minima" Width="200px" Visible="false"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtTemperaturaMinima" runat="server" Width="300px" Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Text="Contenido" Width="200px"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtContenido" runat="server" Width="300px" Text="1" ></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" Text="Unidad de Medida" Width="200px"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtUnidadMedida" runat="server" Width="300px" Text="UNID."></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Equivalencia" Width="200px"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="TxtEquivalencia" runat="server" Width="300px" Text="1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" Text="Dias Minimos Vencimiento" Width="200px" Visible="false"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtDiasMinimosVencimiento" runat="server" Width="300px" Text="0" Visible="false" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label26" runat="server" Text="Dias Minimos Vencimiento en Restaurantes" Width="200px" Visible="false"></asp:Label>  
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="TexboxNormal" ID="txtDiasMinimosVencimientoRestaurantes" runat="server" Width="300px" Text="0" Visible="false" ></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td> 
                                                                <asp:Label ID="Label4" runat="server" Text="Categoria del Artículo" Width="200px" Visible="false"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:DropDownList ID="ddlidCategoriaArticulo" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false" Visible="false"></asp:DropDownList>
                                                            </td>   
                                                        </tr>

                                                         <tr>
                                                            <td> 
                                                                <asp:Label ID="Label5" runat="server" Text="Empaque" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:TextBox ID="txtEmpaque" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false" ></asp:TextBox>
                                                            </td>   
                                                        </tr>

                                                       <tr>
                                                            <td> 
                                                                <asp:Label ID="Label8" runat="server" Text="Minimo Picking" Width="200px"></asp:Label>       
                                                            </t>
                                                            <td>
                                                                <asp:TextBox ID="txtMinPicking" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="false" TextMode="Number" Min="1" Text="1" ></asp:TextBox>
                                                            </td>   
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="chkActivo" Text="Activo" Checked="true"></asp:CheckBox>
                                                                <asp:CheckBox runat="server" ID="chkGenerado"  Checked="false" Visible="false"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <asp:Panel runat="server"  CssClass="TituloPanelVistaDetalle" ID="Vista_MaestroArticulos0">
                                                        <h1 class="TituloPanelTitulo">Listado de Artículos</h1>
                            
                                                    </asp:Panel>
                                                    
                                                    <telerik:RadGrid ID="RadGridMaestroArticulo" AllowPaging="True" Width="100%"  OnNeedDataSource ="RadGridMaestroArticulo_NeedDataSource"
                                                        runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="10" AllowMultiRowSelection="true" OnItemCommand="RadGridMaestroArticulo_ItemCommand">
                                                         <MasterTableView>
                                                            <Columns>
                                                               <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1">
                                                                </telerik:GridClientSelectColumn>--%>

                                                                <telerik:GridBoundColumn UniqueName="idArticulo"
                                                                    SortExpression="idArticulo" HeaderText="Id Articulo" DataField="idArticulo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idInterno"
                                                                    SortExpression="idInterno" HeaderText="Id Interno" DataField="idInterno" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idCompania"
                                                                    SortExpression="idCompania" HeaderText="Id Compania" DataField="idCompania" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Nombre"
                                                                    SortExpression="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreHH"
                                                                    SortExpression="NombreHH" HeaderText="Nombre HH" DataField="NombreHH">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="GTIN"
                                                                    SortExpression="GTIN" HeaderText="GTIN" DataField="GTIN">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Referencia_Interno"
                                                                    SortExpression="Referencia_Interno" HeaderText="Referencia Bexim" DataField="Referencia_Interno" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idBodega" Display="false"
                                                                    SortExpression="idBodega" HeaderText="Id Bodega" DataField="idBodega" Visible="true">
                                                                </telerik:GridBoundColumn>


                                                                <telerik:GridBoundColumn UniqueName="NombreBodega"
                                                                    SortExpression="NombreBodega" HeaderText="Bodega" DataField="NombreBodega">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="PesoKilos"
                                                                    SortExpression="PesoKilos" HeaderText="Peso en Kilos" DataField="PesoKilos" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DimensionUnidadM3"
                                                                    SortExpression="DimensionUnidadM3" HeaderText="Dimension Unidad M3" DataField="DimensionUnidadM3" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idFamilia" Display="false"
                                                                    SortExpression="idFamilia" HeaderText="Id Familia" DataField="idFamilia" Visible="true"> 
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreFamilia"
                                                                    SortExpression="NombreFamilia" HeaderText="Familia del Artículo" DataField="NombreFamilia">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Granel"
                                                                    SortExpression="Granel" HeaderText="Granel" DataField="Granel" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="TemperaturaMaxima"
                                                                    SortExpression="TemperaturaMaxima" HeaderText="Temperatura Maxima" DataField="TemperaturaMaxima" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="TemperaturaMinima"
                                                                    SortExpression="TemperaturaMinima" HeaderText="Temperatura Minima" DataField="TemperaturaMinima" Visible="false">
                                                                </telerik:GridBoundColumn>
                      
                                                                <telerik:GridBoundColumn UniqueName="Contenido"
                                                                    SortExpression="Contenido" HeaderText="Contenido" DataField="Contenido"  Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Unidad_Medida"
                                                                    SortExpression="Unidad_Medida" HeaderText="Unidad de Medida" DataField="Unidad_Medida">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="Empaque"
                                                                    SortExpression="Empaque" HeaderText="Empaque" DataField="Empaque" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DiasMinimosVencimiento"
                                                                    SortExpression="DiasMinimosVencimiento" HeaderText="Dias Minimos de Vencimiento" DataField="DiasMinimosVencimiento" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="DiasMinimosVencimientoRestaurantes"
                                                                    SortExpression="DiasMinimosVencimientoRestaurantes" HeaderText="Dias Minimos Vencimiento Restaurantes" DataField="DiasMinimosVencimientoRestaurantes" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="idCategoriaArticulo" Display="false"
                                                                    SortExpression="idCategoriaArticulo" HeaderText="Id CategoriaArticulo" DataField="idCategoriaArticulo" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="NombreCategoriaArticulo"
                                                                    SortExpression="NombreCategoriaArticulo" HeaderText="Nombre Categoria del Artículo" DataField="NombreCategoriaArticulo" Visible="false">
                                                                </telerik:GridBoundColumn>

                                                                 <telerik:GridBoundColumn UniqueName="MinimoPicking"
                                                                    SortExpression="MinimoPicking" HeaderText="Minimo Picking" DataField="Minimo Picking" Visible="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="Activo"
                                                                    SortExpression="Activo" HeaderText="Activo" DataField="Activo">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn UniqueName="FechaRegistro"
                                                                    SortExpression="FechaRegistro" HeaderText="Fecha Registro" DataField="FechaRegistro">
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
