<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Emos.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Emo.wf_Emos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage1() {
            document.getElementById("loading1").style.display = "block";
        }
    </script>
    <asp:Panel ID="Panel14" runat="server">
        <div id="RestrictionZoneID" class="WindowContenedor">
            <telerik:RadWindowManager RenderMode="Lightweight" OffsetElementID="offsetElement" ID="RadWindowManager1" runat="server" EnableShadow="false">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>
                <Windows>
                    <telerik:RadWindow ID="WinUsuarios"  runat="server" VisibleStatusbar="false" VisibleOnPageLoad="true" RestrictionZoneID="RestrictionZoneID"  AutoSize="true" style="width:1500px">
                        <ContentTemplate>
                            <telerik:RadTabStrip AutoPostBack="false" RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Gestion de Emos" Width="200px"></telerik:RadTab>                                    
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage">
                                <telerik:RadPageView runat="server" ID="RadPageView1">
                                    <asp:Panel ID="Panel1" runat="server" Class="TabContainer">
                                        <asp:UpdatePanel  runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <br />
                                                <br />
                                               
                                                <table style="border-radius: 10px; border: 1px solid grey; width: 98%; border-collapse: initial; margin-left: 1%; margin-top: 1%;" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Bodega"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddBodega" CssClass="TexboxNormal" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddBodega_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaInicio" class="TexboxNormal" runat="server" AutoPostBack="false" DateInput-DateFormat="yyyy/MM/dd">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Fecha Final:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="RDPFechaFinal" runat="server" AutoPostBack="false" DateInput-DateFormat="yyyy/MM/dd"></telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Transportista"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlTransportista" CssClass="TexboxNormal" Width="250px" ></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Buscar" Width="100px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" Class="TexboxNormal" Width="300px"></asp:TextBox>
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>                                                            
                                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" Style="margin-left: 25%"
                                                                AutoPostBack="false" OnClientClick="DisplayLoadingImage1()" OnClick="btnBuscar_Click" />
                                                        </td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td></td>
                                                        <td>                                                            
                                                            <asp:Button runat="server" ID="btnCreateEmo" Text="Reporte Emo" Style="margin-left: 25%" 
                                                                OnClick="btnReporteEmo_Click" PostBackUrl="~/Operaciones/Emo/wf_Emos.aspx"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                <asp:Panel runat="server" ID="Panel2" >                                           
                                                    <asp:Panel runat="server" CssClass="TituloPanelVista" ID="Panel3" Visible="false">
                                                        <h1 class="TituloPanelTitulo">Listado Emos</h1>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" OnClientClick="return false;" />
                                                    </asp:Panel>                                                  

                                                    <%--Revisar los datos del RadGrid--%>                                                                                               
                                                     <telerik:RadGrid ID="RGEmo" RenderMode="Lightweight" runat="server" ShowStatusBar="true" AutoGenerateColumns="False"
                                                         PageSize="10" AllowSorting="True" AllowMultiRowSelection="False" AllowPaging="true"
                                                        OnDetailTableDataBind="RGEmo_DetailTableDataBind" OnNeedDataSource="RGEmo_NeedDataSource">                                                     
                                                        <%--Aca se cargan los datos del RadGrid--%>
                                                         <PagerStyle Mode="NumericPages"></PagerStyle>                                                         
                                                        <MasterTableView DataKeyNames="IdEmo" AllowMultiColumnSorting="True">
                                                           <DetailTables>
                                                            <telerik:GridTableView DataKeyNames="IdSap" Name="Invoices" 
                                                                Width="100%" ShowStatusBar="true" AllowSorting="True">                                                               
                                                                <Columns>
                                                                  <telerik:GridBoundColumn SortExpression="IdSap" HeaderText="IdSap" HeaderButtonType="TextButton"
                                                                        DataField="IdSap">
                                                                  </telerik:GridBoundColumn>   
                                                                  <telerik:GridBoundColumn SortExpression="BillNumber" HeaderText="Número Factura" HeaderButtonType="TextButton"
                                                                     DataField="BillNumber">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="BillPrice" HeaderText="Monto" HeaderButtonType="TextButton"
                                                                     DataField="BillPrice">
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn SortExpression="Weight" HeaderText="Peso" HeaderButtonType="TextButton"
                                                                     DataField="Weight">
                                                                  </telerik:GridBoundColumn> 
                                                                  <telerik:GridBoundColumn SortExpression="Volume" HeaderText="Volumen" HeaderButtonType="TextButton"
                                                                     DataField="Volume">
                                                                  </telerik:GridBoundColumn>                                              
                                                                  <telerik:GridBoundColumn SortExpression="NumberPage" HeaderText="Núm. Páginas" HeaderButtonType="TextButton"
                                                                     DataField="NumberPage">
                                                                  </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                            <Columns>                                                                                                                                               
                                                                     <telerik:GridBoundColumn UniqueName="IdEmo"
                                                                        SortExpression="IdEmo" HeaderText="Número Emo" DataField="IdEmo" Display="false">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="IdInterno"
                                                                        SortExpression="IdInterno" HeaderText="Número Emo" DataField="IdInterno">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn  HeaderButtonType="TextButton"
                                                                        SortExpression="NombreTransportista" HeaderText="Transportista" DataField="NombreTransportista">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderButtonType="TextButton"
                                                                        SortExpression="RecordDate" HeaderText="Fecha de Registro" DataField="RecordDate"
                                                                        DataFormatString="{0:dd/MM/yyyy}">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderButtonType="TextButton"
                                                                        SortExpression="TotalPeso" HeaderText="Peso Total" DataField="TotalPeso">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderButtonType="TextButton"
                                                                        SortExpression="TotalMonto" HeaderText="Monto total" DataField="TotalMonto">
                                                                    </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>                                                                                                        
                                                    </telerik:RadGrid>                                           
                                                    <br />
                                                </asp:Panel>
                                                <h1></h1>    
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
