<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventarioBasicoVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Administracion.Inventario.InventarioBasicoVisor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel4" runat="server" Style="margin-left: 20px">
        <div id="RestrictionZoneID" class="WindowContenedor">
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Windows>
                    <telerik:RadWindow ID="WinUsuarios" runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID" AutoSize="true">
                        <ContentTemplate>
                            <!-- Menú principal de los tabs -->
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Agregar Inventario" Width="200px"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <!-- Ejecución del Primer Tab -->
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                                <!-- Vista del Primer Tab-->
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <!--Formulario de Ingreso de Inventario-->

                                                <!--Campo del Familia-->
                                                <asp:Label runat="server" ID="_lblFamilia" Text="Id Familia" Style="margin-left: 8px;"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtFamilia" Style="margin-top: 10px; margin-left: 55px"></asp:TextBox>

                                                <!-- Campo de Fecha de Aplicar -->
                                                <br />
                                                <br />
                                                <asp:Label runat="server" ID="_lblFechaPorAplicar" Text="Fecha Por Aplicar" Style="margin-left: 5px;"></asp:Label>
                                                <telerik:RadDatePicker runat="server" ID="_rdpFechaPorAplicar" Style="margin-left: 5px;">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                                </telerik:RadDatePicker>

                                                <!-- Campo de Tipo de Inventario -->
                                                <br />
                                                <br />
                                                <asp:Label runat="server" ID="_lblTipoInventario" Text="Tipo de Inventario" Style="margin-left: 5px;"></asp:Label>
                                                <asp:DropDownList ID="lstTipoInventario" runat="server" Width="150px">
                                                    <asp:ListItem Text="MATERIALES" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="EQUIPO" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="MATERIAL DEVOLUTIVO" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="MATERIAL PERECEDERO" Value="4"></asp:ListItem>
                                                </asp:DropDownList>

                                                <!-- Campo de Usuario -->
                                                <br />
                                                <br />
                                                <asp:Label runat="server" ID="Usuario" Text="Usuario a cargo" Style="margin-left: 5px;"></asp:Label>
                                                <asp:DropDownList ID="lstUsarios" runat="server" Width="150px" Style="margin-left: 12px;">
                                                </asp:DropDownList>

                                                <!-- Campo de Botón de Agregar -->
                                                <br />
                                                <br />
                                                <asp:Button runat="server" ID="_btnAgregar" Text="Agregar" OnClick="_btnAgregar_Click" Style="margin-left: 5px;" />

                                                <!--Panel de Consultas por fechas-->
                                                <br />
                                                <br />
                                                <asp:Panel ID="Consultas" runat="server" GroupingText="Consultas" Style="margin-left: 5px;">
                                                    <!--Campo de Fecha de Consulta Inicial-->
                                                    <asp:Label runat="server" ID="_lblFechaInicio" Text="FechaInicio: "></asp:Label>
                                                    <telerik:RadDatePicker runat="server" ID="_rdpFechaInicio" Style="margin-right: 20px;">
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                                    </telerik:RadDatePicker>

                                                    <!--Campo de Fecha de Consulta Final-->
                                                    <asp:Label runat="server" ID="_lblFechaFinal" Text="FechaFinal: "></asp:Label>
                                                    <telerik:RadDatePicker runat="server" ID="_rdpFechaFin">
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                                    </telerik:RadDatePicker>

                                                    <!--Campo de Botón de Buscar-->
                                                    <asp:Button runat="server" ID="_btnBuscar" Text="Buscar" OnClick="_btnBuscar_Click" Style="margin-left: 15px" />
                                                </asp:Panel>

                                                <!--Tabla para mostrar los Inventarios Básicos-->
                                                <br />
                                                <telerik:RadGrid ID="_rgInventarioBasicos" AllowPaging="True" Width="100%" OnNeedDataSource="_rgInventarioBasicos_NeedDataSource"
                                                    runat="server" AutoGenerateColumns="False" AllowSorting="True" PageSize="100" AllowMultiRowSelection="true" PagerStyle-AlwaysVisible="true">
                                                    <MasterTableView>
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="IdFamilia"
                                                                SortExpression="IdFamilia" HeaderText="Id Familia" DataField="IdFamilia">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Familia"
                                                                SortExpression="Familia" HeaderText="Familia" DataField="Familia">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="TipoInventario"
                                                                SortExpression="TipoInventario" HeaderText="Tipo de Inventario" DataField="TipoInventario">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="FechaPorAplicar"
                                                                SortExpression="FechaPorAplicar" HeaderText="Fecha Por Aplicar" DataField="FechaPorAplicar"
                                                                DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="EstadoToShow"
                                                                SortExpression="EstadoToShow" HeaderText="Estado" DataField="EstadoToShow">
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridBoundColumn UniqueName="Usuario"
                                                                SortExpression="Usuario" HeaderText="Usuario a cargo" DataField="Usuario">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
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
