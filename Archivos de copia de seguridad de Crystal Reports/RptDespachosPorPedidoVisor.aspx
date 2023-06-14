<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDespachosPorPedidoVisor.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.RptDespachosPorPedido.RptDespachosPorPedido" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte [Despacho por Solicitud]</title>
    <%--Carga de estilos para visualizar el reporte--%>
    <script lang="javaScript" type="text/javascript" src="../../crystalreportviewers13/js/crviewer/crv.js"></script>
    <%--Bootstrap--%>
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>--%>
    <link rel="stylesheet" href="../../Bootstrap/bootstrap.min.css">
    <script src="../../Bootstrap/bootstrap.min.js"></script>
</head>
<%--<body style="background-color: #7DBAC1">--%>
<body class="container" style="background-color:aliceblue">
    <form id="form1" runat="server">
        <%--<div class="container">--%>
        <%--<div class="row"></div>--%>
        <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/Logos/TraceID2.png" Width="200px" />
        <asp:Button ID="btnRegresar" class="btn btn-primary" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
        <div class="row">
            <div class="col-sm-12">
                <CR:CrystalReportViewer ID="CRDespachoPorSolicitud" runat="server" AutoDataBind="true" ToolPanelView="None" />
            </div>
        </div>
        <%--</div>--%>
    </form>
</body>
</html>

