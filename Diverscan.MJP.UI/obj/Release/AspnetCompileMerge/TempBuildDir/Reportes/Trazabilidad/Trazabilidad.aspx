<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Trazabilidad.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.Trazabilidad.Trazabilidad" %>

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
                                    <telerik:RadTab Text="Trazabilidad" Width="200px"></telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                            
                                            <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                        </asp:Panel>


                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div style="background-position: center; background-position-x: center; background-position-y: center; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                         <img id="loading1" src="http://172.30.1.5/TRACEID/images/loading.gif" style="width:80px;height:80px; display:none;" >
                                                    </center>
                                                </div>

                                                <!--CUERPO-->
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">

                                                    <%-- Visualización de elementos --%>
                                                    

                                                     <tr>
                                                        <td>
                                                            <h1 class="TituloPanelTitulo">Seleccione los Datos</h1>
                                                            <br />
                                                            <asp:Label ID="Label2" runat="server" Text="Búsqueda del artículo"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="200px"   style="margin-left:10px;" AutoPostBack="true" ></asp:DropDownList>
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="Label7" runat="server" Text="Búsqueda del artículo"></asp:Label>
                                                            <asp:TextBox CssClass="TexboxNormal" ID="TxtReferencia" style="margin-left:10px;" runat="server" Width="150px" AutoCompleteType="Disabled" Enabled="true"></asp:TextBox>
                                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" style="margin-left:10px;" Width="133px" OnClick="btnBuscar_Click"
                                                                OnClientClick="DisplayLoadingImage1()" />
                                                            <br />
                                                             <br />
                                                            <asp:Label ID="LbArticulos" runat="server" Text="Artículo:"></asp:Label>
                                                            <asp:DropDownList ID="ddlArticulos" Class="TexboxNormal" runat="server" AutoPostBack="false" style="margin-left:10px;" Width="372px"></asp:DropDownList>
                                                            <h1></h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <%--<asp:Panel ID="PanelFechaRecepcion" runat="server" GroupingText="Búsqueda por rango de fechas">--%>
                                                            <h1 class="TituloPanelTitulo">Búsqueda por Rango de Fechas</h1>
                                                            <h1></h1>
                                                            <asp:Label ID="LblFechaInicial" runat="server" Text="Fecha Inicial:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaInicial" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <asp:Label ID="LblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                                                            <%--<asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true"
                                                                OnClick="_btnBuscar_Click"
                                                                OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>--%>
                                                            <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" AutoPostBack="true" OnClick="_btnBuscar_Click" OnClientClick="DisplayLoadingImage1()"></asp:Button>
                                                            <h1></h1>
                                                        </tr>

                                                    </table>

                                                <!--SIGUIENTE PANEL DE ORDENES DE COMPRA-->

                                                   <asp:Panel ID="PanelTrazabilidad" runat="server" Visible="false" >
                                                    <h1></h1>                                                    
                                                    <%--<h1 class="TituloPanelTitulo">Trazabilidad Bodega</h1>--%>
                                                    <h1 class="TituloPanelTitulo">Trazabilidad de Articulos</h1>
                                                    <h1></h1>
                                                    <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel"   Visible="true" style="margin-left:1%" OnClick="btnExportar_Click"/>
                                                    <%--OnClick="btnExportar_Click"
                                                        
                                                        <input type="button" value="Imprimir Reporte" onclick="javascript: imprimirDiv('gridTrazabilidadBodega')" />--%>
                                                    <h1></h1>
                                                    <div id="gIdOc">
                                                        

                                                       <telerik:RadGrid ID="gridReporteOrdenCompraTrazabilidad" AllowPaging="true" Width="98%" OnNeedDataSource="gridReporteOrdenCompraTrazabilidad_NeedDataSource" style="margin-left:1%"  PagerStyle-AlwaysVisible="true"
                                                        OnClientClick="DisplayLoadingImage1()" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        PageSize="10" AllowMultiRowSelection="false">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                        <MasterTableView>
                                                            <Columns>
                                                               
                                                                    <telerik:GridBoundColumn UniqueName="IdTrazabilidad"
                                                                        SortExpression="IdTrazabilidad" HeaderText="Identidicador Trazabilidad" DataField="IdTrazabilidad"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" Visible ="false">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Cantidad" 
                                                                        SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdEstado"
                                                                        SortExpression="IdEstado" HeaderText="Identidicador Estado" DataField="IdEstado"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Operacion"
                                                                        SortExpression="Operacion" HeaderText="Tipo de Operación" DataField="Operacion"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="FechaRegistro" 
                                                                        SortExpression="FechaRegistro" HeaderText="Fecha de Registro" DataField="FechaRegistro"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True" DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Nombre" 
                                                                        SortExpression="Nombre" HeaderText="Movimiento" DataField="Nombre"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Saldo" 
                                                                        SortExpression="Saldo" HeaderText="Saldo" DataField="Saldo"
                                                                        AllowFiltering="True" AutoPostBackOnFilter="True">
                                                                    </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="true"></Selecting>
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                    </div>
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
