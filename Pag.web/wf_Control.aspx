<%@ Page Title="WMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wf_Control.aspx.cs" Inherits="Diverscan.MJP.UI.wf_Control" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <ajaxToolkit:ModalPopupExtender TargetControlID="btnPruebaModal" ID="modalPass" runat="server"
        Enabled="True" PopupControlID="pnModal" DropShadow="True"
        RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="bg_Page">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            
            <div align="center">

                <asp:Panel runat="server" ID="pnModal" Visible="False">
                    <br/>
                    <asp:Panel runat="server" ID="cambioClave" Visible="False">
                        <div align="center">
                            <table class="tablePosicion">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbBienvenida" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="margin-top: 3%">
                                    <td>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text="Ingrese la contraseña:" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPass1" CssClass="NormalTextBox" Width="200px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Ingrese nuevamente su contraseña:" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPass2" CssClass="NormalTextBox" Width="200px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="margin-top: 3%">
                                    <td align="center">
                                        <br />
                                        <asp:Button runat="server" ID="btnCambiarPass" Text="Guardar" CssClass="myButtonWhite" Width="150px" OnClick="btnCambiarPass_OnClick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <br />
                                        <asp:Label runat="server" ID="lbEstado" Font-Bold="True" ForeColor="#FF0000"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="cambioPrograma" Visible="False">
                        <div align="center">
                            <table style="margin-top: 3%">
                                <tr>
                                    <td align="center" style="text-align: center">
                                        <asp:Image runat="server" ID="imgLodoMJP"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; color: white">-- Seleccione con cual programa iniciara --
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList runat="server" ID="ddlListaProgramas" Width="305px" Height="30px" />
                                    </td>
                                </tr>
                                <tr style="margin-top: 5%">
                                    <td align="center">
                                        <br />

                                        <asp:Button runat="server" ID="btnCerrarModal" Text="Siguiente >>" OnClick="btnCerrarModal_OnClick" CssClass="myButtonWhite" Width="200px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>

                </asp:Panel>

                <asp:Panel runat="server" ID="pnPermisos" Visible="False">
                    <table>
                        <tr>
                            <td align="center" style="text-align: center">
                                <asp:Image runat="server" ID="logoModalMJPA"/>
                            </td>
                        </tr>
                        <br />
                        <tr>
                            <td>
                                <h3>
                                    <br />
                                    Este usuario no cuenta con los permisos para ingresar a esta página<br />
                                    consulte con el administrador cuales son sus permisos de navegación
                                </h3>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCambiarPass"/>
        </Triggers>
    </asp:UpdatePanel>

    <asp:Button runat="server" ID="btnPruebaModal" Enabled="False" Width="0px" Height="0px" BorderWidth="0px" BackColor="#FFFFFF" />
    <asp:Button runat="server" ID="btnPruebaModal2" Enabled="False" Width="0px" Height="0px" BorderWidth="0px" BackColor="#FFFFFF" />
</asp:Content>
