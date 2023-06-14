<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_Credenciales.aspx.cs"
    Inherits="Diverscan.MJP.UI.Administracion.wf_Credenciales" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Control de Bodegas</title>
    <link href="~/Styles/Login.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/StyleButton.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/StyleTextBox.css" rel="stylesheet" type="text/css" />

   <script type="text/javascript">
        window.moveTo(0,0);
            if (document.all) {
            top.window.resizeTo(screen.availWidth,screen.availHeight);
            }
            else if (document.layers||document.getElementById) {
            if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth){
            top.window.outerHeight = screen.availHeight;
            top.window.outerWidth = screen.availWidth;
            }
            }
    </script>
</head>
<body id="CuerpoHtml" runat="server">
    <form id="Form1" runat="server">
          <telerik:RadScriptManager ID="ManejadorDeScriptsLista" Runat="server"></telerik:RadScriptManager>
        <telerik:RadWindowManager ID="ManejadorDeVentanasLista" runat="server" Style="z-index: 20000"></telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="ManejadorAjaxLoadingLista" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
        <%-- <script type="text/javascript" src="http://counter5.01counter.com/private/countertab.js?c=2cd5700c94e5998b8b0860d27beba2a7"></script>--%>
        <div>
            <div class="page">
                <div class="header" id="cabezera" runat="server">
                    <div class="title">
                      <div class="loginDisplay">
                            <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                            <LoggedInTemplate>
                                Welcome <span class="bold"><asp:LoginName ID="HeadLoginName" runat="server" />                              </span>! [
                                <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out"
                                LogoutPageUrl="~/" />
                                ]
                            </LoggedInTemplate>
                            </asp:LoginView>
                        </div><%--End loginDisplay--%>
                            <table class="LogoCrede">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ID="imgLodoMJP"/>
                                    </td>
                                </tr>
                        </table><%--End tblLogo--%>
                    </div><%--End title--%>
                    <div class="clear hideSkiplink">
                        <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                            IncludeStyleBlock="false" Orientation="Horizontal">
                            <Items>
                                <%--<asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home" />--%>
                            </Items>
                        </asp:Menu>
                    </div>
                </div>
                <div class="main">
                    <div align="center">
                        <table class="Table1" >
                            <tr>
                                <td>
                                    <table class="Table2">
                                        <tr>
                                            <td>
                                                <img src="../Images/diverscan.png" id="LogoD" alt="Diverscan"/><br />
                                                <asp:Label runat="server" ID="lblTitUusuario" Text="Usuario"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox CssClass="NormalTextBox" ID="RadtxtUsuario" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblTitPass" Text="Contraseña"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox CssClass="NormalTextBox" ID="RadtxtContrasenna"  TextMode="Password" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <telerik:RadImageButton Class="RadbtnLogin" runat="server" ID="RadbtnLogin" OnClick="IngresarClick"  Text="Aceptar"></telerik:RadImageButton>                                             
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="footer">
                
                    <%--<strong>Version 1.0.1</strong>--%>
                    <strong><asp:Label ID="lblCompiledVersion" runat="server" ></asp:Label></strong>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Distribuidora Panal" Font-Bold="true" Font-Italic="true" ForeColor="black" 
                        BackColor="WhiteSmoke" Font-Underline="true"></asp:Label>
            </div>
            </div>
            
        </div>
    </form>
</body>
</html>
