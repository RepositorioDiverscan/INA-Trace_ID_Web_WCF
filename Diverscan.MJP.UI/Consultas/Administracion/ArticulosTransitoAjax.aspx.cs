using Diverscan.MJP.AccesoDatos.TransitoBodega;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Consultas.Administracion
{
    public partial class ArticulosTransitoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //recibe la opcion para ejecutar la setencia 
            var opcion = Request.Form["opcion"];

            //case para validar la accion segun la opcion
            switch (opcion)
            {
                case "ObtenerArticuloTransito":
                    //Se invoca a la clase 
                    DArticuloTransitoBodega dArticulo = new DArticuloTransitoBodega();
                    int idBodega = Convert.ToInt32(Request.Form["idBodega"]);

                    //almacenamos en una lista 
                    List<EArticuloTransitoBodega> listaArticulo = dArticulo.BuscarArticulosTransitoBodega(idBodega);
                    //convertir la informacion en un json
                    var jsonStringArticulo= new JavaScriptSerializer();
                    jsonStringArticulo.MaxJsonLength = 2147483644;
                    var jsonStringResultArticulo= jsonStringArticulo.Serialize(listaArticulo);

                    //Enviar el JSON con la informacion de la BD 
                    Response.Write(jsonStringResultArticulo);
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