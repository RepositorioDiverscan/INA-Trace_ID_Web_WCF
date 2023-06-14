using Diverscan.MJP.Entidades.Reportes;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Diverscan.MJP.UI.Reportes.SolicitudDevolucion
{
    public partial class SolicitudDevolucionAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var opcion = Request.Form["opcion"]; //Se recibe una opcion para ejecutar una sentencia

            //Llamamos a la capa de Negocio
            n_ReporteSolicitudDevolucion n_Reporte = new n_ReporteSolicitudDevolucion();

            //Casos para realizar un accion segun la opcion
            switch (opcion)
            {
                //Obtener la lista de bodegas
                case "CargarBodegas":

                    //Almacenar en una lista 
                    List<e_bodega> listaBodegas = n_Reporte.CargarBodegas();
                    //Convertir la informacion en un JSON
                    var jsonStrigCargarBodegas = new JavaScriptSerializer();
                    var jsonStringResultCargarBodegas = jsonStrigCargarBodegas.Serialize(listaBodegas);
                    //Enviar el JSON con la informacion de la BD
                    Response.Write(jsonStringResultCargarBodegas);
                    break;


                //Metodo para obtener el reporte
                case "ObtenerReporteSolicitud":

                    //obtener los parámetros
                    var fechaInicio = Convert.ToDateTime(Request.Form["FechaInicio"]);
                    var fechaFin = Convert.ToDateTime(Request.Form["FechaFin"]);
                    var idBodega = Convert.ToInt32(Request.Form["IdBodega"]);


                    //Almacenar en una lista el resultado
                    List<e_ReporteSolicitudDevolucion> listaReporte = n_Reporte.ReporteSolicitudDevolucion(fechaInicio, fechaFin, idBodega);

                    //Convertir la informacion en un JSON
                    var jsonStringReporte = new JavaScriptSerializer();
                    var jsonStringResultReporte = jsonStringReporte.Serialize(listaReporte);

                    //Enviar el JSON con la informacion de BD
                    Response.Write(jsonStringResultReporte);
                    break;
            }
        }
    }
}