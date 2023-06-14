using Diverscan.MJP.AccesoDatos.Operaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.Ingresos
{
    public partial class SinOrdenCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // se instancia a la capa
            IngresosSinOrdenesCompras orden = new IngresosSinOrdenesCompras();
            //se obtiene la opcion a ejecutar
            var opcion = Request.Form["opcion"];


            switch (opcion)
            {
                // case para cargar la orden de compra 
                case "ObtenerSinOrdenCompra":
                    int idBodega = Convert.ToInt32(Request.Form["idBodega"]);
                    DateTime f1 = Convert.ToDateTime(Request.Form["f1"]);
                    DateTime f2 = Convert.ToDateTime(Request.Form["f2"]);
                    string numero = Convert.ToString(Request.Form["numero"]);
                    var sinOrdenCompra = orden.SinOrdenCompras(idBodega, f1, f2, numero);
                    Response.Write(JsonConvert.SerializeObject(sinOrdenCompra, Formatting.Indented));
                    break;

                //opcion para cargar detalle de la orden de compra
                case "ObtenerDetalleSinOrdenCompra":

                    int idMaestroArticulo = Convert.ToInt32(Request.Form["idMaestroArticulo"]);
                    var sinOrdenCompraDetalle = orden.ObtenerDetalleSinOrdenCompras(idMaestroArticulo);
                    Response.Write(JsonConvert.SerializeObject(sinOrdenCompraDetalle, Formatting.Indented));
                    break;

            }

        }

    }
}