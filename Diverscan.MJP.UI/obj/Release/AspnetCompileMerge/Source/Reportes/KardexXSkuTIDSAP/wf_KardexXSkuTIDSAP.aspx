<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_KardexXSkuTIDSAP.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.KardexXSkuTIDSAP.wf_KardexXSkuTIDSAP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server">
        <%--comienza UpdatePanel--%>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div id="Principal" class="RadWindow RadWindow_Outlook rwTransparentWindow rwRoundedCorner rwShadow"
                    style="width: 78%; height: 70%; position: absolute; visibility: visible; z-index: 3002; left: 254px; top: 150px; margin-left: 20px; margin-top: 20px; margin-bottom: 20px;">

                    <div style="background-position: center; background-position-x: center; background-position-y: top; z-index: 1000; position: absolute; margin-left: auto; margin-right: auto; left: 0; right: 0;">
                        <center>
                        <img id="loading1" src="../../Images/loading.gif" style="width:80px;height:80px; display:none;" alt="xx" />                                        
                    </center>
                    </div>
                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Bodega"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                            </td>

                            <td>
                                <telerik:RadDatePicker ID="RDPFechaInicio" runat="server" AutoPostBack="false" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
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
                                <asp:Label ID="Label39" runat="server" Text="SKU" Width="100px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                
                               <%-- <asp:Button runat="server" ID="btnRefrescar" Text="Refrescar" AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnRefrescar_Click" Visible="false" />--%>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="width: 48%; float:left">
                        <asp:Label ID="Label1" runat="server" Text="Kardex TID" Width="402px" Font-Bold="true"></asp:Label>
                        <asp:Button runat="server" ID="btnGenerarReporteTID" Text="Generar Reporte" AutoPostBack="false"  OnClick="btnGenerarReporteTID_Click"/>
                        <br /><br />
                        <telerik:RadGrid
                        ID="RGAKardexTID"
                        AllowPaging="True"
                        Width="100%"
                        OnNeedDataSource="RGAKardexTID_NeedDataSource"
                        runat="server"
                        AutoGenerateColumns="False"
                        AllowSorting="True"
                        PageSize="100"
                        AllowMultiRowSelection="false"
                        EnablePostBackOnRowClick="true">
                        <MasterTableView>

                            <Columns>

                                <telerik:GridBoundColumn UniqueName="Tipo"
                                    SortExpression="Tipo" HeaderText="Transacción" DataField="Tipo">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Socio"
                                    SortExpression="Socio" HeaderText="Socio" DataField="Socio">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Fecha"
                                    SortExpression="Fecha" HeaderText="Fecha" DataField="Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}"
                                    DataType="System.DateTime">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Saldo"
                                    SortExpression="Saldo" HeaderText="Saldo" DataField="Saldo">
                                </telerik:GridBoundColumn>

                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnablePostBackOnRowClick="false">
                            <Selecting AllowRowSelect="true"></Selecting>
                        </ClientSettings>

                    </telerik:RadGrid>
                    </div>
                    
                    <div style="width: 50%; float:right;">
                        <asp:Label ID="Label2" runat="server" Text="Kardex SAP" Width="422px" Font-Bold="true"></asp:Label>
                        <asp:Button runat="server" ID="btnGenerarReporteSAP" Text="Generar Reporte" AutoPostBack="false" OnClick="btnGenerarReporteSAP_Click"/>
                        <br /><br />
                        <telerik:RadGrid
                        ID="RGAKardexSAP"
                        AllowPaging="True"
                        Width="100%"
                        OnNeedDataSource="RGAKardexSAP_NeedDataSource"
                        runat="server"
                        AutoGenerateColumns="False"
                        AllowSorting="True"
                        PageSize="100"
                        AllowMultiRowSelection="false"
                        EnablePostBackOnRowClick="true">
                        <MasterTableView>

                            <Columns>

                                <telerik:GridBoundColumn UniqueName="Tipo"
                                    SortExpression="Tipo" HeaderText="Transacción" DataField="Tipo">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Socio"
                                    SortExpression="Socio" HeaderText="Socio" DataField="Socio">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Fecha"
                                    SortExpression="Fecha" HeaderText="Fecha" DataField="Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}"
                                    DataType="System.DateTime">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Cantidad"
                                    SortExpression="Cantidad" HeaderText="Cantidad" DataField="Cantidad">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="Saldo"
                                    SortExpression="Saldo" HeaderText="Saldo" DataField="Saldo">
                                </telerik:GridBoundColumn>

                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnablePostBackOnRowClick="false">
                            <Selecting AllowRowSelect="true"></Selecting>
                        </ClientSettings>

                    </telerik:RadGrid>
                    </div>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
