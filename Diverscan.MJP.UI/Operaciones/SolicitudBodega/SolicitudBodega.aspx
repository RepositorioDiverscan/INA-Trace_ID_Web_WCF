<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudBodega.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Solicitud_Bodega.SolicitudBodega" %>

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
        function MuestraMensajeOk() {
            alert("Proceso Terminado exitosamente");
        }
        //function onKeyPressed(box) {
        //    $("input[type=text]").keypress(function () {
        //        if (event.keyCode == 13) {                    
        //            //if (box.name === 'txtArticulo') {
        //            //    alert("Entro!!!");
        //            //}
        //        }              
        //    });
        //}
        
    </script>

    <asp:Panel ID="Panel4" runat="server">
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
                                    <telerik:RadTab Text="Solicitud" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                            <h1 class="TituloPanelTitulo">Datos de la Solicitud</h1>
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>

                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="../../Images/loading.gif"" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>
                                                
                                                 <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                    <tr>
                                                        <td>
                                                           <asp:Label style =" margin-top:2px;" runat="server" ID="_lblFechaAplicado" Text="Fecha Entrega" ></asp:Label>
     
                                                        </td>
                                                         <td>
                                                           
                                                          <telerik:RadDatePicker runat="server" ID="_rdpFechaAplicado" AutoPostBack="true"  onchange="DisplayLoadingImage1()" OnSelectedDateChanged ="_rdpFechaAplicado_SelectedDateChanged"></telerik:RadDatePicker>
                                                           
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtNombre" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Comentarios"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txtComentarios" Width="300px" Height="30px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                 <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel2">
                                                    <h1 class="TituloPanelTitulo">Detalle de la Solicitud</h1>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                </asp:Panel>                                                                                                

                                                <h1></h1>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Num.Detalle"></asp:Label>
                                                            <asp:TextBox CssClass="TextBoxBusqueda" style="margin-left:115px;" ID="txtIdPreLineaDetalleSolicitud" runat="server" Width="85px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                   <%-- <--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Búsqueda del artículo"></asp:Label>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TxtReferencia" style="margin-left:65px;" runat="server"
                                                                Width="150px" AutoCompleteType="Disabled" Enabled="true" onKeyPress="javascript:onKeyPressed(this);"></asp:TextBox>
                                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" style="margin-left:10px;" Width="133px"  OnClick="btnBusqueda_Click" AutoPostBack="true"/>
                                                        </td>
                                                        <td>                                                           
                                                        </td>                                                       
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Artículo"></asp:Label>
                                                            <asp:TextBox CssClass="TexboxNormal" style="margin-left:141px;" ID="txtArticulo" runat="server" Width="300px" Enabled="false" ></asp:TextBox>
                                                            <!--<asp:Label ID="lbUnidadMedida" runat="server" Text="" Visible="false"></asp:Label>-->
                                                        </td>                                                     
                                                    </tr>
                                                     <tr>                                                                                                                  
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Cantidad Presentacion"></asp:Label>
                                                             <asp:TextBox CssClass="TexboxNormal" style="margin-left:60px;" ID="txtCantidad" runat="server" Width="300px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>                                                      
                                                    </tr>
                                                </table>

                                           <%--      <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; margin-top:6px; margin-bottom:5px; border-collapse: initial;" id="Table3">
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Presentacion" ></asp:Label>
                                                            <asp:DropDownList ID="ddlpresentacion" style="margin-left:113px;" Class="TexboxNormal"  runat="server" Width="300px"></asp:DropDownList>
                                                            <asp:Label ID="LabelGtin" runat="server" Text="." Visible ="false"></asp:Label>
                                                            
                                                        </td>                                                     
                                                    </tr>
                                                </table>--%>
                                                <h1></h1>
                                                <asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar Artículo" Width="150px" style="margin-left:15px;" OnClientClick="DisplayLoadingImage1()" OnClick="btnAgregarArticulo_Click"  />
                                                <asp:Button ID="btnEditarArticulo" runat="server" Text="Editar Artículo" Width="150px"  style="margin-left:15px;"  OnClientClick="DisplayLoadingImage1()"  OnClick="btnEditarArticulo_Click" Visible="false" />
                                                <asp:Label ID="lblSeparador" runat="server" Text="|||" Visible="false"></asp:Label>
                                                <asp:Button ID="btnEliminarArticulo" runat="server" Text="Eliminar Artículo" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnEliminarArticulo_Click" Visible="false" />
                                                <asp:Label ID="lblSeparador2" runat="server" Text="|||" Visible="false"></asp:Label>
                                                <asp:Button ID="btnCancelarArticulo" runat="server" Text="Cancelar" Width="150px" OnClientClick="DisplayLoadingImage1()" OnClick="btnCancelarArticulo_Click" Visible="false"  />
                                                <asp:Button ID="btnAgregarSolicitud" runat="server" Text="Agregar Solicitud" Width="150px"  style="margin-left:15px;" OnClientClick="DisplayLoadingImage1()"   OnClick="btnAgregarSolicitud_Click"   Enabled="false" />
                                                <h1></h1>

                                                  <asp:Panel runat="server" CssClass="TituloPanelVistaDetalle" ID="Vista_DetalleSolicitud0">
                                                    <h1 class="TituloPanelTitulo">Listado de la Solicitud</h1>
                                                </asp:Panel>

                                                 <telerik:RadGrid
                                                    ID="RadGridOPESALPreDetalleSolicitud"
                                                    AllowPaging="True"
                                                    Width="100%"
                                                     OnNeedDataSource="RadGridOPESALPreDetalleSolicitud_NeedDataSource"
                                                    runat="server"
                                                    AllowFilteringByColumn="true"
                                                    AutoGenerateColumns="False"
                                                    AllowSorting="True"
                                                    PageSize="10"
                                                    AllowMultiRowSelection="true"
                                                    OnItemCommand="RadGridOPESALPreDetalleSolicitud_ItemCommand"
                                                    PagerStyle-AlwaysVisible="true">
                                                    <MasterTableView>
                                                        <Columns>

                                                            <telerik:GridBoundColumn UniqueName="IdPreLineaDetalleSolicitud"
                                                                SortExpression="IdPreLineaDetalleSolicitud" HeaderText="Id Detalle" DataField="IdPreLineaDetalleSolicitud"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="idArticuloInterno"
                                                                SortExpression="idArticuloInterno" HeaderText="SKU" DataField="idArticuloInterno"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="NombreArticuloInterno"
                                                                SortExpression="NombreArticuloInterno" HeaderText="Nombre del Articulo" DataField="NombreArticuloInterno"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Cantidad"
                                                                SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="UnidadMedida"
                                                                SortExpression="UnidadMedida" HeaderText="Unidad Medida" DataField="UnidadMedida"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                          <%--<telerik:GridBoundColumn UniqueName="UnidadesAlisto"
                                                                SortExpression="UnidadesAlisto" HeaderText="Unidades Alisto" DataField="UnidadesAlisto"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>--%>

                                                             <telerik:GridBoundColumn UniqueName="GTIN"
                                                                SortExpression="GTIN" HeaderText="GTIN" DataField="GTIN"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                             <telerik:GridBoundColumn UniqueName="CantidadGTIN"
                                                                SortExpression="CantidadGTIN" HeaderText="Cantidad GTIN" DataField="CantidadGTIN"
                                                                AllowFiltering="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true"></Selecting>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                                 
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



