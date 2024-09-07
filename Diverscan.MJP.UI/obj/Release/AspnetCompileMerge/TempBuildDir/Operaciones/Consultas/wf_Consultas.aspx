<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Consultas.aspx.cs" Inherits="Diverscan.MJP.UI.Operaciones.Consultas.wf_Consultas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel4" runat="server" >   
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
                                    <telerik:RadTab Text="Consultas Motor" Width="200px"></telerik:RadTab>
                                </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage"  >
                                    <telerik:RadPageView runat="server" ID="RadPageView1"  >
                                        <asp:Panel ID="Panel1" runat="server" Class="TabContainer"  >                                        
                                            <asp:Panel runat="server"  CssClass="TituloPanelVista" ID="Vista_MaestroSolicitud">
                                                <h1 class="TituloPanelTitulo">Consultas al motor</h1>
                                                   <asp:ImageButton ID="DummyButton" runat="server" ImageUrl="~/Images/Ico/imageres_123.ico" Height="0px" onClientClick="return false;" />
                                            </asp:Panel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                                            <ContentTemplate>
                                                <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table1">
                                                     <tr>
                                                    <td align ="center">
                                                        <asp:TextBox runat="server" CssClass="TexboxNormal" ID="txmRespuesta" Width="90%" Height="225px"  TextMode="MultiLine" AutoPostBack="true"   ></asp:TextBox>
                                                    </td>
                                                </tr>           
                                                    </table>
                                                    <table width="100%" style="border-radius: 10px; border: 1px solid grey; width: 100%; border-collapse: initial;" id="Table2">
                                                    <tr>
                                                    <h1></h1>
                                                    <td> 
                                                    <asp:Label ID="Label1" runat="server" Text="Consulta"></asp:Label>
                                                  </td>
                                                  <td>
                                                    <asp:TextBox runat="server" ID="txtConsulta" Class="TexboxNormal" Width = "500px" Height ="30px" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:Image ID="ImgCargarColor" runat="server" BorderColor="Black" Width = "35px"  Height ="30px" BorderWidth="1" />
                                                    <asp:Button  ID="btnObtenerAlisto" runat ="server" Text= ">>"     Width = "50px"  Height ="20px" OnClick ="btnConsulta_Click"/>
                                                    <asp:FileUpload ID="FUpld" runat ="server" Width ="200px" on />
                                                    <asp:DropDownList ID="ddlIDUSUARIO" Class="TexboxNormal" runat="server" Width="300px" AutoPostBack="True" Visible ="false" ></asp:DropDownList>
                                                  </td>

                                                </tr>
                                                    <h1></h1>
                                                    </table>     
                                                </ContentTemplate> 
                                                 <Triggers>
                                                   <asp:PostBackTrigger ControlID="btnObtenerAlisto" />
                                                </Triggers>
                                            </asp:UpdatePanel>                         
                                            </asp:Panel>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>.
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

