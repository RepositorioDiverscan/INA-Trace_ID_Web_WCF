<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfSSCCCertificados.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Certificacion.wfSSCCCertificados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
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
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1">
                                <Tabs>
                                    <telerik:RadTab Text="SSCC Certificación" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <h1></h1>
                                                <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                                                    <center>
                                                      <img id="loading1" src="../../images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" >                                        
                                                    </center>
                                                </div>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" class="TexboxNormal" runat="server" AutoPostBack="false" >
                                                                <DateInput DateFormat="dd/MM/yyyy"></DateInput> 
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Búsqueda por los campos con (*)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br /> 
                                                <telerik:RadGrid ID="RGMaestroSolicitud" AllowPaging="True" Width="100%" 
                                                    OnNeedDataSource="RGMaestroSolicitud_NeedDataSource" OnItemCommand="RGMaestroSolicitud_ItemCommand"
                                                    OnClientClick="DisplayLoadingImage1()" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="True" PageSize="50" AllowPading="True" PagerStyle-AlwaysVisible="true">
                                                    <GroupingSettings CaseSensitive="true" />
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridButtonColumn CommandName="btnVerDetalle" Text="Detalle" UniqueName="btnVerDetalle" HeaderText="">
                                                            </telerik:GridButtonColumn>
                                                            <%--<telerik:GridBoundColumn UniqueName="IdMaestroSolicitud"
                                                                SortExpression="IdMaestroSolicitud" HeaderText="Solicitud #" DataField="IdMaestroSolicitud">
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn UniqueName="Comentarios"
                                                                SortExpression="Comentarios" HeaderText="Observación" DataField="Comentarios">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                SortExpression="IdInterno" HeaderText="Id Interno" DataField="IdInterno">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="FechaCreacion"
                                                                SortExpression="Fecha" HeaderText="Fecha Solicitud" DataField="FechaCreacion"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn UniqueName="Nombre"
                                                                SortExpression="Nombre" HeaderText="Destino" DataField="Nombre">
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn UniqueName="PorcentajeAlistado"
                                                                SortExpression="PorcentajeAlistado" HeaderText="Porcentaje Alisto" DataField="PorcentajeAlistado">
                                                            </telerik:GridBoundColumn>
                                                             <%--<telerik:GridBoundColumn UniqueName="FechaEntrega"
                                                                SortExpression="FechaEntrega" HeaderText="Fecha entrega" DataField="FechaEntrega"
                                                                DataFormatString="{0:dd/MM/yyyy}"
                                                                DataType="System.DateTime">
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn UniqueName="SSCCUbicados"
                                                                SortExpression="SSCCUbicados" HeaderText="SSCC Ubicados" DataField="SSCCUbicados">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="Certificado"
                                                                SortExpression="Certificado" HeaderText="Certificado" DataField="Certificado" Display="false">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn UniqueName="CertificadoDisplay" HeaderText="Certificado"> 
                                                                <ItemTemplate> 
                                                                <asp:Label ID="Label1" runat="server" 
                                                                    Text='<%# Convert.ToBoolean(Eval("Certificado")) == true ? "Si" : "No" %>'/> 
                                                                </ItemTemplate> 
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridBoundColumn UniqueName="PorcentajeSSCC"
                                                                SortExpression="PorcentajeSSCC" HeaderText="Porcentaje Certificado" DataField="PorcentajeSSCC">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="PrioridadString" 
                                                                SortExpression="PrioridadString" HeaderText="Prioridad" DataField="PrioridadString">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnablePostBackOnRowClick="false"><Selecting AllowRowSelect="true"></Selecting></ClientSettings>
                                                </telerik:RadGrid>
                                                <br />

                                            <asp:Panel runat="server" ID="Pedidos" Visible="false">                                           
                                                <asp:Panel runat="server" CssClass="TituloPanelVista" ID="PanelPedidos" Visible="false">
                                                    <h1 class="TituloPanelTitulo">SSCC por Ola</h1>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                </asp:Panel>

                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="TableSector">
                                                   
                                                </table>
                                                <br />
                                           <%--Grid nuevo --%>
                                               
                                                 <telerik:RadGrid RenderMode="Lightweight" ID="RGSSCCOla" runat="server" ShowStatusBar="true" AutoGenerateColumns="False"
                                                    PageSize="10" AllowSorting="True" AllowMultiRowSelection="False" AllowPaging="True"
                                                    OnDetailTableDataBind="RGSSCCOla_DetailTableDataBind" OnNeedDataSource="RGSSCCOla_NeedDataSource">
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                    <MasterTableView DataKeyNames="IdSSCC" AllowMultiColumnSorting="True">
                                                        <DetailTables>
                                                            <telerik:GridTableView DataKeyNames="IdInterno" Name="Articulos" 
                                                                Width="100%" ShowStatusBar="true" AllowSorting="True">                                                               
                                                                <Columns>
                                                                  <telerik:GridBoundColumn SortExpression="IdInterno" HeaderText="SKU" HeaderButtonType="TextButton"
                                                                        DataField="IdInterno">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="Nombre" HeaderText="Nombre Articulo" HeaderButtonType="TextButton"
                                                                     DataField="Nombre">
                                                                  </telerik:GridBoundColumn>  
                                                                  <telerik:GridBoundColumn SortExpression="Lote" HeaderText="Lote" HeaderButtonType="TextButton"
                                                                     DataField="Lote">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="FechaVencimiento" HeaderText="Fecha de Vencimiento" HeaderButtonType="TextButton"
                                                                     DataField="FechaVencimiento">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="Cantidad" HeaderText="Cantidad" HeaderButtonType="TextButton"
                                                                     DataField="Cantidad">
                                                                  </telerik:GridBoundColumn> 
                                                                  <telerik:GridBoundColumn SortExpression="CantidadDiferencia" HeaderText="Diferencia" HeaderButtonType="TextButton"
                                                                     DataField="CantidadDiferencia">
                                                                  </telerik:GridBoundColumn> 
                                                                  <telerik:GridBoundColumn SortExpression="Certificado" HeaderText="Certificado" HeaderButtonType="TextButton"
                                                                     DataField="Certificado">
                                                                  </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                        <Columns>                                                            
                                                            <%--<telerik:GridBoundColumn HeaderButtonType="TextButton" DataField="_idSSCC" >
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn SortExpression="DescripcionSSCC" HeaderText="Descripcion SSCC" HeaderButtonType="TextButton"
                                                                DataField="DescripcionSSCC" >
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="ConsecutivoSSCC" HeaderText="Consecutivo SSCC" HeaderButtonType="TextButton"
                                                                DataField="ConsecutivoSSCC">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="FechaProcesadoSSCC" HeaderText="Fecha Certificado" HeaderButtonType="TextButton"
                                                                DataField="FechaProcesadoSSCC">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="UbicacionSSCC" HeaderText="Ubicacion de Certificación" HeaderButtonType="TextButton"
                                                                DataField="UbicacionSSCC">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="UbicacionSSCC" HeaderText="Ubicacion de Certificación" HeaderButtonType="TextButton"
                                                                DataField="UbicacionSSCC">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="NombreUsuario" HeaderText="Certificador" HeaderButtonType="TextButton"
                                                                DataField="NombreUsuario">
                                                            </telerik:GridBoundColumn>  
                                                            <telerik:GridBoundColumn SortExpression="CantidadCertificada" HeaderText="Lineas Certificadas" HeaderButtonType="TextButton"
                                                                DataField="CantidadCertificada">
                                                            </telerik:GridBoundColumn>                                                            
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                     <%--Grid nuevo --%> 
                                                 </asp:Panel>
                                                <br />
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
