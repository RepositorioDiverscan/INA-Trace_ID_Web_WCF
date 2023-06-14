using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.ArticulosDisponiblesBodega
{
    public partial class ArticulosDisponiblesAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // recibe la opcion para ejecutar la sentencia
            var opcion = Request.Form["opcion"];
            //case para validar la accion segun la opcion
            switch (opcion)
            {
                case "ObtenerArticulosDisponibles":
                    //invocamos a la capa negocio
                    n_ArticulosDisponiblesBodega nArticulos = new n_ArticulosDisponiblesBodega();
                    int idBodega = Convert.ToInt32(Request.Form["idBodega"]);
                    //almacenar en una lista
                    List<e_ArticulosDisponiblesBodega> listaArticulos = nArticulos.ConsultarArticulosDisponiblesBodegas(idBodega);
                    //Convertir la informacion en un JSONf1
                    var jsonStringArticulos = new JavaScriptSerializer();
                    jsonStringArticulos.MaxJsonLength = 2147483644;
                    var jsonStringResultDevolucion = jsonStringArticulos.Serialize(listaArticulos);
                    //Enviar el JSON con la informacion de la BD
                    Response.Write(jsonStringResultDevolucion);
                    break;
                case "CargarBodegas":
                    //invocar a la capa de Negocio
                    n_ArticulosDisponiblesBodega nArticulo = new n_ArticulosDisponiblesBodega();
                    //Almacenar en una lista 
                    List<EBodegas> listaBodegas = nArticulo.CargarBodegas();
                    //Convertir la informacion en un JSON
                    var jsonStringAlertaDevolucion = new JavaScriptSerializer();
                    //jsonStringAlertaBodegas.MaxJsonLength = 2147483644;
                    var jsonStringResultAlertaDevolucion = jsonStringAlertaDevolucion.Serialize(listaBodegas);
                    //Enviar el JSON con la informacion de la BD
                    Response.Write(jsonStringResultAlertaDevolucion);
                    break;
            }
        }
    }
}