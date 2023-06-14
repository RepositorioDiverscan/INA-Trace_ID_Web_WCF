<%@ Page Title="WMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wf_Doc_Y_Infor.aspx.cs" Inherits="Diverscan.MJP.UI.wf_Doc_Y_Infor" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <fieldset>
        <table>
            <tr>
                <td style="padding-bottom: 0%">
                    <a href="Documentos/Documentos/Manual MJP Administrador de Almacen v2.pdf" style="padding: 1px; color: black; text-decoration: none">
                        <asp:Image runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual administrador de almacén
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP Carga Inicial v1.pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual Carga inicial
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP HH v1 .pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual HandHeld
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP Ingresos v1.pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual de ingresos
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP Programas.pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual usuarios programa
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP Salidas v1.pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual salidas
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 0%">
                    <br />
                    <a href="Documentos/Documentos/Manual MJP Reportes v1.pdf" style="color: black; text-decoration: none">
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Logos/Descarga.png" Width="50px" ToolTip="Descargar" /></a>
                    Manual de reportes
                </td>
            </tr>
        </table>
    </fieldset>

</asp:Content>
