<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDespachosPorPedidoVisorF.aspx.cs" Inherits="Diverscan.MJP.UI.Reportes.RptDespachosPorPedido.RptDespachosPorPedidoVisorJS" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Bootstrap/bootstrap.min.css" />
    <script src="../../Bootstrap/bootstrap.min.js"></script>
</head>
<script type="text/javascript">
    //Imprimir divs
    function imprimirDiv(divID) {
        //Get the HTML of div
        var divElements = document.getElementById(divID).innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;

        //Reset the page's HTML with div's HTML only
        document.body.innerHTML =
            "<html><head><title></title></head><body>" +
            divElements + "</body>";

        //Print Page
        window.print();

        //Restore orignal HTML
        document.body.innerHTML = oldPage;
    }        
</script>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <%--Logo y Botones --%>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/Logos/TraceID2.png" Width="200px" />
                    <asp:Button ID="btnRegresar" CssClass="btn btn-primary" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                    <span></span>
                    <input type="button" class="btn btn-default" value="Imprimir Reporte" onclick="javascript: imprimirDiv('reporteDespacho')" />
                </div>
            </div>
            <br />
            <%--Datos a imprimir--%>
            <div id="reporteDespacho">
                <%--Fecha Impresión || Número Solicitud--%>
                <div class="row">
                    <div class="col-sm-4">
                        <p class="text-left" style="font-size: smaller">Fecha impresión: <%=datosGeneralesRpt.FechaImpresion%></p>
                    </div>
                    <div class="col-sm-4" style="background-color: white;"></div>
                    <div class="col-sm-4" style="background-color: white;">
                        <p class="text-right" style="font-size: small"><b><%=datosGeneralesRpt.IdInternoSolicitudSAP%></b></p>
                    </div>
                </div>
                <%--Encabezado Reporte || Título principal, Fecha Solicitud--%>
                <div class="row">
                    <div class="col-sm-2" style="background-color: white;"></div>
                    <div class="col-sm-8" style="background-color: white;">
                        <p class="text-center" style="font-size: larger"><b><%=datosGeneralesRpt.TituloPrincipal%></b></p>
                    </div>
                    <div class="col-sm-2" style="background-color: white;">
                        <p class="text-right" style="font-size: smaller"><%=datosGeneralesRpt.FechaSolicitud%></p>
                    </div>
                </div>
                <%--Cliente--%>
                <br />
                <div class="row">
                    <div class="col-sm-12" style="background-color: white;">
                        <p class="text-left" style="font-size: medium"><b>Cliente: <%=datosGeneralesRpt.ClienteDestino%></b></p>
                    </div>
                </div>
                <%--Detalles del Reporte--%>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:GridView CssClass="table table-bordered" ID="GVReporte" runat="server" Font-Size="Smaller">
                        </asp:GridView>
                    </div>
                </div>
                <%--Pie de Reporte--%>
                <div class="row">
                    <div class="col-sm-12">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col" class="text-left">Cantidad Líneas: <%=datosGeneralesRpt.CantidadLineas%></th>
                                    <th scope="col" class="text-right">Total: <%=datosGeneralesRpt.TotalCostos%></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
